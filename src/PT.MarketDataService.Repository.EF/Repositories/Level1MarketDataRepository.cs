using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;
using SharpRepository.Repository;
using SharpRepository.Repository.Configuration;

namespace PT.MarketDataService.Repository.EF.Repositories
{
    public class Level1MarketDataRepository: ConfigurationBasedRepository<Level1MarketData, int>, ILevel1MarketDataRepository
    {
        public Level1MarketDataRepository(ISharpRepositoryConfiguration configuration, string repositoryName = null) : base(configuration, repositoryName)
        {
        }
    }
}