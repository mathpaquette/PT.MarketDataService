using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Enums;

namespace PT.MarketDataService.Core.ValueObjects
{
    public class ScannerChange
    {
        public ScannerChange(ScannerChangeType type, Contract contract)
        {
            Type = type;
            Contract = contract;
        }

        public ScannerChangeType Type { get; }
        public Contract Contract { get; }
    }
}