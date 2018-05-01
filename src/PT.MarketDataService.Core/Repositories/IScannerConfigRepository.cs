using System.Collections.Generic;
using PT.Common.Repository;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Core.Repositories
{
    public interface IScannerConfigRepository : IRepository<ScannerConfig, int>
    {
        IEnumerable<ScannerConfig> GetAllWithParameters();
    }
}