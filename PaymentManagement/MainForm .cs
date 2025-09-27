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
        private UniversityPaymentsForm universityPaymentsForm;
        private OtherPaymentsForm otherPaymentsForm;
        private CoursesForm coursesForm;

        public MainForm()
        {
            InitializeComponent();
            InitializeForms();
        }

        private void InitializeForms()
        {
            // Initialize child forms
            universityPaymentsForm = new UniversityPaymentsForm();
            otherPaymentsForm = new OtherPaymentsForm();
            coursesForm = new CoursesForm();

            // Set forms to fill the tab pages
            universityPaymentsForm.TopLevel = false;
            universityPaymentsForm.FormBorderStyle = FormBorderStyle.None;
            universityPaymentsForm.Dock = DockStyle.Fill;

            otherPaymentsForm.TopLevel = false;
            otherPaymentsForm.FormBorderStyle = FormBorderStyle.None;
            otherPaymentsForm.Dock = DockStyle.Fill;

            coursesForm.TopLevel = false;
            coursesForm.FormBorderStyle = FormBorderStyle.None;
            coursesForm.Dock = DockStyle.Fill;

            // Add forms to tab pages
            tabPageUniversityPayments.Controls.Add(universityPaymentsForm);
            tabPageOtherPayments.Controls.Add(otherPaymentsForm);
            tabPageCourses.Controls.Add(coursesForm);

            // Show the forms
            universityPaymentsForm.Show();
            otherPaymentsForm.Show();
            coursesForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Payment Management System";
            //this.WindowState = FormWindowState.Maximized;
        }
    }
}