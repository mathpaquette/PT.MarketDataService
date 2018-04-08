using System;
using System.Collections.Generic;
using NLog;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Core.DomainServices
{
    public class ScannerService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IScannerRepositoryFactory _scannerRepositoryFactory;
        private readonly IContractRepository _contractRepository;

        public ScannerService(
            IScannerRepositoryFactory scannerRepositoryFactory,
            IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
            _scannerRepositoryFactory = scannerRepositoryFactory;
        }

        public void Save(Scanner scanner)
        {
            CreateNotExistingContracts(scanner.Rows);
            CreateScanner(scanner);
        }

        private void CreateNotExistingContracts(IEnumerable<ScannerRow> scannerRows)
        {
            foreach (var row in scannerRows)
            {
                if (!_contractRepository.TryFind(x => x.ConId == row.Contract.ConId, out var contract))
                {
                    _contractRepository.Add(row.Contract);
                }
                else
                {
                    row.Contract = contract;
                }
            }
        }

        private void CreateScanner(Scanner scanner)
        {
            using (var scannerRepository = _scannerRepositoryFactory.CreateNew())
            {
                try
                {
                    scannerRepository.Add(scanner);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }
    }
}