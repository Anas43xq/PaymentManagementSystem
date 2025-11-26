namespace PaymentManagement
{
    partial class ReportForm
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.cardsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panelUsd = new System.Windows.Forms.Panel();
            this.lblUsdAmount = new System.Windows.Forms.Label();
            this.lblUsdTitle = new System.Windows.Forms.Label();
            this.panelAed = new System.Windows.Forms.Panel();
            this.lblAedAmount = new System.Windows.Forms.Label();
            this.lblAedTitle = new System.Windows.Forms.Label();
            this.panelLbp = new System.Windows.Forms.Panel();
            this.lblLbpAmount = new System.Windows.Forms.Label();
            this.lblLbpTitle = new System.Windows.Forms.Label();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.cardsContainer.SuspendLayout();
            this.panelUsd.SuspendLayout();
            this.panelAed.SuspendLayout();
            this.panelLbp.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(40, 30, 40, 20);
            this.headerPanel.Size = new System.Drawing.Size(1085, 120);
            this.headerPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(40, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(383, 62);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Financial Report";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblSubtitle.Location = new System.Drawing.Point(45, 85);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(269, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Overview of currency balances";
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.contentPanel.Controls.Add(this.cardsContainer);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 120);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(40, 40, 40, 30);
            this.contentPanel.Size = new System.Drawing.Size(1085, 480);
            this.contentPanel.TabIndex = 1;
            // 
            // cardsContainer
            // 
            this.cardsContainer.Controls.Add(this.panelUsd);
            this.cardsContainer.Controls.Add(this.panelAed);
            this.cardsContainer.Controls.Add(this.panelLbp);
            this.cardsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardsContainer.Location = new System.Drawing.Point(40, 40);
            this.cardsContainer.Name = "cardsContainer";
            this.cardsContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.cardsContainer.Size = new System.Drawing.Size(1005, 410);
            this.cardsContainer.TabIndex = 0;
            // 
            // panelUsd
            // 
            this.panelUsd.BackColor = System.Drawing.Color.White;
            this.panelUsd.Controls.Add(this.lblUsdAmount);
            this.panelUsd.Controls.Add(this.lblUsdTitle);
            this.panelUsd.Location = new System.Drawing.Point(10, 10);
            this.panelUsd.Margin = new System.Windows.Forms.Padding(10);
            this.panelUsd.Name = "panelUsd";
            this.panelUsd.Size = new System.Drawing.Size(280, 180);
            this.panelUsd.TabIndex = 0;
            this.panelUsd.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Paint);
            // 
            // lblUsdAmount
            // 
            this.lblUsdAmount.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblUsdAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblUsdAmount.Location = new System.Drawing.Point(20, 80);
            this.lblUsdAmount.Name = "lblUsdAmount";
            this.lblUsdAmount.Size = new System.Drawing.Size(240, 80);
            this.lblUsdAmount.TabIndex = 1;
            this.lblUsdAmount.Text = "$0.00";
            this.lblUsdAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUsdTitle
            // 
            this.lblUsdTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblUsdTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblUsdTitle.Location = new System.Drawing.Point(20, 25);
            this.lblUsdTitle.Name = "lblUsdTitle";
            this.lblUsdTitle.Size = new System.Drawing.Size(240, 30);
            this.lblUsdTitle.TabIndex = 0;
            this.lblUsdTitle.Text = "US DOLLAR";
            this.lblUsdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelAed
            // 
            this.panelAed.BackColor = System.Drawing.Color.White;
            this.panelAed.Controls.Add(this.lblAedAmount);
            this.panelAed.Controls.Add(this.lblAedTitle);
            this.panelAed.Location = new System.Drawing.Point(310, 10);
            this.panelAed.Margin = new System.Windows.Forms.Padding(10);
            this.panelAed.Name = "panelAed";
            this.panelAed.Size = new System.Drawing.Size(280, 180);
            this.panelAed.TabIndex = 1;
            this.panelAed.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Paint);
            // 
            // lblAedAmount
            // 
            this.lblAedAmount.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblAedAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblAedAmount.Location = new System.Drawing.Point(20, 80);
            this.lblAedAmount.Name = "lblAedAmount";
            this.lblAedAmount.Size = new System.Drawing.Size(240, 80);
            this.lblAedAmount.TabIndex = 1;
            this.lblAedAmount.Text = "0.00";
            this.lblAedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAedTitle
            // 
            this.lblAedTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblAedTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblAedTitle.Location = new System.Drawing.Point(20, 25);
            this.lblAedTitle.Name = "lblAedTitle";
            this.lblAedTitle.Size = new System.Drawing.Size(240, 30);
            this.lblAedTitle.TabIndex = 0;
            this.lblAedTitle.Text = "UAE DIRHAM";
            this.lblAedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelLbp
            // 
            this.panelLbp.BackColor = System.Drawing.Color.White;
            this.panelLbp.Controls.Add(this.lblLbpAmount);
            this.panelLbp.Controls.Add(this.lblLbpTitle);
            this.panelLbp.Location = new System.Drawing.Point(610, 10);
            this.panelLbp.Margin = new System.Windows.Forms.Padding(10);
            this.panelLbp.Name = "panelLbp";
            this.panelLbp.Size = new System.Drawing.Size(385, 180);
            this.panelLbp.TabIndex = 2;
            this.panelLbp.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Paint);
            // 
            // lblLbpAmount
            // 
            this.lblLbpAmount.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblLbpAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblLbpAmount.Location = new System.Drawing.Point(20, 80);
            this.lblLbpAmount.Name = "lblLbpAmount";
            this.lblLbpAmount.Size = new System.Drawing.Size(240, 80);
            this.lblLbpAmount.TabIndex = 1;
            this.lblLbpAmount.Text = "0";
            this.lblLbpAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLbpTitle
            // 
            this.lblLbpTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblLbpTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblLbpTitle.Location = new System.Drawing.Point(20, 25);
            this.lblLbpTitle.Name = "lblLbpTitle";
            this.lblLbpTitle.Size = new System.Drawing.Size(240, 30);
            this.lblLbpTitle.TabIndex = 0;
            this.lblLbpTitle.Text = "LEBANESE POUND";
            this.lblLbpTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.White;
            this.footerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.footerPanel.Controls.Add(this.lblTotal);
            this.footerPanel.Controls.Add(this.btnRefresh);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 600);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.footerPanel.Size = new System.Drawing.Size(1085, 100);
            this.footerPanel.TabIndex = 2;
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTotal.Location = new System.Drawing.Point(40, 20);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(600, 58);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total: $0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(873, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(170, 58);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.MouseEnter += new System.EventHandler(this.btnRefresh_MouseEnter);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.btnRefresh_MouseLeave);
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1085, 700);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Financial Report - Payment Management";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.cardsContainer.ResumeLayout(false);
            this.panelUsd.ResumeLayout(false);
            this.panelAed.ResumeLayout(false);
            this.panelLbp.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.FlowLayoutPanel cardsContainer;
        private System.Windows.Forms.Panel panelUsd;
        private System.Windows.Forms.Label lblUsdAmount;
        private System.Windows.Forms.Label lblUsdTitle;
        private System.Windows.Forms.Panel panelAed;
        private System.Windows.Forms.Label lblAedAmount;
        private System.Windows.Forms.Label lblAedTitle;
        private System.Windows.Forms.Panel panelLbp;
        private System.Windows.Forms.Label lblLbpAmount;
        private System.Windows.Forms.Label lblLbpTitle;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRefresh;

        // Additional methods for visual effects
        private void Panel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var panel = sender as System.Windows.Forms.Panel;
            using (var pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(230, 230, 230), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void btnRefresh_MouseEnter(object sender, System.EventArgs e)
        {
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
        }

        private void btnRefresh_MouseLeave(object sender, System.EventArgs e)
        {
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
        }
    }
}