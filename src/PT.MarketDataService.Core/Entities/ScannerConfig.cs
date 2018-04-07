using System;
using System.Collections.Generic;

namespace PT.MarketDataService.Core.Entities
{
    public class ScannerConfig
    {
        public ScannerConfig()
        {
            Parameters = new HashSet<ScannerParameter>();
        }

        public int Id { get; set; }
        public bool Enable { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ICollection<ScannerParameter> Parameters { get; set; }
        
        /// <summary>
        /// Frequency in seconds
        /// </summary>
        public int Frequency { get; set; }
    }
}