using System;
using System.Collections.Generic;
using System.Text;

namespace AA.Crew.GDFD.DBCleanup
{
    public interface IDBLayer
    {
        public void ClearAuditTrail();
        public void ClearExceptionDetails();
        public void ClearPlotingActivity();        
        public void ClearProcessRunDetails();
        public void ClearProcessRuns();
        public void ClearApplicationLocks();
    }
}
