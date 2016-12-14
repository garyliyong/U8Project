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
    public partial class FrmYQ016 : Form
    {

        public static DataTable cxdb = null;
        string cSBVCodeNew = "";

        public FrmYQ016()
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

                if (dgv1.Rows.Count > 0)
                {
                    dgv1.Rows.Clear();
                }
               
                this.Cursor = Cursors.WaitCursor;

              

                sql = @" selecta.cSBVCode, a.dDate ,a.ccusname ,
                        a.cDefine12,a.cDefine13,a.cDefine14,a.cDefine15,a.cDefine8, a.cMaker, a.cMemo ,a.SBVID,a.cdefine11,
                       
                     from SaleBillVouchZT  a   
                       where   ISNULL(a.cChecker,'')<>'' and   ISNULL(a.cdefine11 ,'')<>'' ";

                if (ClsSystem.gnvl(textBox1.Text, "") != "")
                {
                    sql = sql + " and a.cSBVCode >='" + ClsSystem.gnvl(textBox1.Text, "") + "'";
                }
                if (ClsSystem.gnvl(textBox2.Text, "") != "")
                {
                    sql = sql + " and a.cSBVCode <='" + ClsSystem.gnvl(textBox2.Text, "") + "'";
                }
                if (ClsSystem.gnvl(dt1.Value, "") != "")
                {
                    sql = sql + " and datediff(day,a.ddate,'" + ClsSystem.gnvl(dt1.Value.ToShortDateString(), "") + "')<=0 ";

                }
                if (ClsSystem.gnvl(dt2.Value, "") != "")
                {
                    sql = sql + " and datediff(day,a.ddate,'" + ClsSystem.gnvl(dt2.Value.ToShortDateString(), "") + "')>=0 ";
                }

                //if (checkbox1.Checked == true)
                //{
                //    sql = sql + " and a.bReturnFlag= 1 ";
                //}
                //else
                //{
                //    sql = sql + " and a.bReturnFlag= 0 ";
                //}
                if (checkBox2.Checked == true)
                {
                    sql = sql + " and isnull(b.cDefine24,'')<> ''";
                }
                else
                {
                    sql = sql + " and isnull(b.cDefine24,'')=''";
                }
                //sql = sql + "   group by a.DLID ,a.cdlCode  ,a.cSTCode , a.dDate ,a.bReturnFlag, a.cMemo ,a.cMaker, a.cDefine1,a.cDefine2,a.cDefine3,a.cDefine10,a.cDefine11,a.cDefine12,a.cDefine13";
                sql = sql + "   order by a.dDate    ";
                thisAdapter = new SqlDataAdapter(sql, Program.ConnectionString);

                thisAdapter.Fill(dt);


                DataTable db = dt.Tables[0];
                if (db.Rows.Count > 0)
                {
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        dgv1.Rows.Add();

                      
                        dgv1.Rows[i].Cells["cCusName"].Value = ClsSystem.gnvl(db.Rows[i]["cCusName"], "");
                        dgv1.Rows[i].Cells["cdefine14"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine14"], "");
                     
                        dgv1.Rows[i].Cells["dDate"].Value = ClsSystem.gnvl(db.Rows[i]["dDate"], "");
                        dgv1.Rows[i].Cells["cSBVCode"].Value = ClsSystem.gnvl(db.Rows[i]["cSBVCode"], "");
                    
                        dgv1.Rows[i].Cells["cMaker"].Value = ClsSystem.gnvl(db.Rows[i]["cMaker"], "");

                        dgv1.Rows[i].Cells["cMemo"].Value = ClsSystem.gnvl(db.Rows[i]["cMemo"], "");
                        dgv1.Rows[i].Cells["cDefine11"].Value = ClsSystem.gnvl(db.Rows[i]["cDefine11"], "");
                        dgv1.Rows[i].Cells["sbvid"].Value = ClsSystem.gnvl(db.Rows[i]["sbvid"], "");
                      //  dgv1.Rows[i].Cells["cFile"].Value = ClsSystem.gnvl(db.Rows[i]["cFile"], "");


                    }
                }
                //this.dgv1.DataSource = dt.Tables[0];
                this.button4.Text = "全选";
                this.Cursor = Cursors.Default;
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
            if (dgv1.Rows.Count > 0)
            {
               
                if (dgv1.Rows.Count > 0)
                {
                    xmlData = "";
                    xmlData = xmlData + @"<?xml version=""1.0""  encoding=""utf-8""?>";
                    xmlData = xmlData + "<XMLDATA>";
                    xmlData = xmlData + "<HEAD>";
                    xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP> ";
                    xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC> ";
                    xmlData = xmlData + "<BZXX></BZXX> ";
                    xmlData = xmlData + "</HEAD> ";
                    xmlData = xmlData + "<MAIN>";                
                    xmlData = xmlData + "<YQBM>ZDYC0001</YQBM> ";
                    xmlData = xmlData + "<YYBM>" + ClsSystem.gnvl(dgv1.Rows[count].Cells["cdefine11"].Value, "") + "</YYBM> ";
                    xmlData = xmlData + "<FPH>" + ClsSystem.gnvl(dgv1.Rows[count].Cells["cSBVCode"].Value, "") + "</FPH> ";

                    xmlData = xmlData + "<FPHSZJE>" + ClsSystem.gnvl( SqlAccess.ExecuteScalar(" select isnull(sum(iSum),0) from SaleBillVouchs where  sbvid="+ClsSystem.gnvl(dgv1.Rows[count].Cells["sbvid"].Value, ""),Program.ConnectionString),"") + "</FPHSZJE> ";
                    xmlData = xmlData + "</MAIN>";
                    
                    xmlData = xmlData + "</XMLDATA>";
                }
                DataSet ds = new DataSet();
                DataTable db = null;
                string iDLsID = "";
                string CLJG = "";
                string resultXml = SendMessage.SetMessage("YQ016", xmlData);
                string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");
                if (result == "00000")
                {



                    sql = " update SaleBillVouch set cdefine9='" + result + "' where cSBVCode='" + ClsSystem.gnvl(dgv1.Rows[count].Cells["cSBVCode"].Value, "") + "'\n";

                    
                }
                else
                {
                    //TextReader tdr = new StringReader(resultXml);
                    //ds.ReadXml(tdr);

                    //if (ds.Tables.Count >= 4)
                    //{
                    //    db = ds.Tables[2];
                    //}
                    //CLJG = ClsSystem.gnvl(db.Rows[0]["CLJG"], "");
                    //string CLQKMS = ClsSystem.gnvl(db.Rows[0]["CLQKMS"], "");
                //    dgv1.Rows[i].Cells["cdefine24"].Value = "错误编码：" + CLJG + "错误信息:" + CLQKMS;

                    string CWXX = SendMessage.ReadXMl(resultXml, "HEAD", "CWXX");
                    MessageBox.Show("错误编码：" + result + "错误信息:" + CWXX, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //MessageBox.Show("错误编码：" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") + "错误信息:" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["CWXX"], ""), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                SqlAccess.ExecuteSql(sql, Program.ConnectionString);

                this.Cursor = Cursors.Default;

            }
        }

        private void dgv1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgv1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            int row = e.RowIndex;
            cSBVCodeNew = ClsSystem.gnvl(this.dgv1.Rows[row].Cells["cSBVCode"].Value, "");
            dgv1.EndEdit();

            //    DataGridViewCell dgc = dgv1.CurrentCell;
            //if (dgc.OwningColumn.Name == "check")
            //{
            //dgv1.RefreshEdit();


            for (int i = 0; i <= dgv1.Rows.Count - 1; i++)
            {
                if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE")
                {
                    if (ClsSystem.gnvl(dgv1.Rows[i].Cells["cSBVCode"].Value, "") == cSBVCodeNew)
                    {
                        dgv1.Rows[i].Cells["check"].Value = true;
                    }
                }
                else
                {
                    if (ClsSystem.gnvl(dgv1.Rows[i].Cells["cSBVCode"].Value, "") == cSBVCodeNew)
                    {
                        dgv1.Rows[i].Cells["check"].Value = false;
                    }
                }
            }
            //dgv1.EndEdit();
            // dgv1.Refresh();
            //  }
        }

        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           // dgv1.Refresh();
          
          
        }

        private void dgv1_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

      

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgv1.EndEdit();
        }




    }
}
