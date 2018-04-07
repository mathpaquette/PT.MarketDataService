using System;

namespace PT.MarketDataService.Core
{
    public class Exchange
    {
        public static TimeSpan PreMarketOpenHour = new TimeSpan(7, 00, 00);
        public static TimeSpan MarketOpenHour = new TimeSpan(9, 30, 00);
        public static TimeSpan MarketCloseHour = new TimeSpan(16, 00, 00);
        public static TimeSpan MarketCloseHourExtra = new TimeSpan(16, 15, 00);
        public static TimeSpan AfterMarketOpenHour = MarketCloseHour;
        public static TimeSpan AfterMarketCloseHour = new TimeSpan(20, 00, 00);
    }
}