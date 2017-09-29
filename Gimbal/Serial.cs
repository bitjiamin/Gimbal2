using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;


namespace Gimbal
{
    class Serial
    {
        //private SerialPort comm = new SerialPort();
        private StringBuilder builder = new StringBuilder();
        public string result;
        //添加事件注册
        
        public bool serial_open(SerialPort comm, string port,Int32 baudrate)
        {
            //comm.DataReceived += comm_DataReceived;
            comm.PortName = port;
            comm.BaudRate = baudrate;
            comm.DataBits = 8;
            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口
                //comm.Close();
                return true;
            }
            else
            {
                //关闭时点击，则设置好端口，波特率后打开
                try
                {
                    comm.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    //打开串口失败
                    MessageBox.Show("Serial error!", "Warning", MessageBoxButtons.OK);
                    return false;
                }
            }
        }

        public void serial_send(SerialPort comm,string senddata)
        {
            comm.WriteLine(senddata);         
        }

        public string serial_read(SerialPort comm)
        {
            string recv_data = "";
            int i = 0;
            while(comm.BytesToRead>0)
            {
                i++;
                int n = comm.BytesToRead;
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                //received_count += n;//增加接收计数
                comm.Read(buf, 0, n);//读取缓冲数据
                recv_data = recv_data + Encoding.ASCII.GetString(buf);
                Thread.Sleep(50);
                if(i>8)
                {
                    break;
                }
                
            }
            //先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            return recv_data;
        }

        public string serial_readmcu(SerialPort comm)
        {
            string recv_data = "";
            int i = 0;
            while (true)
            {
                i++;
                int n = comm.BytesToRead;
                byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                //received_count += n;//增加接收计数
                comm.Read(buf, 0, n);//读取缓冲数据
                recv_data = recv_data + Encoding.ASCII.GetString(buf);
                Thread.Sleep(50);
                if (i > 50)
                {
                    break;
                }

            }
            //先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            return recv_data;
        }

        void comm_DataReceived(SerialPort comm,object sender, SerialDataReceivedEventArgs e)
        {
            string s_result = "";
            int n = comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据

            comm.Read(buf, 0, n);//读取缓冲数据
            builder.Clear();//清除字符串构造器的内容
            
            //
            builder.Append(Encoding.ASCII.GetString(buf));

            result = s_result + builder.ToString();
        }

        public bool serial_close(SerialPort comm)
        {
            if (comm.IsOpen)
            {
                //打开时点击，则关闭串口
                comm.Close();
                return true;
            }
            else
            {
                return true;
            }
        }

    }
}
