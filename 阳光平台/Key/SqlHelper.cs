using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace SHYSInterface
{
    /// <summary>
    /// 数据库工具类
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet Execute(string connection, string sql)
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            DataSet dataSet = new DataSet();
            try 
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                //sqlCommand.ExecuteNonQuery();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
            return dataSet;
        }

        public static DataSet Query(string connection, string tableName, string key, Dictionary<string, string> wheres,
          string orderBy)
        {
            List<string> keys = new List<string>();
            keys.Add(key);
            return Query(connection, tableName, keys, wheres, orderBy);
        }

        /// <summary>
        /// 查询语句
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="keys">数据库表字段</param>
        /// <param name="wheres">数据库表筛选项</param>
        /// <param name="orderBy">数据库表排序</param>
        /// <returns></returns>
        public static DataSet Query(string connection, string tableName, List<string> keys,Dictionary<string,string> wheres,
            string orderBy)
        {
            StringBuilder sql = new StringBuilder("SELECT ");
            int index = 0;
            if (keys.Count == 0)
            {
                sql.Append(" * ");
            }
            else
            {
                foreach (var value in keys)
                {
                    sql.Append(value);
                    if (index++ != keys.Count - 1)
                    {
                        sql.Append(",");
                    }
                }
            }
            sql.Append(" FROM ").Append(tableName);
            if (wheres != null && wheres.Count > 0)
            {
                sql.Append(" WHERE ");
                index = 0;
                foreach (var dict in wheres)
                {
                    sql.Append(dict.Key).Append(" = ").Append("'").Append(dict.Value).Append("'");
                }
                if (index++ != wheres.Count - 1)
                {
                    sql.Append(" AND ");
                }
            }
            if (orderBy != null && orderBy.Length > 0)
            {
                sql.Append(" ORDER BY ").Append(orderBy);
            }
            return Execute(connection, sql.ToString());
        }


        /// <summary>
        /// 插入表格记录
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="values">数据库表字段和值</param>
        public static void Insert(string connection, string tableName,Dictionary<string, string> values)
        {
            if (values.Count == 0)
            {
                return;
            }
            int index = 0;
            StringBuilder sql = new StringBuilder("INSERT INTO ");
            sql.Append(tableName).Append(" (");
            foreach (var dict in values)
            {
                sql.Append(dict.Key);
                if (index++ != values.Count - 1)
                {
                    sql.Append(",");
                }
            }
            sql.Append(" ) VALUES (");
            index = 0;
            foreach (var dict in values)
            {
                sql.Append("'").Append(dict.Value).Append("'");
                if (index != values.Count - 1)
                {
                    sql.Append(",");
                }
                index++;
            }
            sql.Append(" )");
            Execute(connection, sql.ToString());
        }

         /// <summary>
         /// 更新数据库记录
         /// </summary>
         /// <param name="connection">数据库连接字符串</param>
         /// <param name="tableName">数据库表名</param>
         /// <param name="values">数据库表字段和值</param>
        /// <param name="wheres">数据库表筛选项</param>
        public static void Update(string connection, string tableName, Dictionary<string, string> values,
           Dictionary<string, string> wheres)
        {
            if (values.Count == 0 || wheres.Count == 0)
            {
                return;
            }
            int index = 0;
            StringBuilder sql = new StringBuilder("UPDATE ");
            sql.Append(tableName).Append(" SET ");
            foreach (var dict in values)
            {
                sql.Append(dict.Key).Append("=").Append("'").Append(
                        dict.Value).Append("'");
                if (index++ != values.Count - 1)
                {
                    sql.Append(",");
                }
            }
            sql.Append(" WHERE ");
            index = 0;
            foreach (var dict in wheres)
            {
                sql.Append(dict.Key).Append("=").Append("'").Append(
                        dict.Value).Append("'");
                if (index++ != wheres.Count - 1)
                {
                    sql.Append("AND");
                }
            }
            Execute(connection, sql.ToString());
        }

        /// <summary>
        /// 删除数据库记录
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="wheres">数据库表筛选项</param>
        public static void Delete(string connection, string tableName,Dictionary<string, string> wheres)
        {
            if (wheres.Count == 0)
            {
                return;
            }
            int index = 0;
            StringBuilder sql = new StringBuilder("DELTTE FROM ");
            sql.Append(tableName).Append(" WHERE ");
            foreach (var dict in wheres)
            {
                sql.Append(dict.Key).Append("=").Append("'").Append(
                        dict.Value).Append("'");
                if (index++ != wheres.Count - 1)
                {
                    sql.Append("AND");
                }
            }
            Execute(connection, sql.ToString());
        }
    }
}
