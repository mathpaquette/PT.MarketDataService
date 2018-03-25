using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EF.Mappings
{
    public class ScannerParameterMap : EntityTypeConfiguration<ScannerParameter>
    {
        public ScannerParameterMap()
        {
            ToTable("ScannerParameters");

            // key
            HasKey(t => t.Id);
        }
    }
}