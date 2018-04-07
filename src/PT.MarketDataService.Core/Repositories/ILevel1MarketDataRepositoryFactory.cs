namespace PT.MarketDataService.Core.Repositories
{
    public interface ILevel1MarketDataRepositoryFactory
    {
        ILevel1MarketDataRepository CreateNew();
    }
}