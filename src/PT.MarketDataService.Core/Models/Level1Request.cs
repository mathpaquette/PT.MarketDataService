using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PT.MarketDataService.Core.Extensions;

namespace PT.MarketDataService.Core.Models
{
    public class Level1Request
    {
        public event EventHandler Timeout;

        private CancellationTokenSource _cts;
        private SemaphoreSlim _semaphoreSlim;

        private readonly HashSet<int> _scannerParameterIds;
        private readonly object _mutex = new Object();

        public Level1Request(string symbol, int frequency)
        {
            Symbol = symbol;
            Frequency = TimeSpan.FromSeconds(frequency);
            _scannerParameterIds = new HashSet<int>();
        }

        public string Symbol { get; }
        public TimeSpan Frequency { get; }
        public DateTime LastUpdate { get; private set; }

        public bool HasExpired()
        {
            lock (_mutex)
            {
                var now = DateTime.Now;
                if (now.Subtract(LastUpdate) >= Frequency)
                {
                    LastUpdate = now;
                    return true;
                }
                return false;
            }
        }

        public bool Online
        {
            get
            {
                lock (_scannerParameterIds)
                {
                    return _scannerParameterIds.Count > 0;
                }
            }
        }

        public async void Start()
        {
            _semaphoreSlim = new SemaphoreSlim(0, 1);
            _cts = new CancellationTokenSource();

            while (Online)
            {
                await _semaphoreSlim.WaitAsync(_cts.Token).ContinueWith(task => { });
                await Task.Delay(UntilExpiration, _cts.Token).ContinueWith(task => { });
                Timeout.RaiseEvent(this, EventArgs.Empty);
            }
        }

        public void Signal()
        {
            _semaphoreSlim.Release();
        }

        private TimeSpan UntilExpiration
        {
            get
            {
                var diff = DateTime.Now.Subtract(LastUpdate);
                return diff >= Frequency ? Frequency : Frequency - diff;
            }
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        public bool TrySetOnline(int scannerParameterId)
        {
            lock (_scannerParameterIds)
            {
                var prevCount = _scannerParameterIds.Count;
                _scannerParameterIds.Add(scannerParameterId);
                return prevCount == 0;
            }
        }

        public bool TrySetOffline(int scannerParameterId)
        {
            lock (_scannerParameterIds)
            {
                _scannerParameterIds.Remove(scannerParameterId);
                return _scannerParameterIds.Count == 0;
            }
        }
    }
}