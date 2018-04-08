using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IB.CSharpApiClient;
using IB.CSharpApiClient.Events;
using IBApi;
using NLog;
using PT.MarketDataService.Core.DomainServices;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Infrastructure.Extensions;

namespace PT.MarketDataService.Infrastructure.DomainServices
{

    public class MarketDataProvider : ApiClient, IMarketDataProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IAppConfig _appConfig;

        public MarketDataProvider(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            EventDispatcher.Error += EventDispatcherOnError;
        }

        private void EventDispatcherOnError(object o, ErrorEventArgs args)
        {
            if (args.ErrorCode == 162 && args.Message.Contains("API scanner subscription cancelled"))
                return;

            if (args.Exception != null)
            {
                Logger.Error(args.Exception);
                return;
            }

            Logger.Info($"RequestId: {args.RequestId} Message: {args.Message} ErrorCode: {args.ErrorCode}");
        }

        public Task<Scanner> GetScannerAsync(ScannerParameter scannerParameter)
        {
            var ct = new CancellationTokenSource(DefaultTimeoutMs);
            var res = new TaskCompletionSource<Scanner>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            var reqId = new Random(DateTime.Now.Millisecond).Next();

            var ibScanner = new Scanner()
            {
                Parameter = scannerParameter
            };

            EventHandler<ScannerEventArgs> scanner = (sender, args) =>
            {
                if (args.RequestId != reqId) return;
                var contract = args.ContractDetails.Summary.ToEntityContract();
                ibScanner.Rows.Add(new ScannerRow() { Rank = args.Rank, Contract = contract });
            };

            EventHandler<ScannerEndEventArgs> scannerEnd = (sender, args) =>
            {
                if (args.RequestId != reqId) return;
                res.SetResult(ibScanner);
            };
            EventDispatcher.Scanner += scanner;
            EventDispatcher.ScannerEnd += scannerEnd;

            ClientSocket.reqScannerSubscription(reqId, scannerParameter.ToScannerSubscription(), new List<TagValue>());

            res.Task.ContinueWith(x =>
            {
                ClientSocket.cancelScannerSubscription(reqId);
                EventDispatcher.Scanner -= scanner;
                EventDispatcher.ScannerEnd -= scannerEnd;

            }, TaskContinuationOptions.None);

            return res.Task;
        }

        public Task<Level1MarketData> GetLevel1MarketDataAsync(Core.Entities.Contract contract)
        {
            var reqId = new Random(DateTime.Now.Millisecond).Next();
            var level1MarketData = new Level1MarketData()
            {
                Contract = contract,
                Timestamp = DateTime.Now
            };

            var ct = new CancellationTokenSource(15 * 1000);
            var res = new TaskCompletionSource<Level1MarketData>();
            ct.Token.Register(() => res.TrySetResult(level1MarketData), false);

            EventHandler<TickSizeEventArgs> tickSize = (sender, args) =>
            {
                if (args.RequestId != reqId)
                    return;

                level1MarketData.SetTickSizeValue(args.Field, args.Size);

                if (level1MarketData.Completed())
                    res.SetResult(level1MarketData);
            };

            EventHandler<TickPriceEventArgs> tickPrice = (sender, args) =>
            {
                if (args.RequestId != reqId)
                    return;

                level1MarketData.SetTickPriceValue(args.Field, args.Price);

                if (level1MarketData.Completed())
                    res.SetResult(level1MarketData);
            };

            EventDispatcher.TickSize += tickSize;
            EventDispatcher.TickPrice += tickPrice;

            var ibContract = contract.ToIbContract();

            ClientSocket.reqMktData(reqId, ibContract, GenericTick.OPTION_VOLUME.ToString(), false, false, new List<TagValue>());

            res.Task.ContinueWith(x =>
            {
                ClientSocket.cancelMktData(reqId);
                EventDispatcher.TickSize -= tickSize;
                EventDispatcher.TickPrice -= tickPrice;

            }, TaskContinuationOptions.None);

            return res.Task;
        }

        public async Task InitializeAsync()
        {
            await ConnectAsync(_appConfig.IbHost, _appConfig.IbPort, _appConfig.IbClientId);
        }
    }
}