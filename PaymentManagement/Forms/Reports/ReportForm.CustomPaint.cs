using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaymentManagement
{
    public partial class ReportForm
    {
        /// <summary>
        /// Called after InitializeComponent to apply custom paint events
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyCustomPaintEvents();
            ApplyHoverEffects();
        }

        /// <summary>
        /// Applies custom paint events to panels for enhanced visual effects
        /// </summary>
        private void ApplyCustomPaintEvents()
        {
            // Apply to University section panels
            pnlUniversity.Paint += Panel_PaintWithShadow;
            pnlExam.Paint += CategoryPanel_Paint;
            pnlRegistration.Paint += CategoryPanel_Paint;
            pnlUnivPayment.Paint += CategoryPanel_Paint;
            pnlBus.Paint += CategoryPanel_Paint;

            // Apply to Other Payments section panels
            pnlOtherPayments.Paint += Panel_PaintWithShadow;
            pnlSpeech.Paint += CategoryPanel_Paint;
            pnlDoctor.Paint += CategoryPanel_Paint;
            pnlDental.Paint += CategoryPanel_Paint;
            pnlBraces.Paint += CategoryPanel_Paint;
            pnlMobile.Paint += CategoryPanel_Paint;
            pnlData.Paint += CategoryPanel_Paint;
            pnlInternet.Paint += CategoryPanel_Paint;
            pnlOtherPayment.Paint += CategoryPanel_Paint;
            pnlPatrol.Paint += CategoryPanel_Paint;
        }

        /// <summary>
        /// Paints a panel with a subtle border
        /// </summary>
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            using (var pen = new Pen(Color.FromArgb(230, 230, 230), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        /// <summary>
        /// Paints a panel with a shadow effect for main sections
        /// </summary>
        private void Panel_PaintWithShadow(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw subtle shadow
            using (var shadowBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(shadowBrush, 2, 2, panel.Width - 2, panel.Height - 2);
            }

            // Draw border
            using (var pen = new Pen(Color.FromArgb(220, 220, 220), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        /// <summary>
        /// Paints category panels with rounded corners and subtle effects
        /// </summary>
        private void CategoryPanel_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create rounded rectangle path
            int radius = 8;
            var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            using (var path = GetRoundedRectPath(rect, radius))
            {
                // Fill background
                using (var brush = new SolidBrush(panel.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Draw border
                using (var pen = new Pen(Color.FromArgb(230, 230, 230), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        /// <summary>
        ///  rectangle graphics path
        /// </summary>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            // Top-left corner
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);

            // Top-right corner
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);

            // Bottom-right corner
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);

            // Bottom-left corner
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }


        private void ApplyHoverEffects()
        {
            // University section
            ApplyHoverToPanel(pnlExam);
            ApplyHoverToPanel(pnlRegistration);
            ApplyHoverToPanel(pnlUnivPayment);
            ApplyHoverToPanel(pnlBus);

            // Other Payments section
            ApplyHoverToPanel(pnlSpeech);
            ApplyHoverToPanel(pnlDoctor);
            ApplyHoverToPanel(pnlDental);
            ApplyHoverToPanel(pnlBraces);
            ApplyHoverToPanel(pnlMobile);
            ApplyHoverToPanel(pnlData);
            ApplyHoverToPanel(pnlInternet);
            ApplyHoverToPanel(pnlOtherPayment);
            ApplyHoverToPanel(pnlPatrol);
        }


        private void ApplyHoverToPanel(Panel panel)
        {
            var originalColor = panel.BackColor;
            var hoverColor = Color.FromArgb(245, 247, 250);

            panel.MouseEnter += (s, e) =>
            {
                panel.BackColor = hoverColor;
                panel.Cursor = Cursors.Hand;
            };

            panel.MouseLeave += (s, e) =>
            {
                panel.BackColor = originalColor;
                panel.Cursor = Cursors.Default;
            };
        }


        private void btnRefresh_MouseEnter(object sender, EventArgs e)
        {
            btnRefresh.BackColor = Color.FromArgb(37, 99, 235);
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.BackColor = Color.FromArgb(59, 130, 246);
        }
    }
}