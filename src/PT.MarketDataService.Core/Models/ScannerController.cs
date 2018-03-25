using System;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Core.Models
{
    public class ScannerController
    {
        private readonly IScannerRepositoryFactory _scannerRepositoryFactory;

        public ScannerController(IScannerRepositoryFactory scannerRepositoryFactory)
        {
            _scannerRepositoryFactory = scannerRepositoryFactory;
        }

        public void Start()
        {
            using (var scannerRepository = _scannerRepositoryFactory.CreateNew())
            {
                scannerRepository.Add(new Scanner()
                {
                    Timestamp = DateTime.Now
                });
            }

            using (var scannerRepository = _scannerRepositoryFactory.CreateNew())
            {
                var scanners = scannerRepository.GetAll();
            }
        }
    }
}