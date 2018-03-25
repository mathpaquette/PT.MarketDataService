using System;

namespace PT.MarketDataService.Core.Entities
{
    /// <summary>
    /// Represent a market scanner request
    /// </summary>
    public class Scanner
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public ScannerParameter Parameter { get; set; }
        public string Symbols { get; set; }
    }
}