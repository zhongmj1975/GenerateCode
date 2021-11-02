using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FSELink.Utilities
{
    public class DataTableHelper
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = CommonHelper.GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static List<T> RandomSortList<T>(List<T> ListT)
        {
            Random random = new Random();
            List<T> newList = new List<T>();
            //foreach (T item in ListT)
            //{
            //    int index = Convert.ToInt32(Math.Floor(random.NextDouble()*ListT.Count));
            //    T t = ListT[index];
            //    newList.Add(t);
            //}
            newList = ListT.OrderBy(t => Guid.NewGuid()).ToList();
            return newList;
        }


        public static List<T> RandomSortList<T>(List<T> ListT, int TakeCount)
        {
            Random random = new Random();
            List<T> newList = new List<T>();

            for (int ind = 0; ind < TakeCount; ind++)
            {
                int index = Convert.ToInt32(Math.Floor(random.NextDouble() * ListT.Count));
                T t = ListT[index];
                newList.Add(t);
            }
            random = null;
            newList = ListT.OrderBy(t => Guid.NewGuid()).Take(TakeCount).ToList();
            return newList;
        }

        public static DataTable CreateDataTableFromObject<T>(T t)
        {
            DataTable dtTemp = new DataTable();
            PropertyInfo[] propertyInfos = t.GetType().GetProperties();
            foreach(PropertyInfo info in propertyInfos)
                if(info.CanWrite) dtTemp.Columns.Add(new DataColumn(info.Name));
            return dtTemp;
        }
    }
}
