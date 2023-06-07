using Microsoft.Data.SqlClient;


namespace AA.Crew.GDFD.DBCleanup
{
    public class DBLayer : IDBLayer
    {
        public string _connectionString;
        public DBLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ClearAuditTrail()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlText = "DELETE FROM [dbo].[auditTrail] WHERE created_timestamp < DATEADD(MONTH, -6, GETDATE());";

                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {                    
                    var rows =  cmd.ExecuteNonQuery();
                    
                }
            }
        }
        public void ClearExceptionDetails()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlText = "DELETE FROM [dbo].[exceptionDetails] WHERE last_modify < DATEADD(MONTH, -6, GETDATE());";

                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    var rows = cmd.ExecuteNonQuery();

                }
            }
        }
        public void ClearPlotingActivity()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlText = "DELETE FROM [dbo].[plottingActivity] WHERE run_date < DATEADD(MONTH, -6, GETDATE());";

                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    var rows = cmd.ExecuteNonQuery();

                }
            }
        }
        public void ClearProcessRunDetails()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlText = "DELETE FROM [dbo].[processRunDetails] WHERE CAST((substring(job_id,1,8) +' '+ substring(job_id,9,2) +':'+ substring(job_id,11,2) +':'+ substring(job_id,13,2)) AS datetime) < DATEADD(MONTH, -6, GETDATE());";

                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    var rows = cmd.ExecuteNonQuery();

                }
            }
        }
        public void ClearProcessRuns()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlText = "DELETE FROM [dbo].[processRuns] WHERE start_date < DATEADD(MONTH, -6, GETDATE());";

                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    var rows = cmd.ExecuteNonQuery();

                }
            }
        }
        public void ClearApplicationLocks()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlText = "DELETE FROM[dbo].[applicationLocks] WHERE last_modify < DATEADD(MONTH, -6, GETDATE()); ";

                using (SqlCommand cmd = new SqlCommand(sqlText, conn))
                {
                    var rows = cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
