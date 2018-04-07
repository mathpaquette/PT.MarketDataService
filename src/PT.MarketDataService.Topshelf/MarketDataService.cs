using PT.MarketDataService.Core.Controllers;
using PT.MarketDataService.Core.DomainServices;

namespace PT.MarketDataService.Topshelf
{
    public class MarketDataService
    {
        private readonly ScannerController _scannerController;
        private readonly IMarketDataProvider _marketDataProvider;
        private readonly Level1Controller _level1Controller;

        public MarketDataService(
            IMarketDataProvider marketDataProvider,
            ScannerController scannerController,
            Level1Controller level1Controller)
        {
            _level1Controller = level1Controller;
            _marketDataProvider = marketDataProvider;
            _scannerController = scannerController;
        }

        public async void Start()
        {
            // add log here
            await _marketDataProvider.InitializeAsync();

            _level1Controller.Initialize();
            _scannerController.Initialize();
        }

        public void Stop()
        {
            // add log here
        }
    }
}