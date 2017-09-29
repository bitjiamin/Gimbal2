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
    class SMU
    {
        TcpClient TCP = new TcpClient();
        NetworkStream sendStream;
        private const int bufferSize = 50;
        public string resultdata = "";
        string buffer;
        Thread thread;
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


                thread = new Thread(ListenerServer);
                thread.IsBackground = true;
                thread.Start();
                
                return true;
            }

        }
     
        public void PowerOn(string sendmsg)
        {
            buffer = null;
            if (TCP != null)
            {

                byte[] bufferSend = Encoding.Default.GetBytes(sendmsg);
                sendStream.Write(bufferSend, 0, bufferSend.Length);
                sendmsg = string.Empty;
            }

        }

        private void ListenerServer()
        {
            do
            {
                try
                {
                    int readSize;
                    byte[] bufferreceive = new byte[bufferSize];

                    lock (sendStream)
                    {
                        readSize = sendStream.Read(bufferreceive, 0, bufferSize);
                    }
                    if (readSize == 0)
                        return;

                    string resultreceive = Encoding.Default.GetString(bufferreceive, 0, bufferSize);
                    resultreceive = resultreceive.Replace("\0", "");
                    buffer = buffer + resultreceive;

                    resultdata = buffer;
                }
                catch
                {
                }
            } while (true);
           
        }

        public void PowerOff()
        {
            thread.Abort();
            TCP.Close();
        }

        public double Current()
        {
            return 0.0;
        }

        public double Voltage()
        {
            return 0.0;
        }
    }
}
