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
    public partial class CoursesForm : Form
    {
        public CoursesForm()
        {
            InitializeComponent();
        }

        private void CoursesForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadData();
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView appearance
            dataGridViewCourses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCourses.MultiSelect = false;
            dataGridViewCourses.ReadOnly = true;
            dataGridViewCourses.AllowUserToAddRows = false;
            dataGridViewCourses.AllowUserToDeleteRows = false;
            dataGridViewCourses.RowHeadersVisible = false;

            // Set alternating row colors
            dataGridViewCourses.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void LoadData()
        {
            // TODO: Load data from database
            // This is where you would implement database connection and data loading for courses
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // TODO: Implement Add functionality
            MessageBox.Show("Add Course functionality to be implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // TODO: Implement Edit functionality
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                MessageBox.Show("Edit Course functionality to be implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a course to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // TODO: Implement Delete functionality
            if (dataGridViewCourses.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Delete Course functionality to be implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a course to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewCourses_SelectionChanged(object sender, EventArgs e)
        {
            // Enable/disable buttons based on selection
            bool hasSelection = dataGridViewCourses.SelectedRows.Count > 0;
            btnEdit.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
        }
    }
}


