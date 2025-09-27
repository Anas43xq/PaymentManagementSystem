using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaymentManagementSystem
{
    public partial class UniversityPaymentsControl : UserControl
    {
        public UniversityPaymentsControl()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Add payment logic
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit payment logic
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete payment logic
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Filter payments logic
        }

        private void UniversityPaymentsControl_Load(object sender, EventArgs e)
        {
            // Initialize data
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            // Calculate and update totals
            lblTotalUSD.Text = "Total USD: $0.00";
            lblTotalLBP.Text = "Total LBP: 0";
        }
    }
}