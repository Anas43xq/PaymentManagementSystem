using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaymentManagement
{
    public static class ThemeManager
    {
        public static bool IsDarkMode { get; private set; } = false;

        #region Dark Theme Colors
        public static class DarkTheme
        {
            public static readonly Color Background = Color.FromArgb(32, 32, 32);
            public static readonly Color Surface = Color.FromArgb(45, 45, 45);
            public static readonly Color SurfaceVariant = Color.FromArgb(55, 55, 55);
            public static readonly Color Primary = Color.FromArgb(98, 181, 229);
            public static readonly Color PrimaryHover = Color.FromArgb(118, 201, 249);
            public static readonly Color TextPrimary = Color.FromArgb(230, 230, 230);
            public static readonly Color TextSecondary = Color.FromArgb(180, 180, 180);
            public static readonly Color Border = Color.FromArgb(70, 70, 70);
            public static readonly Color GridAlternate = Color.FromArgb(40, 40, 40);
            public static readonly Color Success = Color.FromArgb(76, 175, 80);
            public static readonly Color Error = Color.FromArgb(244, 67, 54);
        }
        #endregion

        #region Light Theme Colors
        public static class LightTheme
        {
            public static readonly Color Background = Color.White;
            public static readonly Color Surface = Color.FromArgb(245, 245, 245);
            public static readonly Color SurfaceVariant = Color.FromArgb(235, 235, 235);
            public static readonly Color Primary = Color.FromArgb(33, 150, 243);
            public static readonly Color PrimaryHover = Color.FromArgb(13, 130, 223);
            public static readonly Color TextPrimary = Color.Black;
            public static readonly Color TextSecondary = Color.FromArgb(100, 100, 100);
            public static readonly Color Border = Color.FromArgb(200, 200, 200);
            public static readonly Color GridAlternate = Color.LightGray;
            public static readonly Color Success = Color.Green;
            public static readonly Color Error = Color.Red;
        }
        #endregion

        public static void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
        }

        public static void SetDarkMode(bool enabled)
        {
            IsDarkMode = enabled;
        }

        public static void ApplyTheme(Form form)
        {
            if (IsDarkMode)
                ApplyDarkTheme(form);
            else
                ApplyLightTheme(form);
        }

        private static void ApplyDarkTheme(Control control)
        {
            control.BackColor = DarkTheme.Background;
            control.ForeColor = DarkTheme.TextPrimary;

            foreach (Control child in control.Controls)
            {
                if (child is Button btn)
                {
                    bool hasCustomColor = btn.BackColor != Color.White && 
                                         btn.BackColor != SystemColors.Control &&
                                         btn.BackColor != DarkTheme.Surface &&
                                         btn.BackColor != LightTheme.Surface;
                    
                    if (!hasCustomColor)
                    {
                        btn.BackColor = DarkTheme.Surface;
                        btn.ForeColor = DarkTheme.TextPrimary;
                        btn.FlatAppearance.BorderColor = DarkTheme.Border;
                    }
                    else
                    {
                        if (btn.ForeColor == Color.White || btn.ForeColor.GetBrightness() > 0.7f)
                        {
                            btn.ForeColor = Color.White;
                        }
                    }
                }
                else if (child is TextBox txt)
                {
                    txt.BackColor = DarkTheme.SurfaceVariant;
                    txt.ForeColor = DarkTheme.TextPrimary;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (child is ComboBox cmb)
                {
                    cmb.BackColor = DarkTheme.SurfaceVariant;
                    cmb.ForeColor = DarkTheme.TextPrimary;
                    cmb.FlatStyle = FlatStyle.Flat;
                }
                else if (child is DataGridView dgv)
                {
                    dgv.BackgroundColor = DarkTheme.Background;
                    dgv.ForeColor = DarkTheme.TextPrimary;
                    dgv.GridColor = DarkTheme.Border;
                    dgv.DefaultCellStyle.BackColor = DarkTheme.Surface;
                    dgv.DefaultCellStyle.ForeColor = DarkTheme.TextPrimary;
                    dgv.DefaultCellStyle.SelectionBackColor = DarkTheme.Primary;
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = DarkTheme.GridAlternate;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = DarkTheme.SurfaceVariant;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = DarkTheme.TextPrimary;
                    dgv.EnableHeadersVisualStyles = false;
                }
                else if (child is Label lbl)
                {
                    if (lbl.ForeColor == Color.Black || lbl.ForeColor == SystemColors.ControlText ||
                        lbl.ForeColor == Color.FromArgb(64, 64, 64))
                    {
                        lbl.ForeColor = DarkTheme.TextPrimary;
                    }
                }
                else if (child is GroupBox grp)
                {
                    grp.BackColor = DarkTheme.Surface;
                    grp.ForeColor = DarkTheme.TextPrimary;
                }
                else if (child is Panel pnl)
                {
                    pnl.BackColor = DarkTheme.Surface;
                }
                else if (child is DateTimePicker dtp)
                {
                    dtp.BackColor = DarkTheme.SurfaceVariant;
                    dtp.ForeColor = DarkTheme.TextPrimary;
                }

                if (child.HasChildren)
                    ApplyDarkTheme(child);
            }
        }

        private static void ApplyLightTheme(Control control)
        {
            control.BackColor = LightTheme.Background;
            control.ForeColor = LightTheme.TextPrimary;

            foreach (Control child in control.Controls)
            {
                if (child is Button btn)
                {
                    bool hasCustomColor = btn.BackColor != Color.White && 
                                         btn.BackColor != SystemColors.Control &&
                                         btn.BackColor != DarkTheme.Surface &&
                                         btn.BackColor != LightTheme.Surface &&
                                         btn.BackColor != Color.FromArgb(245, 245, 245);
                    
                    if (!hasCustomColor)
                    {
                        btn.BackColor = LightTheme.Surface;
                        btn.ForeColor = LightTheme.TextPrimary;
                    }
                }
                else if (child is TextBox txt)
                {
                    txt.BackColor = Color.White;
                    txt.ForeColor = LightTheme.TextPrimary;
                    txt.BorderStyle = BorderStyle.Fixed3D;
                }
                else if (child is ComboBox cmb)
                {
                    cmb.BackColor = Color.White;
                    cmb.ForeColor = LightTheme.TextPrimary;
                    cmb.FlatStyle = FlatStyle.Standard;
                }
                else if (child is DataGridView dgv)
                {
                    dgv.BackgroundColor = Color.White;
                    dgv.ForeColor = LightTheme.TextPrimary;
                    dgv.GridColor = LightTheme.Border;
                    dgv.DefaultCellStyle.BackColor = Color.White;
                    dgv.DefaultCellStyle.ForeColor = LightTheme.TextPrimary;
                    dgv.DefaultCellStyle.SelectionBackColor = LightTheme.Primary;
                    dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = LightTheme.GridAlternate;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = LightTheme.Surface;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = LightTheme.TextPrimary;
                    dgv.EnableHeadersVisualStyles = true;
                }
                else if (child is Label lbl)
                {
                    if (lbl.ForeColor == DarkTheme.TextPrimary || lbl.ForeColor == Color.White ||
                        lbl.ForeColor == Color.FromArgb(230, 230, 230))
                    {
                        lbl.ForeColor = LightTheme.TextPrimary;
                    }
                }
                else if (child is GroupBox grp)
                {
                    grp.BackColor = LightTheme.Background;
                    grp.ForeColor = LightTheme.TextPrimary;
                }
                else if (child is Panel pnl)
                {
                    pnl.BackColor = LightTheme.Surface;
                }
                else if (child is DateTimePicker dtp)
                {
                    dtp.BackColor = Color.White;
                    dtp.ForeColor = LightTheme.TextPrimary;
                }

                if (child.HasChildren)
                    ApplyLightTheme(child);
            }
        }
    }
}
