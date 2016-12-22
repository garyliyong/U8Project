using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DataBaseHelper;
using System.Data;
using LoginUserControl.Model;

namespace LoginUserControl
{
    public class LoginSettingInfo
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string SqlConnectionString
        {
            get
            {
                string sqlConnString = "server=" + LoginSettingInfo.Server + ";" +
                   "database=UFSystem;" +
                   "uid=sa;" + "pwd=" + LoginSettingInfo.Pwd + ";";
                List<string> keys = new List<string>();
                keys.Add("cAcc_Id");
                keys.Add("cDatabase");
                Dictionary<string, string> wheres = new Dictionary<string, string>();
                wheres.Add("cAcc_Id", Account.Current.cAcc_Id);
                DataSet ds = SqlHelper.Query(sqlConnString, "UA_AccountDatabase", keys, wheres, null);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    throw new Exception("帐套号" + Account.Current.cAcc_Id + "不存在");
                }
                string database = ds.Tables[0].Rows[0]["cDatabase"].ToString();
                if (Year.Length == 0)
                {
                    Year = LoginInfo.LoginDate.Year.ToString();
                }
                database = database.Replace(Account.Current.iYear, Year);
                return "server=" + Server + ";" +
                   "database=" + database + ";" +
                   "uid=sa;" + "pwd=" + Pwd + ";";
            }
        }

        /// <summary>
        /// 数据库服务器
        /// </summary>
        public static string Server { get; set; }

        /// <summary>
        /// 数据库服务器登录密码
        /// </summary>
        public static string Pwd { get; set; }

        /// <summary>
        /// 上次登录用户名
        /// </summary>
        public static string User { get; set; }

        /// <summary>
        /// 上次登录账号
        /// </summary>
        public static string AccId { get; set; }

        /// <summary>
        /// 帐套月份
        /// </summary>
        public static string Year { get; set; }
    }
}
