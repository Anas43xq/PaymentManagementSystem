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
    public partial class MainForm : Form
    {
        private CategoryManagementForm categoryManagementForm;
        private CurrencyManagementForm currencyManagementForm;
        private UniversityPaymentsForm universityPaymentsForm;
        private OtherPaymentsForm otherPaymentsForm;
        private ReportForm reportsForm;
        private bool formsInitialized = false;

        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void InitializeForms()
        {
            universityPaymentsForm = new UniversityPaymentsForm();
            universityPaymentsForm.TopLevel = false;
            universityPaymentsForm.FormBorderStyle = FormBorderStyle.None;
            universityPaymentsForm.Dock = DockStyle.Fill;
            tabPageUniversityPayments.Controls.Add(universityPaymentsForm);

            tabControlMain.SelectedIndexChanged += TabControlMain_SelectedIndexChanged;

            tabControlMain.SelectedTab = tabPageUniversityPayments;

            formsInitialized = true;
        }

        private void TabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabPageOtherPayments && otherPaymentsForm == null)
            {
                otherPaymentsForm = new OtherPaymentsForm();
                otherPaymentsForm.TopLevel = false;
                otherPaymentsForm.FormBorderStyle = FormBorderStyle.None;
                otherPaymentsForm.Dock = DockStyle.Fill;
                tabPageOtherPayments.Controls.Add(otherPaymentsForm);
                otherPaymentsForm.Show();
            }
            else if (tabControlMain.SelectedTab == tabPageReports && reportsForm == null)
            {
                reportsForm = new ReportForm();
                reportsForm.TopLevel = false;
                reportsForm.FormBorderStyle = FormBorderStyle.None;
                reportsForm.Dock = DockStyle.Fill;
                tabPageReports.Controls.Add(reportsForm);
                reportsForm.Show();
            }
        }

        private void btnCategoryManagement_Click(object sender, EventArgs e)
        {
            if (currencyManagementForm != null && !currencyManagementForm.IsDisposed)
            {
                currencyManagementForm.Close();
                currencyManagementForm.Dispose();
            }

            if (categoryManagementForm == null || categoryManagementForm.IsDisposed)
            {
                foreach (Control ctrl in tabPageSettings.Controls)
                {
                    if (ctrl != panelSettingsMenu)
                    {
                        ctrl.Dispose();
                    }
                }
                tabPageSettings.Controls.Clear();
                tabPageSettings.Controls.Add(panelSettingsMenu);

                categoryManagementForm = new CategoryManagementForm();
                categoryManagementForm.MainFormReference = this;
                categoryManagementForm.TopLevel = false;
                categoryManagementForm.FormBorderStyle = FormBorderStyle.None;
                categoryManagementForm.Dock = DockStyle.Fill;
                tabPageSettings.Controls.Add(categoryManagementForm);
                categoryManagementForm.BringToFront();
                categoryManagementForm.Show();
            }
            else
            {
                categoryManagementForm.BringToFront();
            }
        }

        private void btnCurrencyManagement_Click(object sender, EventArgs e)
        {
            if (categoryManagementForm != null && !categoryManagementForm.IsDisposed)
            {
                categoryManagementForm.Close();
                categoryManagementForm.Dispose();
            }

            if (currencyManagementForm == null || currencyManagementForm.IsDisposed)
            {
                foreach (Control ctrl in tabPageSettings.Controls)
                {
                    if (ctrl != panelSettingsMenu)
                    {
                        ctrl.Dispose();
                    }
                }
                tabPageSettings.Controls.Clear();
                tabPageSettings.Controls.Add(panelSettingsMenu);

                currencyManagementForm = new CurrencyManagementForm();
                currencyManagementForm.MainFormReference = this;
                currencyManagementForm.TopLevel = false;
                currencyManagementForm.FormBorderStyle = FormBorderStyle.None;
                currencyManagementForm.Dock = DockStyle.Fill;
                tabPageSettings.Controls.Add(currencyManagementForm);
                currencyManagementForm.BringToFront();
                currencyManagementForm.Show();
            }
            else
            {
                currencyManagementForm.BringToFront();
            }
        }

        private void ShowChildForms()
        {
            if (universityPaymentsForm != null && universityPaymentsForm.Width > 0)
            {
                universityPaymentsForm.Show();
            }

            if (otherPaymentsForm != null && otherPaymentsForm.Width > 0)
            {
                otherPaymentsForm.Show();
            }

            if (reportsForm != null && reportsForm.Width > 0)
            {
                reportsForm.Show();
            }

            if (categoryManagementForm != null && categoryManagementForm.Width > 0)
            {
                categoryManagementForm.Show();
            }

            if (currencyManagementForm != null && currencyManagementForm.Width > 0)
            {
                currencyManagementForm.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Payment Management System";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(800, 600);

            InitializeForms();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);


            if (formsInitialized)
            {
 
                this.BeginInvoke(new Action(() =>
                {
                    ShowChildForms();
                }));
            }
        }

        public void RefreshAllForms()
        {
            try
            {
                universityPaymentsForm?.Refresh();
                otherPaymentsForm?.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing forms: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void RefreshSettingsTheme()
        {
            // Apply theme to active management forms
            if (categoryManagementForm != null && !categoryManagementForm.IsDisposed)
            {
                ThemeManager.ApplyTheme(categoryManagementForm);
            }
            if (currencyManagementForm != null && !currencyManagementForm.IsDisposed)
            {
                ThemeManager.ApplyTheme(currencyManagementForm);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            universityPaymentsForm?.Close();
            otherPaymentsForm?.Close();
            reportsForm?.Close();

            base.OnFormClosing(e);
        }
    }
}