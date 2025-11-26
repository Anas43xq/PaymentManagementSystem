using PaymentBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace PaymentManagement
{
    public partial class CategoryEditForm : Form
    {
        private int _categoryId = -1;
        private bool _isEditMode = false;

        public CategoryEditForm()
        {
            InitializeComponent();
            _isEditMode = false;
        }

        public CategoryEditForm(int categoryId)
        {
            InitializeComponent();
            _categoryId = categoryId;
            _isEditMode = true;
        }

        private void CategoryEditForm_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                this.Text = "Edit Category";
                lblTitle.Text = "Edit Category";
                LoadCategoryData();
            }
            else
            {
                this.Text = "Add Category";
                lblTitle.Text = "Add New Category";
            }
            ThemeManager.ApplyTheme(this);
        }

        private void LoadCategoryData()
        {
            DataTable dt = clsPaymentServices.GetCategoryById(_categoryId);
            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            bool success = false;
            if (_isEditMode)
            {
                success = clsPaymentServices.UpdateCategory(_categoryId, txtName.Text.Trim());
            }
            else
            {
                success = clsPaymentServices.AddCategory(txtName.Text.Trim());
            }

            if (success)
            {
                MessageBox.Show("Category saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
