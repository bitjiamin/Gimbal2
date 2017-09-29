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
    class ScannerCommunications
    {
        TcpClient TCP = new TcpClient();
        string __SN;
        NetworkStream sendStream;
        private const int bufferSize = 50;
     
        string buffer;
        public ScannerCommunications()
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
        public void Scan()
        {
        }

        public void Send(string sendmsg)
        {
            buffer = null;
            if (TCP != null)
            {

                if (sendmsg.Trim() == string.Empty)
                    return;
                string msg = sendmsg.Trim();
                //将信息存入缓存中
                byte[] bufferSend = Encoding.Default.GetBytes(msg);
                //lock (sendStream)
                //{
                sendStream.Write(bufferSend, 0, bufferSend.Length);
                //}
                //rtbtxtShowData.AppendText("发送给服务端的数据:" + msg + "\n");
                sendmsg = string.Empty;

            }
        }

        public string result()
        {
            try
            {
                Thread.Sleep(1500);
                int readSize;
                byte[] buffer = new byte[bufferSize];
                lock (sendStream)
                {
                    readSize = sendStream.Read(buffer, 0, bufferSize);
                }

                UTF8Encoding encoding = new UTF8Encoding();
                __SN = encoding.GetString(buffer);

                __SN = __SN.Substring(0, readSize);
                __SN = __SN.Trim("\r\n".ToCharArray());
                return __SN;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error");
                return "ERROR";
            }
        
        }

        public void Close()
        {
            TCP.Close();
        }
    
    }
}
