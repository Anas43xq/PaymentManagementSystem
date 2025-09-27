using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaymentManagementSystem
{
    public partial class CoursesControl : UserControl
    {
        public CoursesControl()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Add course logic
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit course logic
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete course logic
        }

        private void CoursesControl_Load(object sender, EventArgs e)
        {
            // Initialize data
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            // Calculate and update progress
            int completedCredits = 22; // Example
            int totalCredits = 120; // Example

            progressBarCredits.Maximum = totalCredits;
            progressBarCredits.Value = completedCredits;

            lblProgress.Text = $"{completedCredits} / {totalCredits} credits completed";

            // Update percentage label
            double percentage = (double)completedCredits / totalCredits * 100;
            lblPercentage.Text = $"{percentage:F1}% Complete";
        }
    }
}