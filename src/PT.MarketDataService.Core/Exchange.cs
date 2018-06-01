using System;

namespace PT.MarketDataService.Core
{
    public class Exchange
    {
        /// <summary>
        /// 7:00 AM
        /// </summary>
        public static TimeSpan PreMarketOpenHour = new TimeSpan(7, 00, 00);

        /// <summary>
        /// 9:30 AM
        /// </summary>
        public static TimeSpan MarketOpenHour = new TimeSpan(9, 30, 00);

        /// <summary>
        /// 16:00 PM
        /// </summary>
        public static TimeSpan MarketCloseHour = new TimeSpan(16, 00, 00);

        /// <summary>
        /// 16:00 PM
        /// </summary>
        public static TimeSpan AfterMarketOpenHour = MarketCloseHour;

        /// <summary>
        /// 20:00 PM
        /// </summary>
        public static TimeSpan AfterMarketCloseHour = new TimeSpan(20, 00, 00);
    }
}