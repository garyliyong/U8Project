using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SHYSInterface.退货单
{
    public partial class FormReturnOrderFilter : Form
    {
        public FormReturnOrderFilter()
        {
            InitializeComponent();
        }

        public string FilterCondition { get; set; }

        private void FormReturnOrderFilter_Load(object sender, EventArgs e)
        {
            //医院编码
            string sql = " select YYMC,YYBM from ysxt_YyBM ";
            DataTable db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBoxYYBM.DataSource = db;
            comboBoxYYBM.DisplayMember = "YYMC";
            comboBoxYYBM.ValueMember = "YYBM";
            comboBoxYYBM.Text = "";

            //药品类型
            sql = @" select ypjxmc,ypjxbm from ysxt_ypjx";

            db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBoxYPLX.DataSource = db;
            comboBoxYPLX.DisplayMember = "ypjxmc";
            comboBoxYPLX.ValueMember = "ypjxbm";
            comboBoxYPLX.Text = "";

            //退货单处理方式
            sql = @" select CGBM,CGMC from ysxt_thdclzt";

            db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
            comboBoxTHDCLZT.DataSource = db;
            comboBoxTHDCLZT.DisplayMember = "CGMC";
            comboBoxTHDCLZT.ValueMember = "CGBM";
            comboBoxTHDCLZT.Text = "";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            //是否包含子公司
            checkBoxSFBHZGS.Checked = false;
            //医院编码
            comboBoxYYBM.Text = "";
            //日期
            dateTimePickerStart.Value = DateTime.Today;
            dateTimePickerEnd.Value = DateTime.Today;
            //药品类型
            comboBoxYPLX.Text = "";
            //商品类型
            comboBoxSPLX.Text = "";
            //退货单提交方式
            comboBoxTJFS.Text = "";
            //退货单处理状态
            comboBoxTHDCLZT.Text = "";
            //退货单编号
            textBoxTHDBH.Text = "";
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            //退货单日期不能超过60天
            DateTime dtStart = Convert.ToDateTime(this.dateTimePickerStart.Value.ToShortDateString());
            DateTime dtEnd = Convert.ToDateTime(this.dateTimePickerEnd.Value.ToShortDateString());

            TimeSpan span = dtEnd.Subtract(dtStart);
            int dayDiff = span.Days + 1;
            if (dayDiff >= 60)
            {
                MessageBox.Show("日期天数不能相差60天以上", "提示");
                return;
            }

            StringBuilder xmlData = new StringBuilder();
            xmlData.Append(@"<?xml version=""1.0""  encoding=""utf-8""?> ");
            xmlData.Append("<XMLDATA> ");
            xmlData.Append("<HEAD> ");
            xmlData.Append("<IP>" + SendMessage.GetIP() + "</IP> ");
            xmlData.Append("<MAC>" + SendMessage.GetMAC() + "</MAC> ");
            xmlData.Append("<BZXX></BZXX> ");
            xmlData.Append("</HEAD> ");
            xmlData.Append("<MAIN> ");
            //
            if (checkBoxSFBHZGS.Checked == false)
            {
                xmlData.Append("<SFBHZGS>0</SFBHZGS> ");
            }
            else
            {
                xmlData.Append("<SFBHZGS>1</SFBHZGS> ");
            }
            //
            xmlData.Append("<YYBM>" + comboBoxYYBM.Text + "</YYBM> ");
            //
            xmlData.Append("<QSRQ>" + dateTimePickerStart.Value.Year.ToString("0000") + 
                dateTimePickerStart.Value.Month.ToString("00") + dateTimePickerStart.Value.Day.ToString("00") + "</QSRQ> ");
            xmlData.Append("<JZRQ>" + dateTimePickerEnd.Value.Year.ToString("0000") +
                dateTimePickerEnd.Value.Month.ToString("00") + dateTimePickerEnd.Value.Day.ToString("00") + "</JZRQ> ");
            if (checkBoxDLCGBZ.Checked == true)
            {
                xmlData.Append("<DLCGBZ>1</DLCGBZ> ");
            }
            else
            {
                xmlData.Append("<DLCGBZ>0</DLCGBZ> ");
            }
            //
            if(comboBoxSPLX.Text != "")
            {
                xmlData.Append("<SPLX>" + OrderInfo.GetSPLXCode(comboBoxSPLX.Text) + "</SPLX> ");    
            }
            else
            {
                 xmlData.Append("<SPLX></SPLX> ");
            }

           
            //
            if (comboBoxYPLX.Text != "")
            {
                xmlData.Append("<YPLX>" + OrderInfo.GetYPLXCode(comboBoxYPLX.Text) + "</YPLX> ");
            }
            else
            {
                xmlData.Append("<YPLX></YPLX> ");

            }

            //

            if (comboBoxTJFS.Text != "")
            {
                xmlData.Append("<THDTJFS>" + OrderInfo.GetTJFSCode(comboBoxTJFS.Text) + "</THDTJFS> ");
            }
            else
            {
                xmlData.Append("<THDTJFS></THDTJFS> ");
            }
            if (comboBoxTHDCLZT.Text != "")
            {
                xmlData.Append("<THDCLZT>" + OrderInfo.GetTHDCLZTCode(comboBoxTHDCLZT.Text) + "</THDCLZT> ");
            }
            else
            {
                xmlData.Append("<THDCLZT></THDCLZT> ");
            }

            
            xmlData.Append("<THDBH>" + textBoxTHDBH.Text + "</THDBH> ");
            xmlData.Append("</MAIN> ");
            xmlData.Append("</XMLDATA> ");
            FilterCondition = xmlData.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
