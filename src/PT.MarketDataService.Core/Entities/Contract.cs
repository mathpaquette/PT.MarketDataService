namespace PT.MarketDataService.Core.Entities
{
    public class Contract
    {
        public int Id { get; set; }
        public int ConId { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public string LocalSymbol { get; set; }
        public string SecType { get; set; }
        public string Symbol { get; set; }
        public string TradingClass { get; set; }

        public override int GetHashCode()
        {
            return ConId.GetHashCode();
        }
    }
}