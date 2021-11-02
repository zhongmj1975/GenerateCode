using FSELink.SupperCode;
using FSELink.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace FSELink.ReleaseCode
{
    public partial class MSZZ_ReleaseCode : ServiceBase
    {
        bool blStart = false;
        public MSZZ_ReleaseCode()
        {
            
            InitializeComponent();            
            
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ConfigurationHelper.GetConfig(AppDomain.CurrentDomain.BaseDirectory+"\\System.config");
                blStart = true;
                new Task(ExportFile).Start();
                new Task(CreateCode).Start();
                LogHelper.WriteLog("码上增值数据发布、导出服务已启动！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                blStart = false;
                this.Stop();
            }
        }


        protected override void OnStop()
        {
            blStart = false;
            this.Stop();
            LogHelper.WriteLog("码上增值数据发布、导出服务已停止！");
        }


        private void ExportFile()
        {
            while (true)
            {
                SupperCodeHelper.IsServerStart = blStart;
                if (!blStart) return;
                Thread.Sleep(SystemInfo.ServiceInterval);
                SupperCodeHelper.ExportFileAsync();
            }
        }

        private void CreateCode()
        {
            while (true)
            {
                SupperCodeHelper.IsServerStart = blStart;
                if (!blStart) return;
                Thread.Sleep(SystemInfo.ServiceInterval);

                SupperCodeHelper.GenerateCodeAsync();
            }
        }


    }
}
