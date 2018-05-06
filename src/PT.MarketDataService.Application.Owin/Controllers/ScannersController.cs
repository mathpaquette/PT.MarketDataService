using System;
using System.Collections.Generic;
using System.Web.Http;
using PT.MarketDataService.Application.AppServices;
using PT.MarketDataService.Core.ValueObjects;

namespace PT.MarketDataService.Application.Owin.Controllers
{
    public class ScannersController : ApiController
    {
        private readonly ScannerAppService _scannerAppService;

        public ScannersController(ScannerAppService scannerAppService)
        {
            _scannerAppService = scannerAppService;
        }

        [Route("api/scanners/symbols/by-option-volume")]
        public IEnumerable<ScannerWithOptionVolume> GetScannerSymbolsOrderByOptionVolume(DateTime? timestamp = null, TimeSpan? maxTimeOffset = null)
        {
            return _scannerAppService.GetScannerSymbolsOrderByOptionVolume(timestamp, maxTimeOffset);
        }
    }
}