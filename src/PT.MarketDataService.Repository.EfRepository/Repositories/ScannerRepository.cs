using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using PT.Common.Repository.EfRepository;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Repositories;
using PT.MarketDataService.Core.ValueObjects;
using PT.MarketDataService.Repository.EfRepository.StoredProcedures;

namespace PT.MarketDataService.Repository.EfRepository.Repositories
{
    public class ScannerRepository : EfRepository<Scanner, int>, IScannerRepository
    {
        public ScannerRepository(DbContext context) : base(context)
        {

        }

        public IEnumerable<ScannerWithOptionVolume> GetScannerSymbolsOrderByOptionVolume(DateTime? currentTimestamp = null, TimeSpan? maxTimeOffset = null)
        {
            var sqlQuery = $"{StoredProcedure.MdsGetScannerSymbolsOrderByOptionVolume} @CurrentTimestamp, @MaxTimeOffset";

            var sqlParams = new SqlParameter[]
            {
                new SqlParameter("@CurrentTimestamp", currentTimestamp ?? DateTime.Now),
                new SqlParameter("@MaxTimeOffset", maxTimeOffset ?? TimeSpan.FromMinutes(5))
            };

            return Context.Database.SqlQuery<ScannerWithOptionVolume>(sqlQuery, sqlParams);
        }
    }
}