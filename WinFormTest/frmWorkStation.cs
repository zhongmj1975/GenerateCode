using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class frmWorkStation : Form
    {
        DragControl dragControl;
        public frmWorkStation()
        {
            InitializeComponent();

            dragControl = new DragControl(this);

            tsmiStationDetail.Click += TsmiStationDetail_Click;
            tsmiFullScreen.Click += TsmiFullScreen_Click;

        }

        private void TsmiFullScreen_Click(object sender, EventArgs e)
        {
            frmWorkStation temp = this;
            temp.Parent = null;
            temp.TopLevel = true;
            //temp.ShowDialog();
        }

        private void TsmiStationDetail_Click(object sender, EventArgs e)
        {
            frmMain main = new frmMain();
            main.ShowDialog();
        }


        private void label23_MouseDown(object sender, MouseEventArgs e)
        {
             if(e.Button==MouseButtons.Left)
            {
                cmsWorkStation.Show(label23, e.X, e.Y);
            }
        }
    }
}
