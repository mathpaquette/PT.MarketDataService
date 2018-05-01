using System;
using NLog;
using PT.Common.Repository;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Core.DomainServices
{
    public class Level1MarketDataService : ILevel1MarketDataService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Level1MarketDataService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void PersistLevel1MarketData(Level1MarketData level1MarketData)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                try
                {
                    var level1MarketDataRepository = unitOfWork.GetRepository<IRepository<Level1MarketData>>();
                    level1MarketDataRepository.Add(level1MarketData);

                    unitOfWork.Complete();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }
    }
}