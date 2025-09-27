using clsPaymentEntities;
using PaymentDataLayer;
using System.Data;

namespace PaymentBusinessLayer
{
    public class clsPaymentServices
    {
        public static ClsPayment Find(int paymentId)
        {
            return clsPaymentRepo.GetPaymentInfo(paymentId);
        }

        public static string[] GetUniversityCategories()
        {
            string[] UniversityCategory = { "Registration", "UnivPayment", "BUS", "EXAM" };

            return UniversityCategory;
        }

        private static bool AddPayment(ClsPayment payment)
        {
            return clsPaymentRepo.AddPayment(payment);
        }

        private static bool EditPayment(ClsPayment payment)
        {
            return clsPaymentRepo.UpdatePayment(payment);
        }

        public static bool DeletePayment(int paymentId)
        {
            return clsPaymentRepo.DeletePayment(paymentId);
        }

        public static bool Save(ref ClsPayment payment)
        {
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

        public static decimal GetTotalAmountByCurrencyCode(string currencyCode)
        {
            return clsPaymentRepo.GetTotalAmountByCurrencyCode(currencyCode);
        }

        public static string GetCurrencyCodeByID(int currencyId)
        {
            return clsPaymentRepo.GetCurrencyCodeByID(currencyId);
        }

        public static int GetCurrencyIDByCode(string currencyCode)
        {
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

        public static int GetCategoryIDByName(string categoryName)
        {
            return clsPaymentRepo.GetCategoryIDByName(categoryName);
        }

        public static DataTable GetTotalsByCategoryID(int categoryId)
        {
            return clsPaymentRepo.GetTotalsByCategoryID(categoryId);
        }

        public static DataTable GetTransactionsByCategory(string[] CategoryCollection,bool University = true)
        {
            return clsPaymentRepo.GetTransactionsByCategory(CategoryCollection,University);
        }

        public static DataTable GetTotalsByCategoriesID(string[] CategoryCollection,bool University = true)
        {
            return clsPaymentRepo.GetTotalsByCategoriesID(CategoryCollection,University);
        }

    }
}
