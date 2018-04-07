using System;
using System.Reflection;
using SimpleInjector;

namespace PT.MarketDataService.Topshelf
{
    public class Bootstrapper
    {
        public Container Container { get; }

        public Bootstrapper()
        {
            Container = new Container();
        }

        public void Initialize()
        {
            // Default
            Container.Options.SuppressLifestyleMismatchVerification = true;
            
            // Register modules dynamically
            Assembly.Load("PT.MarketDataService.Application");
            Assembly.Load("PT.MarketDataService.Core");
            Assembly.Load("PT.MarketDataService.Repository.EF");
            Assembly.Load("PT.MarketDataService.Infrastructure");

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Container.RegisterPackages(assemblies);
        }
    }
}