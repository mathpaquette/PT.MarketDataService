using System;

namespace PT.MarketDataService.Core.Entities
{
    public class Level1MarketData
    {
        public DateTime Timestamp { get; set; }
        public string Symbol { get; set; }

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


        /// <summary>
        /// Determined if all values have been received and filled
        /// </summary>
        /// <returns></returns>
        public bool Completed()
        {
            return BidSize.HasValue &&
                   Bid.HasValue &&
                   AskSize.HasValue &&
                   Ask.HasValue &&
                   LastSize.HasValue &&
                   Last.HasValue &&
                   //data.Open.HasValue &&    // not received it premarket
                   //data.High.HasValue &&    // not received it premarket
                   //data.Low.HasValue &&     // not received it premarket
                   //data.Close.HasValue &&   // not received it premarket
                   Volume.HasValue &&
                   PutVolume.HasValue &&
                   CallVolume.HasValue;
        }
    }
}