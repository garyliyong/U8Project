using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace SHYSInterface
{
    public partial class Form5 : Form
    {

        SqlConnection conn ;
        public Form5()
        {
            InitializeComponent();
            try
            {
                conn = new SqlConnection(Program.ConnectionString);
                //    mysqlconn = new SqlConnection(SqlConnString);
                if (conn.State.ToString().ToLower() != "open")
                {
                    conn.Open();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("连接数据库失败，可能的原因是：" + ex.Message.ToString(), "提示");
                return;
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
           // QuryData("YQ001");
        }
        private DataSet  Qury15(string CXLX,string CXBH)
        {
            DataSet ds = new DataSet();
            try
            {
                string xmlData = "";
                string ResultXml = "";
                xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>";
                xmlData = xmlData + "<XMLDATA>";
                xmlData = xmlData + "<HEAD>";
                xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP>";
                xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC>";
                xmlData = xmlData + "<BZXX/>";
                xmlData = xmlData + "</HEAD>";
                xmlData = xmlData + "<MAIN>";
                xmlData = xmlData + "<CXLX>" + CXLX + "</CXLX>";
                xmlData = xmlData + "<CXBH>" + CXBH + "</CXBH>";
                xmlData = xmlData + "</MAIN>";
                xmlData = xmlData + "</XMLDATA>";
                  ResultXml = SendMessage.SetMessage("YQ015", xmlData);
                    TextReader tdr = new StringReader(ResultXml);
                 
                    ds.ReadXml(tdr);

                    //if (ds.Tables.Count > 0)
                    //{
                    //    if (ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") == "00000")
                    //    {

                    //    }
                    //}
                    return ds;
            }
            catch (Exception ex )
            {
                return ds;
                throw;
            }
               
        }
        private string Qury01(string cType)
        {
            string xmlData = "";
            string ResultXml="";
            string czlx = "1";//新增
             

            string strsql = @"sselect cInvCCode as SPLX ,cInvDefine1 as YPLX,  cInvName as tym,'' as CPM,
'' as YWM, cInvName as SPM,cInvDefine2 as YPSPTXM, cInvDefine3 as YPJX,
cInvStd as GG,cEnterprise  as SCQYMC,'' as CPLB,'' as YPBWM,cFile  as YPPZWH,
'' as BZCZ,'' as BZDW,cPackingType  as CGYYDW,'' as BZSL,'' as BZFS,'' as TZMS
 from inventory  where isnull(cInvDefine10,'')<>'' ";

            DataTable db = SqlAccess.ExecuteSqlDataTable(strsql, conn);
            if (db.Rows.Count > 0)
            {
                for (int i = 0; i < db.Rows.Count; i++)
                {
                    xmlData="";
                    ResultXml="";
                    xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>";
                    xmlData = xmlData + "<XMLDATA>";
                    xmlData = xmlData + "<HEAD>";
                    xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP>";
                    xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC>";
                    xmlData = xmlData + "<BZXX/>";
                    xmlData = xmlData + "</HEAD>";
                    xmlData = xmlData + "<MAIN>";
                    xmlData = xmlData + "<CZLX>" + czlx + "</CZLX>";
                    xmlData = xmlData + "<SPLX>" + ClsSystem.gnvl(db.Rows[i]["SPLX"], "") + "</SPLX>";
                    xmlData = xmlData + "<YPLX>" + ClsSystem.gnvl(db.Rows[i]["YPLX"], "") + "</YPLX>";
                    xmlData = xmlData + "<TYM>" + ClsSystem.gnvl(db.Rows[i]["TYM"], "") + "</TYM>";
                    xmlData = xmlData + "<CPM>" + ClsSystem.gnvl(db.Rows[i]["CPM"], "") + "</CPM>";
                    xmlData = xmlData + "<YWM>" + ClsSystem.gnvl(db.Rows[i]["YWM"], "") + "</YWM>";
                    xmlData = xmlData + "<SPM>" + ClsSystem.gnvl(db.Rows[i]["SPM"], "") + "</SPM>";
                    xmlData = xmlData + "<YPSPTXM>" + ClsSystem.gnvl(db.Rows[i]["YPSPTXM"], "") + "</YPSPTXM>";
                    xmlData = xmlData + "<YPJX>" + ClsSystem.gnvl(db.Rows[i]["YPJX"], "") + "</YPJX>";
                    xmlData = xmlData + "<GG>" + ClsSystem.gnvl(db.Rows[i]["GG"], "") + "</GG>";
                    xmlData = xmlData + "<SCQYMC>" + ClsSystem.gnvl(db.Rows[i]["SCQYMC"], "") + "</SCQYMC>";
                    xmlData = xmlData + "<CPLB>" + ClsSystem.gnvl(db.Rows[i]["CPLB"], "") + "</CPLB>";
                    xmlData = xmlData + "<YPBWM>" + ClsSystem.gnvl(db.Rows[i]["YPBWM"], "") + "</YPBWM>";
                    xmlData = xmlData + "<YPPZWH>" + ClsSystem.gnvl(db.Rows[i]["YPPZWH"], "") + "</YPPZWH>";
                    xmlData = xmlData + "<BZCZ>" + ClsSystem.gnvl(db.Rows[i]["BZCZ"], "") + "</BZCZ>";
                    xmlData = xmlData + "<BZDW>" + ClsSystem.gnvl(db.Rows[i]["BZDW"], "") + "</BZDW>";
                    xmlData = xmlData + "<CGYYDW>" + ClsSystem.gnvl(db.Rows[i]["CGYYDW"], "") + "</CGYYDW>";
                    xmlData = xmlData + "<BZSL>" + ClsSystem.gnvl(db.Rows[i]["BZSL"], "") + "</BZSL>";
                    xmlData = xmlData + "<BZFS>" + ClsSystem.gnvl(db.Rows[i]["BZFS"], "") + "</BZFS>";
                    xmlData = xmlData + "<TZMS>" + ClsSystem.gnvl(db.Rows[i]["TZMS"], "") + "</TZMS>";
                    xmlData = xmlData + "</MAIN>";
                    xmlData = xmlData + "</XMLDATA>";

                   
                    DataSet ds =SendMessage.QuryData(cType, xmlData);

                    if (ds.Tables.Count > 0)
                    {
                         if ( ClsSystem.gnvl( ds.Tables[0].Rows[0]["ZTCLJG"],"")=="00000")
                         {

                             xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>";
                             xmlData = xmlData + "<XMLDATA>";
                             xmlData = xmlData + "<HEAD>";
                             xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP>";
                             xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC>";
                             xmlData = xmlData + "<BZXX/>";
                             xmlData = xmlData + "</HEAD>";
                             xmlData = xmlData + "<MAIN>";
                             xmlData = xmlData + "<CXLX>07</CXLX>";
                             xmlData = xmlData + "<CXBH>" + ClsSystem.gnvl(db.Rows[i]["YPBWM"], "") + "</CXBH>";
                             xmlData = xmlData + "</MAIN>";
                             xmlData = xmlData + "</XMLDATA>";
                             DataSet rds = SendMessage.QuryData("YQ015", xmlData);
                             if (rds.Tables.Count > 0)
                             {
                                 if (ClsSystem.gnvl(rds.Tables[0].Rows[0]["ZTCLJG"], "") == "00000")
                                 {
                                     //strsql = "insert into YS_Interface_Records(cMaker,dDate,cType,cFlag,ExCode,errMsg) values("
                                     //                        + "'',getdate(),'" + cType + "','成功','" + VouchNO + "','')";

                                    // SqlAccess.ExecuteSql(strsql, conn);
                                 }
                             }
                         }
                    }




                    }

                }
            return null;
            }




        }




    }

