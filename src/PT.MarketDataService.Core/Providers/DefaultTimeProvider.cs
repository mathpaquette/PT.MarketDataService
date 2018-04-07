using System;

namespace PT.MarketDataService.Core.Providers
{
    public class DefaultTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}