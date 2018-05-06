using System;
using System.Collections.Generic;
using PT.Common.Repository;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.ValueObjects;

namespace PT.MarketDataService.Core.Repositories
{
    public interface IScannerRepository : IRepository<Scanner, int>
    {
        IEnumerable<ScannerWithOptionVolume> GetScannerSymbolsOrderByOptionVolume(DateTime? currentTimestamp = null, TimeSpan? maxTimeOffset = null);
    }
}