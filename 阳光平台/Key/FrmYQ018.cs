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
    public partial class FrmYQ018 : Form
    {

        public static DataTable cxdb = null;

        public FrmYQ018()
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
            string xmlData = "";

            try
            {
              

                this.Cursor = Cursors.WaitCursor;

                if (dgv2.Rows.Count > 0)
                {
                    dgv2.Rows.Clear();
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
                xmlData = xmlData + "<FBRQ></FBRQ> ";
                xmlData = xmlData + "<YPTBDM>" + textBox1.Text.Trim() + "</YPTBDM> ";
                xmlData = xmlData + "</MAIN>";
                xmlData = xmlData + "</XMLDATA>";
                DataSet ds = SendMessage.QuryData("YQ018", xmlData);

             if (ds != null && ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") == "00000" )
             {
                 DataTable db = ds.Tables[3];
                 cxdb = ds.Tables[3];

                 if (db.Rows.Count > 0)
                 {
                     for (int i = 0; i < db.Rows.Count; i++)
                     {
                         dgv2.Rows.Add();

                         dgv2.Rows[i].Cells["SFWJ"].Value = ClsSystem.gnvl(ds.Tables[1].Rows[0]["SFWJ"], "");
                         dgv2.Rows[i].Cells["JLS"].Value = ClsSystem.gnvl(ds.Tables[1].Rows[0]["JLS"], "");

                         dgv2.Rows[i].Cells["YPTBDM"].Value = ClsSystem.gnvl(db.Rows[i]["YPTBDM"], "");
                         dgv2.Rows[i].Cells["PZWH"].Value = ClsSystem.gnvl(db.Rows[i]["PZWH"], "");
                         dgv2.Rows[i].Cells["BWM"].Value = ClsSystem.gnvl(db.Rows[i]["BWM"], "");
                         dgv2.Rows[i].Cells["YPTYM"].Value = ClsSystem.gnvl(db.Rows[i]["YPTYM"], "");
                         dgv2.Rows[i].Cells["JX"].Value = ClsSystem.gnvl(db.Rows[i]["JX"], "");
                         dgv2.Rows[i].Cells["GG"].Value = ClsSystem.gnvl(db.Rows[i]["GG"], "");


                         dgv2.Rows[i].Cells["SCQY"].Value = ClsSystem.gnvl(db.Rows[i]["SCQY"], "");
                         dgv2.Rows[i].Cells["SPM"].Value = ClsSystem.gnvl(db.Rows[i]["SPM"], "");
                         dgv2.Rows[i].Cells["BZSL"].Value = ClsSystem.gnvl(db.Rows[i]["BZSL"], "");

                         dgv2.Rows[i].Cells["BZCZ"].Value = ClsSystem.gnvl(db.Rows[i]["BZCZ"], "");
                         dgv2.Rows[i].Cells["BZFS"].Value = ClsSystem.gnvl(db.Rows[i]["BZFS"], "");
                         dgv2.Rows[i].Cells["GGBZWZBS"].Value = ClsSystem.gnvl(db.Rows[i]["GGBZWZBS"], "");
                         dgv2.Rows[i].Cells["JCXXQYRQ"].Value = ClsSystem.gnvl(db.Rows[i]["JCXXQYRQ"], "");
                         dgv2.Rows[i].Cells["JJDW"].Value = ClsSystem.gnvl(db.Rows[i]["JJDW"], "");
                         dgv2.Rows[i].Cells["JHJGGZDM"].Value = ClsSystem.gnvl(db.Rows[i]["JHJGGZDM"], "");

                         dgv2.Rows[i].Cells["JHGZJGJE"].Value = ClsSystem.gnvl(db.Rows[i]["JHGZJGJE"], "");
                         dgv2.Rows[i].Cells["XSJGGZDM"].Value = ClsSystem.gnvl(db.Rows[i]["XSJGGZDM"], "");
                         dgv2.Rows[i].Cells["XSGZJGJE"].Value = ClsSystem.gnvl(db.Rows[i]["XSGZJGJE"], "");
                         dgv2.Rows[i].Cells["WJGZQYRQ"].Value = ClsSystem.gnvl(db.Rows[i]["WJGZQYRQ"], "");
                         dgv2.Rows[i].Cells["CGFS"].Value = ClsSystem.gnvl(db.Rows[i]["CGFS"], "");
                         dgv2.Rows[i].Cells["CGZT"].Value = ClsSystem.gnvl(db.Rows[i]["CGZT"], "");
                         dgv2.Rows[i].Cells["GZYJ"].Value = ClsSystem.gnvl(db.Rows[i]["GZYJ"], "");

                         dgv2.Rows[i].Cells["YSGZQYRQ"].Value = ClsSystem.gnvl(db.Rows[i]["YSGZQYRQ"], "");
                         dgv2.Rows[i].Cells["ZSFBRQ"].Value = ClsSystem.gnvl(db.Rows[i]["ZSFBRQ"], "");
                        



                     }
                 }
             }
             else
             {
                 MessageBox.Show(ClsSystem.gnvl(ds.Tables[0].Rows[0]["CWXX"], ""), "错误", MessageBoxButtons.OK);
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
                Public.ExportExcel(dgv2);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
          
        }

        private void btnCus_Click(object sender, EventArgs e)
        {

            //frmQueryHW.QueryCondition = businessObject.Rows[para.RowKey].Cells["cHwCode"].Value;
            frmQueryYP HW = new frmQueryYP();
            //frmQueryHW.strmdcode = mdcode;

            HW.ShowDialog();

            if (frmQueryYP.hwID != "")
            {
                textBox1.Text = frmQueryYP.hwID;


            }
        }
    }
}
