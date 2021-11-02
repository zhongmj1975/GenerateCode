using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Utilities
{

    public class SystemInfo
    {
        public static string UpdateURL = "http://sass.jwvdp.com/AppUpdate/";
        public static string[] CaseCodeCatories = { "随机码", "日期码", "固定值", "顺序码", "箱顺序号", "开始盒号","结束盒号","开始包号","结束包号" };

        public static string [] BoxCodeCatories = { "随机码", "日期码", "固定值", "顺序码", "箱顺序号", "开始包号", "结束包号" };

        public static string[] PackageCodeCatories = { "随机码", "日期码", "固定值", "顺序码", "箱顺序号", "盒码序号"};

        public static string[] ClearCodeCatories = { "随机码", "日期码", "固定值", "顺序码", "箱顺序号", "盒码序号","包码序号" };

        public static string[] CiperCodeCatories = { "随机码", "日期码", "固定值", "顺序码" };

        public static string[] SystemUserType = { "系统管理员", "业务用户" };
        public static string[] LotteryType = { "公共彩票","专用彩票" };

        public static string ZipPassword = "";

        public static string strInPort = "InPort";

        public static string strOutPort = "OutPort";

        public static string strReturnPort = "ReturnPort";

        public static string strScrap = "ScrapPort";

        public static string strRecoveryPort = "RecoveryPort";

        public static string strServer = "Server";

        public static string strDataBase = "DataBase";

        public static string strUser = "User";

        public static string strPassword = "Password";

        public static string LoginUser = "LoginUser";

        public static string LoginPass = "LoginPass";

        public static string SavePass = "SavePass";

        public static bool LogOned = false;

        public static int OnLineState = 0;

        public static string ChannelUser = "";

        public static string ChannelPass = "";

        public static string CurrentUserName = string.Empty;

        public static string CurrentPassword = string.Empty;

        public static bool RememberPassword = true;

        public static bool AutoLogOn = false;

        public static bool EncryptClientPassword = true;

        public static string ServiceUserName = "Framework";

        public static string ServicePassword = "Framework654123";

        public static bool LoadAllUser = true;

        public static bool OrganizeDynamicLoading = true;

        public static bool MultiLanguage = false;

        public static string CurrentLanguage = "zh-CN";

        public static string LisenceStatus = "";

        public static string MachineNumber = "";

        public static string ExpireDate = "";

        public static string TerminalCount = "0";

        public static string Themes = string.Empty;

        private int lockWaitMinute = 60;

        public static string WebHostUrl = "WebHostUrl";

        public static bool ShowExceptionDetail = true;

        public static bool ShowSuccessMsg = true;

        public static int PageSize = 50;

        public static bool AllowUserToRegister = true;

        public static bool UseMessage = false;

        public static bool EnableUserAuthorizationScope = false;

        public static bool EnableUserAuthorization = true;

        public static bool EnableModulePermission = false;

        public static bool EnablePermissionItem = false;

        public static bool EnableTableConstraintPermission = false;

        public static bool EnableTableFieldPermission = true;

        public static bool EnableOrganizePermission = false;

        public static bool EnableHandWrittenSignature = true;

        public static bool EnableRecordLogOnLog = true;

        public static bool EnableRecordLog = true;

        public static bool UpdateVisit = true;

        public static int OnLineLimit = 0;

        public static bool EnableCheckIPAddress = false;

        public static bool LogException = true;

        public static bool LogSQL = false;

        public static bool EventLog = false;

        public static string DefaultPassword = "abcd1234";

        public static bool EnableCheckPasswordStrength = false;

        public static bool EnableEncryptServerPassword = true;

        public static int PasswordMiniLength = 6;

        public static bool NumericCharacters = true;

        public static int PasswordChangeCycle = 3;

        public static bool CheckOnLine = false;

        public static int AccountMinimumLength = 4;

        public static int PasswordErrowLockLimit = 5;

        public static int PasswordErrowLockCycle = 30;

        public static int ServiceInterval = 3000;

        public static int PerCount = 500000;

        public static bool EncryptDbConnection = false;

        public static string DbConection = string.Empty;

        public static string WorkFlowDbConnectionString = string.Empty;

        public static int ProcedureTimeOut = 3000;

        public static int PerFileExportCount = 500000;

        public static int PerExportReadCount = 100000;

        public static int PerThreadCodeCount = 500000; //每个线程生码数量

        public static string ExportSplitChar = "";

        public static int ImportPerSaveCount = 500000;

        public static string ImportSplictChar = "";

        public static bool NeedRegister = true;

        public static int OnLineTime0ut = 140;

        public static int OnLineCheck = 60;

        public static string RegisterKey = string.Empty;

        public static string StartupPath = string.Empty;

        public static bool MatchCase = true;

        public static int TopLimit = 200;

        public static int LockNoWaitCount = 5;

        public static int LockNoWaitTickMilliSeconds = 30;

        public static bool ServerCache = false;

        public static string LastUpdate = "2012.05.08";

        public static string Version = "1.1";

        public static bool ShowInformation = true;

        public static string TimeFormat = "HH:mm:ss";

        public static string DateFormat = "yyyy-MM-dd";

        public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public static string SoftName = string.Empty;

        public static string SoftFullName = string.Empty;

        public static string RootMenuCode = string.Empty;

        public static bool ClientCache = false;

        public static string CustomerCompanyName = string.Empty;

        public static string AppIco = "Resource\\App.ico";

        public static string AddInDirectory = "AddIn\\";

        public static string DataExportPath = "";

        public static string RegisterException = "请您联系：Jacky.zhong QQ:34429581 手机：13925938475 电子邮件：34429581@qq.com 对软件进行注册。";

        public static string CustomerPhone = "";

        public static string CompanyName = "";

        public static string CompanyPhone = "13925938475";

        public static string Copyright = "Copyright 2009-2021 Jacky.Zhong";

        public static string BugFeedback = "mailto:sirzmj@@163.com?subject=On the UMPllatForm feedback&body=Here to enter your valuable feedback";

        public static string IEDownloadUrl = "http://download.microsoft.com/download/ie6sp1/finrel/6_sp1/W98NT42KMeXP/CN/ie6setup.exe";

        public static string HelpNamespace = string.Empty;

        public static string UploadDirectory = "Document/";

        public static string MainAssembly = string.Empty;

        public static string MainForm = "FrmMaiForm";

        public static string LogOnAssembly = "NET";

        public static string LogOnForm = "FrmLogOn";

        public static string ServiceFactory = "ServiceFactory";

        public static string Service = "ServiceAdapter";

        public static string DbProviderAssmely = "Utilities";

        public static string CurrentStyle = "VisualStudio2010Blue";

        public static string CurrentThemeColor = string.Empty;

        public static string ErrorReportFrom = "34429581@qq.com";

        public static string ErrorReportPort = "465";

        public static string ErrorReportMailServer = "smtp.163.com";

        public static string ErrorReportMailUserName = "sirzmj@1636.com";

        public static string ErrorReportMailPassword = "umplatform2012";

        public static string VoiceService = "";

        public static string LogistQueryInterface = "umplatform2012";

        public static string DyFrameworkBlog = "http://www.cnblogs.com/huyong/";

        public static string DyFrameworkWeibo = "http://t.qq.com/yonghu86";

        public static string Appid = "";

        public static string AppSecret = "";

        public static string MCHID = "";

        public static string Key = "";

        public static string Notify_Url = "";

        public static string SMSInterface = "";

        public static string VerfyCodeInterface = "";

        private static string sysbasedatamodule = "('车辆暂扣')";

        public static string NodeTimeout = "72";


        public static bool isLogin = false;

        public int LockWaitMinute
        {
            get
            {
                return lockWaitMinute;
            }
            set
            {
                lockWaitMinute = value;
            }
        }

    }

}
