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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageUniversityPayments = new System.Windows.Forms.TabPage();
            this.tabPageOtherPayments = new System.Windows.Forms.TabPage();
            this.tabPageCourses = new System.Windows.Forms.TabPage();
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageUniversityPayments);
            this.tabControlMain.Controls.Add(this.tabPageOtherPayments);
            this.tabControlMain.Controls.Add(this.tabPageCourses);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1200, 700);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageUniversityPayments
            // 
            this.tabPageUniversityPayments.Location = new System.Drawing.Point(4, 25);
            this.tabPageUniversityPayments.Name = "tabPageUniversityPayments";
            this.tabPageUniversityPayments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUniversityPayments.Size = new System.Drawing.Size(1192, 671);
            this.tabPageUniversityPayments.TabIndex = 0;
            this.tabPageUniversityPayments.Text = "University Payments";
            this.tabPageUniversityPayments.UseVisualStyleBackColor = true;
            // 
            // tabPageOtherPayments
            // 
            this.tabPageOtherPayments.Location = new System.Drawing.Point(4, 25);
            this.tabPageOtherPayments.Name = "tabPageOtherPayments";
            this.tabPageOtherPayments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOtherPayments.Size = new System.Drawing.Size(1192, 671);
            this.tabPageOtherPayments.TabIndex = 1;
            this.tabPageOtherPayments.Text = "Other Payments";
            this.tabPageOtherPayments.UseVisualStyleBackColor = true;
            // 
            // tabPageCourses
            // 
            this.tabPageCourses.Location = new System.Drawing.Point(4, 25);
            this.tabPageCourses.Name = "tabPageCourses";
            this.tabPageCourses.Size = new System.Drawing.Size(1192, 671);
            this.tabPageCourses.TabIndex = 2;
            this.tabPageCourses.Text = "Courses";
            this.tabPageCourses.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControlMain);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Management System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageUniversityPayments;
        private System.Windows.Forms.TabPage tabPageOtherPayments;
        private System.Windows.Forms.TabPage tabPageCourses;
    }
}