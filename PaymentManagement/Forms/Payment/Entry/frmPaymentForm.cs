using clsPaymentEntities;
using PaymentBusinessLayer;
using PaymentManagement.Helpers;
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
        public int UserID { get; set; }

        public frmPaymentForm(enFormMode formMode, int PaymentID)
        {
            InitializeComponent();
            _formMode = formMode;
            _PaymentID = PaymentID;
        }

        private decimal _combinedAmount = 0;
        private string _combinedNotes = "";
        private bool _isCombinedPayment = false;

        public void SetCombinedPayment(decimal totalAmountUSD, string notes)
        {
            _isCombinedPayment = true;
            _combinedAmount = totalAmountUSD;
            _combinedNotes = notes;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
  
            if (Session.CurrentUser == null || Session.CurrentUser.ID <= 0)
            {
                MessageBox.Show("No user is logged in.", "Authentication Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            if (UserID <= 0)
            {
                UserID = Session.CurrentUser.ID;
            }

            ThemeManager.ApplyTheme(this);
            SetupForm();
            SetupComboBoxes();
            SetupDatePickers();

            txtAmount.Focus();

            if (_PaymentID != -1 && _formMode == enFormMode.Edit)
            {
                payment = clsPaymentServices.Find(_PaymentID, UserID);
                if (payment != null)
                {
                    FillFeilds();
                }
                else
                {
                    MessageBox.Show("Payment not found or you don't have permission to edit it.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
            }

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
            string action = _formMode == enFormMode.Add ? "New Payment" : "Edit Payment";
            this.Text = action;
        }

        private void FillFeilds()
        {
            if (_formMode == enFormMode.Edit)
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
            cmbCurrency.Items.Clear();

            DataTable dt = clsPaymentServices.GetAllCurrencies();

            foreach (DataRow row in dt.Rows)
            {
                cmbCurrency.Items.Add(row["Code"].ToString());
            }

            cmbCategory.Items.Clear();

            dt = clsPaymentServices.GetAllCategories();

            foreach (DataRow row in dt.Rows)
            {
                cmbCategory.Items.Add(row["Name"].ToString());
            }

            cmbCurrency.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
        }

        private void SetupDatePickers() => dtpPaymentDate.Value = DateTime.Now;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                payment.UserID = UserID;
                payment.Amount = Convert.ToDecimal(txtAmount.Text);
                payment.TransactionDate = dtpPaymentDate.Value;
                payment.Notes = txtNotes.Text;
                payment.CurrencyID = clsPaymentServices.GetCurrencyIDByCode(cmbCurrency.SelectedItem.ToString());
                payment.CategoryID = clsPaymentServices.GetCategoryIDByName(cmbCategory.SelectedItem.ToString());


                if (_formMode == enFormMode.Add)
                {
                    payment.Mode = ClsPayment.enMode.AddNew;
                }
                else
                {
                    payment.Mode = ClsPayment.enMode.Update;
                }

                if (clsPaymentServices.Save(ref payment))
                {
                    MessageBox.Show($"Payment {(_formMode == enFormMode.Add ? "added" : "updated")} successfully!",
                                   "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"Failed to {(_formMode == enFormMode.Add ? "add" : "update")} payment.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

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
                txtAmount.TextChanged -= txtAmount_TextChanged; 

                string formattedText = string.Format("{0:N2}", value);
                int commasBeforeCursor = txtAmount.Text.Substring(0, selectionStart).Count(c => c == ',');
                int commasAfterFormat = formattedText.Substring(0, Math.Min(selectionStart, formattedText.Length)).Count(c => c == ',');

                txtAmount.Text = formattedText;
                txtAmount.SelectionStart = Math.Min(selectionStart + (commasAfterFormat - commasBeforeCursor), txtAmount.Text.Length);
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