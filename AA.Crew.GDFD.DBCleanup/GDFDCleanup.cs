using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
namespace AA.Crew.GDFD.DBCleanup
{
    public class GDFDCleanup
    {
        private readonly IDBLayer _dbLayer;
        

        public GDFDCleanup(IDBLayer dbLayer)
        {
            this._dbLayer = dbLayer;
            
        }

        [FunctionName("PurgeOldData")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"GDFD Timer trigger function started at: {DateTime.Now}");            
            
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            log.LogInformation($"ASPNETCORE_ENVIRONMENT value from azure config settings is : {environment}");

            _dbLayer.ClearAuditTrail();
            _dbLayer.ClearExceptionDetails();
            _dbLayer.ClearPlotingActivity();
            _dbLayer.ClearProcessRunDetails();
            _dbLayer.ClearProcessRuns();
            _dbLayer.ClearApplicationLocks();

            log.LogInformation($"GDFD Timer trigger function completed at: {DateTime.Now}");

        }
    }
}
