using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace SHYSInterface
{
    static class Program
    {
        public static string ConnectionString = string.Empty;
        public static string cacc_id = string.Empty;
        public static string userCode = string.Empty;
        public static string userPwd = string.Empty;
        public static string server = string.Empty;
        //public static string CRMConnectionString = string.Empty;
        //public static String ls_error;
        //public static string startdate = "";
        //public static string enddate = "";
        //public static string officeCode = "";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConnectionString = ConfigurationSettings.AppSettings["SqlConnectionString"].ToString();
            cacc_id = ConfigurationSettings.AppSettings["cacc_id"].ToString();
            userCode = ConfigurationSettings.AppSettings["userCode"].ToString();
            userPwd = ConfigurationSettings.AppSettings["userPwd"].ToString();
            server = ConfigurationSettings.AppSettings["server"].ToString();
            //CrmPort = ConfigurationSettings.AppSettings["crmport"].ToString();
            //startdate = ConfigurationSettings.AppSettings["startdate"].ToString();
            //enddate = ConfigurationSettings.AppSettings["enddate"].ToString();
            //officeCode = ConfigurationSettings.AppSettings["officeCode"].ToString();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form6());
        }
    }
}
