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
    public partial class FrmStart : Form
    {
        public FrmStart()
        {
            InitializeComponent();
        }

        private void FrmStart_Load(object sender, EventArgs e)
        {

        }

        public void UpdateMsg(string msg)
        {
            lbStart.Text = msg;
        }

    }
}
