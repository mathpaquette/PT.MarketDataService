using System.Collections.Generic;

namespace PT.MarketDataService.Core.Entities
{
    public class ScannerParameter
    {
        public ScannerParameter()
        {
            Scanners = new HashSet<Scanner>();
        }

        public string ExcludeConvertible { get; set; }
        public double? CouponRateBelow { get; set; }
        public double? CouponRateAbove { get; set; }
        public string MaturityDateBelow { get; set; }
        public string MaturityDateAbove { get; set; }
        public string SpRatingBelow { get; set; }
        public string SpRatingAbove { get; set; }
        public string MoodyRatingBelow { get; set; }
        public string MoodyRatingAbove { get; set; }
        public double? MarketCapBelow { get; set; }
        public double? MarketCapAbove { get; set; }
        public int? AverageOptionVolumeAbove { get; set; }
        public int? AboveVolume { get; set; }
        public double? BelowPrice { get; set; }
        public double? AbovePrice { get; set; }
        public string ScanCode { get; set; }
        public string LocationCode { get; set; }
        public string Instrument { get; set; }
        public int? NumberOfRows { get; set; }
        public string ScannerSettingPairs { get; set; }
        public string StockTypeFilter { get; set; }

        public int Id { get; set; }
        public int ScannerConfigId { get; set; }
        public ScannerConfig ScannerConfig { get; set; }
        public ICollection<Scanner> Scanners { get; set; }
    }
}