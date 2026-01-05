using System.Drawing;
using System.Windows.Forms;

namespace PaymentManagement
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;

        // Field declarations
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private CustomMonthCalendar monthCalendar1;
        private Panel contentPanel;
        private FlowLayoutPanel cardsContainer;

        // University Panel
        private Panel pnlUniversity;
        private Label lblTotalUniversity;
        private Panel pnlExam;
        private Label lblExamTitle;
        private Label lblExam;
        private Panel pnlRegistration;
        private Label lblRegistrationTitle;
        private Label lblRegistration;
        private Panel pnlUnivPayment;
        private Label lblUnivPaymentTitle;
        private Label lblUnivPayment;
        private Panel pnlBus;
        private Label lblBusTitle;
        private Label lblBus;

        // Other Payments Panel
        private Panel pnlOtherPayments;
        private Label lblTotalOtherPayments;
        private Panel pnlSpeech;
        private Label lblSpeechTitle;
        private Label lblSpeech;
        private Panel pnlDoctor;
        private Label lblDoctorTitle;
        private Label lblDoctor;
        private Panel pnlDental;
        private Label lblDentalTitle;
        private Label lblDental;
        private Panel pnlBraces;
        private Label lblBracesTitle;
        private Label lblBraces;
        private Panel pnlMobile;
        private Label lblMobileTitle;
        private Label lblMobile;
        private Panel pnlData;
        private Label lblDataTitle;
        private Label lblData;
        private Panel pnlInternet;
        private Label lblInternetTitle;
        private Label lblInternet;
        private Panel pnlOtherPayment;
        private Label lblOtherPaymentTitle;
        private Label lblOtherPayment;
        private Panel pnlPatrol;
        private Label lblPatrolTitle;
        private Label lblPatrol;

        // Footer Panel
        private Panel footerPanel;
        private Label lblTotal;
        private Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.monthCalendar1 = new PaymentManagement.CustomMonthCalendar();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.cardsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlUniversity = new System.Windows.Forms.Panel();
            this.lblTotalUniversity = new System.Windows.Forms.Label();
            this.pnlExam = new System.Windows.Forms.Panel();
            this.lblExamTitle = new System.Windows.Forms.Label();
            this.lblExam = new System.Windows.Forms.Label();
            this.pnlRegistration = new System.Windows.Forms.Panel();
            this.lblRegistrationTitle = new System.Windows.Forms.Label();
            this.lblRegistration = new System.Windows.Forms.Label();
            this.pnlUnivPayment = new System.Windows.Forms.Panel();
            this.lblUnivPaymentTitle = new System.Windows.Forms.Label();
            this.lblUnivPayment = new System.Windows.Forms.Label();
            this.pnlBus = new System.Windows.Forms.Panel();
            this.lblBusTitle = new System.Windows.Forms.Label();
            this.lblBus = new System.Windows.Forms.Label();
            this.pnlOtherPayments = new System.Windows.Forms.Panel();
            this.lblTotalOtherPayments = new System.Windows.Forms.Label();
            this.pnlSpeech = new System.Windows.Forms.Panel();
            this.lblSpeechTitle = new System.Windows.Forms.Label();
            this.lblSpeech = new System.Windows.Forms.Label();
            this.pnlDoctor = new System.Windows.Forms.Panel();
            this.lblDoctorTitle = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.pnlDental = new System.Windows.Forms.Panel();
            this.lblDentalTitle = new System.Windows.Forms.Label();
            this.lblDental = new System.Windows.Forms.Label();
            this.pnlBraces = new System.Windows.Forms.Panel();
            this.lblBracesTitle = new System.Windows.Forms.Label();
            this.lblBraces = new System.Windows.Forms.Label();
            this.pnlMobile = new System.Windows.Forms.Panel();
            this.lblMobileTitle = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.pnlData = new System.Windows.Forms.Panel();
            this.lblDataTitle = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.pnlInternet = new System.Windows.Forms.Panel();
            this.lblInternetTitle = new System.Windows.Forms.Label();
            this.lblInternet = new System.Windows.Forms.Label();
            this.pnlOtherPayment = new System.Windows.Forms.Panel();
            this.lblOtherPaymentTitle = new System.Windows.Forms.Label();
            this.lblOtherPayment = new System.Windows.Forms.Label();
            this.pnlPatrol = new System.Windows.Forms.Panel();
            this.lblPatrolTitle = new System.Windows.Forms.Label();
            this.lblPatrol = new System.Windows.Forms.Label();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.cardsContainer.SuspendLayout();
            this.pnlUniversity.SuspendLayout();
            this.pnlExam.SuspendLayout();
            this.pnlRegistration.SuspendLayout();
            this.pnlUnivPayment.SuspendLayout();
            this.pnlBus.SuspendLayout();
            this.pnlOtherPayments.SuspendLayout();
            this.pnlSpeech.SuspendLayout();
            this.pnlDoctor.SuspendLayout();
            this.pnlDental.SuspendLayout();
            this.pnlBraces.SuspendLayout();
            this.pnlMobile.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.pnlInternet.SuspendLayout();
            this.pnlOtherPayment.SuspendLayout();
            this.pnlPatrol.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.headerPanel.Controls.Add(this.monthCalendar1);
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Padding = new System.Windows.Forms.Padding(40, 30, 40, 30);
            this.headerPanel.Size = new System.Drawing.Size(1400, 160);
            this.headerPanel.TabIndex = 2;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.monthCalendar1.BackColor = System.Drawing.Color.White;
            this.monthCalendar1.CalendarSize = PaymentManagement.CustomMonthCalendar.SizePreset.Small;
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.monthCalendar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.monthCalendar1.Location = new System.Drawing.Point(1048, 0);
            this.monthCalendar1.MaximumSize = new System.Drawing.Size(400, 350);
            this.monthCalendar1.MaxSelectionCount = 31;
            this.monthCalendar1.MinimumSize = new System.Drawing.Size(180, 150);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.Scheme = PaymentManagement.CustomMonthCalendar.ColorScheme.Default;
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.monthCalendar1.TitleForeColor = System.Drawing.Color.White;
            this.monthCalendar1.TrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar1_DateChanged);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(45, 95);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(386, 28);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Track and analyze your payment categories";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(40, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(542, 72);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Financial Dashboard";
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.contentPanel.Controls.Add(this.cardsContainer);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 160);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(40, 40, 40, 30);
            this.contentPanel.Size = new System.Drawing.Size(1400, 790);
            this.contentPanel.TabIndex = 0;
            // 
            // cardsContainer
            // 
            this.cardsContainer.AutoSize = true;
            this.cardsContainer.Controls.Add(this.pnlUniversity);
            this.cardsContainer.Controls.Add(this.pnlOtherPayments);
            this.cardsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardsContainer.Location = new System.Drawing.Point(40, 40);
            this.cardsContainer.Name = "cardsContainer";
            this.cardsContainer.Size = new System.Drawing.Size(1320, 740);
            this.cardsContainer.TabIndex = 0;
            // 
            // pnlUniversity
            // 
            this.pnlUniversity.BackColor = System.Drawing.Color.White;
            this.pnlUniversity.Controls.Add(this.lblTotalUniversity);
            this.pnlUniversity.Controls.Add(this.pnlExam);
            this.pnlUniversity.Controls.Add(this.pnlRegistration);
            this.pnlUniversity.Controls.Add(this.pnlUnivPayment);
            this.pnlUniversity.Controls.Add(this.pnlBus);
            this.pnlUniversity.Location = new System.Drawing.Point(0, 0);
            this.pnlUniversity.Margin = new System.Windows.Forms.Padding(0, 0, 20, 20);
            this.pnlUniversity.Name = "pnlUniversity";
            this.pnlUniversity.Padding = new System.Windows.Forms.Padding(30);
            this.pnlUniversity.Size = new System.Drawing.Size(620, 360);
            this.pnlUniversity.TabIndex = 0;
            // 
            // lblTotalUniversity
            // 
            this.lblTotalUniversity.AutoSize = true;
            this.lblTotalUniversity.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalUniversity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalUniversity.Location = new System.Drawing.Point(30, 25);
            this.lblTotalUniversity.Name = "lblTotalUniversity";
            this.lblTotalUniversity.Size = new System.Drawing.Size(336, 46);
            this.lblTotalUniversity.TabIndex = 0;
            this.lblTotalUniversity.Text = "University Expenses";
            // 
            // pnlExam
            // 
            this.pnlExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlExam.Controls.Add(this.lblExamTitle);
            this.pnlExam.Controls.Add(this.lblExam);
            this.pnlExam.Location = new System.Drawing.Point(30, 85);
            this.pnlExam.Name = "pnlExam";
            this.pnlExam.Padding = new System.Windows.Forms.Padding(20);
            this.pnlExam.Size = new System.Drawing.Size(260, 100);
            this.pnlExam.TabIndex = 1;
            // 
            // lblExamTitle
            // 
            this.lblExamTitle.AutoSize = true;
            this.lblExamTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblExamTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblExamTitle.Location = new System.Drawing.Point(20, 18);
            this.lblExamTitle.Name = "lblExamTitle";
            this.lblExamTitle.Size = new System.Drawing.Size(103, 25);
            this.lblExamTitle.TabIndex = 0;
            this.lblExamTitle.Text = "Exam Fees";
            // 
            // lblExam
            // 
            this.lblExam.AutoSize = true;
            this.lblExam.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblExam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblExam.Location = new System.Drawing.Point(20, 50);
            this.lblExam.Name = "lblExam";
            this.lblExam.Size = new System.Drawing.Size(109, 46);
            this.lblExam.TabIndex = 1;
            this.lblExam.Text = "$0.00";
            // 
            // pnlRegistration
            // 
            this.pnlRegistration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlRegistration.Controls.Add(this.lblRegistrationTitle);
            this.pnlRegistration.Controls.Add(this.lblRegistration);
            this.pnlRegistration.Location = new System.Drawing.Point(310, 85);
            this.pnlRegistration.Name = "pnlRegistration";
            this.pnlRegistration.Padding = new System.Windows.Forms.Padding(20);
            this.pnlRegistration.Size = new System.Drawing.Size(260, 100);
            this.pnlRegistration.TabIndex = 2;
            // 
            // lblRegistrationTitle
            // 
            this.lblRegistrationTitle.AutoSize = true;
            this.lblRegistrationTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRegistrationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblRegistrationTitle.Location = new System.Drawing.Point(20, 18);
            this.lblRegistrationTitle.Name = "lblRegistrationTitle";
            this.lblRegistrationTitle.Size = new System.Drawing.Size(120, 25);
            this.lblRegistrationTitle.TabIndex = 0;
            this.lblRegistrationTitle.Text = "Registration";
            // 
            // lblRegistration
            // 
            this.lblRegistration.AutoSize = true;
            this.lblRegistration.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblRegistration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblRegistration.Location = new System.Drawing.Point(20, 50);
            this.lblRegistration.Name = "lblRegistration";
            this.lblRegistration.Size = new System.Drawing.Size(109, 46);
            this.lblRegistration.TabIndex = 1;
            this.lblRegistration.Text = "$0.00";
            // 
            // pnlUnivPayment
            // 
            this.pnlUnivPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlUnivPayment.Controls.Add(this.lblUnivPaymentTitle);
            this.pnlUnivPayment.Controls.Add(this.lblUnivPayment);
            this.pnlUnivPayment.Location = new System.Drawing.Point(30, 205);
            this.pnlUnivPayment.Name = "pnlUnivPayment";
            this.pnlUnivPayment.Padding = new System.Windows.Forms.Padding(20);
            this.pnlUnivPayment.Size = new System.Drawing.Size(260, 100);
            this.pnlUnivPayment.TabIndex = 3;
            // 
            // lblUnivPaymentTitle
            // 
            this.lblUnivPaymentTitle.AutoSize = true;
            this.lblUnivPaymentTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUnivPaymentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblUnivPaymentTitle.Location = new System.Drawing.Point(20, 18);
            this.lblUnivPaymentTitle.Name = "lblUnivPaymentTitle";
            this.lblUnivPaymentTitle.Size = new System.Drawing.Size(184, 25);
            this.lblUnivPaymentTitle.TabIndex = 0;
            this.lblUnivPaymentTitle.Text = "University Payment";
            // 
            // lblUnivPayment
            // 
            this.lblUnivPayment.AutoSize = true;
            this.lblUnivPayment.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblUnivPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblUnivPayment.Location = new System.Drawing.Point(20, 50);
            this.lblUnivPayment.Name = "lblUnivPayment";
            this.lblUnivPayment.Size = new System.Drawing.Size(109, 46);
            this.lblUnivPayment.TabIndex = 1;
            this.lblUnivPayment.Text = "$0.00";
            // 
            // pnlBus
            // 
            this.pnlBus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlBus.Controls.Add(this.lblBusTitle);
            this.pnlBus.Controls.Add(this.lblBus);
            this.pnlBus.Location = new System.Drawing.Point(310, 205);
            this.pnlBus.Name = "pnlBus";
            this.pnlBus.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBus.Size = new System.Drawing.Size(260, 100);
            this.pnlBus.TabIndex = 4;
            // 
            // lblBusTitle
            // 
            this.lblBusTitle.AutoSize = true;
            this.lblBusTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblBusTitle.Location = new System.Drawing.Point(20, 18);
            this.lblBusTitle.Name = "lblBusTitle";
            this.lblBusTitle.Size = new System.Drawing.Size(183, 25);
            this.lblBusTitle.TabIndex = 0;
            this.lblBusTitle.Text = "Bus Transportation";
            // 
            // lblBus
            // 
            this.lblBus.AutoSize = true;
            this.lblBus.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblBus.Location = new System.Drawing.Point(20, 50);
            this.lblBus.Name = "lblBus";
            this.lblBus.Size = new System.Drawing.Size(109, 46);
            this.lblBus.TabIndex = 1;
            this.lblBus.Text = "$0.00";
            // 
            // pnlOtherPayments
            // 
            this.pnlOtherPayments.BackColor = System.Drawing.Color.White;
            this.pnlOtherPayments.Controls.Add(this.lblTotalOtherPayments);
            this.pnlOtherPayments.Controls.Add(this.pnlSpeech);
            this.pnlOtherPayments.Controls.Add(this.pnlDoctor);
            this.pnlOtherPayments.Controls.Add(this.pnlDental);
            this.pnlOtherPayments.Controls.Add(this.pnlBraces);
            this.pnlOtherPayments.Controls.Add(this.pnlMobile);
            this.pnlOtherPayments.Controls.Add(this.pnlData);
            this.pnlOtherPayments.Controls.Add(this.pnlInternet);
            this.pnlOtherPayments.Controls.Add(this.pnlOtherPayment);
            this.pnlOtherPayments.Controls.Add(this.pnlPatrol);
            this.pnlOtherPayments.Location = new System.Drawing.Point(640, 0);
            this.pnlOtherPayments.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.pnlOtherPayments.Name = "pnlOtherPayments";
            this.pnlOtherPayments.Padding = new System.Windows.Forms.Padding(30);
            this.pnlOtherPayments.Size = new System.Drawing.Size(620, 720);
            this.pnlOtherPayments.TabIndex = 1;
            // 
            // lblTotalOtherPayments
            // 
            this.lblTotalOtherPayments.AutoSize = true;
            this.lblTotalOtherPayments.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalOtherPayments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTotalOtherPayments.Location = new System.Drawing.Point(30, 25);
            this.lblTotalOtherPayments.Name = "lblTotalOtherPayments";
            this.lblTotalOtherPayments.Size = new System.Drawing.Size(274, 46);
            this.lblTotalOtherPayments.TabIndex = 0;
            this.lblTotalOtherPayments.Text = "Other Payments";
            // 
            // pnlSpeech
            // 
            this.pnlSpeech.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.pnlSpeech.Controls.Add(this.lblSpeechTitle);
            this.pnlSpeech.Controls.Add(this.lblSpeech);
            this.pnlSpeech.Location = new System.Drawing.Point(30, 85);
            this.pnlSpeech.Name = "pnlSpeech";
            this.pnlSpeech.Padding = new System.Windows.Forms.Padding(20);
            this.pnlSpeech.Size = new System.Drawing.Size(260, 100);
            this.pnlSpeech.TabIndex = 1;
            // 
            // lblSpeechTitle
            // 
            this.lblSpeechTitle.AutoSize = true;
            this.lblSpeechTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSpeechTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.lblSpeechTitle.Location = new System.Drawing.Point(20, 18);
            this.lblSpeechTitle.Name = "lblSpeechTitle";
            this.lblSpeechTitle.Size = new System.Drawing.Size(152, 25);
            this.lblSpeechTitle.TabIndex = 0;
            this.lblSpeechTitle.Text = "Speech Therapy";
            // 
            // lblSpeech
            // 
            this.lblSpeech.AutoSize = true;
            this.lblSpeech.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblSpeech.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblSpeech.Location = new System.Drawing.Point(20, 50);
            this.lblSpeech.Name = "lblSpeech";
            this.lblSpeech.Size = new System.Drawing.Size(109, 46);
            this.lblSpeech.TabIndex = 1;
            this.lblSpeech.Text = "$0.00";
            // 
            // pnlDoctor
            // 
            this.pnlDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.pnlDoctor.Controls.Add(this.lblDoctorTitle);
            this.pnlDoctor.Controls.Add(this.lblDoctor);
            this.pnlDoctor.Location = new System.Drawing.Point(310, 85);
            this.pnlDoctor.Name = "pnlDoctor";
            this.pnlDoctor.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDoctor.Size = new System.Drawing.Size(260, 100);
            this.pnlDoctor.TabIndex = 2;
            // 
            // lblDoctorTitle
            // 
            this.lblDoctorTitle.AutoSize = true;
            this.lblDoctorTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDoctorTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.lblDoctorTitle.Location = new System.Drawing.Point(20, 18);
            this.lblDoctorTitle.Name = "lblDoctorTitle";
            this.lblDoctorTitle.Size = new System.Drawing.Size(125, 25);
            this.lblDoctorTitle.TabIndex = 0;
            this.lblDoctorTitle.Text = "Doctor Visits";
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblDoctor.Location = new System.Drawing.Point(20, 50);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(109, 46);
            this.lblDoctor.TabIndex = 1;
            this.lblDoctor.Text = "$0.00";
            // 
            // pnlDental
            // 
            this.pnlDental.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.pnlDental.Controls.Add(this.lblDentalTitle);
            this.pnlDental.Controls.Add(this.lblDental);
            this.pnlDental.Location = new System.Drawing.Point(30, 205);
            this.pnlDental.Name = "pnlDental";
            this.pnlDental.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDental.Size = new System.Drawing.Size(260, 100);
            this.pnlDental.TabIndex = 3;
            // 
            // lblDentalTitle
            // 
            this.lblDentalTitle.AutoSize = true;
            this.lblDentalTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDentalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.lblDentalTitle.Location = new System.Drawing.Point(20, 18);
            this.lblDentalTitle.Name = "lblDentalTitle";
            this.lblDentalTitle.Size = new System.Drawing.Size(115, 25);
            this.lblDentalTitle.TabIndex = 0;
            this.lblDentalTitle.Text = "Dental Care";
            // 
            // lblDental
            // 
            this.lblDental.AutoSize = true;
            this.lblDental.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblDental.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblDental.Location = new System.Drawing.Point(20, 50);
            this.lblDental.Name = "lblDental";
            this.lblDental.Size = new System.Drawing.Size(109, 46);
            this.lblDental.TabIndex = 1;
            this.lblDental.Text = "$0.00";
            // 
            // pnlBraces
            // 
            this.pnlBraces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.pnlBraces.Controls.Add(this.lblBracesTitle);
            this.pnlBraces.Controls.Add(this.lblBraces);
            this.pnlBraces.Location = new System.Drawing.Point(310, 205);
            this.pnlBraces.Name = "pnlBraces";
            this.pnlBraces.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBraces.Size = new System.Drawing.Size(260, 100);
            this.pnlBraces.TabIndex = 4;
            // 
            // lblBracesTitle
            // 
            this.lblBracesTitle.AutoSize = true;
            this.lblBracesTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBracesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.lblBracesTitle.Location = new System.Drawing.Point(20, 18);
            this.lblBracesTitle.Name = "lblBracesTitle";
            this.lblBracesTitle.Size = new System.Drawing.Size(132, 25);
            this.lblBracesTitle.TabIndex = 0;
            this.lblBracesTitle.Text = "Dental Braces";
            // 
            // lblBraces
            // 
            this.lblBraces.AutoSize = true;
            this.lblBraces.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBraces.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblBraces.Location = new System.Drawing.Point(20, 50);
            this.lblBraces.Name = "lblBraces";
            this.lblBraces.Size = new System.Drawing.Size(109, 46);
            this.lblBraces.TabIndex = 1;
            this.lblBraces.Text = "$0.00";
            // 
            // pnlMobile
            // 
            this.pnlMobile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.pnlMobile.Controls.Add(this.lblMobileTitle);
            this.pnlMobile.Controls.Add(this.lblMobile);
            this.pnlMobile.Location = new System.Drawing.Point(30, 325);
            this.pnlMobile.Name = "pnlMobile";
            this.pnlMobile.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMobile.Size = new System.Drawing.Size(260, 100);
            this.pnlMobile.TabIndex = 5;
            // 
            // lblMobileTitle
            // 
            this.lblMobileTitle.AutoSize = true;
            this.lblMobileTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMobileTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.lblMobileTitle.Location = new System.Drawing.Point(20, 18);
            this.lblMobileTitle.Name = "lblMobileTitle";
            this.lblMobileTitle.Size = new System.Drawing.Size(114, 25);
            this.lblMobileTitle.TabIndex = 0;
            this.lblMobileTitle.Text = "Mobile Bills";
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblMobile.Location = new System.Drawing.Point(20, 50);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(109, 46);
            this.lblMobile.TabIndex = 1;
            this.lblMobile.Text = "$0.00";
            // 
            // pnlData
            // 
            this.pnlData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.pnlData.Controls.Add(this.lblDataTitle);
            this.pnlData.Controls.Add(this.lblData);
            this.pnlData.Location = new System.Drawing.Point(310, 325);
            this.pnlData.Name = "pnlData";
            this.pnlData.Padding = new System.Windows.Forms.Padding(20);
            this.pnlData.Size = new System.Drawing.Size(260, 100);
            this.pnlData.TabIndex = 6;
            // 
            // lblDataTitle
            // 
            this.lblDataTitle.AutoSize = true;
            this.lblDataTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDataTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.lblDataTitle.Location = new System.Drawing.Point(20, 18);
            this.lblDataTitle.Name = "lblDataTitle";
            this.lblDataTitle.Size = new System.Drawing.Size(120, 25);
            this.lblDataTitle.TabIndex = 0;
            this.lblDataTitle.Text = "Mobile Data";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblData.Location = new System.Drawing.Point(20, 50);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(109, 46);
            this.lblData.TabIndex = 1;
            this.lblData.Text = "$0.00";
            // 
            // pnlInternet
            // 
            this.pnlInternet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.pnlInternet.Controls.Add(this.lblInternetTitle);
            this.pnlInternet.Controls.Add(this.lblInternet);
            this.pnlInternet.Location = new System.Drawing.Point(30, 445);
            this.pnlInternet.Name = "pnlInternet";
            this.pnlInternet.Padding = new System.Windows.Forms.Padding(20);
            this.pnlInternet.Size = new System.Drawing.Size(260, 100);
            this.pnlInternet.TabIndex = 7;
            // 
            // lblInternetTitle
            // 
            this.lblInternetTitle.AutoSize = true;
            this.lblInternetTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblInternetTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.lblInternetTitle.Location = new System.Drawing.Point(20, 18);
            this.lblInternetTitle.Name = "lblInternetTitle";
            this.lblInternetTitle.Size = new System.Drawing.Size(153, 25);
            this.lblInternetTitle.TabIndex = 0;
            this.lblInternetTitle.Text = "Internet Service";
            // 
            // lblInternet
            // 
            this.lblInternet.AutoSize = true;
            this.lblInternet.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblInternet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblInternet.Location = new System.Drawing.Point(20, 50);
            this.lblInternet.Name = "lblInternet";
            this.lblInternet.Size = new System.Drawing.Size(109, 46);
            this.lblInternet.TabIndex = 1;
            this.lblInternet.Text = "$0.00";
            // 
            // pnlOtherPayment
            // 
            this.pnlOtherPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlOtherPayment.Controls.Add(this.lblOtherPaymentTitle);
            this.pnlOtherPayment.Controls.Add(this.lblOtherPayment);
            this.pnlOtherPayment.Location = new System.Drawing.Point(310, 445);
            this.pnlOtherPayment.Name = "pnlOtherPayment";
            this.pnlOtherPayment.Padding = new System.Windows.Forms.Padding(20);
            this.pnlOtherPayment.Size = new System.Drawing.Size(260, 100);
            this.pnlOtherPayment.TabIndex = 8;
            // 
            // lblOtherPaymentTitle
            // 
            this.lblOtherPaymentTitle.AutoSize = true;
            this.lblOtherPaymentTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblOtherPaymentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblOtherPaymentTitle.Location = new System.Drawing.Point(20, 18);
            this.lblOtherPaymentTitle.Name = "lblOtherPaymentTitle";
            this.lblOtherPaymentTitle.Size = new System.Drawing.Size(148, 25);
            this.lblOtherPaymentTitle.TabIndex = 0;
            this.lblOtherPaymentTitle.Text = "Other Expenses";
            // 
            // lblOtherPayment
            // 
            this.lblOtherPayment.AutoSize = true;
            this.lblOtherPayment.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblOtherPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblOtherPayment.Location = new System.Drawing.Point(20, 50);
            this.lblOtherPayment.Name = "lblOtherPayment";
            this.lblOtherPayment.Size = new System.Drawing.Size(109, 46);
            this.lblOtherPayment.TabIndex = 1;
            this.lblOtherPayment.Text = "$0.00";
            // 
            // pnlPatrol
            // 
            this.pnlPatrol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.pnlPatrol.Controls.Add(this.lblPatrolTitle);
            this.pnlPatrol.Controls.Add(this.lblPatrol);
            this.pnlPatrol.Location = new System.Drawing.Point(30, 565);
            this.pnlPatrol.Name = "pnlPatrol";
            this.pnlPatrol.Padding = new System.Windows.Forms.Padding(20);
            this.pnlPatrol.Size = new System.Drawing.Size(260, 100);
            this.pnlPatrol.TabIndex = 9;
            // 
            // lblPatrolTitle
            // 
            this.lblPatrolTitle.AutoSize = true;
            this.lblPatrolTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPatrolTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblPatrolTitle.Location = new System.Drawing.Point(20, 18);
            this.lblPatrolTitle.Name = "lblPatrolTitle";
            this.lblPatrolTitle.Size = new System.Drawing.Size(143, 25);
            this.lblPatrolTitle.TabIndex = 0;
            this.lblPatrolTitle.Text = "Patrol Services";
            // 
            // lblPatrol
            // 
            this.lblPatrol.AutoSize = true;
            this.lblPatrol.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblPatrol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblPatrol.Location = new System.Drawing.Point(20, 50);
            this.lblPatrol.Name = "lblPatrol";
            this.lblPatrol.Size = new System.Drawing.Size(109, 46);
            this.lblPatrol.TabIndex = 1;
            this.lblPatrol.Text = "$0.00";
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.footerPanel.Controls.Add(this.lblTotal);
            this.footerPanel.Controls.Add(this.btnRefresh);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 880);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1400, 70);
            this.footerPanel.TabIndex = 0;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(3, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(313, 46);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Grand Total: $0.00";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(2400, 14);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(160, 42);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "↻ Refresh Data";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // ReportForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1400, 950);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.headerPanel);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Financial Dashboard - Payment Management System";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.cardsContainer.ResumeLayout(false);
            this.pnlUniversity.ResumeLayout(false);
            this.pnlUniversity.PerformLayout();
            this.pnlExam.ResumeLayout(false);
            this.pnlExam.PerformLayout();
            this.pnlRegistration.ResumeLayout(false);
            this.pnlRegistration.PerformLayout();
            this.pnlUnivPayment.ResumeLayout(false);
            this.pnlUnivPayment.PerformLayout();
            this.pnlBus.ResumeLayout(false);
            this.pnlBus.PerformLayout();
            this.pnlOtherPayments.ResumeLayout(false);
            this.pnlOtherPayments.PerformLayout();
            this.pnlSpeech.ResumeLayout(false);
            this.pnlSpeech.PerformLayout();
            this.pnlDoctor.ResumeLayout(false);
            this.pnlDoctor.PerformLayout();
            this.pnlDental.ResumeLayout(false);
            this.pnlDental.PerformLayout();
            this.pnlBraces.ResumeLayout(false);
            this.pnlBraces.PerformLayout();
            this.pnlMobile.ResumeLayout(false);
            this.pnlMobile.PerformLayout();
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.pnlInternet.ResumeLayout(false);
            this.pnlInternet.PerformLayout();
            this.pnlOtherPayment.ResumeLayout(false);
            this.pnlOtherPayment.PerformLayout();
            this.pnlPatrol.ResumeLayout(false);
            this.pnlPatrol.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}