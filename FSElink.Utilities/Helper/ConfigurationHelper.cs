using System;
using System.Collections.Generic;
using System.Text;

namespace FSELink.Utilities
{
    public class ConfigurationHelper
    {
 
        public static void GetConfig(string configFile)
        {
            SystemInfo.CustomerCompanyName = ConfigHelper.GetValue(configFile, "CustomerCompanyName");
            SystemInfo.SoftName = ConfigHelper.GetValue(configFile, "SoftName");
            SystemInfo.SoftFullName = ConfigHelper.GetValue(configFile, "SoftFullName");
            SystemInfo.Copyright = ConfigHelper.GetValue(configFile, "Copyright");
            SystemInfo.Version = ConfigHelper.GetValue(configFile, "Version");
            SystemInfo.Version = ConfigHelper.GetValue(configFile, "Version");
            SystemInfo.CurrentLanguage = ConfigHelper.GetValue(configFile, "Languange");
            SystemInfo.DataExportPath = ConfigHelper.GetValue(configFile, "DataExportPath");
            SystemInfo.ZipPassword = ConfigHelper.GetValue(configFile, "ZipPassword");
            SystemInfo.ServiceInterval =Convert.ToInt32(ConfigHelper.GetValue(configFile, "ServiceInterval"));
            SystemInfo.PerThreadCodeCount = Convert.ToInt32(ConfigHelper.GetValue(configFile, "PerThreadCodeCount"));
            SystemInfo.PerFileExportCount = Convert.ToInt32(ConfigHelper.GetValue(configFile, "PerFileExportCount"));

            if (ConfigHelper.GetValue(configFile, "EncryptDbConnection").ToUpper() == "TRUE")
            {
                SystemInfo.EncryptDbConnection = true;
            }
            else
            {
                SystemInfo.EncryptDbConnection = false;
            }

                SystemInfo.DbConection = ConfigHelper.GetValue(configFile, "SqlConnStr");  
            
            if (SystemInfo.EncryptDbConnection)
            {
                
                SystemInfo.DbConection = SecretHelper.AESDecrypt(SystemInfo.DbConection);
                string aas = SecretHelper.AESEncrypt(SystemInfo.DbConection);
            }

        }
    }
}
