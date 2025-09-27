using System.Data;
using System.Data.SqlClient;

namespace PaymentDataLayer
{
    internal static class clsPaymentRepoHelpers
    {

        public static DataTable GetAllCategories()
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "SELECT Name FROM Categories";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }

        public static DataTable GetAllCurrencies()
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "SELECT Code FROM Currencies";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }

        public static DataTable GetAllTransactions()
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "SELECT * FROM TransactionDetails";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
        }
    }
}