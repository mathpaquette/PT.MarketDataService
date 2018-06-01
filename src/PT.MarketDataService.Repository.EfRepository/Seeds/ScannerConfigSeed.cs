using System;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EfRepository.Seeds
{
    public static class ScannerConfigSeed
    {
        public static ScannerConfig GetConfiguredScannerConfig(int scannerConfigId, string scanCode, TimeSpan startTime, TimeSpan endTime)
        {
            double[] prices = { 0, 7, 14, 21, 28, 35, 42, 49, 56, 63, 70, 82, 103, 124, 194, 264 };

            var scannerConfig = new ScannerConfig()
            {
                Id = scannerConfigId,
                Enable = true,
                StartTime = startTime,
                EndTime = endTime,
                Frequency = 60
            };

            for (int i = 0; i < prices.Length; i++)
            {
                var parameter = new ScannerParameter()
                {
                    ScanCode = scanCode,
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
    }
}