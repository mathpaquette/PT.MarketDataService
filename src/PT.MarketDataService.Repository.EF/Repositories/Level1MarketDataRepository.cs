using System.Data.Entity;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;
using SharpRepository.EfRepository;
using SharpRepository.Repository;
using SharpRepository.Repository.Caching;
using SharpRepository.Repository.Configuration;

namespace PT.MarketDataService.Repository.EF.Repositories
{
    public class Level1MarketDataRepository: EfRepository<Level1MarketData, int>, ILevel1MarketDataRepository
    {
        private readonly MarketDataServiceContext _marketDataServiceContext;

        public Level1MarketDataRepository(MarketDataServiceContext dbContext, ICachingStrategy<Level1MarketData, int> cachingStrategy = null) : base(dbContext, cachingStrategy)
        {
            _marketDataServiceContext = dbContext;
        }

        protected override void AddItem(Level1MarketData entity)
        {
            _marketDataServiceContext.Contracts.Attach(entity.Contract);
            base.AddItem(entity);
        }
    }
}