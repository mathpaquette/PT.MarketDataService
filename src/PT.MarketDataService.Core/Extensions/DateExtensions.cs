using System;

namespace PT.MarketDataService.Core.Extensions
{
    public static class DateExtensions
    {
        public static bool IsBusinessDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday &&
                date.DayOfWeek != DayOfWeek.Sunday;
        }

        public static DateTime NextBusinessDay(this DateTime date)
        {
            do
            {
                date = date.AddDays(1);
            } while (!IsBusinessDay(date));

            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static TimeSpan RoundUp(this TimeSpan ts, TimeSpan d)
        {
            return new TimeSpan((ts.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks);
        }
    }
}