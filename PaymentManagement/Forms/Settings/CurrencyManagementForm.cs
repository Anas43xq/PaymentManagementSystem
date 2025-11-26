using System;
using System.Windows.Forms;
using PaymentBusinessLayer;

namespace PaymentManagement
{
    public partial class CurrencyManagementForm : Form
    {
        public Form MainFormReference { get; set; }

        public CurrencyManagementForm()
        {
            InitializeComponent();
        }

        private void CurrencyManagementForm_Load(object sender, EventArgs e)
        {
            LoadCurrencies();
            ThemeManager.ApplyTheme(this);
        }

        private void LoadCurrencies()
        {
            dgvCurrencies.DataSource = clsPaymentServices.GetAllCurrencies();
            
            if (dgvCurrencies.Columns.Count > 0)
            {
                dgvCurrencies.Columns["ID"].Visible = false;
                dgvCurrencies.Columns["Code"].HeaderText = "Code";
                dgvCurrencies.Columns["Name"].HeaderText = "Currency Name";
                dgvCurrencies.Columns["Symbol"].HeaderText = "Symbol";
                dgvCurrencies.Columns["Symbol"].Width = 80;
                dgvCurrencies.Columns["IsActive"].Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Console.WriteLine("=== CurrencyManagement - Add Button Clicked ===");
            
            try
            {
                CurrencyEditForm form = new CurrencyEditForm();
                Console.WriteLine("CurrencyEditForm created successfully");
                
                form.StartPosition = FormStartPosition.CenterParent;
                Console.WriteLine("Showing form as dialog...");
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCurrencies();
                    Console.WriteLine("Currency saved, grid refreshed");
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
            LoadCurrencies();
        }

        private void dgvCurrencies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    string currencyCode = dgvCurrencies.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                    CurrencyEditForm frm = new CurrencyEditForm(currencyCode);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadCurrencies();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCurrencies.CurrentRow != null)
            {
                try
                {
                    string currencyCode = dgvCurrencies.CurrentRow.Cells["Code"].Value.ToString();
                    CurrencyEditForm frm = new CurrencyEditForm(currencyCode);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadCurrencies();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCurrencies.CurrentRow != null)
            {
                string currencyCode = dgvCurrencies.CurrentRow.Cells["Code"].Value.ToString();
                string currencyName = dgvCurrencies.CurrentRow.Cells["Name"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to delete '{currencyName}'?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (clsPaymentServices.DeleteCurrency(currencyCode))
                    {
                        MessageBox.Show("Currency deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCurrencies();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete currency.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
