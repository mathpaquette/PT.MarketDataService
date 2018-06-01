using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NLog;
using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Core.Enums;
using PT.MarketDataService.Core.Events;
using PT.MarketDataService.Core.Factories;
using PT.MarketDataService.Core.Models;

namespace PT.MarketDataService.Core.Controllers
{
    public class Level1Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMarketDataProvider _marketDataProvider;
        private readonly ILevel1MarketDataService _level1MarketDataService;
        private readonly ScannerController _scannerController;
        private readonly Dictionary<string, Level1Request> _level1RequestsBySymbol;
        private readonly ActionBlock<Level1Request> _level1RequestQueue;
        private readonly ILevel1RequestFactory _level1RequestFactory;

        public Level1Controller(
            IMarketDataProvider marketDataProvider,
            ILevel1MarketDataService level1MarketDataService,
            ILevel1RequestFactory level1RequestFactory,
            ScannerController scannerController)
        {
            _level1RequestFactory = level1RequestFactory;
            _marketDataProvider = marketDataProvider;
            _level1MarketDataService = level1MarketDataService;
            _scannerController = scannerController;

            _level1RequestsBySymbol = new Dictionary<string, Level1Request>();

            _level1RequestQueue = new ActionBlock<Level1Request>(
                async request =>
                {
                    try { await ProcessRequest(request); }
                    catch (Exception e) { Logger.Error(e); }
                },
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
                Logger.Info("Not expired level 1 request for Symbol: {0}... ({1}), Until next expiration: {2} ms", 
                    request.Symbol, _level1RequestQueue.InputCount, request.UntilExpiration.TotalMilliseconds);
                request.Signal();
                return;
            }

            // make a market request
            Logger.Info("Requesting level 1 for Symbol: {0}... ({1})", request.Symbol, _level1RequestQueue.InputCount);
            var level1MarketData = await _marketDataProvider.GetLevel1MarketDataAsync(request.Symbol);

            request.Signal();

            // persist the data
            _level1MarketDataService.PersistLevel1MarketData(level1MarketData);
        }

        private void ScannerControllerOnScannerChange(object sender, ScannerChangeEventArgs args)
        {
            foreach (var scannerChange in args.ScannerChanges)
            {
                switch (scannerChange.Type)
                {
                    case ScannerChangeType.Added:
                        CreateLevel1Request(args.ScannerParameterId, scannerChange.Symbol);
                        break;
                    case ScannerChangeType.Removed:
                        RemoveLevel1Request(args.ScannerParameterId, scannerChange.Symbol);
                        break;
                }
            }

            if (!args.ScannerOnline)
            {
                CleanUpLevel1Request();
            }
        }

        private void CleanUpLevel1Request()
        {
            lock (_level1RequestsBySymbol)
            {
                var requests = _level1RequestsBySymbol.Values.ToList();
                foreach (var request in requests)
                {
                    if (request.Online)
                        continue;

                    request.Timeout -= RequestOnTimeout;
                    _level1RequestsBySymbol.Remove(request.Symbol);
                    Logger.Info("Symbol: {0} is completely OFFLINE ({1})", request.Symbol, _level1RequestsBySymbol.Count);
                }
            }
        }

       private void CreateLevel1Request(int scannerParameterId, string symbol)
        {
            lock (_level1RequestsBySymbol)
            {
                if (!_level1RequestsBySymbol.TryGetValue(symbol, out var request))
                {
                    request = _level1RequestFactory.CreateNew(symbol);
                    request.Timeout += RequestOnTimeout;
                    _level1RequestsBySymbol.Add(symbol, request);
                }

                if (request.TrySetOnline(scannerParameterId))
                {
                    request.Start();
                    _level1RequestQueue.Post(request);
                    Logger.Info("Symbol: {0} is ONLINE with Parameter: {1}", symbol, scannerParameterId);
                }
            }
        }

        private void RemoveLevel1Request(int scannerParameterId, string symbol)
        {
            lock (_level1RequestsBySymbol)
            {
                if (_level1RequestsBySymbol.TryGetValue(symbol, out var request))
                {
                    if (request.TrySetOffline(scannerParameterId))
                    {
                        request.Stop();
                        Logger.Info("Symbol: {0} is OFFLINE with Parameter: {1}", symbol, scannerParameterId);
                    }
                }
                else
                {
                    Logger.Error($"Trying to remove unexisting Symbol: {symbol} on ScannerParameter: {scannerParameterId}");
                }
            }
        }

        private void RequestOnTimeout(object sender, EventArgs eventArgs)
        {
            _level1RequestQueue.Post((Level1Request)sender);
        }
    }
}