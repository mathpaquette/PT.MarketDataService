using System;
using NLog;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Core.DomainServices
{
    public class Level1MarketDataService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ILevel1MarketDataRepositoryFactory _level1MarketDataRepositoryFactory;
        private readonly IContractRepository _contractRepository;

        public Level1MarketDataService(
            ILevel1MarketDataRepositoryFactory level1MarketDataRepositoryFactory,
            IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
            _level1MarketDataRepositoryFactory = level1MarketDataRepositoryFactory;
        }

        public void Save(Level1MarketData level1MarketData)
        {
            CreateNotExistingContract(level1MarketData);
            AddLevel1MarketData(level1MarketData);
        }

        private void CreateNotExistingContract(Level1MarketData level1MarketData)
        {
            if (!_contractRepository.TryFind(x => x.ConId == level1MarketData.Contract.ConId, out var contract))
            {
                _contractRepository.Add(level1MarketData.Contract);
            }
            else
            {
                level1MarketData.Contract = contract;
            }
        }

        private void AddLevel1MarketData(Level1MarketData level1MarketData)
        {
            try
            {
                using (var repo = _level1MarketDataRepositoryFactory.CreateNew())
                {
                    repo.Add(level1MarketData);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }

}