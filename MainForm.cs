using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PaymentEntities;

namespace PaymentManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize dashboard
        }

        private void test()
        {
            clsPayment dal = new clsPayment();

            // Insert payment
            dal.AddPayment(DateTime.Now, DateTime.Now.AddDays(10), "September Rent");

            // Load payments into DataGridView
            DataTable dt = dal.GetAllPayments();
            dataGridView1.DataSource = dt;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle tab change events
        }
    }
}