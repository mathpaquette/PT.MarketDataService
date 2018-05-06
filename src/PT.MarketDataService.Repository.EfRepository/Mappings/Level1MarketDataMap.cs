using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EfRepository.Mappings
{
    public class Level1MarketDataMap : EntityTypeConfiguration<Level1MarketData>
    {
        public Level1MarketDataMap()
        {
            ToTable("Level1MarketDatas");

            // key
            HasKey(t => new { t.Timestamp, t.Symbol });

            Property(x => x.Symbol).HasMaxLength(8);
        }
    }
}