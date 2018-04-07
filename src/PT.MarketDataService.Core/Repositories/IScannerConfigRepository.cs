using System.Collections.Generic;
using PT.MarketDataService.Core.Entities;
using SharpRepository.Repository;

namespace PT.MarketDataService.Core.Repositories
{
    public interface IScannerConfigRepository : IRepository<ScannerConfig, int>
    {
        new IEnumerable<ScannerConfig> GetAll();
    }
}