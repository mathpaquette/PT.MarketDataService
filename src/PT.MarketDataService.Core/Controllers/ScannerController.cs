using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NLog;
using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Events;
using PT.MarketDataService.Core.Extensions;
using PT.MarketDataService.Core.Models;
using PT.MarketDataService.Core.Providers;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Core.Controllers
{
    public class ScannerController
    {
        public event EventHandler<ScannerChangeEventArgs> ScannerChange;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly ActionBlock<ScannerRequest> _scannerRequestsQueue;
        private readonly IMarketDataProvider _marketDataProvider;
        private readonly IScannerRequestService _scannerRequestService;
        private readonly IScannerRepositoryFactory _scannerRepositoryFactory;
        private readonly ITimeProvider _timeProvider;


        public ScannerController(
            IMarketDataProvider marketDataProvider,
            IScannerRequestService scannerRequestService,
            IScannerRepositoryFactory scannerRepositoryFactory,
            ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _scannerRepositoryFactory = scannerRepositoryFactory;
            _scannerRequestService = scannerRequestService;
            _marketDataProvider = marketDataProvider;

            _scannerRequestsQueue = new ActionBlock<ScannerRequest>(
                async request => await ProcessRequest(request), 
                new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 2 });
        }

        public void Initialize()
        {
            InitializeScannerRequests();
        }

        private async Task ProcessRequest(ScannerRequest request)
        {
            if (!request.IsOnline())
            {
                Logger.Info("ScannerParameter: {0} is OFFLINE. Scheduled on: {1}", request.Parameter.Id, _timeProvider.Now + request.UntilExpiration);
                request.Signal();
                NotifyScannerChanges(request, new Scanner());
                return;
            }

            // fetch the scanner from the market
            Logger.Info("Requesting scanner for ScannerParameter: {0}...", request.Parameter.Id);
            var scanner = await _marketDataProvider.GetScannerAsync(request.Parameter);
            Logger.Info("Received scanner for ScannerParameter: {0}", request.Parameter.Id);

            request.Signal();

            // Notify changes
            NotifyScannerChanges(request, scanner);

            // save the to database
            PersistScanner(scanner, request.Parameter.Id);
        }

        private void NotifyScannerChanges(ScannerRequest request, Scanner current)
        {
            var scannerChanges = request.GetScannerChanges(current);
            ScannerChange.RaiseEvent(this, new ScannerChangeEventArgs(request.Parameter.Id, scannerChanges));
        }

        private void PersistScanner(Scanner scanner, int scannerParamId)
        {
            using (var scannerRepository = _scannerRepositoryFactory.CreateNew())
            {
                try
                {
                    scanner.ParameterId = scannerParamId;
                    scannerRepository.Add(scanner);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        private void InitializeScannerRequests()
        {
            var scannerRequests = _scannerRequestService.GetScannerRequests().ToList();
            foreach (var scannerRequest in scannerRequests)
            {
                scannerRequest.Timeout += ScannerRequestOnTimeout;
                scannerRequest.Start();

                _scannerRequestsQueue.Post(scannerRequest);
            }
        }
        private void ScannerRequestOnTimeout(object sender, EventArgs eventArgs)
        {
            _scannerRequestsQueue.Post((ScannerRequest)sender);
        }
    }
}