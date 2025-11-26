using PaymentBusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PaymentManagement
{
    public enum PageContext
    {
        University,
        Other
    }

    internal static class DataGridViewExtensions
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi?.SetValue(dgv, setting, null);
        }
    }

    internal static class UIHelper
    {
        internal static DataTable LoadData(PageContext context, string specificCategory = "")
        {
            string[] categories = clsPaymentServices.GetUniversityCategories();
            DataTable dt;

            if (!string.IsNullOrEmpty(specificCategory) && !specificCategory.Equals("All Purposes", StringComparison.OrdinalIgnoreCase))
            {
                categories = new string[] { specificCategory };

                dt = clsPaymentServices.GetTransactionsByCategory(categories);
            }

            else if (context == PageContext.University)
            {
                dt = clsPaymentServices.GetTransactionsByCategory(categories);
            }
            else if (context == PageContext.Other)
            {
                dt = clsPaymentServices.GetTransactionsByCategory(categories, false);
            }
            else
            {
                dt = new DataTable();
            }


            if (dt.Columns.Contains("FormattedAmount"))
                dt.Columns["FormattedAmount"].ReadOnly = false;

            foreach (DataRow row in dt.Rows)
            {
                string amountStr = row["FormattedAmount"].ToString();
                if (amountStr.Length > 3)
                {
                    string numberStr = amountStr.Substring(3).Trim().Replace(",", "");
                    if (decimal.TryParse(numberStr, out decimal amount))
                    {
                        string currency = row["CurrencyCode"].ToString();
                        if (currency == PaymentConstants.Currencies.LBP)
                            row["FormattedAmount"] = PaymentConstants.Currencies.LBP + " " + amount.ToString("N2");
                    }
                }
            }

            return dt;
        }

        internal static void UpdateTotals(PageContext context, Label LBP, Label AED, Label USD, string specificCategory = "")
        {
            string[] categories = clsPaymentServices.GetUniversityCategories();
            DataTable dt;

            if (!string.IsNullOrEmpty(specificCategory) && !specificCategory.Equals("All Purposes", StringComparison.OrdinalIgnoreCase))
            {
                categories = new string[] { specificCategory };
            }

            if (!string.IsNullOrEmpty(specificCategory) && !specificCategory.Equals("All Purposes", StringComparison.OrdinalIgnoreCase))
            {
                categories = new string[] { specificCategory };

              dt = clsPaymentServices.GetTotalsByCategories(categories);
            }

            else if (context == PageContext.University)
            {
                dt = clsPaymentServices.GetTotalsByCategories(categories);
            }
            else if (context == PageContext.Other)
            {
                dt = clsPaymentServices.GetTotalsByCategories(categories, false);
            }
            else
            {
                dt = new DataTable();
            }

            decimal totalUSD = 0;
            decimal totalLBP = 0;
            decimal totalAED = 0;

            foreach (DataRow row in dt.Rows)
            {
                string currency = row["Code"].ToString();
                if (currency == PaymentConstants.Currencies.USD)
                    totalUSD += Convert.ToDecimal(row["TotalAmount"]);
                else if (currency == PaymentConstants.Currencies.LBP)
                    totalLBP += Convert.ToDecimal(row["TotalAmount"]);
                else if (currency == "AED")
                    totalAED += Convert.ToDecimal(row["TotalAmount"]);
            }

            LBP.Text = $"Total LBP: {totalLBP:N0} LBP";
            AED.Text = $"Total AED: {totalAED:N2}";
            USD.Text = $"Total USD: ${totalUSD:N2}";
        }

        internal static void SetupDGV(DataGridView dgv)
        {
            dgv.SuspendLayout();
            try
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.MultiSelect = true;
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.RowHeadersVisible = false;
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                
                dgv.DoubleBuffered(true);
                dgv.EnableHeadersVisualStyles = false;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            }
            finally
            {
                dgv.ResumeLayout();
            }
        }

        internal static void btnAdd_Click(PageContext context, DataGridView dgv, Label LBP, Label AED, Label USD, string specificCategory = "")
        {
            using (frmPaymentForm form = new frmPaymentForm(frmPaymentForm.enFormMode.Add, -1))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    dgv.DataSource = LoadData(context, specificCategory);
                    UpdateTotals(context, LBP, AED, USD, specificCategory);
                }
            }
        }

        internal static void btnEdit_Click(PageContext context, DataGridView dgv, Label LBP, Label AED, Label USD, string specificCategory = "")
        {
            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select exactly one transaction to edit.", PaymentConstants.DialogTitles.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int transactionID = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
            using (frmPaymentForm form = new frmPaymentForm(frmPaymentForm.enFormMode.Edit, transactionID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    dgv.DataSource = LoadData(context, specificCategory);
                    UpdateTotals(context, LBP, AED, USD, specificCategory);
                }
            }
        }

        internal static void btnDelete_Click(PageContext context, DataGridView dgv, Label LBP, Label AED, Label USD, string specificCategory = "")
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show(PaymentConstants.Messages.SelectRowToDelete, PaymentConstants.DialogTitles.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(PaymentConstants.Messages.ConfirmDelete, PaymentConstants.DialogTitles.ConfirmDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            int transactionID = Convert.ToInt32(dgv.SelectedRows[0].Cells["ID"].Value);
            if (clsPaymentServices.DeletePayment(transactionID))
            {
                dgv.DataSource = LoadData(context, specificCategory);
                UpdateTotals(context, LBP, AED, USD, specificCategory);
                MessageBox.Show(PaymentConstants.Messages.DeleteSuccess, PaymentConstants.DialogTitles.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(PaymentConstants.Messages.DeleteFailed, PaymentConstants.DialogTitles.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         internal static void DatePickerFilter(DateTimePicker DTP, DataGridView dgv)
        {
            if (DTP.Value == null) return;

            DateTime selectedDate = DTP.Value.Date;
            string dateString = selectedDate.ToString("yyyy-MM-dd");

            try
            {
                DataTable dt = clsPaymentServices.FilterTransactionByDate(dateString);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgv.DataSource = dt;
                    if (dgv.Columns.Contains("Notes"))
                        dgv.Columns["Notes"].Visible = false;
                    if (dgv.Columns.Contains("Amount"))
                        dgv.Columns["Amount"].Visible = false;
                }
                else
                {
                    MessageBox.Show("No transactions found for the selected date.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering by date: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Combines multiple selected transactions and opens payment form with total in USD
        /// </summary>
        internal static void CombineTransactions(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count < 2)
            {
                MessageBox.Show("Please select at least 2 transactions to combine.", PaymentConstants.DialogTitles.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalUSD = 0;
            int transactionCount = dgv.SelectedRows.Count;
            string notes = $"Combined {transactionCount} transactions: ";
            var transactionIDs = new System.Collections.Generic.List<int>();

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                try
                {
                    decimal amount = Convert.ToDecimal(row.Cells["Amount"].Value);
                    string currency = row.Cells["CurrencyCode"].Value.ToString();
                    int transactionID = Convert.ToInt32(row.Cells["ID"].Value);
                    transactionIDs.Add(transactionID);

                    decimal amountInUSD = CurrencyConverter.ConvertToUSD(amount, currency);
                    totalUSD += amountInUSD;

                    notes += $"#{transactionID}({amount:N2} {currency}), ";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing transaction: {ex.Message}", PaymentConstants.DialogTitles.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            notes = notes.TrimEnd(',', ' ');

            string message = $"Total of {transactionCount} transactions:\n\n";
            message += $"Combined Total (USD): ${totalUSD:N2}\n\n";
            message += "Do you want to create a new payment with this total?\n";
            message += "\n⚠️ Warning: The original transactions will be deleted after combining.";

            DialogResult result = MessageBox.Show(message, "Combine Transactions", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (frmPaymentForm form = new frmPaymentForm(frmPaymentForm.enFormMode.Add, -1))
                {
                    form.SetCombinedPayment(totalUSD, notes);
                    
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        int deletedCount = 0;
                        foreach (int id in transactionIDs)
                        {
                            if (clsPaymentServices.DeletePayment(id))
                            {
                                deletedCount++;
                            }
                        }
                        
                        MessageBox.Show($"Successfully combined {transactionCount} transactions.\n{deletedCount} original transactions deleted.", 
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Toggles dark/light theme for the application
        /// </summary>
        internal static void ToggleTheme(Form form)
        {
            ThemeManager.ToggleTheme();
            ThemeManager.ApplyTheme(form);
        }

        /// <summary>
        /// Styles total labels with colorful design
        /// </summary>
        internal static void StyleTotalLabels(Label lblTotalLBP, Label lblTotalAED, Label lblTotalUSD)
        {
            lblTotalLBP.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalLBP.ForeColor = Color.FromArgb(142, 68, 173);
            
            lblTotalAED.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalAED.ForeColor = Color.FromArgb(230, 126, 34);
            
            lblTotalUSD.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotalUSD.ForeColor = Color.FromArgb(39, 174, 96);
        }
    }
}
