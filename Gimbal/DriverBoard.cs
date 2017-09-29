using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Gimbal
{
    class DriverBoard
    {
       /*
        * io set(5,)
        */
        TcpClient TCP = new TcpClient();
        NetworkStream sendStream;
        public DriverBoard()
        {
        }

        public bool Connect(string ip, string port)
        {
            if (ip.Trim() == string.Empty)
            {
                return false;
            }
            if (port.Trim() == string.Empty)
            {
                return false;
            }
            IPAddress ipadd = IPAddress.Parse(ip);
            if (TCP.Connected)
            {
                return true;
            }
            else
            {
                try
                {
                    TCP.Connect(ipadd, int.Parse(port));
                    sendStream = TCP.GetStream();
                }
                catch
                {
                    MessageBox.Show("Connect Fail!", "Error", MessageBoxButtons.OK);
                    return false;
                }

                return true;
            }

        }

        public void Send(string sendmsg)
        {
            if (TCP != null)
            {
                //要发送的信息
                if (sendmsg == string.Empty)
                    return;

                string msg = sendmsg;
                //将信息存入缓存中
                byte[] buffer = Encoding.Default.GetBytes(msg);
                //lock (sendStream)
                //{
                sendStream.Write(buffer, 0, buffer.Length);
                //}
                //rtbtxtShowData.AppendText("发送给服务端的数据:" + msg + "\n");
                sendmsg = string.Empty;
            }

            string buf = ReadString(100);
            if (buf == null) buf = "";
            Console.WriteLine("~~~~~~~Driver Board Response:"+buf);
        }

        public string ReadString(int length)
        {
            try
            {
                byte[] tmp = new byte[length];
                int readSize = sendStream.Read(tmp, 0, length);
                string buffer = Encoding.Default.GetString(tmp, 0, readSize);
                return buffer;
            }
            catch(Exception exp)
            {
                return exp.Message;
            }
            
            return null;
        }

        public void PowerVDD()
        {
            Send("[123]io set(5,bit1=1,bit2=1,bit7=1,bit8=1,bit9=1)\r\n");
            Thread.Sleep(10);
        }

        public void InitYogi()
        {
            Send("[123]i2c read(CH1,0x33,0x37,1)\r\n");
            Send("[123]i2c read(CH1,0x33,0x30,1)\r\n");
            Thread.Sleep(10);
        }

        public void BypassYogi()
        {
            Send("[123]i2c write(CH1,0x33,0x37,1,0x80)\r\n");
            Send("[123]i2c write(CH1,0x33,0x30,1,0xe0)\r\n");
            Send("[123]i2c write(CH1,0x33,0x39,1,0x1e)\r\n");
            Send("[123]i2c write(CH1,0x33,0x1f,1,0xfe)\r\n");
            Send("[123]i2c write(CH1,0x33,0x38,1,0x80)\r\n");
        }
        
        public string ReadYogiNVM()
        {
            //Send("[123]i2c read(CH1,0x33,0x37,1)\r\n");
            //Send("[123]i2c read(CH1,0x33,0x30,1)\r\n");
            //Send("[123]i2c read(CH1,0x33,0x39,1)\r\n");
            //Send("[123]i2c read(CH1,0x33,0x1f,1)\r\n");
            //Send("[123]i2c read(CH1,0x33,0x38,1)\r\n");
            Send("[123]i2c read(CH1,0x33,0x37,1)\r\n");
            Send("[123]i2c read(CH1,0x33,0x30,1)\r\n");
            Send("[123]i2c read(CH1,0x33,0x39,1)\r\n");
            Send("[123]i2c read(CH1,0x33,0x1f,1)\r\n");
            string buffer0x1f = ReadString(100);
            Send("[123]i2c read(CH1,0x33,0x38,1)\r\n");
            Thread.Sleep(500);
            string buffer = ReadString(100);
            return "";
        }

        public string SetYogiActiMode()
        {
            Send("[123]fpga io set(1,CH1=1)\r\n");
            Thread.Sleep(10);
          //  string buffer = ReadString(100);
            return "";
        }

        public string ReadYogi()
        {
            Send("[123]i2c read(CH1,0x33,0x00,16)\r\n");
            Send("[123]i2c read(CH1,0x33,0x10,16)\r\n");
            Send("[123]i2c read(CH1,0x33,0x20,16)\r\n");
            Send("[123]i2c read(CH1,0x33,0x30,16)\r\n");
            Thread.Sleep(10);
        //    string buffer = ReadString(100);

            return "";
        }

        public void Reset()
        {
            Send("[123]io chip set(cp1=0x0000)\r\n");
            Send("[123]io chip set(cp2=0x0000)\r\n");
        }
    }
}
