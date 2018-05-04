using System;
using System.Collections.Generic;
using PT.MarketDataService.Core.ValueObjects;

namespace PT.MarketDataService.Core.Events
{
    public class ScannerChangeEventArgs : EventArgs
    {
        public int ScannerParameterId { get; }
        public bool ScannerOnline { get; }
        public IEnumerable<ScannerChange> ScannerChanges { get; }

        public ScannerChangeEventArgs(int scannerParameterId, IEnumerable<ScannerChange> scannerChanges, bool scannerOnline)
        {
            ScannerParameterId = scannerParameterId;
            ScannerChanges = scannerChanges;
            ScannerOnline = scannerOnline;
        }
    }
}