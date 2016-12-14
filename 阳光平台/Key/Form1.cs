using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.IO;
namespace Key
{
    public partial class Form1 : Form
    {
        SqlConnection thisConnection;
        SqlCommand myCommand;
        string type = string.Empty;
        string strsql = "";
        string return_string = "";
        public Form1()
        {
            InitializeComponent();
            thisConnection = new SqlConnection(Program.CRMConnectionString);
            thisConnection.Open();
            creatTable();
        }
        private int savedata(String str_sql)
        {
            myCommand.CommandText = str_sql;
            int intNumber = myCommand.ExecuteNonQuery();
            return intNumber;
        }
        #region//创建Peach  接口日志管理表
        private void creatTable()
        {
            try
            {
                StringBuilder sbTable = new StringBuilder();
                sbTable.Append("");

                #region//Peach  接口日志管理

                sbTable.Append("CREATE TABLE [U8Log_CRM](");
                sbTable.Append("[autoid] [int] IDENTITY(1,1) NOT NULL,");
                sbTable.Append("[content1] ntext NULL,");
                sbTable.Append("[ddate] [smalldatetime] NULL,");
                sbTable.Append("[maker] [nvarchar](50) NULL,");
                sbTable.Append("[createtime] [datetime] NULL,");
                sbTable.Append("[cdefine1] [nvarchar](500) NULL,");
                sbTable.Append("[cdefine2] [nvarchar](500) NULL,");
                sbTable.Append("[cdefine3] [nvarchar](500) NULL,");
                sbTable.Append("[cdefine4] [nvarchar](500) NULL,");
                sbTable.Append("[cdefine5] [nvarchar](500) NULL)");
                #endregion

                if (thisConnection.State.ToString().ToLower() != "open")
                {
                    thisConnection.Open();
                }

                myCommand = thisConnection.CreateCommand();
                myCommand.Connection = thisConnection;

                savedata(sbTable.ToString());
            }
            catch (Exception)
            {
            }
            finally
            {
                //if (thisConnection.State.ToString().ToLower() == "open")
                //{
                //    thisConnection.Close();
                //}
            }
        }
        #endregion
        #region 日志数据插入
        private void insertrz(string content1, string status, string vouchtypename, SqlCommand myCommandrz)
        {

            strsql = "insert into [U8Log_CRM] ([content1],maker,createtime,cdefine1,cdefine2) VALUES ( ";
            strsql = strsql + " '" + content1 + "',";
            strsql = strsql + " 'demo',";
            strsql = strsql + " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            strsql = strsql + " '" + status + "', ";
            strsql = strsql + " '" + vouchtypename + "' ";
            strsql = strsql + " ) ";

            myCommandrz.CommandText = strsql;
            myCommandrz.ExecuteNonQuery();


        }
        #endregion

        private void  error_fh(string temp1,string temp2,string type,string bk_no)
        {
            
            if (temp1 != "")
            {
                if (temp2 == "")
                {
                    return_string = return_string + "\r\n" + "工作单号" + bk_no + ":"+ type + " " + temp1 + "在CRM不存在！";
                    insertrz(return_string, "", "", myCommand);
                }

            }
 
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //Key.WebReference.ServiceCrm sc = new Key.WebReference.ServiceCrm();
            return_string = "";
            
                SqlDataAdapter ad ;
                SqlDataReader myDataReader = null;
                DataTable Table = new DataTable();
                string sql = string.Empty;
                string info = "";
                string strxml = "";
                DataTable testtable = new DataTable();
                XmlDocument xmlDoc1 = new XmlDocument();
                string ccus_id = "";
                myCommand = thisConnection.CreateCommand();
                SqlCommand crmcommand = thisConnection.CreateCommand();
                Program.ls_error = "\r\n\r\n======================================================================================================";
                insertrz(Program.ls_error, "", "", myCommand);
                Program.ls_error = "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 开始导入   处理人：admin";
                insertrz(Program.ls_error, "", "", myCommand);
                string czxs = "";

                string fhz = "";
                string msg = "";
                msg = CRMWebServiceControl.InvokeWeb_LoginIn(Program.LoginName, Program.LoginPwd, Program.CrmName, Program.CrmPort);

                #region 导入出货明细空运

                String dsstr = sc.ExportBusinessData("AF", Program.startdate, Program.enddate,Program.officeCode);
                sc.Timeout = 10000000;
                TextReader tdr = new StringReader(dsstr);
                DataSet ds = new DataSet();
                ds.ReadXml(tdr);
                
                //Table = DBHelper.GetTable("select REF_BUSINESS_ID,业务员,工作号,主单号,分单号,客服,起运港,中转港,目的港,一程目的港,一程航班号,二程目的港,二程航班号,航线,月,业务类型,揽货方式,发货人,货代,海外代理,业务日期,收货人,本地代理,委托人,销售重量,成本重量,实际重量,总票数,本位币合计,应收RMB,应收USD,应收totalRMB,单证货物品名,ONE_CARRIER_CODE from g_ky", Program.CRMConnectionString);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //if (ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], "") != "AFPCISHA110121012")
                    //{
                    //    continue;
                    //}


                    czxs = "";
                    return_string = "";
                    strxml = @"<Interface type='save' model='object' value='deliverair'>";
                    strxml = strxml + "<Row> ";
                    //出口委托人取客户编码
                    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["CONSIGNER_AND_CONSIGNEE"], "") + "' and is_deleted=0 ", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.AccountID>" + czxs + "</deliverair.AccountID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["CONSIGNER_AND_CONSIGNEE"], ""), czxs, "出口委托人", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //进口委托人
                    strxml = strxml + "<deliverair.jinkweitren>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["AI_CONSIGNER_ID"], "").Replace("&", "＆") + @"</deliverair.jinkweitren> ";


                    strxml = strxml + "<deliverair.IdentifyCode>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["REF_BUSINESS_ID"], "") + @"</deliverair.IdentifyCode> ";
                    strxml = strxml + "<deliverair.Name>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], "") + @"</deliverair.Name>";
                    strxml = strxml + "<deliverair.TypeID>2027</deliverair.TypeID> ";
                    strxml = strxml + "<deliverair.workno>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["MAWB_NO"], "") + @"</deliverair.workno>";
                    strxml = strxml + "<deliverair.fendh>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["HAWB_NO"], "") + @"</deliverair.fendh>";

                    //起运港取城市编码
                    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["POL_CODE"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.qiygID>" + czxs + @"</deliverair.qiygID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["POL_CODE"], ""), czxs, "起运港", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //中转港取城市编码
                    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["POD_CODE"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.zhongzgID>" + czxs + @"</deliverair.zhongzgID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["POD_CODE"], ""), czxs, "中转港", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //目的港取城市编码
                    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["PORT_DEST_CODE"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.mudgID>" + czxs + @"</deliverair.mudgID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["PORT_DEST_CODE"], ""), czxs, "业务类型", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //一程目的港取城市编码
                    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["ONE_POD_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.mudg1ID>" + czxs + @"</deliverair.mudg1ID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["ONE_POD_ID"], ""), czxs, "一程目的港", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //一程航班号
                    strxml = strxml + "<deliverair.hangbh1>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["ONE_FLIGHT_NO"], "") + @"</deliverair.hangbh1> ";

                    //一程航空公司取客户编码
                    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["ONE_CARRIER_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.hangkgs1ID>" + czxs + @"</deliverair.hangkgs1ID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["ONE_CARRIER_ID"], ""), czxs, "一程航空公司", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));


                    //二层目的港取城市编码
                    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["TWO_POD_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.mudg2ID>" + czxs + @"</deliverair.mudg2ID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["TWO_POD_ID"], ""), czxs, "二层目的港", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //二程航班号
                    strxml = strxml + "<deliverair.hangbh2>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["TWO_FLIGHT_NO"], "") + @"</deliverair.hangbh2> ";

                    //二程航空公司取客户编码
                    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["TWO_CARRIER_ID"], "") + "' and is_deleted=0 ", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.hangkgs2ID>" + czxs + @"</deliverair.hangkgs2ID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["TWO_CARRIER_ID"], ""), czxs, "二程航空公司", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //年月
                    strxml = strxml + "<deliverair.yearmonth>" + DateTime.Parse(ClsSystem.gnvl(ds.Tables[0].Rows[i]["MONTH_NAME"], "")).ToString("yyyy-MM") + @"</deliverair.yearmonth>";


                    //业务类型
                    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'deliverShip.jinck') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BUSINESS_TYPE"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.businesstype>" + czxs + @"</deliverair.businesstype>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["BUSINESS_TYPE"], ""), czxs, "业务类型", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));


                    //业务日期
                    strxml = strxml + "<deliverair.businessdate>" + DateTime.Parse(ClsSystem.gnvl(ds.Tables[0].Rows[i]["BUSINESS_DATE"], "")).ToString("yyyy-MM-dd") + @"</deliverair.businessdate>";
                    //销售重量
                    strxml = strxml + "<deliverair.xiaoszhongl>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SALES_CHARGE_WEIGHT"], "") + @"</deliverair.xiaoszhongl>";
                    //成本重量
                    strxml = strxml + "<deliverair.chengbzhongl>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["COST_CHARGE_WEIGHT"], "") + @"</deliverair.chengbzhongl>";
                    //实际重量
                    strxml = strxml + "<deliverair.shijzhongl>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["WHS_CHARGE_WEIGHT"], "") + @"</deliverair.shijzhongl>";
                    //总票数
                    strxml = strxml + "<deliverair.zongps>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["TOTAL_TICKTS"], "") + @"</deliverair.zongps>";
                    //本位币合计
                    strxml = strxml + "<deliverair.sumlrbenwb>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OTHER_PROFIT_TOTAL"], "") + @"</deliverair.sumlrbenwb>";
                    //应收RMB
                    strxml = strxml + "<deliverair.ARrmb>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_AMOUNT_RMB"], "") + @"</deliverair.ARrmb>";
                    //应收USD
                    strxml = strxml + "<deliverair.ARusd>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_AMOUNT_USD"], "") + @"</deliverair.ARusd>";
                    //应收totalRMB
                    strxml = strxml + "<deliverair.ARbwb>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_AMOUNT_TOTAL_RMB"], "") + @"</deliverair.ARbwb>";

                    strxml = strxml + "<deliverair.AIRwsR>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_UNAMOUNT_RMB"], "0") + @"</deliverair.AIRwsR>";
                    strxml = strxml + "<deliverair.AIRwsU>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_UNAMOUNT_USD"], "0") + @"</deliverair.AIRwsU>";
                    strxml = strxml + "<deliverair.AIRwsTOTAL>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_UNAMOUNT_TOTAL_RMB"], "0") + @"</deliverair.AIRwsTOTAL>";


                    //客服取员工编码
                    czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["CUST_SERVICE_PERSON_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.servicerID>" + czxs + @"</deliverair.servicerID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["CUST_SERVICE_PERSON_ID"], ""), czxs, "客服", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //业务员取员工编码
                    czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["FIRST_SALES_ID"], "") + "' and is_deleted=0 ", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.OwnerID>" + czxs + @"</deliverair.OwnerID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["FIRST_SALES_ID"], ""), czxs, "业务员", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //航线
                    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'deliverair.airline') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["LINEDEF_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.airline>" + czxs + @"</deliverair.airline>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["LINEDEF_ID"], ""), czxs, "航线", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //揽货方式取识别码
                    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'usersinfor.lanhfangs') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["CARGO_SOURCE_MODE_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.lanhfangs>" + czxs + @"</deliverair.lanhfangs>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["CARGO_SOURCE_MODE_ID"], ""), czxs, "揽货方式", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));



                    //发货人
                    //czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPPER_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.fanhuoren>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPPER_ID"], "").Replace("&", "＆") + @"</deliverair.fanhuoren>";
                   // error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPPER_ID"], ""), czxs, "发货人", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    ////进口收货人
                    //czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["AI_CONSIGNEE_ID"], "") + "'", Program.CRMConnectionString);
                    //strxml = strxml + "<deliverair.shouhrID>" + czxs + @"</deliverair.shouhrID>";
                    //error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["AI_CONSIGNEE_ID"], ""), czxs, "进口收货人", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));


                    //出口收货人
                    //czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["AO_CONSIGNEE_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.chukoushouhren>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["AO_CONSIGNEE_ID"], "").Replace("&", "＆") + @"</deliverair.chukoushouhren>";

                    //海外代理
                    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OCEAN_AGENT_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.haiydailID>" + czxs + @"</deliverair.haiydailID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["OCEAN_AGENT_ID"], ""), czxs, "海外代理", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //货代
                    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["FORWARDER_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.huodID>" + czxs + @"</deliverair.huodID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["FORWARDER_ID"], ""), czxs, "货代", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //本地代理
                    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["LOCAL_AGENT_ID"], "") + "'", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.benddailID>" + czxs + @"</deliverair.benddailID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["LOCAL_AGENT_ID"], ""), czxs, "本地代理", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                    //周数
                    strxml = strxml + "<deliverair.zhous>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["WEEK_NAME"], "") + @"</deliverair.zhous>";
                    //业务员相关办事处 FIRST_SALES_OFFICE_ID


                   // czxs = DBHelper.FindOther("dept_id", "tc_department", "dept_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["FIRST_SALES_OFFICE_ID"], "") + "'", Program.CRMConnectionString);
                    czxs = DBHelper.FindOther("dept_id", "tc_department", "dept_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["input_company"], "") + "'", Program.CRMConnectionString);

                    strxml = strxml + "<deliverair.DepartmentID>" + czxs + @"</deliverair.DepartmentID>";

                    //提单收货人
                    strxml = strxml + "<deliverair.HBLshouhr>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["DOC_CONSIGNEE_ID"], "").Replace("&", "＆") + @"</deliverair.HBLshouhr>";
                    //单证货物品名
                    strxml = strxml + "<deliverair.HBLhuom>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["DOC_GOODS_DESC"], "").Replace("&", "＆") + @"</deliverair.HBLhuom>";


                    //客服取员工编码
                    czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SOURCE_MODE_CS_ID"], "") + "' and is_deleted=0 ", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.aircsID>" + czxs + @"</deliverair.aircsID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["SOURCE_MODE_CS_ID"], ""), czxs, "客服人员", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));


                    //业务员取员工编码
                    czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SALES_ID"], "") + "' and is_deleted=0 ", Program.CRMConnectionString);
                    strxml = strxml + "<deliverair.salesID>" + czxs + @"</deliverair.salesID>";
                    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["SALES_ID"], ""), czxs, "业务员", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));



                    //<CONSIGNER_ID/>

                    //</>

                    //</>
                    //年月
                    //<MONTH_NAME/>
                    //业务类型取识别码
                    //<BUSINESS_TYPE/>
                    //业务日期
                    //<BUSINESS_DATE/>




                    ////起运港,中转港,目的港,一程目的港,一程航班号,二程目的港,二程航班号 需要确认

                    ////=========================================================================



                    //czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'usersinfor.lanhfangs') and e.identify_code='" + ClsSystem.gnvl(Table.Rows[i]["揽货方式"], "") + "'", Program.CRMConnectionString);
                    //strxml = strxml + "<deliverair.lanhfangs>" + czxs + @"</deliverair.lanhfangs>";

                    ////发货人，货代========================

                    ////===================================

                    //strxml = strxml + "<deliverair.haiwdail>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["海外代理"], "") + @"</deliverair.haiwdail>";
                    //strxml = strxml + "<deliverair.shouhuor>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["收货人"], "") + @"</deliverair.shouhuor>";
                    //strxml = strxml + "<deliverair.benddail>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["本地代理"], "") + @"</deliverair.benddail>";
                    //strxml = strxml + "<deliverair.haiwdail>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["海外代理"], "") + @"</deliverair.haiwdail>";


                    //strxml = strxml + "<deliverair.huowpinm>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["单证货物品名"], "") + @"</deliverair.huowpinm>";



                    strxml = strxml + " </Row></Interface>";

                    if (return_string != "")
                    {
                    }
                    else
                    {
                        info = CRMWebServiceControl.InvokeWeb_EAI(strxml, Program.CrmName, Program.CrmPort, msg);

                        xmlDoc1 = new XmlDocument();
                        xmlDoc1.LoadXml(info);
                        XmlNode xnl = xmlDoc1.SelectSingleNode("Interface");
                        foreach (XmlNode xnf in xnl)
                        {
                            XmlElement xe = (XmlElement)xnf;

                            if (xe.GetAttribute("success") == "false")
                            {
                                //MessageBox.Show(xe.GetAttribute("message"));
                                //MessageBox.Show(info);

                                Program.ls_error = "\r\n导入CRM[" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], "") + "]失败：" + xe.GetAttribute("message");
                                insertrz(Program.ls_error, "", "", myCommand);
                                break;
                            }
                            else
                            {
                                //MessageBox.Show(xe.GetAttribute("message"));
                                //MessageBox.Show(info);

                                //if (fhz == "")
                                //{
                                    fhz = ClsSystem.gnvl(ds.Tables[0].Rows[i]["REF_BUSINESS_ID"], "");
                                //}
                                //else
                                //{
                                //    fhz = fhz + ";" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["REF_BUSINESS_ID"], "");
                                //}
                                    string aa = "";
                                    if (fhz != "")
                                    {
                                        sc.UpdateBusinessStatus(fhz, out aa);
                                    }

                            }
                        }
                    }
                }
                //string aa = "";
                //if (fhz != "")
                //{
                //    sc.UpdateBusinessStatus(fhz, out aa);
                //}
                #endregion

                //#region 导入出货明细海运
               
                ////string aa = "";
                ////string aaaa = "";
                ////string xm = @"<?xml version=\""1.0\"" encoding=\""utf-8\"" ?><DsSbCust><SB_CUST><CUST_ID>201111010001</CUST_ID><CUST_ALIAS></CUST_ALIAS><CUST_TYPE>0</CUST_TYPE><CUST_NAME></CUST_NAME><ADDR></ADDR><CUST_NAME_NATIVE>宁波莱悦电子有限公司</CUST_NAME_NATIVE><ADDR_NATIVE></ADDR_NATIVE><ZIP></ZIP><INV_TITLE></INV_TITLE><CREDIT_DAYS>666</CREDIT_DAYS><CREDIT_AMT>7888</CREDIT_AMT><START_DATE>2011/12/8 14:28:47</START_DATE><CITY_ID></CITY_ID><INPUT_USER>0</INPUT_USER><INPUT_USER_NAME></INPUT_USER_NAME><INPUT_OFFICE></INPUT_OFFICE><RELATED_OFFICE></RELATED_OFFICE><STATUS>1003</STATUS><ACTIVE>0</ACTIVE><TEL>02188888888</TEL><FAX>02188888889</FAX><CREDIT_CURRENCY>1001</CREDIT_CURRENCY><CITY_NAME></CITY_NAME><CUST_ALIAS_CN>宁波莱悦电子有限公司</CUST_ALIAS_CN><REM_CODE>nblr</REM_CODE><DELETED_REASON>0</DELETED_REASON><CUST_INNER_OUTER></CUST_INNER_OUTER><DELETED>0</DELETED><MODIFY_USER>0</MODIFY_USER><MODIDY_USER_NAME></MODIDY_USER_NAME><MODIFY_DATE>2011/12/22 9:03:21</MODIFY_DATE><INPUT_DATE>2011/12/8 14:28:47</INPUT_DATE><RELATE_USER></RELATE_USER><CREDIT_NO>0</CREDIT_NO><CREDIT_INVALIDATION_DATE>0</CREDIT_INVALIDATION_DATE><CREDIT_REMARK>0</CREDIT_REMARK></SB_CUST><SB_CUST_CONTACT><CONTACT_ID></CONTACT_ID><CUST_ID></CUST_ID><CONTACT></CONTACT><EMAIL></EMAIL><TEL></TEL><FAX></FAX><MOBILE_NO></MOBILE_NO><RESPONSIBLE></RESPONSIBLE><BIRTHDAY></BIRTHDAY><ACTIVE></ACTIVE><DELETED>0</DELETED><CONTACT_TYPE></CONTACT_TYPE><CONTACT_EN></CONTACT_EN><POSITION></POSITION><HOBBY></HOBBY><MSN></MSN></SB_CUST_CONTACT></DsSbCust>";
                ////bool aaa= sc.CreateCust(xm,out aaaa);
                ////string aa="";

                //fhz = "";
                //return_string = "";

                //sc.Timeout = 1000000;
                //dsstr = sc.ExportBusinessData("OO",Program.startdate,Program.enddate );
                
                //tdr = new StringReader(dsstr);
                //ds = new DataSet();
                //ds.ReadXml(tdr);

                ////Table = DBHelper.GetTable("select 箱号,是否同行,业务类型,[JOB NO],工作号,HBL_NO,MBL_NO,MASTER_BK_ID,收货地,POL_ID,POD_ID,交货地,船东目的地,揽货方式,进出口标志,委托单类型,月,业务日期,是否外托,HBL放单方式,货主拼箱总单,TUE_TOTAL,ALL_20_TOTAL,ALL_40_TOTAL,ALL_45_TOTAL,ALL_40HQ_TOTAL,尺码数,公斤数,计费吨,本位币合计,应收RMB,应收USD,应收totalRMB,HBL货描,总提单票数 from g_hy", Program.CRMConnectionString);
            
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    czxs = "";
                //    return_string = "";
                //    strxml = @"<Interface type='save' model='object' value='deliverShip'>";
                //    strxml = strxml + "<Row> ";
                //    //工作号
                //    strxml = strxml + "<deliverShip.IdentifyCode>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["REF_BUSINESS_ID"], "") + @"</deliverShip.IdentifyCode> ";

                //    strxml = strxml + "<deliverShip.Name>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], "") + @"</deliverShip.Name> ";
                //    strxml = strxml + "<deliverShip.TypeID>2023</deliverShip.TypeID> ";

                //    //业务类型
                //    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'deliverShip.jinck') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BUSINESS_TYPE_NO"], "") + "'", Program.CRMConnectionString);

                //    strxml = strxml + "<deliverShip.jinck>" + czxs + @"</deliverShip.jinck>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["BUSINESS_TYPE_NO"], ""), czxs, "业务类型", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //航线
                //    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'usersinfor.shipline') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["LINE_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.shipline>" + czxs + @"</deliverShip.shipline>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["LINE_ID"], ""), czxs, "航线",ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //卸货港
                //    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["POD_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.xiehgID>" + czxs + @"</deliverShip.xiehgID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["POD_ID"], ""), czxs, "卸货港", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //货代目的地
                //    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["HBL_DEST_LOCATION_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.huodmuddID>" + czxs + @"</deliverShip.huodmuddID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["HBL_DEST_LOCATION_ID"], ""), czxs, "货代目的地", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                //    //交货地
                //    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["MBL_DELIVERY_LOCATION_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.jiaohdID>" + czxs + @"</deliverShip.jiaohdID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["MBL_DELIVERY_LOCATION_ID"], ""), czxs, "交货地", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));


                //    //揽货方式取识别码
                //    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'usersinfor.lanhfangs') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SOURCE_MODE"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.Canvassmethod>" + czxs + @"</deliverShip.Canvassmethod>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["SOURCE_MODE"], ""), czxs, "揽货方式", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //业务员
                //    czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SALES_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.OwnerID>" + czxs + @"</deliverShip.OwnerID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["SALES_ID"], ""), czxs, "业务员", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //货代提单号
                //    strxml = strxml + "<deliverShip.ladnumber>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["HBL_NO"], "") + @"</deliverShip.ladnumber> ";
                //    //船东提单号
                //    strxml = strxml + "<deliverShip.Shipladnum>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["MBL_NO"], "") + @"</deliverShip.Shipladnum> ";
                //    //出口委托人
                //    //czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OO_CONSIGNER_ID"], "") + "'", Program.CRMConnectionString);
                //    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OO_CONSIGNER_ID"], "") + "'", Program.CRMConnectionString);

                //    strxml = strxml + "<deliverShip.AccountID>" + czxs + "</deliverShip.AccountID> ";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["PARTNER_ID"], ""), czxs, "出口委托人", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //进口委托人
                //    //czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OI_CONSIGNER_ID"], "") + "'", Program.CRMConnectionString);

                //    strxml = strxml + "<deliverShip.jinkweitren>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OI_CONSIGNER_ID"], "") + "</deliverShip.jinkweitren> ";

                //    //船公司
                //    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["CARRIER_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.shipcompanyID>" + czxs + @"</deliverShip.shipcompanyID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["CARRIER_ID"], ""), czxs, "船公司", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //业务日期
                //    strxml = strxml + "<deliverShip.businessdate>" + DateTime.Parse(ClsSystem.gnvl(ds.Tables[0].Rows[i]["BUSINESS_DATE"], "")).ToString("yyyy-MM-dd") + @"</deliverShip.businessdate>";
                //    //单证员
                //    czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["DOC_PERSON_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.danzhengyuanID>" + czxs + @"</deliverShip.danzhengyuanID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["DOC_PERSON_ID"], ""), czxs, "单证员", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //海外代理
                //    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["AGENT_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.HaiwdailID>" + czxs + @"</deliverShip.HaiwdailID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["AGENT_ID"], ""), czxs, "海外代理", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //委托单类型
                //    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'deliverShip.weituodanleix') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BL_TYPE"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.weituodanleix>" + czxs + @"</deliverShip.weituodanleix>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["BL_TYPE"], ""), czxs, "委托单类型", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //操作
                //    czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OPERATOR_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.caozID>" + czxs + @"</deliverShip.caozID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["OPERATOR_ID"], ""), czxs, "操作员", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                //    //收货地
                //    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["RECEIPT_LOCATION_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.shouhdID>" + czxs + @"</deliverShip.shouhdID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["RECEIPT_LOCATION_ID"], ""), czxs, "收货地", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                //    //装货港
                //    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["RECEIPT_LOCATION_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.zhuanghgID>" + czxs + @"</deliverShip.zhuanghgID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["RECEIPT_LOCATION_ID"], ""), czxs, "装货港", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //HBL放单方式
                //    czxs = DBHelper.FindOther("e.enum_key", "tc_enum e LEFT OUTER JOIN tc_enum_str str1 ON e.org_id=str1.org_id AND e.attr_name=str1.attr_name AND e.enum_key=str1.enum_key AND str1.lang_id=1 LEFT OUTER JOIN tc_enum_str str2 ON e.org_id=str2.org_id AND e.attr_name=str2.attr_name AND e.enum_key=str2.enum_key AND str2.lang_id=2 LEFT OUTER JOIN tc_enum_str str3 ON e.org_id=str3.org_id AND e.attr_name=str3.attr_name AND e.enum_key=str3.enum_key AND str3.lang_id=3", "(e.org_id=1) AND (e.attr_name=N'deliverShip.HBLfdfs') and e.identify_code='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["HBL_RELEASE_MODE"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.HBLfdfs>" + czxs + @"</deliverShip.HBLfdfs>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["HBL_RELEASE_MODE"], ""), czxs, "HBL放单方式", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //进口收货人
                //    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OI_CONSIGNEE_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.shouhuorID>" + czxs + @"</deliverShip.shouhuorID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["OI_CONSIGNEE_ID"], ""), czxs, "进口收货人", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                //    //出口收货人
                //    //czxs = DBHelper.FindOther("user_id", "tc_user", "login_name='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OI_CONSIGNEE_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.chukshouhren>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["OO_CONSIGNEE_ID"], "") + @"</deliverShip.chukshouhren>";



                //    //是否外托
                //    strxml = strxml + "<deliverShip.shifweit>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["IS_RESALE"], "") + @"</deliverShip.shifweit> ";
                //    //是否同行
                //    strxml = strxml + "<deliverShip.shiftongh>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["IS_PEER"], "") + @"</deliverShip.shiftongh> ";

                //    //承运人
                //    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPAGENT_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.chengyrID>" + czxs + @"</deliverShip.chengyrID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPAGENT_ID"], ""), czxs, "承运人", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                //    //货代
                //    czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["FORWARDER_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.huodID>" + czxs + @"</deliverShip.huodID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["FORWARDER_ID"], ""), czxs, "货代", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                //    //船名
                //    strxml = strxml + "<deliverShip.shipname>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["VESS_ID"], "") + @"</deliverShip.shipname> ";
                //    //航次
                //    strxml = strxml + "<deliverShip.hangci>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["VOY_ID"], "") + @"</deliverShip.hangci> ";

                //    //发货人
                //    //czxs = DBHelper.FindOther("account_id", "tc_account", "account_number='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPPER_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.fahuoren>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPPER_ID"], "") + @"</deliverShip.fahuoren>";
                //    //error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["SHIPPER_ID"], ""), czxs, "发货人", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));

                //    //船东目的地
                //    czxs = DBHelper.FindOther("gkgl_id", "tcu_gkgl", "gkgl_char01='" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["MBL_DEST_LOCATION_ID"], "") + "'", Program.CRMConnectionString);
                //    strxml = strxml + "<deliverShip.chuandmuddID>" + czxs + @"</deliverShip.chuandmuddID>";
                //    error_fh(ClsSystem.gnvl(ds.Tables[0].Rows[i]["MBL_DEST_LOCATION_ID"], ""), czxs, "船东目的地", ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], ""));
                //    //JOB NO
                //    strxml = strxml + "<deliverShip.JOBNO>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["LINK_NO"], "") + @"</deliverShip.JOBNO> ";
                //    //箱号
                //    strxml = strxml + "<deliverShip.xiangh>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["CTN_NO"], "") + @"</deliverShip.xiangh>";

                //    //协作公司
                //    //strxml = strxml + "<deliverShip.COOPERATOR_OFFICE_ID>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["COOPERATOR_OFFICE_ID"], "") + @"</deliverShip.COOPERATOR_OFFICE_ID>";

                //    //月
                //    strxml = strxml + "<deliverShip.yearmonth>" + DateTime.Parse(ClsSystem.gnvl(ds.Tables[0].Rows[i]["MONTH_COUNT"], "")).ToString("yyyy-MM") + @"</deliverShip.yearmonth>";
                //    //TUE_TOTAL
                //    strxml = strxml + "<deliverShip.sumTEU>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["TUE_TOTAL"], "") + @"</deliverShip.sumTEU>";
                //    strxml = strxml + "<deliverShip.chig20>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["ALL_20_TOTAL"], "") + @"</deliverShip.chig20>";
                //    strxml = strxml + "<deliverShip.chigui40>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["ALL_40_TOTAL"], "") + @"</deliverShip.chigui40>";
                //    strxml = strxml + "<deliverShip.chigui45>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["ALL_45_TOTAL"], "") + @"</deliverShip.chigui45>";
                //    strxml = strxml + "<deliverShip.hqchigui40>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["ALL_40HQ_TOTAL"], "") + @"</deliverShip.hqchigui40>";
                //    strxml = strxml + "<deliverShip.huowuchim>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["CBM_TOTAL"], "") + @"</deliverShip.huowuchim>";
                //    strxml = strxml + "<deliverShip.huowgongj>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["KGS_TOTAL"], "") + @"</deliverShip.huowgongj>";
                //    strxml = strxml + "<deliverShip.huowjifd>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["CHARGE_WEIGHT"], "") + @"</deliverShip.huowjifd>";
                //    strxml = strxml + "<deliverShip.zongtids>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BL_TOTAL"], "") + @"</deliverShip.zongtids>";
                //    strxml = strxml + "<deliverShip.benwblir>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["PROFIT_TOTAL"], "") + @"</deliverShip.benwblir>";
                //    strxml = strxml + "<deliverShip.ARrenmb>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_AMOUNT_RMB"], "") + @"</deliverShip.ARrenmb>";
                //    strxml = strxml + "<deliverShip.ARusd>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_AMOUNT_USD"], "") + @"</deliverShip.ARusd>";
                //    strxml = strxml + "<deliverShip.ARbenwb>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["R_AMOUNT_TOTAL_RMB"], "") + @"</deliverShip.ARbenwb>";
                //    //提单收货人
                //    strxml = strxml + "<deliverShip.HBLshouhr>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["HBL_CONSIGNEE_ID"], "") + @"</deliverShip.HBLshouhr>";
                //    //HBL货描
                //    strxml = strxml + "<deliverShip.HBLhuom>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["HBL_GOODS_DESC"], "") + @"</deliverShip.HBLhuom>";

                //    //周数
                //    strxml = strxml + "<deliverShip.zhous>" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["week_count"], "") + @"</deliverShip.zhous>";





                ////    ////////strxml = strxml + "<deliverShip.IdentifyCode>" + ClsSystem.gnvl(Table.Rows[i]["工作号"], "") + @"</deliverShip.IdentifyCode> ";
                ////    ////////strxml = strxml + "<deliverShip.Name>" + ClsSystem.gnvl(Table.Rows[i]["工作号"], "") + @"</deliverShip.Name>";
                ////    ////////strxml = strxml + "<deliverShip.TypeID>2023</deliverShip.TypeID> ";
                ////    ////////strxml = strxml + "<deliverShip.deliveraddress>" + ClsSystem.gnvl(Table.Rows[i]["交货地"], "") + @"</deliverShip.deliveraddress>";
                ////    ////////if (ClsSystem.gnvl(Table.Rows[i]["揽货方式"], "") == "E")
                ////    ////////{
                ////    ////////    strxml = strxml + "<deliverShip.Canvassmethod>1001</deliverShip.Canvassmethod>";
                ////    ////////}
                ////    ////////if (ClsSystem.gnvl(Table.Rows[i]["揽货方式"], "") == "D")
                ////    ////////{
                ////    ////////    strxml = strxml + "<deliverShip.Canvassmethod>1002</deliverShip.Canvassmethod>";
                ////    ////////}
                ////    ////////if (ClsSystem.gnvl(Table.Rows[i]["揽货方式"], "") == "B")
                ////    ////////{
                ////    ////////    strxml = strxml + "<deliverShip.Canvassmethod>1003</deliverShip.Canvassmethod>";
                ////    ////////}
                ////    ////////if (ClsSystem.gnvl(Table.Rows[i]["揽货方式"], "") == "A")
                ////    ////////{
                ////    ////////    strxml = strxml + "<deliverShip.Canvassmethod>1004</deliverShip.Canvassmethod>";
                ////    ////////}
                ////    ////////strxml = strxml + "<deliverShip.xiangh>" + ClsSystem.gnvl(Table.Rows[i]["箱号"], "") + @"</deliverShip.xiangh>";
                ////    ////////strxml = strxml + "<deliverShip.JOBNO>" + ClsSystem.gnvl(Table.Rows[i]["JOB NO"], "") + @"</deliverShip.JOBNO>";
                ////    ////////strxml = strxml + "<deliverShip.shouhuodi>" + ClsSystem.gnvl(Table.Rows[i]["收货地"], "") + @"</deliverShip.shouhuodi>";
                ////    ////////strxml = strxml + "<deliverShip.chuandmudid>" + ClsSystem.gnvl(Table.Rows[i]["船东目的地"], "") + @"</deliverShip.chuandmudid>";
                ////    ////////if (ClsSystem.gnvl(Table.Rows[i]["进出口标志"], "") == "I")
                ////    ////////{
                ////    ////////    strxml = strxml + "<deliverShip.jinck>1001</deliverShip.jinck>";
                ////    ////////}
                ////    ////////if (ClsSystem.gnvl(Table.Rows[i]["进出口标志"], "") == "O")
                ////    ////////{
                ////    ////////    strxml = strxml + "<deliverShip.jinck>1002</deliverShip.jinck>";
                ////    ////////}
                ////    //////////委托单类型
                ////    //////////strxml = strxml + "<deliverShip.jinck>" + ClsSystem.gnvl(Table.Rows[i]["委托单类型"], "") + @"</deliverShip.jinck>";
                ////    //////////HBL放单方式
                ////    //////////strxml = strxml + "<deliverShip.Name>" + ClsSystem.gnvl(Table.Rows[i]["HBL放单方式"], "") + @"</deliverShip.Name> ";
                ////    //////////委托人
                ////    ////////strxml = strxml + "<deliverShip.AccountID>17</deliverShip.AccountID> ";
                ////    ////////strxml = strxml + "<deliverShip.yearmonth>" + DateTime.Parse(ClsSystem.gnvl(Table.Rows[i]["月"], "")).ToString("yyyy-MM") + @"</deliverShip.yearmonth>";


                ////    ////////strxml = strxml + "<deliverShip.shifweit>" + ClsSystem.gnvl(Table.Rows[i]["是否外托"], "") + @"</deliverShip.shifweit>";
                ////    ////////strxml = strxml + "<deliverShip.huozpingxzongd>" + ClsSystem.gnvl(Table.Rows[i]["货主拼箱总单"], "") + @"</deliverShip.huozpingxzongd>";
                ////    ////////strxml = strxml + "<deliverShip.sumTEU>" + ClsSystem.gnvl(Table.Rows[i]["TUE_TOTAL"], "") + @"</deliverShip.sumTEU>";
                ////    ////////strxml = strxml + "<deliverShip.chig20>" + ClsSystem.gnvl(Table.Rows[i]["ALL_20_TOTAL"], "") + @"</deliverShip.chig20>";
                ////    ////////strxml = strxml + "<deliverShip.chigui40>" + ClsSystem.gnvl(Table.Rows[i]["ALL_40_TOTAL"], "") + @"</deliverShip.chigui40>";
                ////    ////////strxml = strxml + "<deliverShip.chigui45>" + ClsSystem.gnvl(Table.Rows[i]["ALL_45_TOTAL"], "") + @"</deliverShip.chigui45>";
                ////    ////////strxml = strxml + "<deliverShip.hqchigui40>" + ClsSystem.gnvl(Table.Rows[i]["ALL_40HQ_TOTAL"], "") + @"</deliverShip.hqchigui40>";
                ////    ////////strxml = strxml + "<deliverShip.huowuchim>" + ClsSystem.gnvl(Table.Rows[i]["尺码数"], "") + @"</deliverShip.huowuchim>";
                ////    ////////strxml = strxml + "<deliverShip.huowgongj>" + ClsSystem.gnvl(Table.Rows[i]["公斤数"], "") + @"</deliverShip.huowgongj>";
                ////    ////////strxml = strxml + "<deliverShip.huowjifd>" + ClsSystem.gnvl(Table.Rows[i]["计费吨"], "") + @"</deliverShip.huowjifd>";
                ////    ////////strxml = strxml + "<deliverShip.zongtids>" + ClsSystem.gnvl(Table.Rows[i]["总提单票数"], "") + @"</deliverShip.zongtids>";
                ////    ////////strxml = strxml + "<deliverShip.benwblir>" + ClsSystem.gnvl(Table.Rows[i]["本位币合计"], "") + @"</deliverShip.benwblir>";
                ////    ////////strxml = strxml + "<deliverShip.ARrenmb>" + ClsSystem.gnvl(Table.Rows[i]["应收RMB"], "") + @"</deliverShip.ARrenmb>";
                ////    ////////strxml = strxml + "<deliverShip.ARusd>" + ClsSystem.gnvl(Table.Rows[i]["应收USD"], "") + @"</deliverShip.ARusd>";
                ////    ////////strxml = strxml + "<deliverShip.ARbenwb>" + ClsSystem.gnvl(Table.Rows[i]["应收totalRMB"], "") + @"</deliverShip.ARbenwb>";
                ////    ////////strxml = strxml + "<deliverShip.huowmiaos>" + ClsSystem.gnvl(Table.Rows[i]["HBL货描"], "") + @"</deliverShip.huowmiaos>";
                //    strxml = strxml + " </Row></Interface>";

                //    if (return_string != "")
                //    {
                //    }
                //    else
                //    {

                //        info = CRMWebServiceControl.InvokeWeb_EAI(strxml, Program.CrmName, Program.CrmPort, msg);

                //        xmlDoc1 = new XmlDocument();
                //        xmlDoc1.LoadXml(info);
                //        XmlNode xnl = xmlDoc1.SelectSingleNode("Interface");
                //        foreach (XmlNode xnf in xnl)
                //        {
                //            XmlElement xe = (XmlElement)xnf;

                //            if (xe.GetAttribute("success") == "false")
                //            {
                //                //MessageBox.Show(xe.GetAttribute("message"));
                //                //MessageBox.Show(info);



                //                Program.ls_error = "\r\n导入CRM[" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["BK_NO"], "") + "]失败：" + xe.GetAttribute("message");
                //                insertrz(Program.ls_error, "", "", myCommand);
                //                break;
                //            }
                //            else
                //            {
                //                //MessageBox.Show(xe.GetAttribute("message"));
                //                //MessageBox.Show(info);

                //                if (fhz == "")
                //                {
                //                    fhz = ClsSystem.gnvl(ds.Tables[0].Rows[i]["REF_BUSINESS_ID"], "");
                //                }
                //                else
                //                {
                //                    fhz = fhz + ";" + ClsSystem.gnvl(ds.Tables[0].Rows[i]["REF_BUSINESS_ID"], "");
                //                }

                //                //Program.ls_error = "\r\n导入CRM客户[" + ClsSystem.gnvl(Table.Rows[i]["CORPFULLName"], "") + "]成功";
                //                //insertrz(Program.ls_error, "", "", myCommand);

                //                //strsql = "update tc_account set acct_date01=getdate(),shipping_address='" + ClsSystem.gnvl(Table.Rows[i]["ADDRESS"], "") + "',acct_int03='" + ClsSystem.gnvl(khlx, "0") + "',acct_int01='" + ClsSystem.gnvl(chengshi, "0") + "'";
                //                //strsql = strsql + " where identify_code='" + ClsSystem.gnvl(Table.Rows[i]["MD5KEY"], "") + "'";
                //                //crmcommand.CommandText = strsql;
                //                //crmcommand.ExecuteNonQuery();

                //                //strsql = "select account_id from tc_account where identify_code='" + ClsSystem.gnvl(Table.Rows[i]["MD5KEY"], "") + "'";
                //                //myCommand.CommandText = strsql;
                //                //myDataReader = myCommand.ExecuteReader();
                //                //if (myDataReader.Read())
                //                //{
                //                //    ccus_id = ClsSystem.gnvl(myDataReader.GetValue(0), "");
                //                //}
                //                //myDataReader.Close();
                //                //break;
                //            }
                //        }
                //    }
                //}
                //aa = "";
                ////if (fhz != "")
                ////{
                ////    sc.UpdateBusinessStatus(fhz, out aa);
                ////}
                //#endregion

                

               

               


                


                //===============================================隐藏
               

            
                //===================================================

                Program.ls_error = "\r\n导入结束！\r\n======================================================================================================";
                insertrz(Program.ls_error, "", "", myCommand);

                thisConnection.Close();
                this.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(msg);
            try
            {
                string strxml = string.Empty;
                for (int i = 0; i < this.dataGridView.Rows.Count; i++)
                {
                    
                    if (type == "YJS")
                    {
                        #region 导入CRM客户档案
                        string id = DBHelper.FindOther("account_id", "tc_account", "identify_code='" + this.dataGridView.Rows[i].Cells["客户MD5"].Value + "'", Program.CRMConnectionString);
                        if (!string.IsNullOrEmpty(id))
                        {
                            strxml = @"<Interface type='save' model='object' value='Account'>
                            <Row>
                            <Account.ID>" + id + @"</Account.ID>
                            <Account.TypeID>" + this.dataGridView.Rows[i].Cells["客户类型ID"].Value + @"</Account.TypeID>
                            <Account.Name>" + this.dataGridView.Rows[i].Cells["客户名称"].Value + @"</Account.Name>
                            <Account.Email>" + this.dataGridView.Rows[i].Cells["邮件"].Value + @"</Account.Email>
                            <Account.Fax>" + this.dataGridView.Rows[i].Cells["传真"].Value + @"</Account.Fax>
                            <Account.Phone>" + this.dataGridView.Rows[i].Cells["电话"].Value + @"</Account.Phone>
                            <Account.ShippingAddress>" + this.dataGridView.Rows[i].Cells["地址"].Value + @"<Account.ShippingAddress>
                            <Account.ShippingZipCode>" + this.dataGridView.Rows[i].Cells["邮政编码"].Value + @"<Account.ShippingZipCode>
                           </Row>
                            </Interface>";
                        }
                        else
                        {
                              //<Account.TypeID>" + this.dataGridView.Rows[i].Cells["客户类型ID"].Value + @"</Account.TypeID>
                            strxml = @"<Interface type='save' model='object' value='Account'>
                            <Row>
                            <Account.IdentifyCode>" + this.dataGridView.Rows[i].Cells["客户MD5"].Value + @"</Account.IdentifyCode>
                            <Account.TypeID>26</Account.TypeID>
                            <Account.Name>" + this.dataGridView.Rows[i].Cells["客户名称"].Value + @"</Account.Name>
                            <Account.Email>" + this.dataGridView.Rows[i].Cells["邮件"].Value + @"</Account.Email>
                            <Account.Fax>" + this.dataGridView.Rows[i].Cells["传真"].Value + @"</Account.Fax>
                            <Account.Phone>" + this.dataGridView.Rows[i].Cells["电话"].Value + @"</Account.Phone>
                            <Account.ShippingAddress>" + this.dataGridView.Rows[i].Cells["地址"].Value + @"<Account.ShippingAddress>
                            <Account.ShippingZipCode>" + this.dataGridView.Rows[i].Cells["邮政编码"].Value + @"<Account.ShippingZipCode>
                            </Row>
                            </Interface>";

                        }
                        //string info = CRMWebServiceControl.InvokeWeb_EAI(strxml, Program.CrmName, Program.CrmPort, msg);
                        #endregion
                        

                        //MessageBox.Show(info);
                    }
                    else
                    {

                        int j = DBHelper.Morage(" exec sp_updatecustomerByMD5KEY '" +
                                this.dataGridView.Rows[i].Cells["客户MD5"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["客户名称"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["客户名称"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["客户类型ID"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["客户MD5"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["邮政编码"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["地址"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["电话"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["传真"].Value + "','" +
                                this.dataGridView.Rows[i].Cells["邮件"].Value + "',1,'CRM',1", Program.ConnectionString);
                    }
                    return;
                }

                for (int i = 0; i < this.dataGridView.Rows.Count; i++)
                {

                    if (type == "YJS")
                    {
                        
                    }
                }
                MessageBox.Show("ok");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnserach_Click(object sender, EventArgs e)
        {
//            try
//            {
                
//                if (radioButton1.Checked)
//                {
//                    sql = @" select account_id as id,identify_code as md5, account_name as name,account_type as typeid,shipping_address as address ,shipping_zipcode as zipcode,account_phone as phone,account_email as email,
//                                               account_fax as fax from tc_account 
//                                               where  tc_account.org_id=1 and tc_account.deleted_id=0  and account_id<>0  and identify_code<>''";
//                    if (!string.IsNullOrEmpty(this.txtcusname.Text))
//                    {
//                        sql += " and account_name like '%" + this.txtcusname.Text + "%'";
//                    }
//                    Table = DBHelper.GetTable(sql, Program.CRMConnectionString);
//                    type = "CRM";
//                }
//                else
//                {
//                    Table = DBHelper.GetTable("exec sp_getcustomerex", Program.ConnectionString);
                    
//                    //foreach (DataRow item in Table.Select("CORPFULLName= '" + this.txtcusname.Text + "'"))
//                    //{
//                    //    testtable.Rows.Add(item);
//                    //} 
//                    type = "YJS";
//                }
//                if (Table.Rows.Count > 0)
//                {
//                    this.dataGridView.RowCount = Table.Rows.Count;
//                    if (type == "CRM")
//                    {
//                        for (int i = 0; i < Table.Rows.Count; i++)
//                        {
//                            this.dataGridView.Rows[i].Cells["客户ID"].Value = Table.Rows[i]["ID"].ToString();
//                            this.dataGridView.Rows[i].Cells["客户MD5"].Value = Table.Rows[i]["md5"].ToString();
//                            this.dataGridView.Rows[i].Cells["客户名称"].Value = Table.Rows[i]["name"].ToString();
//                            this.dataGridView.Rows[i].Cells["客户类型ID"].Value = Table.Rows[i]["typeid"].ToString();
//                            this.dataGridView.Rows[i].Cells["邮政编码"].Value = Table.Rows[i]["zipcode"].ToString();
//                            this.dataGridView.Rows[i].Cells["地址"].Value = Table.Rows[i]["address"].ToString();
//                            this.dataGridView.Rows[i].Cells["电话"].Value = Table.Rows[i]["phone"].ToString();
//                            this.dataGridView.Rows[i].Cells["邮件"].Value = Table.Rows[i]["email"].ToString();
//                            this.dataGridView.Rows[i].Cells["传真"].Value = Table.Rows[i]["fax"].ToString();
//                            this.dataGridView.Rows[i].Cells["来源"].Value = "CRM";
//                        }
//                    }
//                    else
//                    {
//                        for (int i = 0; i < Table.Rows.Count; i++)
//                        {
//                            this.dataGridView.Rows[i].Cells["客户ID"].Value = Table.Rows[i]["ID"].ToString();
//                            this.dataGridView.Rows[i].Cells["客户MD5"].Value = Table.Rows[i]["MD5KEY"].ToString();
//                            this.dataGridView.Rows[i].Cells["客户名称"].Value = Table.Rows[i]["CORPFULLName"].ToString();
//                            this.dataGridView.Rows[i].Cells["客户类型ID"].Value = Table.Rows[i]["CORPCLASS"].ToString();
//                            this.dataGridView.Rows[i].Cells["邮政编码"].Value = Table.Rows[i]["POSTCODE"].ToString();
//                            this.dataGridView.Rows[i].Cells["地址"].Value = Table.Rows[i]["ADDRESS"].ToString();
//                            this.dataGridView.Rows[i].Cells["电话"].Value = Table.Rows[i]["PHONE"].ToString();
//                            this.dataGridView.Rows[i].Cells["邮件"].Value = Table.Rows[i]["EMAIL"].ToString();
//                            this.dataGridView.Rows[i].Cells["传真"].Value = Table.Rows[i]["FAX"].ToString();
//                            this.dataGridView.Rows[i].Cells["来源"].Value = "YJS";
//                            this.dataGridView.Rows[i].Cells["删除"].Value = Table.Rows[i]["FAX"].ToString();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex )
//            {
//                MessageBox.Show(ex.Message);

//            }
        }

        private void btnshuaxin_Click(object sender, EventArgs e)
        {

        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          