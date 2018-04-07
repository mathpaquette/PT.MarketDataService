using PT.MarketDataService.Core.Controllers;
using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Core.Providers;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PT.MarketDataService.Core
{
    public class CoreModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ScannerController>(Lifestyle.Singleton);
            container.Register<Level1Controller>(Lifestyle.Singleton);
            container.Register<ITimeProvider, DefaultTimeProvider>(Lifestyle.Singleton);
            container.Register<IScannerRequestService, ScannerRequestService>();
        }
    }
}