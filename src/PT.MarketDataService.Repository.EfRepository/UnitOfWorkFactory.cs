using PT.Common.Repository;
using SimpleInjector;

namespace PT.MarketDataService.Repository.EfRepository
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly Container _container;

        public UnitOfWorkFactory(Container container)
        {
            _container = container;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_container);
        }
    }
}