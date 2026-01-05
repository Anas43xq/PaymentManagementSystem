using System;
using System.Windows.Forms;
using PaymentBusinessLayer;
using PaymentManagement.Helpers;

namespace PaymentManagement.Forms.Auth
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            InitializeCustom();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Pre-fill username if user just signed up or session exists
            if (Session.CurrentUser != null && !String.IsNullOrEmpty(Session.CurrentUser.Username))
            {
                txtUsername.Text = Session.CurrentUser.Username;
                txtPassword.Focus(); // Focus on password field for better UX
            }
            else
            {
                txtUsername.Focus(); // Otherwise focus on username field
            }
        }

        private void InitializeCustom()
        {
            // Enable double buffering for smooth rendering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #region Button Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validation
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "Signing in...";
                this.Cursor = Cursors.WaitCursor;

                var user = clsPaymentServices.AuthenticateUser(username, password);

                if (user == null)
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                Session.CurrentUser = user;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Sign In";
                this.Cursor = Cursors.Default;
            }
        }

        private void lnkSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var signup = new frmSignup())
            {
                signup.ShowDialog(this);
            }
        }

        #endregion
    }
}