using System;

namespace PT.MarketDataService.Core.Entities
{
    public class ScannerConfig
    {

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ScannerParameter Parameter { get; set; }
        
        /// <summary>
        /// Frequency in seconds
        /// </summary>
        public int Frequency { get; set; }
    }
}