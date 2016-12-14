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
using Key;
using System.Xml;

namespace SHYSInterface
{
    public partial class FrmYQ004 : Form
    {

        public static DataTable cxdb = null;
        public static Boolean ispd = false;
        string cSBVCodeNew = "";

        public FrmYQ004()
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

        private void QuerySaleBillVouch(DataTable db)
        {
            Dictionary<string, string> csocodes = new Dictionary<string, string>();
            List<DataTable> tables = new List<DataTable>();
            //销售订单的备注为原始销售订单号
            for (int i = 0; i < db.Rows.Count; ++i)
            {
                //销售发票号
                string sbvcode = ClsSystem.gnvl(db.Rows[i]["cSBVCode"], "");
                
                string csocode = "";
                if (checkBoxQuery.Checked)
                {
                    //原始销售订单号
                    csocode = DBHelper.FindOther("csocode", 
                    "SaleBillVouchZT", "cSBVCode = '" + sbvcode + "'", Program.ConnectionString);
                }
                else
                {
                    //发货退货单号
                    csocode = DBHelper.FindOther("cDLCode", 
                 "SaleBillVouchZT", "cSBVCode = '" + sbvcode + "'", Program.ConnectionString);
                }
                
                if (csocodes.ContainsKey(sbvcode))
                {
                    continue;
                }
                else
                {
                    csocodes.Add(sbvcode, csocode);
                }
                string sql = @" select a.cdefine9,a.cdefine10 ,a.cSBVCode, a.dDate ,a.cSTCode ,a.bReturnFlag,a.ccusname ,
                        a.cDefine12,a.cDefine13,a.cDefine14,a.cDefine15,a.cDefine8, a.cMaker, a.cMemo ,a.SBVID,a.cdefine11,
                          b.cinvcode,b.cinvname,b.cDefine28,b.iQuantity,b.cInvDefine5,b.cDefine29,b.cDefine30,b.cBatch ,
                           b.cSoCode ,inv.cFile ,b.dMDate ,b.cExpirationdate ,b.iUnitPrice ,b.iTaxUnitPrice ,b.iTaxRate ,
                           b.iTax ,b.iMoney ,b.iSum ,       b.iQuotedPrice,   inv.fRetailPrice,b.idlsid ,
                         b.cdefine24,b.cdefine25,b.iDLsID,b.cDefine32,b.cDefine33,a.sbvid,inv.cFile,b.iNum,b.iInvExchRate 
                     from SaleBillVouchZT  a   join SaleBillVouchZW b on a.sbvid=b.sbvid 
                          left join inventory inv  on inv.cinvcode=b.cinvcode
                       where a.cmemo like '" + csocode + "'";
                sql = sql + " and a.bReturnFlag= 0 ";
                if (checkBox2.Checked == true) //是否传递
                {
                    sql = sql + " and isnull(b.cDefine24,'')<>''";
                }
                else
                {
                    sql = sql + " and isnull(b.cDefine24,'')=''";
                }
                
                sql = sql + "   order by a.dDate    ";

                DataSet dt = new DataSet();
                SqlDataAdapter thisAdapter = new SqlDataAdapter(sql, Program.ConnectionString);
                thisAdapter.Fill(dt);
                if (dt.Tables.Count > 0)
                {
                    DataTable table = dt.Tables[0];
                    if(table.Rows.Count > 0)
                    {

                        tables.Add(table.Copy());
                    }
                }
            }
            foreach (var table in tables)
            {
                for (int i = 0; i < table.Rows.Count; ++i)
                {
                    db.Rows.Add(table.Rows[i].ItemArray);
                }
            }

        }
        private void WriteU8BVCode(int i)
        {
            try 
            { 
                if (dgv1.Rows[i].Cells["cSBVCode"].Value.ToString() !=
                            dgv1.Rows[i].Cells["cSBVCodeU8"].Value.ToString())
                {
                    string strPath = System.Environment.CurrentDirectory;
                    string strxml = strPath + @"\BvCode.xml";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(strxml);
                    XmlElement rootElem = doc.DocumentElement;   //获取根节点
                    string xPath = "code[@key='" + dgv1.Rows[i].Cells["cSBVCode"].Value.ToString() + "']";
                    XmlNode node = rootElem.SelectSingleNode(xPath);
                    if (node == null)
                    {
                        XmlElement element = doc.CreateElement("code");
                        {
                            XmlAttribute attribute = doc.CreateAttribute("key");
                            attribute.Value = dgv1.Rows[i].Cells["cSBVCodeU8"].Value.ToString();
                            element.Attributes.Append(attribute);
                        }
                        {
                            XmlAttribute attribute = doc.CreateAttribute("value");
                            attribute.Value = dgv1.Rows[i].Cells["cSBVCode"].Value.ToString();
                            element.Attributes.Append(attribute);
                        }
                        rootElem.AppendChild(element);
                    }
                    doc.Save(strxml);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private string QueryU8BVCode(string bvCode)
        {
            string strPath = System.Environment.CurrentDirectory;
            string strxml = strPath + @"\BvCode.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(strxml);
            XmlElement rootElem = doc.DocumentElement;   //获取根节点
            string xPath = "code[@key='" + bvCode + "']";
            XmlNode node = rootElem.SelectSingleNode(xPath);
            if (node != null)
            {
               foreach (XmlAttribute attr in node.Attributes)
               {
                   if (attr.Name == "value")
                   {
                       return attr.Value;
                    }
               }
            }
            return bvCode;

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

              

                sql = @" select a.cdefine9,a.cdefine10 ,a.cSBVCode, a.dDate ,a.cSTCode ,a.bReturnFlag,a.ccusname ,
                        a.cDefine12,a.cDefine13,a.cDefine14,a.cDefine15,a.cDefine8, a.cMaker, a.cMemo ,a.SBVID,a.cdefine11,
                          b.cinvcode,b.cinvname,b.cDefine28,b.iQuantity,b.cInvDefine5,b.cDefine29,b.cDefine30,b.cBatch ,
                           b.cSoCode ,inv.cFile ,b.dMDate ,b.cExpirationdate ,b.iUnitPrice ,b.iTaxUnitPrice ,b.iTaxRate ,
                           b.iTax ,b.iMoney ,b.iSum ,       b.iQuotedPrice,   inv.fRetailPrice,b.idlsid ,
                         b.cdefine24,b.cdefine25,b.iDLsID,b.cDefine32,b.cDefine33,a.sbvid,inv.cFile,b.iNum,b.iInvExchRate 
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

                if (checkbox1.Checked == true) //是否退货
                {
                    sql = sql + " and a.bReturnFlag= 1 ";
                }
                else
                {
                    sql = sql + " and a.bReturnFlag= 0 ";
                }
                if (checkBox2.Checked == true) //是否传递
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
                //查找新增的销售订单
                try
                {
                    QuerySaleBillVouch(db);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "查询新增订单失败！");
                }
               
                
                if (db.Rows.Count > 0)
                {
                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        dgv1.Rows.Add();

                        dgv1.Rows[i].Cells["cdefine24"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine24"], "");
                        dgv1.Rows[i].Cells["cdefine25"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine25"], "");
                        dgv1.Rows[i].Cells["PSDTM"].Value = "ZD1" + ClsSystem.gnvl(db.Rows[i]["iDLsID"], "");
                        dgv1.Rows[i].Cells["cCusName"].Value = ClsSystem.gnvl(db.Rows[i]["cCusName"], "");
                        dgv1.Rows[i].Cells["cdefine14"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine14"], "");
                        dgv1.Rows[i].Cells["cdefine32"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine32"], "");

                        dgv1.Rows[i].Cells["cdefine33"].Value = ClsSystem.gnvl(db.Rows[i]["cdefine33"], "");
                        dgv1.Rows[i].Cells["dDate"].Value = ClsSystem.gnvl(db.Rows[i]["dDate"], "");
                        //u8发票号
                        dgv1.Rows[i].Cells["cSBVCodeU8"].Value = ClsSystem.gnvl(db.Rows[i]["cSBVCode"], "");
                        dgv1.Rows[i].Cells["cSBVCode"].Value = QueryU8BVCode(ClsSystem.gnvl(db.Rows[i]["cSBVCode"], ""));

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


                        dgv1.Rows[i].Cells["iUnitPrice"].Value = ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(db.Rows[i]["iUnitPrice"]) * Public.GetNum(db.Rows[i]["iInvExchRate"]),4), "");
                        dgv1.Rows[i].Cells["iTaxUnitPrice"].Value = ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(db.Rows[i]["iTaxUnitPrice"]) * Public.GetNum(db.Rows[i]["iInvExchRate"]),4), "");
                        dgv1.Rows[i].Cells["iMoney"].Value = ClsSystem.gnvl(db.Rows[i]["iMoney"], "");
                        dgv1.Rows[i].Cells["iTax"].Value = ClsSystem.gnvl(db.Rows[i]["iTax"], "");
                        dgv1.Rows[i].Cells["iSum"].Value = ClsSystem.gnvl(db.Rows[i]["iSum"], "");
                        dgv1.Rows[i].Cells["iTaxRate"].Value = ClsSystem.gnvl(db.Rows[i]["iTaxRate"], "");
                        dgv1.Rows[i].Cells["iQuotedPrice"].Value = ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(db.Rows[i]["iQuotedPrice"]) * Public.GetNum(db.Rows[i]["iInvExchRate"]),4), "");
                        dgv1.Rows[i].Cells["fRetailPrice"].Value = ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(db.Rows[i]["fRetailPrice"]) * Public.GetNum(db.Rows[i]["iInvExchRate"]),4), "");

                        dgv1.Rows[i].Cells["cMaker"].Value = ClsSystem.gnvl(db.Rows[i]["cMaker"], "");

                        dgv1.Rows[i].Cells["cMemo"].Value = ClsSystem.gnvl(db.Rows[i]["cMemo"], "");
                        dgv1.Rows[i].Cells["cDefine11"].Value = ClsSystem.gnvl(db.Rows[i]["cDefine11"], "");
                        dgv1.Rows[i].Cells["sbvid"].Value = ClsSystem.gnvl(db.Rows[i]["sbvid"], "");
                        dgv1.Rows[i].Cells["cFile"].Value = ClsSystem.gnvl(db.Rows[i]["cFile"], "");

                        
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
            ispd = true;
        }

        /// <summary>
        /// 发票作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
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
            
            List<string> sbvids = new List<string>();

            if (dgv1.Rows.Count > 0)
            {
                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE" && ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cdefine24"].Value, "").ToUpper() == "00000")
                    {
                        yybm = ClsSystem.gnvl(dgv1.Rows[i].Cells["cdefine11"].Value, "");
                        SBVID = ClsSystem.gnvl(this.dgv1.Rows[i].Cells["SBVID"].Value, "");
                        //PSDBM = ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cDefine32"].Value, "");
                        if (yybm_O != yybm && y > 0)
                        {
                            MessageBox.Show("请选择同一客户上传", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        bool exist = false;
                        foreach (var value in sbvids)
                        {
                            if (value == SBVID)
                            {
                                exist = true;
                                break;
                            }
                        }
                        if(!exist)
                        {
                            sbvids.Add(SBVID);
                        }
                        //if (SBVID != SBVID_o && y > 0)
                        //{

                        //    MessageBox.Show("请选择同一发票", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                        if (PSDBM_O != PSDBM && y > 0)
                        {
                            MessageBox.Show("请选择同一医院配送点编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        hsje = hsje + Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iSum"].Value), 4);

                        yybm_O = yybm;
                        SBVID_o = SBVID;
                        PSDBM_O = PSDBM;
                        y++;
                        count = i;
                    }
                }
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
                    xmlData = xmlData + "<CZLX>3</CZLX> ";// 作废

                    xmlData = xmlData + "<YQBM>ZDYC0001</YQBM> ";
                    xmlData = xmlData + "<YYBM>" + ClsSystem.gnvl(dgv1.Rows[count].Cells["cdefine11"].Value, "") + "</YYBM> ";
                    xmlData = xmlData + "<PSDBM>" + ClsSystem.gnvl(dgv1.Rows[count].Cells["cDefine32"].Value, "") + "</PSDBM> ";

                    xmlData = xmlData + "<JLS>" + y.ToString() + "</JLS> ";
                    xmlData = xmlData + "</MAIN>";
                    xmlData = xmlData + "<DETAIL>";

                    for (int i = 0; i < dgv1.Rows.Count; i++)
                    {
                        if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE" && ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cDefine24"].Value, "").ToUpper() == "00000")
                        {
                            SBVID = ClsSystem.gnvl(this.dgv1.Rows[i].Cells["SBVID"].Value, "");

                            //   dts = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);                    

                            xmlData = xmlData + "<STRUCT>";

                            xmlData = xmlData + "<FPH>" + ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cSBVCode"].Value, "") + "</FPH> ";


                            xmlData = xmlData + "<FPRQ>" + Convert.ToDateTime(dgv1.Rows[i].Cells["dDate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[i].Cells["dDate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[i].Cells["dDate"].Value).Day.ToString("00") + "</FPRQ> ";
                            xmlData = xmlData + "<FPHSZJE>" + ClsSystem.gnvl(hsje, "0") + "</FPHSZJE>";
                            xmlData = xmlData + "<DLCGBZ>0</DLCGBZ> ";
                            xmlData = xmlData + "<FPBZ>" + ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cMemo"].Value, "") + "</FPBZ> ";
                            xmlData = xmlData + "<GLMXBH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine29"].Value, "") + "</GLMXBH> ";
                            xmlData = xmlData + "<XSDDH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["iDLsID"].Value, "") + "</XSDDH> ";
                            xmlData = xmlData + "<SPLX>1</SPLX> ";

                            xmlData = xmlData + "<SFCH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["bReturnFlag"].Value, "") + "</SFCH> ";

                            if (ClsSystem.gnvl(dgv1.Rows[i].Cells["bReturnFlag"].Value, "") == "1")
                            {
                                xmlData = xmlData + "<GLBZ>0</GLBZ> ";
                            }
                            else
                            {
                                xmlData = xmlData + "<GLBZ>1</GLBZ> ";
                            }
                            xmlData = xmlData + "<WFGLSM></WFGLSM> ";
                            xmlData = xmlData + "<ZXSPBM>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine28"].Value, "") + "</ZXSPBM> ";
                            xmlData = xmlData + "<SCPH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cBatch"].Value, "") + "</SCPH> ";
                            xmlData = xmlData + "<PZWH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cFile"].Value, "") + "</PZWH> ";
                            xmlData = xmlData + "<SPSL>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["iNum"].Value, "") + "</SPSL> ";
                            xmlData = xmlData + "<CGJLDW>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine30"].Value, "") + "</CGJLDW> ";
                            xmlData = xmlData + "<SCRQ>" + Convert.ToDateTime(dgv1.Rows[i].Cells["dMDate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[i].Cells["dMDate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[i].Cells["dMDate"].Value).Day.ToString("00") + "</SCRQ> ";
                            xmlData = xmlData + "<YXRQ>" + Convert.ToDateTime(dgv1.Rows[i].Cells["cExpirationdate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[i].Cells["cExpirationdate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[i].Cells["cExpirationdate"].Value).Day.ToString("00") + "</YXRQ> ";
                            xmlData = xmlData + "<WSDJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iUnitPrice"].Value), 4), "") + "</WSDJ> ";
                            xmlData = xmlData + "<HSDJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iTaxUnitPrice"].Value), 4), "") + "</HSDJ> ";
                            xmlData = xmlData + "<SL>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iTaxRate"].Value), 4), "") + "</SL> ";
                            xmlData = xmlData + "<SE>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iTax"].Value), 4), "") + "</SE> ";
                            xmlData = xmlData + "<WSJE>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iMoney"].Value), 4), "") + "</WSJE> ";
                            xmlData = xmlData + "<HSJE>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iSum"].Value), 4), "") + "</HSJE> ";

                            xmlData = xmlData + "<PFJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iQuotedPrice"].Value), 4), "") + "</PFJ> ";
                            xmlData = xmlData + "<LSJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["fRetailPrice"].Value), 4), "") + "</LSJ> ";

                            xmlData = xmlData + "</STRUCT>";
                        }
                    }
                    xmlData = xmlData + "</DETAIL>";
                    xmlData = xmlData + "</XMLDATA>";
                }
                DataSet ds = new DataSet();
                DataTable db = null;
                string iDLsID = "";
                string CLJG = "";
                string resultXml = SendMessage.SetMessage("YQ004", xmlData);
                string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");
                if (result == "00000")
                {
                    foreach (var value in sbvids)
                    {
                        sql += " update SaleBillVouchs set cdefine24=null  where sbvid=" + value + "\n";
                    }
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
                    TextReader tdr = new StringReader(resultXml);
                    ds.ReadXml(tdr);
                    string CLQKMS = "";
                    if (ds.Tables.Count == 3)
                    {
                        db = ds.Tables[2];
                        CLJG = ClsSystem.gnvl(db.Rows[0]["CLJG"], "");
                        CLQKMS = ClsSystem.gnvl(db.Rows[0]["CLQKMS"], "");
                    }

                    string CWXX = SendMessage.ReadXMl(resultXml, "HEAD", "CWXX");
                    MessageBox.Show("错误编码：" + result + "错误信息:" + CWXX + "\n\r" + CLQKMS, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //MessageBox.Show("错误编码：" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") + "错误信息:" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["CWXX"], ""), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                try
                {

                    SqlAccess.ExecuteSql(sql, Program.ConnectionString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                this.Cursor = Cursors.Default;
            }
            
        }

        /// <summary>
        /// 上传发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            decimal iInvExchRate = 0;
            if (dgv1.Rows.Count > 0)
            {
                for (int i = 0; i < dgv1.Rows.Count; i++)
                {
                    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cdefine24"].Value, "").ToUpper() == "00000")
                    {
                        continue;
                    }
                    if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE" && 
                        ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cdefine24"].Value, "").ToUpper() != "00000")
                    {
                        yybm = ClsSystem.gnvl(dgv1.Rows[i].Cells["cdefine11"].Value, "");
                        SBVID = ClsSystem.gnvl(this.dgv1.Rows[i].Cells["SBVID"].Value, "");
                        //PSDBM = ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cDefine32"].Value, "");
                        if (yybm_O != yybm && y > 0)
                        {
                            MessageBox.Show("请选择同一客户上传", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //if (SBVID != SBVID_o && y > 0)
                        //{

                        //    MessageBox.Show("请选择同一发票", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                        if (PSDBM_O != PSDBM && y > 0)
                        {
                            MessageBox.Show("请选择同一医院配送点编码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        hsje = hsje + Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iSum"].Value), 4);

                        yybm_O = yybm;
                        SBVID_o = SBVID;
                        PSDBM_O = PSDBM;
                        y++;
                        count = i;
                    }
                }
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
                    xmlData = xmlData + "<CZLX>1</CZLX> ";

                    xmlData = xmlData + "<YQBM>ZDYC0001</YQBM> ";
                    xmlData = xmlData + "<YYBM>" + ClsSystem.gnvl(dgv1.Rows[count].Cells["cdefine11"].Value, "") + "</YYBM> ";
                    xmlData = xmlData + "<PSDBM>" + ClsSystem.gnvl(dgv1.Rows[count].Cells["cDefine32"].Value, "") + "</PSDBM> ";

                    xmlData = xmlData + "<JLS>" + y.ToString() + "</JLS> ";
                    xmlData = xmlData + "</MAIN>";
                    xmlData = xmlData + "<DETAIL>";

                    for (int i = 0; i < dgv1.Rows.Count; i++)
                    {
                        WriteU8BVCode(i);
                        if (ClsSystem.gnvl(this.dgv1.Rows[i].Cells["check"].Value, "").ToUpper() == "TRUE" && ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cDefine24"].Value, "").ToUpper() != "00000")
                        {
                            SBVID = ClsSystem.gnvl(this.dgv1.Rows[i].Cells["SBVID"].Value, "");
                            iInvExchRate = Public.GetNum(dgv1.Rows[i].Cells["iInvExchRate"].Value);

                            //   dts = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);                    
                           
                                xmlData = xmlData + "<STRUCT>";

                                xmlData = xmlData + "<FPH>" + ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cSBVCode"].Value, "") + "</FPH> ";


                                xmlData = xmlData + "<FPRQ>" + Convert.ToDateTime(dgv1.Rows[i].Cells["dDate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[i].Cells["dDate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[i].Cells["dDate"].Value).Day.ToString("00") + "</FPRQ> ";
                                xmlData = xmlData + "<FPHSZJE>" + ClsSystem.gnvl(hsje, "0") + "</FPHSZJE>";
                                xmlData = xmlData + "<DLCGBZ>0</DLCGBZ> ";
                                xmlData = xmlData + "<FPBZ>" + ClsSystem.gnvl(this.dgv1.Rows[i].Cells["cMemo"].Value, "") + "</FPBZ> ";
                                xmlData = xmlData + "<GLMXBH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine29"].Value, "") + "</GLMXBH> ";
                                xmlData = xmlData + "<XSDDH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["iDLsID"].Value, "") + "</XSDDH> ";
                                xmlData = xmlData + "<SPLX>1</SPLX> ";

                                xmlData = xmlData + "<SFCH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["bReturnFlag"].Value, "") + "</SFCH> ";

                                if (ClsSystem.gnvl(dgv1.Rows[i].Cells["bReturnFlag"].Value, "") == "1")
                                {
                                    xmlData = xmlData + "<GLBZ>0</GLBZ> ";
                                }
                                else
                                {
                                    xmlData = xmlData + "<GLBZ>1</GLBZ> ";
                                }
                                xmlData = xmlData + "<WFGLSM></WFGLSM> ";
                                xmlData = xmlData + "<ZXSPBM>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine28"].Value, "") + "</ZXSPBM> ";
                                xmlData = xmlData + "<SCPH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cBatch"].Value, "") + "</SCPH> ";
                                xmlData = xmlData + "<PZWH>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cFile"].Value, "") + "</PZWH> ";
                                xmlData = xmlData + "<SPSL>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["iNum"].Value, "") + "</SPSL> ";
                                xmlData = xmlData + "<CGJLDW>" + ClsSystem.gnvl(dgv1.Rows[i].Cells["cDefine30"].Value, "") + "</CGJLDW> ";
                                xmlData = xmlData + "<SCRQ>" + Convert.ToDateTime(dgv1.Rows[i].Cells["dMDate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[i].Cells["dMDate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[i].Cells["dMDate"].Value).Day.ToString("00") + "</SCRQ> ";
                                xmlData = xmlData + "<YXRQ>" + Convert.ToDateTime(dgv1.Rows[i].Cells["cExpirationdate"].Value).Year.ToString("0000") + Convert.ToDateTime(dgv1.Rows[i].Cells["cExpirationdate"].Value).Month.ToString("00") + Convert.ToDateTime(dgv1.Rows[i].Cells["cExpirationdate"].Value).Day.ToString("00") + "</YXRQ> ";
                                xmlData = xmlData + "<WSDJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iUnitPrice"].Value) , 4), "") + "</WSDJ> ";
                                xmlData = xmlData + "<HSDJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iTaxUnitPrice"].Value) , 4), "") + "</HSDJ> ";
                                xmlData = xmlData + "<SL>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iTaxRate"].Value), 4), "") + "</SL> ";
                                xmlData = xmlData + "<SE>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iTax"].Value), 4), "") + "</SE> ";
                                xmlData = xmlData + "<WSJE>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iMoney"].Value), 4), "") + "</WSJE> ";
                                xmlData = xmlData + "<HSJE>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iSum"].Value), 4), "") + "</HSJE> ";

                                xmlData = xmlData + "<PFJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["iQuotedPrice"].Value), 4), "") + "</PFJ> ";
                                xmlData = xmlData + "<LSJ>" + ClsSystem.gnvl(Public.ChinaRound(Public.GetNum(dgv1.Rows[i].Cells["fRetailPrice"].Value), 4), "") + "</LSJ> ";

                                xmlData = xmlData + "</STRUCT>";


                            
                          
                        }
                    }
                    xmlData = xmlData + "</DETAIL>";
                    xmlData = xmlData + "</XMLDATA>";
                }
                DataSet ds = new DataSet();
                DataTable db = null;
                string iDLsID = "";
                string CLJG = "";
                string resultXml = SendMessage.SetMessage("YQ004", xmlData);
                string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");
                if (result == "00000")
                {
                    try
                    {
                        TextReader tdr = new StringReader(resultXml);
                        ds.ReadXml(tdr);
                        if (ds.Tables.Count == 3)
                        {
                            db = ds.Tables[2];
                            for (int j = 0; j < db.Rows.Count; j++)
                            {
                                iDLsID = ClsSystem.gnvl(db.Rows[j]["XSDDH"], "");
                                CLJG = ClsSystem.gnvl(db.Rows[j]["CLJG"], "");

                                sql = sql + " update SaleBillVouchs set cdefine24='" + CLJG + "' where iDLsID=" + iDLsID + "\n";

                            }
                        }  
                    }
                    catch (Exception ex)
                    {


                    }
                    
                }
                else
                {
                    TextReader tdr = new StringReader(resultXml);
                    ds.ReadXml(tdr);
                    string CLQKMS = "";
                    if (ds.Tables.Count == 3)
                    {
                        db = ds.Tables[2];
                        CLJG = ClsSystem.gnvl(db.Rows[0]["CLJG"], "");
                        CLQKMS = ClsSystem.gnvl(db.Rows[0]["CLQKMS"], "");
                    }
                    //dgv1.Rows[i].Cells["cdefine24"].Value = "错误编码：" + CLJG + "错误信息:" + CLQKMS;

                    string CWXX = SendMessage.ReadXMl(resultXml, "HEAD", "CWXX");
                    MessageBox.Show("错误编码：" + result + "错误信息:" + CWXX + "\n\r" + CLQKMS, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    //MessageBox.Show("错误编码：" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["ZTCLJG"], "") + "错误信息:" + ClsSystem.gnvl(ds.Tables[0].Rows[0]["CWXX"], ""), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                try
                {

                    SqlAccess.ExecuteSql(sql, Program.ConnectionString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

         //   int row = e.RowIndex;
            if (checkBoxSingle.Checked == true)
            {
                return;
            }
            dgv1.EndEdit();


            Boolean isTrue = false;
            int row = dgv1.CurrentRow.Index;
            cSBVCodeNew = ClsSystem.gnvl(this.dgv1.Rows[row].Cells["cSBVCode"].Value, "");


            if (ClsSystem.gnvl(this.dgv1.Rows[row].Cells["check"].Value, "").ToUpper() == "TRUE")
            {
                isTrue = true;
            }
            else
            {
                isTrue = false;
            }

            for (int i = 0; i <= dgv1.Rows.Count - 1; i++)
            {

                if (ClsSystem.gnvl(dgv1.Rows[i].Cells["cSBVCode"].Value, "") == cSBVCodeNew)
                {
                    dgv1.Rows[i].Cells["check"].Value = isTrue;
                }

            }


            dgv1.EndEdit();
            dgv1.RefreshEdit();
          
        }

        private void dgv1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
          
        }

        private void dgv1_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

      

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
              
       
           
        }

        private void dgv1_CurrentCellChanged(object sender, EventArgs e)
        {
           
        }




    }
}
