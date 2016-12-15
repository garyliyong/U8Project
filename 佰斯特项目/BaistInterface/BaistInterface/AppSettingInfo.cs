using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DataBaseHelper;
using System.Data;
using LoginUserControl.Model;
using LoginUserControl;

namespace BaistInterface
{
    public class AppSettingInfo
    {
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public static string Server
        {
            get
            {
                return ConfigurationSettings.AppSettings["server"].ToString();
            }
        }

        /// <summary>
        /// 数据库服务器登录密码
        /// </summary>
        public static string Pwd
        {
            get
            {
                return ConfigurationSettings.AppSettings["pwd"].ToString();
            }
        }

        /// <summary>
        /// 上次登录用户名
        /// </summary>
        public static string User
        {
            get
            {
                return ConfigurationSettings.AppSettings["user"].ToString();
            }

            set
            {
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfa.AppSettings.Settings["user"].Value = value;
                cfa.Save();
            }
        }

        /// <summary>
        /// 上次登录账号
        /// </summary>
        public static string AccId
        {
            get
            {
                return ConfigurationSettings.AppSettings["accid"].ToString();
            }

            set
            {
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfa.AppSettings.Settings["accid"].Value = value;
                cfa.Save();
            }
        }

        public static string Year
        {
            get
            {
                return ConfigurationSettings.AppSettings["year"].ToString();
            }
        }
    }
}
