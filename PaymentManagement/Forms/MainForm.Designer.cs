namespace PaymentManagement
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabPageOtherPayments = new System.Windows.Forms.TabPage();
            this.tabPageUniversityPayments = new System.Windows.Forms.TabPage();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageReports = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.btnCategoryManagement = new System.Windows.Forms.Button();
            this.btnCurrencyManagement = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelSettingsMenu = new System.Windows.Forms.Panel();
            this.tabControlMain.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.panelSettingsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageOtherPayments
            // 
            this.tabPageOtherPayments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabPageOtherPayments.Location = new System.Drawing.Point(4, 44);
            this.tabPageOtherPayments.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageOtherPayments.Name = "tabPageOtherPayments";
            this.tabPageOtherPayments.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageOtherPayments.Size = new System.Drawing.Size(1592, 814);
            this.tabPageOtherPayments.TabIndex = 1;
            this.tabPageOtherPayments.Text = "💳 Other Payments";
            // 
            // tabPageUniversityPayments
            // 
            this.tabPageUniversityPayments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabPageUniversityPayments.Location = new System.Drawing.Point(4, 44);
            this.tabPageUniversityPayments.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageUniversityPayments.Name = "tabPageUniversityPayments";
            this.tabPageUniversityPayments.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageUniversityPayments.Size = new System.Drawing.Size(1592, 814);
            this.tabPageUniversityPayments.TabIndex = 0;
            this.tabPageUniversityPayments.Text = "🎓 University Payments";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Controls.Add(this.tabPageUniversityPayments);
            this.tabControlMain.Controls.Add(this.tabPageOtherPayments);
            this.tabControlMain.Controls.Add(this.tabPageReports);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMain.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.Padding = new System.Drawing.Point(20, 3);
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1600, 862);
            this.tabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageReports
            // 
            this.tabPageReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabPageReports.Location = new System.Drawing.Point(4, 44);
            this.tabPageReports.Name = "tabPageReports";
            this.tabPageReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReports.Size = new System.Drawing.Size(1592, 814);
            this.tabPageReports.TabIndex = 2;
            this.tabPageReports.Text = "📊 Reports";
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.tabPageSettings.Controls.Add(this.panelSettingsMenu);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 44);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(1592, 814);
            this.tabPageSettings.TabIndex = 3;
            this.tabPageSettings.Text = "⚙️ Settings";
            // 
            // panelSettingsMenu
            // 
            this.panelSettingsMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panelSettingsMenu.Controls.Add(this.btnLogout);
            this.panelSettingsMenu.Controls.Add(this.btnCurrencyManagement);
            this.panelSettingsMenu.Controls.Add(this.btnCategoryManagement);
            this.panelSettingsMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSettingsMenu.Location = new System.Drawing.Point(3, 3);
            this.panelSettingsMenu.Name = "panelSettingsMenu";
            this.panelSettingsMenu.Size = new System.Drawing.Size(250, 808);
            this.panelSettingsMenu.TabIndex = 0;
            // 
            // btnCategoryManagement
            // 
            this.btnCategoryManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCategoryManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCategoryManagement.FlatAppearance.BorderSize = 0;
            this.btnCategoryManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategoryManagement.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnCategoryManagement.ForeColor = System.Drawing.Color.White;
            this.btnCategoryManagement.Location = new System.Drawing.Point(20, 30);
            this.btnCategoryManagement.Name = "btnCategoryManagement";
            this.btnCategoryManagement.Size = new System.Drawing.Size(210, 50);
            this.btnCategoryManagement.TabIndex = 0;
            this.btnCategoryManagement.Text = "📁 Category Management";
            this.btnCategoryManagement.UseVisualStyleBackColor = false;
            this.btnCategoryManagement.Click += new System.EventHandler(this.btnCategoryManagement_Click);
            // 
            // btnCurrencyManagement
            // 
            this.btnCurrencyManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnCurrencyManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCurrencyManagement.FlatAppearance.BorderSize = 0;
            this.btnCurrencyManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCurrencyManagement.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnCurrencyManagement.ForeColor = System.Drawing.Color.White;
            this.btnCurrencyManagement.Location = new System.Drawing.Point(20, 100);
            this.btnCurrencyManagement.Name = "btnCurrencyManagement";
            this.btnCurrencyManagement.Size = new System.Drawing.Size(210, 50);
            this.btnCurrencyManagement.TabIndex = 1;
            this.btnCurrencyManagement.Text = "💱 Currency Management";
            this.btnCurrencyManagement.UseVisualStyleBackColor = false;
            this.btnCurrencyManagement.Click += new System.EventHandler(this.btnCurrencyManagement_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(20, 750);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(210, 50);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 862);
            this.Controls.Add(this.tabControlMain);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1327, 728);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Management System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabPageSettings.ResumeLayout(false);
            this.panelSettingsMenu.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageOtherPayments;
        private System.Windows.Forms.TabPage tabPageUniversityPayments;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageReports;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Panel panelSettingsMenu;
        private System.Windows.Forms.Button btnCategoryManagement;
        private System.Windows.Forms.Button btnCurrencyManagement;
        private System.Windows.Forms.Button btnLogout;
    }
}