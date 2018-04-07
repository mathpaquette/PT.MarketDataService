using System.Collections.Generic;
using System.Linq;
using PT.MarketDataService.Core.Models;
using PT.MarketDataService.Core.Providers;
using PT.MarketDataService.Core.Repositories;

namespace PT.MarketDataService.Core.DomainServices
{
    public class ScannerRequestService : IScannerRequestService
    {
        private readonly IScannerConfigRepository _scannerConfigRepository;
        private readonly ITimeProvider _timeProvider;


        public ScannerRequestService(
            IScannerConfigRepository scannerConfigRepository,
            ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _scannerConfigRepository = scannerConfigRepository;
        }

        public IEnumerable<ScannerRequest> GetScannerRequests()
        {
            var scannerConfigs = _scannerConfigRepository.GetAll().ToList();

            foreach (var scannerConfig in scannerConfigs)
            {
                foreach (var parameter in scannerConfig.Parameters)
                {
                    yield return new ScannerRequest(scannerConfig.StartTime, scannerConfig.EndTime, scannerConfig.Frequency, parameter, _timeProvider);
                }
            }
        }
    }
}