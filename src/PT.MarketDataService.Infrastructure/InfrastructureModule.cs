using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Core.Repositories;
using PT.MarketDataService.Infrastructure.DomainServices;
using PT.MarketDataService.Infrastructure.Repositories;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PT.MarketDataService.Infrastructure
{
    public class InfrastructureModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            // infrastructure
            container.Register<IScannerRepositoryFactory, ScannerRepositoryFactory>();
            container.Register<ILevel1MarketDataRepositoryFactory, Level1MarketDataRepositoryFactory>();
            container.Register<IMarketDataProvider, MarketDataProvider>(Lifestyle.Singleton);
            container.Register<IAppConfig, AppConfig>(Lifestyle.Singleton);
        }
    }
}