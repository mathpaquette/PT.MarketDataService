using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Infrastructure.Extensions
{
    public static class ContractExtensions
    {
        public static Contract ToEntityContract(this IBApi.Contract contract)
        {
            return new Contract()
            {
                ConId = contract.ConId,
                Currency = contract.Currency,
                Exchange = contract.Exchange,
                LocalSymbol = contract.LocalSymbol,
                SecType = contract.SecType,
                Symbol = contract.Symbol,
                TradingClass = contract.TradingClass
            };
        }

        public static IBApi.Contract ToIbContract(this Core.Entities.Contract contract)
        {
            return new IBApi.Contract()
            {
                ConId = contract.ConId,
                Exchange = "SMART"
            };
        }
    }
}