using System;
using PT.MarketDataService.Core;
using PT.MarketDataService.Repository.EfRepository.Seeds;
using PT.MarketDataService.Repository.EfRepository.StoredProcedures;

namespace PT.MarketDataService.Repository.EfRepository.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PT.MarketDataService.Repository.EfRepository.MarketDataServiceContext>
    {
        private const string OptVolumeMostActive = "OPT_VOLUME_MOST_ACTIVE";
        private const string VolumeMostActive = "MOST_ACTIVE";

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PT.MarketDataService.Repository.EfRepository.MarketDataServiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var optMostActiveScannerConfig = ScannerConfigSeed.GetConfiguredScannerConfig(1, OptVolumeMostActive, Exchange.MarketOpenHour, Exchange.MarketCloseHour.Add(TimeSpan.FromMinutes(15)));
            var mostActivePreMarketScannerConfig = ScannerConfigSeed.GetConfiguredScannerConfig(2, VolumeMostActive, Exchange.PreMarketOpenHour, Exchange.MarketOpenHour);
            var mostActiveAfterMarketScannerConfig = ScannerConfigSeed.GetConfiguredScannerConfig(3, VolumeMostActive, Exchange.MarketCloseHour, Exchange.AfterMarketCloseHour);

            context.ScannerConfigs.AddOrUpdate(optMostActiveScannerConfig);
            context.ScannerConfigs.AddOrUpdate(mostActivePreMarketScannerConfig);
            context.ScannerConfigs.AddOrUpdate(mostActiveAfterMarketScannerConfig);

            // Create Stored Procedures
            GetScannerSymbolsOrderByOptionVolume(context);
        }

        private void GetScannerSymbolsOrderByOptionVolume(MarketDataServiceContext context)
        {
            context.Database.ExecuteSqlCommand(StoredProcedureFiles.mdsGetScannerSymbolsOrderByOptionVolume);
        }
    }
}