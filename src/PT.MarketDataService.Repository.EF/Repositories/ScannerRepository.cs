using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;
using SharpRepository.EfRepository;
using SharpRepository.Repository.Caching;

namespace PT.MarketDataService.Repository.EF.Repositories
{
    public class ScannerRepository : EfRepository<Scanner, int>, IScannerRepository
    {
        private readonly MarketDataServiceContext _marketDataServiceContext;

        public ScannerRepository(MarketDataServiceContext dbContext, ICachingStrategy<Scanner, int> cachingStrategy = null) : base(dbContext, cachingStrategy)
        {
            _marketDataServiceContext = dbContext;
        }

        protected override void AddItem(Scanner entity)
        {
            _marketDataServiceContext.ScannerParameters.Attach(entity.Parameter);

            foreach (var row in entity.Rows)
            {
                _marketDataServiceContext.Contracts.Attach(row.Contract);
            }

            base.AddItem(entity);
        }
    }
}