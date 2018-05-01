using System.Collections.Generic;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Models;

namespace PT.MarketDataService.Core.DomainServices
{
    public interface IScannerService
    {
        IEnumerable<ScannerRequest> GetScannerRequests();
        void PersistScanner(Scanner scanner, int scannerParameterId);
    }
}