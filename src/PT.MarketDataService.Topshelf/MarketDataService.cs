using PT.MarketDataService.Application.Owin;
using PT.MarketDataService.Core.Controllers;
using PT.MarketDataService.Core.DomainServices;

namespace PT.MarketDataService.Topshelf
{
    public class MarketDataService
    {
        private readonly ScannerController _scannerController;
        private readonly IMarketDataProvider _marketDataProvider;
        private readonly Level1Controller _level1Controller;
        private readonly OwinStartup _owinStartup;
        private readonly IAppConfig _appConfig;

        public MarketDataService(
            IMarketDataProvider marketDataProvider,
            ScannerController scannerController,
            Level1Controller level1Controller,
            OwinStartup owinStartup,
            IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _level1Controller = level1Controller;
            _marketDataProvider = marketDataProvider;
            _scannerController = scannerController;
            _owinStartup = owinStartup;
        }

        public async void Start()
        {
            // add log here
            if (_appConfig.EnableMarketDataCollector)
            {
                await _marketDataProvider.InitializeAsync();
                _level1Controller.Initialize();
                _scannerController.Initialize();
            }

            if (_appConfig.EnableWebApi)
            {
                _owinStartup.Initialize();
            }
        }

        public void Stop()
        {
            // add log here
        }
    }
}