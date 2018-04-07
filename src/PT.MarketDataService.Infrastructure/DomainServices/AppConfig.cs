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
        }

        public string IbHost { get; }
        public int IbPort { get; }
        public int IbClientId { get; }
        public int Level1RequestFrequencySec { get; }
    }
}