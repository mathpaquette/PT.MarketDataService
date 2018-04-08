using System;

namespace PT.MarketDataService.Core.Entities
{
    public class Level1MarketData
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public int? BidSize { get; set; }
        public double? Bid { get; set; }
        public int? AskSize { get; set; }
        public double? Ask { get; set; }

        public int? LastSize { get; set; }
        public double? Last { get; set; }

        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Close { get; set; }

        public int? Volume { get; set; }

        public int? PutVolume { get; set; }
        public int? CallVolume { get; set; }
    }
}