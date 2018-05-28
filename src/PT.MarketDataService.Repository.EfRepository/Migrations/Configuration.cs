using PT.MarketDataService.Repository.EfRepository.Seeds;
using PT.MarketDataService.Repository.EfRepository.StoredProcedures;

namespace PT.MarketDataService.Repository.EfRepository.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PT.MarketDataService.Repository.EfRepository.MarketDataServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PT.MarketDataService.Repository.EfRepository.MarketDataServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var scannerConfig = OptVolumeMostActiveScannerConfigSeed.GetOptVolumeMostActiveScannerConfig();
            context.ScannerConfigs.AddOrUpdate(scannerConfig);

            // Create Stored Procedures
            GetScannerSymbolsOrderByOptionVolume(context);
        }

        private void GetScannerSymbolsOrderByOptionVolume(MarketDataServiceContext context)
        {
            context.Database.ExecuteSqlCommand(StoredProcedureFiles.mdsGetScannerSymbolsOrderByOptionVolume);
        }
    }
}