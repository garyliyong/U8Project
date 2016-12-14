using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SHYSInterface
{
    public partial class FrmYQ001 : Form
    {
        public FrmYQ001()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "";




            try
            {
                sql = @"select  inv.cinvcode,inv.cInvName,inv.cCurrencyName,inv.cEnglishName,ex.cidefine8,
                     inv.cInvStd,inv.cEnterprise ,ex.cidefine1,ex.cidefine2,inv.cFile ,ex.cidefine3,c.cComUnitName, ex.cidefine4
                   from Inventory inv join Inventory_extradefine ex on  inv.cinvcode=ex.cInvCode
                  join ComputationUnit c on c.cComunitCode=inv.cCAComUnitCode
                  where   isnull(inv.cInvDefine10,'')='' order by inv.cinvcode";


                this.Cursor = Cursors.WaitCursor;




                if (dgv2.Rows.Count > 0)
                {
                    dgv2.Rows.Clear();
                }


                DataTable db = SqlAccess.ExecuteSqlDataTable(sql,Program.ConnectionString);



                if (db.Rows.Count > 0)
                {
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        dgv2.Rows.Add();

                        dgv2.Rows[i].Cells["cinvcode"].Value = ClsSystem.gnvl(db.Rows[i]["cinvcode"], "");
                        dgv2.Rows[i].Cells["cInvName"].Value = ClsSystem.gnvl(db.Rows[i]["cInvName"], "");
                        dgv2.Rows[i].Cells["cidefine1"].Value = ClsSystem.gnvl(db.Rows[i]["cidefine1"], "");
                        dgv2.Rows[i].Cells["cCurrencyName"].Value = ClsSystem.gnvl(db.Rows[i]["cCurrencyName"], "");
                        dgv2.Rows[i].Cells["cEnglishName"].Value = ClsSystem.gnvl(db.Rows[i]["cEnglishName"], "");

                        dgv2.Rows[i].Cells["cidefine8"].Value = ClsSystem.gnvl(db.Rows[i]["cidefine8"], "");
                        dgv2.Rows[i].Cells["cInvStd"].Value = ClsSystem.gnvl(db.Rows[i]["cInvStd"], "");
                        dgv2.Rows[i].Cells["cEnterprise"].Value = ClsSystem.gnvl(db.Rows[i]["cEnterprise"], "");
                        dgv2.Rows[i].Cells["cidefine2"].Value = ClsSystem.gnvl(db.Rows[i]["cidefine2"], "");

                        dgv2.Rows[i].Cells["cFile"].Value = ClsSystem.gnvl(db.Rows[i]["cFile"], "");
                        dgv2.Rows[i].Cells["cidefine3"].Value = ClsSystem.gnvl(db.Rows[i]["cidefine3"], "");
                        dgv2.Rows[i].Cells["cComUnitName"].Value = ClsSystem.gnvl(db.Rows[i]["cComUnitName"], "");
                        dgv2.Rows[i].Cells["cidefine4"].Value = ClsSystem.gnvl(db.Rows[i]["cidefine4"], "");


                    }
                }

                this.Cursor = Cursors.Default;
          //      MessageBox.Show("未上传的存货查询完成", "提示", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK);
                throw;
            }
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
            string xmlData = "";
            string resultXMl = "";
            string sql = "";
            this.Cursor = Cursors.WaitCursor;
            if (dgv2.Rows.Count > 0)
            {
                for (int i = 0; i < dgv2.Rows.Count; i++)
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
                    xmlData = xmlData + "<CZLX>1</CZLX> ";
                    xmlData = xmlData + "<SPLX>1</SPLX> ";
                    xmlData = xmlData + "<YPLX>1</YPLX> ";
                    xmlData = xmlData + "<TYM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cCurrencyName"].Value, "") + "</TYM> ";
                    xmlData = xmlData + "<CPM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cInvName"].Value, "") + "</CPM> ";
                    xmlData = xmlData + "<YWM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cEnglishName"].Value, "") + "</YWM> ";
                    xmlData = xmlData + "<SPM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cInvName"].Value, "") + "</SPM> ";
                    xmlData = xmlData + "<YPSPTXM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cidefine8"].Value, "") + "</YPSPTXM> ";
                    xmlData = xmlData + "<YPJX>1</YPJX> ";
                    xmlData = xmlData + "<GG>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cInvStd"].Value, "") + "</GG> ";
                    xmlData = xmlData + "<SCQYMC>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cEnterprise"].Value, "") + "</SCQYMC> ";
                    xmlData = xmlData + "<CPLB>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cInvStd"].Value, "") + "</CPLB> ";
                    xmlData = xmlData + "<YPBWM>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cidefine2"].Value, "") + "</YPBWM> ";
                    xmlData = xmlData + "<YPPZWH>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cFile"].Value, "") + "</YPPZWH> ";
                    xmlData = xmlData + "<BZCZ>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cidefine3"].Value, "") + "</BZCZ> ";
                    xmlData = xmlData + "<BZDW>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cComUnitName"].Value, "") + "</BZDW> ";
                    xmlData = xmlData + "<CGYYDW>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cComUnitName"].Value, "") + "</CGYYDW> ";
                    xmlData = xmlData + "<BZSL>1</BZSL> ";
                    xmlData = xmlData + "<BZFS>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cidefine4"].Value, "") + "</BZFS> ";
                    xmlData = xmlData + "<TZMS>" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cComUnitName"].Value, "") + "</TZMS> ";
                    xmlData = xmlData + "</MAIN>";
                    xmlData = xmlData + "</XMLDATA>";

                    resultXMl = SendMessage.SetMessage("YQ001", xmlData);


                    string result = SendMessage.ReadXMl(resultXMl, "HEAD", "ZTCLJG");
                 
                    if (result == "00000")
                    {
                        sql = @" update inventory set cInvDefine10='00000' where cinvcode='" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cinvcode"].Value, "") + "'";
                        SqlAccess.ExecuteSql(sql, Program.ConnectionString);
                    }
                    dgv2.Rows[i].Cells["resultXMl"].Value = ClsSystem.gnvl(result, "");

                }
            }

            this.Cursor = Cursors.Default;
        }
    }
}
