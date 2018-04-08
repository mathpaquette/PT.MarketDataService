namespace PT.MarketDataService.Core.Entities
{
    public class ScannerRow
    {
        public int Rank { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public int Id { get; set; }
        public int ScannerId { get; set; }
        public Scanner Scanner { get; set; }
    }
}