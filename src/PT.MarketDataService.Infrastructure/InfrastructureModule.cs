using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Infrastructure.DomainServices;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PT.MarketDataService.Infrastructure
{
    public class InfrastructureModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            // infrastructure
            container.Register<IMarketDataProvider, MarketDataProvider>(Lifestyle.Singleton);
            container.Register<IAppConfig, AppConfig>(Lifestyle.Singleton);
        }
    }
}