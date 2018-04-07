using PT.MarketDataService.Core.Enums;

namespace PT.MarketDataService.Core.ValueObjects
{
    public class ScannerChange
    {
        public ScannerChange(ScannerChangeType type, string symbol)
        {
            Type = type;
            Symbol = symbol;
        }

        public ScannerChangeType Type { get; }
        public string Symbol { get; }
    }
}