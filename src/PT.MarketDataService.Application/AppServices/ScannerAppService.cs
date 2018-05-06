using System;
using System.Collections.Generic;
using PT.MarketDataService.Core.Repositories;
using PT.MarketDataService.Core.ValueObjects;

namespace PT.MarketDataService.Application.AppServices
{
    public class ScannerAppService
    {
        private readonly IScannerRepository _scannerRepository;

        public ScannerAppService(IScannerRepository scannerRepository)
        {
            _scannerRepository = scannerRepository;
        }

        public IEnumerable<ScannerWithOptionVolume> GetScannerSymbolsOrderByOptionVolume(DateTime? currentTimestamp = null, TimeSpan? maxTimeOffset = null)
        {
            return _scannerRepository.GetScannerSymbolsOrderByOptionVolume(currentTimestamp, maxTimeOffset);
        }
    }
}