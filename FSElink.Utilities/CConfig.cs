using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FSELink.Utilities
{
   public class CConfig
    {
         
        #region base
        static CConfig()
        {
            ConfigFile ="config.xml";
        }

        public static string ConfigFile { get; set; }

        private static bool saveconfig(string key, string value)
        {
            try
            {
                //value = CPublicUtils.Encrypt(value);
                if (!File.Exists(ConfigFile))
                {
                    var xeRoot = new XElement("AppSetting");
                    xeRoot.Save(ConfigFile);
                }

                var xeR = XElement.Load(ConfigFile);
                XElement xe;
                if (xeR.Elements("KeyValue").Count() == 1)
                { xe = xeR.Elements("KeyValue").ToArray()[0]; }
                else
                { xe = new XElement("KeyValue"); xeR.Add(xe); }

                xe.SetElementValue(key, value);
                xeR.Save(ConfigFile);

                return true;
            }
            catch (Exception ex) { return false; }
        }

        private static string loadconfig(string key)
        {
            try
            {
                if (!File.Exists(ConfigFile))
                {
                    return "";
                }
                var xER = XElement.Load(ConfigFile);
                if (xER.Elements("KeyValue").ToArray()[0].Elements().Where((el) => el.Name == key).ToArray().Length < 1) return "";
                var xe = xER.Elements("KeyValue").ToArray()[0].Elements().Where((el) => el.Name == key).ToArray()[0];

                if (xe == null)
                    return "";
                else
                    return xe.Value; //return CPublicUtils.DesDecrypt(xe.Value);
            }
            catch (Exception ex) { return ""; }
        }

        #endregion

        public static bool ActiveDevice
        {
            get
            {
                var v = loadconfig("ActiveDevice").Trim();
                if (v=="True")
                {
                    v = "True";
                    saveconfig("ActiveDevice", v);
                }
                return Convert.ToBoolean(v);
            }
            set { saveconfig("ActiveDevice", value.ToString()); }
        }

        public static string Custom
        {
            get
            {
                var v = loadconfig("Custom");
                if (v == "")
                {
                    v = "默认客户";
                    saveconfig("Custom", v);
                }
                return v;
            }
            set { saveconfig("Custom", value); }
        }

        public static string DBUser
        {
            get
            {
                var v = loadconfig("DBUser");
                if (v == "")
                {
                    v = "sa";
                    saveconfig("DBUser", v);
                }
                return v;
            }
            set { saveconfig("DBUser", value); }
        }
        public static string DBPassword
        {
            get
            {
                var v = loadconfig("DBPassword");
                if (v == "")
                {
                    v = "";
                    saveconfig("DBPassword", v);
                }
                return v;
            }
            set { saveconfig("DBPassword", value); }
        }
        public static string DBServer
        {
            get
            {
                var v = loadconfig("DBServer");
                if (v == "")
                {
                    v = ".";
                    saveconfig("DBServer", v);
                }
                return v;
            }
            set { saveconfig("DBServer", value); }
        }
        public static string DBName
        {
            get
            {
                var v = loadconfig("DBName");
                if (v == "")
                {
                    v = "";
                    saveconfig("DBName", v);
                }
                return v;
            }
            set { saveconfig("DBName", value); }
        }
        public static string BoxPortName
        {
            get
            {
                var v = loadconfig("BoxPortName");
                if (v == "")
                {
                    v = "COM1";
                    saveconfig("BoxPortName", v);
                }
                return v;
            }
            set { saveconfig("BoxPortName", value); }
        }
        public static string CardPortName
        {
            get
            {
                var v = loadconfig("CardPortName");
                if (v == "")
                {
                    v = "COM1";
                    saveconfig("CardPortName", v);
                }
                return v;
            }
             set { saveconfig("CardPortName", value); }
        }
        public static int CardBaudRate
        {
            get
            {
                var v = loadconfig("CardBaudRate");
                if (v == "")
                {
                    v = "9600";
                    saveconfig("CardBaudRate", v);
                }
                return Convert.ToInt32(v);
            }
             set { saveconfig("CardBaudRate", value + ""); }
        }
        public static int BoxBaudRate
        {
            get
            {
                var v = loadconfig("BoxBaudRate");
                if (v == "")
                {
                    v = "9600";
                    saveconfig("BoxBaudRate", v);
                }
                return Convert.ToInt32(v);
            }
            set { saveconfig("BoxBaudRate", value + ""); }
        }
        public static string MacNumber
        {
            get
            {
                var v = loadconfig("MacNumber");
                if (v == "")
                {
                    v = "一号机台";
                    saveconfig("MacNumber", v);
                }
                return v;
            }
            set { saveconfig("MacNumber", value); }
        }


        public static string CardPre
        {
            get
            {
                var v = loadconfig("CardPre");
                if (v == "")
                {
                    v = "ulq";
                    saveconfig("CardPre", v);
                }
                return v;
            }
            set { saveconfig("CardPre", value); }
        }
        public static int GroupCountPre
        {
            get
            {
                var v = loadconfig("GroupCountPre");
                if (v == "")
                {
                    v = "250";
                    saveconfig("GroupCountPre", v);
                }
                return Convert.ToInt32( v );
            }
            set { saveconfig("GroupCountPre", value+""); }
        }
        public static int GroupCount
        {
            get
            {
                var v = loadconfig("GroupCount");
                if (v == "")
                {
                    v = "2";
                    saveconfig("GroupCount", v);
                }
                return Convert.ToInt32(v);
            }
            set { saveconfig("GroupCount", value + ""); }
        }
        public static int BitCount
        {
            get
            {
                var v = loadconfig("BitCount");
                if (v == "")
                {
                    v = "5";
                    saveconfig("BitCount", v);
                }
                return Convert.ToInt32(v);
            }
             set { saveconfig("BitCount", value + ""); }
        }
        public static int NoBeg
        {
            get
            {
                var v = loadconfig("NoBeg");
                if (v == "")
                {
                    v = "0";
                    saveconfig("NoBeg", v);
                }
                return Convert.ToInt32(v);
            }
             set { saveconfig("NoBeg", value + ""); }
        }
        public static int NoEnd
        {
            get
            {
                var v = loadconfig("NoEnd");
                if (v == "")
                {
                    v = "0";
                    saveconfig("NoEnd", v);
                }
                return Convert.ToInt32(v);
            }
             set { saveconfig("NoEnd", value + ""); }
        }

    }
}
