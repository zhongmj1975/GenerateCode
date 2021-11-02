using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FSELink.Utilities;

namespace WinFormTest
{
    public partial class frmOrder : Form
    {


        int formCount = 1;
        public frmOrder()
        {
            InitializeComponent();
            ShowStation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            frmWorkStation main = new frmWorkStation();
            //main.FormBorderStyle = FormBorderStyle.None;
            main.TopLevel = false;
            flowLayoutPanel1.Controls.Add(main);
           // main.Text += formCount.ToString();
            main.Show();
            formCount++;
        }

        private void ShowStation(int stationCount = 10)
        {
            for (int ind = 0; ind < stationCount; ind++)
            {

                frmWorkStation main = new frmWorkStation();
                //main.FormBorderStyle = FormBorderStyle.None;
                main.TopLevel = false;
                flowLayoutPanel1.Controls.Add(main);
                main.Show();
            }
        }


        private void frmTest_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmWorkStation station = new frmWorkStation();
            station.Show();
        }
    }
}
