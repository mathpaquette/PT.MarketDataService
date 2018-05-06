namespace PT.MarketDataService.Core.ValueObjects
{
    public class ScannerWithOptionVolume
    {
        public string Symbol { get; set; }
        public int? OptionVolume { get; set; }
    }
}