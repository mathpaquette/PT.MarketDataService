using System;
using System.Threading.Tasks;

namespace PT.Common.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        T GetRepository<T>() where T : class;
        int Complete();
        Task<int> CompleteAsync();
    }
}