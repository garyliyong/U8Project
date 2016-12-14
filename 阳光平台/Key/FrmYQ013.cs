using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SHYSInterface
{
    public partial class FrmYQ013 : Form
    {

        public static DataTable cxdb = null;

        public FrmYQ013()
        {
            InitializeComponent();
        }
      
     

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count > 0)
            {
                if (this.button4.Text == "全选")
                {

                    for (int i = 0; i <= dgv1.Rows.Count - 1; i++)
                    {
                        dgv1.Rows[i].Cells["check"].Value = true;
                    }
                    this.button4.Text = "全消";
                }
                else
                {
                    for (int i = 0; i <= dgv1.Rows.Count - 1; i++)
                    {
                        dgv1.Rows[i].Cells["check"].Value = false;
                    }
                    this.button4.Text = "全选";
                }

            }
        }
        private void GetTable()
        {

            DataSet dt = new DataSet();
            SqlDataAdapter thisAdapter = null;
            string sql = "";

            try
            {
                dgv1.EndEdit();

                DataTable db = (DataTable)dgv1.DataSource;

                if (db != null)
                {
                    db.Rows.Clear();
                }
                dgv1.DataSource = db;


                sql = @" select a.cSBVCode  , a.dDate ,a.bReturnFlag,a.ccusname ,
                        a.cDefine14,a.cMaker ,a.cdefine11,
                        b.cinvcode,b.cinvname,b.cDefine29,b.cBatch,b.cmemo ,  b.cdefine25,b.iDLsID,b.cDefine32,b.cDefine33,'ZD1' +Convert(char(10),b.iDLsID) as PSDTM,a.sbvid
                     from  SaleBillVouchZT  a   join SaleBillVouchZW b on a.sbvid=b.sbvid  where 
                       ISNULL(a.cChecker,'')<>''";
                 
                if (ClsSystem.gnvl(textBox1.Text, "") != "")
                {
                    sql = sql + " and a.cSBVCode>='" + ClsSystem.gnvl(textBox1.Text, "") + "'";
                }
                if (ClsSystem.gnvl(textBox2.Text, "") != "")
                {
                    sql = sql + " and a.cSBVCode<='" + ClsSystem.gnvl(textBox2.Text, "") + "'";
                }
                if (ClsSystem.gnvl(dt1.Value, "") != "")
                {
                    sql = sql + " and datediff(day,a.ddate,'" + ClsSystem.gnvl(dt1.Value.ToShortDateString(), "") + "')<=0 ";

                }
                if (ClsSystem.gnvl(dt2.Value, "") != "")
                {
                    sql = sql + " and datediff(day,a.ddate,'" + ClsSystem.gnvl(dt2.Value.ToShortDateString(), "") + "')>=0 ";
                }

                if (checkbox1.Checked == true)
                {
                    sql = sql + " and a.bReturnFlag= 1 ";
                }
                else
                {
                    sql = sql + " and a.bReturnFlag= 0 ";
                }
                if (checkBox2.Checked == true)
                {
                    sql = sql + " and isnull(b.cdefine25,'')<> ''";
                }
                else
                {
                    sql = sql + " and isnull(b.cdefine25,'')=''";
                }
                //sql = sql + "   group by a.DLID ,a.cdlCode  ,a.cSTCode , a.dDate ,a.bReturnFlag, a.cMemo ,a.cMaker, a.cDefine1,a.cDefine2,a.cDefine3,a.cDefine10,a.cDefine11,a.cDefine12,a.cDefine13";
                sql = sql + "   order by a.dDate    ";
                thisAdapter = new SqlDataAdapter(sql, Program.ConnectionString);

                thisAdapter.Fill(dt);

                this.dgv1.DataSource = dt.Tables[0];
                this.button4.Text = "全选";
                //  this.textBox1.Text = "查询到 " + dt.Tables[0].Rows.Count.ToString() + " 条记录。";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            GetTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
               string xmlData = "";
            string resultXMl = "";
            string sql = "";
            string sbvid = "";
            string yybm="";
            string yybm_O = "";
            int y = 0;
            DataTable dts = null;
            this.Cursor = Cursors.WaitCursor;
            if (dgv1.Rows.Count > 0)
            {
                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE" )
                    {


                        if (ClsSystem.gnvl(dgv1.Rows[i].Cells["cmemo"].Value, "") == "")
                       {
                           MessageBox.Show("终止原因未填入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           return;
                       }
                        if (ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine29"].Value, "") == "")
                       {
                           MessageBox.Show("订单明细编号未填入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           return;
                       }
                       yybm_O = yybm;
                       y++;
                    }
                }

                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE")
                    {
                        sbvid = ClsSystem.gnvl(this.dgv1.Rows[i].Cells["sbvid"].Value, "");


                        xmlData = "";
                        xmlData = xmlData + @"<?xml version=""1.0""  encoding=""utf-8""?>";
                        xmlData = xmlData + "<XMLDATA>";
                        xmlData = xmlData + "<HEAD>";
                        xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP> ";
                        xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC> ";
                        xmlData = xmlData + "<BZXX></BZXX> ";
                        xmlData = xmlData + "</HEAD> ";
                        xmlData = xmlData + "<MAIN>";

                        xmlData = xmlData + "<DDMXBH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine29"].Value, "") + "</DDMXBH> ";

                        xmlData = xmlData + "<ZZYY>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cmemo"].Value, "") + "</ZZYY> ";

                        xmlData = xmlData + "</MAIN>";

                        xmlData = xmlData + "</XMLDATA>";

                        string resultXml = SendMessage.SetMessage("YQ013", xmlData);
                        string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");

                        if (result == "00000")
                        {

                            dgv1.Rows[i].Cells["result"].Value = result;

                        }
                        else
                        {
                            MessageBox.Show("错误编码：" + result + "错误信息:" + SendMessage.ReadXMl(resultXml, "HEAD", "CWXX"), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                  
                }
            }
         
             
            
         

            this.Cursor = Cursors.Default;
        
        }

        private void dgv1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
//            dgv1.EndEdit();
//            string dlid = ClsSystem.gnvl(dgv1.CurrentRow.Cells["dlid"].Value, "");

//            string sql = @" select cinvcode,cinvname,cDefine28,iQuantity,cInvDefine3,cDefine29,cDefine30,cBatch ,dMDate ,cExpirationdate ,cSoCode 
//                     from  Sales_FHD_w  where dlid=" + dlid                   ;

//            DataTable db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
//            dgv2.DataSource = db;

        }

      
    }
}
