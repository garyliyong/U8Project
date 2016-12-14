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
    public partial class FrmYQ003 : Form
    {

        public static DataTable cxdb = null;

        public FrmYQ003()
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
                //DataTable db = (DataTable)dgv1.DataSource;

                //if (db != null)
                //{
                //    db.Rows.Clear();
                //}
             //   dgv1.DataSource = db;
                this.Cursor = Cursors.WaitCursor;

//                sql = @" select a.cdefine9 ,a.cdlCode  , a.dDate ,a.cSTCode ,a.bReturnFlag,a.ccusname ,
//                        a.cDefine12,a.cDefine13,a.cDefine14,a.cDefine8, a.cMaker, a.cMemo ,a.DLID,a.cdefine11,
//                    b.cinvcode,b.cinvname,b.iQuantity,b.cInvDefine5,b.cDefine28,b.cDefine29,b.cDefine30,b.cBatch ,b.dMDate ,b.cExpirationdate  ,
//                 b.cdefine25,b.iDLsID,b.cDefine32,b.cDefine33,b.cDefine35
//                     from Sales_FHD_T  a join Sales_FHD_W  b on b.dlid=a.dlid  where 
//                        ISNULL(a.cverifier ,'')<>''  and   ISNULL(a.cdefine11 ,'')<>'' ";

//                if (ClsSystem.gnvl(textBox1.Text, "") != "")
//                {
//                    sql = sql + " and a.cdlCode>='" + ClsSystem.gnvl(textBox1.Text, "") + "'";
//                }
//                if (ClsSystem.gnvl(textBox2.Text, "") != "")
//                {
//                    sql = sql + " and a.cdlCode<='" + ClsSystem.gnvl(textBox2.Text, "") + "'";
//                }
//                if (ClsSystem.gnvl(dt1.Value, "") != "")
//                {
//                    sql = sql + " and datediff(day,a.ddate,'" + ClsSystem.gnvl(dt1.Value.ToShortDateString(), "") + "')<=0 ";

//                }
//                if (ClsSystem.gnvl(dt2.Value, "") != "")
//                {
//                    sql = sql + " and datediff(day,a.ddate,'" + ClsSystem.gnvl(dt2.Value.ToShortDateString(), "") + "')>=0 ";
//                }

//                if (checkbox1.Checked == true)
//                {
//                    sql = sql + " and a.bReturnFlag= 1 ";
//                }
//                else
//                {
//                    sql = sql + " and a.bReturnFlag= 0 ";
//                }
//                if (checkBox2.Checked == true)
//                {
//                    sql = sql + " and isnull(b.cdefine25,'')<> ''";
//                }
//                else
//                {
//                    sql = sql + " and isnull(b.cdefine25,'')=''";
//                }
//                //sql = sql + "   group by a.DLID ,a.cdlCode  ,a.cSTCode , a.dDate ,a.bReturnFlag, a.cMemo ,a.cMaker, a.cDefine1,a.cDefine2,a.cDefine3,a.cDefine10,a.cDefine11,a.cDefine12,a.cDefine13";
//                sql = sql + "   order by a.dDate    ";
                
                sql = @" select a.cdefine9,a.cdefine10 ,a.cSBVCode, a.dDate ,a.cSTCode ,a.bReturnFlag,a.ccusname ,
                        a.cDefine12,a.cDefine13,a.cDefine14,a.cDefine15,a.cDefine8, a.cMaker, a.cMemo ,a.SBVID,a.cdefine11,
                          b.cinvcode,b.cinvname,b.cDefine28,b.iQuantity,b.cInvDefine5,b.cDefine29,b.cDefine30,b.cBatch ,
                           b.cSoCode ,inv.cFile ,b.dMDate ,b.cExpirationdate ,b.iUnitPrice ,b.iTaxUnitPrice ,b.iTaxRate ,
                           b.iTax ,b.iMoney ,b.iSum ,       b.iQuotedPrice,   inv.fRetailPrice,
                     b.cdefine23,   b.cdefine24, b.cdefine25,b.iDLsID,b.cDefine32,b.cDefine33,b.cdefine34,a.sbvid,
                           inv.cFile,inv.cInvStd ,inv.cAddress ,b.iNum
                     from SaleBillVouchZT  a   join SaleBillVouchZW b on a.sbvid=b.sbvid 
                          left join inventory inv  on inv.cinvcode=b.cinvcode
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
                DataTable db = dt.Tables[0];
                if (db.Rows.Count > 0)
                {
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        dgv1.Rows.Add();

                        dgv1.Rows[i].Cells["zxlx"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine23"], "");
                        dgv1.Rows[i].Cells["cdefine24"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine24"], "");
                        dgv1.Rows[i].Cells["cdefine25"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine25"], "");
                        dgv1.Rows[i].Cells["PSDTM"].Value = "ZD1" + ClsSystem.gnvl(db.Rows[i]["iDLsID"], "");
                        dgv1.Rows[i].Cells["cCusName"].Value = ClsSystem.gnvl(db.Rows[i]["cCusName"], "");
                        dgv1.Rows[i].Cells["cdefine14"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine14"], "");
                        dgv1.Rows[i].Cells["cdefine32"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine32"], "");

                        dgv1.Rows[i].Cells["cdefine33"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine33"], "");
                        dgv1.Rows[i].Cells["cdefine34"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine34"], "");
                        dgv1.Rows[i].Cells["dDate"].Value = ClsSystem.gnvl(db.Rows[i]["dDate"], "");
                        dgv1.Rows[i].Cells["cSBVCode"].Value = ClsSystem.gnvl(db.Rows[i]["cSBVCode"], "");
                        dgv1.Rows[i].Cells["cInvCode"].Value = ClsSystem.gnvl(db.Rows[i]["cInvCode"], "");
                        dgv1.Rows[i].Cells["cInvName"].Value = ClsSystem.gnvl(db.Rows[i]["cInvName"], "");

                        dgv1.Rows[i].Cells["iQuantity"].Value = ClsSystem.gnvl(db.Rows[i]["iQuantity"], "");
                        dgv1.Rows[i].Cells["iNum"].Value = ClsSystem.gnvl(db.Rows[i]["iNum"], "");

                        dgv1.Rows[i].Cells["cInvDefine5"].Value = ClsSystem.gnvl(db.Rows[i]["cInvDefine5"], "");
                        dgv1.Rows[i].Cells["cDefine28"].Value = ClsSystem.gnvl(db.Rows[i]["cDefine28"], "");
                        dgv1.Rows[i].Cells["cDefine29"].Value = ClsSystem.gnvl(db.Rows[i]["cDefine29"], "");

                        dgv1.Rows[i].Cells["cDefine30"].Value = ClsSystem.gnvl(db.Rows[i]["cDefine30"], "");

                        dgv1.Rows[i].Cells["iDLsID"].Value = ClsSystem.gnvl(db.Rows[i]["iDLsID"], "");
                        dgv1.Rows[i].Cells["cBatch"].Value = ClsSystem.gnvl(db.Rows[i]["cBatch"], "");
                        dgv1.Rows[i].Cells["dMDate"].Value = ClsSystem.gnvl(db.Rows[i]["dMDate"], "");
                        dgv1.Rows[i].Cells["cExpirationdate"].Value = ClsSystem.gnvl(db.Rows[i]["cExpirationdate"], "");
                        dgv1.Rows[i].Cells["bReturnFlag"].Value = ClsSystem.gnvl(db.Rows[i]["bReturnFlag"], "");

                        dgv1.Rows[i].Cells["cMaker"].Value = ClsSystem.gnvl(db.Rows[i]["cMaker"], "");

                        dgv1.Rows[i].Cells["cMemo"].Value = ClsSystem.gnvl(db.Rows[i]["cMemo"], "");
                        dgv1.Rows[i].Cells["cDefine11"].Value = ClsSystem.gnvl(db.Rows[i]["cDefine11"], "");
                        dgv1.Rows[i].Cells["sbvid"].Value = ClsSystem.gnvl(db.Rows[i]["sbvid"], "");

                        dgv1.Rows[i].Cells["cInvStd"].Value = ClsSystem.gnvl(db.Rows[i]["cInvStd"], "");
                        dgv1.Rows[i].Cells["cAddress"].Value = ClsSystem.gnvl(db.Rows[i]["cAddress"], "");


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
            string xmlData = "";
            string sql = "";
            string sbvid = "";
            //  int y = 0;

            this.Cursor = Cursors.WaitCursor;
            if (dgv1.Rows.Count > 0)
            {
                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE" && ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cdefine25"].Value, "").ToUpper() == "00000")
                    {
                        //yybm= ClsSystem.gnvl(dgv1.Rows[i].Cells["cdefine11"].Value, "");
                        //PSDBM = ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine32"].Value, "");
                        //if (yybm_O != yybm&&y>0)
                        //{
                        //    MessageBox.Show("请选择同一客户上传", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                        //if (PSDBM_O != PSDBM && y > 0)
                        //{
                        //    MessageBox.Show("请选择同一医院配送点编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                        if (ClsSystem.gnvl(ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine34"].Value, ""), "") == "")
                        {
                            MessageBox.Show("配送箱数未填入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (ClsSystem.gnvl(dgv1.Rows[i].Cells["zxlx"].Value, "") == "")
                        {
                            MessageBox.Show("装箱类型未填入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //yybm_O = yybm;
                        //PSDBM_O = PSDBM;
                        //y++;

                    }
                }

                //if (count < 1)
                //{
                //    MessageBox.Show("请选择配送数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                for (int j = 0; j < dgv1.Rows.Count; j++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[j].Cells["check"].Value, "").ToUpper() == "TRUE" && ClsSystem.gnvl(this.dgv1.Rows[j].Cells["cdefine25"].Value, "").ToUpper() == "00000")
                    {
                        if (ClsSystem.gnvl(this.dgv1.Rows[j].Cells["cdefine24"].Value, "").ToUpper() == "00000")
                        {
                            MessageBox.Show("第"+(j+1).ToString()+"行已上传发票,不能作废", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        xmlData = xmlData + "<CZLX>3</CZLX> ";//作废
                        xmlData = xmlData + "<PSDH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["PSDTM"].Value, "") + "</PSDH> ";
                        xmlData = xmlData + "<YQBM>ZDYC0001</YQBM> ";
                        xmlData = xmlData + "<YYBM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cdefine11"].Value, "") + "</YYBM> ";
                        xmlData = xmlData + "<PSDBM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine32"].Value, "") + "</PSDBM> ";
                        xmlData = xmlData + "<CJRQ>" + DateTime.Today.Year.ToString("0000") + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + "</CJRQ> ";
                        xmlData = xmlData + "<SDRQ></SDRQ> ";
                        xmlData = xmlData + "<ZXS>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cdefine34"].Value, "") + "</ZXS> ";
                        xmlData = xmlData + "<JLS>1</JLS> ";
                        xmlData = xmlData + "</MAIN>";
                        xmlData = xmlData + "<DETAIL>";


                        sbvid = ClsSystem.gnvl(this.dgv1.Rows[j].Cells["sbvid"].Value, "");
                        //                        sql = @" select cinvcode,cinvname,cDefine28,iQuantity,cInvDefine3,cDefine29,cDefine30,cBatch ,dMDate ,cExpirationdate ,cSoCode 
                        //                     from  Sales_FHD_w  where dlid=" + DLID;

                        //                        dts = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);

                        //for (int k = 0; k < dts.Rows.Count; k++)
                        //{

                        xmlData = xmlData + "<STRUCT>";

                        xmlData = xmlData + "<PSDTM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["PSDTM"].Value, "") + "</PSDTM> ";
                        if (ClsSystem.gnvl(dgv1.Rows[j].Cells["zxlx"].Value, "") == "整箱")
                        {
                            xmlData = xmlData + "<ZXLX>1</ZXLX> ";
                        }
                        else
                        {
                            xmlData = xmlData + "<ZXLX>2</ZXLX> ";
                        }
                        xmlData = xmlData + "<SPLX>1</SPLX> ";
                        xmlData = xmlData + "<ZXSPBM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine28"].Value, "") + "</ZXSPBM> ";
                        xmlData = xmlData + "<SCPH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cBatch"].Value, "") + "</SCPH> ";
                        xmlData = xmlData + "<SCRQ>" + Convert.ToDateTime(dgv1.Rows[j].Cells["dMDate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[j].Cells["dMDate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[j].Cells["dMDate"].Value).Day.ToString("00") + "</SCRQ> ";
                        xmlData = xmlData + "<YXRQ>" + Convert.ToDateTime(dgv1.Rows[j].Cells["cExpirationdate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[j].Cells["cExpirationdate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[j].Cells["cExpirationdate"].Value).Day.ToString("00") + "</YXRQ> ";
                        xmlData = xmlData + "<XSDDH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["iDLsID"].Value, "") + "</XSDDH> ";
                        xmlData = xmlData + "<WLPTDDH></WLPTDDH> ";
                        xmlData = xmlData + "<DDMXBH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine29"].Value, "") + "</DDMXBH> ";
                        xmlData = xmlData + "<PSL>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[j].Cells["iNum"].Value), 2), "") + "</PSL> ";
                        xmlData = xmlData + "<CGJLDW>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine30"].Value, "") + "</CGJLDW> ";

                        xmlData = xmlData + "</STRUCT>";

                        xmlData = xmlData + "</DETAIL>";
                        xmlData = xmlData + "</XMLDATA>";

                        string iDLsID = "";
                        iDLsID = ClsSystem.gnvl(dgv1.Rows[j].Cells["iDLsID"].Value, "");
                        DataSet ds = new DataSet();
                        DataTable db = new DataTable();
                        string resultXml = SendMessage.SetMessage("YQ003", xmlData);

                        string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");



                        if (result == "00000")
                        {
                            //TextReader tdr = new StringReader(resultXml);
                            //ds.ReadXml(tdr);
                            ////result = "";
                            ////result = SendMessage.ReadXMl(resultXml, "MAIN", "JLS");
                            ////if (Public.GetNum(result) > 0)
                            ////{
                            //if (ds.Tables.Count >= 4)
                            //{
                            //    db = ds.Tables[3];

                            //    for (int k = 0; k < db.Rows.Count; k++)
                            //    {
                            //        // PSDH = ClsSystem.gnvl(db.Rows[j]["PSDH"], "");
                            //        iDLsID = ClsSystem.gnvl(db.Rows[k]["XSDDH"], "");
                            //        CLJG = ClsSystem.gnvl(db.Rows[k]["CLJG"], "");
                                    sql =  " update SaleBillVouchs set cdefine25=null , cDefine23 ='" + ClsSystem.gnvl(dgv1.Rows[j].Cells["zxlx"].Value, "") + "', cDefine34=" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cdefine34"].Value, "") + " where iDLsID =" + iDLsID + "\n";
                                    dgv1.Rows[j].Cells["cdefine25"].Value = DBNull.Value;
                               // }
                                SqlAccess.ExecuteSql(sql, Program.ConnectionString);
                            //}
                            //else
                            //{
                            //    dgv1.Rows[j].Cells["cdefine25"].Value = "错误:未返回明细";
                            //}
                        }
                        else
                        {
                            //TextReader tdr = new StringReader(resultXml);
                            //ds.ReadXml(tdr);

                            //if (ds.Tables.Count >= 4)
                            //{
                            //    db = ds.Tables[3];
                            //}
                            //CLJG = ClsSystem.gnvl(db.Rows[0]["CLJG"], "");
                            string CWXX = SendMessage.ReadXMl(resultXml, "HEAD", "CWXX");
                            //dgv1.Rows[j].Cells["cdefine25"].Value = "错误编码：" + CLJG + "错误信息:" + CLQKMS;
                            MessageBox.Show("错误编码：" + result + "错误信息:" + CWXX, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }



                    }
                }

            }


            this.Cursor = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
               string xmlData = "";
            string sql = "";
            string sbvid = "";;
          //  int y = 0;
            DataTable dts = null;
           
            this.Cursor = Cursors.WaitCursor;
            if (dgv1.Rows.Count > 0)
            {
                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE" && ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cdefine25"].Value, "").ToUpper() != "00000")
                    { 
                       //yybm= ClsSystem.gnvl(dgv1.Rows[i].Cells["cdefine11"].Value, "");
                       //PSDBM = ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine32"].Value, "");
                       //if (yybm_O != yybm&&y>0)
                       //{
                       //    MessageBox.Show("请选择同一客户上传", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       //    return;
                       //}
                       //if (PSDBM_O != PSDBM && y > 0)
                       //{
                       //    MessageBox.Show("请选择同一医院配送点编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       //    return;
                       //}
                       if (ClsSystem.gnvl(ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine34"].Value, ""), "") == "")
                       {
                           MessageBox.Show("配送箱数未填入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           return;
                       }
                       if (ClsSystem.gnvl(dgv1.Rows[i].Cells["zxlx"].Value, "") == "")
                       {
                           MessageBox.Show("装箱类型未填入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           return;
                       }
                       //yybm_O = yybm;
                       //PSDBM_O = PSDBM;
                       //y++;
                      
                    }
                }

                //if (count < 1)
                //{
                //    MessageBox.Show("请选择配送数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                for (int j = 0; j < dgv1.Rows.Count; j++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[j].Cells["check"].Value, "").ToUpper() == "TRUE" && ClsSystem.gnvl(this.dgv1.Rows[j].Cells["cdefine25"].Value, "").ToUpper() != "00000")
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
                        xmlData = xmlData + "<PSDH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["PSDTM"].Value, "") + "</PSDH> ";
                        xmlData = xmlData + "<YQBM>ZDYC0001</YQBM> ";
                        xmlData = xmlData + "<YYBM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cdefine11"].Value, "") + "</YYBM> ";
                        xmlData = xmlData + "<PSDBM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine32"].Value, "") + "</PSDBM> ";
                        xmlData = xmlData + "<CJRQ>" + DateTime.Today.Year.ToString("0000") + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00") + "</CJRQ> ";
                        xmlData = xmlData + "<SDRQ></SDRQ> ";
                        xmlData = xmlData + "<ZXS>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cdefine34"].Value, "") + "</ZXS> ";
                        xmlData = xmlData + "<JLS>1</JLS> ";
                        xmlData = xmlData + "</MAIN>";
                        xmlData = xmlData + "<DETAIL>";


                        sbvid = ClsSystem.gnvl(this.dgv1.Rows[j].Cells["sbvid"].Value, "");
                        //                        sql = @" select cinvcode,cinvname,cDefine28,iQuantity,cInvDefine3,cDefine29,cDefine30,cBatch ,dMDate ,cExpirationdate ,cSoCode 
                        //                     from  Sales_FHD_w  where dlid=" + DLID;

                        //                        dts = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);

                        //for (int k = 0; k < dts.Rows.Count; k++)
                        //{

                        xmlData = xmlData + "<STRUCT>";

                        xmlData = xmlData + "<PSDTM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["PSDTM"].Value, "") + "</PSDTM> ";
                        if (ClsSystem.gnvl(dgv1.Rows[j].Cells["zxlx"].Value, "") == "整箱")
                        {
                            xmlData = xmlData + "<ZXLX>1</ZXLX> ";
                        }
                        else
                        {
                            xmlData = xmlData + "<ZXLX>2</ZXLX> ";
                        }
                        xmlData = xmlData + "<SPLX>1</SPLX> ";
                        xmlData = xmlData + "<ZXSPBM>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine28"].Value, "") + "</ZXSPBM> ";
                        xmlData = xmlData + "<SCPH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cBatch"].Value, "") + "</SCPH> ";
                        xmlData = xmlData + "<SCRQ>" + Convert.ToDateTime(dgv1.Rows[j].Cells["dMDate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[j].Cells["dMDate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[j].Cells["dMDate"].Value).Day.ToString("00") + "</SCRQ> ";
                        xmlData = xmlData + "<YXRQ>" + Convert.ToDateTime(dgv1.Rows[j].Cells["cExpirationdate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[j].Cells["cExpirationdate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[j].Cells["cExpirationdate"].Value).Day.ToString("00") + "</YXRQ> ";
                        xmlData = xmlData + "<XSDDH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["iDLsID"].Value, "") + "</XSDDH> ";
                        xmlData = xmlData + "<WLPTDDH></WLPTDDH> ";
                        xmlData = xmlData + "<DDMXBH>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine29"].Value, "") + "</DDMXBH> ";
                        xmlData = xmlData + "<PSL>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[j].Cells["iNum"].Value), 2), "") + "</PSL> ";
                        xmlData = xmlData + "<CGJLDW>" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cDefine30"].Value, "") + "</CGJLDW> ";

                        xmlData = xmlData + "</STRUCT>";

                        xmlData = xmlData + "</DETAIL>";
                        xmlData = xmlData + "</XMLDATA>";

                        string iDLsID = "";
                        string CLJG = "";
                        DataSet ds = new DataSet();
                        DataTable db= new DataTable();
                        string resultXml = SendMessage.SetMessage("YQ003", xmlData);
                       
                            string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");

                        

                            if (result == "00000")
                            {
                                TextReader tdr = new StringReader(resultXml);
                                ds.ReadXml(tdr);
                                //result = "";
                                //result = SendMessage.ReadXMl(resultXml, "MAIN", "JLS");
                                //if (Public.GetNum(result) > 0)
                                //{
                                if (ds.Tables.Count >= 4)
                                {
                                     db = ds.Tables[3];

                                    for (int k = 0; k < db.Rows.Count; k++)
                                    {
                                        // PSDH = ClsSystem.gnvl(db.Rows[j]["PSDH"], "");
                                        iDLsID = ClsSystem.gnvl(db.Rows[k]["XSDDH"], "");
                                        CLJG = ClsSystem.gnvl(db.Rows[k]["CLJG"], "");
                                        sql = " update SaleBillVouchs set cdefine25='" + CLJG + "',cDefine23 ='" + ClsSystem.gnvl(dgv1.Rows[j].Cells["zxlx"].Value, "") +"', cDefine34=" + ClsSystem.gnvl(dgv1.Rows[j].Cells["cdefine34"].Value, "") + " where iDLsID =" + iDLsID + "\n";
                                        dgv1.Rows[j].Cells["cdefine25"].Value = CLJG;
                                    }
                                    SqlAccess.ExecuteSql(sql, Program.ConnectionString);
                                }
                                else
                                {
                                    dgv1.Rows[j].Cells["cdefine25"].Value = "错误:未返回明细";
                                }
                            }
                            else
                            {
                               TextReader tdr = new StringReader(resultXml);
                                ds.ReadXml(tdr);
                               
                                if (ds.Tables.Count >= 4)
                                {
                                    db = ds.Tables[3];
                                }
                                 CLJG = ClsSystem.gnvl(db.Rows[0]["CLJG"], "");
                                string CLQKMS = ClsSystem.gnvl(db.Rows[0]["CLQKMS"], "");
                                dgv1.Rows[j].Cells["cdefine25"].Value = "错误编码：" + CLJG + "错误信息:" + CLQKMS;
                                MessageBox.Show("错误编码：" + CLJG + "错误信息:" + CLQKMS, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button5_Click(object sender, EventArgs e)
        {
            DataRow newrow = null;
            DataTable db = new DataTable();
            db.Columns.Add("DataColumn1");
            db.Columns.Add("DataColumn2");
            db.Columns.Add("DataColumn3");
            db.Columns.Add("DataColumn4");
            db.Columns.Add("DataColumn5");
            db.Columns.Add("DataColumn6");
            db.Columns.Add("DataColumn7");
            db.Columns.Add("DataColumn8");
            db.Columns.Add("DataColumn9");
            db.Columns.Add("DataColumn10");
          
                 
            for (int i = 0; i < dgv1.Rows.Count; i++)
            {
                if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE")
                {
                    newrow = db.Rows.Add();
                    newrow["DataColumn1"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cSBVCode"].Value, "");
                  
                        newrow["DataColumn2"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cinvname"].Value, "");
                 
                    newrow["DataColumn3"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cInvStd"].Value, "");
                    if (ClsSystem.gnvl(dgv1.Rows[i].Cells["cAddress"].Value, "").Length>=13)
                    {
                          if (Public.GetPass(Program.cacc_id, "2015-07-15"))
                    {
                        newrow["DataColumn4"] ="   "+ ClsSystem.gnvl(dgv1.Rows[i].Cells["cAddress"].Value, "").Substring(0, 13)+"                                    ";                 
                    }
                    else

                       {
                       newrow["DataColumn4"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cAddress"].Value, "").Substring(0,13) ;
                          }
                
                    } else 
                    {
                          newrow["DataColumn4"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cAddress"].Value, "") ;
                    }
                    newrow["DataColumn5"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["PSDTM"].Value, "");
                    newrow["DataColumn6"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cExpirationdate"].Value, "");
                    newrow["DataColumn7"] = ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iNum"].Value), 2), "");
                    newrow["DataColumn8"] = "震达医药";
                    newrow["DataColumn9"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cBatch"].Value, "");                 

                  
                        newrow["DataColumn10"] = ClsSystem.gnvl(dgv1.Rows[i].Cells["cCusName"].Value, "");
                   
                


                }
            }
            if (db.Rows.Count > 0)
            {
                XtraReport1 report = new XtraReport1();
                report.DataSource = db;
                report.PrinterName = "条码打印";
                report.ShowPreview();
            }
            else
            {
                MessageBox.Show("未选择数据", "错误", MessageBoxButtons.OK);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //string str="";
            //int maxvalue=0;
            //str="ZD1"+ DateTime.Today.Year.ToString("00") + DateTime.Today.Month.ToString("00") + DateTime.Today.Day.ToString("00");

            //SqlAccess.ExecuteScalar(" select 
            //for (int i = 0; i < dgv1.Rows.Count; i++)
            //{
            //    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE")
            //    {
                    
            //       dgv1.Rows[count].Cells["cDefine35"].Value =
            //    }
            //}
       
        }
    }
}
