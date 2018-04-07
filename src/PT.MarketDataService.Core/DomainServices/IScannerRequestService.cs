using System.Collections.Generic;
using PT.MarketDataService.Core.Models;

namespace PT.MarketDataService.Core.DomainServices
{
    public interface IScannerRequestService
    {
        IEnumerable<ScannerRequest> GetScannerRequests();
    }
}