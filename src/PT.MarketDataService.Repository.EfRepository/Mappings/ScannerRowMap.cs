using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EfRepository.Mappings
{
    public class ScannerRowMap : EntityTypeConfiguration<ScannerRow>
    {
        public ScannerRowMap()
        {
            ToTable("ScannerRows");

            // key
            HasKey(t => t.Id);

            Property(x => x.Symbol).HasMaxLength(8);

            HasIndex(i => i.Symbol);
        }
    }
}