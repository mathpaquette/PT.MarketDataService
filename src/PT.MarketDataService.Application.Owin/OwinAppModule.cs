using SimpleInjector;
using SimpleInjector.Packaging;

namespace PT.MarketDataService.Application.Owin
{
    public class OwinAppModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<OwinStartup>(Lifestyle.Singleton);
        }
    }
}