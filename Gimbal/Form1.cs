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
        extern static int MTCP_TSCR(string dutsn, string socketsn, string sw, int mode, int slotid, int station);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_INIT(string sw, string tn, string lot, int v);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_VDCR(float[] rsp);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_ALPR(float[] rsp);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_ALPH(string[] path, ushort img_cnt, ushort width, ushort height, uint size, string i_dr, string v_for);

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_POST();

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_TSED();

        [DllImport("GimbalDll.dll")]
        extern static int MTCP_CLOSE();
        int ItemIndex = 0;
        DriverBoard __DriverBoard = new DriverBoard();
        Stopwatch stopwatch = new Stopwatch();
        TxtHelper txtHelper = new TxtHelper();
        private string folderName = string.Empty;
        private SerialPort commcu = new SerialPort();
        private SerialPort comtec = new SerialPort();
        private SerialPort comscan = new SerialPort();
        AvtCam Avt = new AvtCam();
        Serial Com = new Serial();
        ImageProcess pro = new ImageProcess();
        TCPClient Tcp = new TCPClient();
        TCPClient Tcp1 = new TCPClient();
        ScannerCommunications scanner = new ScannerCommunications();
        BaumerSDK BM = new BaumerSDK();
        ProcessTif tif = new ProcessTif();
        Operxml file = new Operxml();
        HObject ho_Image;
        string basepath = System.IO.Directory.GetCurrentDirectory();
        Thread live;
        Thread Process, TecP;
        bool livestate = false;
        bool exit = false;
        string barcode = null;
        Int32 X;   //X轴旋转角度
        Int32 Y;   //Y轴旋转角度
        String I;  //SMU电流值
        Int32 T;   //等待TEC稳定时间
        string voltage;
        string current;
        double meani;  //smu回采电流平均值
        double meanv;  //smu回采电压平均值
        string barcodeCom = "com1";
        string SourceCom = "com8";
        string TecCom = "com5";
        string power = @"reset()
                        smua.source.output = smua.OUTPUT_OFF
                        digio.writebit(2, 1)
                        period_timer = trigger.timer[1]
                        pulse_timer = trigger.timer[2]
                        smua.trigger.source.listi({1})    
                        smua.source.rangei = 1
                        smua.source.limitv = 3
                        smua.trigger.measure.action = smua.DISABLE
                        period_timer.delay = 0.033
                        period_timer.count = 10 
                        period_timer.stimulus = smua.trigger.ARMED_EVENT_ID
                        period_timer.passthrough = true
                        pulse_timer.delay = 0.0036
                        pulse_timer.stimulus = period_timer.EVENT_ID
                        pulse_timer.count = 1
                        pulse_timer.passthrough=false
                        digio.trigger[1].mode = digio.TRIG_FALLING
                        digio.trigger[1].pulsewidth = 0.0006
                        digio.trigger[1].stimulus =  smua.trigger.ARMED_EVENT_ID
                        smua.trigger.count = 1
                        smua.trigger.arm.count = 200
                        smua.trigger.arm.stimulus = 0
                        smua.trigger.source.stimulus = period_timer.EVENT_ID
                        smua.trigger.source.action = smua.ENABLE
                        smua.trigger.endpulse.stimulus = pulse_timer.EVENT_ID
                        smua.trigger.endpulse.action = smua.SOURCE_IDLE
                        smua.trigger.endsweep.action = smua.SOURCE_IDLE
                        smua.source.output = smua.OUTPUT_ON
                        smua.trigger.initiate()
                        waitcomplete()";
        string openTEC = @"$W" + "\r\n";
        string closeTEC = @"$Q" + "\r\n";
        string TECTemp = @"$R100?" + "\r\n";



        string __pathLogDir = "Log";
        string __pathBlackImagePath = "BlackImage";
        string __pathNormalImagePath = "NormalImage";

        public Form1()
        {
            InitializeComponent();
            btnsnap.Enabled = true;
            btnlive.Text = "Live";

            ListViewItem item = new ListViewItem();
            item.SubItems.Add("Hi");
            __DriverBoard.Connect("192.168.0.66", "7600");

            Control.CheckForIllegalCrossThreadCalls = false;
            comboProduct.SelectedIndex = 0;
            comboCamera.SelectedIndex = 0;
            BM.Initialize();
            BM.Play();

            Com.serial_open(comtec, TecCom, 115200);  //打开TEC
            // Com.serial_send(comtec, openTEC);
            //Com.serial_send(comtec, TECTemp);
            //Thread.Sleep(500);
            //txtTECTemp.Text = Com.serial_read(comtec);
            //Com.serial_close(comtec);

            Com.serial_open(comscan, barcodeCom, 115200);

            string IP = file.ReadXmlFile("IP");
            string Port = file.ReadXmlFile("Port");

            Tcp.tcpconnect(IP, Port);

            //string IP1 = "169.254.1.99";
            //string Port1 = "49211";
            //Tcp1.tcpconnect(IP1, Port1);
            
            string IPScanner = "169.254.1.99";
            string PortScanner = "49211";
            scanner.Connect(IPScanner, PortScanner);

            Process = new Thread(new ThreadStart(Processthread));
            Process.IsBackground = true;
            Process.Start();

            TecP = new Thread(new ThreadStart(ReadTecTemp));
            TecP.IsBackground = true;
            TecP.Start();



            //AVT Read image
            /*
            HTuple hv_Window = hWindowControl1.HalconWindow;
            HObject ho_image;
            HTuple width, height;
            HOperatorSet.GenEmptyObj(out ho_image);
            Avt.Open();
            Avt.OneShot(ref ho_image);
            HOperatorSet.GetImageSize(ho_image, out width, out height);
            HOperatorSet.SetPart(hv_Window, 0, 0, width - 1, height - 1);
            HOperatorSet.DispObj(ho_image, hv_Window);
            Avt.Close();
             * */
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboCamera.SelectedIndex = 1;
            //Add item
            AddRow("Scan Barcode");
            AddRow("MTCP Current");
            AddRow("MTCP Steady Time");
            AddRow("MTCP Position X");
            AddRow("MTCP Position Y");
            AddRow("Black Image Capture");
            AddRow("SMU Power On");
            AddRow("Image Capture");
            AddRow("SMU Current");
            AddRow("SMU Voltage");
            AddRow("MTCP Post");
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
            InitialLogPath();
            Log("Initial Testing...");

            if (checkboxScan.Checked)
            {
                txtBarcode.Text = "";
            }

            stopwatch.Reset();
            
            HTuple hv_Window = hWindowControl1.HalconWindow;
            HObject ho_image;
            HOperatorSet.GenEmptyObj(out ho_image);
            HOperatorSet.DispObj(ho_image, hv_Window);
            ResetListView();
        }

        void CaptureImage(string time, ref string imagefile)
        {
            string CaptureBlackstatus = "failed";
            try
            {
                Log("Black image capturing...");
                HTuple hv_Window = hWindowControl1.HalconWindow;
                HObject ho_image;
                HTuple width, height;
                HOperatorSet.GenEmptyObj(out ho_image);
                Avt.OneShot(ref ho_image);

                string Sx, Sy;
                float x, y;
                x = (float)X / 1000;
                y = (float)Y / 1000;
                Sx = x.ToString();
                Sy = y.ToString();
                string PicName;
                string cutbarcode;
                cutbarcode = barcode.Replace("\r", "").Replace("\n", "");
                PicName = cutbarcode + " X" + Sx + " Y" + Sy + " " + time + "Black";

                imagefile = __pathLogDir + PicName + ".tif";
                HOperatorSet.WriteImage(ho_image, "tiff", 0, imagefile);
                HOperatorSet.GetImageSize(ho_image, out width, out height);
                HOperatorSet.SetPart(hv_Window, 0, 0, height - 1, width - 1);
                HOperatorSet.DispObj(ho_image, hv_Window);
                CaptureBlackstatus = "pass";
                Log("Black image capture finished!\r\n"+"X="+Sx+"\r\n"+"Y=" +Sy+"\r\n");
            }
            catch (Exception ex)
            {
                CaptureBlackstatus = "failed";
            }
            AddValueAndStatus(5, 2, "");
            AddValueAndStatus(5, 3, CaptureBlackstatus);
        }

        private void Processthread()
        {
            while(true)
            {
                switch(Tcp.result)
                {
                    //产品上料防呆
                    case("0100bb00bb000000"):
                        InitialTest();
                        stopwatch.Start();
                        Tcp.result = null;
                        HTuple hv_window = hWindowControl1.HalconWindow;
                        Thread.Sleep(200);
                      //  string result = action_Diffuser(hv_window);
                        string result = "1";
                        if (result == "1")
                        {
                            byte[] data = { 0x02, 0x00, 0xdd, 0x00, 0xdd, 0x00, 0x00, 0x00 };
                            Tcp.sendbytes(data);
                            
                        }
                        else if (result == "-1")
                        {
                            byte[] data = { 0x02, 0x00, 0xde, 0x00, 0xde, 0x00, 0x00, 0x00 };
                            Tcp.sendbytes(data);
                            MessageBox.Show("Product is not correct in DUT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        Log("Loading DUT" + Tcp.result);
                        break;
                    //产品扫条码
                    case("0300bb00bb000000"):
                        Log("Start to scan SN!");
                        Tcp.result = null;
                        Thread.Sleep(200);

                        if (checkboxScan.Checked)
                        {
                            //scan from camera
                            //  Com.serial_send(comscan,"S");
                            scanner.Send("T");
                            Thread.Sleep(1500);

                            barcode = scanner.result();

                            //  barcode = Com.serial_read(comscan);
                        }
                        else
                        {
                            //barcode = "FWP638701S2H6CWC5";
                            barcode = txtBarcode.Text;
                        }

                        string statusBarcode = "pass";
                        string statusMTCP = "failed";
                       
                        if (barcode.ToUpper() == "<ERROR>") //"ERROR"\r\n
                        {
                            statusBarcode = "failed";
                            barcode = "ERROR";
                            byte[] data = { 0x03, 0x00, 0xee, 0x00, 0xee, 0x00, 0x00, 0x00 };
                            Tcp.sendbytes(data);
                            MessageBox.Show("Barcode is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            stopwatch.Stop();
                        }
                        else
                        {
                            //与MTCP通讯
                            statusBarcode = "pass";
                            try
                            {
                                MTCP_OPEN("169.254.1.111", 61808, 5000);
                                string barcodecut;
                                barcodecut = barcode.Replace("\r", "").Replace("\n", "");
                                MTCP_TSCR(barcodecut, "123456", "1.0", 0, 1, 1);
                                MTCP_INIT("1.0", "GimbalTest", "Gimballot", 10);
                                float[] vdcr_rsp = new float[2];
                                MTCP_VDCR(vdcr_rsp);
                                I = vdcr_rsp[0].ToString();      //SMU电流值
                                T = (Int32)vdcr_rsp[1];          //等待TEC稳定时间
                                float[] alpr_rsp = new float[2];
                                MTCP_ALPR(alpr_rsp);
                                X = (Int32)(alpr_rsp[0] * 1000);    //X轴旋转角度
                                Y = (Int32)(alpr_rsp[1] * 1000);    //Y轴旋转角度
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

                            string msg = string.Format("MTCP Response:T={0},I={1},X={2},Y={3}",I,T,X,Y);
                            Log(msg);
                            //与beckhoff通讯
                            byte[] data = { 0x03, 0x00, 0xdd, 0x00, 0xdd, 0x00, 0x00, 0x00 };
                            Tcp.sendbytes(data);
                        }

                        txtBarcode.Text = barcode;
                        Log("Scan Finished!");
                        AddValueAndStatus(0, 2, barcode);
                        AddValueAndStatus(0, 3, statusBarcode);
                        AddValueAndStatus(1, 2, I);
                        AddValueAndStatus(1, 3, statusMTCP);
                        AddValueAndStatus(2, 2, T.ToString());
                        AddValueAndStatus(2, 3, statusMTCP);
                        AddValueAndStatus(3, 2, X.ToString());
                        AddValueAndStatus(3, 3, statusMTCP);
                        AddValueAndStatus(4, 2, Y.ToString());
                        AddValueAndStatus(4, 3, statusMTCP);
                       
                        break;

                    //发送X、Y坐标
                    case ("1000dd00dd000000"):
                        Tcp.result = null;
                        //send position x
                        //Int32 X=002000;           //前3位是角度整数部分，后3位是角度小数部分
                        X = GetPosition(xpos.Value);
                        byte[] arryX2=new byte[4];
                        ConvertIntToByteArray(X,ref arryX2);             //整数转字节数组
                        byte[] arryX1={ 0x20, 0x00, 0x00, 0x00 };        //定义第一个字节数组
                        byte[] dataX = arryX1.Concat(arryX2).ToArray();  //连接两个字节数组
                        // byte[] dataX = { 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };  //最终发送数据形式
                        Tcp.sendbytes(dataX);
                        //send position y
                        //Int32 Y=002000;         //前3位是角度整数部分，后3位是角度小数部分
                        Y = GetPosition(ypos.Value);
                        byte[] arryY2=new byte[4];
                        ConvertIntToByteArray(Y,ref arryY2);             //整数转字节数组
                        byte[] arryY1={ 0x20, 0x00, 0x01, 0x00 };        //定义第一个字节数组
                        byte[] dataY = arryY1.Concat(arryY2).ToArray();  //连接两个字节数组
                        // byte[] dataY = { 0x20, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 };   //最终发送数据形式
                        Tcp.sendbytes(dataY);
                        Log("Send x,y to PLC");
                        break;
                     //X坐标超出范围报警
                    case("20000000ee000000"):
                        Tcp.result = null;
                        MessageBox.Show("X angle out of range", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    //Y坐标超出范围报警
                    case ("20000100ee000000"):
                        Tcp.result = null;
                        MessageBox.Show("Y angle out of range", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    //AVT相机采集数据
                    case ("2000dd00dd000000"):
                        Log("Start to capture...");
                        Tcp.result = null;
                
                        HTuple hv_Window = hWindowControl1.HalconWindow;
                        HObject ho_image;
                        HTuple width, height;
                        HOperatorSet.GenEmptyObj(out ho_image);

                        double exposureTimeAbs = Convert.ToInt32(ExposureTime.Text);//500000;
               
                        Avt.Open_NoTrigger(exposureTimeAbs);
                        ushort[,] rawdata = new ushort[tif.height, tif.width];
                      
                        string time1;
                        time1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                        //Capture black image
                        string pathblack = string.Empty;
                        CaptureImage(time1, ref pathblack);
                        //打开TEC
                        Com.serial_send(comtec, openTEC);
                        Thread.Sleep(T*1000);   //TEC稳定时间

#if true
                        Log("Enable DUT Yogi...");
                        __DriverBoard.PowerVDD();
                        __DriverBoard.InitYogi();
                        __DriverBoard.BypassYogi();
                        __DriverBoard.ReadYogi();
                        Log("Enable Yogi finished!");
#endif
                     

                        Log("Turn on unit...");     
                        Process = new Thread(new ThreadStart(OperateMCU));
                        Process.IsBackground = true;
                        Process.Start();
                        
                        Thread.Sleep(400);
                        Avt.Open(exposureTimeAbs);
                        Log("Start to capture image with unit power on...");
                        Avt.OneShot(ref ho_image);
                        Tb_graymax.Text=pro.getmax(ho_image).ToString();

                        //关闭TEC
                        Com.serial_send(comtec, closeTEC);

                        
                        string Sx, Sy;
                        float x, y;
                        x = (float)X / 1000;
                        y = (float)Y / 1000;
                        Sx = x.ToString();
                        Sy = y.ToString();
                        string PicName;
                        string cutbarcode;
                        cutbarcode = barcode.Replace("\r", "").Replace("\n", "");
                        PicName = cutbarcode + " X" + Sx + " Y" + Sy + " " + time1;
                        HOperatorSet.WriteImage(ho_image, "tiff", 0, __pathLogDir+PicName);
                        HOperatorSet.GetImageSize(ho_image, out width, out height);
                        HOperatorSet.SetPart(hv_Window, 0, 0, height - 1, width - 1);
                        HOperatorSet.DispObj(ho_image, hv_Window);
                        Avt.Close();
                        Log("Capture finished!...");
                        AddValueAndStatus(7, 2, "");
                        AddValueAndStatus(7, 3, "pass");
                        //与MTCP通讯
                        Thread.Sleep(2500);//等待smu计算平均电流和电压值3
                        string statusMTCPSendData = "failed";
                        try
                        {
                            Log("Send data to MTCP...");
                            uint size = (uint)(width * height);
                            string[] path = { __pathLogDir + PicName + ".tif", pathblack};//{ pathimage + PicName + ".tif", pathblack };
                            ushort img_cnt = 2;//2;
                            float meaniI = (float)meani;
                            float meanvV = (float)meanv;
                            int ret = MTCP_ALPH(path, img_cnt, (ushort)width, (ushort)height, 0, current, voltage);
                            Log(string.Format("ALPH Return Value : {0}", ret));
                            MTCP_POST();
                            MTCP_TSED();
                            MTCP_CLOSE();

                            __DriverBoard.Reset();
                            SMU_PowerOff();
                            stopwatch.Stop();
                            Log("MTCP Send Completed!\r\n"+"meanI="+meani+"\r\n"+"meanV="+meanvV+"\r\n");
                            statusMTCPSendData = "pass";
                        }
                        catch(Exception exp)
                        {
                            statusMTCPSendData = "failed";
                            MessageBox.Show(exp.Message);
                        }
                        AddValueAndStatus(8, 2, meani.ToString());
                        AddValueAndStatus(8, 3, statusMTCPSendData);
                        AddValueAndStatus(9, 2, meanv.ToString());
                        AddValueAndStatus(9, 3, statusMTCPSendData);
                        AddValueAndStatus(10, 2, " ");
                        AddValueAndStatus(10, 3, "OK");
                        //与Beckhoff通讯
                        byte[] dataState = { 0x30, 0x00, 0xdd, 0x00, 0xdd, 0x00, 0x00, 0x00 };
                        Tcp.sendbytes(dataState);    
                        break;
                    //产品下料
                    case("4000dd00dd000000"):
                        Tcp.result = null;
                         __DriverBoard.Reset();
                         SMU_PowerOff();
                         stopwatch.Stop();
                      //  MessageBox.Show("Test Finished", "State", MessageBoxButtons.OK);
                        break;
                    //机器报错
                    case("ee00ee00ee000000"):
                         __DriverBoard.Reset();
                         SMU_PowerOff();
                         stopwatch.Stop();
                        Tcp.result = null;
                        double t = (double)stopwatch.ElapsedMilliseconds/1000.0;
                        lblTime.Text = t.ToString("f3");
                        MessageBox.Show("Machine Error", "Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        break;
                }
            }
        }


        void ParseMCUResponse(string vi, out double meanC, out double meanV,out string voltage, out string current)
        {
            string match_current = "Current:(.*)Voltage";
            string match_volt = "Voltage:(.*)Time";

            Regex regexCurrent = new Regex(match_current, RegexOptions.Singleline);

            //current
            Match mc = regexCurrent.Match(vi);
            string vc = mc.Groups[1].ToString();
            vc = vc.Trim();
            string[] arr = vc.Split(",".ToCharArray());
            current = vc;
            ArrayList arrayCurrent = new ArrayList();

            double sumC = 0;
            foreach (var i in arr)
            {
                var value = Convert.ToDouble(i);
                arrayCurrent.Add(value);
                sumC = sumC + value;
            }
            meanC = sumC / arrayCurrent.Count;

            //voltage
            Regex regexVolt = new Regex(match_volt, RegexOptions.Singleline);
            Match mv = regexVolt.Match(vi);
            string vv = mv.Groups[1].ToString();
            vv = vv.Trim();
            string[] arr1 = vv.Split(",".ToCharArray());
            voltage = vv;
            ArrayList arrayVolt = new ArrayList();
            double sumV = 0;
            foreach (var i in arr1)
            {
                var value = Convert.ToDouble(i);
                arrayVolt.Add(Convert.ToDouble(i));
                sumV = sumV + value;
            }
            meanV = sumV / arrayVolt.Count;

            Log(vi+"\r\n");
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
        private void OperateMCU()
        { 
           // Thread.Sleep(1000);
           /*
            string para = @"num_of_pulses=10
                            leveli=1.0
                            script.user.scripts.smu_pulse()
                            ";
            * */
            string para = "num_of_pulses=100" + "\r\n" + "leveli=" + I + "\r\n" + "script.user.scripts.smu_pulse()" + "\r\n";
            Com.serial_open(commcu, SourceCom, 9600);  //打开源表
            Com.serial_send(commcu, para);

            string vi = Com.serial_readmcu(commcu);
            Console.WriteLine("~~~~~~~~~~~~~~~SMU Response:");
            Console.WriteLine(vi);
           

            Com.serial_close(commcu);

            ParseMCUResponse(vi, out meani, out meanv,out voltage, out current);
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            AddValueAndStatus(6, 2, "");
            AddValueAndStatus(6, 3, "pass");
            Log(para);
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
            exit = true;
            BM.Exit();
            Avt.Close();
            //Com.serial_open(comtec, TecCom, 115200);  //关闭TEC
           // Com.serial_send(comtec, closeTEC);
            Com.serial_close(comtec);
            Com.serial_close(comscan);
            txtTECTemp.Dispose();
        }

        private void livethread()
        {
            HTuple hv_Subsampling = new HTuple(), hv_Sharpness = new HTuple();
            hv_Subsampling = 3;
            if(comboCamera.SelectedIndex==0)
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
                    Avt.Open_NoTrigger(1000);
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
            if (comboCamera.SelectedIndex == 0)
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
                Avt.Open_NoTrigger(1000);
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

        private void btnlive_Click(object sender, EventArgs e)
        {
            livestate = !livestate;
            live = new Thread(new ThreadStart(livethread));
            live.IsBackground = true;
            live.Start();
            if(livestate)
            {
                btnsnap.Enabled = false;
                btnlive.Text = "Stop";
            }
            else
            {
                btnsnap.Enabled = true;
                btnlive.Text = "Live";
            }
        }

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
                this.folderName = DateTime.Now.ToString("yyyyMMdd-hhmmss");

                __pathLogDir = Application.StartupPath + "\\Log\\" + this.folderName+"\\";
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
            txtHelper.WriteText(__pathLogDir + "\\Gimbal.log", msg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double t = (double)stopwatch.ElapsedMilliseconds/1000.0;
            lblTime.Text = t.ToString("f3");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtTECTemp_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btDefaultSN_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = "FWP638701S2H6CWC5";
        }

    }
}
