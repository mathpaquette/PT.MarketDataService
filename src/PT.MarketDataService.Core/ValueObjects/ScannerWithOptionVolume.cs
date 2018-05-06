using System;

namespace PT.MarketDataService.Core.ValueObjects
{
    public class ScannerWithOptionVolume
    {
        public DateTime Timestamp { get; set; }
        public string Symbol { get; set; }
        public int? OptionVolume { get; set; }
    }
}