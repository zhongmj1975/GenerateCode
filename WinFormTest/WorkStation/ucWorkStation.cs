using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScanSystem.Controller
{
    public partial class ucWorkStation : UserControl
    {
        public ucWorkStation()
        {
            InitializeComponent();
        }

        private Point mousePoint = new Point();
        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.mousePoint.X = e.X;
            this.mousePoint.Y = e.Y;
            this.BringToFront();
        }

        private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Control.MousePosition.Y - mousePoint.Y-165;
                this.Left = Control.MousePosition.X - mousePoint.X;
            }
        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            this.BringToFront();
        }


    }
}
