using System;
using System.Collections.Generic;
using PT.MarketDataService.Core.ValueObjects;

namespace PT.MarketDataService.Core.Events
{
    public class ScannerChangeEventArgs : EventArgs
    {
        public int ScannerParameterId { get; set; }
        public IEnumerable<ScannerChange> ScannerChanges { get; }

        public ScannerChangeEventArgs(int scannerParameterId, IEnumerable<ScannerChange> scannerChanges)
        {
            ScannerParameterId = scannerParameterId;
            ScannerChanges = scannerChanges;
        }
    }
}