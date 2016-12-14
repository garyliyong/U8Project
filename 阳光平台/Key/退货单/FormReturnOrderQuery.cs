using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace SHYSInterface.退货单
{
    public partial class FormReturnOrderQuery : Form
    {

        public FormReturnOrderQuery()
        {
            InitializeComponent();
        }

        //查询退货单信息
        private void buttonReturnOrderQuery_Click(object sender, EventArgs e)
        {
            try
            {
                FormReturnOrderFilter formReturnOrderFilter = new FormReturnOrderFilter();
                if (formReturnOrderFilter.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                string xmlData = formReturnOrderFilter.FilterCondition;
                if (ClsSystem.gnvl(xmlData, "") != "")
                {
                    string resultXml = SendMessage.SetMessage("YQ011", xmlData);
                    string result = SendMessage.ReadXMl(resultXml, "HEAD", "ZTCLJG");
                    if (result != "00000")
                    {
                        //获取失败信息
                    }
                    else
                    {
                        dataGridViewReturnOrder.Rows.Clear();

                        TextReader textReader = new StringReader(resultXml);
                        DataSet ds = new DataSet();
                        ds.ReadXml(textReader);
                        result = SendMessage.ReadXMl(resultXml, "MAIN", "JLS");
                        if (int.Parse(result) > 0)
                        {
                            if (ds.Tables.Count > 3)
                            {
                                DataTable db = ds.Tables[3];
                                for (int i = 0; i < db.Rows.Count; ++i)
                                {
                                    dataGridViewReturnOrder.Rows.Add();
                                    dataGridViewReturnOrder.Rows[i].Cells["SELECTED"].Value = true;
                                    dataGridViewReturnOrder.Rows[i].Cells["SFWJ"].Value = ClsSystem.gnvl(ds.Tables[1].Rows[0]["SFWJ"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["THDBH"].Value = ClsSystem.gnvl(db.Rows[i]["THDBH"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["YQBM"].Value = ClsSystem.gnvl(db.Rows[i]["YQBM"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["YYBM"].Value = ClsSystem.gnvl(db.Rows[i]["YYBM"], "");
                                    //TODO:附加医院名称
                                    dataGridViewReturnOrder.Rows[i].Cells["YYMC"].Value = OrderInfo.GetYYMC(ClsSystem.gnvl(db.Rows[i]["YYBM"], ""));


                                    dataGridViewReturnOrder.Rows[i].Cells["PSDBM"].Value = ClsSystem.gnvl(db.Rows[i]["PSDBM"], ""); 
                                    dataGridViewReturnOrder.Rows[i].Cells["PSDZ"].Value = ClsSystem.gnvl(db.Rows[i]["PSDZ"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["THDTJRQ"].Value = ClsSystem.gnvl(db.Rows[i]["THDTJRQ"], "");

                                    //TODO:显示提交方式名称
                                    dataGridViewReturnOrder.Rows[i].Cells["THDTJFS"].Value = OrderInfo.GetTJFSName(ClsSystem.gnvl(db.Rows[i]["THDTJFS"], ""));

                                    //TODO:显示处理状态名称
                                    dataGridViewReturnOrder.Rows[i].Cells["THDCLZT"].Value = OrderInfo.GetTHDCLZTName(ClsSystem.gnvl(db.Rows[i]["THDCLZT"], ""));
                                    dataGridViewReturnOrder.Rows[i].Cells["DLCGBZ"].Value = ClsSystem.gnvl(db.Rows[i]["DLCGBZ"], "");
                                    //TODO:显示商品类型名称
                                    dataGridViewReturnOrder.Rows[i].Cells["SPLX"].Value = OrderInfo.GetSPLXName(ClsSystem.gnvl(db.Rows[i]["SPLX"], ""));

                                    //TODO:显示药品类型名称
                                    dataGridViewReturnOrder.Rows[i].Cells["YPLX"].Value = OrderInfo.GetYPLXName(ClsSystem.gnvl(db.Rows[i]["YPLX"], ""));
                                    dataGridViewReturnOrder.Rows[i].Cells["ZXSPBM"].Value = ClsSystem.gnvl(db.Rows[i]["ZXSPBM"], "");

                                    dataGridViewReturnOrder.Rows[i].Cells["CPM"].Value = ClsSystem.gnvl(db.Rows[i]["CPM"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["YPJX"].Value = ClsSystem.gnvl(db.Rows[i]["YPJX"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["GG"].Value = ClsSystem.gnvl(db.Rows[i]["GG"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["CGJLDW"].Value = ClsSystem.gnvl(db.Rows[i]["CGJLDW"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["YYDWMC"].Value = ClsSystem.gnvl(db.Rows[i]["YYDWMC"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["BZNHSL"].Value = ClsSystem.gnvl(db.Rows[i]["BZNHSL"], "");


                                    dataGridViewReturnOrder.Rows[i].Cells["SCQYMC"].Value = ClsSystem.gnvl(db.Rows[i]["SCQYMC"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["SCPH"].Value = ClsSystem.gnvl(db.Rows[i]["SCPH"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["THSL"].Value = ClsSystem.gnvl(db.Rows[i]["THSL"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["THDJ"].Value = ClsSystem.gnvl(db.Rows[i]["THDJ"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["THZJ"].Value = ClsSystem.gnvl(db.Rows[i]["THZJ"], "");
                                    dataGridViewReturnOrder.Rows[i].Cells["THYY"].Value = ClsSystem.gnvl(db.Rows[i]["THYY"], "");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("退货单查询失败!    " + ex.Message);
            }
           
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewReturnOrder.Rows.Count; ++i)
            {
                if (dataGridViewReturnOrder.Rows[i].IsNewRow)
                    continue;
                dataGridViewReturnOrder.Rows[i].Cells["SELECTED"].Value = !bool.Parse(dataGridViewReturnOrder.Rows[i].Cells["SELECTED"].Value.ToString());
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = OrderInfo.GetDgvToTable(dataGridViewReturnOrder) ;
                DataRow[] rows = table.Select("SELECTED = true");
                if (rows.Count() == 0)
                {
                    throw new Exception("请选择退货单条目");
                }
                HashSet<string> uids = new HashSet<string>();
                
                for (int i = 0; i < rows.Count(); ++i)
                {
                    //（医院编码-统编代码-退货单提交日期）作为退货单记录唯一标识符
                    uids.Add(rows[i]["YYBM"].ToString() + "-" + rows[i]["ZXSPBM"].ToString() + "-" + rows[i]["THDTJRQ"].ToString());
                }
                DataTable newTable = OrderInfo.ToDataTable(rows);
                foreach (string uid in uids)
                {
                    string []splits = uid.Split('-');
                    string query = "YYBM='" + splits[0]+ "' and ZXSPBM='" +splits[1]+ "' and THDTJRQ='" +splits[2]+ "'";
                    DataRow[] newRows = newTable.Select(query);
                    if (newRows.Count() > 0)
                    {
                        OrderInfo.ImportReturnOrder(newRows);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("退货单导入U8失败!    " + ex.Message);
            }
            MessageBox.Show("退货单导入U8成功!");
        }
    }
}
