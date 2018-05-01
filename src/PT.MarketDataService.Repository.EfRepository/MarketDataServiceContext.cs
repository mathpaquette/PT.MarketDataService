using System.Data.Entity;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Repository.EfRepository.Mappings;

namespace PT.MarketDataService.Repository.EfRepository
{
    public class MarketDataServiceContext : DbContext
    {
        public MarketDataServiceContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer(new DefaultInitializer());
        }

        public DbSet<Level1MarketData> Level1MarketDatas { get; set; }
        public DbSet<Scanner> Scanners { get; set; }
        public DbSet<ScannerConfig> ScannerConfigs { get; set; }
        public DbSet<ScannerParameter> ScannerParameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Level1MarketDataMap());
            modelBuilder.Configurations.Add(new ScannerMap());
            modelBuilder.Configurations.Add(new ScannerRowMap());
            modelBuilder.Configurations.Add(new ScannerConfigMap());
            modelBuilder.Configurations.Add(new ScannerParameterMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}