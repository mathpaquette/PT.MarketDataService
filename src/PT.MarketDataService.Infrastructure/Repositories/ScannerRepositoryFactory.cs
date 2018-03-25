using PT.MarketDataService.Core.Repositories;
using SimpleInjector;

namespace PT.MarketDataService.Infrastructure.Repositories
{
    public class ScannerRepositoryFactory : IScannerRepositoryFactory
    {
        private readonly Container _container;

        public ScannerRepositoryFactory(Container container)
        {
            _container = container;
        }

        public IScannerRepository CreateNew()
        {
            return _container.GetInstance<IScannerRepository>();
        }
    }
}