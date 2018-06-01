using IBApi;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Infrastructure.Extensions
{
    public static class Level1MarketDataExtensions
    {
        public static void SetTickPriceValue(this Level1MarketData level1MarketData, int field, double price)
        {
            switch (field)
            {
                case TickType.BID:
                    level1MarketData.Bid = price;
                    break;
                case TickType.ASK:
                    level1MarketData.Ask = price;
                    break;
                case TickType.LAST:
                    level1MarketData.Last = price;
                    break;
                case TickType.OPEN:
                    level1MarketData.Open = price;
                    break;
                case TickType.HIGH:
                    level1MarketData.High = price;
                    break;
                case TickType.LOW:
                    level1MarketData.Low = price;
                    break;
                case TickType.CLOSE:
                    level1MarketData.Close = price;
                    break;
            }
        }

        public static void SetTickSizeValue(this Level1MarketData level1MarketData, int field, int size)
        {
            switch (field)
            {
                case TickType.BID_SIZE:
                    level1MarketData.BidSize = size;
                    break;
                case TickType.ASK_SIZE:
                    level1MarketData.AskSize = size;
                    break;
                case TickType.LAST_SIZE:
                    level1MarketData.LastSize = size;
                    break;
                case TickType.VOLUME:
                    level1MarketData.Volume = size;
                    break;
                case TickType.OPTION_CALL_VOLUME:
                    level1MarketData.CallVolume = size;
                    break;
                case TickType.OPTION_PUT_VOLUME:
                    level1MarketData.PutVolume = size;
                    break;
            }
        }
    }
}