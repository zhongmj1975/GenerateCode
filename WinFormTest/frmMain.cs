using FSELink.Bussiness;
using FSELink.Entities;
using FSELink.SupperCode;
using FSELink.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class frmMain : Form
    {
        bool blStart = false;
        bool blInit = false;
        public frmMain()
        {
            try
            {
                InitializeComponent();
                ConfigurationHelper.GetConfig("System.config");
                blInit = true;
            }
            catch(Exception ex)
            {
                blInit = false;
                toolStripStatusLabel1.Text = ex.Message;
            }
      
        }


 
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            
            if(!blStart)
            {
                button2.Text = "停止服务";
                toolStripStatusLabel1.Text = "服务已启动";
                button2.BackColor = Color.Green;               
                statusStrip1.BackColor = Color.Green;
                blStart = true;
                new Task(ExportFile).Start();
                new Task(CreateCode).Start();
            }
            else
            {
                button2.Text = "启动服务";
                toolStripStatusLabel1.Text = "服务已停止";
                button2.BackColor = Color.Red;
                statusStrip1.BackColor = Color.Red;
                blStart = false;
                
            }
        }

       
        private async void ExportFile()
        {
            while(true)
            {
                SupperCodeHelper.IsServerStart = blStart;
                if (!blStart) return;
                Thread.Sleep(SystemInfo.ServiceInterval);
               await SupperCodeHelper.ExportFileAsync();
            }
        }

        private async void CreateCode()
        {
            while (true)
            {
                SupperCodeHelper.IsServerStart = blStart;
                if (!blStart) return;
                Thread.Sleep(SystemInfo.ServiceInterval);                
                await  SupperCodeHelper.GenerateCodeAsync();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(blStart)
            {
                if( MessageBox.Show("服务已启用，不能直接终止服务，是否需要退出生码系统？","提示信息",MessageBoxButtons.OKCancel,MessageBoxIcon.Information)==DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                    blStart = false;
            }
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right)
            {
                contextMenuStrip1.Show(this, e.X, e.Y);
            }
        }
    }
}
