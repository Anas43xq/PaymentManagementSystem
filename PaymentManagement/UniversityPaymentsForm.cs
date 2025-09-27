using PaymentBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaymentManagement
{
    public partial class UniversityPaymentsForm : Form
    {
        public UniversityPaymentsForm()
        {
            InitializeComponent();
        }

        private void UniversityPaymentsForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadData();
            UpdateTotals();

            dgvUniversityPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView appearance
            dgvUniversityPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUniversityPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUniversityPayments.MultiSelect = false;
            dgvUniversityPayments.ReadOnly = true;
            dgvUniversityPayments.AllowUserToAddRows = false;
            dgvUniversityPayments.AllowUserToDeleteRows = false;
            dgvUniversityPayments.RowHeadersVisible = false;

            // Set alternating row colors
            dgvUniversityPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void LoadData()
        {
            string[] UniversityCategory = clsPaymentServices.GetUniversityCategories();

            DataTable dt = clsPaymentServices.GetTransactionsByCategory(UniversityCategory);

            dt.Columns["FormattedAmount"].ReadOnly = false;

            foreach (DataRow row in dt.Rows)
            {
                string amountStr = row["FormattedAmount"].ToString();

                string numberStr = amountStr.Substring(3).Trim();

                numberStr = numberStr.Replace(",", "");

                if (decimal.TryParse(numberStr, out decimal amount))
                {
                    string currency = row["CurrencyCode"].ToString();

                    if (currency == "LBP")
                        row["FormattedAmount"] = "LBP " + amount.ToString("N2");
                }
            }


            dgvUniversityPayments.DataSource = dt;
        }

        private void UpdateTotals()
        {
            string[] UniversityCategory = clsPaymentServices.GetUniversityCategories();

            DataTable dt = clsPaymentServices.GetTotalsByCategoriesID(UniversityCategory);

            decimal totalUSD = 0;
            decimal totalLBP = 0;

            foreach (DataRow row in dt.Rows)
            {
                string currency = row["Code"].ToString();

                if (currency == "USD")
                    totalUSD = Convert.ToDecimal(row["TotalAmount"]);
                else if (currency == "LBP")
                    totalLBP = Convert.ToDecimal(row["TotalAmount"]);
            }
            lblTotalUSD.Text = $"Total USD: ${totalUSD:N2}";
            lblTotalLBP.Text = $"Total LBP: {totalLBP:N0} LBP";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPaymentForm form = new frmPaymentForm(frmPaymentForm.enFormMode.Add,-1);

            form.ShowDialog();

            LoadData();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUniversityPayments.SelectedRows.Count > 0)
            {
                int TranscationID = Convert.ToInt32(dgvUniversityPayments.SelectedRows[0].Cells["ID"].Value);

                using (frmPaymentForm form = new frmPaymentForm(frmPaymentForm.enFormMode.Edit, TranscationID))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // Reload only if changes were saved
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
            if (dgvUniversityPayments.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this payment?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int TranscationID = Convert.ToInt32(dgvUniversityPayments.SelectedRows[0].Cells["ID"].Value);
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
  
            }
            else
            {
                MessageBox.Show("Please select a payment to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }-
        }
        private void dataGridViewUniversityPayments_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dgvUniversityPayments.SelectedRows.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            UpdateTotals();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }
    }
}
