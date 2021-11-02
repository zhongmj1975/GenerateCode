using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace FSELink.Utilities
{
    public class ObjectHelper
    {

        /// <summary>
        /// Set object's property value
        /// 
        /// Code was created by Jacky.zhong on 2017-11-09
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="adaptedRow"></param>
        /// <param name="propertyInfo"></param>

        public static string GetPropertyValue(object entity, string properytname, string value)
        {
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();
            string strTemp = "";
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name.Contains(properytname.ToString()))
                {
                    strTemp=propertyInfo.GetValue(entity,null).ToString();
                    break;
                }
            }
            return strTemp;
        }


        /// <summary>
        /// Set object's property value
        /// 
        /// Code was created by Jacky.zhong on 2017-11-09
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="adaptedRow"></param>
        /// <param name="propertyInfo"></param>

        public static void SetPropertyValue(object entity,string pos,string value )
        {
             PropertyInfo [] propertyInfos = entity.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name.Contains(pos.ToString()))
                {
                    if (propertyInfo.PropertyType == typeof(DateTime?) ||
                        propertyInfo.PropertyType == typeof(DateTime))
                    {
                        DateTime date = DateTime.MaxValue;
                        DateTime.TryParse(value,
                            CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

                        propertyInfo.SetValue(entity, date, null);
                        return;
                    }
                    else if (propertyInfo.PropertyType == typeof(int?) || propertyInfo.PropertyType == typeof(int))
                    {
                        propertyInfo.SetValue(entity, Convert.ToInt32(value), null);
                        return;
                    }
                    else
                    {
                        propertyInfo.SetValue(entity, value.ToString(), null);
                        return;
                    }
                }
            }
        }

    }
}
