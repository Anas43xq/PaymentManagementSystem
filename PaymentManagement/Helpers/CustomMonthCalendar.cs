using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaymentManagement
{
    public class CustomMonthCalendar : MonthCalendar
    {
        private Size _customSize = new Size(250, 200);

        public CustomMonthCalendar()
        {
            // Set custom dimensions
            this.CalendarDimensions = new Size(1, 1);

            // Styling
            this.BackColor = Color.White;
            this.TitleBackColor = Color.FromArgb(15, 23, 42);
            this.TitleForeColor = Color.White;
            this.TrailingForeColor = Color.FromArgb(203, 213, 225);
            this.ForeColor = Color.FromArgb(30, 41, 59);

            // Font
            this.Font = new Font("Segoe UI", 10F);

            // Selection appearance
            this.ShowToday = true;
            this.ShowTodayCircle = true;
            this.ShowWeekNumbers = false;

            // Maximum selection
            this.MaxSelectionCount = 31;

            // First day of week
            this.FirstDayOfWeek = Day.Monday;

            // Set initial size
            this.Size = _customSize;
        }

        // Override Size property to make it resizable
        public new Size Size
        {
            get { return _customSize; }
            set
            {
                _customSize = value;
                base.Size = value;
                AdjustFontSize();
            }
        }

        // Allow custom width
        public new int Width
        {
            get { return _customSize.Width; }
            set
            {
                _customSize.Width = value;
                base.Size = _customSize;
                AdjustFontSize();
            }
        }

        // Allow custom height
        public new int Height
        {
            get { return _customSize.Height; }
            set
            {
                _customSize.Height = value;
                base.Size = _customSize;
                AdjustFontSize();
            }
        }

        // Override MinimumSize and MaximumSize to allow full flexibility
        public override Size MinimumSize
        {
            get { return new Size(180, 150); }
            set { base.MinimumSize = value; }
        }

        public override Size MaximumSize
        {
            get { return new Size(400, 350); }
            set { base.MaximumSize = value; }
        }

        // Automatically adjust font based on size
        private void AdjustFontSize()
        {
            float fontSize = 10F;

            if (_customSize.Width < 200)
            {
                fontSize = 8F;
            }
            else if (_customSize.Width < 250)
            {
                fontSize = 9F;
            }
            else if (_customSize.Width < 300)
            {
                fontSize = 10F;
            }
            else
            {
                fontSize = 11F;
            }

            this.Font = new Font("Segoe UI", fontSize);
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _customSize = base.Size;
            AdjustFontSize();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void OnDateChanged(DateRangeEventArgs drevent)
        {
            base.OnDateChanged(drevent);
        }

        protected override void OnDateSelected(DateRangeEventArgs drevent)
        {
            base.OnDateSelected(drevent);
        }

        // Property to easily set size preset
        public enum SizePreset
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }

        private SizePreset _sizePreset = SizePreset.Medium;

        public SizePreset CalendarSize
        {
            get { return _sizePreset; }
            set
            {
                _sizePreset = value;
                ApplySizePreset();
            }
        }

        private void ApplySizePreset()
        {
            switch (_sizePreset)
            {
                case SizePreset.Small:
                    this.Size = new Size(200, 170);
                    break;
                case SizePreset.Medium:
                    this.Size = new Size(250, 200);
                    break;
                case SizePreset.Large:
                    this.Size = new Size(300, 240);
                    break;
                case SizePreset.ExtraLarge:
                    this.Size = new Size(350, 280);
                    break;
            }
        }

        // Custom color scheme property
        public enum ColorScheme
        {
            Default,
            Blue,
            Green,
            Purple,
            Dark
        }

        private ColorScheme _colorScheme = ColorScheme.Default;

        public ColorScheme Scheme
        {
            get { return _colorScheme; }
            set
            {
                _colorScheme = value;
                ApplyColorScheme();
            }
        }

        private void ApplyColorScheme()
        {
            switch (_colorScheme)
            {
                case ColorScheme.Default:
                    this.TitleBackColor = Color.FromArgb(15, 23, 42);
                    this.TitleForeColor = Color.White;
                    this.ForeColor = Color.FromArgb(30, 41, 59);
                    this.BackColor = Color.White;
                    break;
                case ColorScheme.Blue:
                    this.TitleBackColor = Color.FromArgb(37, 99, 235);
                    this.TitleForeColor = Color.White;
                    this.ForeColor = Color.FromArgb(30, 64, 175);
                    this.BackColor = Color.FromArgb(239, 246, 255);
                    break;
                case ColorScheme.Green:
                    this.TitleBackColor = Color.FromArgb(5, 150, 105);
                    this.TitleForeColor = Color.White;
                    this.ForeColor = Color.FromArgb(6, 95, 70);
                    this.BackColor = Color.FromArgb(236, 253, 245);
                    break;
                case ColorScheme.Purple:
                    this.TitleBackColor = Color.FromArgb(99, 102, 241);
                    this.TitleForeColor = Color.White;
                    this.ForeColor = Color.FromArgb(67, 56, 202);
                    this.BackColor = Color.FromArgb(237, 233, 254);
                    break;
                case ColorScheme.Dark:
                    this.TitleBackColor = Color.FromArgb(17, 24, 39);
                    this.TitleForeColor = Color.FromArgb(209, 213, 219);
                    this.ForeColor = Color.FromArgb(209, 213, 219);
                    this.BackColor = Color.FromArgb(31, 41, 55);
                    this.TrailingForeColor = Color.FromArgb(107, 114, 128);
                    break;
            }
            this.Invalidate();
        }

        // Helper method to set specific size
        public void SetCustomSize(int width, int height)
        {
            this.Size = new Size(width, height);
        }

        // Helper method to set date range easily
        public void SetDateRange(DateTime start, DateTime end)
        {
            this.SelectionStart = start;
            this.SelectionEnd = end;
        }

        // Helper method to get selected range
        public (DateTime start, DateTime end) GetSelectedRange()
        {
            return (this.SelectionStart, this.SelectionEnd);
        }

        // Set to current month
        public void SetCurrentMonth()
        {
            DateTime now = DateTime.Now;
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);
            SetDateRange(firstDay, lastDay);
        }

        // Set to current week
        public void SetCurrentWeek()
        {
            DateTime today = DateTime.Today;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime monday = today.AddDays(-diff);
            DateTime sunday = monday.AddDays(6);
            SetDateRange(monday, sunday);
        }

        // Set to last 7 days
        public void SetLast7Days()
        {
            DateTime end = DateTime.Today;
            DateTime start = end.AddDays(-6);
            SetDateRange(start, end);
        }

        // Set to last 30 days
        public void SetLast30Days()
        {
            DateTime end = DateTime.Today;
            DateTime start = end.AddDays(-29);
            SetDateRange(start, end);
        }
    }
}