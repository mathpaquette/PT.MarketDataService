namespace PT.Common.Repository
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}