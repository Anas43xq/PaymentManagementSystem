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

        #region Users

        private static void PrepareUserParameters(SqlCommand cmd, clsPaymentEntities.ClsUser user, bool isUpdate = false)
        {
            if (isUpdate)
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user.ID;

            cmd.Parameters.Add("@Username", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(user.Username) ? (object)DBNull.Value : user.Username;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(user.Email) ? (object)DBNull.Value : user.Email;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarBinary).Value = user.PasswordHash ?? (object)DBNull.Value;
            cmd.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary).Value = user.PasswordSalt ?? (object)DBNull.Value;
        }

        public static clsPaymentEntities.ClsUser GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return null;

            string query = "SELECT UserId, Username, Email, PasswordHash, PasswordSalt, CreatedAt FROM dbo.Users WHERE Username = @Username";
            return ExecuteSingleRow(query,
                cmd => cmd.Parameters.AddWithValue("@Username", username),
                MapUserFromReader);
        }

        public static bool CreateUser(string username, string email, string password)
        {
            var user = new clsPaymentEntities.ClsUser
            {
                Username = username,
                Email = email
            };

            AuthHelper.CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            return AddUser(user);
        }

        public static bool AddUser(clsPaymentEntities.ClsUser user)
        {
            if (user == null) return false;

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (RecordExists("Users", "Username", user.Username))
            {
                MessageBox.Show("Username already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string query = @"
                INSERT INTO dbo.Users (Username, Email, PasswordHash, PasswordSalt, CreatedAt)
                VALUES (@Username, @Email, @PasswordHash, @PasswordSalt, GETDATE());
                SELECT SCOPE_IDENTITY();";

            return ExecuteScalar(query,
                cmd => PrepareUserParameters(cmd, user),
                result =>
                {
                    if (int.TryParse(result?.ToString(), out int newId))
                    {
                        user.ID = newId;
                        return true;
                    }
                    return false;
                });
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
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
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

        private static clsPaymentEntities.ClsUser MapUserFromReader(SqlDataReader reader)
        {
            return new clsPaymentEntities.ClsUser
            {
                ID = reader.GetInt32(reader.GetOrdinal("UserId")),
                Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? null : reader.GetString(reader.GetOrdinal("Username")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                PasswordHash = reader.IsDBNull(reader.GetOrdinal("PasswordHash")) ? null : (byte[])reader[reader.GetOrdinal("PasswordHash")],
                PasswordSalt = reader.IsDBNull(reader.GetOrdinal("PasswordSalt")) ? null : (byte[])reader[reader.GetOrdinal("PasswordSalt")],
                CreatedAt = reader.IsDBNull(reader.GetOrdinal("CreatedAt")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }

        #endregion

        #region Parameter Preparation Methods

        private static void PreparePaymentParameters(SqlCommand cmd, ClsPayment payment, bool isUpdate = false)
        {
            if (isUpdate)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = payment.ID;

            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = payment.Amount;
            cmd.Parameters.Add("@TransactionDate", SqlDbType.Date).Value = payment.TransactionDate;
            cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = payment.CategoryID;
            cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = payment.CurrencyID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = payment.UserID;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(payment.Notes) ? (object)DBNull.Value : payment.Notes;
        }

        #endregion

        #region Payments

        public static ClsPayment GetPaymentInfo(int PaymentID, int UserID)
        {
            string query = @"SELECT 
                T.ID, T.Amount, T.TransactionDate, T.CategoryID, T.CurrencyID, T.UserID, T.Notes, T.CreatedAt,
                C.Name AS CategoryName, CU.Code AS CurrencyCode
            FROM Transactions T
            INNER JOIN Categories C ON T.CategoryID = C.ID
            INNER JOIN Currencies CU ON T.CurrencyID = CU.ID
            WHERE T.ID = @ID AND T.UserID = @UserID";

            return ExecuteSingleRow(query,
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@ID", PaymentID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                },
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
            if (payment.UserID <= 0)
            {
                MessageBox.Show("UserID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

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
                    ([Amount],[TransactionDate],[CategoryID],[CurrencyID],[UserID],[Notes],[CreatedAt])
                VALUES
                    (@Amount,@TransactionDate,@CategoryID,@CurrencyID,@UserID,@Notes,GETDATE());
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
                WHERE ID = @ID AND UserID = @UserID";

            return ExecuteNonQuery(query, cmd => PreparePaymentParameters(cmd, payment, true));
        }

        public static bool DeletePayment(int PaymentID, int UserID)
        {
            string query = "DELETE FROM Transactions WHERE ID = @ID AND UserID = @UserID";
            return ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@ID", PaymentID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
            });
        }

        public static bool PaymentExists(int paymentID, int UserID)
        {
            string query = "SELECT COUNT(*) FROM Transactions WHERE ID = @ID AND UserID = @UserID";
            return ExecuteScalar(query,
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@ID", paymentID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                },
                result => Convert.ToInt32(result) > 0);
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


        private static ClsCategory GetCategoryBy(string columnName, object value)
        {
            string query = $"SELECT * FROM Categories WHERE {columnName} = @Value";
            return ExecuteSingleRow(query,
                cmd => cmd.Parameters.AddWithValue("@Value", value),
                MapCategoryFromReader);
        }

        private static bool CategoryExistsBy(string columnName, object value) =>
            RecordExists("Categories", columnName, value);

        private static void PrepareCategoryParameters(SqlCommand cmd, ClsCategory category, bool isUpdate = false)
        {
            if (isUpdate)
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = category.ID;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value =
                string.IsNullOrEmpty(category.Name) ? (object)DBNull.Value : category.Name;

            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = category.IsActive;
        }

        public static int GetCategoryIDByName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) return 0;
            return ExecuteScalar("SELECT ID FROM Categories WHERE Name = @Name",
                cmd => cmd.Parameters.AddWithValue("@Name", categoryName),
                result => Convert.ToInt32(result));
        }

        public static string GetCategoryNameByID(int categoryID) =>
            GetFieldById("Categories", "Name", "ID", categoryID);


        public static ClsCategory GetCategoryInfo(int categoryID) =>
            GetCategoryBy("ID", categoryID);

        public static ClsCategory GetCategoryInfoByName(string categoryName) =>
            GetCategoryBy("Name", categoryName);


        public static bool CategoryExists(int categoryID) => CategoryExistsBy("ID", categoryID);

        public static bool CategoryExistsByName(string categoryName) => CategoryExistsBy("Name", categoryName);


        public static DataTable GetAllCategories() =>
            ExecuteDataTable("SELECT ID, Name, IsActive FROM Categories WHERE IsActive = 1 ORDER BY Name");

        public static DataTable GetCategoryById(int categoryID) =>
            ExecuteDataTable("SELECT ID, Name FROM Categories WHERE ID = @ID",
                cmd => cmd.Parameters.AddWithValue("@ID", categoryID));

        public static DataTable GetCategories(string[] collection, bool includeListed = true)
        {
            if (collection == null || collection.Length == 0) return new DataTable();

            var paramNames = collection.Select((c, i) => $"@cat{i}").ToArray();
            string query = $@"
            SELECT Name
            FROM Categories
            WHERE Name {(includeListed ? "" : "NOT")} IN ({string.Join(",", paramNames)})
            ORDER BY Name";

            return ExecuteDataTable(query, cmd =>
            {
                for (int i = 0; i < collection.Length; i++)
                    cmd.Parameters.AddWithValue(paramNames[i], collection[i]);
            });
        }

        //===============================
        // 6. ADD / UPDATE / DELETE
        //===============================

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

        //public static bool AddCategory(ClsCategory category)
        //{
        //    string query = "INSERT INTO Categories (Name, IsActive) VALUES (@Name, @IsActive)";
        //    return ExecuteNonQuery(query, cmd => PrepareCategoryParameters(cmd, category));
        //}

        //public static bool UpdateCategory(ClsCategory category)
        //{
        //    string query = "UPDATE Categories SET Name = @Name, IsActive = @IsActive WHERE ID = @ID";
        //    return ExecuteNonQuery(query, cmd => PrepareCategoryParameters(cmd, category, true));
        //}

        public static bool DeleteCategory(int categoryID)
        {
            // Soft delete by setting IsActive = 0
            string query = "UPDATE Categories SET IsActive = 0 WHERE ID = @ID";
            return ExecuteNonQuery(query, cmd => cmd.Parameters.AddWithValue("@ID", categoryID));
        }

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

        #region Reports / Utilities - USER SPECIFIC

        public static DataTable FilterTransactionByDate(string date, int userId)
        {
            string query = @"
        SELECT 
              ID
            , UserID
            , Amount
            , FormattedAmount
            , TransactionDate
            , CategoryName
            , CurrencyCode
            , Notes
            , CreatedAt
        FROM [DBPayments].[dbo].[TransactionDetails]
        WHERE TransactionDate = @Date
          AND UserID = @UserID;
    ";

            return ExecuteDataTable(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@UserID", userId);
            });
        }

        public static decimal GetTotalAmountByCurrencyCode(string currencyCode, int userId)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                return 0;

            string query = @"
        SELECT ISNULL(SUM(Amount), 0) AS TotalAmount
        FROM [DBPayments].[dbo].[TransactionDetails]
        WHERE CurrencyCode = @CurrencyCode
          AND UserID = @UserID
        GROUP BY CurrencyCode;
    ";

            return ExecuteScalar(
                query,
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@CurrencyCode", currencyCode.ToUpper());
                    cmd.Parameters.AddWithValue("@UserID", userId);
                },
                result => Convert.ToDecimal(result)
            );
        }


        public static DataTable GetTotalsByCategoryID(int categoryID, int UserID)
        {
            string query = @"
                SELECT 
                    cat.Name AS CatName,
                    c.Code AS CurrencyCode,
                    SUM(t.Amount) AS TotalAmount
                FROM Transactions t
                INNER JOIN Categories cat ON t.CategoryID = cat.ID
                INNER JOIN Currencies c ON t.CurrencyID = c.ID
                WHERE cat.ID = @CategoryID AND t.UserID = @UserID
                GROUP BY cat.Name, c.Code";

            return ExecuteDataTable(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
            });
        }

        private static (string sqlInClause, Action<SqlCommand> binder) BuildCategoryFilter(string[] categories, int userId, bool university)
        {
            var paramNames = categories.Select((c, i) => $"@cat{i}").ToArray();
            string inClause = string.Join(",", paramNames);

            string sqlIn = $"CategoryName {(university ? "" : "NOT ")}IN ({inClause}) AND UserID = @UserID";

            // binder adds all SQL parameters
            void binder(SqlCommand cmd)
            {
                for (int i = 0; i < categories.Length; i++)
                    cmd.Parameters.AddWithValue(paramNames[i], categories[i]);

                cmd.Parameters.AddWithValue("@UserID", userId);
            }

            return (sqlIn, binder);
        }

        public static DataTable GetTotalsByCategories(string[] categories, int userId, bool university = true)
        {
            if (categories == null || categories.Length == 0)
                return new DataTable();

            var (filter, binder) = BuildCategoryFilter(categories, userId, university);

            string query = $@"
              SELECT 
                  CurrencyCode AS Code,
                  ISNULL(SUM(Amount), 0) AS TotalAmount
              FROM [DBPayments].[dbo].[TransactionDetails]
              WHERE {filter}
              GROUP BY CurrencyCode
              ORDER BY CurrencyCode;
              ";

            return ExecuteDataTable(query, binder);
        }

        public static DataTable GetTransactionsByCategory(string[] categories, int userId, bool university = true)
        {
            if (categories == null || categories.Length == 0)
                return new DataTable();

            var (filter, binder) = BuildCategoryFilter(categories, userId, university);

            string query = $@"
        SELECT 
              ID
            , UserID
            , Amount
            , FormattedAmount
            , TransactionDate
            , CategoryName
            , CurrencyCode
            , Notes
            , CreatedAt
        FROM [DBPayments].[dbo].[TransactionDetails]
        WHERE {filter}
        ORDER BY TransactionDate DESC;
    ";

            return ExecuteDataTable(query, binder);
        }

        public static DataTable GetCategoryTotals(int userID)
        {
            // Get current year's data by default
            DateTime fromDate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime toDate = new DateTime(DateTime.Now.Year, 12, 31);

            return GetCategoryTotalsByDateRange(userID, fromDate, toDate);
        }

        public static DataTable GetCategoryTotalsByDateRange(int userID, DateTime fromDate, DateTime toDate)
        {
            string query = @"
        SELECT
            UserID,
            CategoryName,
            CurrencyCode,
            SUM(Amount) AS TotalAmount
        FROM TransactionDetails
        WHERE UserID = @UserID
          AND TransactionDate BETWEEN @FromDate AND @ToDate
        GROUP BY
            UserID,
            CategoryName,
            CurrencyCode
        ORDER BY
            CategoryName,
            CurrencyCode";

            return ExecuteDataTable(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
            });
        }

        public static DataTable GetAllTransactions(int UserID)
        {
            string query = @"SELECT T.*, C.Name AS CategoryName, CU.Code AS CurrencyCode
                FROM Transactions T
                INNER JOIN Categories C ON T.CategoryID = C.ID
                INNER JOIN Currencies CU ON T.CurrencyID = CU.ID
                WHERE T.UserID = @UserID
                ORDER BY T.TransactionDate DESC";

            return ExecuteDataTable(query, cmd => cmd.Parameters.AddWithValue("@UserID", UserID));
        }

        public static DataTable GetAllCurrencies()
        {
            string query = "SELECT * FROM Currencies WHERE IsActive = 1 ORDER BY Code";
            return ExecuteDataTable(query);
        }

        public static DataTable GetAllCurrencyTotals(int UserID)
        {
            string query = @"
                SELECT 
                    C.Code, 
                    ISNULL(SUM(T.Amount), 0) AS TotalAmount
                FROM dbo.Currencies AS C
                LEFT JOIN dbo.Transactions AS T ON C.ID = T.CurrencyID AND T.UserID = @UserID
                GROUP BY C.Code
                ORDER BY C.Code";

            return ExecuteDataTable(query, cmd => cmd.Parameters.AddWithValue("@UserID", UserID));
        }

        public static DataTable GetCurrencyByCode(string currencyCode)
        {
            string query = "SELECT ID, Code, Name, Symbol FROM Currencies WHERE Code = @Code AND IsActive = 1";
            return ExecuteDataTable(query, cmd => cmd.Parameters.AddWithValue("@Code", currencyCode));
        }

        public static bool AddCurrency(string currencyCode, string currencyName, string symbol)
        {
            string query = "INSERT INTO Currencies (Code, Name, Symbol, IsActive) VALUES (@Code, @Name, @Symbol, GETDATE())";
            return ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Code", currencyCode);
                cmd.Parameters.AddWithValue("@Name", currencyName);
                cmd.Parameters.AddWithValue("@Symbol", symbol);
            });
        }

        public static bool UpdateCurrency(string currencyCode, string currencyName, string symbol)
        {
            string query = "UPDATE Currencies SET Name = @Name, Symbol = @Symbol, UpdatedAt = GETDATE() WHERE Code = @Code";
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