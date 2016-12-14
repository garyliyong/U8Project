using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SHYSInterface
{
    public partial class FrmYQ010 : Form
    {

        public static DataTable cxdb = null;

        public FrmYQ010()
        {
            InitializeComponent();
        }
        //创建SQL语句
        private string BuildSqlSelect()
        {
            string strSQL = "";

            frmFilter Filter = new frmFilter();
            if (DialogResult.OK == Filter.ShowDialog())
            {
                strSQL += frmFilter.QueryCondition;
                return strSQL;
            }
            else
            {
                return "";
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "";

            try
            {
                dgv2.EndEdit();
                 sql = BuildSqlSelect();

                this.Cursor = Cursors.WaitCursor;
                if (dgv2.Rows.Count > 0)
                {
                    dgv2.Rows.Clear();
                }
                  DataSet ds= new DataSet();
                 

                if (ClsSystem.gnvl(sql,"") != "")
                {
                    string resultXml= SendMessage.SetMessage("YQ010", sql);

                     string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");

                     if (result == "00000")
                     {


                         TextReader tdr = new StringReader(resultXml);
                         ds.ReadXml(tdr);
                         result = "";
                         result = SendMessage.ReadXMl(resultXml, "MAIN", "JLS");
                         if (Public.GetNum(result) > 0)
                         {
                             DataTable db = ds.Tables[3];

                             //dt.DefaultView.Sort = "JHDH ASC";
                             //DataTable db  = dt.DefaultView.ToTable();
                             cxdb = db;


                             if (db.Rows.Count > 0)
                             {
                                 for (int i = 0; i < db.Rows.Count; i++)
                                 {
                                     dgv2.Rows.Add();

                                     dgv2.Rows[i].Cells["check"].Value = true;
                                     dgv2.Rows[i].Cells["SFWJ"].Value = ClsSystem.gnvl(ds.Tables[1].Rows[0]["SFWJ"], "");
                                     dgv2.Rows[i].Cells["JLS"].Value = ClsSystem.gnvl(ds.Tables[1].Rows[0]["JLS"], "");
                                     dgv2.Rows[i].Cells["DDMXBH"].Value = ClsSystem.gnvl(db.Rows[i]["DDMXBH"], "");
                                     dgv2.Rows[i].Cells["JHDH"].Value = ClsSystem.gnvl(db.Rows[i]["JHDH"], "");
                                     dgv2.Rows[i].Cells["YQBM"].Value = ClsSystem.gnvl(db.Rows[i]["YQBM"], "");

                                     dgv2.Rows[i].Cells["YYBM"].Value = ClsSystem.gnvl(db.Rows[i]["YYBM"], "");
                                     dgv2.Rows[i].Cells["YYMC"].Value = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cus.cCusName from Customer_extradefine  ex join Customer cus on cus.cCusCode=ex.cCusCode where ex.ccdefine1='" + ClsSystem.gnvl( db.Rows[i]["YYBM"],"") + "'", Program.ConnectionString), "");
                                     dgv2.Rows[i].Cells["PSDBM"].Value = ClsSystem.gnvl(db.Rows[i]["PSDBM"], "");
                                     dgv2.Rows[i].Cells["PSDZ"].Value = ClsSystem.gnvl(db.Rows[i]["PSDZ"], "");
                                     dgv2.Rows[i].Cells["CGLX"].Value = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cgmc from ysxt_CGMS  with(nolock) where cgbm='" + ClsSystem.gnvl(db.Rows[i]["CGLX"], "") + "'", Program.ConnectionString), "");
                                     if (ClsSystem.gnvl(db.Rows[i]["DDLX"], "")=="1")
                                     {
                                         dgv2.Rows[i].Cells["DDLX"].Value = "医院自行订单";
                                     }else
                                     {
                                         dgv2.Rows[i].Cells["DDLX"].Value = "托管药库订单";
                                     }
                                     if ( ClsSystem.gnvl(db.Rows[i]["SPLX"], "")=="1")
                                     {
                                         dgv2.Rows[i].Cells["SPLX"].Value = "药品类";
                                     }
                                     else if (ClsSystem.gnvl(db.Rows[i]["SPLX"], "") == "2")
                                     {
                                         dgv2.Rows[i].Cells["SPLX"].Value = "医用耗材器械类";
                                     }
                                     else
                                     {
                                         dgv2.Rows[i].Cells["SPLX"].Value = "其他";
                                     }
                                     dgv2.Rows[i].Cells["YPLX"].Value = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select ypjxmc from ysxt_Ypjx  with(nolock) where ypjxbm='" + ClsSystem.gnvl(db.Rows[i]["YPLX"], "") + "'", Program.ConnectionString), ""); 
                                     dgv2.Rows[i].Cells["ZXSPBM"].Value = ClsSystem.gnvl(db.Rows[i]["ZXSPBM"], "");
                                     dgv2.Rows[i].Cells["CPM"].Value = ClsSystem.gnvl(db.Rows[i]["CPM"], "");
                                     dgv2.Rows[i].Cells["YPJX"].Value = ClsSystem.gnvl(db.Rows[i]["YPJX"], "");

                                     dgv2.Rows[i].Cells["CFGG"].Value = ClsSystem.gnvl(db.Rows[i]["CFGG"], "");

                                     dgv2.Rows[i].Cells["BZDWMC"].Value = ClsSystem.gnvl(db.Rows[i]["BZDWMC"], "");
                                     dgv2.Rows[i].Cells["YYDWMC"].Value = ClsSystem.gnvl(db.Rows[i]["YYDWMC"], "");
                                     dgv2.Rows[i].Cells["BZNHSL"].Value = ClsSystem.gnvl(db.Rows[i]["BZNHSL"], "");
                                     dgv2.Rows[i].Cells["SCQYMC"].Value = ClsSystem.gnvl(db.Rows[i]["SCQYMC"], "");
                                     dgv2.Rows[i].Cells["CGJLDW"].Value = ClsSystem.gnvl(db.Rows[i]["CGJLDW"], "");
                                     dgv2.Rows[i].Cells["CGSL"].Value = ClsSystem.gnvl(db.Rows[i]["CGSL"], "");
                                     //  dgv2.Rows[i].Cells["DCPSBZ"].Value = ClsSystem.gnvl(db.Rows[i]["DCPSBZ"], "");
                                     if (ClsSystem.gnvl(db.Rows[i]["DDTJFS"], "")=="1")
                                     {
                                     dgv2.Rows[i].Cells["DDTJFS"].Value ="医院填报" ;
                                     } else 
                                     {
                                         dgv2.Rows[i].Cells["DDTJFS"].Value = "药企代填";
                                     }
                                     dgv2.Rows[i].Cells["DDCLZT"].Value = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select mc from ysxt_ddclzt  with(nolock) where bm='" + ClsSystem.gnvl(db.Rows[i]["DDCLZT"], "") + "'", Program.ConnectionString), ""); 

                                     dgv2.Rows[i].Cells["DDTJRQ"].Value = ClsSystem.gnvl(db.Rows[i]["DDTJRQ"], "");
                                     dgv2.Rows[i].Cells["BZSM"].Value = ClsSystem.gnvl(db.Rows[i]["BZSM"], "");


                                 }
                             }
                         }
                         else
                         {
                             MessageBox.Show("未查到订单记录" , "错误", MessageBoxButtons.OK);
                             return;
                         }
                     }
                     else
                     {
                         MessageBox.Show("处理结果：" + result + ClsSystem.gnvl(ds.Tables[0].Rows[0]["CWXX"], ""), "错误", MessageBoxButtons.OK);
                         return;
                     }
                }
              

                this.Cursor = Cursors.Default;
          //      MessageBox.Show("未上传的存货查询完成", "提示", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK);
                return;
            }
        }
      
        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
              
                string strSQL = "";
                string BWB = "人民币";
              

                this.Cursor = Cursors.WaitCursor;
                U8Login.clsLogin u8Login = null;
                u8Login = APIinterface.GetU8Login();
                if (u8Login == null)
                {
                    MessageBox.Show("登陆错误：U8登陆失败,检查是否配置正确", "提示", MessageBoxButtons.OK);
                    return;
                }

                //cxdb.DefaultView.Sort = "JHDH ASC";
                //DataTable dt = cxdb.DefaultView.ToTable();
                string strMsg = "";
                string ddh = "";
                string result = "";
                int count=0;
                string YYBM="";
                string DDMXBH="";
                string PSDBM="";
                string sql = "";
              
                string strrq="";
                string strsj="";
                string pdbm = "";
                string pdbm_O = "";
                string DDTJRQ = "";
                DataTable dt = new DataTable();
                dt = cxdb;
              

                if (dt.Rows.Count > 0)
                {
                   
                    dt.Columns.Add("Uflag");
                     for (int i = 0; i < dt.Rows.Count; i++)
                    {          

                        if (ClsSystem.gnvl(this.dgv2.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE")
                        {
                           
                           YYBM= ClsSystem.gnvl(dt.Rows[i]["YYBM"], "");
                           PSDBM = ClsSystem.gnvl(dt.Rows[i]["PSDBM"], "");
                           DDMXBH = ClsSystem.gnvl(dt.Rows[i]["DDMXBH"], "");
                           sql = @" select  count(autoid)   from SO_SODetails  sd  with(nolock) left join so_somain so with(nolock) on sd.id=so.id " +
                              " where so.cdefine11='" + YYBM + "' and sd.cdefine32='" + PSDBM + "' and sd.cdefine29='" + DDMXBH + "'";
                           count = Convert.ToInt16(SqlAccess.ExecuteScalar(sql, Program.ConnectionString));
                             if (count < 1)
                             {
                                 dt.Rows[i]["Uflag"] = "1";
                             }
                            strrq=ClsSystem.gnvl(Public.GetInfo("/",ClsSystem.gnvl(dt.Rows[i]["DDTJRQ"], ""),1),"");
                            strsj=ClsSystem.gnvl(Public.GetInfo("/",ClsSystem.gnvl(dt.Rows[i]["DDTJRQ"], ""),2),"");
                            dt.Rows[i]["DDTJRQ"] = strrq + strsj;
                        }
                     }

                     dt.DefaultView.Sort = "YYBM ASC,PSDBM ASC,DDTJRQ ASC";
                     DataTable db = dt.DefaultView.ToTable();

                     DataView rowfilter = new DataView(db);
                     rowfilter.RowFilter = "Uflag= '1'";
                     rowfilter.RowStateFilter = DataViewRowState.CurrentRows;
                     DataTable dts = rowfilter.ToTable();

                     for (int j = 0; j < dts.Rows.Count; j++)
                    {


                        YYBM = ClsSystem.gnvl(dts.Rows[j]["YYBM"], "");
                        PSDBM = ClsSystem.gnvl(dts.Rows[j]["PSDBM"], "");
                        DDTJRQ = ClsSystem.gnvl(dts.Rows[j]["DDTJRQ"], "");
                        pdbm = YYBM + "-" + PSDBM + "-" + DDTJRQ;
                        if (pdbm != pdbm_O)
                        {

                                strMsg = APIinterface.InSO(u8Login, BWB, dts, j);
                                    if (strMsg.IndexOf("错误") > 0)
                                    {
                                        MessageBox.Show(strMsg, "提示", MessageBoxButtons.OK);
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                    else
                                    {
                                        ddh = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select csocode from SO_SOMain with(nolock) where ID =" + strMsg, Program.ConnectionString), "");
                                        result =result+ ddh + ",";
                                    }
                             
                            }
                        pdbm_O = YYBM + "-" + PSDBM + "-" + DDTJRQ;
                      
                         
                           
                    }
                }

                this.Cursor = Cursors.Default;

                if (result != "")
                {
                    MessageBox.Show("生成销售订单成功:" + result, "提示", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    MessageBox.Show("无销售订单", "提示", MessageBoxButtons.OK);
                    return;
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show("生成失败", "提示", MessageBoxButtons.OK);
                this.Cursor = Cursors.Default;
                return;
            }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgv2.Rows.Count > 0)
            {
                if (this.button4.Text == "全选")
                {

                    for (int i = 0; i <= dgv2.Rows.Count - 1; i++)
                    {
                        dgv2.Rows[i].Cells["check"].Value = true;
                    }
                    this.button4.Text = "全消";
                }
                else
                {
                    for (int i = 0; i <= dgv2.Rows.Count - 1; i++)
                    {
                        dgv2.Rows[i].Cells["check"].Value = false;
                    }
                    this.button4.Text = "全选";
                }

            }
        }
    }
}
