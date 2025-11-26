using System;

namespace PaymentBusinessLayer
{
    /// <summary>
    /// Contains constant values used throughout the payment management system
    /// </summary>
    public static class PaymentConstants
    {
        #region Category Names

        /// <summary>
        /// University-related payment categories
        /// </summary>
        public static class UniversityCategories
        {
            public const string Registration = "Registration";
            public const string UniversityPayment = "UnivPayment";
            public const string Bus = "BUS";
            public const string Exam = "EXAM";

            /// <summary>
            /// Gets all university category names
            /// </summary>
            public static string[] GetAll() => new[]
            {
                Registration,
                UniversityPayment,
                Bus,
                Exam
            };
        }

        #endregion

        #region Currency Codes

        /// <summary>
        /// Supported currency codes
        /// </summary>
        public static class Currencies
        {
            public const string USD = "USD";
            public const string LBP = "LBP";
            public const string AED = "AED";
        }

        #endregion

        #region Validation Messages

        /// <summary>
        /// Common validation and error messages
        /// </summary>
        public static class Messages
        {
            public const string SelectRowToEdit = "Please select a payment to edit.";
            public const string SelectRowToDelete = "Please select a payment to delete.";
            public const string ConfirmDelete = "Are you sure you want to delete this payment?";
            public const string DeleteSuccess = "Payment deleted successfully.";
            public const string DeleteFailed = "Failed to delete the payment.";
            public const string SaveSuccess = "Payment saved successfully.";
            public const string SaveFailed = "Failed to save the payment.";
            public const string InvalidAmount = "Please enter a valid amount.";
            public const string InvalidDate = "Please enter a valid date.";
            public const string RequiredField = "This field is required.";
        }

        #endregion

        #region Dialog Titles

        /// <summary>
        /// Common dialog box titles
        /// </summary>
        public static class DialogTitles
        {
            public const string Warning = "Warning";
            public const string Error = "Error";
            public const string Success = "Success";
            public const string ConfirmDelete = "Confirm Delete";
            public const string Information = "Information";
        }

        #endregion
    }
}
