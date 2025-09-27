using PaymentBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaymentManagement
{
    public partial class OtherPaymentsForm : Form
    {
        public OtherPaymentsForm()
        {
            InitializeComponent();
        }

        private void OtherPaymentsForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            SetupFilters();
            LoadData();
            UpdateTotals();
        }

        private void SetupDataGridView()
        {
            dgvOtherPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOtherPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOtherPayments.MultiSelect = false;
            dgvOtherPayments.ReadOnly = true;
            dgvOtherPayments.AllowUserToAddRows = false;
            dgvOtherPayments.AllowUserToDeleteRows = false;
            dgvOtherPayments.RowHeadersVisible = false;

            dgvOtherPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void SetupFilters()
        {
            cmbPurposeFilter.Items.Clear();

            DataTable dt = clsPaymentServices.GetCategories(clsPaymentServices.GetUniversityCategories(), false);

            foreach (DataRow row in dt.Rows)
            {
                cmbPurposeFilter.Items.Add(row["Name"].ToString());
            }

            if (cmbPurposeFilter.Items.Count > 0)
                cmbPurposeFilter.SelectedIndex = 0;
        }

        private void LoadData(string SpecficCategory = "")
        {
            string[] OtherCategory = clsPaymentServices.GetUniversityCategories();
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(SpecficCategory))
            {

                OtherCategory = new string[] { SpecficCategory };
                dt = clsPaymentServices.GetTransactionsByCategory(OtherCategory);
            }
            else
            {
                dt = clsPaymentServices.GetTransactionsByCategory(OtherCategory, false);
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
                        if (currency == "LBP")
                            row["FormattedAmount"] = "LBP " + amount.ToString("N2");
                    }
                }
            }

            dgvOtherPayments.DataSource = dt;
        }

        private void UpdateTotals(string SpecficCategory = "")
        {
            string[] OtherCategory = clsPaymentServices.GetUniversityCategories();
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(SpecficCategory))
            {
                OtherCategory = new string[] { SpecficCategory };
                dt = clsPaymentServices.GetTotalsByCategoriesID(OtherCategory);
            }
            else
            {
                dt = clsPaymentServices.GetTotalsByCategoriesID(OtherCategory, false);
            }

            decimal totalUSD = 0;
            decimal totalLBP = 0;

            foreach (DataRow row in dt.Rows)
            {
                string currency = row["Code"].ToString();

                if (currency == "USD")
                    totalUSD += Convert.ToDecimal(row["TotalAmount"]);
                else if (currency == "LBP")
                    totalLBP += Convert.ToDecimal(row["TotalAmount"]);
            }

            lblTotalUSD.Text = $"Total USD: ${totalUSD:N2}";
            lblTotalLBP.Text = $"Total LBP: {totalLBP:N0} LBP";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPaymentForm paymentForm = new frmPaymentForm(frmPaymentForm.enFormMode.Add, -1);

            paymentForm.ShowDialog();

            LoadData();
            UpdateTotals();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvOtherPayments.SelectedRows.Count > 0)
            {
                int TranscationID = Convert.ToInt32(dgvOtherPayments.SelectedRows[0].Cells["ID"].Value);

                using (frmPaymentForm form = new frmPaymentForm(frmPaymentForm.enFormMode.Edit, TranscationID))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        UpdateTotals();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a payment to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvOtherPayments.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this payment?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int TranscationID = Convert.ToInt32(dgvOtherPayments.SelectedRows[0].Cells["ID"].Value);
                    if (clsPaymentServices.DeletePayment(TranscationID))
                    {
                        LoadData();
                        UpdateTotals();
                        MessageBox.Show("Payment deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a payment to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dataGridViewOtherPayments_SelectionChanged(object sender, EventArgs e)
        {
            // Enable/disable buttons based on selection
            bool hasSelection = dgvOtherPayments.SelectedRows.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        private void cmbPurposeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Category = cmbPurposeFilter.SelectedItem.ToString();
            LoadData(Category);
            UpdateTotals(Category);
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            cmbPurposeFilter.SelectedIndex = 0;

            btnRefresh_Click(sender,e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            UpdateTotals();
        }
    }
}
