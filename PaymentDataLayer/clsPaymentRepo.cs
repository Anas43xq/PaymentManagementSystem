using clsPaymentEntities;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace PaymentDataLayer
{
    public static class clsRepoSettings
    {
        internal static string ConnectionString = 
            ConfigurationManager.ConnectionStrings["PaymentDB"]?.ConnectionString 
            ?? "Server=.;Database=DBPayments;Trusted_Connection=True;";
    }

    public static class clsPaymentRepo
    {
        #region Core Database Methods

        private static T ExecuteScalar<T>(string query, Action<SqlCommand> parameterize = null, Func<object, T> map = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    parameterize?.Invoke(cmd);
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        return map != null ? map(result) : (T)Convert.ChangeType(result, typeof(T));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing query: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return default;
        }

        private static bool ExecuteNonQuery(string query, Action<SqlCommand> parameterize = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    parameterize?.Invoke(cmd);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static T ExecuteSingleRow<T>(string query, Action<SqlCommand> parameterize, Func<SqlDataReader, T> map)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    parameterize?.Invoke(cmd);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                            return map(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing query: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return default;
        }

        private static DataTable ExecuteDataTable(string query, Action<SqlCommand> parameterize = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsRepoSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    parameterize?.Invoke(cmd);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing DataTable query: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private static DataRow ExecuteDataRow(string query, Action<SqlCommand> parameterize = null)
        {
            DataTable dt = ExecuteDataTable(query, parameterize);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        #endregion

        #region Generic CRUD Operations

        private static bool RecordExists(string tableName, string columnName, object value)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @Value";
            return ExecuteScalar(query,
                cmd => cmd.Parameters.AddWithValue("@Value", value),
                result => Convert.ToInt32(result) > 0);
        }

        private static bool DeleteRecord(string tableName, string columnName, object value)
        {
            string query = $"DELETE FROM {tableName} WHERE {columnName} = @Value";
            return ExecuteNonQuery(query, cmd => cmd.Parameters.AddWithValue("@Value", value));
        }

        private static string GetFieldById<T>(string tableName, string fieldName, string idColumn, T id)
        {
            string query = $"SELECT {fieldName} FROM {tableName} WHERE {idColumn} = @ID";
            return ExecuteSingleRow(query,
                cmd => cmd.Parameters.AddWithValue("@ID", id),
                reader => reader[fieldName].ToString());
        }

        private static int GetCount(string tableName, string whereClause = null)
        {
            string query = $"SELECT COUNT(*) FROM {tableName}";
            if (!string.IsNullOrEmpty(whereClause))
                query += $" WHERE {whereClause}";

            return ExecuteScalar(query, null, result => Convert.ToInt32(result));
        }

        #endregion

        #region Mapper Functions

        private static ClsPayment MapPaymentFromReader(SqlDataReader reader)
        {
            return new ClsPayment
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                Amount = reader.GetDecimal(reader.GetOrdinal("Amount")),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate")),
                CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                CurrencyID = reader.GetInt32(reader.GetOrdinal("CurrencyID")),
                CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? null : reader.GetString(reader.GetOrdinal("CategoryName")),
                CurrencyCode = reader.IsDBNull(reader.GetOrdinal("CurrencyCode")) ? null : reader.GetString(reader.GetOrdinal("CurrencyCode")),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                Mode = ClsPayment.enMode.Update
            };
        }

        private static ClsCategory MapCategoryFromReader(SqlDataReader reader)
        {
            return new ClsCategory
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Type = reader.IsDBNull(reader.GetOrdinal("Type")) ? null : reader.GetString(reader.GetOrdinal("Type")),
                IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? true : reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }

        private static ClsCurrency MapCurrencyFromReader(SqlDataReader reader)
        {
            return new ClsCurrency
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                Code = reader.GetString(reader.GetOrdinal("Code")),
                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                Symbol = reader.IsDBNull(reader.GetOrdinal("Symbol")) ? null : reader.GetString(reader.GetOrdinal("Symbol")),
                IsActive = reader.IsDBNull(reader.GetOrdinal("IsActive")) ? true : reader.GetBoolean(reader.GetOrdinal("IsActive")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
            };
        }

        #endregion

        #region Parameter Preparation Methods

        private static void PreparePaymentParameters(SqlCommand cmd, ClsPayment payment, bool isUpdate = false)
        {
            if (isUpdate)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = payment.ID;

            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = payment.Amount;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = payment.TransactionDate;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = payment.CategoryID;
            cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = payment.CurrencyID;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(payment.Notes) ? (object)DBNull.Value : payment.Notes;
        }

        #endregion

        #region Payments

        public static ClsPayment GetPaymentInfo(int PaymentID)
        {
            string query = @"SELECT 
                T.ID, T.Amount, T.TransactionDate, T.CategoryID, T.CurrencyID, T.Notes, T.CreatedAt,
                C.Name AS CategoryName, CU.Code AS CurrencyCode
            FROM Transactions T
            INNER JOIN Categories C ON T.CategoryID = C.ID
            INNER JOIN Currencies CU ON T.CurrencyID = CU.ID
            WHERE T.ID = @ID";
            return ExecuteSingleRow(query,
                cmd => cmd.Parameters.AddWithValue("@ID", PaymentID),
                MapPaymentFromReader);
        }

        public static bool SavePayment(ref ClsPayment payment)
        {
            bool success = payment.Mode == ClsPayment.enMode.AddNew
                ? AddPayment(payment)
                : UpdatePayment(payment);

            if (success && payment.Mode == ClsPayment.enMode.AddNew)
                payment.Mode = ClsPayment.enMode.Update;

            return success;
        }

        private static bool AddPayment(ClsPayment payment)
        {
            if (!RecordExists("Categories", "ID", payment.CategoryID))
            {
                MessageBox.Show($"CategoryID {payment.CategoryID} does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!RecordExists("Currencies", "ID", payment.CurrencyID))
            {
                MessageBox.Show($"CurrencyID {payment.CurrencyID} does not exist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string query = @"
                INSERT INTO [dbo].[Transactions]
                    ([Amount],[TransactionDate],[CategoryID],[CurrencyID],[Notes])
                VALUES
                    (@Amount,@TransactionDate,@CategoryID,@CurrencyID,@Notes);
                SELECT SCOPE_IDENTITY();";

            return ExecuteScalar(query,
                cmd => PreparePaymentParameters(cmd, payment),
                result =>
                {
                    if (int.TryParse(result?.ToString(), out int newId))
                    {
                        payment.ID = newId;
                        return true;
                    }
                    return false;
                });
        }

        private static bool UpdatePayment(ClsPayment payment)
        {
            string query = @"
                UPDATE [dbo].[Transactions]
                SET 
                    [Amount]          = @Amount,
                    [TransactionDate] = @TransactionDate,
                    [CategoryID]      = @CategoryID,
                    [CurrencyID]      = @CurrencyID,
                    [Notes]           = @Notes
                WHERE ID = @ID";

            return ExecuteNonQuery(query, cmd => PreparePaymentParameters(cmd, payment, true));
        }

        public static bool DeletePayment(int PaymentID) =>
            DeleteRecord("Transactions", "ID", PaymentID);

        public static bool PaymentExists(int paymentID) =>
            RecordExists("Transactions", "ID", paymentID);

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

        public static int GetCategoryIDByName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) return 0;

            string query = "SELECT ID FROM Categories WHERE Name = @CategoryName";
            return ExecuteScalar(query,
                cmd => cmd.Parameters.AddWithValue("@CategoryName", categoryName),
                result => Convert.ToInt32(result));
        }

        public static string GetCategoryNameByID(int categoryID) =>
            GetFieldById("Categories", "Name", "ID", categoryID);

        public static ClsCategory GetCategoryInfo(int categoryID)
        {
            string query = "SELECT * FROM Categories WHERE ID = @ID";
            return ExecuteSingleRow(query,
                cmd => cmd.Parameters.AddWithValue("@ID", categoryID),
                MapCategoryFromReader);
        }



        public static bool CategoryExists(int categoryID) =>
            RecordExists("Categories", "ID", categoryID);

        public static bool CategoryExistsByName(string categoryName) =>
            RecordExists("Categories", "Name", categoryName);

        #endregion

        #region Currencies

        public static string GetCurrencyCodeByID(int currencyID) =>
            GetFieldById("Currencies", "Code", "ID", currencyID);

        public static int GetCurrencyIDByCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) return 0;

            string query = "SELECT ID FROM Currencies WHERE Code = @CurrencyCode";
            return ExecuteScalar(query,
                cmd => cmd.Parameters.AddWithValue("@CurrencyCode", currencyCode),
                result => Convert.ToInt32(result));
        }

        public static ClsCurrency GetCurrencyInfo(int currencyID)
        {
            string query = "SELECT * FROM Currencies WHERE ID = @ID";
            return ExecuteSingleRow(query,
                cmd => cmd.Parameters.AddWithValue("@ID", currencyID),
                MapCurrencyFromReader);
        }

        public static ClsCurrency GetCurrencyInfoByCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) return null;

            string query = "SELECT * FROM Currencies WHERE Code = @Code";
            return ExecuteSingleRow(query,
                cmd => cmd.Parameters.AddWithValue("@Code", currencyCode),
                MapCurrencyFromReader);
        }



        public static bool CurrencyExists(int currencyID) =>
            RecordExists("Currencies", "ID", currencyID);

        public static bool CurrencyExistsByCode(string currencyCode) =>
            RecordExists("Currencies", "Code", currencyCode);

        #endregion

        #region Reports / Utilities

        public static DataTable FilterTransactionByDate(string date)
        {
            string query = "SELECT * FROM TransactionDetails WHERE TransactionDate = @Date";
            return ExecuteDataTable(query,
                cmd => cmd.Parameters.AddWithValue("@Date", date));
        }

        public static decimal GetTotalAmountByCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) return 0;

            string query = @"
                SELECT ISNULL(SUM(t.Amount), 0) AS TotalAmount
                FROM Transactions t
                INNER JOIN Currencies c ON t.CurrencyID = c.ID
                WHERE c.Code = @CurrencyCode
                GROUP BY c.Code";

            return ExecuteScalar(query,
                cmd => cmd.Parameters.AddWithValue("@CurrencyCode", currencyCode.ToUpper()),
                result => Convert.ToDecimal(result));
        }

        public static DataTable GetTotalsByCategoryID(int categoryID)
        {
            string query = @"
                SELECT 
                    cat.Name AS CatName,
                    c.Code AS CurrencyCode,
                    SUM(t.Amount) AS TotalAmount
                FROM TransactionDetails td
                WHERE cat.ID = @CategoryID
                GROUP BY cat.Name, c.Code";

            return ExecuteDataTable(query,
                cmd => cmd.Parameters.AddWithValue("@CategoryID", categoryID));
        }

        public static DataTable GetTotalsByCategories(string[] categoryCollection, bool university = true)
        {
            if (categoryCollection == null || categoryCollection.Length == 0)
                return new DataTable();

            var paramNames = categoryCollection.Select((c, i) => $"@cat{i}").ToArray();
            string query = $@"
                SELECT c.Code, ISNULL(SUM(t.Amount), 0) AS TotalAmount
                FROM Transactions t
                INNER JOIN Categories cat ON t.CategoryID = cat.ID
                INNER JOIN Currencies c ON t.CurrencyID = c.ID
                WHERE cat.Name {(university ? "" : "NOT")} IN ({string.Join(",", paramNames)})
                GROUP BY c.Code
                ORDER BY c.Code";

            return ExecuteDataTable(query, cmd =>
            {
                for (int i = 0; i < categoryCollection.Length; i++)
                    cmd.Parameters.AddWithValue(paramNames[i], categoryCollection[i]);
            });
        }

        public static DataTable GetTransactionsByCategory(string[] categoryCollection, bool university = true)
        {
            if (categoryCollection == null || categoryCollection.Length == 0)
                return new DataTable();

            var paramNames = categoryCollection.Select((c, i) => $"@cat{i}").ToArray();
            string query = $@"
                SELECT td.*
                FROM TransactionDetails td
                WHERE td.CategoryName {(university ? "" : "NOT")} IN ({string.Join(",", paramNames)})
                ORDER BY td.TransactionDate DESC";

            return ExecuteDataTable(query, cmd =>
            {
                for (int i = 0; i < categoryCollection.Length; i++)
                    cmd.Parameters.AddWithValue(paramNames[i], categoryCollection[i]);
            });
        }

        public static DataTable GetAllTransactions()
        {
            string query = "SELECT * FROM TransactionDetails ORDER BY TransactionDate DESC";
            return ExecuteDataTable(query);
        }

        public static DataTable GetAllCurrencies()
        {
            string query = "SELECT * FROM Currencies WHERE IsActive = 1 ORDER BY Code";
            return ExecuteDataTable(query);
        }

        public static DataTable GetAllCurrencyTotals()
        {
            string query = @"
                SELECT 
                    C.Code, 
                    ISNULL(SUM(T.Amount), 0) AS TotalAmount
                FROM dbo.Currencies AS C
                LEFT JOIN dbo.Transactions AS T ON C.ID = T.CurrencyID
                GROUP BY C.Code
                ORDER BY C.Code";

            return ExecuteDataTable(query);
        }


        public static DataTable GetAllCategories()
        {
            string query = "SELECT ID, Name, IsActive FROM Categories WHERE IsActive = 1 ORDER BY Name";
            return ExecuteDataTable(query);
        }

        public static DataTable GetCategories(string[] collection, bool university = true)
        {
            if (collection == null || collection.Length == 0)
                return new DataTable();

            var paramNames = collection.Select((c, i) => $"@cat{i}").ToArray();
            string query = $@"
                SELECT Name
                FROM Categories
                WHERE Name {(university ? "" : "NOT")} IN ({string.Join(",", paramNames)})
                ORDER BY Name";

            return ExecuteDataTable(query, cmd =>
            {
                for (int i = 0; i < collection.Length; i++)
                    cmd.Parameters.AddWithValue(paramNames[i], collection[i]);
            });
        }

        public static DataTable GetCategoryById(int categoryId)
        {
            string query = "SELECT ID, Name FROM Categories WHERE ID = @CategoryID";
            return ExecuteDataTable(query, cmd => cmd.Parameters.AddWithValue("@CategoryID", categoryId));
        }

        public static bool AddCategory(string categoryName)
        {
            string query = "INSERT INTO Categories (Name, IsActive) VALUES (@Name, 1)";
            return ExecuteNonQuery(query, cmd => cmd.Parameters.AddWithValue("@Name", categoryName));
        }

        public static bool UpdateCategory(int categoryId, string categoryName)
        {
            string query = "UPDATE Categories SET Name = @Name WHERE ID = @CategoryID";
            return ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@Name", categoryName);
            });
        }

        public static bool DeleteCategory(int categoryId)
        {
            string query = "UPDATE Categories SET IsActive = 0 WHERE ID = @CategoryID";
            return ExecuteNonQuery(query, cmd => cmd.Parameters.AddWithValue("@CategoryID", categoryId));
        }

        public static DataTable GetCurrencyByCode(string currencyCode)
        {
            string query = "SELECT ID, Code, Name, Symbol FROM Currencies WHERE Code = @Code AND IsActive = 1";
            return ExecuteDataTable(query, cmd => cmd.Parameters.AddWithValue("@Code", currencyCode));
        }

        public static bool AddCurrency(string currencyCode, string currencyName, string symbol)
        {
            string query = "INSERT INTO Currencies (Code, Name, Symbol, IsActive) VALUES (@Code, @Name, @Symbol, 1)";
            return ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Code", currencyCode);
                cmd.Parameters.AddWithValue("@Name", currencyName);
                cmd.Parameters.AddWithValue("@Symbol", symbol);
            });
        }

        public static bool UpdateCurrency(string currencyCode, string currencyName, string symbol)
        {
            string query = "UPDATE Currencies SET Name = @Name, Symbol = @Symbol WHERE Code = @Code";
            return ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Code", currencyCode);
                cmd.Parameters.AddWithValue("@Name", currencyName);
                cmd.Parameters.AddWithValue("@Symbol", symbol);
            });
        }

        public static bool DeleteCurrency(string currencyCode)
        {
            string query = "UPDATE Currencies SET IsActive = 0 WHERE Code = @Code";
            return ExecuteNonQuery(query, cmd => cmd.Parameters.AddWithValue("@Code", currencyCode));
        }

        #endregion
    }
}
