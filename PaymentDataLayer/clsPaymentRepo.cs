using clsPaymentEntities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PaymentDataLayer
{
    public static class clsRepoSettings
    {
        internal static string ConnectionString = "Server=.;Database=DBPayments;Trusted_Connection=True;";
    }

    public static class clsPaymentRepo
    {
        #region Payments

        public static ClsPayment GetPaymentInfo(int PaymentID)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "SELECT * FROM TransactionDetails WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", PaymentID);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClsPayment payment = new ClsPayment
                            {
                                ID = (int)reader["ID"],
                                Description = reader["Description"] as string,
                                Amount = (decimal)reader["Amount"],
                                TransactionDate = (DateTime)reader["TransactionDate"],
                                CategoryName = reader["CategoryName"] as string,
                                CurrencyCode = reader["CurrencyCode"] as string,
                                Notes = reader["Notes"] as string,
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                Mode = ClsPayment.enMode.Update
                            };
                            return payment;
                        }
                    }
                }
            }
            return null;
        }
        public static bool AddPayment(ClsPayment Payment)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                conn.Open();

                // Validate Category
                using (SqlCommand checkCategory = new SqlCommand(
                    "SELECT COUNT(*) FROM Categories WHERE ID = @CategoryID", conn))
                {
                    checkCategory.Parameters.AddWithValue("@CategoryID", Payment.CategoryID);
                    if ((int)checkCategory.ExecuteScalar() == 0)
                        throw new ArgumentException($"CategoryID {Payment.CategoryID} does not exist.");
                }

                // Validate Currency
                using (SqlCommand checkCurrency = new SqlCommand(
                    "SELECT COUNT(*) FROM Currencies WHERE ID = @CurrencyID", conn))
                {
                    checkCurrency.Parameters.AddWithValue("@CurrencyID", Payment.CurrencyID);
                    if ((int)checkCurrency.ExecuteScalar() == 0)
                        throw new ArgumentException($"CurrencyID {Payment.CurrencyID} does not exist.");
                }

                // Insert Transaction
                string sql = @"
                    INSERT INTO [dbo].[Transactions]
                        ([Description],[Amount],[TransactionDate],[CategoryID],[CurrencyID],[Notes])
                    VALUES
                        (@Description,@Amount,@TransactionDate,@CategoryID,@CurrencyID,@Notes);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(Payment.Description) ? (object)DBNull.Value : Payment.Description);
                    cmd.Parameters.AddWithValue("@Amount", Payment.Amount);
                    cmd.Parameters.AddWithValue("@TransactionDate", Payment.TransactionDate);
                    cmd.Parameters.AddWithValue("@CategoryID", Payment.CategoryID);
                    cmd.Parameters.AddWithValue("@CurrencyID", Payment.CurrencyID);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Payment.Notes) ? (object)DBNull.Value : Payment.Notes);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool UpdatePayment(ClsPayment Payment)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = @"
                    UPDATE [dbo].[Transactions]
                    SET 
                        [Description]     = @Description,
                        [Amount]          = @Amount,
                        [TransactionDate] = @TransactionDate,
                        [CategoryID]      = @CategoryID,
                        [CurrencyID]      = @CurrencyID,
                        [Notes]           = @Notes
                    WHERE ID = @ID;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", Payment.ID);
                    cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(Payment.Description) ? (object)DBNull.Value : Payment.Description);
                    cmd.Parameters.AddWithValue("@Amount", Payment.Amount);
                    cmd.Parameters.AddWithValue("@TransactionDate", Payment.TransactionDate);
                    cmd.Parameters.AddWithValue("@CategoryID", Payment.CategoryID);
                    cmd.Parameters.AddWithValue("@CurrencyID", Payment.CurrencyID);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Payment.Notes) ? (object)DBNull.Value : Payment.Notes);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public static bool DeletePayment(int PaymentID)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "DELETE FROM Transactions WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", PaymentID);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
       
        }
        public static bool Save(ClsPayment Payment)
        {
            switch (Payment.Mode)
            {
                case ClsPayment.enMode.AddNew:
                    if (AddPayment(Payment))
                    {
                        Payment.Mode = ClsPayment.enMode.Update;
                        return true;
                    }
                    break;

                case ClsPayment.enMode.Update:
                    return UpdatePayment(Payment);
            }
            return false;
        }

        #endregion

        #region Categories

        public static int GetCategoryIDByName(string CategoryName)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "SELECT ID FROM Categories WHERE Name = @CategoryName";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public static bool AddCategory(string CategoryName)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "INSERT INTO Categories (Name) VALUES (@Name)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", CategoryName);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool UpdateCategoryByID(int CategoryID, string NewName)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "UPDATE Categories SET Name = @Name WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", CategoryID);
                    cmd.Parameters.AddWithValue("@Name", NewName);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool DeleteCategoryByID(int CategoryID)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "DELETE FROM Categories WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", CategoryID);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        #endregion

        #region Currencies

        public static string GetCurrencyCodeByID(int CurrencyID)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "SELECT Code FROM Currencies WHERE ID = @CurrencyID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrencyID", CurrencyID);
                    conn.Open();
                    return cmd.ExecuteScalar()?.ToString() ?? "";
                }
            }
        }

        public static int GetCurrencyIDByCode(string CurrencyCode)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "SELECT ID FROM Currencies WHERE Code = @CurrencyCode";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public static bool AddCurrency(string CurrencyCode)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "INSERT INTO Currencies (Code) VALUES (@Code)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Code", CurrencyCode);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool DeleteCurrencyByID(int CurrencyID)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = "DELETE FROM Currencies WHERE ID = @ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", CurrencyID);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        #endregion

        #region Reports / Utilities

        public static decimal GetTotalAmountByCurrencyCode(string CurrencyCode)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string sql = @"
                    SELECT SUM(t.Amount) AS TotalAmount
                    FROM Transactions t
                    INNER JOIN Currencies c ON t.CurrencyID = c.ID
                    WHERE c.Code = @CurrencyCode
                    GROUP BY c.Code;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode.ToUpper());
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? (decimal)result : 0;
                }
            }
        }

        public static DataTable GetTotalsByCategoryID(int categoryID)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                string query = @"
            SELECT 
                cat.Name AS CatName,
                c.Code AS CurrencyCode,
                SUM(t.Amount) AS TotalAmount
               FROM Transactions t
               INNER JOIN Categories cat ON t.CategoryID = cat.ID
               INNER JOIN Currencies c ON t.CurrencyID = c.ID
               WHERE cat.ID = @CategoryID
               GROUP BY cat.Name, c.Code;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    return dt;
                }
            }
        }

        public static DataTable GetTotalsByCategoriesID(string[] CategoryCollection, bool University = true)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                if (CategoryCollection == null || CategoryCollection.Length == 0)
                    return new DataTable();

                var paramNames = CategoryCollection.Select((c, i) => $"@cat{i}").ToArray();
                string sql = $@"
               SELECT c.Code, SUM(t.Amount) AS TotalAmount
               FROM Transactions t
               INNER JOIN Categories cat ON t.CategoryID = cat.ID
               INNER JOIN Currencies c ON t.CurrencyID = c.ID
               WHERE cat.Name {(University ? "" : "Not")} IN ({string.Join(",", paramNames)})
               GROUP BY c.Code
               ORDER BY c.Code;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    for (int i = 0; i < CategoryCollection.Length; i++)
                        cmd.Parameters.AddWithValue(paramNames[i], CategoryCollection[i]);

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

        public static DataTable GetTransactionsByCategory(string[] CategoryCollection, bool University = true)
        {
            using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
            {
                if (CategoryCollection == null || CategoryCollection.Length == 0)
                    return new DataTable();

                var paramNames = CategoryCollection.Select((c, i) => $"@cat{i}").ToArray();
                string sql = $@"
                            SELECT td.*
                            FROM TransactionDetails td
                            INNER JOIN Categories cat ON td.CategoryName = cat.Name
                            INNER JOIN Currencies c ON td.CurrencyCode = c.Code
                            WHERE cat.Name {(University ? "" : "NOT")} IN ({string.Join(",", paramNames)})
                            ORDER BY td.TransactionDate DESC;
                                                          ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    for (int i = 0; i < CategoryCollection.Length; i++)
                        cmd.Parameters.AddWithValue(paramNames[i], CategoryCollection[i]);

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

        #endregion
    }
}
