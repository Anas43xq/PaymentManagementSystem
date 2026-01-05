using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaymentManagement.Forms.Auth
{
    public partial class frmSignup
    {
        private Panel currentFocusedPanel = null;
        private Point mouseDownLocation;

        #region Custom Painting

        private void pnlLeft_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                pnl.ClientRectangle,
                Color.FromArgb(41, 128, 185),
                Color.FromArgb(52, 152, 219),
                LinearGradientMode.Vertical))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillRectangle(brush, pnl.ClientRectangle);

                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
                {
                    e.Graphics.FillEllipse(circleBrush, -50, -50, 220, 220);
                    e.Graphics.FillEllipse(circleBrush, 220, 400, 280, 280);
                }
            }
        }

        // Input panel with rounded corners and border
        private void pnlInput_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = 8;
            Rectangle rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
            using (GraphicsPath path = GetRoundedRect(rect, radius))
            {
                // Fill background
                using (SolidBrush brush = new SolidBrush(pnl.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Draw border
                Color borderColor = (pnl == currentFocusedPanel)
                    ? Color.FromArgb(41, 128, 185)
                    : Color.FromArgb(220, 220, 220);

                int borderWidth = (pnl == currentFocusedPanel) ? 2 : 1;

                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        // Primary button with rounded corners
        private void btnPrimary_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = 8;
            Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);

            using (GraphicsPath path = GetRoundedRect(rect, radius))
            {
                e.Graphics.FillPath(new SolidBrush(btn.BackColor), path);
            }

            // Draw text
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, rect,
                btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // Secondary button with rounded corners
        private void btnSecondary_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = 8;
            Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);

            using (GraphicsPath path = GetRoundedRect(rect, radius))
            {
                e.Graphics.FillPath(new SolidBrush(btn.BackColor), path);
            }

            // Draw text
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, rect,
                btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // Helper method to create rounded rectangles
        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        #endregion

        #region Input Focus Events

        private void txtInput_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            currentFocusedPanel = txt.Parent as Panel;
            currentFocusedPanel?.Invalidate();
        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            if (currentFocusedPanel != null)
            {
                Panel prevPanel = currentFocusedPanel;
                currentFocusedPanel = null;
                prevPanel.Invalidate();
            }
        }

        #endregion

        #region Hover Effects

        private void pnlInput_MouseEnter(object sender, EventArgs e)
        {
            Panel pnl = sender as Panel;
            if (pnl != currentFocusedPanel)
            {
                pnl.BackColor = Color.FromArgb(248, 248, 248);
            }
        }

        private void pnlInput_MouseLeave(object sender, EventArgs e)
        {
            Panel pnl = sender as Panel;
            if (pnl != currentFocusedPanel)
            {
                pnl.BackColor = Color.FromArgb(250, 250, 250);
            }
        }

        private void btnSignup_MouseEnter(object sender, EventArgs e)
        {
            btnSignup.BackColor = Color.FromArgb(52, 152, 219);
        }

        private void btnSignup_MouseLeave(object sender, EventArgs e)
        {
            btnSignup.BackColor = Color.FromArgb(41, 128, 185);
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.FromArgb(220, 224, 225);
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.BackColor = Color.FromArgb(236, 240, 241);
        }

        #endregion

        #region Form Dragging

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
            }
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - mouseDownLocation.X;
                this.Top += e.Y - mouseDownLocation.Y;
            }
        }

        #endregion
    }
}