using System.Configuration;
using PT.MarketDataService.Core.DomainServices;

namespace PT.MarketDataService.Infrastructure.DomainServices
{
    public class AppConfig : IAppConfig
    {
        public AppConfig()
        {
            IbHost = ConfigurationManager.AppSettings["IbHost"];
            IbPort = int.Parse(ConfigurationManager.AppSettings["IbPort"]);
            IbClientId = int.Parse(ConfigurationManager.AppSettings["IbClientId"]);
            Level1RequestFrequencySec = int.Parse(ConfigurationManager.AppSettings["Level1RequestFrequencySec"]);
            EnableMarketDataCollector = bool.Parse(ConfigurationManager.AppSettings["EnableMarketDataCollector"]);
            EnableWebApi = bool.Parse(ConfigurationManager.AppSettings["EnableWebApi"]);
            WebApiListenAddress = ConfigurationManager.AppSettings["WebApiListenAddress"];
        }

        public string IbHost { get; }
        public int IbPort { get; }
        public int IbClientId { get; }
        public int Level1RequestFrequencySec { get; }
        public bool EnableMarketDataCollector { get; }
        public bool EnableWebApi { get; }
        public string WebApiListenAddress { get; }
    }
}