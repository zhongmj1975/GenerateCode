using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace FSELink.Utilities
{
    public class ConfigHelper
    {
        //public static string AppSettings(string key)
        //{
        //    return ConfigurationManager.get_AppSettings()[key].ToString().Trim();
        //}

        //public static string ConnectionStrings(string name)
        //{
        //    return ConfigurationManager.get_ConnectionStrings().get_Item(name).get_ConnectionString()
        //        .Trim();
        //}

        public static string ConnectionStrings(string xmlFile, string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFile);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
            string text = "";
            XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xmlElement != null)
            {
                return xmlElement.GetAttribute("value");
            }

            throw new Exception("key关键字不存在");
        }

        public static void SetConnectionStrings(string xmlFile, string key, string value)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFile);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
            XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xmlElement != null)
            {
                xmlElement.SetAttribute("value", value);
                xmlDocument.Save(xmlFile);
            }
        }

        public static void SetValue(string key, string value)
        {
            XmlDocument xmlDocument = new XmlDocument();
           // xmlDocument.Load(HttpContext.get_Current().get_Server().MapPath("/XmlConfig/Config.xml"));
            XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
            XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xmlElement != null)
            {
                xmlElement.SetAttribute("value", value);
            }
            else
            {
                XmlElement xmlElement2 = xmlDocument.CreateElement("add");
                xmlElement2.SetAttribute("key", key);
                xmlElement2.SetAttribute("value", value);
                xmlNode.AppendChild(xmlElement2);
            }

           // xmlDocument.Save(HttpContext.get_Current().get_Server().MapPath("/XmlConfig/Config.xml"));
        }

        public static void SetValue(string xmlFile, string key, string value)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFile);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
            XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xmlElement != null)
            {
                xmlElement.SetAttribute("value", value);
            }
            else
            {
                XmlElement xmlElement2 = xmlDocument.CreateElement("add");
                xmlElement2.SetAttribute("key", key);
                xmlElement2.SetAttribute("value", value);
                xmlNode.AppendChild(xmlElement2);
            }

            xmlDocument.Save(xmlFile);
        }

        public static string GetValue(string xmlFile, string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFile);
            XmlNode xmlNode = xmlDocument.SelectSingleNode("//appSettings");
            string text = "";
            XmlElement xmlElement = (XmlElement)xmlNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xmlElement != null)
            {
                return xmlElement.GetAttribute("value");
            }

            throw new Exception("key关键字不存在");
        }
    }
}
