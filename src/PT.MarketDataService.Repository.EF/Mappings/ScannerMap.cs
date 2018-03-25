using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EF.Mappings
{
    public class ScannerMap : EntityTypeConfiguration<Scanner>
    {
        public ScannerMap()
        {
            ToTable("Scanners");

            // key
            HasKey(t => t.Id);
        }
    }
}