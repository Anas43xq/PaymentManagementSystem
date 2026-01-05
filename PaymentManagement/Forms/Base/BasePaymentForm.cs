using PaymentBusinessLayer;
using PaymentManagement.Helpers;
using System;
using System.Data;
using System.Windows.Forms;

namespace PaymentManagement
{
    public partial class BasePaymentForm : Form
    {
        protected PageContext pageContext;

        public BasePaymentForm()
        {
            InitializeComponent();
        }

        protected virtual void BasePaymentForm_Load(object sender, EventArgs e)
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in. Please login first.", "Authentication Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            ThemeManager.ApplyTheme(this);
            UIHelper.StyleTotalLabels(lblTotalLBP, lblTotalAED, lblTotalUSD);
            SetupDGV();
            SetupFilters();
            WireUpEventHandlers();
            RefreshData();
        }

        protected virtual void SetupDGV()
        {
            if (dgvPayments != null)
            {
                UIHelper.SetupDGV(dgvPayments);
            }
        }

        protected virtual void SetupFilters()
        {
            if (cmbPurposeFilter == null) return;

            cmbPurposeFilter.Items.Clear();
            cmbPurposeFilter.Items.Add("All Purposes");

            DataTable dt = clsPaymentServices.GetCategories(clsPaymentServices.GetUniversityCategories(), false);

            foreach (DataRow row in dt.Rows)
            {
                cmbPurposeFilter.Items.Add(row["Name"].ToString());
            }

            if (cmbPurposeFilter.Items.Count > 0)
                cmbPurposeFilter.SelectedIndex = 0;
        }

        protected virtual void WireUpEventHandlers()
        {

            if (editToolStripMenuItem != null)
                editToolStripMenuItem.Click += btnEdit_Click;
            if (deleteToolStripMenuItem != null)
                deleteToolStripMenuItem.Click += btnDelete_Click;
            if (combineToolStripMenuItem != null)
                combineToolStripMenuItem.Click += btnCombine_Click;
        }

        protected virtual void LoadData(string specificCategory = "")
        {
            if (dgvPayments == null) return;
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0) return;

            dgvPayments.DataSource = UIHelper.LoadData(pageContext, Session.CurrentUser.ID, specificCategory);

            if (dgvPayments.Columns.Contains("Amount"))
                dgvPayments.Columns["Amount"].Visible = false;

            if(dgvPayments.Columns.Contains("UserID"))
                dgvPayments.Columns["UserID"].Visible = false;

        }

        protected virtual void UpdateTotals(string specificCategory = "")
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0) return;

            UIHelper.UpdateTotals(pageContext, Session.CurrentUser.ID, lblTotalLBP, lblTotalAED, lblTotalUSD, specificCategory);
        }

        protected virtual void RefreshData(string specificCategory = "")
        {
            UpdateTotals(specificCategory);
            LoadData(specificCategory);
        }

        protected virtual void btnAdd_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UIHelper.btnAdd_Click(pageContext, Session.CurrentUser.ID, dgvPayments, lblTotalLBP, lblTotalAED, lblTotalUSD);
        }

        protected virtual void btnEdit_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UIHelper.btnEdit_Click(pageContext, Session.CurrentUser.ID, dgvPayments, lblTotalLBP, lblTotalAED, lblTotalUSD);
        }

        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UIHelper.btnDelete_Click(pageContext, Session.CurrentUser.ID, dgvPayments, lblTotalLBP, lblTotalAED, lblTotalUSD);
        }

        protected virtual void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        protected virtual void btnClearFilter_Click(object sender, EventArgs e)
        {
            if (cmbPurposeFilter != null)
                cmbPurposeFilter.SelectedIndex = 0;

            if (DPPurposeFilter != null)
            {
                DPPurposeFilter.ValueChanged -= DPPurposeFilter_ValueChanged;
                DPPurposeFilter.Value = DateTime.Now;
                DPPurposeFilter.ValueChanged += DPPurposeFilter_ValueChanged;
            }

            RefreshData();
        }

        protected virtual void btnCombine_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UIHelper.CombineTransactions(dgvPayments, Session.CurrentUser.ID);
            RefreshData();
        }

        protected virtual void btnTheme_Click(object sender, EventArgs e)
        {
            UIHelper.ToggleTheme(this);
        }

        protected virtual void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvPayments?.DataSource is DataTable dt)
            {
                string title = pageContext == PageContext.University ? "UniversityTransactions" : "OtherTransactions";
                ExcelHelper.ExportToExcel(dt, title);
            }
            else
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected virtual void btnImport_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int importedCount = ExcelHelper.ImportTransactions(Session.CurrentUser.ID);
            if (importedCount > 0)
            {
                RefreshData();
                MessageBox.Show($"Successfully imported {importedCount} transactions.", "Import Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected virtual void DgvPayments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPayments == null) return;

            bool hasSelection = dgvPayments.SelectedRows.Count > 0;
            if (btnEdit != null) btnEdit.Enabled = hasSelection;
            if (btnDelete != null) btnDelete.Enabled = hasSelection;
        }

        protected virtual void CmbPurposeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPurposeFilter == null) return;

            string category = cmbPurposeFilter.SelectedItem.ToString();
            RefreshData(category);
        }

        protected virtual void DPPurposeFilter_ValueChanged(object sender, EventArgs e)
        {
            if (DPPurposeFilter != null && dgvPayments != null && Session.CurrentUser != null)
            {
                UIHelper.DatePickerFilter(DPPurposeFilter, dgvPayments, Session.CurrentUser.ID);
            }
        }
    }
}