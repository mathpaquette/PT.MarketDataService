using System;
using NLog;
using Topshelf;

namespace PT.MarketDataService.Topshelf
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            var bootstrapper = new Bootstrapper();
            bootstrapper.Initialize();

            var rc = HostFactory.Run(x =>
            {
                x.Service<MarketDataService>(s =>
                {
                    s.ConstructUsing(name => bootstrapper.Container.GetInstance<MarketDataService>());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Market Data Service Host");
                x.SetDisplayName("MarketDataService");
                x.SetServiceName("MarketDataService");
                x.UseNLog();
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception)args.ExceptionObject;
            Logger.Error(e);
        }
    }
}