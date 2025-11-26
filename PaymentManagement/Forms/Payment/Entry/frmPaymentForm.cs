using clsPaymentEntities;
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
using System.Windows.Forms.VisualStyles;
using static PaymentManagement.frmPaymentForm;

namespace PaymentManagement
{
    public partial class frmPaymentForm : Form
    {
        public enum enFormMode
        {
            Add,
            Edit
        }

        private ClsPayment payment = new ClsPayment();
        private enFormMode _formMode;
        private int _PaymentID;

        public frmPaymentForm(enFormMode formMode,int PaymentID)
        {
            InitializeComponent();
            _formMode = formMode;
            _PaymentID = PaymentID;
        }

        private decimal _combinedAmount = 0;
        private string _combinedNotes = "";
        private bool _isCombinedPayment = false;

        /// <summary>
        /// Sets the form to display combined payment data
        /// </summary>
        public void SetCombinedPayment(decimal totalAmountUSD, string notes)
        {
            _isCombinedPayment = true;
            _combinedAmount = totalAmountUSD;
            _combinedNotes = notes;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            SetupForm();
            SetupComboBoxes();
            SetupDatePickers();
            
            txtAmount.Focus();

            if (_PaymentID != 1 && _formMode == enFormMode.Edit)
            {
                payment = clsPaymentServices.Find(_PaymentID);
                if (payment != null )
                {
                    FillFeilds();
                }
            }

            // Apply combined payment values if this is a combined transaction
            if (_isCombinedPayment)
            {
                txtAmount.Text = _combinedAmount.ToString();
                cmbCurrency.Text = PaymentConstants.Currencies.USD;
                txtNotes.Text = _combinedNotes;
                this.Text = "Add Combined Payment";
            }

        }

        private void SetupForm()
        {
            string action = _formMode == enFormMode.Add ? "Add" : "Edit";
            this.Text = $"{action} Form";
        }

        private void FillFeilds()
        {
            if(_formMode == enFormMode.Edit)
            {
                txtAmount.Text = payment.Amount.ToString();
                dtpPaymentDate.Value = payment.TransactionDate;
                txtNotes.Text = payment.Notes;
                cmbCurrency.SelectedItem = payment.CurrencyCode;
                cmbCategory.Text = payment.CategoryName;
            }
        }

        private void SetupComboBoxes()
        {
            // Setup Currency ComboBox
            cmbCurrency.Items.Clear();

            DataTable dt = clsPaymentServices.GetAllCurrencies();

            foreach (DataRow row in dt.Rows)
            {
                cmbCurrency.Items.Add(row["Code"].ToString());
            }



            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add("Cash");
            cmbPaymentMethod.Items.Add("Bank Transfer");
            cmbPaymentMethod.Items.Add("Credit Card");
            cmbPaymentMethod.Items.Add("Check");

            cmbCategory.Items.Clear();

            dt = clsPaymentServices.GetAllCategories();

            foreach (DataRow row in dt.Rows)
            {
                cmbCategory.Items.Add(row["Name"].ToString());
            }
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Done");
            cmbStatus.Items.Add("Cancelled");
            cmbStatus.Items.Add("In Progress");
            cmbStatus.Items.Add("On Hold");

            cmbCurrency.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            cmbStatus.SelectedItem = "Done";
            cmbPaymentMethod.SelectedItem = "Cash";
        }

        private void SetupDatePickers()
        {
            // Set default dates
            dtpPaymentDate.Value = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                payment.Amount = Convert.ToDecimal(txtAmount.Text);
                payment.TransactionDate = dtpPaymentDate.Value;
                payment.Notes = txtNotes.Text;
                payment.CurrencyID = clsPaymentServices.GetCurrencyIDByCode(cmbCurrency.SelectedItem.ToString());
                payment.CategoryID = clsPaymentServices.GetCategoryIDByName(cmbCategory.SelectedItem.ToString());

                if (clsPaymentServices.Save(ref payment))
                {
                    MessageBox.Show($"Payment {(_formMode == enFormMode.Add ? "added" : "updated")} successfully!",
                                   "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateForm()
        {

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please enter an amount.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            decimal amount;
            if (!decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (dtpPaymentDate.Value > DateTime.Now.AddYears(1))
            {
                MessageBox.Show("Payment date cannot be more than 1 year in the future.", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpPaymentDate.Focus();
                return false;
            }
            return true;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = char.ToUpper(e.KeyChar);
            var currencyMap = new Dictionary<char, string>
            {
                {'U',"USD"},
                {'A',"AED"},
                {'L',"LBP"}
            };

            if (currencyMap.TryGetValue(key, out string currency))
            {
                cmbCurrency.SelectedItem = currency;
                e.Handled = true;
            }
            
            // Allow: digits, control keys (backspace, delete), and decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text == string.Empty) return;

            int selectionStart = txtAmount.SelectionStart;
            int selectionLength = txtAmount.SelectionLength;

            string cleanText = txtAmount.Text.Replace(",", "");
            if (decimal.TryParse(cleanText, out decimal value))
            {
                txtAmount.TextChanged -= txtAmount_TextChanged; // prevent recursion
                txtAmount.Text = string.Format("{0:N2}", value); // "N2" = comma-separated with 2 decimal places
                txtAmount.SelectionStart = Math.Min(selectionStart + 1, txtAmount.Text.Length);
                txtAmount.SelectionLength = selectionLength;
                txtAmount.TextChanged += txtAmount_TextChanged;
            }
        }


        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            string CurrencyCode = cmbCurrency.SelectedItem.ToString();

            lblAmount.Text = "Amount (";

            lblAmount.Text += CurrencyCode == "USD" ? "$):" : CurrencyCode == "LBP" ? "LBP):" : "AED)";
        }
    }
}
