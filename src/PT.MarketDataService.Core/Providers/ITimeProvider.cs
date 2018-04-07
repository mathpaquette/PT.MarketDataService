using System;

namespace PT.MarketDataService.Core.Providers
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}