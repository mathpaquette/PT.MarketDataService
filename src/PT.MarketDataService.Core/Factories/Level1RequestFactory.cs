using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Core.Models;
using PT.MarketDataService.Core.Providers;

namespace PT.MarketDataService.Core.Factories
{
    public class Level1RequestFactory : ILevel1RequestFactory
    {
        private readonly IAppConfig _appConfig;
        private readonly ITimeProvider _timeProvider;

        public Level1RequestFactory(IAppConfig appConfig, ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _appConfig = appConfig;
        }

        public Level1Request CreateNew(string symbol)
        {
            return new Level1Request(symbol, _appConfig.Level1RequestFrequencySec, _timeProvider);
        }
    }
}