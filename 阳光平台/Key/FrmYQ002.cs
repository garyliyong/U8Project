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
    public partial class FrmYQ002 : Form
    {
        public FrmYQ002()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "";




            try
            {
//                sql = @"select  inv.cinvcode,inv.cInvName,inv.cCurrencyName,inv.cEnglishName,ex.cidefine8,
//                     inv.cInvStd,inv.cEnterprise ,ex.cidefine1,ex.cidefine2,inv.cFile ,ex.cidefine3,c.cComUnitName, ex.cidefine4
//                   from Inventory inv join Inventory_extradefine ex on  inv.cinvcode=ex.cInvCode
//                  join ComputationUnit c on c.cComunitCode=inv.cCAComUnitCode
//                  where   isnull(inv.cInvDefine10,'')='' order by inv.cinvcode";
                sql = @"select top 100 inv.cinvcode ,inv.cinvname, iex.cidefine1,com.cComUnitName , sum( case when isnull(bgspstop,0) =1 or isnull(bstopflag,0) =1 then 0 else ISNULL(iQuantity,0)-isnull(fStopQuantity,0) - ISNULL(fOutQuantity,0) end) AS fAvailQtty   
                    from CurrentStock csk  join inventory inv on inv.cinvcode=csk.cinvcode
left join Inventory_extradefine  iex on iex.cinvcode=inv.cinvcode
left join ComputationUnit com on com.cComUnitCode=inv.cComUnitCode
where isnull(iex.cidefine1,'')<>''
 group by inv.cinvcode ,inv.cinvname,iex.cidefine1 ,com.cComUnitName 
having  sum( case when isnull(bgspstop,0) =1 or isnull(bstopflag,0) =1 then 0 else ISNULL(iQuantity,0)-isnull(fStopQuantity,0) - ISNULL(fOutQuantity,0) end) <>0 ";

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
                        dgv2.Rows[i].Cells["ZXSPBM"].Value = ClsSystem.gnvl(db.Rows[i]["cidefine1"], "");
                        dgv2.Rows[i].Cells["SPLX"].Value = "药品";
                        dgv2.Rows[i].Cells["KCSL"].Value = ClsSystem.gnvl(db.Rows[i]["fAvailQtty"], "");

                        dgv2.Rows[i].Cells["KCDW"].Value = ClsSystem.gnvl(db.Rows[i]["cComUnitName"], "");
                       


                    }
                }

                this.Cursor = Cursors.Default;
          //      MessageBox.Show("未上传的存货查询完成", "提示", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK);
                return ;
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
               
                    xmlData = "";
                    xmlData = xmlData + @"<?xml version=""1.0""  encoding=""utf-8""?>";
                    xmlData = xmlData + "<XMLDATA>";
                    xmlData = xmlData + "<HEAD>";
                    xmlData = xmlData + "<IP>" + SendMessage.GetIP() + "</IP> ";
                    xmlData = xmlData + "<MAC>" + SendMessage.GetMAC() + "</MAC> ";
                    xmlData = xmlData + "<BZXX></BZXX> ";
                    xmlData = xmlData + "</HEAD> ";
                    xmlData = xmlData + "<MAIN>";
                    xmlData = xmlData + "<KCCBSJ>" + DateTime.Today.Year.ToString("0000") + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + "/" + DateTime.Today.Hour.ToString("00") + DateTime.Today.Minute.ToString("00") + DateTime.Today.Second.ToString("00")+"/" + "</KCCBSJ> ";
                    xmlData = xmlData + "<JLS>" + dgv2.Rows.Count .ToString()+ "</JLS> ";               
                 
                    xmlData = xmlData + "</MAIN>";
                    xmlData = xmlData + "<DETAIL>";
                    for (int i = 0; i < dgv2.Rows.Count; i++)
                    {
                        xmlData = xmlData + "<STRUCT>";
                        xmlData = xmlData + "<SPLX>1</SPLX> ";
                        xmlData = xmlData + "<ZXSPBM>"+ClsSystem.gnvl(dgv2.Rows[i].Cells["ZXSPBM"].Value, "")+"</ZXSPBM> ";
                        xmlData = xmlData + "<YPKCL>1</YPKCL> ";
                        xmlData = xmlData + "<KCSL>" + ClsSystem.gnvl(Public.ChinaRound( Public.GetNum( dgv2.Rows[i].Cells["KCSL"].Value),0), "0") + "</KCSL> ";
                        xmlData = xmlData + "<KCDW>"+ClsSystem.gnvl(dgv2.Rows[i].Cells["KCDW"].Value, "")+"</KCDW> ";
                        xmlData = xmlData + "</STRUCT>";
                    }
                    xmlData = xmlData + "</DETAIL>";
                    xmlData = xmlData + "</XMLDATA>";

                    resultXMl = SendMessage.SetMessage("YQ002", xmlData);


                    //string result = SendMessage.ReadXMl(resultXMl, "HEAD", "ZTCLJG");
                 
                    //if (result == "00000")
                    //{
                    ////    sql = @" update inventory set cInvDefine10='00000' where cinvcode='" + ClsSystem.gnvl(dgv2.Rows[i].Cells["cinvcode"].Value, "") + "'";
                    ////    SqlAccess.ExecuteSql(sql, Program.ConnectionString);
                    //     dgv2.Rows[i].Cells["resultXMl"].Value = ClsSystem.gnvl(result, "");
                    //}
                   

                
            }

            this.Cursor = Cursors.Default;
        }
    }
}
