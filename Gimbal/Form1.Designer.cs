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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ypos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReadimage
            // 
            this.btnReadimage.Location = new System.Drawing.Point(924, 399);
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
            // 
            // btnRealTimeImage
            // 
            this.btnRealTimeImage.Location = new System.Drawing.Point(1128, 465);
            this.btnRealTimeImage.Name = "btnRealTimeImage";
            this.btnRealTimeImage.Size = new System.Drawing.Size(164, 34);
            this.btnRealTimeImage.TabIndex = 6;
            this.btnRealTimeImage.Text = "Live";
            this.btnRealTimeImage.UseVisualStyleBackColor = true;
           // this.btnRealTimeImage.Click += new System.EventHandler(this.btnRealTimeImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1128, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(751, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Camera";
            // 
            // comboCamera
            // 
            this.comboCamera.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCamera.FormattingEnabled = true;
            this.comboCamera.Items.AddRange(new object[] {
            "Baumer",
            "AVT"});
            this.comboCamera.Location = new System.Drawing.Point(751, 241);
            this.comboCamera.Name = "comboCamera";
            this.comboCamera.Size = new System.Drawing.Size(164, 23);
            this.comboCamera.TabIndex = 9;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(751, 151);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(164, 21);
            this.txtBarcode.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(764, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Barcode";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(987, 84);
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
            this.menuStrip1.Size = new System.Drawing.Size(998, 25);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tCPSettingToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(77, 21);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // tCPSettingToolStripMenuItem
            // 
            this.tCPSettingToolStripMenuItem.Name = "tCPSettingToolStripMenuItem";
            this.tCPSettingToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.tCPSettingToolStripMenuItem.Text = "TCP Setting";
            this.tCPSettingToolStripMenuItem.Click += new System.EventHandler(this.tCPSettingToolStripMenuItem_Click);
            // 
            // btnTECTemp
            // 
            this.btnTECTemp.Location = new System.Drawing.Point(751, 399);
            this.btnTECTemp.Name = "btnTECTemp";
            this.btnTECTemp.Size = new System.Drawing.Size(167, 35);
            this.btnTECTemp.TabIndex = 15;
            this.btnTECTemp.Text = "Read TEC Temp";
            this.btnTECTemp.UseVisualStyleBackColor = true;
            this.btnTECTemp.Click += new System.EventHandler(this.btnTECTemp_Click);
            // 
            // txtTECTemp
            // 
            this.txtTECTemp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTECTemp.Location = new System.Drawing.Point(753, 195);
            this.txtTECTemp.Name = "txtTECTemp";
            this.txtTECTemp.Size = new System.Drawing.Size(162, 21);
            this.txtTECTemp.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(751, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "TEC(℃)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(751, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "BarCode";
            // 
            // btnsnap
            // 
            this.btnsnap.Location = new System.Drawing.Point(751, 455);
            this.btnsnap.Name = "btnsnap";
            this.btnsnap.Size = new System.Drawing.Size(167, 35);
            this.btnsnap.TabIndex = 19;
            this.btnsnap.Text = "Snap";
            this.btnsnap.UseVisualStyleBackColor = true;
            this.btnsnap.Click += new System.EventHandler(this.btnsnap_Click);
            // 
            // btnlive
            // 
            this.btnlive.Location = new System.Drawing.Point(751, 506);
            this.btnlive.Name = "btnlive";
            this.btnlive.Size = new System.Drawing.Size(167, 35);
            this.btnlive.TabIndex = 20;
            this.btnlive.Text = "Live";
            this.btnlive.UseVisualStyleBackColor = true;
            this.btnlive.Click += new System.EventHandler(this.btnlive_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 554);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 15);
            this.label7.TabIndex = 21;
            this.label7.Text = "Ver 1.0.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(751, 279);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 15);
            this.label8.TabIndex = 24;
            this.label8.Text = "X Position:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(749, 331);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "Y Position:";
            // 
            // xpos
            // 
            this.xpos.Location = new System.Drawing.Point(751, 297);
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
            this.ypos.Location = new System.Drawing.Point(751, 349);
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
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(41, 136);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(640, 405);
            this.hWindowControl1.TabIndex = 28;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(640, 405);
            // 
            // btnROI
            // 
            this.btnROI.Location = new System.Drawing.Point(924, 455);
            this.btnROI.Name = "btnROI";
            this.btnROI.Size = new System.Drawing.Size(62, 39);
            this.btnROI.TabIndex = 29;
            this.btnROI.Text = "Set ROI";
            this.btnROI.UseVisualStyleBackColor = true;
            this.btnROI.Click += new System.EventHandler(this.btnROI_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 572);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
    }
}

