using FSELink.Utilities;
using NetCore.SqlELink;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace FSELink.DAL
{
    public class DbContext<T> where T : class, new()
    {
        public SqlELinkClient Db;//用来处理事务多表查询和复杂的操作
        public DbContext()
        {
            //CConfig.ConfigFile = "config.xml";
            //var fisss = CConfig.ConfigFile;
            Db = new SqlELinkClient(new ConnectionConfig()
            {
               // ConnectionString = "server=" + CConfig.DBServer + ";uid=" + CConfig.DBUser + ";pwd=" + CConfig.DBPassword + ";database=" + CConfig.DBName,
                ConnectionString =SystemInfo.DbConection,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            //Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    Console.WriteLine(sql + "\r\n" +
            //        Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            //    Console.WriteLine();
            //};

        }
        //注意：不能写成静态的
        
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据
        public SimpleClient<T> CurrentManager { get { return new SimpleClient<T>(Db); } }//用来处理T_RolePermission表的常用操作

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public async virtual Task<List<T>> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public async virtual Task<List<T>> GetList(Expression<Func<T, bool>> whereExpression)
        {
            return  CurrentDb.GetList(whereExpression);
        }



        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public async virtual Task<List<T>> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }


        /// <summary>
        /// 获取符合条件的记录数量
        /// </summary>
        /// <returns></returns>
        
        public async  Task<int> GetRowCount(Expression<Func<T,bool>> whereExpression)
        {
            return CurrentDb.Count(whereExpression);
        }



        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public async virtual Task<T> GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Delete(dynamic data)
        {
            return CurrentDb.Delete(data);
        }


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Delete(T data)
        {
            return CurrentDb.Delete(data);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Delete(dynamic[] ids)
        {
            return CurrentDb.AsDeleteable().In(ids).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Delete(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Update(List<T> objs)
        {
            return CurrentDb.UpdateRange(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Insert(T obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<bool> Insert(List<T> objs)
        {
            return CurrentDb.InsertRange(objs);
        }

        /// <summary>
        /// 获取实体对象的注解信息
        /// </summary>
        /// <param name="obj">实例化的对象</param>
        /// <returns></returns>
        public async virtual Task<List<KeyValuePair<string, string>>> GetCustomAttribute(T obj)
        {
            List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

            PropertyInfo[] peroperties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in peroperties)
            {
                ELinkColumn[] attrs = (ELinkColumn[])property.GetCustomAttributes(typeof(ELinkColumn));
             
                 foreach(ELinkColumn column in attrs)
                    keyValuePairs.Add(new KeyValuePair<string, string>(column.ColumnName, column.ColumnDescription));
            }
            return keyValuePairs;
           
        }



        public async Task<string []> GetProperyName(Type T)
        {
            var client = CurrentDb.AsELinkClient();
            var entityInfo = client.EntityMaintenance.GetEntityInfo<T>();
            string [] columns = entityInfo.Columns.Where(it => !it.IsIdentity).Select(it => it.DbColumnName).ToArray();
            return columns;
        }

            /// <summary>
        /// 此方法只适用于SQL Server 数据库
        /// </summary>
        /// <param name="list"></param>
        public async virtual void SqlBulkCopy(List<T> list)
        {
            var client = CurrentDb.AsELinkClient();
            var entityInfo = client.EntityMaintenance.GetEntityInfo<T>();
            var columns = entityInfo.Columns.Where(it => !it.IsIdentity).Select(it => it.DbColumnName).ToArray();
            var conn = client.Ado.Connection as SqlConnection;
            var tran = client.Ado.Transaction as SqlTransaction;
            if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
            System.Data.DataTable dtTemp = DataTableHelper.ToDataTable(list);
            using (var bcp = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
            {
                foreach (var col in columns)
                    bcp.ColumnMappings.Add(col.ToString(), col.ToString());
                bcp.BatchSize = 50000;
                bcp.BulkCopyTimeout = 3600;

                bcp.DestinationTableName = entityInfo.DbTableName;
                bcp.WriteToServer(dtTemp);
            }
        }


        /// <summary>
        /// 此方法只适用于SQL Server 数据库
        /// </summary>
        /// <param name="list"></param>
        public async virtual void SqlBulkCopy(System.Data.DataTable dtSource)
        {
            var client = CurrentDb.AsELinkClient();
            var entityInfo = client.EntityMaintenance.GetEntityInfo<T>();
            var columns = entityInfo.Columns.Where(it => !it.IsIdentity).Select(it => it.DbColumnName).ToArray();
            var conn = client.Ado.Connection as SqlConnection;
            var tran = client.Ado.Transaction as SqlTransaction;
            if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
          //  System.Data.DataTable dtTemp = DataTableHelper.ToDataTable(list);
            using (var bcp = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
            {
                foreach (var col in columns)
                    bcp.ColumnMappings.Add(col.ToString(), col.ToString());
                bcp.BatchSize = 50000;
                bcp.BulkCopyTimeout = 3600;

                bcp.DestinationTableName = entityInfo.DbTableName;
                bcp.WriteToServer(dtSource);
            }
        }




        public async  virtual Task<string> GetTableName(Type type)
        {
            string strTableName = "";
            var client = CurrentDb.AsELinkClient();
            var entityInfo = client.EntityMaintenance.GetEntityInfo<T>();
            strTableName = entityInfo.DbTableName;
            return strTableName;
        }

       
       


        //自已扩展更多方法 
    }


}
