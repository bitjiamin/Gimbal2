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
            this.btnReadimage = new System.Windows.Forms.Button();
            this.comboProduct = new System.Windows.Forms.ComboBox();
            this.btnRealTimeImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboCamera = new System.Windows.Forms.ComboBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTECTemp = new System.Windows.Forms.Button();
            this.txtTECTemp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnsnap = new System.Windows.Forms.Button();
            this.btnlive = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.xpos = new System.Windows.Forms.NumericUpDown();
            this.ypos = new System.Windows.Forms.NumericUpDown();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.btnROI = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ypos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReadimage
            // 
            this.btnReadimage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadimage.Location = new System.Drawing.Point(908, 399);
            this.btnReadimage.Name = "btnReadimage";
            this.btnReadimage.Size = new System.Drawing.Size(63, 34);
            this.btnReadimage.TabIndex = 1;
            this.btnReadimage.Text = "Read Image";
            this.btnReadimage.UseVisualStyleBackColor = true;
            this.btnReadimage.Click += new System.EventHandler(this.btnReadimage_Click);
            // 
            // comboProduct
            // 
            this.comboProduct.FormattingEnabled = true;
            this.comboProduct.Items.AddRange(new object[] {
            "Normal",
            "Diffuser"});
            this.comboProduct.Location = new System.Drawing.Point(1181, 290);
            this.comboProduct.Name = "comboProduct";
            this.comboProduct.Size = new System.Drawing.Size(111, 20);
            this.comboProduct.TabIndex = 5;
            this.comboProduct.Visible = false;
            // 
            // btnRealTimeImage
            // 
            this.btnRealTimeImage.Location = new System.Drawing.Point(1128, 465);
            this.btnRealTimeImage.Name = "btnRealTimeImage";
            this.btnRealTimeImage.Size = new System.Drawing.Size(164, 34);
            this.btnRealTimeImage.TabIndex = 6;
            this.btnRealTimeImage.Text = "Live";
            this.btnRealTimeImage.UseVisualStyleBackColor = true;
            this.btnRealTimeImage.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1128, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Product";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(736, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Camera";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboCamera
            // 
            this.comboCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboCamera.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCamera.FormattingEnabled = true;
            this.comboCamera.Items.AddRange(new object[] {
            "Baumer",
            "AVT"});
            this.comboCamera.Location = new System.Drawing.Point(736, 238);
            this.comboCamera.Name = "comboCamera";
            this.comboCamera.Size = new System.Drawing.Size(164, 23);
            this.comboCamera.TabIndex = 9;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(736, 154);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(164, 21);
            this.txtBarcode.TabIndex = 10;
            this.txtBarcode.Text = "FWP638701S2H6CWC5";
            this.txtBarcode.TextChanged += new System.EventHandler(this.txtBarcode_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(748, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Barcode";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(971, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(365, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 40);
            this.label4.TabIndex = 13;
            this.label4.Text = "Gimbal Tester";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.configureToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tCPSettingToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // tCPSettingToolStripMenuItem
            // 
            this.tCPSettingToolStripMenuItem.Name = "tCPSettingToolStripMenuItem";
            this.tCPSettingToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.tCPSettingToolStripMenuItem.Text = "TCP Setting";
            this.tCPSettingToolStripMenuItem.Click += new System.EventHandler(this.tCPSettingToolStripMenuItem_Click);
            // 
            // btnTECTemp
            // 
            this.btnTECTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTECTemp.Location = new System.Drawing.Point(735, 399);
            this.btnTECTemp.Name = "btnTECTemp";
            this.btnTECTemp.Size = new System.Drawing.Size(167, 35);
            this.btnTECTemp.TabIndex = 15;
            this.btnTECTemp.Text = "Read TEC Temp";
            this.btnTECTemp.UseVisualStyleBackColor = true;
            this.btnTECTemp.Click += new System.EventHandler(this.btnTECTemp_Click);
            // 
            // txtTECTemp
            // 
            this.txtTECTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTECTemp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTECTemp.Location = new System.Drawing.Point(736, 196);
            this.txtTECTemp.Name = "txtTECTemp";
            this.txtTECTemp.Size = new System.Drawing.Size(164, 21);
            this.txtTECTemp.TabIndex = 16;
            this.txtTECTemp.TextChanged += new System.EventHandler(this.txtTECTemp_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(736, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "TEC(℃)";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(736, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "BarCode";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // btnsnap
            // 
            this.btnsnap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsnap.Location = new System.Drawing.Point(735, 455);
            this.btnsnap.Name = "btnsnap";
            this.btnsnap.Size = new System.Drawing.Size(167, 35);
            this.btnsnap.TabIndex = 19;
            this.btnsnap.Text = "Snap";
            this.btnsnap.UseVisualStyleBackColor = true;
            this.btnsnap.Click += new System.EventHandler(this.btnsnap_Click);
            // 
            // btnlive
            // 
            this.btnlive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnlive.Location = new System.Drawing.Point(735, 506);
            this.btnlive.Name = "btnlive";
            this.btnlive.Size = new System.Drawing.Size(167, 35);
            this.btnlive.TabIndex = 20;
            this.btnlive.Text = "Live";
            this.btnlive.UseVisualStyleBackColor = true;
            this.btnlive.Click += new System.EventHandler(this.btnlive_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 554);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 15);
            this.label7.TabIndex = 21;
            this.label7.Text = "Ver 2.1.0";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(736, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "X Angle(degree):";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(736, 306);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "Y Angle(degree):";
            // 
            // xpos
            // 
            this.xpos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xpos.Location = new System.Drawing.Point(736, 282);
            this.xpos.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.xpos.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            -2147483648});
            this.xpos.Name = "xpos";
            this.xpos.Size = new System.Drawing.Size(164, 21);
            this.xpos.TabIndex = 26;
            // 
            // ypos
            // 
            this.ypos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ypos.Location = new System.Drawing.Point(736, 324);
            this.ypos.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.ypos.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            -2147483648});
            this.ypos.Name = "ypos";
            this.ypos.Size = new System.Drawing.Size(164, 21);
            this.ypos.TabIndex = 27;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(41, 136);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(416, 405);
            this.hWindowControl1.TabIndex = 28;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(416, 405);
            // 
            // btnROI
            // 
            this.btnROI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnROI.Location = new System.Drawing.Point(908, 455);
            this.btnROI.Name = "btnROI";
            this.btnROI.Size = new System.Drawing.Size(62, 39);
            this.btnROI.TabIndex = 29;
            this.btnROI.Text = "Set ROI";
            this.btnROI.UseVisualStyleBackColor = true;
            this.btnROI.Click += new System.EventHandler(this.btnROI_Click);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.Blue;
            this.lblTime.Location = new System.Drawing.Point(922, 111);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(48, 16);
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
            this.listView1.Location = new System.Drawing.Point(463, 136);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(264, 405);
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
            // 
            // Value
            // 
            this.Value.Text = "Value";
            // 
            // Status
            // 
            this.Status.Text = "Status";
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
            this.checkboxScan.Location = new System.Drawing.Point(912, 154);
            this.checkboxScan.Name = "checkboxScan";
            this.checkboxScan.Size = new System.Drawing.Size(54, 16);
            this.checkboxScan.TabIndex = 32;
            this.checkboxScan.Text = "Scan?";
            this.checkboxScan.UseVisualStyleBackColor = true;
            // 
            // ExposureTime
            // 
            this.ExposureTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExposureTime.Location = new System.Drawing.Point(736, 363);
            this.ExposureTime.Name = "ExposureTime";
            this.ExposureTime.Size = new System.Drawing.Size(84, 21);
            this.ExposureTime.TabIndex = 33;
            this.ExposureTime.Text = "350";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(736, 348);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "Exposure(us):";
            // 
            // btDefaultSN
            // 
            this.btDefaultSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDefaultSN.Location = new System.Drawing.Point(908, 136);
            this.btDefaultSN.Name = "btDefaultSN";
            this.btDefaultSN.Size = new System.Drawing.Size(59, 19);
            this.btDefaultSN.TabIndex = 35;
            this.btDefaultSN.Text = "default";
            this.btDefaultSN.UseVisualStyleBackColor = true;
            this.btDefaultSN.Click += new System.EventHandler(this.btDefaultSN_Click);
            // 
            // Tb_graymax
            // 
            this.Tb_graymax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Tb_graymax.Location = new System.Drawing.Point(826, 363);
            this.Tb_graymax.Name = "Tb_graymax";
            this.Tb_graymax.Size = new System.Drawing.Size(74, 21);
            this.Tb_graymax.TabIndex = 36;
            // 
            // MaxGrayvalue
            // 
            this.MaxGrayvalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxGrayvalue.AutoSize = true;
            this.MaxGrayvalue.Location = new System.Drawing.Point(827, 348);
            this.MaxGrayvalue.Name = "MaxGrayvalue";
            this.MaxGrayvalue.Size = new System.Drawing.Size(53, 12);
            this.MaxGrayvalue.TabIndex = 37;
            this.MaxGrayvalue.Text = "MaxGray:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 572);
            this.Controls.Add(this.MaxGrayvalue);
            this.Controls.Add(this.Tb_graymax);
            this.Controls.Add(this.btDefaultSN);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ExposureTime);
            this.Controls.Add(this.checkboxScan);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnROI);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.ypos);
            this.Controls.Add(this.xpos);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnlive);
            this.Controls.Add(this.btnsnap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTECTemp);
            this.Controls.Add(this.btnTECTemp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.comboCamera);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRealTimeImage);
            this.Controls.Add(this.comboProduct);
            this.Controls.Add(this.btnReadimage);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ypos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadimage;
        private System.Windows.Forms.ComboBox comboProduct;
        private System.Windows.Forms.Button btnRealTimeImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboCamera;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tCPSettingToolStripMenuItem;
        private System.Windows.Forms.Button btnTECTemp;
        private System.Windows.Forms.TextBox txtTECTemp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnsnap;
        private System.Windows.Forms.Button btnlive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown xpos;
        private System.Windows.Forms.NumericUpDown ypos;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button btnROI;
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
    }
}

