using PaymentBusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace PaymentManagement
{
    public partial class ReportForm : Form
    {
        DataTable _dtreport;
        private bool _dataLoaded = false;

        public ReportForm()
        {
            InitializeComponent();
            InitializeData();
            this.VisibleChanged += ReportForm_VisibleChanged;
        }

        private void ReportForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !_dataLoaded)
            {
                LoadData();
                _dataLoaded = true;
            }
        }

        private void InitializeData()
        {
            lblUsdAmount.Text = "$0.00";
            lblAedAmount.Text = "AED 0.00";
            lblLbpAmount.Text = "LBP 0";
            lblTotal.Text = "Total: $0.00 USD | AED 0.00";
        }

        public void LoadData()
        {
            _dtreport = clsPaymentServices.GetAllCurrencyTotals();
            if (_dtreport == null) return;

            decimal totalUsd = 0;
            decimal totalAed = 0;
            decimal totalLbp = 0;

            if (_dtreport.Rows.Count > 0)
            {
                foreach (DataRow row in _dtreport.Rows)
                {
                    string currency = row["Code"].ToString();
                    decimal amount = Convert.ToDecimal(row["TotalAmount"]);

                    if (currency == "USD")
                    {
                        totalUsd = amount;
                        lblUsdAmount.Text = $"${amount:N2}";
                    }
                    else if (currency == "AED")
                    {
                        totalAed = amount;
                        lblAedAmount.Text = $"AED {amount:N2}";
                    }
                    else if (currency == "LBP")
                    {
                        totalLbp = amount;
                        lblLbpAmount.Text = $"LBP {amount:N0}";
                    }
                }
                UpdateAmounts(totalUsd, totalAed, totalLbp);
            }
        }

        private void UpdateTotal(decimal usd, decimal aed, decimal lbp)
        {
            decimal totalUsd = usd + (aed / 3.67m) + (lbp / 89500m);
            decimal totalAed = (usd * 3.67m) + aed + (lbp / 24400m);
            lblTotal.Text = $"Total: ${totalUsd:N2} USD | AED {totalAed:N2}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _dataLoaded = false;
            LoadData();
            _dataLoaded = true;
        }

        public void UpdateAmounts(decimal usd, decimal aed, decimal lbp)
        {
            lblUsdAmount.Text = $"${usd:N2}";
            lblAedAmount.Text = $"AED {aed:N2}";
            lblLbpAmount.Text = $"LBP {lbp:N0}";

            UpdateTotal(usd, aed, lbp);
        }

    }
}