using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using PT.MarketDataService.Core.DomainServices;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using Swashbuckle.Application;

namespace PT.MarketDataService.Application.Owin
{
    public class OwinStartup
    {
        private readonly Container _container;
        private readonly IAppConfig _appConfig;

        public OwinStartup(Container container, IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _container = container;
        }

        public void Initialize()
        {
            var server = WebApp.Start(_appConfig.WebApiListenAddress, (appBuilder) =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    using (AsyncScopedLifestyle.BeginScope(_container))
                    {
                        await next();
                    }
                });
                // Configure Web API for self-host. 
                HttpConfiguration config = new HttpConfiguration();

                // Attribute routing.
                config.MapHttpAttributeRoutes();

                // Swashbuckle
                config.EnableSwagger(c => c.SingleApiVersion("v1", "MarketDataService API"))
                      .EnableSwaggerUi();

                // SimpleInjector
                config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(_container);

                appBuilder.UseWebApi(config);
            });
        }
    }
}