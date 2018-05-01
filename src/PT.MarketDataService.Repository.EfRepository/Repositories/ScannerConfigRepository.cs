using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PT.Common.Repository.EfRepository;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Repository.EfRepository.Repositories
{
    public class ScannerConfigRepository : EfRepository<ScannerConfig, int>, IScannerConfigRepository
    {
        private readonly MarketDataServiceContext _marketDataServiceContext;

        public ScannerConfigRepository(MarketDataServiceContext context) : base(context)
        {
            _marketDataServiceContext = context;
        }

        public IEnumerable<ScannerConfig> GetAllWithParameters()
        {
            return _marketDataServiceContext.ScannerConfigs.Include(x => x.Parameters).ToList();
        }
    }
}