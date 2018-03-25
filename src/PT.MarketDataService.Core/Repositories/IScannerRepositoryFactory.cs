namespace PT.MarketDataService.Core.Repositories
{
    public interface IScannerRepositoryFactory
    {
        IScannerRepository CreateNew();
    }
}