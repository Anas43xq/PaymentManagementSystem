using clsPaymentEntities;
using PaymentDataLayer;
using System;
using System.Data;

namespace PaymentBusinessLayer
{
    public class clsPaymentServices
    {
        #region Validation

        /// <summary>
        /// Validates payment data before save
        /// </summary>
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

        /// <summary>
        /// Validates date string for filtering
        /// </summary>
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

        #region Payment Operations

        public static ClsPayment Find(int paymentId)
        {
            if (paymentId <= 0) return null;
            return clsPaymentRepo.GetPaymentInfo(paymentId);
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

        public static bool DeletePayment(int paymentId)
        {
            if (paymentId <= 0)
                throw new ArgumentException("Invalid payment ID.");

            return clsPaymentRepo.DeletePayment(paymentId);
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

        public static decimal GetTotalAmountByCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentException("Currency code cannot be empty.");

            return clsPaymentRepo.GetTotalAmountByCurrencyCode(currencyCode);
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

        public static DataTable GetAllTransactions()
        {
            return clsPaymentRepo.GetAllTransactions();
        }

        public static DataTable GetAllCurrencies()
        {
            return clsPaymentRepo.GetAllCurrencies();
        }

        public static DataTable GetAllCategories()
        {
            return clsPaymentRepo.GetAllCategories();
        }

        public static DataTable FilterTransactionByDate(string date)
        {
            if (!ValidateDate(date, out string errorMessage))
                throw new ArgumentException(errorMessage);

            return clsPaymentRepo.FilterTransactionByDate(date);
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

        public static DataTable GetTotalsByCategoryID(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Invalid category ID.");

            return clsPaymentRepo.GetTotalsByCategoryID(categoryId);
        }

        public static DataTable GetTransactionsByCategory(string[] categoryCollection, bool university = true)
        {
            if (categoryCollection == null || categoryCollection.Length == 0)
                return new DataTable();

            return clsPaymentRepo.GetTransactionsByCategory(categoryCollection, university);
        }

        public static DataTable GetTotalsByCategories(string[] categoryCollection, bool university = true)
        {
            if (categoryCollection == null || categoryCollection.Length == 0)
                return new DataTable();

            return clsPaymentRepo.GetTotalsByCategories(categoryCollection, university);
        }

        public static DataTable GetAllCurrencyTotals()
        {
            return clsPaymentRepo.GetAllCurrencyTotals();
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

        public static DataTable GetCurrencyByCode(string currencyCode)
        {
            if (string.IsNullOrWhiteSpace(currencyCode))
                throw new ArgumentException("Currency code cannot be empty.");

            return clsPaymentRepo.GetCurrencyByCode(currencyCode);
        }

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
