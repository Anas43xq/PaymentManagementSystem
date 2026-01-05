namespace PaymentManagement
{
    partial class frmPaymentForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelNotes = new System.Windows.Forms.Panel();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.panelCategory = new System.Windows.Forms.Panel();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.panelPaymentDate = new System.Windows.Forms.Panel();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.lblPaymentDate = new System.Windows.Forms.Label();
            this.panelCurrency = new System.Windows.Forms.Panel();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.panelAmount = new System.Windows.Forms.Panel();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelNotes.SuspendLayout();
            this.panelCategory.SuspendLayout();
            this.panelPaymentDate.SuspendLayout();
            this.panelCurrency.SuspendLayout();
            this.panelAmount.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panelHeader.Controls.Add(this.lblFormTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.panelHeader.Size = new System.Drawing.Size(700, 80);
            this.panelHeader.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(30, 20);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(212, 41);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "New Payment";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelButtons);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 80);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(30, 30, 30, 20);
            this.panelMain.Size = new System.Drawing.Size(700, 570);
            this.panelMain.TabIndex = 1;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Controls.Add(this.panelNotes);
            this.panelContent.Controls.Add(this.panelCategory);
            this.panelContent.Controls.Add(this.panelPaymentDate);
            this.panelContent.Controls.Add(this.panelCurrency);
            this.panelContent.Controls.Add(this.panelAmount);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(30, 30);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(40, 35, 40, 35);
            this.panelContent.Size = new System.Drawing.Size(640, 450);
            this.panelContent.TabIndex = 0;
            // 
            // panelNotes
            // 
            this.panelNotes.Controls.Add(this.txtNotes);
            this.panelNotes.Controls.Add(this.lblNotes);
            this.panelNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNotes.Location = new System.Drawing.Point(40, 275);
            this.panelNotes.Name = "panelNotes";
            this.panelNotes.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelNotes.Size = new System.Drawing.Size(560, 150);
            this.panelNotes.TabIndex = 4;
            // 
            // txtNotes
            // 
            this.txtNotes.AcceptsReturn = true;
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Location = new System.Drawing.Point(0, 31);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(560, 99);
            this.txtNotes.TabIndex = 1;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblNotes.Location = new System.Drawing.Point(0, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblNotes.Size = new System.Drawing.Size(56, 31);
            this.lblNotes.TabIndex = 0;
            this.lblNotes.Text = "Notes";
            // 
            // panelCategory
            // 
            this.panelCategory.Controls.Add(this.cmbCategory);
            this.panelCategory.Controls.Add(this.lblCategory);
            this.panelCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCategory.Location = new System.Drawing.Point(40, 215);
            this.panelCategory.Name = "panelCategory";
            this.panelCategory.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelCategory.Size = new System.Drawing.Size(560, 60);
            this.panelCategory.TabIndex = 3;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(0, 31);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(560, 31);
            this.cmbCategory.TabIndex = 1;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCategory.Location = new System.Drawing.Point(0, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblCategory.Size = new System.Drawing.Size(84, 31);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Category";
            // 
            // panelPaymentDate
            // 
            this.panelPaymentDate.Controls.Add(this.dtpPaymentDate);
            this.panelPaymentDate.Controls.Add(this.lblPaymentDate);
            this.panelPaymentDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPaymentDate.Location = new System.Drawing.Point(40, 155);
            this.panelPaymentDate.Name = "panelPaymentDate";
            this.panelPaymentDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelPaymentDate.Size = new System.Drawing.Size(560, 60);
            this.panelPaymentDate.TabIndex = 2;
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpPaymentDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpPaymentDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPaymentDate.Location = new System.Drawing.Point(0, 31);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(560, 30);
            this.dtpPaymentDate.TabIndex = 1;
            // 
            // lblPaymentDate
            // 
            this.lblPaymentDate.AutoSize = true;
            this.lblPaymentDate.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPaymentDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPaymentDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblPaymentDate.Location = new System.Drawing.Point(0, 0);
            this.lblPaymentDate.Name = "lblPaymentDate";
            this.lblPaymentDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblPaymentDate.Size = new System.Drawing.Size(123, 31);
            this.lblPaymentDate.TabIndex = 0;
            this.lblPaymentDate.Text = "Payment Date";
            // 
            // panelCurrency
            // 
            this.panelCurrency.Controls.Add(this.cmbCurrency);
            this.panelCurrency.Controls.Add(this.lblCurrency);
            this.panelCurrency.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCurrency.Location = new System.Drawing.Point(40, 95);
            this.panelCurrency.Name = "panelCurrency";
            this.panelCurrency.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelCurrency.Size = new System.Drawing.Size(560, 60);
            this.panelCurrency.TabIndex = 1;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCurrency.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Location = new System.Drawing.Point(0, 31);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(560, 31);
            this.cmbCurrency.TabIndex = 1;
            this.cmbCurrency.SelectedIndexChanged += new System.EventHandler(this.cmbCurrency_SelectedIndexChanged);
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCurrency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurrency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCurrency.Location = new System.Drawing.Point(0, 0);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblCurrency.Size = new System.Drawing.Size(81, 31);
            this.lblCurrency.TabIndex = 0;
            this.lblCurrency.Text = "Currency";
            // 
            // panelAmount
            // 
            this.panelAmount.Controls.Add(this.txtAmount);
            this.panelAmount.Controls.Add(this.lblAmount);
            this.panelAmount.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAmount.Location = new System.Drawing.Point(40, 35);
            this.panelAmount.Name = "panelAmount";
            this.panelAmount.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelAmount.Size = new System.Drawing.Size(560, 60);
            this.panelAmount.TabIndex = 0;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAmount.Location = new System.Drawing.Point(0, 31);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(560, 30);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblAmount.Location = new System.Drawing.Point(0, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblAmount.Size = new System.Drawing.Size(75, 31);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "Amount";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(30, 480);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(640, 70);
            this.panelButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnCancel.Location = new System.Drawing.Point(380, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 45);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(520, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPaymentForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(700, 650);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaymentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payment Form";
            this.Load += new System.EventHandler(this.PaymentForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelNotes.ResumeLayout(false);
            this.panelNotes.PerformLayout();
            this.panelCategory.ResumeLayout(false);
            this.panelCategory.PerformLayout();
            this.panelPaymentDate.ResumeLayout(false);
            this.panelPaymentDate.PerformLayout();
            this.panelCurrency.ResumeLayout(false);
            this.panelCurrency.PerformLayout();
            this.panelAmount.ResumeLayout(false);
            this.panelAmount.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Panel panelCurrency;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Panel panelPaymentDate;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.Label lblPaymentDate;
        private System.Windows.Forms.Panel panelCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Panel panelNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}