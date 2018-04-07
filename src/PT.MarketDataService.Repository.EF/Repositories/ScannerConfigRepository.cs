using System.Collections.Generic;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;
using SharpRepository.Repository;
using SharpRepository.Repository.Configuration;
using SharpRepository.Repository.FetchStrategies;

namespace PT.MarketDataService.Repository.EF.Repositories
{
    public class ScannerConfigRepository : ConfigurationBasedRepository<ScannerConfig, int>, IScannerConfigRepository
    {
        public ScannerConfigRepository(ISharpRepositoryConfiguration configuration, string repositoryName = null) : base(configuration, repositoryName)
        {
        }

        public new IEnumerable<ScannerConfig> GetAll()
        {
            var strategy = new GenericFetchStrategy<ScannerConfig>();
            strategy.Include(x => x.Parameters);

            return base.Repository.GetAll(strategy);
        }
    }
}