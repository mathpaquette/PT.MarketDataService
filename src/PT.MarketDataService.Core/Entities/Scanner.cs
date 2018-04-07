using System;
using System.Collections.Generic;

namespace PT.MarketDataService.Core.Entities
{
    /// <summary>
    /// Represent a market scanner request
    /// </summary>
    public class Scanner
    {
        public Scanner()
        {
            Rows = new List<ScannerRow>();
            Timestamp = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }

        public int ParameterId { get; set; }
        public ScannerParameter Parameter { get; set; }
        public ICollection<ScannerRow> Rows { get; set; }
    }
}