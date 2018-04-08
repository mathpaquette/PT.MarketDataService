using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EF.Mappings
{
    public class ScannerRowMap : EntityTypeConfiguration<ScannerRow>
    {
        public ScannerRowMap()
        {
            ToTable("ScannerRows");

            // key
            HasKey(t => t.Id);

            HasRequired(s => s.Contract);
        }
    }
}