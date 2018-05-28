namespace PT.MarketDataService.Core.DomainServices
{
    public interface IAppConfig
    {
        string IbHost { get; }
        int IbPort { get; }
        int IbClientId { get; }
        int Level1RequestFrequencySec { get; }
        bool EnableMarketDataCollector { get; }
        bool EnableWebApi { get; }
        string WebApiListenAddress { get; }
    }
}