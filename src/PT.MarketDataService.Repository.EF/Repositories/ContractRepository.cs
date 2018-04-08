using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;
using SharpRepository.Repository;
using SharpRepository.Repository.Configuration;

namespace PT.MarketDataService.Repository.EF.Repositories
{
    public class ContractRepository : ConfigurationBasedRepository<Contract, int>, IContractRepository
    {
        public ContractRepository(ISharpRepositoryConfiguration configuration, string repositoryName = null) : base(configuration, repositoryName)
        {
        }
    }
}