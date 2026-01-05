using PaymentBusinessLayer;
using PaymentManagement.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace PaymentManagement
{
    public partial class ReportForm : Form
    {
        private DataTable _dtreport;
        private bool _dataLoaded = false;
        private DateTime _selectedFromDate;
        private DateTime _selectedToDate;

        public ReportForm()
        {
            InitializeComponent();
            InitializeData();
            InitializeDateRange();
            this.VisibleChanged += ReportForm_VisibleChanged;
            monthCalendar1.SetCustomSize(220, 160);
        }

        #region Initialization

        private void InitializeData()
        {

            lblTotal.Text = "Grand Total: $0.00";

   
            lblTotalUniversity.Text = "Total University: $0.00";
            lblTotalOtherPayments.Text = "Total Other Payments: $0.00";

 
            ResetAllCategoryLabels();
        }

        private void InitializeDateRange()
        {
            // Set default date range to current month
            _selectedFromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _selectedToDate = _selectedFromDate.AddMonths(1).AddDays(-1);

            // Set MonthCalendar selection
            monthCalendar1.SelectionStart = _selectedFromDate;
            monthCalendar1.SelectionEnd = _selectedToDate; 
        }

        private void ResetAllCategoryLabels()
        {

            lblExam.Text = "$0.00";
            lblRegistration.Text = "$0.00";
            lblUnivPayment.Text = "$0.00";
            lblBus.Text = "$0.00";


            lblSpeech.Text = "$0.00";
            lblDoctor.Text = "$0.00";
            lblDental.Text = "$0.00";
            lblBraces.Text = "$0.00";
            lblMobile.Text = "$0.00";
            lblData.Text = "$0.00";
            lblInternet.Text = "$0.00";
            lblOtherPayment.Text = "$0.00";
            lblPatrol.Text = "$0.00";
        }

        #endregion

        #region Event Handlers

        private void ReportForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !_dataLoaded)
            {
                LoadData();
                _dataLoaded = true;
            }
        }

        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            _selectedFromDate = e.Start;
            _selectedToDate = e.End;

            LoadCategoryData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _dataLoaded = false;
            LoadData();
            _dataLoaded = true;
        }

        #endregion

        #region Data Loading

        public void LoadData()
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in.", "Authentication Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dtreport = clsPaymentServices.GetAllCurrencyTotals(Session.CurrentUser.ID);

            if (_dtreport != null && _dtreport.Rows.Count > 0)
            {
                decimal totalUsd = 0;

                foreach (DataRow row in _dtreport.Rows)
                {
                    string currency = row["Code"].ToString();
                    decimal amount = Convert.ToDecimal(row["TotalAmount"]);

                    if (currency.Equals("USD", StringComparison.OrdinalIgnoreCase))
                    {
                        totalUsd = amount;
                    }
                }

                UpdateTotal(totalUsd, 0, 0);
            }

            LoadCategoryData();
        }

        private void LoadCategoryData()
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
                return;


            var categoryData = clsPaymentServices.GetCategoryTotalsByDateRange(
                Session.CurrentUser.ID,
                _selectedFromDate,
                _selectedToDate);

            if (categoryData == null || categoryData.Rows.Count == 0)
            {
                ResetAllLabels();
                return;
            }


            var categoryTotalsUSD = new System.Collections.Generic.Dictionary<string, decimal>();

            foreach (DataRow row in categoryData.Rows)
            {
                string category = row["CategoryName"].ToString();
                string currency = row["CurrencyCode"].ToString();
                decimal amount = Convert.ToDecimal(row["TotalAmount"]);

                decimal amountInUSD = CurrencyConverter.ConvertToUSD(amount, currency);

                if (categoryTotalsUSD.ContainsKey(category))
                {
                    categoryTotalsUSD[category] += amountInUSD;
                }
                else
                {
                    categoryTotalsUSD[category] = amountInUSD;
                }
            }

            // Reset all labels first
            ResetAllLabels();

            // Calculate and update all totals
            CalculateAndDisplayTotals(categoryTotalsUSD);
        }

        private void CalculateAndDisplayTotals(System.Collections.Generic.Dictionary<string, decimal> categoryTotalsUSD)
        {
            decimal totalUniversity = 0;
            decimal totalOtherPayments = 0;
            decimal grandTotal = 0;

            foreach (var kvp in categoryTotalsUSD)
            {
                decimal amount = kvp.Value;
                UpdateCategoryLabel(kvp.Key, amount);

                // Accumulate section totals
                if (IsUniversityCategory(kvp.Key))
                {
                    totalUniversity += amount;
                }
                else
                {
                    totalOtherPayments += amount;
                }

                grandTotal += amount;
            }

            // Update section totals
            lblTotalUniversity.Text = $"Total University: ${totalUniversity:N2}";
            lblTotalOtherPayments.Text = $"Total Other Payments: ${totalOtherPayments:N2}";

            lblTotal.Text = $"Grand Total: ${grandTotal:N2} ({_selectedFromDate:MMM dd} - {_selectedToDate:MMM dd, yyyy})";
        }

        private void ResetAllLabels()
        {

            ResetAllCategoryLabels();


            lblTotalUniversity.Text = "Total University: $0.00";
            lblTotalOtherPayments.Text = "Total Other Payments: $0.00";


            lblTotal.Text = "Grand Total: $0.00";
        }

        #endregion

        #region Category Mapping

        private bool IsUniversityCategory(string category)
        {
            string[] universityCategories = { "EXAM", "Registration", "UnivPayment", "BUS" };
            return System.Array.Exists(universityCategories, cat =>
                cat.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        private void UpdateCategoryLabel(string category, decimal amountUSD)
        {
            // University categories
            if (category.Equals("EXAM", StringComparison.OrdinalIgnoreCase))
            {
                lblExam.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Registration", StringComparison.OrdinalIgnoreCase))
            {
                lblRegistration.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("UnivPayment", StringComparison.OrdinalIgnoreCase))
            {
                lblUnivPayment.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("BUS", StringComparison.OrdinalIgnoreCase))
            {
                lblBus.Text = $"${amountUSD:N2}";
            }
            // Other payment categories
            else if (category.Equals("Speech", StringComparison.OrdinalIgnoreCase) ||
                     category.Equals("Speech Doctor", StringComparison.OrdinalIgnoreCase))
            {
                lblSpeech.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
            {
                lblDoctor.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Dental", StringComparison.OrdinalIgnoreCase))
            {
                lblDental.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Braces", StringComparison.OrdinalIgnoreCase) ||
                     category.Equals("Dental Braces", StringComparison.OrdinalIgnoreCase))
            {
                lblBraces.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Mobile", StringComparison.OrdinalIgnoreCase))
            {
                lblMobile.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Data", StringComparison.OrdinalIgnoreCase) ||
                     category.Equals("Mobile Data", StringComparison.OrdinalIgnoreCase))
            {
                lblData.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Internet", StringComparison.OrdinalIgnoreCase))
            {
                lblInternet.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Other Payment", StringComparison.OrdinalIgnoreCase))
            {
                lblOtherPayment.Text = $"${amountUSD:N2}";
            }
            else if (category.Equals("Patrol", StringComparison.OrdinalIgnoreCase))
            {
                lblPatrol.Text = $"${amountUSD:N2}";
            }
        }

        #endregion

        #region Legacy Methods

        private void UpdateTotal(decimal usd, decimal aed, decimal lbp)
        {
            // Convert all currencies to USD
            decimal usdFromAed = CurrencyConverter.ConvertToUSD(aed, "AED");
            decimal usdFromLbp = CurrencyConverter.ConvertToUSD(lbp, "LBP");
            decimal totalUsd = usd + usdFromAed + usdFromLbp;

            lblTotal.Text = $"Total: ${totalUsd:N2}";
        }

        public void UpdateAmounts(decimal usd, decimal aed, decimal lbp)
        {
            UpdateTotal(usd, aed, lbp);
        }

        #endregion
    }
}