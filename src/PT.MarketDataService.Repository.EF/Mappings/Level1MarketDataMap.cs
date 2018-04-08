using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EF.Mappings
{
    public class Level1MarketDataMap : EntityTypeConfiguration<Level1MarketData>
    {
        public Level1MarketDataMap()
        {
            ToTable("Level1MarketDatas");

            // key
            HasKey(t => t.Id);

            HasRequired(s => s.Contract);
        }
    }
}