using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SHYSInterface
{
    public partial class frmFilter : Form
    {
        #region 全局变量
      //  private object vbLogin = (object)MyNetUserControl.cls;
        private static string strSQL;
        private StringBuilder strbSQL;

        public static string QueryCondition
        {
            get { return strSQL; }

            set { strSQL = value; }
        }
        #endregion

        public frmFilter()
        {
            InitializeComponent();
        }

        private void frmFilter2_Load(object sender, EventArgs e)
        {
            this.ClearQueryCondition();
            DataTable db = new DataTable();
            //医院编码
            string sql = " select YYMC,YYBM from ysxt_YyBM ";

            db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBox1.DataSource = db;
            comboBox1.DisplayMember = "YYMC";
            comboBox1.ValueMember = "YYBM";
            comboBox1.Text = "";
            //for (int i = 0; i < db.Rows.Count; i++)
            //{
            //    DataRow row = db.Rows[i];
            //    this.comboBox1.Items.Add(row["vt_name"].ToString() + "|" + row["vt_ID"].ToString());
            //}

            //药品剂型
            sql = @" select ypjxmc,ypjxbm from ysxt_ypjx";

            db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBox2.DataSource = db;
            comboBox2.DisplayMember = "ypjxmc";
            comboBox2.ValueMember = "ypjxbm";
            comboBox2.Text = "";
            //采购模式
            sql = @" select cgmc,cgbm from ysxt_cgms";

            db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBox3.DataSource = db;
            comboBox3.DisplayMember = "cgmc";
            comboBox3.ValueMember = "cgbm";
            comboBox3.Text = "";
            //订单处理状态
            sql = @" select mc,bm from ysxt_ddclzt";

            db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBox7.DataSource = db;
            comboBox7.DisplayMember = "mc";
            comboBox7.ValueMember = "bm";
            comboBox7.Text = "";

        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        //选择经由商社
        private void txtKeHu1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmQueryCustomer.QueryCondition = this.txtKeHu1.Text;
            //frmQueryCustomer Customer = new frmQueryCustomer();

            //if (DialogResult.OK == Customer.ShowDialog())
            //{
            //    this.txtKeHu1.Text = frmQueryCustomer.CustomerID;
            //}
        }

        //选择经由商社
        private void txtKeHu2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmQueryCustomer.QueryCondition = this.txtKeHu2.Text;
            //frmQueryCustomer Customer = new frmQueryCustomer();

            //if (DialogResult.OK == Customer.ShowDialog())
            //{
            //    this.txtKeHu2.Text = frmQueryCustomer.CustomerID;
            //}
        }

        //选择部门
        private void txtYYS1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmQueryDepartment.QueryCondition = this.txtYYS1.Text;
            //frmQueryDepartment Department = new frmQueryDepartment();

            //if (DialogResult.OK == Department.ShowDialog())
            //{
            //    this.txtYYS1.Text = frmQueryDepartment.DepartmentID;
            //}
        }

        //选择部门
        private void txtYYS2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmQueryDepartment.QueryCondition = this.txtYYS2.Text;
            //frmQueryDepartment Department = new frmQueryDepartment();

            //if (DialogResult.OK == Department.ShowDialog())
            //{
            //    this.txtYYS2.Text = frmQueryDepartment.DepartmentID;
            //}
        }

        //选择产品
        private void txtChanPin1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmQueryInventory.QueryCondition = this.txtChanPin1.Text;
            //frmQueryInventory Inventory = new frmQueryInventory();

            //if (DialogResult.OK == Inventory.ShowDialog())
            //{
            //    this.txtChanPin1.Text = frmQueryInventory.InvCode;
            //}
        }

        //选择产品
        private void txtChanPin2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmQueryInventory.QueryCondition = this.txtChanPin2.Text;
            //frmQueryInventory Inventory = new frmQueryInventory();

            //if (DialogResult.OK == Inventory.ShowDialog())
            //{
            //    this.txtChanPin2.Text = frmQueryInventory.InvCode;
            //}
        }

        //清空查询条件
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearQueryCondition();
        }        
        
        //过滤单据
        private void btnFilter_Click(object sender, EventArgs e)
        {
            this.strbSQL = new StringBuilder();

            if (this.checkBox1.Checked == false && comboBox1.Text.Trim() == "" && comboBox2.Text.Trim() == "" && comboBox3.Text.Trim() == "" && comboBox4.Text.Trim() == "" && comboBox5.Text.Trim() == "" && comboBox6.Text.Trim() == "" && comboBox7.Text.Trim() == ""  && this.dtpDJRQ1.Text.Trim() == "" && this.dtpDJRQ2.Text.Trim() == "")
            {
                MessageBox.Show("请先选择条件", "提示");
                return;
            }
            DateTime dt1 = Convert.ToDateTime(this.dtpDJRQ1.Value.ToShortDateString());
            DateTime dt2 = Convert.ToDateTime(this.dtpDJRQ2.Value.ToShortDateString());

            TimeSpan span = dt2.Subtract(dt1);
            int dayDiff = span.Days + 1;
            if (dayDiff >= 60)
            {
                MessageBox.Show("日期天数不能相差60天以上", "提示");
                return;
            }


          this.strbSQL.Append(@"<?xml version=""1.0""  encoding=""utf-8""?>");
          this.strbSQL.Append( "<XMLDATA>");
           this.strbSQL.Append( "<HEAD>");
           this.strbSQL.Append( "<IP>" + SendMessage.GetIP() + "</IP>");
           this.strbSQL.Append( "<MAC>" + SendMessage.GetMAC() + "</MAC> ");
           this.strbSQL.Append( "<BZXX></BZXX> ");
           this.strbSQL.Append( "</HEAD> ");
           this.strbSQL.Append( "<MAIN>");


              if (this.checkBox1.Checked == false)
                {
                    this.strbSQL.Append("<SFBHZGS>0</SFBHZGS> ");
                }
                else
                {
                    this.strbSQL.Append("<SFBHZGS>1</SFBHZGS> ");
                }

              if (this.comboBox1.Text.Trim() != "")
              {
                  this.strbSQL.Append("<YYBM>" + comboBox1.Text + "</YYBM> ");
              }
              else
              {
                  this.strbSQL.Append("<YYBM></YYBM> ");
              }


              //if (this.dtpDJRQ1.Checked == true)
              //{
                  this.strbSQL.Append("<QSRQ>" + this.dtpDJRQ1.Value.Year.ToString("0000")+this.dtpDJRQ1.Value.Month.ToString("00")+this.dtpDJRQ1.Value.Day.ToString("00")+ "</QSRQ> ");
              //}
              //else
              //{
              //    this.strbSQL.Append("<QSRQ></QSRQ> ");
              //}


              //if (this.dtpDJRQ2.Checked == true)
              //{
                  this.strbSQL.Append("<JZRQ>" + this.dtpDJRQ2.Value.Year.ToString("0000") + dtpDJRQ2.Value.Month.ToString("00") + dtpDJRQ2.Value.Day.ToString("00")  +"</JZRQ> ");
              //}
              //else
              //{
              //    this.strbSQL.Append("<JZRQ></JZRQ> ");
              //}
              if (this.comboBox2.Text.Trim() != "")
              {
                  this.strbSQL.Append("<YPLX>" + comboBox2.Text + "</YPLX> ");
              }
              else
              {
                  this.strbSQL.Append("<YPLX></YPLX> ");
              }
              if (this.comboBox3.Text.Trim() != "")
              {
                  this.strbSQL.Append("<CGLX>" + comboBox3.Text + "</CGLX> ");
              }
              else
              {
                  this.strbSQL.Append("<CGLX></CGLX> ");
              }
              if (this.comboBox4.Text.Trim() != "")
              {
                  if (comboBox4.Text == "医院自行订单")
                  {
                      this.strbSQL.Append("<DDLX>" + YsxtEnum.DDLX.医院自行订单 + "</DDLX> ");
                  }
                  else
                  {
                      this.strbSQL.Append("<DDLX>" + YsxtEnum.DDLX.托管药库订单 + "</DDLX> ");
                  }
              }
              else
              {
                  this.strbSQL.Append("<DDLX></DDLX> ");
              }
              if (this.comboBox5.Text.Trim() != "")
              {
                  if (comboBox5.Text == "药品类")
                  {
                      this.strbSQL.Append("<SPLX>1</SPLX> ");
                  }
                  else if (comboBox5.Text == "医用耗材器械类")
                  {
                      this.strbSQL.Append("<SPLX>2</SPLX> ");
                  }
                  else
                  {
                      this.strbSQL.Append("<SPLX>9</SPLX> ");
                  }
              }
              else
              {
                  this.strbSQL.Append("<SPLX></SPLX> ");
              }
              if (this.comboBox6.Text.Trim() != "")
              {
                  if (comboBox6.Text == "医院填报")
                  {
                      this.strbSQL.Append("<DDTJFS>1</DDTJFS> ");
                  }
                  else
                  {
                      this.strbSQL.Append("<DDTJFS>2</DDTJFS> ");
                  }
              }
              else
              {
                  this.strbSQL.Append("<DDTJFS></DDTJFS> ");
              }
            if (this.comboBox7.Text.Trim() != "")
            {
                this.strbSQL.Append("<DDCLZT>" + comboBox7.Text + "</DDCLZT> ");
            }
            else
            {
                this.strbSQL.Append("<DDCLZT></DDCLZT> ");
            }
            if (this.textBox1.Text.Trim() != "")
            {
                this.strbSQL.Append("<DDMXBH>" + textBox1.Text + "</DDMXBH> ");
            }
            else
            {
                this.strbSQL.Append("<DDMXBH></DDMXBH> ");
            }
            this.strbSQL.Append("</MAIN> ");
            this.strbSQL.Append("</XMLDATA> ");

            QueryCondition = strbSQL.ToString();

            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        //清空查询条件
        private void ClearQueryCondition()
        {
           
        
        
            this.textBox1.Text = "";
         

            this.dtpDJRQ1.Value = DateTime.Today;
            this.dtpDJRQ1.Checked = false;
            this.dtpDJRQ2.Value = DateTime.Today;
            this.dtpDJRQ2.Checked = false;
            this.comboBox1.Text = "";
            this.comboBox2.Text = ""; 
            this.comboBox3.Text = "";
            this.comboBox4.Text = "";
            this.comboBox5.Text = "";
            this.comboBox6.Text = "";
            this.comboBox7.Text = "";
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            //txtKeHu1.Text = "";
            //U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
            //obj.RefID = "Customer_AA";// 
            //obj.Mode = U8RefService.RefModes.modeRefing;
            //obj.Web = false;
            //obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
            //obj.RememberLastRst = false;
            //ADODB.Recordset retRstGrid = null, retRstClass = null;
            //string sErrMsg = "";
            //obj.GetPortalHwnd((int)this.Handle);
            //if (obj.ShowRefSecond(ref vbLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
            //{
            //    MessageBox.Show(sErrMsg);
            //}
            //else
            //{
            //    if (retRstGrid != null)
            //    {
            //        txtKeHu1.Text = retRstGrid.Fields["cCusCode"].Value.ToString();
            //       // txtKeHu1.Text = retRstGrid.Fields["cCusName"].Value.ToString();
                   
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            // txtKeHu2.Text="";
            //U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
            //obj.RefID = "Customer_AA";// 
            //obj.Mode = U8RefService.RefModes.modeRefing;
            //obj.Web = false;
            //obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
            //obj.RememberLastRst = false;
            //ADODB.Recordset retRstGrid = null, retRstClass = null;
            //string sErrMsg = "";
            //obj.GetPortalHwnd((int)this.Handle);
            //if (obj.ShowRefSecond(ref vbLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
            //{
            //    MessageBox.Show(sErrMsg);
            //}
            //else
            //{
            //    if (retRstGrid != null)
            //    {
            //        txtKeHu2.Text = retRstGrid.Fields["cCusCode"].Value.ToString();
            //      //  txtKeHu2.Text = retRstGrid.Fields["cCusName"].Value.ToString();

            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //textBox1.Text = "";
            //U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
            //obj.RefID = "SA_REF_SOMain_SA";// 
            //obj.Mode = U8RefService.RefModes.modeRefing;
            //obj.Web = false;
            //obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
            //obj.RememberLastRst = false;
            //ADODB.Recordset retRstGrid = null, retRstClass = null;
            //string sErrMsg = "";
            //obj.GetPortalHwnd((int)this.Handle);
            //if (obj.ShowRefSecond(ref vbLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
            //{
            //    MessageBox.Show(sErrMsg);
            //}
            //else
            //{
            //    if (retRstGrid != null)
            //    {
            //        textBox1.Text = retRstGrid.Fields["csocode"].Value.ToString();
            //        //  txtKeHu2.Text = retRstGrid.Fields["cCusName"].Value.ToString();

            //    }
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textBox2.Text = "";
            //U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
            //obj.RefID = "SA_REF_SOMain_SA";// 
            //obj.Mode = U8RefService.RefModes.modeRefing;
            //obj.Web = false;
            //obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
            //obj.RememberLastRst = false;
            //ADODB.Recordset retRstGrid = null, retRstClass = null;
            //string sErrMsg = "";
            //obj.GetPortalHwnd((int)this.Handle);
            //if (obj.ShowRefSecond(ref vbLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
            //{
            //    MessageBox.Show(sErrMsg);
            //}
            //else
            //{
            //    if (retRstGrid != null)
            //    {
            //        textBox2.Text = retRstGrid.Fields["csocode"].Value.ToString();
            //        //  txtKeHu2.Text = retRstGrid.Fields["cCusName"].Value.ToString();

            //    }
            //}
        }
    }
}