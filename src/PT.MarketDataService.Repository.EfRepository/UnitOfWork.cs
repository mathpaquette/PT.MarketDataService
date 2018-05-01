using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PT.Common.Repository;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace PT.MarketDataService.Repository.EfRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Scope _scope;

        public UnitOfWork(Container container)
        {
            _scope = ThreadScopedLifestyle.BeginScope(container);
            _context = _scope.Container.GetInstance<DbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }

        public T GetRepository<T>() where T : class
        {
            var repository = _scope.Container.GetInstance<T>();

            // check if the instance is inheriting from IRepository interface
            var isRepository = repository.GetType()
                             .GetInterfaces()
                             .Any(x => x.IsGenericType &&
                                       x.GetGenericTypeDefinition() == typeof(IRepository<>) || 
                                       x.GetGenericTypeDefinition() == typeof(IRepository<,>));
            if (!isRepository)
                throw new NotSupportedException("Requested instance isn't a repository");

            return repository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}