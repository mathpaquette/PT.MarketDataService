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

        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }
        public TimeSpan Frequency { get; }
        public ScannerParameter Parameter { get; }
        public DateTime LastUpdate { get; private set; }
        public TimeSpan SinceLastUpdate => _timeProvider.Now - LastUpdate;

        public bool IsOnline()
        {
            var now = _timeProvider.Now;
            if (now.TimeOfDay >= StartTime && now.TimeOfDay <= EndTime && now.IsBusinessDay())
            {
                LastUpdate = now;
                return true;
            }
            return false;
        }

        public async void Start()
        {
            while (!_cts.IsCancellationRequested)
            {
                await _semaphoreSlim.WaitAsync(_cts.Token).ContinueWith(task => { });
                await Task.Delay(UntilExpiration, _cts.Token).ContinueWith(task => { });
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
                var next = now.Add(Frequency).TimeOfDay;

                if (now.IsBusinessDay())
                {
                    if (next >= StartTime && next <= EndTime)
                        return Frequency;

                    if (next < StartTime)
                        return StartTime - now.TimeOfDay;
                }

                return now.NextBusinessDay() + StartTime - now;
            }
        }

        public IEnumerable<ScannerChange> GetScannerChanges(Scanner current)
        {
            var previousScannerRowsByContract = _previousScanner.Rows.ToDictionary(x => x.Contract);
            var scannerRowsByContract = current.Rows.ToDictionary(x => x.Contract);

            // Check for new
            foreach (var contract in scannerRowsByContract.Keys)
            {
                if (!previousScannerRowsByContract.ContainsKey(contract))
                {
                    yield return new ScannerChange(ScannerChangeType.Added, contract);
                }
            }

            // Check for old
            foreach (var contract in previousScannerRowsByContract.Keys)
            {
                if (!scannerRowsByContract.ContainsKey(contract))
                {
                    yield return new ScannerChange(ScannerChangeType.Removed, contract);
                }
            }

            // Assign new scanner
            _previousScanner = current;
        }
    }
}