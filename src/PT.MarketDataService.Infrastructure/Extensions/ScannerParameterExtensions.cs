using IBApi;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Infrastructure.Extensions
{
    public static class ScannerParameterExtensions
    {
        public static ScannerSubscription ToScannerSubscription(this ScannerParameter param)
        {
            var res = new ScannerSubscription();

            if (param.NumberOfRows.HasValue) res.NumberOfRows = (int)param.NumberOfRows;
            if (param.AbovePrice.HasValue) res.AbovePrice = (double)param.AbovePrice;
            if (param.BelowPrice.HasValue) res.BelowPrice = (double)param.BelowPrice;
            if (param.AboveVolume.HasValue) res.AboveVolume = (int)param.AboveVolume;
            if (param.AverageOptionVolumeAbove.HasValue) res.AverageOptionVolumeAbove = (int)param.AverageOptionVolumeAbove;
            if (param.MarketCapAbove.HasValue) res.MarketCapAbove = (double)param.MarketCapAbove;
            if (param.MarketCapBelow.HasValue) res.MarketCapBelow = (double)param.MarketCapBelow;
            if (param.CouponRateAbove.HasValue) res.CouponRateAbove = (double)param.CouponRateAbove;
            if (param.CouponRateBelow.HasValue) res.CouponRateBelow = (double)param.CouponRateBelow;
            res.Instrument = param.Instrument;
            res.LocationCode = param.LocationCode;
            res.ScanCode = param.ScanCode;
            res.MoodyRatingAbove = param.MoodyRatingAbove;
            res.MoodyRatingBelow = param.MoodyRatingBelow;
            res.SpRatingAbove = param.SpRatingAbove;
            res.SpRatingBelow = param.SpRatingBelow;
            res.MaturityDateAbove = param.MaturityDateAbove;
            res.MaturityDateBelow = param.MaturityDateBelow;
            res.ExcludeConvertible = param.ExcludeConvertible;
            res.ScannerSettingPairs = param.ScannerSettingPairs;
            res.StockTypeFilter = param.StockTypeFilter;

            return res;
        }
    }
}