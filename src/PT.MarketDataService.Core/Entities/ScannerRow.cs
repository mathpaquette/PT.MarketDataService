namespace PT.MarketDataService.Core.Entities
{
    public class ScannerRow
    {
        public int Rank { get; set; }
        public string Symbol { get; set; }

        public int Id { get; set; }
        public int ScannerId { get; set; }
        public Scanner Scanner { get; set; }
    }
}