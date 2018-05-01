using System.Data.Entity;
using PT.MarketDataService.Core;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EfRepository
{
    public class DefaultInitializer : CreateDatabaseIfNotExists<MarketDataServiceContext>
    {
        private const string MostActiveOption = "OPT_VOLUME_MOST_ACTIVE";

        protected override void Seed(MarketDataServiceContext context)
        {
            var optVolumeMostActiveScannerConfig = GetOptVolumeMostActiveScannerConfig();
            var preMarketScannerConfig = GetPreMarketMostActiveScannerConfig();
            var afterMarketScannerConfig = GetAfterMarketMostActiveScannerConfig();

            context.ScannerConfigs.Add(optVolumeMostActiveScannerConfig);
            context.SaveChanges();
            base.Seed(context);
        }

        private ScannerConfig GetOptVolumeMostActiveScannerConfig()
        {
            double[] prices = { 0, 7, 14, 21, 28, 35, 42, 49, 56, 63, 70, 82, 103, 124, 194, 264 };

            var scannerConfig = new ScannerConfig()
            {
                StartTime = Exchange.MarketOpenHour,
                EndTime = Exchange.MarketCloseHour,
                Frequency = 60
            };

            for (int i = 0; i < prices.Length; i++)
            {
                var parameter = new ScannerParameter()
                {
                    ScanCode = MostActiveOption,
                    Instrument = "STK",
                    LocationCode = "STK.US",
                    NumberOfRows = 50,
                    AbovePrice = prices[i],
                };

                if (i != prices.Length - 1)
                {
                    parameter.BelowPrice = prices[i + 1] + 0.01;
                }

                scannerConfig.Parameters.Add(parameter);
            }

            return scannerConfig;
        }

        // TODO: implement this one
        private ScannerConfig GetPreMarketMostActiveScannerConfig()
        {
            return null;
        }

        // TODO: implement this one
        private ScannerConfig GetAfterMarketMostActiveScannerConfig()
        {
            return null;
        }
    }
}