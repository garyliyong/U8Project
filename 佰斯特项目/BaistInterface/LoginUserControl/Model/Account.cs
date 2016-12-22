using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataBaseHelper;

namespace LoginUserControl.Model
{
    /// <summary>
    /// 帐套  UA_Account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 帐套ID
        /// </summary>
        public string cAcc_Id { get; set; }

        /// <summary>
        /// 帐套名称
        /// </summary>
        public string cAcc_Name { get; set; }

        /// <summary>
        /// 帐套年份
        /// </summary>
        public string iYear { get; set; }

        /// <summary>
        /// 帐套月份
        /// </summary>
        public string iMonth { get; set; }

        /// <summary>
        /// 获取帐套信息
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            return "[" + cAcc_Id + "]" + cAcc_Name;
        }

        /// <summary>
        /// u8数据库表名
        /// </summary>
        public static string TableName
        {
            get
            {
                return "UA_Account";
            }
        }

        /// <summary>
        /// 数据库查询关键字
        /// </summary>
        public static List<string> Keys
        {
            get
            {
                List<string> keys = new List<string>();
                keys.Add("cAcc_Id");
                keys.Add("cAcc_Name");
                keys.Add("iYear");
                keys.Add("iMonth");
                return keys;
            }
        }

        /// <summary>
        /// 当前登录帐套
        /// </summary>
        public static Account Current { get; set; }

        /// <summary>
        /// 获取所有的帐套号
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAccountIds()
        {
            List<string> ids = new List<string>();
            foreach (Account account in Accounts)
            {
                ids.Add(account.cAcc_Id);
            }
            return ids;
        }

        private static List<Account> accounts = null;
        /// <summary>
        /// 获取所有的帐套
        /// </summary>
        public static List<Account> Accounts
        {
            get
            {
                if (accounts == null)
                {
                    string sqlConnString = "server=" + LoginSettingInfo.Server + ";" +
                    "database=UFSystem;" +
                    "uid=sa;" + "pwd=" + LoginSettingInfo.Pwd + ";";
                    DataSet ds = SqlHelper.Query(sqlConnString, Account.TableName, Account.Keys, null, null);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        accounts = new List<Account>();
                        for (int row = 0; row < ds.Tables[0].Rows.Count; ++row)
                        {
                            Account account = new Account();
                            account.cAcc_Id = ds.Tables[0].Rows[row]["cAcc_Id"].ToString();
                            account.cAcc_Name = ds.Tables[0].Rows[row]["cAcc_Name"].ToString();
                            account.iYear = ds.Tables[0].Rows[row]["iYear"].ToString();
                            account.iMonth = ds.Tables[0].Rows[row]["iMonth"].ToString();
                            accounts.Add(account);
                        }
                    }
                }
                return accounts;
            }
        }
    }
}
