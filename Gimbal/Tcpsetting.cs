using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gimbal
{
    public partial class Tcpsetting : Form
    {
        Operxml file = new Operxml();
        public Tcpsetting()
        {
            InitializeComponent();
        }

        private void Tcpsetting_Load(object sender, EventArgs e)
        {
            IP.Text=file.ReadXmlFile("/TCPconfig","IP");
            Port.Text = file.ReadXmlFile("/TCPconfig","Port");
        }

        private void Read_Click(object sender, EventArgs e)
        {
            IP.Text = file.ReadXmlFile("/TCPconfig","IP");
            Port.Text = file.ReadXmlFile("/TCPconfig","Port");
        }

        private void write_Click(object sender, EventArgs e)
        {
            file.CreateXmlFile(IP.Text, Port.Text);
        }

    }
}
