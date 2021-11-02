using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FSELink.Utilities
{
    public class DataBase
    {

        string strServer;
        string strUserId;
        string strPwd;
        string strBaseName;
        string strConn;

        SqlConnection m_conn = new SqlConnection();
        SqlCommand m_Cmd = new SqlCommand();

        /// <summary>
        /// 创建数据库连接和命令对象
        /// </summary>
        public DataBase()
        {
            strServer = CConfig.DBServer;  //数据库服务器
            strUserId = CConfig.DBUser;   //数据库登录用户
            strPwd = CConfig.DBPassword;     //数据库密码
            strBaseName = CConfig.DBName;  //数据库名称
            strConn = "Server=" + strServer + ";Database=" + strBaseName + ";User id=" + strUserId + ";PWD=" + strPwd+ ";Connect Timeout=1000";

            try
            {
                m_conn = new SqlConnection(strConn);
                m_Cmd = new SqlCommand();
                m_Cmd.Connection = m_conn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 多条Transact-SQL语句提交数据库
        /// </summary>
        /// <param name="strSqls"></param>
        /// <returns></returns>
        public bool ExecDataBySqls(List<string> strSqls)
        {
            bool boolsSucceed;
            if (m_conn.State == ConnectionState.Closed)
            {
                m_conn.Open();
            }
            SqlTransaction sqltran = m_conn.BeginTransaction();
            try
            {
                m_Cmd.Transaction = sqltran;
                m_Cmd.CommandTimeout = 3600;
                foreach (string s in strSqls)
                {
                    m_Cmd.CommandType = CommandType.Text;
                    m_Cmd.CommandText = s;
                    m_Cmd.ExecuteNonQuery();
                }
                sqltran.Commit();
                boolsSucceed = true;
            }
            catch (Exception ex)
            {
                throw ex;
                sqltran.Rollback();
                boolsSucceed = false;
                throw ex;
            }
            finally
            {
                m_conn.Close();
                strSqls.Clear();
            }
            return boolsSucceed;
        }

        /// <summary>
        /// Transact-SQL语句提交数据库
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExecDataBySql(string strSql)
        {
            if (m_conn.State == ConnectionState.Closed)
            {
                m_conn.Open();
            }
            try
            {
                m_Cmd.CommandType = CommandType.Text;
                m_Cmd.CommandText = strSql;
                return m_Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
                //return -1 ;
            }
            finally
            {
                m_conn.Close();
            }
        }

        /// <summary>
        /// 通过Transact-SQL语句得到SqlDataReader实例
        /// </summary>
        /// <param name="strSql">Transact-SQL语句</param>
        /// <returns></returns>
        public SqlDataReader GetDataReader(string strSql)
        {
            SqlDataReader sdr;
            m_Cmd.CommandType = CommandType.Text;
            m_Cmd.CommandText = strSql;
            try
            {
                if (m_conn.State == ConnectionState.Closed)
                {
                    m_conn.Open();
                }
                //执行Transact-SQL语句，若SqlDataReader 对象关闭，则对应的数据连接也关闭
                sdr = m_Cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                throw e;
            }
            return sdr;
        }

        public DataTable GetDataTable(string strSqlCode, string strTableName)
        {
            DataTable dt = null;
            SqlDataAdapter sda = null;
            try
            {
                sda = new SqlDataAdapter(strSqlCode, m_conn);
                dt = new DataTable(strTableName);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }

        public bool Test(string user, string password, string server, string dbname)
        {
            strConn = "Server=" + server + ";Database=" + dbname + ";User id=" + user + ";PWD=" + password;
            m_conn = new SqlConnection(strConn);
            try
            {
                m_conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                if (m_conn.State == System.Data.ConnectionState.Open)
                    m_conn.Close();
            }
        }

        #region SqlBulkCopy批量插入数据
        /// <summary>
        /// 执行SqlBulkCopy批量插入，执行事务。
        /// </summary>
        /// <param name="connectionString">数据连接</param>
        /// <param name="TableName">表名</param>
        /// <param name="dt">要插入的数据</param>
        /// <returns></returns>
        public int SqlBulkCopy(string TableName, DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(strConn, SqlBulkCopyOptions.UseInternalTransaction))
                {

                    sqlbulkcopy.DestinationTableName = TableName;
                    sqlbulkcopy.BulkCopyTimeout = 3600;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }
                    sqlbulkcopy.WriteToServer(dt);
                    return 1;

                }
            }
        }


       
        /// <summary>
        /// 执行SqlBulkCopy批量插入，执行事务。
        /// </summary>
        /// <param name="connectionString">数据连接</param>
        /// <param name="TableName">表名</param>
        /// <param name="dt">要插入的数据</param>
        /// <returns></returns>
        public int SqlBulkCopy(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(strConn, SqlBulkCopyOptions.UseInternalTransaction))
                {

                    sqlbulkcopy.DestinationTableName = dt.TableName;
                    sqlbulkcopy.BulkCopyTimeout = 3600;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sqlbulkcopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                    }
                    sqlbulkcopy.WriteToServer(dt);
                    return 1;

                }
            }
        }


        /// <summary>  
        /// 批量插入  
        /// </summary>  
        /// <typeparam name="T">泛型集合的类型</typeparam>  
        /// <param name="conn">连接对象</param>  
        /// <param name="tableName">将泛型集合插入到本地数据库表的表名</param>  
        /// <param name="list">要插入大泛型集合</param>  
        public static void BulkInsert<T>(SqlConnection conn, string tableName, IList<T> list)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open(); //打开Connection连接  
            }
            using (var bulkCopy = new SqlBulkCopy(conn))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = tableName;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T))

                    .Cast<PropertyDescriptor>()
                    .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                    .ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close(); //关闭Connection连接  
            }
        }
        #endregion
    }
}
