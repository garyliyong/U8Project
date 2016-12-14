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
    public partial class Form2 : Form
    {
        private System.Data.SqlClient.SqlDataAdapter thisAdapter;
        SqlConnection thisConnection;
        string strsql = "";
        SqlDataReader mydatareader = null;
        SqlTransaction myTran2 = null;
        public Form2()
        {
            InitializeComponent();

            //DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.BackColor = Color.FromArgb(198, 227, 255);

            //this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(198, 227, 255);
            //this.dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataGridView.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(198, 227, 255);
            //this.dataGridView.GridColor = Color.Black;
            ////style_head.
            //foreach (DataGridViewColumn col in this.dataGridView.Columns)
            //{
            //    col.HeaderCell.Style = style;

            //}
            //foreach (DataGridViewRow row in this.dataGridView.Rows)
            //{
            //    row.HeaderCell.Style = style;

            //}
            //this.dataGridView.EnableHeadersVisualStyles = false;  //<----

            //style = new DataGridViewCellStyle();
            //style.BackColor = Color.FromArgb(198, 227, 255);

            //this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(198, 227, 255);
            //this.dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(198, 227, 255);
            //this.dataGridView1.GridColor = Color.Black;
            ////style_head.
            //foreach (DataGridViewColumn col in this.dataGridView1.Columns)
            //{
            //    col.HeaderCell.Style = style;

            //}
            //foreach (DataGridViewRow row in this.dataGridView1.Rows)
            //{
            //    row.HeaderCell.Style = style;

            //}
            //this.dataGridView1.EnableHeadersVisualStyles = false;  //<----

         //   thisConnection = new SqlConnection(Program.CRMConnectionString);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string cmemo = "";
            try
            {
                if (thisConnection.State.ToString().ToLower() != "open")
                {
                    thisConnection.Open();
                }
                this.textBox1.Text = "";
                DataSet mydataset1 = new DataSet();

                //strsql = "select  [content1] as 内容 from U8Log_CRM where convert(varchar(10),createtime,121) = '" + this.dateTimePicker3.Value.ToString("yyyy-MM-dd") + "' order by autoid ";
                strsql = "select  [content1] as 内容 from U8Log_CRM where CONVERT(varchar(19),createtime,121) >= '" + this.dateTimePicker3.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and CONVERT(varchar(19),createtime,121) <= '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' order by autoid ";
                thisAdapter = new SqlDataAdapter(strsql, thisConnection);
                thisAdapter.Fill(mydataset1, "detail");
                if (mydataset1.Tables["detail"].Rows.Count != 0)
                {
                    for (int i = 0; i < mydataset1.Tables["detail"].Rows.Count; i++)
                    {
                        
                        cmemo = cmemo + mydataset1.Tables["detail"].Rows[i][0].ToString();
                    }

                }
                else
                {
                    MessageBox.Show("没有数据！", "提示", MessageBoxButtons.OK);
                }
                this.textBox1.Text = cmemo;
            }
            catch (Exception)
            {

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.comboBox1.Text = "客户";
            //this.comboBox2.Text = "客户";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.dataGridView.Rows.Add();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SqlDataReader mydatareader = null;
            //int maxautoid = 0;
            //if (thisConnection.State.ToString().ToLower() != "open")
            //{
            //    thisConnection.Open();
            //}
            //try
            //{
            //    SqlCommand cmd2 = new SqlCommand("", thisConnection);
            //    cmd2 = thisConnection.CreateCommand();
            //    myTran2 = thisConnection.BeginTransaction();
            //    cmd2.Transaction = myTran2;

            //    if (this.comboBox1.Text == "客户")
            //    {
            //        strsql = "delete from g_SchemaDoc where autoid>10 and ctype='客户'";
            //    }
            //    if (this.comboBox1.Text == "联系人")
            //    {
            //        strsql = "delete from g_SchemaDoc where autoid>23 and ctype='联系人' ";
            //    }
            //    cmd2.CommandText = strsql;
            //    cmd2.ExecuteNonQuery();

            //    for (int i = 0; i < this.dataGridView.RowCount; i++)
            //    {
            //        if (ClsSystem.gnvl(this.dataGridView.Rows[i].Cells["表列名"].Value, "") != "")
            //        {
            //            if (ClsSystem.gnvl(this.dataGridView.Rows[i].Cells["ID"].Value, "") == "")
            //            {
            //                strsql = "select max(autoid) from g_SchemaDoc where ctype='" + this.comboBox1.Text + "'";
            //                cmd2.CommandText = strsql;
            //                mydatareader = cmd2.ExecuteReader();
            //                if (mydatareader.Read())
            //                {
            //                    maxautoid = int.Parse(ClsSystem.gnvl(mydatareader.GetValue(0), "0")) + 1;
            //                }
            //                mydatareader.Close();

            //                strsql = "insert into g_SchemaDoc(autoid,fildname,fldbname,fldcrmname,ctype) values( ";
            //                strsql = strsql + "'" + maxautoid + "','" + ClsSystem.gnvl(this.dataGridView.Rows[i].Cells["表列名"].Value, "") + "',";
            //                strsql = strsql + "'" + ClsSystem.gnvl(this.dataGridView.Rows[i].Cells["显示名称"].Value, "") + "',";
            //                strsql = strsql + "'" + ClsSystem.gnvl(this.dataGridView.Rows[i].Cells["字段名"].Value, "") + "','" + this.comboBox1.Text + "')";
            //                cmd2.CommandText = strsql;
            //                cmd2.ExecuteNonQuery();

            //            }
            //        }
            //    }
            //    myTran2.Commit();
            //    MessageBox.Show("保存成功！");
            //}
            //catch (Exception ex )
            //{
            //    myTran2.Rollback();
            //    MessageBox.Show(ex.Message);
                

            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.dataGridView.RowCount =0;
            //strsql = "select fildname,fldbname,fldcrmname,AUTOID from g_SchemaDoc where ctype='"+ this.comboBox1.Text +"' ORDER BY AUTOID";
            //thisAdapter = new SqlDataAdapter(strsql, thisConnection);
            //DataSet mydataset = new DataSet();
            //thisAdapter.Fill(mydataset, "客户数据字典");
            //for (int i = 0; i < mydataset.Tables["客户数据字典"].Rows.Count; i++)
            //{
            //    this.dataGridView.Rows.Add(1);
            //    this.dataGridView.Rows[i].Cells["表列名"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["fildname"], "");
            //    this.dataGridView.Rows[i].Cells["显示名称"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["fldbname"], "");
            //    this.dataGridView.Rows[i].Cells["字段名"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["fldcrmname"], "");
            //    this.dataGridView.Rows[i].Cells["ID"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["AUTOID"], "");
            //    if (this.comboBox1.Text == "客户")
            //    {
            //        if (int.Parse(ClsSystem.gnvl(this.dataGridView.Rows[i].Cells["ID"].Value, "0")) <= 10)
            //        {
            //            this.dataGridView.Rows[i].Cells["表列名"].ReadOnly = true;
            //            this.dataGridView.Rows[i].Cells["显示名称"].ReadOnly = true;
            //            this.dataGridView.Rows[i].Cells["字段名"].ReadOnly = true;
            //        }
            //    }
            //    if (this.comboBox1.Text == "联系人")
            //    {
            //        if (int.Parse(ClsSystem.gnvl(this.dataGridView.Rows[i].Cells["ID"].Value, "0")) <= 23)
            //        {
            //            this.dataGridView.Rows[i].Cells["表列名"].ReadOnly = true;
            //            this.dataGridView.Rows[i].Cells["显示名称"].ReadOnly = true;
            //            this.dataGridView.Rows[i].Cells["字段名"].ReadOnly = true;
            //        }
            //    }
            //}

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.dataGridView1.Rows.Add();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SqlDataReader mydatareader = null;
            //int maxautoid = 0;


            //if (thisConnection.State.ToString().ToLower() != "open")
            //{
            //    thisConnection.Open();
            //}
            //try
            //{
            //    SqlCommand cmd2 = new SqlCommand("", thisConnection);
            //    cmd2 = thisConnection.CreateCommand();
            //    myTran2 = thisConnection.BeginTransaction();
            //    cmd2.Transaction = myTran2;

               
            //        strsql = "delete from g_TableContrase where autoid>25  ";
                
            //    cmd2.CommandText = strsql;
            //    cmd2.ExecuteNonQuery();

            //    for (int i = 0; i < this.dataGridView1.RowCount; i++)
            //    {
            //        if (ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["研究所列名"].Value, "") != "")
            //        {
            //            if (ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["存储过程参数"].Value, "") == "")
            //            {
            //                myTran2.Rollback();
            //                MessageBox.Show("请选择存储过程参数");
                            
            //                return;
            //            }
            //            if (ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["ID1"].Value, "") == "")
            //            {
            //                strsql = "select max(autoid) from g_TableContrase ";
            //                cmd2.CommandText = strsql;
            //                mydatareader = cmd2.ExecuteReader();
            //                if (mydatareader.Read())
            //                {
            //                    maxautoid = int.Parse(ClsSystem.gnvl(mydatareader.GetValue(0), "0")) + 1;
            //                }
            //                mydatareader.Close();

            //                strsql = "insert into g_TableContrase(autoid,cyjsfildname,ccrmfildname,cparatype,ctype) values( ";
            //                strsql = strsql + "'" + maxautoid + "','" + ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["研究所列名"].Value, "") + "',";
            //                strsql = strsql + "'" + ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["CRM显示名称"].Value, "") + "',";
            //                if (ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["存储过程参数"].Value, "0") == "0")
            //                {
            //                    strsql = strsql + "null,";
            //                }
            //                else
            //                {
            //                    strsql = strsql + "'" + ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["存储过程参数"].Value, "") + "',";
            //                }
                            
            //                strsql=strsql + " '" + this.comboBox2.Text + "')";
            //                cmd2.CommandText = strsql;
            //                cmd2.ExecuteNonQuery();

            //            }
            //        }
            //    }
            //    myTran2.Commit();
            //    MessageBox.Show("保存成功！");
            //}
            //catch (Exception ex)
            //{
            //    myTran2.Rollback();
            //    MessageBox.Show(ex.Message);


            //}
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.dataGridView1.RowCount = 0;
            //strsql = "select cyjsfildname,ccrmfildname,cparatype,AUTOID from g_TableContrase where ctype='" + this.comboBox2.Text + "' ORDER BY AUTOID";
            //thisAdapter = new SqlDataAdapter(strsql, thisConnection);
            //DataSet mydataset = new DataSet();
            //thisAdapter.Fill(mydataset, "客户数据字典");
            //for (int i = 0; i < mydataset.Tables["客户数据字典"].Rows.Count; i++)
            //{
            //    this.dataGridView1.Rows.Add(1);
            //    this.dataGridView1.Rows[i].Cells["研究所列名"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["cyjsfildname"], "");
            //    this.dataGridView1.Rows[i].Cells["CRM显示名称"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["ccrmfildname"], "");
            //    this.dataGridView1.Rows[i].Cells["存储过程参数"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["cparatype"], "0");
            //    this.dataGridView1.Rows[i].Cells["ID1"].Value = ClsSystem.gnvl(mydataset.Tables["客户数据字典"].Rows[i]["AUTOID"], "");
                
            //    if (int.Parse(ClsSystem.gnvl(this.dataGridView1.Rows[i].Cells["ID1"].Value, "0")) <= 25)
            //    {
            //        this.dataGridView1.Rows[i].Cells["研究所列名"].ReadOnly = true;
            //        this.dataGridView1.Rows[i].Cells["CRM显示名称"].ReadOnly = true;
            //        this.dataGridView1.Rows[i].Cells["存储过程参数"].ReadOnly = true;
            //    }
                
               
            //}
        }
    }
}
