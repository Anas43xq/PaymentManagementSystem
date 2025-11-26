using PaymentBusinessLayer;
using System.Data;

namespace PaymentManagement
{
    public partial class OtherPaymentsForm : BasePaymentForm
    {
        public OtherPaymentsForm()
        {
            InitializeComponent();
            pageContext = PageContext.Other;
        }

        private void OtherPaymentsForm_Load(object sender, System.EventArgs e)
        {
            BasePaymentForm_Load(sender, e);
        }

        protected override void SetupFilters()
        {
            if (cmbPurposeFilter?.Items.Count > 0) return;
            
            cmbPurposeFilter?.BeginUpdate();
            try
            {
                cmbPurposeFilter?.Items.Add("All Purposes");

                DataTable dt = clsPaymentServices.GetCategories(clsPaymentServices.GetUniversityCategories(), false);

                foreach (DataRow row in dt.Rows)
                {
                    cmbPurposeFilter?.Items.Add(row["Name"].ToString());
                }

                if (cmbPurposeFilter?.Items.Count > 0)
                    cmbPurposeFilter.SelectedIndex = 0;
            }
            finally
            {
                cmbPurposeFilter?.EndUpdate();
            }
        }
    }
}
