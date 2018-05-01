using System.Data.Entity;
using NUnit.Framework;
using PT.Common.Repository;
using PT.Common.Repository.EfRepository;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Topshelf;

namespace PT.MarketDataService.Tests.Integration.Repositories
{
    public class EfRepositoryTests
    {
        [Test]
        public void UnitOfWork_Should_Use_Same_DbContext()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();

            bootstrapper.Container.Register<TestRepository1>();
            bootstrapper.Container.Register<TestRepository2>();

            var unitOfWork = bootstrapper.Container.GetInstance<IUnitOfWorkFactory>().Create();
            var testRepository1 = unitOfWork.GetRepository<TestRepository1>();
            var testRepository2 = unitOfWork.GetRepository<TestRepository2>();

            Assert.AreEqual(testRepository1.Context, testRepository2.Context);
        }

        class TestRepository1 : EfRepository<Scanner>
        {
            public new DbContext Context;

            public TestRepository1(DbContext context) : base(context)
            {
                Context = context;
            }
        }

        class TestRepository2 : EfRepository<ScannerConfig>
        {
            public new DbContext Context;

            public TestRepository2(DbContext context) : base(context)
            {
                Context = context;
            }
        }
    }
}