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
    public partial class FrmYQ005 : Form
    {
        public Boolean isTrue = false;
        public static DataTable cxdb = null;

        public FrmYQ005()
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
                string JHDH = "";
                string JHDH_old = "";

                this.Cursor = Cursors.WaitCursor;
                U8Login.clsLogin u8Login = null;
                u8Login = APIinterface.GetU8Login();
                if (u8Login == null)
                {
                    MessageBox.Show("登陆错误：U8登陆失败,检查是否配置正确", "提示", MessageBoxButtons.OK);
                    return;
                }
                cxdb.DefaultView.Sort = "JHDH ASC";
                DataTable dt = cxdb.DefaultView.ToTable();
                string strMsg = "";
                string ddh = "";
                string result = "";
                int count=0;
                string YYBM="";
                string pdbm = "";
                string pdbm_O = "";
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        count =0;                      

                        //if (ClsSystem.gnvl(this.dgv2.Rows[j].Cells["check"].Value, "").ToUpper() == "TRUE")
                        //{
                            JHDH = ClsSystem.gnvl(dt.Rows[j]["JHDH"], "");
                           YYBM= ClsSystem.gnvl(dt.Rows[j]["YYBM"], "");
                           pdbm = YYBM + "-" + JHDH;
                           if (pdbm!=pdbm_O)
                            {
                                count = Convert.ToInt16(SqlAccess.ExecuteScalar(" select count(ID)   from so_somain with(nolock) where    cdefine14  ='" + pdbm + "'", Program.ConnectionString));
                                if (count < 1)
                                {
                                    strMsg = APIinterface.InSO(u8Login, BWB, dt, j);
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
                                //else
                                //{
                                //    MessageBox.Show("计划单号:" + JHDH + "已经生成销售订单", "提示", MessageBoxButtons.OK);
                                //    return;
                                //}
                            }
                      //  }
                           pdbm_O = YYBM + "-" + JHDH; 
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
                    MessageBox.Show("无销售订单成功", "提示", MessageBoxButtons.OK);
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

      
        //清空查询条件
        private void ClearQueryCondition()
        {



       
            this.comboBox1.Text = "";
        
            this.comboBox4.Text = "";
            this.comboBox5.Text = "";
         

        }

        private void FrmYQ005_Load(object sender, EventArgs e)
        {
            this.ClearQueryCondition();
            DataTable db = new DataTable();
            //医院编码
            string sql = " select YYMC,YYBM from ysxt_YyBM  order by YYBM ";

            db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBox1.DataSource = db;
            comboBox1.DisplayMember = "YYMC";
            comboBox1.ValueMember = "YYBM";
            comboBox1.Text = "";

            comboBox4.Text = "托管药库订单";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string xmlData = "";
            string resultXMl = "";
            string sql = "";
            string SBVID = "";
            string SBVID_o = "";
            string yybm = "";
            string yybm_O = "";
            string PSDBM = "";
            string PSDBM_O = "";
            int y = 0;
            int count = 0;
            DataTable dts = null;
            this.Cursor = Cursors.WaitCursor;
            decimal hsje = 0;

            if (ClsSystem.gnvl(comboBox1.Text, "")=="")
            {

                MessageBox.Show("医院没有选择", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return;
        }
            if (ClsSystem.gnvl(comboBox4.Text, "") == "")
            {

                MessageBox.Show("订单类型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox4.Focus();
                return;
            }
            if (ClsSystem.gnvl(comboBox5.Text, "") == "")
            {

                MessageBox.Show("配送点编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox5.Focus();
                return;
            }
            if (dgv2.Rows.Count <1)
            {
                MessageBox.Show("表体无数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
            }

            if (dgv2.Rows.Count > 0)
            {
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    if (!dgv2.Rows[i].IsNewRow)
                    {
                        count++;
                    }
                }
                    xmlData = "";
                    xmlData = xmlData + @"<?xml version=""1.0""  encoding=""utf-8""?>";
                    xmlData = xmlData + "<XMLDATA>";
                    xmlData = xmlData + "<HEAD>";
                    xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP> ";
                    xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC> ";
                    xmlData = xmlData + "<BZXX></BZXX> ";
                    xmlData = xmlData + "</HEAD> ";
                    xmlData = xmlData + "<MAIN>";
                    xmlData = xmlData + "<CZLX>1</CZLX> ";

                    //xmlData = xmlData + "<YQBM>ZDYC0001</YQBM> ";
                    xmlData = xmlData + "<YYBM>" + ClsSystem.gnvl(comboBox1.SelectedValue.ToString(), "") + "</YYBM> ";
                    xmlData = xmlData + "<PSDBM>" + ClsSystem.gnvl(comboBox5.SelectedValue.ToString(), "") + "</PSDBM> ";
                    xmlData = xmlData + "<DDLX>" + ClsSystem.gnvl((comboBox4.SelectedIndex + 1).ToString(), "") + "</DDLX> ";
                    xmlData = xmlData + "<DDBH></DDBH> ";
                    xmlData = xmlData + "<YYJHDH></YYJHDH> ";

                    xmlData = xmlData + "<ZWDHRQ>" + Convert.ToDateTime(dateTimePicker1.Value).Year.ToString("0000") +
                        Convert.ToDateTime(dateTimePicker1.Value).Month.ToString("00") + 
                        Convert.ToDateTime(dateTimePicker1.Value).Day.ToString("00") + 
                        
                        "</ZWDHRQ> ";
                    xmlData = xmlData + "<JLS>" + count.ToString() + "</JLS> ";
                    xmlData = xmlData + "</MAIN>";
                    xmlData = xmlData + "<DETAIL>";

                    for (int i = 0; i < dgv2.Rows.Count; i++)
                    {
                        if (dgv2.Rows[i].IsNewRow)
                        {
                            continue;
                        }
                            //SBVID = ClsSystem.gnvl(this.dgv2.Rows[i].Cells["SBVID"].Value, "");

                            //   dts = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);                    

                            xmlData = xmlData + "<STRUCT>";

                            xmlData = xmlData + "<SXH>" + ClsSystem.gnvl(this.dgv2.Rows[i].Cells["xh"].Value, "") + "</SXH> ";


                         //   xmlData = xmlData + "<CGLX>" + Convert.ToDateTime(dgv2.Rows[i].Cells["dDate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv2.Rows[i].Cells["dDate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv2.Rows[i].Cells["dDate"].Value).Day.ToString("00") + "</CGLX> ";
                            xmlData = xmlData + "<CGLX>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["CGLX"].Value, "0") + "</CGLX>";

                            xmlData = xmlData + "<SPLX>" + ClsSystem.gnvl(this.dgv2.Rows[i].Cells["SPLX"].Value, "") + "</SPLX> ";
                            xmlData = xmlData + "<ZXSPBM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["ZXSPBM"].Value, "") + "</ZXSPBM> ";                   

                            xmlData = xmlData + "<CGJLDW>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["CGJLDW"].Value, "") + "</CGJLDW> ";

                            xmlData = xmlData + "<CGSL>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["CGSL"].Value, "") + "</CGSL> ";

                            xmlData = xmlData + "<CGDJ>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["CGDJ"].Value, "") + "</CGDJ> ";
                            xmlData = xmlData + "<YQBM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["YQBM"].Value, "") + "</YQBM> ";
                        //多次配送标识    
                        xmlData = xmlData + "<DCPSBS>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["DCPSBS"].Value, "") + "</DCPSBS> ";

                            xmlData = xmlData + "<BZSM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["BZSM"].Value, "") + "</BZSM> ";
                       
                            xmlData = xmlData + "</STRUCT>";




                        
                    }
                    xmlData = xmlData + "</DETAIL>";
                    xmlData = xmlData + "</XMLDATA>";
                
                DataSet ds = new DataSet();
                DataTable db = null;
                string DDBH = "";
                string DDMXBH = "";
                string resultXml = SendMessage.SetMessage("YQ005", xmlData);
                string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");
                if (result == "00000")
                {
                    DDBH = SendMessage.ReadXMl(resultXml, "MAIN", "DDBH");
                    textBox1.Text = DDBH;
                    TextReader tdr = new StringReader(resultXml);
                    ds.ReadXml(tdr);
                    if (ds.Tables.Count >= 3)
                    {
                        db = ds.Tables[3];
                        for (int j = 0; j < db.Rows.Count; j++)
                        {
                            DDMXBH = ClsSystem.gnvl(db.Rows[j]["DDMXBH"], "");
                            //  CLJG = ClsSystem.gnvl(db.Rows[j]["CLJG"], "");
                            dgv2.Rows[j].Cells["DDMXBH"].Value = DDMXBH;
                        }
                    }
                    
                }
                else
                {
                   

                    string CWXX = SendMessage.ReadXMl(resultXml, "HEAD", "CWXX");
                    MessageBox.Show("错误编码：" + result + "错误信息:" + CWXX, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //MessageBox.Show("错误编码：" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") + "错误信息:" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["CWXX"], ""), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
              
                this.Cursor = Cursors.Default;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string xmlData = "";
            string resultXMl = "";
            string sql = "";
            decimal CGSL = 0;
            this.Cursor = Cursors.WaitCursor;
         

            if (ClsSystem.gnvl(comboBox1.Text, "") == "")
            {

                MessageBox.Show("医院没有选择", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return;
            }
            if (ClsSystem.gnvl(comboBox4.Text, "") == "")
            {

                MessageBox.Show("订单类型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox4.Focus();
                return;
            }
            if (ClsSystem.gnvl(comboBox5.Text, "") == "")
            {

                MessageBox.Show("配送点编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox5.Focus();
                return;
            }
            if (dgv2.Rows.Count < 1)
            {
                MessageBox.Show("表体无数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (dgv2.Rows.Count > 0)
            {
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    if (dgv2.Rows[i].IsNewRow) continue;
                    //CGSL += int.Parse(ClsSystem.gnvl(this.dgv2.Rows[i].Cells["CGSL"].Value, ""));
                    CGSL++;//商品品种数目
                }
                xmlData = "";
                xmlData = xmlData + @"<?xml version=""1.0""  encoding=""utf-8""?>";
                xmlData = xmlData + "<XMLDATA>";
                xmlData = xmlData + "<HEAD>";
                xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP> ";
                xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC> ";
                xmlData = xmlData + "<BZXX></BZXX> ";
                xmlData = xmlData + "</HEAD> ";

                xmlData = xmlData + "<MAIN>";
                xmlData = xmlData + "<YYBM>" + ClsSystem.gnvl(comboBox1.SelectedValue.ToString(), "") + "</YYBM> ";
                xmlData = xmlData + "<PSDBM>" + ClsSystem.gnvl(comboBox5.SelectedValue.ToString(), "") + "</PSDBM> ";
                xmlData = xmlData + "<DDLX>" + ClsSystem.gnvl((comboBox4.SelectedIndex + 1).ToString(), "") + "</DDLX> ";
                xmlData = xmlData + "<DDBH>" + ClsSystem.gnvl(textBox1.Text, "") + "</DDBH> ";
                xmlData = xmlData + "<SPSL>" + CGSL + "</SPSL> ";
                xmlData = xmlData + "</MAIN>";
             
                xmlData = xmlData + "</XMLDATA>";

                string resultXml = SendMessage.SetMessage("YQ006", xmlData);
                string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");
                if (result == "00000")
                {
                    MessageBox.Show("确认成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                  
                    string CWXX = SendMessage.ReadXMl(resultXml, "HEAD", "CWXX");
                    MessageBox.Show("错误编码：" + result + "错误信息:" + CWXX, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //MessageBox.Show("错误编码：" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") + "错误信息:" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["CWXX"], ""), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
            

                this.Cursor = Cursors.Default;

            }
        }

      

        private void dgv2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            string sql="";
               DataGridViewCell dgc = dgv2.CurrentCell;
              decimal iTaxUnitPrice = 0;
              decimal iDisRate = 0;
              if (dgc.OwningColumn.Name == "ZXSPBM")
              {
                  frmQueryYP HW = new frmQueryYP();
                  //frmQueryHW.strmdcode = mdcode;

                  HW.ShowDialog();

                  if (frmQueryYP.hwID != "")
                  {
                      this.dgv2.Rows[i].Cells["xh"].Value = dgv2.Rows.Count + 1;
                      this.dgv2.Rows[i].Cells["ZXSPBM"].Value = frmQueryYP.hwID;
                      this.dgv2.Rows[i].Cells["YQBM"].Value = "ZDYC0001";
                      sql = @"select  c.cComUnitName from inventory inv left join ComputationUnit  c on c.cComunitCode =inv.cComunitCode  where inv.cInvCode='" + frmQueryYP.hwCode + "'";
                      this.dgv2.Rows[i].Cells["CGJLDW"].Value=ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql,Program.ConnectionString),"");
                      
                  }
              }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isTrue)
            {
                string xmlData = "";
                string resultXMl = "";

                if (ClsSystem.gnvl(comboBox1.Text, "") == "")
                {

                    MessageBox.Show("医院没有选择", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;
                }

                xmlData = "";
                xmlData = xmlData + @"<?xml version=""1.0""  encoding=""utf-8""?>";
                xmlData = xmlData + "<XMLDATA>";
                xmlData = xmlData + "<HEAD>";
                xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP> ";
                xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC> ";
                xmlData = xmlData + "<BZXX></BZXX> ";
                xmlData = xmlData + "</HEAD> ";

                xmlData = xmlData + "<MAIN>";
                xmlData = xmlData + "<YYBM>" + comboBox1.SelectedValue.ToString() + "</YYBM> ";
                xmlData = xmlData + "</MAIN>";
                xmlData = xmlData + "</XMLDATA>";
           //     MessageBox.Show(xmlData);
             //   DataSet ds = SendMessage.QuryData("YQ017", xmlData);
                DataSet ds = new DataSet();
                string resultXml = SendMessage.SetMessage("YQ017", xmlData);
           //     MessageBox.Show(resultXml);
                string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");
             
                if (result == "00000")
                {
                    TextReader tdr = new StringReader(resultXml);
                    ds.ReadXml(tdr);
                }

                if (ds.Tables.Count > 3)
              //  if (ds != null && ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") == "00000")
                {
                    DataTable db = ds.Tables[3];
                    cxdb = ds.Tables[3];

                    if (db.Rows.Count > 0)
                    {
                        comboBox5.DataSource = db;

                        comboBox5.DisplayMember = "PSDMC";
                        comboBox5.ValueMember = "PSDBM";
                        comboBox5.Text = "";

                    }
                }
                else
                {
                    result = SendMessage.ReadXMl(resultXml, "HEAD", "CWXX");
                    MessageBox.Show(result, "错误", MessageBoxButtons.OK);
                }

            }
            isTrue = false;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            isTrue = true;
        }
    }
}
