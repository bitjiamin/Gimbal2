using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

using System.Runtime.InteropServices;
using HalconDotNet;
using System.IO;
using System.Diagnostics;

using System.Text.RegularExpressions;
using System.Collections;

namespace Gimbal
{
    public partial class Form1 : Form
    {

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_OPEN(string ip, int port, int timeout);


        [DllImport("GimbalDll.dll")]
        extern static int MTCP_TSCR(string dutsn, string socketsn, string sw, int mode, int slotid, int station, float dcr);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_INIT(string sw, string tn, string lot, int v, uint lotsize, string diffuserlotid, string username);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_ALPH(string[] path, ushort img_cnt, ushort width, ushort height, uint size, float dcr, string i_dr, string v_for);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_VDCR(float[] rsp);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_ALPR(float[] rsp);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_POST();

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_TSED();

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_CLOSE();

        [DllImport("GimbalDll.dll")]
        public extern static void MTCP_GENL(string path, int direction);

        int ItemIndex = 0;
        DriverBoard __DriverBoard = new DriverBoard();
        Stopwatch stopwatch = new Stopwatch();
        Stopwatch sensorjudge = new Stopwatch();
        TxtHelper txtHelper = new TxtHelper();
        private string folderName = string.Empty;
        private SerialPort commcu = new SerialPort();
        private SerialPort comtec = new SerialPort();
        AvtCam Avt = new AvtCam();
        Serial Com = new Serial();
        ImageProcess pro = new ImageProcess();
        ScannerCommunications TCPScanner = new ScannerCommunications();
        TCPClient TcpPLC = new TCPClient();
        SMU TcpSMUReset = new SMU();
        SMU TcpSMUProcess = new SMU();
        BaumerSDK BM = new BaumerSDK();
        ProcessTif tif = new ProcessTif();
        Operxml file = new Operxml();
        HObject ho_Image;
        string basepath = System.IO.Directory.GetCurrentDirectory();
        Thread live;
        Thread Process, TecP;
        bool livestate = false;
        bool exit = false;
        bool anglejudge;
        string barcode = null;
        Int32 X;   //X轴旋转角度
        Int32 Y;   //Y轴旋转角度
        String I;  //SMU电流值
        Int32 T;   //等待TEC稳定时间
        string voltage;
        string current;
        string voltagePeaks;
        string currentPeaks;
        int ControlChoose;
        int Controltime;
        int SmuTwice;
        double meani;  //smu回采电流平均值
        double meanv;  //smu回采电压平均值
        string SourceCom = "com8";
        string TecCom = "com5";
        string openTEC = @"$W" + "\r\n";
        string closeTEC = @"$Q" + "\r\n";
        string TECTemp = @"$R101?" + "\r\n";
        public static string UserName = "Admin";
        public static string Password = "123456";
        public static string enterUserName;
        public static string enterPassword;
        public static string enterdiffuserlotID;
        float dcrRead;
        public static int total = 0;
        public static int pass = 0;
        public static int fail = 0;
        public static double passpercent = 0;
        public static double failpercent = 0;
        public static bool t=false;
        public static bool NewLotBool = false;
        public static string DeviceName="B800";
        public static int StepNameIndex=0;
        public static int TestModeIndex=0;
        public static string LotID="";
        public static string DiffusionLotID="";
        public static decimal LotSize=0;
        public static string OperatorName="";
        public static string TesterID="TIDI002";
        DateTime beginT;
        DateTime loadT;
        DateTime scanT;
        DateTime cylinT;
        DateTime tecT;
        DateTime enableT;
        DateTime firstT;
        DateTime rotateT;
        DateTime secondT;
        DateTime rotateT2;
        DateTime thirdT;
        DateTime lastT;

        CSV_File csvcontrol = new CSV_File();
        DataTable st = new DataTable("Table_AX");
        DataTable et = new DataTable("Table_AX");

        string __pathLogDir_1 = "";
        string __pathLogDir="";
        string __pathLogDir_2 = "";
        string __pathLot="";
        string exposureTimeAbs = "";

        public Form1()
        {
            InitializeComponent();
            Program.FrmStartShow.Close();

                LoginForm loginform = new LoginForm();
                loginform.ShowDialog();
            while(true)
            {
                if(!t)
                this.Close();
                else
                {
                  //  if (enterdiffuserlotID == "")
                    if(false)                               //客户不想登入的时候输入ID号
                    {
                        MessageBox.Show("Please enter Slot ID");
                        t = false;
                        loginform.ShowDialog();
                    }
                    else
                    {
                      //  ID.Text = enterdiffuserlotID;
                        labelUserName.Text = enterUserName;
                        //  Diffuser_lotid = Convert.ToInt32(enterdiffuserlotID);
                        if (Password == enterPassword && UserName == enterUserName)
                        {
                            txtBarcode.Enabled = true;
                            xpos.Enabled = true;
                            ypos.Enabled = true;
                            ExposureTime.Enabled = true;
                            CurrentDefault.Enabled = true;
                            ManualPosition.Enabled = true;
                            LabelLoginMode.Text = "Administrator";
                        }
                        else
                        {
                            LabelLoginMode.Text = "Operator";
                        }

                        string DCRpath = @"d:\\vault\\intelli_Gimbal\\DCRdata.txt";
                        string DCR = "0";
                        if (File.Exists(DCRpath))
                        {
                            DCR = System.IO.File.ReadAllText(DCRpath);
                        }
                        //var dcr = Convert.ToUInt64(DCR, 16);
                        //   dcrRead = dcr / 1000000;
                        dcrRead = ((float)Convert.ToDouble(DCR)) * 1000;
                        //btnsnap.Enabled = true;
                        //btnlive.Text = "Live";

                        string IPdb = file.ReadXmlFile("DriverBoard", "IP");
                        string Portdb = file.ReadXmlFile("DriverBoard", "Port");
                        __DriverBoard.Connect(IPdb, Portdb);

                        Control.CheckForIllegalCrossThreadCalls = false;
                        comboProduct.SelectedIndex = 0;
                        BM.Initialize();
                        BM.Play();

                        Com.serial_open(comtec, TecCom, 115200);

                        string IP = file.ReadXmlFile("TCPconfig", "IP");
                        string Port = file.ReadXmlFile("TCPconfig", "Port");

                        TcpPLC.tcpconnect(IP, Port);

                        //string IPScanner = "169.254.1.99";
                        //string PortScanner = "49211";
                        string IPScanner = file.ReadXmlFile("Scannerconfig", "IP");
                        string PortScanner = file.ReadXmlFile("Scannerconfig", "Port");
                        TCPScanner.Connect(IPScanner, PortScanner);

                        string IPSMU = file.ReadXmlFile("TcpSMUReset", "IP");
                        string PortSMU = file.ReadXmlFile("TcpSMUReset", "Port");
                        TcpSMUReset.Connect(IPSMU, PortSMU);
                        Thread.Sleep(500);
                        TcpSMUReset.PowerOff();

                        string IPSMU1 = file.ReadXmlFile("TcpSMUProcess", "IP");
                        string PortSMU1 = file.ReadXmlFile("TcpSMUProcess", "Port");
                        TcpSMUProcess.Connect(IPSMU1, PortSMU1);

                        __pathLogDir_1 = file.ReadXmlFile("Filepath", "path");
                        exposureTimeAbs = file.ReadXmlFile("Exposetime", "time");


                        Process = new Thread(new ThreadStart(Processthread));
                        Process.IsBackground = true;
                        Process.Start();

                        TecP = new Thread(new ThreadStart(ReadTecTemp));
                        TecP.IsBackground = true;
                        TecP.Start();

                        this.cleartotal.Enabled = false;
                        //添加列
                        st.Columns.Add("Cloum0", typeof(string));
                        st.Columns.Add("Cloum1", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum2", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum3", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum4", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum5", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum6", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum7", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum8", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum9", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum10", System.Type.GetType("System.String"));
                        st.Columns.Add("Cloum11", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum0", typeof(string));
                        et.Columns.Add("Cloum1", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum2", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum3", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum4", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum5", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum6", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum7", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum8", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum9", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum10", System.Type.GetType("System.String"));
                        et.Columns.Add("Cloum11", System.Type.GetType("System.String"));
                        DataRow dr = st.NewRow();
                        DataRow de = et.NewRow();

                        //添加行
                        for (int i = 0; i < 9; i++)
                        {
                            st.Rows.Add();
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            et.Rows.Add();
                        }

                        break;
                    }
                }
            }
             
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            TcpSMUProcess.resultdata = null;
            string text = System.IO.File.ReadAllText(Application.StartupPath + "\\pulseinterface.txt");
            TcpSMUProcess.PowerOn(text);
            this.Total.Text = "0";
            this.Pass.Text = "0";
            this.Fail.Text = "0";
            this.PassPercent.Text = "0";
            this.FailPercent.Text = "0";

            this.VERSION.Text = "4.2.7";

          //  double exposureTimeAbsPosition0 = Convert.ToInt32(ExposureTime.Text);//500000;
            double exposureTimeAbsPosition0 = Convert.ToInt32(exposureTimeAbs);
            Avt.Open_TriggerPositionZero(exposureTimeAbsPosition0);
            ExposureTime.Text = exposureTimeAbs;
            //Add item
            AddRow("Scan Barcode");
            AddRow("MTCP Current");
            AddRow("MTCP Steady Time");
            AddRow("MTCP Position X");
            AddRow("MTCP Position Y");
            AddRow("SMU Power On");
            AddRow("Zero Position Image Capture");
            AddRow("Zero Position SMU Current");
            AddRow("Zero Position SMU Voltage");
            AddRow("SMU Power On");
            AddRow("Target Position1 Image Capture");
            AddRow("Target Position1 SMU Current");
            AddRow("Target Position1 SMU Voltage");
            AddRow("MTCP Position X_2");
            AddRow("MTCP Position Y_2");
            AddRow("SMU Power On");
            AddRow("Target Position2 Image Capture");
            AddRow("Target Position2 SMU Current");
            AddRow("Target Position2 SMU Voltage");
            AddRow("MTCP Post");
           // TestResult.Text = "Pass";
            TestResult.Text = "TEST";
            TestResult.BackColor = Color.Silver;
            Thread.Sleep(2000);
            var receive = TcpSMUProcess.resultdata;
        }

        void ResetListView()
        {
            foreach (ListViewItem lvi in listView1.Items)  //选中项遍历 
            {
                lvi.SubItems[2].Text = " ";
                lvi.SubItems[3].Text = " ";
            }
            ItemIndex = 0;
        }

        private Decimal ChangeDataToD(string strData)
        {
            Decimal dData = 0.0M;
            if (strData.Contains("e") || strData.Contains("E"))
            {
                dData = Convert.ToDecimal(Decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
            }
            return dData;
        }

        private string StrToStr(string oriData)
        {
            Decimal endVal=ChangeDataToD(oriData);
            return endVal.ToString("f2");
        }

        void AddValueAndStatus(int rowIndex, int colIndex, string value)
        {
            listView1.BeginUpdate();

            ListViewItem item = listView1.Items[rowIndex];

            item.SubItems[colIndex].Text = value;

            listView1.EndUpdate();

        }

        void AddRow(string name)
        {
            listView1.BeginUpdate();

            ListViewItem lvi = new ListViewItem();

            lvi.Text = ItemIndex.ToString();
            lvi.SubItems.Add(name);
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");

            this.listView1.Items.Add(lvi);

            listView1.EndUpdate();

            ItemIndex++;
        }
        void Log(string msg)
        {
            Console.WriteLine("################:" + msg);
            SaveLog(msg);
        }

        void InitialTest()
        {
         //   Log("Initial Testing...");

            if (checkboxScan.Checked)
            {
                txtBarcode.Text = "";
            }

            stopwatch.Reset();
            sensorjudge.Reset();
            ResetHWindow();
            ResetListView();
        }

        void ResetHWindow()
        {
            HTuple hv_Window = hWindowControl1.HalconWindow;
            HObject ho_image;
            HOperatorSet.GenEmptyObj(out ho_image);
            HOperatorSet.DispObj(ho_image, hv_Window);
        }

        void PictureNameByBarcodeAndXY(string time, ref string picname, ref string Sx, ref string Sy, bool isblack)
        {
            float x, y;
            x = (float)X / 1000;
            y = (float)Y / 1000;
            Sx = x.ToString();
            Sy = y.ToString();
            string cutbarcode;
            cutbarcode = barcode.Replace("\r", "").Replace("\n", "");
            if (isblack == true)
            {
                picname = cutbarcode + " X" + Sx + " Y" + Sy + " " + time + "_" + "Black";
            }
            else
            {
                picname = cutbarcode + " X" + Sx + " Y" + Sy + " " + time +"_"+"Light";
            }
        }

        void HalconWriteImage(HObject ho_image, string imagefile)
        {
            HTuple width, height;
            HTuple hv_Window = hWindowControl1.HalconWindow;
            HOperatorSet.WriteImage(ho_image, "tiff", 0, imagefile);
            HOperatorSet.GetImageSize(ho_image, out width, out height);
            HOperatorSet.SetPart(hv_Window, 0, 0, height - 1, width - 1);
            HOperatorSet.DispObj(ho_image, hv_Window);
        }
        void CaptureBlackImage(string time, ref string imagefile)
        {
            string CaptureBlackstatus = "failed";
            try
            {
                Log("Black image capturing...");
                
                HObject ho_image;
                HOperatorSet.GenEmptyObj(out ho_image);
                string PicName = null;
                string Sx = null;
                string Sy = null;
                Avt.OneShot(ref ho_image);
                
                PictureNameByBarcodeAndXY(time, ref PicName, ref Sx, ref Sy, true);
                imagefile = __pathLogDir + PicName + ".tif";

                HalconWriteImage(ho_image, imagefile);
                CaptureBlackstatus = "pass";
                Log("Black image capture finished!\r\n"+"X="+Sx+"\r\n"+"Y=" +Sy+"\r\n");
                ho_image.Dispose();
            }
            catch (Exception ex)
            {
                CaptureBlackstatus = "failed";
            }
            AddValueAndStatus(5, 2, "");
            AddValueAndStatus(5, 3, CaptureBlackstatus);
        }

        void Baumer()
        {
            string result = "1";
            if (result == "1")
            {
                byte[] data = { 0x02, 0x00, 0xdd, 0x00, 0xdd, 0x00, 0x00, 0x00 };
                TcpPLC.sendbytes(data);

            }
            else if (result == "-1")
            {
                byte[] data = { 0x02, 0x00, 0xde, 0x00, 0xde, 0x00, 0x00, 0x00 };
                TcpPLC.sendbytes(data);
                MessageBox.Show("Product is not correct in DUT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void GetBarcode()
        {
            if (checkboxScan.Checked)
            {
                TCPScanner.Send("T");
                Thread.Sleep(150);
                barcode = TCPScanner.result();
            }
            else
            {
                barcode = "FWP638701S2H6CWC5";
                //barcode = txtBarcode.Text;
            }
        }

        void MTCPCommunication()
        {
            string statusBarcode = "pass";
            string statusMTCP = "failed";

            if (barcode.ToUpper() == "<ERROR>") //"ERROR"\r\n
            {
                statusBarcode = "failed";
                barcode = "ERROR";
                byte[] data = { 0x03, 0x00, 0xee, 0x00, 0xee, 0x00, 0x00, 0x00 };
                TcpPLC.sendbytes(data);
                MessageBox.Show("Barcode is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                stopwatch.Stop();
            }
            else
            {
                //与MTCP通讯
                statusBarcode = "pass";
                try
                {
                    string IP = file.ReadXmlFile("MTCP", "IP");
                    int Port = Convert.ToInt32(file.ReadXmlFile("MTCP", "Port"));
                    MTCP_OPEN(IP, Port, 5000);
                    //MTCP_OPEN("169.254.1.111", 61808, 5000);
                    string barcodecut;
                    barcodecut = barcode.Replace("\r", "").Replace("\n", "");
                    MTCP_TSCR(barcodecut, "123456", "1.0", 0, 1, 1, dcrRead);
                    MTCP_INIT("1.0", "TIDI", "Gimballot", 10, 002, LotID, enterUserName);
                    float[] vdcr_rsp = new float[2];
                    MTCP_VDCR(vdcr_rsp);
                    I = vdcr_rsp[0].ToString();      //SMU电流值
                    if (CurrentDefault.Checked == true)
                    {
                        I = "0.82";
                    }
                    T = (Int32)vdcr_rsp[1];          //等待TEC稳定时间
                    float[] alpr_rsp = new float[2];
                    MTCP_ALPR(alpr_rsp);
                    X = (Int32)(alpr_rsp[0] * 1000);    //X轴旋转角度
                    Y = -(Int32)(alpr_rsp[1] * 1000);    //Y轴旋转角度
                   // Y = -(Int32)(Math.Atan(Math.Tan(Y) * Math.Cos(X)) * 180*1000 / Math.PI);      //尹总公式
                    statusMTCP = "pass";

                }
                catch
                {
                    I = "1";
                    T = 5;
                    X = 0;
                    Y = 0;
                    statusMTCP = "failed";
                }

                string msg = string.Format("MTCP Response:T={0},I={1},X={2},Y={3}", T, I, X, Y);
                Log(msg);
                /*
                //与beckhoff通讯
                byte[] data = { 0x03, 0x00, 0xdd, 0x00, 0xdd, 0x00, 0x00, 0x00 };
                TcpPLC.sendbytes(data);
                 * */
            }
            MTCPStatus(statusBarcode, statusMTCP);
        }

        void MTCPStatus(string statusBarcode, string statusMTCP)
        {
            AddValueAndStatus(0, 2, barcode);
            AddValueAndStatus(0, 3, statusBarcode);
            AddValueAndStatus(1, 2, I);
            AddValueAndStatus(1, 3, statusMTCP);
            AddValueAndStatus(2, 2, T.ToString());
            AddValueAndStatus(2, 3, statusMTCP);
            AddValueAndStatus(3, 2, (X/1000.0).ToString());
            AddValueAndStatus(4, 2, (-Y/1000.0).ToString());
            if (X > 50000 || Y > 50000 || X < -50000 || Y < -50000)
            {
                AddValueAndStatus(3, 3, "fail");
                AddValueAndStatus(4, 3, "fail");
            }
            else
            {
                AddValueAndStatus(3, 3, statusMTCP);
                AddValueAndStatus(4, 3, statusMTCP);
            }
        }

        void XYPosition()
        {
            //send position x
            //Int32 X=002000;           //前3位是角度整数部分，后3位是角度小数部分
            if (ManualPosition.Checked == true)
            {
                X = GetPosition(xpos.Value);
            }
            byte[] arryX2 = new byte[4];
            ConvertIntToByteArray(X, ref arryX2);             //整数转字节数组
            byte[] arryX1 = { 0x20, 0x00, 0x00, 0x00 };        //定义第一个字节数组
            byte[] dataX = arryX1.Concat(arryX2).ToArray();  //连接两个字节数组
            // byte[] dataX = { 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };  //最终发送数据形式
            TcpPLC.sendbytes(dataX);

            //send position y
            //Int32 Y=002000;         //前3位是角度整数部分，后3位是角度小数部分
            if (ManualPosition.Checked == true)
            {
                Y = GetPosition(ypos.Value);
            }
            byte[] arryY2 = new byte[4];
            ConvertIntToByteArray(Y, ref arryY2);             //整数转字节数组
            byte[] arryY1 = { 0x20, 0x00, 0x01, 0x00 };        //定义第一个字节数组
            byte[] dataY = arryY1.Concat(arryY2).ToArray();  //连接两个字节数组
            // byte[] dataY = { 0x20, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };   //最终发送数据形式
            TcpPLC.sendbytes(dataY);
        }

        void EnableDUTYogi()
        {
            Log("Enable DUT Yogi...");
            __DriverBoard.PowerVDD();
            Thread.Sleep(10);
            __DriverBoard.InitYogi();
            Thread.Sleep(1700);
            __DriverBoard.BypassYogi();
            __DriverBoard.ReadYogi();
            __DriverBoard.SetYogiActiMode();
           // __DriverBoard.ReadYogiNVM();
            Log("Enable Yogi finished!");
        }

        void SendDataToMTCP(HTuple width, HTuple height, string picname, string pathblack)
        {
            string statusMTCPSendData = "failed";
            try
            {
                Log("Send data to MTCP...");
                uint size = (uint)(width * height);
                string[] path = { __pathLogDir + pathblack + ".tif" , __pathLogDir + picname + ".tif"};//{ pathimage + PicName + ".tif", pathblack };
                ushort img_cnt = 2;//2;
                float meaniI = (float)meani;
                float meanvV = (float)meanv;
                //int ret = MTCP_ALPH(path, img_cnt, (ushort)width, (ushort)height, 0, current, voltage);  //voltagePeaks
                int ret = MTCP_ALPH(path, img_cnt, (ushort)width, (ushort)height, 100, dcrRead,currentPeaks, voltagePeaks);
                Log(string.Format("ALPH Return Value : {0}", ret));
                Log("MTCP Send Completed!\r\n" + "meanI=" + meani + "\r\n" + "meanV=" + meanvV + "\r\n");
                statusMTCPSendData = "pass";
            }
            catch (Exception exp)
            {
                statusMTCPSendData = "failed";
                MessageBox.Show(exp.Message);
            }

            AddValueAndStatus(10, 3, statusMTCPSendData);
            AddValueAndStatus(11, 3, statusMTCPSendData);
           // AddValueAndStatus(17, 2, " ");
           // AddValueAndStatus(17, 3, "OK");
        }

        void SendDataToMTCP_2(HTuple width, HTuple height, string picname)
        {
            string statusMTCPSendData = "failed";
            try
            {
                Log("Send data to MTCP...");
                uint size = (uint)(width * height);
                string[] path = { __pathLogDir + picname + ".tif" };
                ushort img_cnt = 1;//2;
                float meaniI = (float)meani;
                float meanvV = (float)meanv;
                int ret = MTCP_ALPH(path, img_cnt, (ushort)width, (ushort)height, 100, dcrRead, currentPeaks, voltagePeaks);
                Log(string.Format("ALPH Return Value : {0}", ret));
                Log("MTCP Send Completed!\r\n" + "meanI=" + meani + "\r\n" + "meanV=" + meanvV + "\r\n");
                statusMTCPSendData = "pass";
            }
            catch (Exception exp)
            {
                statusMTCPSendData = "failed";
                MessageBox.Show(exp.Message);
            }

            AddValueAndStatus(10, 3, statusMTCPSendData);
            AddValueAndStatus(11, 3, statusMTCPSendData);
            AddValueAndStatus(19, 2, " ");
            AddValueAndStatus(19, 3, "OK");
        }
        private void Processthread()
        {
            string time1 = string.Empty;
            string picname_zero = null;
            while (true)
            {
                try
                {
                    switch (TcpPLC.result)
                    {
                        //产品上料防呆
                        case ("0100bb00bb000000"):
                            beginT = DateTime.Now;
                            TestResult.Text = "Testing...";
                            TestResult.BackColor = Color.Yellow;
                            InitialTest();
                            ControlChoose = 0;
                            Controltime = 0;
                            SmuTwice = 0;
                            anglejudge = true;
                            stopwatch.Start();
                            TcpPLC.result = null;
                            Baumer();
                         //   Log("Loading DUT" + TcpPLC.result);
                            break;
                        //产品扫条码
                        case ("0300bb00bb000000"):
                            loadT = DateTime.Now;
                        //    Log("Start to scan SN!");
                            TcpPLC.result = null;
                            Thread.Sleep(200);

                            GetBarcode();
                            scanT = DateTime.Now;
                            InitialLogPath(); //by barcode
                            Log("Initial Testing...");
                            Log("Loading DUT" + TcpPLC.result);
                            Log("Start to scan SN!");
                            MTCPCommunication();
                            txtBarcode.Text = barcode;
                            Log("Scan Finished!" +" "+"SN:"+ barcode.Replace("\r", "").Replace("\n", ""));
                            //判断是否有产品bin文件
                            if ( T == 0 && X == 0 && Y == 0 && checkboxScan.Checked)
                            {
                                //与beckhoff通讯
                                byte[] data = { 0x03, 0x00, 0xee, 0x00, 0xee, 0x00, 0x00, 0x00 };
                                TcpPLC.sendbytes(data);
                                MessageBox.Show("NoBinfileExist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                //与beckhoff通讯
                                byte[] data = { 0x03, 0x00, 0xdd, 0x00, 0xdd, 0x00, 0x00, 0x00 };
                                TcpPLC.sendbytes(data);
                            }
                            //判断读的角度是否大于50度
                            if (X > 50000 || Y > 50000 || X < -50000 || Y < -50000)
                            {
                                anglejudge = false;
                                X = 0;
                                Y = 0;
                            }
                            Com.serial_send(comtec, openTEC);
                            break;

                        //发送X、Y坐标
                        case ("1000dd00dd000000"):
                            TcpPLC.result = null; 
                            HTuple hv_Window_Zero = hWindowControl1.HalconWindow;
                            HObject ho_image_zero;
                            HTuple width_zero, height_zero;
                            HOperatorSet.GenEmptyObj(out ho_image_zero);
                            cylinT = DateTime.Now;
                            Thread.Sleep(T * 1000);   //TEC稳定时间
                            tecT = DateTime.Now;
#if true
                            EnableDUTYogi();
#endif
                                                       
                            Log("Turn on unit...");
                            enableT = DateTime.Now;
                           
                            Process = new Thread(new ThreadStart(OperateSMU));
                            Process.IsBackground = true;
                            Process.Start();

                            AddValueAndStatus(5, 2, "");
                            AddValueAndStatus(5, 3, "pass");
                            Avt.OneShotTrigger(out ho_image_zero, 10000);

                            time1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string Sx_zero = null;
                            string Sy_zero = null;

                            PictureNameByBarcodeAndXY(time1, ref picname_zero, ref Sx_zero, ref Sy_zero, false);
                            HalconWriteImage(ho_image_zero, __pathLogDir + picname_zero+"ZeroPosition");
                            HOperatorSet.GetImageSize(ho_image_zero, out width_zero, out height_zero);
                            ho_image_zero.Dispose();
                           
                            AddValueAndStatus(6, 2, "");
                            AddValueAndStatus(6, 3, "pass");

                            while (SmuTwice == 0)
                            {
                                Thread.Sleep(100);//等待smu第一个线程结束
                            }
                            firstT = DateTime.Now;
                            XYPosition();
                            sensorjudge.Start();
                            Log("Send x,y to PLC");
                            break;
                        //X坐标超出范围报警
                        case ("20000000ee000000"):
                            TcpPLC.result = null;
                            MessageBox.Show("X angle out of range", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        //Y坐标超出范围报警
                        case ("20000100ee000000"):
                            TcpPLC.result = null;
                            MessageBox.Show("Y angle out of range", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        //AVT相机采集数据
                        case ("2000dd00dd000000"):
                            if (ControlChoose==1)
                            {
                                sensorjudge.Stop();
                                rotateT = DateTime.Now;
                                Log("Start to capture...");
                                TcpPLC.result = null;

                                HTuple hv_Window = hWindowControl1.HalconWindow;
                                HObject ho_image;
                                HTuple width, height;
                                HOperatorSet.GenEmptyObj(out ho_image);

                                Log("Start to capture image until unit power on...");

                                Log("Turn on unit...");                          

                                Process = new Thread(new ThreadStart(OperateSMU));
                                Process.IsBackground = true;
                                Process.Start();

                                AddValueAndStatus(9, 2, "");
                                AddValueAndStatus(9, 3, "pass");
                   
                                Avt.OneShotTrigger(out ho_image, 10000);

                               // Tb_graymax.Text = pro.getmax(ho_image).ToString();

                                string Sx = null;
                                string Sy = null;
                                string picname = null;

                                PictureNameByBarcodeAndXY(time1, ref picname, ref Sx, ref Sy, false);
                                HalconWriteImage(ho_image, __pathLogDir + picname);
                                HOperatorSet.GetImageSize(ho_image, out width, out height);
                                ho_image.Dispose();
                                AddValueAndStatus(10, 2, "");
                                AddValueAndStatus(10, 3, "pass");

                                Log("Capture finished!...");
                                while (Controltime==0)
                                {
                                    Thread.Sleep(100);//等待smu计算平均电流和电压值2
                                }
                                secondT = DateTime.Now;
                                //与MTCP通讯
                                SendDataToMTCP(width, height, picname, picname_zero + "ZeroPosition");
                                float[] alpr_rsp = new float[2];
                                MTCP_ALPR(alpr_rsp);
                                X = (Int32)(alpr_rsp[0] * 1000);    //X轴旋转角度
                                Y = -(Int32)(alpr_rsp[1] * 1000);    //Y轴旋转角度
                                AddValueAndStatus(13, 2, (X / 1000.0).ToString());

                                AddValueAndStatus(14, 2, (-Y / 1000.0).ToString());

                                if (X > 50000 || Y > 50000 || X < -50000 || Y < -50000)
                                {
                                    anglejudge = false;
                                    X = 0;
                                    Y = 0;
                                    AddValueAndStatus(13, 3, "pass");
                                    AddValueAndStatus(14, 3, "pass");
                                }
                                else
                                {
                                    AddValueAndStatus(13, 3, "pass");
                                    AddValueAndStatus(14, 3, "pass");
                                }
                                XYPosition();
                                Log("Send x,y to PLC");
                            }
                          else
                            {
                                rotateT2 = DateTime.Now;
                                Log("Start to capture...");
                                TcpPLC.result = null;

                                HTuple hv_Window = hWindowControl1.HalconWindow;
                                HObject ho_image;
                                HTuple width, height;
                                HOperatorSet.GenEmptyObj(out ho_image);

                                Log("Start to capture image until unit power on...");

                                Log("Turn on unit...");

                                Process = new Thread(new ThreadStart(OperateSMU));
                                Process.IsBackground = true;
                                Process.Start();

                                AddValueAndStatus(15, 2, "");
                                AddValueAndStatus(15, 3, "pass");

                                Avt.OneShotTrigger(out ho_image, 10000);

                                // Tb_graymax.Text = pro.getmax(ho_image).ToString();
                                time1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                                string Sx = null;
                                string Sy = null;
                                string picname = null;

                                PictureNameByBarcodeAndXY(time1, ref picname, ref Sx, ref Sy, false);
                                HalconWriteImage(ho_image, __pathLogDir + picname);
                                HOperatorSet.GetImageSize(ho_image, out width, out height);
                                ho_image.Dispose();
                                AddValueAndStatus(16, 2, "");
                                AddValueAndStatus(16, 3, "pass");

                                Log("Capture finished!...");
                                while (Controltime == 1)
                                {
                                    Thread.Sleep(100);//等待smu计算平均电流和电压值3
                                }
                                thirdT = DateTime.Now;
                                //与MTCP通讯
                                SendDataToMTCP_2(width, height, picname);
                                //与Beckhoff通讯
                                byte[] dataState = { 0x30, 0x00, 0xdd, 0x00, 0xdd, 0x00, 0x00, 0x00 };
                                TcpPLC.sendbytes(dataState);
                                total = total + 1;
                                this.Total.Text = total.ToString();
                                if (MTCP_POST() == 0)
                                {
                                    if (anglejudge)
                                    {
                                        TestResult.Text = "Pass";
                                        TestResult.BackColor = Color.Lime;
                                    }
                                    else
                                    {
                                        TestResult.Text = "Fail";
                                        TestResult.BackColor = Color.AliceBlue;
                                    }
                                    pass = pass + 1;
                                    this.Pass.Text=pass.ToString();
                                }
                                else
                                {
                                    TestResult.Text = "Fail";
                                    TestResult.BackColor = Color.Red;
                                }
                                fail = total - pass;
                                this.Fail.Text = fail.ToString();
                                passpercent = (pass / (total*1.0))*100;
                                this.PassPercent.Text = passpercent.ToString("0.00");
                                failpercent =((total - pass) / (total*1.0))*100;
                                this.FailPercent.Text = failpercent.ToString("0.00");
                                       MTCP_TSED();
                                       MTCP_CLOSE();
                                lastT = DateTime.Now;
                                Log("time   stamp:start  "+beginT.ToString("yyyy-MM-dd HH:mm:ss.fff") );
                                Log("load    time:  " + loadT.Subtract(beginT).TotalSeconds.ToString("f2"));
                                Log("scan    time:  " + scanT.Subtract(loadT).TotalSeconds.ToString("f2"));
                                Log("cylin   time:  " + cylinT.Subtract(scanT).TotalSeconds.ToString("f2"));
                                Log("tec     time:  " + tecT.Subtract(cylinT).TotalSeconds.ToString("f2"));
                                Log("enable  time:  " + enableT.Subtract(tecT).TotalSeconds.ToString("f2"));
                                Log("first   time:  " + firstT.Subtract(enableT).TotalSeconds.ToString("f2"));
                                Log("rotate  time:  " + rotateT.Subtract(firstT).TotalSeconds.ToString("f2"));
                                Log("second  time:  " + secondT.Subtract(rotateT).TotalSeconds.ToString("f2"));
                                Log("rotate2 time:  " + rotateT2.Subtract(secondT).TotalSeconds.ToString("f2"));
                                Log("third   time:  " + thirdT.Subtract(rotateT2).TotalSeconds.ToString("f2"));
                                Log("cylout  time:  " + lastT.Subtract(thirdT).TotalSeconds.ToString("f2"));
                                Log("total   time:  " + lastT.Subtract(beginT).TotalSeconds.ToString("f2"));
                                Log("time   stamp:end  " + lastT.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                //更改文件夹名称
                                movefolder(__pathLogDir, __pathLogDir_2);
                                //删除本地图片文件夹
                                if (SaveImage.Checked == false)
                                {
                                    if (Directory.Exists(__pathLogDir_2))
                                    {
                                        Directory.Delete(__pathLogDir_2, true);
                                    }
                                }

                                Com.serial_send(comtec, closeTEC);
                                stopwatch.Stop();
                            }
                            break;
                        //产品下料
                        case ("4000dd00dd000000"):

                            TcpPLC.result = null;
                            __DriverBoard.Reset();
                            TcpSMUReset.PowerOff();
                            stopwatch.Stop();

                            break;
                        //机器报错
                        case ("ee00ee00ee000000"):
                            sensorjudge.Stop();
                            __DriverBoard.Reset();
                            TcpSMUReset.PowerOff();
                            stopwatch.Stop();
                            TcpPLC.result = null;
                            double t = (double)stopwatch.ElapsedMilliseconds / 1000.0;
                            lblTime.Text = t.ToString("f3");
                            MessageBox.Show("Machine Error", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        default:
                            break;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                if (sensorjudge.ElapsedMilliseconds > 10000)
                {
                    sensorjudge.Stop();
                    sensorjudge.Reset();
                    MessageBox.Show("Can't get the signal from PLC,Please cheack the sensor of cylinder.", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
      
        void ParseMCUResponse(string vi, out string voltage, out string current, out string voltagepeaks, out string currentpeaks)
        {
            string match_current = "Current:(.*)Voltage";
            string match_volt = "Voltage:(.*)Time";
            string match_currentPeaks = "CurrentPeaks:(.*)VoltPeaks";
            string match_voltPeaks = "VoltPeaks:(.*)PeaksEnd";
            //current
            current = RegexData(vi, match_current);
            currentpeaks = RegexData(vi, match_currentPeaks);
            //voltage
            voltage = RegexData(vi, match_volt);
            voltagepeaks = RegexData(vi, match_voltPeaks);
             
            Log(vi+"\r\n");
        }

        string RegexData(string vi, string match_current)
        {
            string current;
            Regex regexCurrent = new Regex(match_current, RegexOptions.Singleline);
            Match mc = regexCurrent.Match(vi);
            string vc = mc.Groups[1].ToString();
            vc = vc.Trim();
            current = vc;
            return current;
        }


        void SMU_PowerOff()
        {
            Com.serial_open(commcu, SourceCom, 9600);  //打开源表
            Com.serial_send(commcu, "*RST\r\n");
            Com.serial_send(commcu, "abort\r\n");
            Com.serial_send(commcu, "smua.reset()\r\n");
            Com.serial_send(commcu, "smub.reset()\r\n");

            string vi = Com.serial_readmcu(commcu);
            Console.WriteLine("~~~~~~~~~~~~~~~SMU Response:");
            Console.WriteLine(vi);


            Com.serial_close(commcu);
        }
        private void OperateSMU()
        {
            TcpSMUProcess.resultdata = null;
            string para_light_up = "abort\r\n" + "smua.reset()\r\n" + "pulse_current_N(10,0.009,4.5," + I + ",0.001,0.001,2000,0.001,1,1,0)\r\n";
            TcpSMUProcess.PowerOn(para_light_up);
            Thread.Sleep(4200);
            var vi = TcpSMUProcess.resultdata;
            TcpSMUProcess.resultdata = null;

            Console.WriteLine("~~~~~~~~~~~~~~~SMU Response:");
            Console.WriteLine(vi);

            ParseMCUResponse(vi, out voltage, out current, out voltagePeaks, out currentPeaks);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            
             if (ControlChoose == 0)
            {
                AddValueAndStatus(7, 2,StrToStr(current));
                AddValueAndStatus(7, 3, "pass");
                AddValueAndStatus(8, 2, StrToStr(voltage));
                AddValueAndStatus(8, 3, "pass");
                ControlChoose++;
                SmuTwice = 1;
            }
             else if (ControlChoose == 1)
            {
                AddValueAndStatus(11, 2, StrToStr(current));
                AddValueAndStatus(11, 3, "pass");
                AddValueAndStatus(12, 2, StrToStr(voltage));
                AddValueAndStatus(12, 3, "pass");
                ControlChoose++;
                Controltime = 1;
            }
            else
             {
                 AddValueAndStatus(17, 2, StrToStr(current));
                 AddValueAndStatus(17, 3, "pass");
                 AddValueAndStatus(18, 2, StrToStr(voltage));
                 AddValueAndStatus(18, 3, "pass");
                 Controltime = 2;
             }

             Log(para_light_up);
        }

        private Int32 GetPosition(decimal pos)
        {
            double p = (double)pos;
            string s_p = p.ToString("f3");
            p = Convert.ToDouble(s_p)*1000;
            Int32 result = (Int32)p;
            return result;
        }


        private void btnReadimage_Click(object sender, EventArgs e)
        {
            HTuple hv_window = hWindowControl1.HalconWindow;
            string result = action_Diffuser(hv_window);
        }

  
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Dispose();
            txtTECTemp.Dispose();
       
            TecP.Abort();
            Avt.Close();
            if (Process.IsAlive)
            {
                Process.Abort();
            }
            Com.serial_close(comtec);
            exit = true;
            BM.Exit();
            
        }

        private void livethread()
        {
            HTuple hv_Subsampling = new HTuple(), hv_Sharpness = new HTuple();
            hv_Subsampling = 3;
            if(true)
            {
                HTuple hv_window = hWindowControl1.HalconWindow;
                HTuple hv_WidthWin, hv_HeightWin;
                while(livestate)
                {
                    BM.SwTrigger();
                    Thread.Sleep(100);
                    int n = 0;
                    while (true)
                    {
                        if (n >= 500)
                            break;
                        else if (BM.j)
                        {
                            break;
                        }
                        n++;
                        Thread.Sleep(10);
                    }
                    HOperatorSet.GetImageSize(BM.ho_image, out hv_WidthWin, out hv_HeightWin);
                    HOperatorSet.SetPart(hv_window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
                    HOperatorSet.DispObj(BM.ho_image, hv_window);
                    try
                    {
                        pro.action_calculate_auto_correlation(BM.ho_image, hv_Subsampling, out hv_Sharpness);
                        pro.disp_message(hv_window, hv_Sharpness, "window", 12, 12, "black", "true");
                    }
                    catch
                    { }
                }
            }
            else
            {
                HObject ho_image;
                HTuple hv_window = hWindowControl1.HalconWindow;
                HTuple hv_WidthWin, hv_HeightWin;
                bool AVTOpen;
                if (livestate)
                {
                    Avt.Open_TriggerPositionZero(1000);
                    AVTOpen = true;
                }
                else
                    AVTOpen = false;
                while(livestate)
                {
                    HOperatorSet.GenEmptyObj(out ho_image);
                    Avt.OneShot(ref ho_image);
                    HOperatorSet.GetImageSize(ho_image, out hv_WidthWin, out hv_HeightWin);
                    HOperatorSet.SetPart(hv_window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
                    HOperatorSet.DispObj(ho_image, hv_window);
                    try
                    {
                        pro.action_calculate_auto_correlation(ho_image, hv_Subsampling, out hv_Sharpness);
                        pro.disp_message(hv_window, hv_Sharpness, "window", 12, 12, "black", "true");
                    }
                    catch
                    { }
                }
                if(AVTOpen)
                    Avt.Close();
            }
            //实时拍照结束后使能按钮
            //btnsnap.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tCPSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tcpsetting T_form = new Tcpsetting();
            T_form.Show();
        }

        static bool ConvertIntToByteArray(Int32 m, ref byte[] arry)
        {
            if (arry == null) return false;
            if (arry.Length < 4) return false;

            arry[0] = (byte)(m & 0xFF);
            arry[1] = (byte)((m & 0xFF00) >> 8);
            arry[2] = (byte)((m & 0xFF0000) >> 16);
            arry[3] = (byte)((m >> 24) & 0xFF);

            return true;
        }

        private void ReadTecTemp()
        {
            while(!exit)
            {
                try
                {
                    //Com.serial_open(comtec, TecCom, 115200);  //读取TEC温度
                    Com.serial_send(comtec, TECTemp);
                    Thread.Sleep(500);
                    string temp = Com.serial_read(comtec);
                    temp = temp.Substring(9, 5);
                    double t = Convert.ToDouble(temp) * 10;

                    txtTECTemp.Text = t.ToString();
                    //Com.serial_close(comtec);
                }
                catch
                {
                    txtTECTemp.Text = "0";
                }
                Thread.Sleep(500);
            }   
        }

        private void btnsnap_Click(object sender, EventArgs e)
        {
            if (true)
            {
                HObject ho_image;
                HTuple hv_WidthWin, hv_HeightWin;
                HOperatorSet.GenEmptyObj(out ho_image);
                HTuple hv_Window = hWindowControl1.HalconWindow;

                BM.SwTrigger();
                Thread.Sleep(100);
                int k = 0;
                while (true)
                {
                    if (k >= 500)
                        break;
                    else if (BM.j)
                    {
                        ho_Image = BM.ho_image;
                        break;
                    }
                    k++;
                    Thread.Sleep(10);
                }
                //write image
                HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\product");
                //Display image
                HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
                HOperatorSet.SetPart(hv_Window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
                HOperatorSet.DispObj(ho_Image, hv_Window);

            }
            else
            {
                Avt.Open_TriggerPositionZero(1000);
                HObject ho_image;
                HTuple width, height;
                HOperatorSet.GenEmptyObj(out ho_image);
                HTuple hv_Window = hWindowControl1.HalconWindow;

                Avt.OneShot(ref ho_image);

                HOperatorSet.WriteImage(ho_image, "tiff", 0, basepath + @"\image\Product\" + @"snap.tiff");
                HOperatorSet.GetImageSize(ho_image, out width, out height);
                HOperatorSet.SetPart(hv_Window, 0, 0, height - 1, width - 1);
                HOperatorSet.DispObj(ho_image, hv_Window);

                Avt.Close();
            }

        }

        //private void btnlive_Click(object sender, EventArgs e)
        //{
        //    livestate = !livestate;
        //    live = new Thread(new ThreadStart(livethread));
        //    live.IsBackground = true;
        //    live.Start();
        //    if(livestate)
        //    {
        //        btnsnap.Enabled = false;
        //        btnlive.Text = "Stop";
        //    }
        //    else
        //    {
        //        btnsnap.Enabled = true;
        //        btnlive.Text = "Live";
        //    }
        //}

        private void btnTECTemp_Click(object sender, EventArgs e)
        {
            string para = @"num_of_pulses=10
                            leveli=1.0
                            script.user.scripts.smu_pulse()
                            ";
            Com.serial_open(commcu, SourceCom, 9600);  //打开源表
            Com.serial_send(commcu, para);
            string result = Com.serial_readmcu(commcu);
            Com.serial_close(commcu);
        }

        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem, HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {
            // Local iconic variables 

            // Local control variables 

            HTuple hv_Red = null, hv_Green = null, hv_Blue = null;
            HTuple hv_Row1Part = null, hv_Column1Part = null, hv_Row2Part = null;
            HTuple hv_Column2Part = null, hv_RowWin = null, hv_ColumnWin = null;
            HTuple hv_WidthWin = null, hv_HeightWin = null, hv_MaxAscent = null;
            HTuple hv_MaxDescent = null, hv_MaxWidth = null, hv_MaxHeight = null;
            HTuple hv_R1 = new HTuple(), hv_C1 = new HTuple(), hv_FactorRow = new HTuple();
            HTuple hv_FactorColumn = new HTuple(), hv_UseShadow = null;
            HTuple hv_ShadowColor = null, hv_Exception = new HTuple();
            HTuple hv_Width = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_W = new HTuple(), hv_H = new HTuple(), hv_FrameHeight = new HTuple();
            HTuple hv_FrameWidth = new HTuple(), hv_R2 = new HTuple();
            HTuple hv_C2 = new HTuple(), hv_DrawMode = new HTuple();
            HTuple hv_CurrentColor = new HTuple();
            HTuple hv_Box_COPY_INP_TMP = hv_Box.Clone();
            HTuple hv_Color_COPY_INP_TMP = hv_Color.Clone();
            HTuple hv_Column_COPY_INP_TMP = hv_Column.Clone();
            HTuple hv_Row_COPY_INP_TMP = hv_Row.Clone();
            HTuple hv_String_COPY_INP_TMP = hv_String.Clone();
            //
            //Prepare window
            HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
            HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part,
                out hv_Column2Part);
            HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin,
                out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            //
            //default settings
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
            {
                hv_Color_COPY_INP_TMP = "";
            }
            //
            hv_String_COPY_INP_TMP = ((("" + hv_String_COPY_INP_TMP) + "")).TupleSplit("\n");
            //
            //Estimate extentions of text depending on font size.
            HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent,
                out hv_MaxWidth, out hv_MaxHeight);
            if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
            {
                hv_R1 = hv_Row_COPY_INP_TMP.Clone();
                hv_C1 = hv_Column_COPY_INP_TMP.Clone();
            }
            else
            {
                //Transform image to window coordinates
                hv_FactorRow = (1.0 * hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
                hv_FactorColumn = (1.0 * hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
                hv_R1 = ((hv_Row_COPY_INP_TMP - hv_Row1Part) + 0.5) * hv_FactorRow;
                hv_C1 = ((hv_Column_COPY_INP_TMP - hv_Column1Part) + 0.5) * hv_FactorColumn;
            }
            //
            //Display text box depending on text size
            hv_UseShadow = 1;
            hv_ShadowColor = "gray";
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleEqual("true"))) != 0)
            {
                if (hv_Box_COPY_INP_TMP == null)
                    hv_Box_COPY_INP_TMP = new HTuple();
                hv_Box_COPY_INP_TMP[0] = "#fce9d4";
                hv_ShadowColor = "#f28d26";
            }
            if ((int)(new HTuple((new HTuple(hv_Box_COPY_INP_TMP.TupleLength())).TupleGreater(
                1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual("true"))) != 0)
                {
                    //Use default ShadowColor set above
                }
                else if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(1))).TupleEqual(
                    "false"))) != 0)
                {
                    hv_UseShadow = 0;
                }
                else
                {
                    hv_ShadowColor = hv_Box_COPY_INP_TMP[1];
                    //Valid color?
                    try
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(
                            1));
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
                        throw new HalconException(hv_Exception);
                    }
                }
            }
            if ((int)(new HTuple(((hv_Box_COPY_INP_TMP.TupleSelect(0))).TupleNotEqual("false"))) != 0)
            {
                //Valid color?
                try
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
                    throw new HalconException(hv_Exception);
                }
                //Calculate box extents
                hv_String_COPY_INP_TMP = (" " + hv_String_COPY_INP_TMP) + " ";
                hv_Width = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                        hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
                    hv_Width = hv_Width.TupleConcat(hv_W);
                }
                hv_FrameHeight = hv_MaxHeight * (new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                    ));
                hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
                hv_R2 = hv_R1 + hv_FrameHeight;
                hv_C2 = hv_C1 + hv_FrameWidth;
                //Display rectangles
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                //Set shadow color
                HOperatorSet.SetColor(hv_WindowHandle, hv_ShadowColor);
                if ((int)(hv_UseShadow) != 0)
                {
                    HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
                }
                //Set box color
                HOperatorSet.SetColor(hv_WindowHandle, hv_Box_COPY_INP_TMP.TupleSelect(0));
                HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
                HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
            }
            //Write text.
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index % (new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
                    )));
                if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
                    "auto")))) != 0)
                {
                    HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
                }
                else
                {
                    HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                }
                hv_Row_COPY_INP_TMP = hv_R1 + (hv_MaxHeight * hv_Index);
                HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
                HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                    hv_Index));
            }
            //Reset changed window settings
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
            HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part,
                hv_Column2Part);

            return;
        }

        public string action_Normal(HTuple hv_window)
        {
            HTuple hv_WidthWin, hv_HeightWin, hv_Number_1 = null;
            HObject ROI1, ho_ImageReduced_1, ho_Region_1, ho_RegionFillUp1, ho_RegionOpening_1, ho_SelectedRegions_1, ho_ConnectedRegions_1;
            //读取图像
            BM.SwTrigger();
            Thread.Sleep(100);
            int k = 0;
            while (true)
            {
                if (k >= 500)
                    break;
                else if (BM.j)
                {
                    ho_Image = BM.ho_image;
                    break;
                }
                k++;
                Thread.Sleep(10);
            }
            //write image
            HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\product");
          //  string time;
          //  time = DateTime.Now.ToString("yyyyMMddHHmmss");
          //  HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\" + time);
            //read image
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\image\product");
            //Display image
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_window);
            //读取ROI文件
            HOperatorSet.ReadRegion(out ROI1, basepath + @"\ROI_Normal_Pass.reg");
            //blob分析Pass
            HOperatorSet.ReduceDomain(ho_Image, ROI1, out ho_ImageReduced_1);
            HOperatorSet.Threshold(ho_ImageReduced_1, out ho_Region_1, 50, 255);
            HOperatorSet.FillUp(ho_Region_1, out ho_RegionFillUp1);
            HOperatorSet.OpeningCircle(ho_RegionFillUp1, out ho_RegionOpening_1, 35);
            HOperatorSet.SelectShape(ho_RegionOpening_1, out ho_SelectedRegions_1, "area", "and", 3000, 10000);
            HOperatorSet.Connection(ho_SelectedRegions_1, out ho_ConnectedRegions_1);
            HOperatorSet.CountObj(ho_ConnectedRegions_1, out hv_Number_1);
            //分析并返回结果
            if (hv_Number_1 == 1)
            {
                disp_message(hv_window, "pass", hv_window, 100, hv_WidthWin - 500, "red", "false");
                return "1";
            }
            else
            {
                disp_message(hv_window, "fail", hv_window, 100, hv_WidthWin - 500, "red", "false");
                return "-1";
            }
        }

        public string action_Diffuser(HTuple hv_window)
        {
            HTuple hv_WidthWin, hv_HeightWin, hv_Number_1 = null;
            HObject ROI1, ho_ImageReduced_1, ho_Region_1, ho_RegionFillUp1, ho_RegionOpening_1, ho_SelectedRegions_1, ho_ConnectedRegions_1;
            //读取图像
            BM.SwTrigger();
            Thread.Sleep(100);
            int k = 0;
            while (true)
            {
                if (k >= 500)
                    break;
                else if (BM.j)
                {
                    ho_Image = BM.ho_image;
                    break;
                }
                k++;
                Thread.Sleep(10);
            }
            //write image
            HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\product");
           // string time;
           // time = DateTime.Now.ToString("yyyyMMddHHmmss");
           // HOperatorSet.WriteImage(ho_Image, "png", -1, basepath + @"\image\" + time);
            //read image
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\image\product");
            //Display image
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_window);
            //读取ROI文件
            HOperatorSet.ReadRegion(out ROI1, basepath + @"\ROI_Diffuser_Pass.reg");
            //blob分析Pass
            HOperatorSet.ReduceDomain(ho_Image, ROI1, out ho_ImageReduced_1);
            HOperatorSet.Threshold(ho_ImageReduced_1, out ho_Region_1, 50, 255);
            HOperatorSet.FillUp(ho_Region_1, out ho_RegionFillUp1);
            HOperatorSet.OpeningCircle(ho_RegionFillUp1, out ho_RegionOpening_1, 35);
            HOperatorSet.SelectShape(ho_RegionOpening_1, out ho_SelectedRegions_1, "area", "and", 3000, 10000);
            HOperatorSet.Connection(ho_SelectedRegions_1, out ho_ConnectedRegions_1);
            HOperatorSet.CountObj(ho_ConnectedRegions_1, out hv_Number_1);
            //分析并返回结果
            if (hv_Number_1 == 1)
            {
                disp_message(hv_window, "pass", hv_window, 100, hv_WidthWin - 500, "red", "false");
                return "1";
            }
            else
            {
                disp_message(hv_window, "fail", hv_window, 100, hv_WidthWin - 500, "red", "false");
                return "-1";
            }
        }

        private void btnROI_Click(object sender, EventArgs e)
        {
            hWindowControl1.Focus();
            HTuple hv_Window = hWindowControl1.HalconWindow;
            HTuple hv_row1, hv_column1, hv_row2, hv_column2, hv_WidthWin, hv_HeightWin;
            HObject ROI;
            //读取并显示产品
            HOperatorSet.ReadImage(out ho_Image, basepath + @"\product");
            HOperatorSet.GetImageSize(ho_Image, out hv_WidthWin, out hv_HeightWin);
            HOperatorSet.SetPart(hv_Window, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
            HOperatorSet.DispObj(ho_Image, hv_Window);
            //选择ROI
            disp_message(hv_Window, "Please Select ROI!", "window", 10, 10, "red", "true");
            HOperatorSet.DrawRectangle1(hv_Window, out hv_row1, out hv_column1, out hv_row2, out hv_column2);
            HOperatorSet.GenRectangle1(out ROI, hv_row1, hv_column1, hv_row2, hv_column2);
            //保存ROI文件
            HOperatorSet.WriteRegion(ROI, basepath + @"\ROI_Diffuser_Pass.reg");
            disp_message(hv_Window, "Save ROI Success!", "window", 10, 10, "red", "true");
        }

        void InitialLogPath()
        {
            try
            {
                string cutbarcode =barcode;
                this.folderName = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                if (barcode.ToUpper() == "<ERROR>") //"ERROR"\r\n
                {
                    cutbarcode = "ERROR";
                }
              //  __pathLogDir = Application.StartupPath + "\\Log\\" + this.folderName + "_" + cutbarcode + "\\";
                __pathLogDir = __pathLogDir_1 + "\\Log\\" + this.folderName + "_" + cutbarcode +"\\";
                __pathLogDir_2 = __pathLogDir_1 + "\\Log\\" + this.folderName + "_" + cutbarcode + ".done\\";
                if (!File.Exists(__pathLogDir))
                    Directory.CreateDirectory(__pathLogDir);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Create Log Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void SaveLog(string msg)
        {
            txtHelper.WriteText(__pathLogDir_1 + "\\Log\\" + "\\Gimbal.log", DateTime.Now.ToString("yyyyMMdd-hhmmss") + ":" + msg);
            txtHelper.WriteText(__pathLogDir + "\\Gimbal.log", DateTime.Now.ToString("yyyyMMdd-hhmmss") + ":" + msg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double t = (double)stopwatch.ElapsedMilliseconds/1000.0;
            lblTime.Text = t.ToString("f3");
        }

        private void btDefaultSN_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = "FWP638701S2H6CWC5";
        }

 
        private void cleartotal_Click(object sender, EventArgs e)
        {
            if (__pathLot == "")
                MessageBox.Show("Please click New Lot button first!");
            else
            {

                //填数据
                string lotdata2 = DateTime.Now.ToString("dd-MMM-yyyy_HHmmss");
                string __pathLot2 = "E:\\LotSummary\\" + TesterID + "_" + DeviceName + "_" + Form1_StepName.SelectedText + "_" + LotID + "_" + DiffusionLotID + "_" + Form1_TestMode.SelectedText + "_" + lotdata2 + "_end" + ".csv";

                et.Rows[0][0] = "index";
                et.Rows[0][1] = "group";
                et.Rows[0][2] = "groupinfo";
                et.Rows[0][3] = "item";
                et.Rows[0][4] = "starttime";
                et.Rows[0][5] = "endtime";
                et.Rows[0][6] = "testtime";
                et.Rows[0][7] = "low";
                et.Rows[0][8] = "high";
                et.Rows[0][9] = "unit";
                et.Rows[0][10] = "value";
                et.Rows[0][11] = "result";
                et.Rows[1][0] = "1";
                et.Rows[1][1] = "LOTE";
                et.Rows[1][3] = "#PASS_BIN_1";
                et.Rows[1][4] = "st";
                et.Rows[1][5] = "et";
                et.Rows[1][6] = "0";
                et.Rows[1][10] = "0";
                et.Rows[1][11] = "PASS";
                et.Rows[2][0] = "2";
                et.Rows[2][1] = "LOTE";
                et.Rows[2][3] = "#PASS_BIN_2";
                et.Rows[2][4] = "st";
                et.Rows[2][5] = "et";
                et.Rows[2][6] = "0";
                et.Rows[2][10] = "0";
                et.Rows[2][11] = "PASS";
                et.Rows[3][0] = "3";
                et.Rows[3][1] = "LOTE";
                et.Rows[3][3] = "#PASS_BIN_3";
                et.Rows[3][4] = "st";
                et.Rows[3][5] = "et";
                et.Rows[3][6] = "0";
                et.Rows[3][10] = "0";
                et.Rows[3][11] = "PASS";
                et.Rows[4][0] = "4";
                et.Rows[4][1] = "LOTE";
                et.Rows[4][3] = "#FAIL_BIN_5";
                et.Rows[4][4] = "st";
                et.Rows[4][5] = "et";
                et.Rows[4][6] = "0";
                et.Rows[4][10] = "0";
                et.Rows[4][11] = "PASS";
                et.Rows[5][0] = "5";
                et.Rows[5][1] = "LOTE";
                et.Rows[5][3] = "#FAIL_BIN_6";
                et.Rows[5][4] = "st";
                et.Rows[5][5] = "et";
                et.Rows[5][6] = "0";
                et.Rows[5][10] = "0";
                et.Rows[5][11] = "PASS";
                et.Rows[6][0] = "6";
                et.Rows[6][1] = "LOTE";
                et.Rows[6][3] = "#FAIL_BIN_7";
                et.Rows[6][4] = "st";
                et.Rows[6][5] = "et";
                et.Rows[6][6] = "0";
                et.Rows[6][10] = "0";
                et.Rows[6][11] = "PASS";
                et.Rows[7][0] = "7";
                et.Rows[7][1] = "LOTE";
                et.Rows[7][3] = "#FAIL_BIN_2D";
                et.Rows[7][4] = "st";
                et.Rows[7][5] = "et";
                et.Rows[7][6] = "0";
                et.Rows[7][10] = "0";
                et.Rows[7][11] = "PASS";
                et.Rows[8][0] = "8";
                et.Rows[8][1] = "LOTE";
                et.Rows[8][3] = "#OVERRALL_YIELD";
                et.Rows[8][4] = "st";
                et.Rows[8][5] = "et";
                et.Rows[8][6] = "0";
                et.Rows[8][10] = "0";
                et.Rows[8][11] = "PASS";
                et.Rows[9][0] = "9";
                et.Rows[9][1] = "LOTE";
                et.Rows[9][3] = "#TOTAL_INPUT";
                et.Rows[9][4] = "st";
                et.Rows[9][5] = "et";
                et.Rows[9][6] = "0";
                et.Rows[9][10] = "0";
                et.Rows[9][11] = "PASS";
                csvcontrol.SaveCSV(et, __pathLot2);
                Thread.Sleep(2000);
                string IP = file.ReadXmlFile("MTCP", "IP");
                int Port = Convert.ToInt32(file.ReadXmlFile("MTCP", "Port"));
                MTCP_OPEN(IP, Port, 5000);
                MTCP_GENL(__pathLot2, 1);
                MTCP_CLOSE();
                __pathLot = "";

                EndLot endlot = new EndLot();
                endlot.ShowDialog();

                    total = 0;
                    pass = 0;
                    fail = 0;
                    this.Total.Text = "0";
                    this.Pass.Text = "0";
                    this.Fail.Text = "0";
                    this.PassPercent.Text = "0.00";
                    this.FailPercent.Text = "0.00";
                    this.Form1_StepName.SelectedIndex = 0;
                    this.Form1_TestMode.SelectedIndex = 0;
                    this.Form1_LotID.Text = "";
                    this.Form1_DiffusionLotID.Text = "";
                    this.Form1_LotSize.Value = 0;
                    this.Form1_OperatorName.Text = "";

                    this.cleartotal.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (__pathLot == "")
            {
                New_Lot newlot = new New_Lot();
                newlot.ShowDialog();
                if (NewLotBool)
                {
                    this.Form1_DeviceName.Text = DeviceName;
                    this.Form1_StepName.SelectedIndex = StepNameIndex;
                    this.Form1_TestMode.SelectedIndex = TestModeIndex;
                    this.Form1_LotID.Text = LotID;
                    this.Form1_DiffusionLotID.Text = DiffusionLotID;
                    this.Form1_LotSize.Value = LotSize;
                    this.Form1_OperatorName.Text = OperatorName;
                    this.Form1_TesterID.Text = TesterID;
                    string lotdata = DateTime.Now.ToString("dd-MMM-yyyy_HHmmss");
                    __pathLot = "E:\\LotSummary\\" + TesterID + "_" + DeviceName + "_" + Form1_StepName.SelectedText + "_" + LotID + "_" + DiffusionLotID + "_" + Form1_TestMode.SelectedText + "_" + lotdata + "_start" + ".csv";
                    st.Rows[0][0] = "index";
                    st.Rows[0][1] = "group";
                    st.Rows[0][2] = "groupinfo";
                    st.Rows[0][3] = "item";
                    st.Rows[0][4] = "starttime";
                    st.Rows[0][5] = "endtime";
                    st.Rows[0][6] = "testtime";
                    st.Rows[0][7] = "low";
                    st.Rows[0][8] = "high";
                    st.Rows[0][9] = "unit";
                    st.Rows[0][10] = "value";
                    st.Rows[0][11] = "result";
                    st.Rows[1][0] = "1";
                    st.Rows[1][1] = "LOTS";
                    st.Rows[1][3] = "#TESTER_NAME";
                    st.Rows[1][4] = "st";
                    st.Rows[1][5] = "et";
                    st.Rows[1][6] = "0";
                    st.Rows[1][10] = "TIDI";
                    st.Rows[1][11] = "PASS";
                    st.Rows[2][0] = "2";
                    st.Rows[2][1] = "LOTS";
                    st.Rows[2][3] = "#TESTER_ID";
                    st.Rows[2][4] = "st";
                    st.Rows[2][5] = "et";
                    st.Rows[2][6] = "0";
                    st.Rows[2][10] = "2";
                    st.Rows[2][11] = "PASS";
                    st.Rows[3][0] = "3";
                    st.Rows[3][1] = "LOTS";
                    st.Rows[3][3] = "#LOT_NAME";
                    st.Rows[3][4] = "st";
                    st.Rows[3][5] = "et";
                    st.Rows[3][6] = "0";
                    st.Rows[3][10] = LotID;
                    st.Rows[3][11] = "PASS";
                    st.Rows[4][0] = "4";
                    st.Rows[4][1] = "LOTS";
                    st.Rows[4][3] = "#LOT_SIZE";
                    st.Rows[4][4] = "st";
                    st.Rows[4][5] = "et";
                    st.Rows[4][6] = "0";
                    st.Rows[4][10] = LotSize.ToString();
                    st.Rows[4][11] = "PASS";
                    st.Rows[5][0] = "5";
                    st.Rows[5][1] = "LOTS";
                    st.Rows[5][3] = "#D_LOT_NAME";
                    st.Rows[5][4] = "st";
                    st.Rows[5][5] = "et";
                    st.Rows[5][6] = "0";
                    st.Rows[5][10] = DiffusionLotID;
                    st.Rows[5][11] = "PASS";
                    st.Rows[6][0] = "6";
                    st.Rows[6][1] = "LOTS";
                    st.Rows[6][3] = "#OPERATOR";
                    st.Rows[6][4] = "st";
                    st.Rows[6][5] = "et";
                    st.Rows[6][6] = "0";
                    st.Rows[6][10] = OperatorName;
                    st.Rows[6][11] = "PASS";
                    st.Rows[7][0] = "7";
                    st.Rows[7][1] = "LOTS";
                    st.Rows[7][3] = "#AM_SW_VER";
                    st.Rows[7][4] = "st";
                    st.Rows[7][5] = "et";
                    st.Rows[7][6] = "0";
                    st.Rows[7][10] = this.VERSION.Text;
                    st.Rows[7][11] = "PASS";
                    st.Rows[8][0] = "8";
                    st.Rows[8][1] = "LOTS";
                    st.Rows[8][3] = "#TEST_MODE";
                    st.Rows[8][4] = "st";
                    st.Rows[8][5] = "et";
                    st.Rows[8][6] = "0";
                    st.Rows[8][10] = TestModeIndex.ToString();
                    st.Rows[8][11] = "PASS";
                    csvcontrol.SaveCSV(st, __pathLot);
                    total = 0;
                    pass = 0;
                    fail = 0;
                    this.Total.Text = "0";
                    this.Pass.Text = "0";
                    this.Fail.Text = "0";
                    this.PassPercent.Text = "0.00";
                    this.FailPercent.Text = "0.00";
                    Thread.Sleep(2000);
                    string IP = file.ReadXmlFile("MTCP", "IP");
                    int Port = Convert.ToInt32(file.ReadXmlFile("MTCP", "Port"));
                    MTCP_OPEN(IP, Port, 5000);
                    MTCP_GENL(__pathLot, 1);
                    MTCP_CLOSE();
                    this.cleartotal.Enabled = true;
                }
                NewLotBool = false;
            }
            else
                MessageBox.Show("Please End Lot first!");
        }

        public bool movefolder(string SourceFolderName,string TargetFolderName)
        {
            try
            {
                Directory.Move(SourceFolderName, TargetFolderName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

