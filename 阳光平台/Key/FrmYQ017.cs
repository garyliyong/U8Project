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
    public partial class FrmYQ017 : Form
    {

        public static DataTable cxdb = null;

        public FrmYQ017()
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
                xmlData = xmlData + "<YYBM>"+textBox1.Text.Trim()+"</YYBM> ";
                xmlData = xmlData + "</MAIN>";
                xmlData = xmlData + "</XMLDATA>";
              //  DataSet ds = SendMessage.QuryData("YQ017", xmlData);
                DataSet ds = new DataSet();
                 string resultXml= SendMessage.SetMessage("YQ017", xmlData);

                     string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");

                     if (result == "00000")
                     {
                         TextReader tdr = new StringReader(resultXml);
                         ds.ReadXml(tdr);
                     }

             if (ds.Tables.Count>3 )
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
                       
                         dgv2.Rows[i].Cells["PSDBM"].Value = ClsSystem.gnvl(db.Rows[i]["PSDBM"], "");
                         dgv2.Rows[i].Cells["PSDMC"].Value = ClsSystem.gnvl(db.Rows[i]["PSDMC"], "");
                         dgv2.Rows[i].Cells["PSDZ"].Value = ClsSystem.gnvl(db.Rows[i]["PSDZ"], "");



                         dgv2.Rows[i].Cells["LXRXM"].Value = ClsSystem.gnvl(db.Rows[i]["LXRXM"], "");
                         dgv2.Rows[i].Cells["LXDH"].Value = ClsSystem.gnvl(db.Rows[i]["LXDH"], "");
                         dgv2.Rows[i].Cells["YZBM"].Value = ClsSystem.gnvl(db.Rows[i]["YZBM"], "");
                         
                         dgv2.Rows[i].Cells["BZXX"].Value = ClsSystem.gnvl(db.Rows[i]["BZXX"], "");


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
                
                throw;
            }
          
        }

        private void btnCus_Click(object sender, EventArgs e)
        {

            //frmQueryHW.QueryCondition = businessObject.Rows[para.RowKey].Cells["cHwCode"].Value;
            frmQueryHW HW = new frmQueryHW();
            //frmQueryHW.strmdcode = mdcode;

            HW.ShowDialog();

            if (frmQueryHW.hwID != "")
            {
                textBox1.Text = frmQueryHW.hwID;


            }
        }
    }
}
