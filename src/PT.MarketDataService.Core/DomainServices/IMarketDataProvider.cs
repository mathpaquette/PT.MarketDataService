using System.Threading.Tasks;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Core.DomainServices
{
    public interface IMarketDataProvider
    {
        Task InitializeAsync();
        Task<Scanner> GetScannerAsync(ScannerParameter scannerParameter);
        Task<Level1MarketData> GetLevel1MarketDataAsync(string symbol);
    }
}