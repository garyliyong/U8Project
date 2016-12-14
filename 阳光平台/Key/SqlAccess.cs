using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SHYSInterface
{
    class SqlAccess
    {
        /// <summary>
        /// 连接sql server数据库
        /// </summary>
        /// <param name="sqlconnstr"></param>
        /// <returns></returns>
        public static SqlConnection SqlConn(string sqlconnstr)
        {
            SqlConnection sqlconn = null;
            try
            {
                sqlconn = new SqlConnection(sqlconnstr);
                if (sqlconn.State.ToString().ToLower() != "open")
                {
                    sqlconn.Open();
                }
                return sqlconn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="Account"></param>
        public static void ExecuteSql(string sqlcmd, string Account)
        {
            SqlConnection conn = new SqlConnection(Account);
            SqlCommand cmd;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="conn"></param>
        public static void ExecuteSql(string sqlcmd, SqlConnection conn)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="tran"></param>
        public static void ExecuteSql(string sqlcmd, SqlTransaction tran)
        {
            SqlConnection conn = tran.Connection;
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand(sqlcmd, conn, tran);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回一个值
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlcmd, string Account)
        {
            SqlConnection conn = new SqlConnection(Account);
            SqlCommand cmd;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                object obj = cmd.ExecuteScalar();
                conn.Close();
                return obj;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 返回一个值
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlcmd, SqlTransaction tran)
        {
            SqlConnection conn = tran.Connection;
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand(sqlcmd, conn, tran);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回一个值
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="conn"></param>
        /// <returns></returns>

        public static object ExecuteScalar(string sqlcmd, SqlConnection conn)
        {
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                object obj = cmd.ExecuteScalar();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static DataTable ExecuteSqlDataTable(string sqlcmd, string Account)
        {
            SqlConnection conn = new SqlConnection(Account);
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            SqlCommand cmd;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static DataTable ExecuteSqlDataTable(string sqlcmd, SqlTransaction tran)
        {
            SqlConnection conn = tran.Connection;
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand(sqlcmd, conn, tran);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sqlcmd"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DataTable ExecuteSqlDataTable(string sqlcmd, SqlConnection conn)
        {
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand(sqlcmd, conn);
                cmd.CommandTimeout = 80000;
                cmd.CommandType = CommandType.Text;
                cmd.Prepare();
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
