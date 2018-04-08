using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NLog;
using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Enums;
using PT.MarketDataService.Core.Events;
using PT.MarketDataService.Core.Models;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Core.Controllers
{
    public class Level1Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMarketDataProvider _marketDataProvider;
        private readonly ScannerController _scannerController;
        private readonly Dictionary<Contract, Level1Request> _level1RequestsByContract;
        private readonly ActionBlock<Level1Request> _level1RequestQueue;

        private readonly int _level1RequestFrequency;
        private readonly Level1MarketDataService _level1MarketDataService;

        public Level1Controller(
            IMarketDataProvider marketDataProvider,
            ScannerController scannerController,
            Level1MarketDataService level1MarketDataService,
            IAppConfig appConfig)
        {
            _level1MarketDataService = level1MarketDataService;
            _level1RequestFrequency = appConfig.Level1RequestFrequencySec;

            _scannerController = scannerController;
            _marketDataProvider = marketDataProvider;
            _level1RequestsByContract = new Dictionary<Contract, Level1Request>();

            _level1RequestQueue = new ActionBlock<Level1Request>(
                async request => await ProcessRequest(request),
                new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 5 });
        }

        public void Initialize()
        {
            _scannerController.ScannerChange += ScannerControllerOnScannerChange;
        }

        private async Task ProcessRequest(Level1Request request)
        {
            if (!request.Online)
                return;

            if (!request.HasExpired())
            {
                Logger.Info("Not expired level 1 request for Symbol: {0}... ({1})", request.Contract, _level1RequestQueue.InputCount);
                request.Signal();
                return;
            }

            // make a market request
            Logger.Info("Requesting level 1 for Symbol: {0}... ({1})", request.Contract, _level1RequestQueue.InputCount);
            var level1MarketData = await _marketDataProvider.GetLevel1MarketDataAsync(request.Contract);

            request.Signal();

            // persist the data
            try
            {
                _level1MarketDataService.Save(level1MarketData);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ScannerControllerOnScannerChange(object sender, ScannerChangeEventArgs args)
        {
            foreach (var scannerChange in args.ScannerChanges)
            {
                switch (scannerChange.Type)
                {
                    case ScannerChangeType.Added:
                        CreateLevel1Request(args.ScannerParameterId, scannerChange.Contract);
                        break;
                    case ScannerChangeType.Removed:
                        RemoveLevel1Request(args.ScannerParameterId, scannerChange.Contract);
                        break;
                }
            }
        }

       private void CreateLevel1Request(int scannerParameterId, Contract contract)
        {
            lock (_level1RequestsByContract)
            {
                if (!_level1RequestsByContract.TryGetValue(contract, out var request))
                {
                    request = new Level1Request(contract, _level1RequestFrequency);
                    request.Timeout += RequestOnTimeout;
                    _level1RequestsByContract.Add(contract, request);
                }

                if (request.TrySetOnline(scannerParameterId))
                {
                    request.Start();
                    _level1RequestQueue.Post(request);
                    Logger.Info("Symbol: {0} is ONLINE with Parameter: {1}", contract, scannerParameterId);
                }
            }
        }

        private void RemoveLevel1Request(int scannerParameterId, Contract contract)
        {
            lock (_level1RequestsByContract)
            {
                if (_level1RequestsByContract.TryGetValue(contract, out var request))
                {
                    if (request.TrySetOffline(scannerParameterId))
                    {
                        request.Stop();
                        Logger.Info("Symbol: {0} is OFFLINE with Parameter: {1}", contract, scannerParameterId);
                    }
                }
                else
                {
                    Logger.Error($"Trying to remove unexisting Symbol: {contract} on ScannerParameter: {scannerParameterId}");
                }
            }
        }

        private void RequestOnTimeout(object sender, EventArgs eventArgs)
        {
            _level1RequestQueue.Post((Level1Request)sender);
        }
    }
}