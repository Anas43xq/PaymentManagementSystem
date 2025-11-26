using PaymentBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace PaymentManagement
{
    public partial class CategoryManagementForm : Form
    {
        private DataTable dtCategories;
        public Form MainFormReference { get; set; }

        public CategoryManagementForm()
        {
            InitializeComponent();
        }

        private void CategoryManagementForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            ThemeManager.ApplyTheme(this);
        }

        private void LoadCategories()
        {
            dtCategories = clsPaymentServices.GetAllCategories();
            dgvCategories.DataSource = dtCategories;

            if (dgvCategories.Columns.Contains("ID"))
                dgvCategories.Columns["ID"].HeaderText = "ID";
            if (dgvCategories.Columns.Contains("Name"))
                dgvCategories.Columns["Name"].HeaderText = "Category Name";
            if (dgvCategories.Columns.Contains("IsUniversityCategory"))
                dgvCategories.Columns["IsUniversityCategory"].HeaderText = "University";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            try
            {
                CategoryEditForm form = new CategoryEditForm();
  
                
                form.StartPosition = FormStartPosition.CenterParent;

                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCategories();
                    Console.WriteLine("Category saved, grid refreshed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error: {ex.Message}\n\nCheck console for details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count != 1) return;

            try
            {
                int categoryId = Convert.ToInt32(dgvCategories.SelectedRows[0].Cells["ID"].Value);
                CategoryEditForm form = new CategoryEditForm(categoryId);
                form.StartPosition = FormStartPosition.CenterParent;
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCategories();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int categoryId = Convert.ToInt32(dgvCategories.SelectedRows[0].Cells["ID"].Value);
                if (clsPaymentServices.DeleteCategory(categoryId))
                {
                    MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategories();
                }
                else
                {
                    MessageBox.Show("Failed to delete category. It may be in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            editToolStripMenuItem_Click(sender, e);
        }
    }
}
