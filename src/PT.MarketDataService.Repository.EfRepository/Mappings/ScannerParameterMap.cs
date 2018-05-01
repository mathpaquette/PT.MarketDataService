using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EfRepository.Mappings
{
    public class ScannerParameterMap : EntityTypeConfiguration<ScannerParameter>
    {
        public ScannerParameterMap()
        {
            ToTable("ScannerParameters");

            // key
            HasKey(t => t.Id);

            // many-to-one
            HasMany(p => p.Scanners)
                .WithRequired(s => s.Parameter)
                .HasForeignKey(s => s.ParameterId);
        }
    }
}