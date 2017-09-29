namespace Gimbal
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboProduct = new System.Windows.Forms.ComboBox();
            this.btnRealTimeImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTECTemp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.VERSION = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.xpos = new System.Windows.Forms.NumericUpDown();
            this.ypos = new System.Windows.Forms.NumericUpDown();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.lblTime = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkboxScan = new System.Windows.Forms.CheckBox();
            this.ExposureTime = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btDefaultSN = new System.Windows.Forms.Button();
            this.Tb_graymax = new System.Windows.Forms.TextBox();
            this.MaxGrayvalue = new System.Windows.Forms.Label();
            this.CurrentDefault = new System.Windows.Forms.CheckBox();
            this.ManualPosition = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LabelLoginMode = new System.Windows.Forms.Label();
            this.TestResult = new System.Windows.Forms.Label();
            this.Total = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Pass = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.PassPercent = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.FailPercent = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cleartotal = new System.Windows.Forms.Button();
            this.SaveImage = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Fail = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Form1_DeviceName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Form1_TestMode = new System.Windows.Forms.ComboBox();
            this.Form1_StepName = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.Form1_LotSize = new System.Windows.Forms.NumericUpDown();
            this.Form1_TesterID = new System.Windows.Forms.TextBox();
            this.Form1_OperatorName = new System.Windows.Forms.TextBox();
            this.Form1_DiffusionLotID = new System.Windows.Forms.TextBox();
            this.Form1_LotID = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ypos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Form1_LotSize)).BeginInit();
            this.SuspendLayout();
            // 
            // comboProduct
            // 
            this.comboProduct.FormattingEnabled = true;
            this.comboProduct.Items.AddRange(new object[] {
            "Normal",
            "Diffuser"});
            this.comboProduct.Location = new System.Drawing.Point(1482, 160);
            this.comboProduct.Margin = new System.Windows.Forms.Padding(4);
            this.comboProduct.Name = "comboProduct";
            this.comboProduct.Size = new System.Drawing.Size(46, 26);
            this.comboProduct.TabIndex = 5;
            this.comboProduct.Visible = false;
            // 
            // btnRealTimeImage
            // 
            this.btnRealTimeImage.Location = new System.Drawing.Point(1408, 154);
            this.btnRealTimeImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnRealTimeImage.Name = "btnRealTimeImage";
            this.btnRealTimeImage.Size = new System.Drawing.Size(66, 40);
            this.btnRealTimeImage.TabIndex = 6;
            this.btnRealTimeImage.Text = "Live";
            this.btnRealTimeImage.UseVisualStyleBackColor = true;
            this.btnRealTimeImage.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1530, 165);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Product";
            this.label1.Visible = false;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(1646, 256);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(244, 28);
            this.txtBarcode.TabIndex = 10;
            this.txtBarcode.Text = "FWP638701S2H6CWC5";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1732, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Barcode";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 36);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(2067, 126);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(876, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(350, 60);
            this.label4.TabIndex = 13;
            this.label4.Text = "Gimbal Tester";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configureToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(2055, 34);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(52, 28);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(124, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tCPSettingToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(107, 28);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // tCPSettingToolStripMenuItem
            // 
            this.tCPSettingToolStripMenuItem.Name = "tCPSettingToolStripMenuItem";
            this.tCPSettingToolStripMenuItem.Size = new System.Drawing.Size(193, 30);
            this.tCPSettingToolStripMenuItem.Text = "TCP Setting";
            this.tCPSettingToolStripMenuItem.Click += new System.EventHandler(this.tCPSettingToolStripMenuItem_Click);
            // 
            // txtTECTemp
            // 
            this.txtTECTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTECTemp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTECTemp.Location = new System.Drawing.Point(1785, 405);
            this.txtTECTemp.Margin = new System.Windows.Forms.Padding(4);
            this.txtTECTemp.Name = "txtTECTemp";
            this.txtTECTemp.Size = new System.Drawing.Size(244, 28);
            this.txtTECTemp.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1626, 410);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 21);
            this.label5.TabIndex = 17;
            this.label5.Text = "TEC(℃)";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1646, 228);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 21);
            this.label6.TabIndex = 18;
            this.label6.Text = "BarCode";
            // 
            // VERSION
            // 
            this.VERSION.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VERSION.AutoSize = true;
            this.VERSION.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VERSION.Location = new System.Drawing.Point(18, 1080);
            this.VERSION.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VERSION.Name = "VERSION";
            this.VERSION.Size = new System.Drawing.Size(81, 21);
            this.VERSION.TabIndex = 21;
            this.VERSION.Text = "Ver 4.2.6";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1626, 460);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 21);
            this.label8.TabIndex = 24;
            this.label8.Text = "X Angle(degree):";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1626, 512);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 21);
            this.label9.TabIndex = 25;
            this.label9.Text = "Y Angle(degree):";
            // 
            // xpos
            // 
            this.xpos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xpos.DecimalPlaces = 2;
            this.xpos.Enabled = false;
            this.xpos.Location = new System.Drawing.Point(1785, 458);
            this.xpos.Margin = new System.Windows.Forms.Padding(4);
            this.xpos.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.xpos.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            -2147483648});
            this.xpos.Name = "xpos";
            this.xpos.Size = new System.Drawing.Size(246, 28);
            this.xpos.TabIndex = 26;
            // 
            // ypos
            // 
            this.ypos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ypos.DecimalPlaces = 2;
            this.ypos.Enabled = false;
            this.ypos.Location = new System.Drawing.Point(1785, 510);
            this.ypos.Margin = new System.Windows.Forms.Padding(4);
            this.ypos.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ypos.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            -2147483648});
            this.ypos.Name = "ypos";
            this.ypos.Size = new System.Drawing.Size(246, 28);
            this.ypos.TabIndex = 27;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(62, 204);
            this.hWindowControl1.Margin = new System.Windows.Forms.Padding(4);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(844, 855);
            this.hWindowControl1.TabIndex = 28;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(844, 855);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.Blue;
            this.lblTime.Location = new System.Drawing.Point(1917, 189);
            this.lblTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(70, 24);
            this.lblTime.TabIndex = 30;
            this.lblTime.Text = "0.000";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Index,
            this.Description,
            this.Value,
            this.Status});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(916, 204);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(702, 853);
            this.listView1.TabIndex = 31;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Index
            // 
            this.Index.Text = "Index";
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 235;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 170;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 74;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkboxScan
            // 
            this.checkboxScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkboxScan.AutoSize = true;
            this.checkboxScan.Checked = true;
            this.checkboxScan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxScan.Location = new System.Drawing.Point(1648, 291);
            this.checkboxScan.Margin = new System.Windows.Forms.Padding(4);
            this.checkboxScan.Name = "checkboxScan";
            this.checkboxScan.Size = new System.Drawing.Size(79, 22);
            this.checkboxScan.TabIndex = 32;
            this.checkboxScan.Text = "Scan?";
            this.checkboxScan.UseVisualStyleBackColor = true;
            // 
            // ExposureTime
            // 
            this.ExposureTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExposureTime.Enabled = false;
            this.ExposureTime.Location = new System.Drawing.Point(1785, 562);
            this.ExposureTime.Margin = new System.Windows.Forms.Padding(4);
            this.ExposureTime.Name = "ExposureTime";
            this.ExposureTime.Size = new System.Drawing.Size(248, 28);
            this.ExposureTime.TabIndex = 33;
            this.ExposureTime.Text = "0";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(1626, 562);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 24);
            this.label10.TabIndex = 34;
            this.label10.Text = "Exposure(us):";
            // 
            // btDefaultSN
            // 
            this.btDefaultSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDefaultSN.Location = new System.Drawing.Point(1900, 256);
            this.btDefaultSN.Margin = new System.Windows.Forms.Padding(4);
            this.btDefaultSN.Name = "btDefaultSN";
            this.btDefaultSN.Size = new System.Drawing.Size(88, 28);
            this.btDefaultSN.TabIndex = 35;
            this.btDefaultSN.Text = "default";
            this.btDefaultSN.UseVisualStyleBackColor = true;
            this.btDefaultSN.Click += new System.EventHandler(this.btDefaultSN_Click);
            // 
            // Tb_graymax
            // 
            this.Tb_graymax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Tb_graymax.Location = new System.Drawing.Point(1785, 615);
            this.Tb_graymax.Margin = new System.Windows.Forms.Padding(4);
            this.Tb_graymax.Name = "Tb_graymax";
            this.Tb_graymax.ReadOnly = true;
            this.Tb_graymax.Size = new System.Drawing.Size(248, 28);
            this.Tb_graymax.TabIndex = 36;
            // 
            // MaxGrayvalue
            // 
            this.MaxGrayvalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxGrayvalue.AutoSize = true;
            this.MaxGrayvalue.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaxGrayvalue.Location = new System.Drawing.Point(1626, 616);
            this.MaxGrayvalue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MaxGrayvalue.Name = "MaxGrayvalue";
            this.MaxGrayvalue.Size = new System.Drawing.Size(91, 24);
            this.MaxGrayvalue.TabIndex = 37;
            this.MaxGrayvalue.Text = "MaxGray:";
            // 
            // CurrentDefault
            // 
            this.CurrentDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentDefault.AutoSize = true;
            this.CurrentDefault.Enabled = false;
            this.CurrentDefault.Location = new System.Drawing.Point(1648, 340);
            this.CurrentDefault.Margin = new System.Windows.Forms.Padding(4);
            this.CurrentDefault.Name = "CurrentDefault";
            this.CurrentDefault.Size = new System.Drawing.Size(178, 22);
            this.CurrentDefault.TabIndex = 38;
            this.CurrentDefault.Text = "Current Default?";
            this.CurrentDefault.UseVisualStyleBackColor = true;
            // 
            // ManualPosition
            // 
            this.ManualPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ManualPosition.AutoSize = true;
            this.ManualPosition.Enabled = false;
            this.ManualPosition.Location = new System.Drawing.Point(1648, 366);
            this.ManualPosition.Name = "ManualPosition";
            this.ManualPosition.Size = new System.Drawing.Size(178, 22);
            this.ManualPosition.TabIndex = 39;
            this.ManualPosition.Text = "Manual Position?";
            this.ManualPosition.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1202, 1084);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 18);
            this.label11.TabIndex = 40;
            this.label11.Text = "User Name:";
            // 
            // labelUserName
            // 
            this.labelUserName.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(1286, 1084);
            this.labelUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(71, 18);
            this.labelUserName.TabIndex = 40;
            this.labelUserName.Text = "default";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1430, 1084);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 18);
            this.label12.TabIndex = 40;
            this.label12.Text = "Login Mode:";
            // 
            // LabelLoginMode
            // 
            this.LabelLoginMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LabelLoginMode.AutoSize = true;
            this.LabelLoginMode.Location = new System.Drawing.Point(1530, 1084);
            this.LabelLoginMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelLoginMode.Name = "LabelLoginMode";
            this.LabelLoginMode.Size = new System.Drawing.Size(71, 18);
            this.LabelLoginMode.TabIndex = 40;
            this.LabelLoginMode.Text = "default";
            // 
            // TestResult
            // 
            this.TestResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TestResult.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestResult.ForeColor = System.Drawing.Color.Black;
            this.TestResult.Location = new System.Drawing.Point(1650, 165);
            this.TestResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TestResult.Name = "TestResult";
            this.TestResult.Size = new System.Drawing.Size(246, 63);
            this.TestResult.TabIndex = 41;
            this.TestResult.Text = "label14";
            this.TestResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Total
            // 
            this.Total.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Total.Location = new System.Drawing.Point(206, 1080);
            this.Total.Margin = new System.Windows.Forms.Padding(4);
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Size = new System.Drawing.Size(121, 28);
            this.Total.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(144, 1084);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 18);
            this.label14.TabIndex = 43;
            this.label14.Text = "Total:";
            // 
            // Pass
            // 
            this.Pass.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Pass.Location = new System.Drawing.Point(394, 1080);
            this.Pass.Margin = new System.Windows.Forms.Padding(4);
            this.Pass.Name = "Pass";
            this.Pass.ReadOnly = true;
            this.Pass.Size = new System.Drawing.Size(121, 28);
            this.Pass.TabIndex = 42;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(344, 1084);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 18);
            this.label15.TabIndex = 43;
            this.label15.Text = "Pass:";
            // 
            // PassPercent
            // 
            this.PassPercent.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PassPercent.Location = new System.Drawing.Point(850, 1080);
            this.PassPercent.Margin = new System.Windows.Forms.Padding(4);
            this.PassPercent.Name = "PassPercent";
            this.PassPercent.ReadOnly = true;
            this.PassPercent.Size = new System.Drawing.Size(68, 28);
            this.PassPercent.TabIndex = 42;
            this.PassPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(726, 1084);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 18);
            this.label16.TabIndex = 43;
            this.label16.Text = "Pass Percent:";
            // 
            // FailPercent
            // 
            this.FailPercent.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.FailPercent.Location = new System.Drawing.Point(1086, 1080);
            this.FailPercent.Margin = new System.Windows.Forms.Padding(4);
            this.FailPercent.Name = "FailPercent";
            this.FailPercent.ReadOnly = true;
            this.FailPercent.Size = new System.Drawing.Size(68, 28);
            this.FailPercent.TabIndex = 42;
            this.FailPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(962, 1084);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(125, 18);
            this.label17.TabIndex = 43;
            this.label17.Text = "Fail Percent:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(926, 1086);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 18);
            this.label2.TabIndex = 44;
            this.label2.Text = "%";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1161, 1086);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 18);
            this.label18.TabIndex = 45;
            this.label18.Text = "%";
            // 
            // cleartotal
            // 
            this.cleartotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cleartotal.Location = new System.Drawing.Point(1850, 990);
            this.cleartotal.Margin = new System.Windows.Forms.Padding(4);
            this.cleartotal.Name = "cleartotal";
            this.cleartotal.Size = new System.Drawing.Size(140, 69);
            this.cleartotal.TabIndex = 47;
            this.cleartotal.Text = "End Lot";
            this.cleartotal.UseVisualStyleBackColor = true;
            this.cleartotal.Click += new System.EventHandler(this.cleartotal_Click);
            // 
            // SaveImage
            // 
            this.SaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveImage.AutoSize = true;
            this.SaveImage.Checked = true;
            this.SaveImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SaveImage.Location = new System.Drawing.Point(1648, 316);
            this.SaveImage.Margin = new System.Windows.Forms.Padding(4);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(124, 22);
            this.SaveImage.TabIndex = 48;
            this.SaveImage.Text = "Saveimage?";
            this.SaveImage.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(536, 1086);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 18);
            this.label19.TabIndex = 50;
            this.label19.Text = "Fail:";
            // 
            // Fail
            // 
            this.Fail.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Fail.Location = new System.Drawing.Point(586, 1082);
            this.Fail.Margin = new System.Windows.Forms.Padding(4);
            this.Fail.Name = "Fail";
            this.Fail.ReadOnly = true;
            this.Fail.Size = new System.Drawing.Size(121, 28);
            this.Fail.TabIndex = 49;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1664, 990);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 69);
            this.button1.TabIndex = 51;
            this.button1.Text = "New Lot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1_DeviceName
            // 
            this.Form1_DeviceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_DeviceName.Location = new System.Drawing.Point(1785, 670);
            this.Form1_DeviceName.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_DeviceName.Name = "Form1_DeviceName";
            this.Form1_DeviceName.ReadOnly = true;
            this.Form1_DeviceName.Size = new System.Drawing.Size(247, 28);
            this.Form1_DeviceName.TabIndex = 53;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(1626, 674);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 24);
            this.label13.TabIndex = 52;
            this.label13.Text = "Device Name:";
            // 
            // Form1_TestMode
            // 
            this.Form1_TestMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_TestMode.Enabled = false;
            this.Form1_TestMode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Form1_TestMode.FormattingEnabled = true;
            this.Form1_TestMode.Items.AddRange(new object[] {
            "FIRSTTEST",
            "RETEST"});
            this.Form1_TestMode.Location = new System.Drawing.Point(1785, 778);
            this.Form1_TestMode.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_TestMode.Name = "Form1_TestMode";
            this.Form1_TestMode.Size = new System.Drawing.Size(248, 29);
            this.Form1_TestMode.TabIndex = 56;
            // 
            // Form1_StepName
            // 
            this.Form1_StepName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_StepName.Enabled = false;
            this.Form1_StepName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Form1_StepName.FormattingEnabled = true;
            this.Form1_StepName.Items.AddRange(new object[] {
            "GIMBAL TEST",
            "DCR"});
            this.Form1_StepName.Location = new System.Drawing.Point(1785, 723);
            this.Form1_StepName.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_StepName.Name = "Form1_StepName";
            this.Form1_StepName.Size = new System.Drawing.Size(246, 29);
            this.Form1_StepName.TabIndex = 57;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(1626, 782);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(105, 24);
            this.label20.TabIndex = 54;
            this.label20.Text = "Test Mode:";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(1626, 728);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 24);
            this.label21.TabIndex = 55;
            this.label21.Text = "Step Name:";
            // 
            // Form1_LotSize
            // 
            this.Form1_LotSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_LotSize.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Form1_LotSize.Location = new System.Drawing.Point(1785, 939);
            this.Form1_LotSize.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_LotSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Form1_LotSize.Name = "Form1_LotSize";
            this.Form1_LotSize.ReadOnly = true;
            this.Form1_LotSize.Size = new System.Drawing.Size(250, 28);
            this.Form1_LotSize.TabIndex = 67;
            // 
            // Form1_TesterID
            // 
            this.Form1_TesterID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_TesterID.Location = new System.Drawing.Point(1785, 1044);
            this.Form1_TesterID.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_TesterID.Name = "Form1_TesterID";
            this.Form1_TesterID.ReadOnly = true;
            this.Form1_TesterID.Size = new System.Drawing.Size(252, 28);
            this.Form1_TesterID.TabIndex = 63;
            // 
            // Form1_OperatorName
            // 
            this.Form1_OperatorName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_OperatorName.Location = new System.Drawing.Point(1785, 992);
            this.Form1_OperatorName.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_OperatorName.Name = "Form1_OperatorName";
            this.Form1_OperatorName.ReadOnly = true;
            this.Form1_OperatorName.Size = new System.Drawing.Size(247, 28);
            this.Form1_OperatorName.TabIndex = 64;
            // 
            // Form1_DiffusionLotID
            // 
            this.Form1_DiffusionLotID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_DiffusionLotID.Location = new System.Drawing.Point(1785, 886);
            this.Form1_DiffusionLotID.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_DiffusionLotID.Name = "Form1_DiffusionLotID";
            this.Form1_DiffusionLotID.ReadOnly = true;
            this.Form1_DiffusionLotID.Size = new System.Drawing.Size(248, 28);
            this.Form1_DiffusionLotID.TabIndex = 65;
            // 
            // Form1_LotID
            // 
            this.Form1_LotID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Form1_LotID.Location = new System.Drawing.Point(1785, 834);
            this.Form1_LotID.Margin = new System.Windows.Forms.Padding(4);
            this.Form1_LotID.Name = "Form1_LotID";
            this.Form1_LotID.ReadOnly = true;
            this.Form1_LotID.Size = new System.Drawing.Size(248, 28);
            this.Form1_LotID.TabIndex = 66;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(1626, 1052);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(90, 24);
            this.label22.TabIndex = 58;
            this.label22.Text = "Tester ID:";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(1626, 998);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(150, 24);
            this.label23.TabIndex = 59;
            this.label23.Text = "Operator Name:";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(1626, 944);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 24);
            this.label24.TabIndex = 60;
            this.label24.Text = "Lot Size:";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(1626, 890);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(147, 24);
            this.label25.TabIndex = 61;
            this.label25.Text = "Diffusion Lot ID:";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(1626, 836);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 24);
            this.label26.TabIndex = 62;
            this.label26.Text = "Lot ID:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2055, 1125);
            this.Controls.Add(this.Form1_LotSize);
            this.Controls.Add(this.Form1_TesterID);
            this.Controls.Add(this.Form1_OperatorName);
            this.Controls.Add(this.Form1_DiffusionLotID);
            this.Controls.Add(this.Form1_LotID);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.Form1_TestMode);
            this.Controls.Add(this.Form1_StepName);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.Form1_DeviceName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.Fail);
            this.Controls.Add(this.SaveImage);
            this.Controls.Add(this.cleartotal);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.FailPercent);
            this.Controls.Add(this.PassPercent);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.TestResult);
            this.Controls.Add(this.LabelLoginMode);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ManualPosition);
            this.Controls.Add(this.CurrentDefault);
            this.Controls.Add(this.MaxGrayvalue);
            this.Controls.Add(this.Tb_graymax);
            this.Controls.Add(this.btDefaultSN);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ExposureTime);
            this.Controls.Add(this.checkboxScan);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.ypos);
            this.Controls.Add(this.xpos);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.VERSION);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTECTemp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRealTimeImage);
            this.Controls.Add(this.comboProduct);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ypos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Form1_LotSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboProduct;
        private System.Windows.Forms.Button btnRealTimeImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tCPSettingToolStripMenuItem;
        private System.Windows.Forms.TextBox txtTECTemp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label VERSION;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown xpos;
        private System.Windows.Forms.NumericUpDown ypos;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Index;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkboxScan;
        private System.Windows.Forms.TextBox ExposureTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btDefaultSN;
        private System.Windows.Forms.TextBox Tb_graymax;
        private System.Windows.Forms.Label MaxGrayvalue;
        private System.Windows.Forms.CheckBox CurrentDefault;
        private System.Windows.Forms.CheckBox ManualPosition;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label LabelLoginMode;
        private System.Windows.Forms.Label TestResult;
        private System.Windows.Forms.TextBox Total;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Pass;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox PassPercent;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox FailPercent;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button cleartotal;
        private System.Windows.Forms.CheckBox SaveImage;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox Fail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Form1_DeviceName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox Form1_TestMode;
        private System.Windows.Forms.ComboBox Form1_StepName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown Form1_LotSize;
        private System.Windows.Forms.TextBox Form1_TesterID;
        private System.Windows.Forms.TextBox Form1_OperatorName;
        private System.Windows.Forms.TextBox Form1_DiffusionLotID;
        private System.Windows.Forms.TextBox Form1_LotID;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
    }
}

