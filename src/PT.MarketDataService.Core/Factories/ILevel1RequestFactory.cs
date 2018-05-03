using PT.MarketDataService.Core.Models;

namespace PT.MarketDataService.Core.Factories
{
    public interface ILevel1RequestFactory
    {
        Level1Request CreateNew(string symbol);
    }
}