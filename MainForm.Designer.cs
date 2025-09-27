namespace PaymentManagementSystem
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
            this.tabCourses = new System.Windows.Forms.TabPage();
            this.coursesControl1 = new PaymentManagementSystem.CoursesControl();
            this.tabOtherPayments = new System.Windows.Forms.TabPage();
            this.otherPaymentsControl1 = new PaymentManagementSystem.OtherPaymentsControl();
            this.tabUniversityPayments = new System.Windows.Forms.TabPage();
            this.universityPaymentsControl1 = new PaymentManagementSystem.UniversityPaymentsControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCourses.SuspendLayout();
            this.tabOtherPayments.SuspendLayout();
            this.tabUniversityPayments.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCourses
            // 
            this.tabCourses.Controls.Add(this.coursesControl1);
            this.tabCourses.Location = new System.Drawing.Point(4, 25);
            this.tabCourses.Margin = new System.Windows.Forms.Padding(4);
            this.tabCourses.Name = "tabCourses";
            this.tabCourses.Padding = new System.Windows.Forms.Padding(4);
            this.tabCourses.Size = new System.Drawing.Size(1592, 833);
            this.tabCourses.TabIndex = 2;
            this.tabCourses.Text = "Courses";
            this.tabCourses.UseVisualStyleBackColor = true;
            // 
            // coursesControl1
            // 
            this.coursesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coursesControl1.Location = new System.Drawing.Point(4, 4);
            this.coursesControl1.Margin = new System.Windows.Forms.Padding(5);
            this.coursesControl1.Name = "coursesControl1";
            this.coursesControl1.Size = new System.Drawing.Size(1584, 825);
            this.coursesControl1.TabIndex = 0;
            // 
            // tabOtherPayments
            // 
            this.tabOtherPayments.Controls.Add(this.otherPaymentsControl1);
            this.tabOtherPayments.Location = new System.Drawing.Point(4, 25);
            this.tabOtherPayments.Margin = new System.Windows.Forms.Padding(4);
            this.tabOtherPayments.Name = "tabOtherPayments";
            this.tabOtherPayments.Padding = new System.Windows.Forms.Padding(4);
            this.tabOtherPayments.Size = new System.Drawing.Size(1592, 833);
            this.tabOtherPayments.TabIndex = 1;
            this.tabOtherPayments.Text = "Other Payments";
            this.tabOtherPayments.UseVisualStyleBackColor = true;
            // 
            // otherPaymentsControl1
            // 
            this.otherPaymentsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.otherPaymentsControl1.Location = new System.Drawing.Point(4, 4);
            this.otherPaymentsControl1.Margin = new System.Windows.Forms.Padding(5);
            this.otherPaymentsControl1.Name = "otherPaymentsControl1";
            this.otherPaymentsControl1.Size = new System.Drawing.Size(1584, 825);
            this.otherPaymentsControl1.TabIndex = 0;
            // 
            // tabUniversityPayments
            // 
            this.tabUniversityPayments.Controls.Add(this.universityPaymentsControl1);
            this.tabUniversityPayments.Location = new System.Drawing.Point(4, 25);
            this.tabUniversityPayments.Margin = new System.Windows.Forms.Padding(4);
            this.tabUniversityPayments.Name = "tabUniversityPayments";
            this.tabUniversityPayments.Padding = new System.Windows.Forms.Padding(4);
            this.tabUniversityPayments.Size = new System.Drawing.Size(1592, 833);
            this.tabUniversityPayments.TabIndex = 0;
            this.tabUniversityPayments.Text = "University Payments";
            this.tabUniversityPayments.UseVisualStyleBackColor = true;
            // 
            // universityPaymentsControl1
            // 
            this.universityPaymentsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.universityPaymentsControl1.Location = new System.Drawing.Point(4, 4);
            this.universityPaymentsControl1.Margin = new System.Windows.Forms.Padding(5);
            this.universityPaymentsControl1.Name = "universityPaymentsControl1";
            this.universityPaymentsControl1.Size = new System.Drawing.Size(1584, 825);
            this.universityPaymentsControl1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabUniversityPayments);
            this.tabControl1.Controls.Add(this.tabOtherPayments);
            this.tabControl1.Controls.Add(this.tabCourses);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1600, 862);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 862);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Management System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabCourses.ResumeLayout(false);
            this.tabOtherPayments.ResumeLayout(false);
            this.tabUniversityPayments.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabCourses;
        private CoursesControl coursesControl1;
        private System.Windows.Forms.TabPage tabOtherPayments;
        private OtherPaymentsControl otherPaymentsControl1;
        private System.Windows.Forms.TabPage tabUniversityPayments;
        private UniversityPaymentsControl universityPaymentsControl1;
        private System.Windows.Forms.TabControl tabControl1;
    }
}