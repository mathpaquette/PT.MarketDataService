using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Core.DomainServices
{
    public interface ILevel1MarketDataService
    {
        void PersistLevel1MarketData(Level1MarketData level1MarketData);
    }
}