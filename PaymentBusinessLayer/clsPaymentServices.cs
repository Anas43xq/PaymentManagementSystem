using clsPaymentEntities;
using PaymentDataLayer;
using System;
using System.Data;

namespace PaymentBusinessLayer
{
    public class clsPaymentServices
    {
        #region Validation

        private static bool ValidatePayment(ClsPayment payment, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (payment == null)
            {
                errorMessage = "Payment object cannot be null.";
                return false;
            }

            if (payment.Amount <= 0)
            {
                errorMessage = PaymentConstants.Messages.InvalidAmount;
                return false;
            }

            if (payment.TransactionDate > DateTime.Now.AddDays(1))
            {
                errorMessage = PaymentConstants.Messages.InvalidDate;
                return false;
            }

            if (payment.CategoryID <= 0)
            {
                errorMessage = "Please select a valid category.";
                return false;
            }

            if (payment.CurrencyID <= 0)
            {
                errorMessage = "Please select a valid currency.";
                return false;
            }

            return true;
        }

        private static bool ValidateDate(string date, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(date))
            {
                errorMessage = "Date cannot be empty.";
                return false;
            }

            if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                errorMessage = PaymentConstants.Messages.InvalidDate;
                return false;
            }

            return true;
        }

        #endregion

        #region Authentication / User Management

        public static bool RegisterUser(string username, string email, string password, string roleName = "User")
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            if (!string.IsNullOrWhiteSpace(email) && !email.Contains("@"))
                throw new ArgumentException("Email is not in a valid format.");


            var existing = PaymentDataLayer.clsPaymentRepo.GetUserByUsername(username);
            if (existing != null)
                throw new ArgumentException("Username already exists.");


            bool ok = PaymentDataLayer.clsPaymentRepo.CreateUser(username, email, password);
            if (!ok)
                throw new InvalidOperationException("Failed to create user");

            

            return true;
        }

        public static clsPaymentEntities.ClsUser AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            return ValidateUser(username, password);
        }

        public static clsPaymentEntities.ClsUser ValidateUser(string username, string password)
        {
            var user = clsPaymentRepo.GetUserByUsername(username);
            if (user == null) return null;

            if (user.PasswordHash == null || user.PasswordSalt == null)
                return null;

            bool ok = AuthHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
            return ok ? user : null;
        }

        #endregion

        #region Payment Operations

        public static ClsPayment Find(int paymentId,int UserID)
        {
            if (paymentId <= 0) return null;
            return clsPaymentRepo.GetPaymentInfo(paymentId,UserID);
        }

        public static string[] GetUniversityCategories()
        {
            return PaymentConstants.UniversityCategories.GetAll();
        }

        private static bool AddPayment(ClsPayment payment)
        {
            if (!ValidatePayment(payment, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            return clsPaymentRepo.SavePayment(ref payment);
        }

        private static bool EditPayment(ClsPayment payment)
        {
            if (!ValidatePayment(payment, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            return clsPaymentRepo.SavePayment(ref payment);
        }

        public static bool DeletePayment(int paymentId,int UserID)
        {
            if (paymentId <= 0)
                throw new ArgumentException("Invalid payment ID.");

            return clsPaymentRepo.DeletePayment(paymentId, UserID);
        }

        public static bool Save(ref ClsPayment payment)
        {
            if (!ValidatePayment(payment, out string errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            switch (payment.Mode)
            {
                case ClsPayment.enMode.AddNew:
                    if (AddPayment(payment))
                    {
                        payment.Mode = ClsPayment.enMode.Update;
                        return true;
                    }
                    break;

                case ClsPayment.enMode.Update:
                    return EditPayment(payment);
            }

            return false;
        }

        #endregion

        #region Query Operations

        public static decimal GetTotalAmountByCurrencyCode(string currencyCode,int UserId)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentException("Currency code cannot be empty.");

            return clsPaymentRepo.GetTotalAmountByCurrencyCode(currencyCode, UserId);
        }

        public static string GetCurrencyCodeByID(int currencyId)
        {
            if (currencyId <= 0) return string.Empty;
            return clsPaymentRepo.GetCurrencyCodeByID(currencyId);
        }

        public static int GetCurrencyIDByCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode)) return 0;
            return clsPaymentRepo.GetCurrencyIDByCode(currencyCode);
        }

        public static DataTable GetAllTransactions(int UserID)
        {
            return clsPaymentRepo.GetAllTransactions( UserID);
        }

        public static DataTable GetAllCurrencies()
        {
            return clsPaymentRepo.GetAllCurrencies();
        }

        public static DataTable GetCurrencyByCode(string currencyCode) => clsPaymentRepo.GetCurrencyByCode(currencyCode);

        public static DataTable GetAllCurrencyTotals(int UserID) => clsPaymentRepo.GetAllCurrencyTotals(UserID);

        public static DataTable GetAllCategories()
        {
            return clsPaymentRepo.GetAllCategories();
        }

        public static DataTable FilterTransactionByDate(string date,int UserID)
        {
            if (!ValidateDate(date, out string errorMessage))
                throw new ArgumentException(errorMessage);

            return clsPaymentRepo.FilterTransactionByDate(date,UserID);
        }

        public static DataTable GetCategories(string[] collection, bool university = true)
        {
            if (collection == null || collection.Length == 0)
                return new DataTable();

            return clsPaymentRepo.GetCategories(collection, university);
        }

        public static int GetCategoryIDByName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) return 0;
            return clsPaymentRepo.GetCategoryIDByName(categoryName);
        }

        public static DataTable GetTotalsByCategoryID(int categoryID, int UserID)
        {
            if (categoryID <= 0)
                throw new ArgumentException("Invalid category ID.");

            return clsPaymentRepo.GetTotalsByCategoryID(categoryID, UserID);
        }

        public static DataTable GetTransactionsByCategory(string[] categoryCollection,int UserID ,bool university = true)
        {
            if (categoryCollection == null || categoryCollection.Length == 0)
                return new DataTable();

            return clsPaymentRepo.GetTransactionsByCategory(categoryCollection, UserID, university);
        }

        public static DataTable GetTotalsByCategories(string[] categoryCollection, int UserID, bool university = true)
        {
            if (categoryCollection == null || categoryCollection.Length == 0)
                return new DataTable();

            return clsPaymentRepo.GetTotalsByCategories(categoryCollection, UserID, university);
        }


        #endregion

        #region Category Management

        public static DataTable GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID.");

            return clsPaymentRepo.GetCategoryById(categoryId);
        }

        public static bool AddCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category name cannot be empty.");

            return clsPaymentRepo.AddCategory(categoryName);
        }

        public static DataTable GetCategoryTotalsByDateRange(int userID, DateTime fromDate, DateTime toDate) => clsPaymentRepo.GetCategoryTotalsByDateRange(userID, fromDate, toDate);

        public static bool UpdateCategory(int categoryId, string categoryName)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID.");

            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category name cannot be empty.");

            return clsPaymentRepo.UpdateCategory(categoryId, categoryName);
        }

        public static bool DeleteCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID.");

            return clsPaymentRepo.DeleteCategory(categoryId);
        }

        #endregion

        #region Currency Management


        public static bool AddCurrency(string currencyCode, string currencyName, string symbol)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentException("Currency code cannot be empty.");

            if (string.IsNullOrWhiteSpace(currencyName))
                throw new ArgumentException("Currency name cannot be empty.");

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Currency symbol cannot be empty.");

            return clsPaymentRepo.AddCurrency(currencyCode, currencyName, symbol);
        }

        public static bool UpdateCurrency(string currencyCode, string currencyName, string symbol)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentException("Currency code cannot be empty.");

            if (string.IsNullOrWhiteSpace(currencyName))
                throw new ArgumentException("Currency name cannot be empty.");

            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Currency symbol cannot be empty.");

            return clsPaymentRepo.UpdateCurrency(currencyCode, currencyName, symbol);
        }

        public static bool DeleteCurrency(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentException("Currency code cannot be empty.");

            return clsPaymentRepo.DeleteCurrency(currencyCode);
        }

        #endregion
    }
}
