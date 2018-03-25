using System;
using Topshelf;

namespace PT.MarketDataService.Topshelf
{
    public class Program
    {
        public static void Main()
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<MarketDataServiceBootstrapper>(s =>
                {
                    s.ConstructUsing(name => new MarketDataServiceBootstrapper());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Market Data Service Host");
                x.SetDisplayName("MarketDataService");
                x.SetServiceName("MarketDataService");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}