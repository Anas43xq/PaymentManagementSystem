using System;

namespace clsPaymentEntities
{
    // Main Transaction/Payment class
    public class ClsPayment
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        // Primary key
        public int ID { get; set; }

        // Basic transaction info
        public decimal Amount { get; set; }  // Fixed: Should be decimal for money
        public DateTime TransactionDate { get; set; }
        public string Notes { get; set; }

        // Foreign keys - Fixed: Should be int, not string
        public int CategoryID { get; set; }
        public int CurrencyID { get; set; }

        // Display properties (loaded from related tables)
        public string CategoryName { get; set; }
        public string CategoryType { get; set; } // Income or Expense
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }

        // Calculated property for display
        public string FormattedAmount
        {
            get { return CurrencySymbol + Amount.ToString("N2"); }
        }

        // Audit fields
        public DateTime CreatedAt { get; set; }

        // Constructor
        public ClsPayment()
        {
            Mode = enMode.AddNew;
            TransactionDate = DateTime.Today;
        }
    }

    // Category class
    public class ClsCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Income" or "Expense"
        public bool IsActive { get; set; } = true;

        // For display in ComboBox
        public override string ToString()
        {
            return Name;
        }
    }

    public class ClsCurrency
    {
        public int ID { get; set; }
        public string Code { get; set; }        // USD, LBP, AED
        public string Name { get; set; }        // US Dollar, Lebanese Pound, UAE Dirham
        public string Symbol { get; set; }      // $, €, د.إ
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // For display in ComboBox
        public string DisplayText
        {
            get { return $"{Code} ({Symbol})"; }
        }

        public override string ToString()
        {
            return DisplayText;
        }
    }

    // Summary class for reports
    public class ClsPaymentSummary
    {
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
        public string CurrencySymbol { get; set; }

        public string FormattedTotal
        {
            get { return CurrencySymbol + TotalAmount.ToString("N2"); }
        }
    }

    // Monthly summary class
    public class ClsMonthlySummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal Balance { get; set; }

        public string MonthName
        {
            get { return new DateTime(Year, Month, 1).ToString("MMMM yyyy"); }
        }

        public string FormattedIncome { get; set; }
        public string FormattedExpenses { get; set; }
        public string FormattedBalance { get; set; }
    }
}