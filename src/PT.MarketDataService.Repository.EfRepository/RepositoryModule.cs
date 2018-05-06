using System.Data.Entity;
using PT.Common.Repository;
using PT.Common.Repository.EfRepository;
using PT.MarketDataService.Core.Repositories;
using PT.MarketDataService.Repository.EfRepository.Repositories;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PT.MarketDataService.Repository.EfRepository
{
    public class RepositoryModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<DbContext, MarketDataServiceContext>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(EfRepository<>), Lifestyle.Scoped);
            container.Register(typeof(IRepository<,>), typeof(EfRepository<,>), Lifestyle.Scoped);
            container.Register<IUnitOfWorkFactory, UnitOfWorkFactory>();

            // custom repositories
            container.Register<IScannerConfigRepository, ScannerConfigRepository>();
            container.Register<IScannerRepository, ScannerRepository>();
        }
    }
}