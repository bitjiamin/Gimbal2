using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Gimbal
{
    class TCPClient
    {
        TcpClient client = new TcpClient();
        private const int bufferSize = 100;
        NetworkStream sendStream;
        public delegate void showData(string msg);
        public string result="";
        private void ListenerServer()
        {
            do
            {
                try
                {
                    int readSize;
                    byte[] buffer = new byte[bufferSize];

                    lock (sendStream)
                    {

                        readSize = sendStream.Read(buffer, 0, bufferSize);

                    }
                    if (readSize == 0)
                        return;
                    
                    //result = Encoding.Default.GetString(buffer, 0, readSize);
                    char[] c_result = new char[8];
                    string s_result;
                    string a_result = string.Empty;
                    for(int i = 0; i<8;i++)
                    {
                        c_result[i] = (char)buffer[i];
                      //  s_result = Convert.ToInt32(c_result[i]).ToString();
                        s_result = Convert.ToString(c_result[i],16);
                        if(s_result.Length<2)
                        {
                            s_result = "0" + s_result;
                        }
                        a_result = a_result + s_result;

                    }
                    result = a_result;
                    //dispmsg.Invoke(new showData(rtbtxtShowData.AppendText), "服务端曰：" + Encoding.Default.GetString(buffer, 0, readSize) + "\n");
                }
                catch
                {
                    //rtbtxtShowData.Invoke(new showData(rtbtxtShowData.AppendText), "报错");
                }

                //result = string.Empty;
                //将缓存中的数据写入传输流
            } while (true);
        }

        public void tcpsend(string sendmsg)
        {
            if (client != null)
            {
                //要发送的信息
                if (sendmsg.Trim() == string.Empty)
                    return;
                string msg = sendmsg.Trim();
                //将信息存入缓存中
                byte[] buffer = Encoding.Default.GetBytes(msg);
                //lock (sendStream)
                //{
                sendStream.Write(buffer, 0, buffer.Length);
                //}
                //rtbtxtShowData.AppendText("发送给服务端的数据:" + msg + "\n");
                sendmsg = string.Empty;
            }
        }

        public void sendbytes(byte[] data)
        {
            if (client != null)
            {
                sendStream.Write(data, 0, data.Length);
            }
        }

        public bool tcpconnect(string ip,string port)
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
            if(client.Connected)
            {
                return true;
            }
            else
            {
                try
                {
                    client.Connect(ipadd, int.Parse(port));
                    sendStream = client.GetStream();
                }
                catch
                {
                    MessageBox.Show("Connect Fail!", "Error", MessageBoxButtons.OK);
                    return false;
                }
                //rtbtxtShowData.AppendText("开始连接服务端....\n");
                //rtbtxtShowData.AppendText("已经连接服务端\n");
                //获取用于发送数据的传输流

                Thread thread = new Thread(ListenerServer);
                thread.IsBackground = true; //设置为后台进程,这样当主线程退出时,这个线程就会退出
                thread.Start();
                return true;
            }
            
        }

    }
}
