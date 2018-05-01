using System;
using System.Collections.Generic;
using NLog;
using PT.Common.Repository;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Models;
using PT.MarketDataService.Core.Providers;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Core.DomainServices
{
    public class ScannerService : IScannerService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ITimeProvider _timeProvider;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ScannerService(
            ITimeProvider timeProvider,
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _timeProvider = timeProvider;
        }

        public IEnumerable<ScannerRequest> GetScannerRequests()
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var scannerConfigRepository = unitOfWork.GetRepository<IScannerConfigRepository>();
                var scannerConfigs = scannerConfigRepository.GetAllWithParameters();

                foreach (var scannerConfig in scannerConfigs)
                {
                    foreach (var parameter in scannerConfig.Parameters)
                    {
                        yield return new ScannerRequest(scannerConfig.StartTime, scannerConfig.EndTime, scannerConfig.Frequency, parameter, _timeProvider);
                    }
                }
            }
        }

        public void PersistScanner(Scanner scanner, int scannerParameterId)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                try
                {
                    var scannerRepository = unitOfWork.GetRepository<IRepository<Scanner, int>>();

                    scanner.ParameterId = scannerParameterId;
                    scannerRepository.Add(scanner);

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