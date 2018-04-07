using PT.MarketDataService.Core.Repositories;
using SimpleInjector;

namespace PT.MarketDataService.Infrastructure.Repositories
{
    public class Level1MarketDataRepositoryFactory : ILevel1MarketDataRepositoryFactory
    {
        private readonly Container _container;

        public Level1MarketDataRepositoryFactory(Container container)
        {
            _container = container;
        }

        public ILevel1MarketDataRepository CreateNew()
        {
            return _container.GetInstance<ILevel1MarketDataRepository>();
        }
    }
}