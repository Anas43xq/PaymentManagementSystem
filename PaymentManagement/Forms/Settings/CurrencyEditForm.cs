using System;
using System.Windows.Forms;
using PaymentBusinessLayer;
using System.Data;

namespace PaymentManagement
{
    public partial class CurrencyEditForm : Form
    {
        private string _currencyCode;
        private bool _isEditMode;

        public CurrencyEditForm()
        {
            InitializeComponent();
            _isEditMode = false;
        }

        public CurrencyEditForm(string currencyCode)
        {
            InitializeComponent();
            _currencyCode = currencyCode;
            _isEditMode = true;
        }

        private void CurrencyEditForm_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                lblTitle.Text = "Edit Currency";
                LoadCurrencyData();
                txtCode.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Add New Currency";
            }
            ThemeManager.ApplyTheme(this);
        }

        private void LoadCurrencyData()
        {
            DataTable dt = clsPaymentServices.GetCurrencyByCode(_currencyCode);
            if (dt.Rows.Count > 0)
            {
                txtCode.Text = dt.Rows[0]["Code"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtSymbol.Text = dt.Rows[0]["Symbol"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            string code = txtCode.Text.Trim();
            string name = txtName.Text.Trim();
            string symbol = txtSymbol.Text.Trim();

            bool success = false;
            if (_isEditMode)
            {
                success = clsPaymentServices.UpdateCurrency(_currencyCode, name, symbol);
            }
            else
            {
                success = clsPaymentServices.AddCurrency(code, name, symbol);
            }

            if (success)
            {
                MessageBox.Show("Currency saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save currency.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please enter currency code.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter currency name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSymbol.Text))
            {
                MessageBox.Show("Please enter currency symbol.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSymbol.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void panelButtons_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
