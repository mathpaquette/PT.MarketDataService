using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EfRepository.Mappings
{
    public class ScannerMap : EntityTypeConfiguration<Scanner>
    {
        public ScannerMap()
        {
            ToTable("Scanners");

            // key
            HasKey(t => t.Id);

            HasMany(s => s.Rows)
                .WithRequired(r => r.Scanner)
                .HasForeignKey(r => r.ScannerId);
        }
    }
}