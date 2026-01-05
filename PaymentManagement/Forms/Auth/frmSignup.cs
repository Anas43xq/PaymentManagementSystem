using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PaymentBusinessLayer;
using PaymentManagement.Helpers;

namespace PaymentManagement.Forms.Auth
{
    public partial class frmSignup : Form
    {
        public frmSignup()
        {
            InitializeComponent();
            InitializeCustom();
        }

        private void InitializeCustom()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #region Validation Methods

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateInputs(out string errorMessage)
        {
            errorMessage = string.Empty;

            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrEmpty(username))
            {
                errorMessage = "Username is required.";
                txtUsername.Focus();
                return false;
            }

            if (username.Length < 3)
            {
                errorMessage = "Username must be at least 3 characters long.";
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(email))
            {
                errorMessage = "Email is required.";
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(email))
            {
                errorMessage = "Please enter a valid email address.";
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                errorMessage = "Password is required.";
                txtPassword.Focus();
                return false;
            }

            if (password.Length < 8)
            {
                errorMessage = "Password must be at least 8 characters long.";
                txtPassword.Focus();
                return false;
            }

            if (password != confirm)
            {
                errorMessage = "Passwords do not match.";
                txtConfirm.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (!ValidateInputs(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Disable button during registration
                btnSignup.Enabled = false;
                btnSignup.Text = "Creating Account...";
                this.Cursor = Cursors.WaitCursor;

                clsPaymentServices.RegisterUser(username, email, password, "User");

                MessageBox.Show("Account created successfully. You may now login.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Store username for auto-fill in login form
                if (Session.CurrentUser == null)
                {
                    Session.CurrentUser = new clsPaymentEntities.ClsUser();
                }

                Session.CurrentUser.Username = username;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSignup.Enabled = true;
                btnSignup.Text = "Create Account";
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}