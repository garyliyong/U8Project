using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Key
{
   public class DBHelper
    {


        public DBHelper()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetTable(string sql, string connection)
        {
            try
            {
                SqlDataAdapter ad = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <param name="sql">有效的insert update delete语句</param>
        /// <returns>影响的行数</returns>
        public static int Morage(string sql, string connection)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = sql;
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
       
        /// <summary>
        /// 查找特定字段的值
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="other">字段名</param>
        /// <param name="strwhere">条件</param>
        /// <returns>string</returns>
        public static string FindOther(string other, string tablename, string strwhere, string connection)
        {
            string sql = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    sql = string.Format(" select {0} as other from {1} where {2} ", other, tablename, strwhere);
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = con;
                        cmd.CommandText = sql;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return reader["other"].ToString();
                        }
                        else
                        {
                            
                            return "";
                            
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        /// <summary>
        /// 检查物品是否存在
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="where">条件</param>
        /// <returns>bool</returns>
        public static bool Exits(string tablename, string where, string connection)
        {
            string strsql = "";
            try
            {
                using (SqlConnection con = new SqlConnection(connection))
                {
                    strsql = string.Format(" select * from {0} ", tablename);
                    if (where != "")
                    {
                        strsql += " WHERE " + where;
                    }
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = strsql;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch
            {

                return false;
            }
        }

    }
}
