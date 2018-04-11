using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PT.MarketDataService.Core.Entities;
using PT.MarketDataService.Core.Enums;
using PT.MarketDataService.Core.Extensions;
using PT.MarketDataService.Core.Providers;
using PT.MarketDataService.Core.ValueObjects;

namespace PT.MarketDataService.Core.Models
{
    public class ScannerRequest
    {
        public event EventHandler Timeout;
        private readonly ITimeProvider _timeProvider;

        private readonly CancellationTokenSource _cts;
        private readonly SemaphoreSlim _semaphoreSlim;
        private Scanner _previousScanner;

        public ScannerRequest(TimeSpan startTime, TimeSpan endTime, int frequency, ScannerParameter parameter, ITimeProvider timeProvider)
        {
            StartTime = startTime;
            EndTime = endTime;
            Frequency = TimeSpan.FromSeconds(frequency);
            Parameter = parameter;
            _timeProvider = timeProvider;

            _cts = new CancellationTokenSource();
            _semaphoreSlim = new SemaphoreSlim(0, 1);

            // default
            _previousScanner = new Scanner();
        }


        public bool Online { get; private set; }
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }
        public TimeSpan Frequency { get; }
        public ScannerParameter Parameter { get; }
        public DateTime LastUpdate { get; private set; }
        public TimeSpan SinceLastUpdate => _timeProvider.Now - LastUpdate;

        public bool TrySetOffline()
        {
            var now = _timeProvider.Now;
            if (now.TimeOfDay >= StartTime && now.TimeOfDay <= EndTime && now.IsBusinessDay())
            {
                LastUpdate = now;
                Online = true;
                return false;
            }

            Online = false;
            return true;
        }

        public async void Start()
        {
            while (!_cts.IsCancellationRequested)
            {
                await _semaphoreSlim.WaitAsync(_cts.Token).ContinueWith(task => { });
                var delay = Online ? Frequency : UntilExpiration;
                await Task.Delay(delay, _cts.Token).ContinueWith(task => { });
                Timeout.RaiseEvent(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            _cts.Cancel();
        }

        public void Signal()
        {
            _semaphoreSlim.Release();
        }

        public TimeSpan UntilExpiration
        {
            get
            {
                var now = _timeProvider.Now;

                if (now.IsBusinessDay())
                {
                    if (now.TimeOfDay >= StartTime && now.TimeOfDay <= EndTime)
                        return Frequency;

                    if (now.TimeOfDay < StartTime)
                        return StartTime - now.TimeOfDay;
                }

                return now.NextBusinessDay() + StartTime - now;
            }
        }

        public IEnumerable<ScannerChange> GetScannerChanges(Scanner current)
        {
            var previousScannerRowsBySymbol = _previousScanner.Rows.ToDictionary(x => x.Symbol);
            var scannerRowsBySymbol = current.Rows.ToDictionary(x => x.Symbol);

            // Check for new
            foreach (var symbol in scannerRowsBySymbol.Keys)
            {
                if (!previousScannerRowsBySymbol.ContainsKey(symbol))
                {
                    yield return new ScannerChange(ScannerChangeType.Added, symbol);
                }
            }

            // Check for old
            foreach (var symbol in previousScannerRowsBySymbol.Keys)
            {
                if (!scannerRowsBySymbol.ContainsKey(symbol))
                {
                    yield return new ScannerChange(ScannerChangeType.Removed, symbol);
                }
            }

            // Assign new scanner
            _previousScanner = current;
        }
    }
}