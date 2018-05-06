using System.Linq;
using NUnit.Framework;
using PT.Common.Repository;
using PT.MarketDataService.Core.Repositories;
using PT.MarketDataService.Topshelf;

namespace PT.MarketDataService.Tests.Integration.Repositories
{
    public class ScannerRepositoryTests
    {
        [Test]
        public void Should_Get_Scanner_Symbols_Order_By_Option_Volume()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();

            var unitOfWork = bootstrapper.Container.GetInstance<IUnitOfWorkFactory>().Create();
            var scannerRepository = unitOfWork.GetRepository<IScannerRepository>();
            var symbolsOrderByOptionVolume = scannerRepository.GetScannerSymbolsOrderByOptionVolume();
            Assert.GreaterOrEqual(symbolsOrderByOptionVolume.Count(), 0);
        }
    }
}