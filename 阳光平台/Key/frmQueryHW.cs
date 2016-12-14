using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SHYSInterface
{
    public partial class frmQueryHW : Form
    {
        #region 全局变量
        string strSelect;
        StringBuilder strbSelect;

        private static string strWareHouseID;
        private static string strWareHouseName;
        private static string strQueryCondition;

        public  static string strmdcode;

        public static string hwID
        {
            get { return strWareHouseID; }

            set { strWareHouseID = value; }
        }

        public static string hwName
        {
            get { return strWareHouseName; }

            set { strWareHouseName = value; }
        }

        public static string QueryCondition
        {
            get { return strQueryCondition; }

            set { strQueryCondition = value; }
        }
        #endregion

        public frmQueryHW()
        {
            InitializeComponent();

            this.ExcuteQuery();
            this.InitControlState();
        }

        //取消查询
        private void tlsubCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        //确认查询
        private void tlsubOK_Click(object sender, EventArgs e)
        {
            this.ReturnResult();
        }

        //确认查询
        private void dgvShow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ReturnResult();
        }

        //执行查询
        private void tlsubQuery_Click(object sender, EventArgs e)
        {
            this.ExcuteQuery();
        }

        //清空查询条件
        private void tlsubClear_Click(object sender, EventArgs e)
        {
            this.cboCondition.SelectedIndex = 0;
            this.cboRelation.SelectedIndex = 0;
            this.txtValue.Text = "";
            this.cboCondition.Focus();
        }

        //创建SQL语句
        private void BuildSqlSelect()
        {
            strSelect = "";
            strbSelect = new StringBuilder();
            strbSelect.Append(" select cus.cCusCode  as 客户编码,cus.cCusName  as 客户名称,ex.ccdefine1  as 医院编码 from Customer cus with(nolock)  left join  Customer_extradefine ex on  cus.cCusCode=ex.cCusCode ");
          
        

            //如果查询条件不为空，则默认是按条件查询
            if (QueryCondition != null && QueryCondition != "")
            {
                strSelect = strbSelect.ToString();
                strSelect += "where cus.cCusCode like'%" + QueryCondition + "%' or ";
                strSelect += " cus.cCusName like'%" + QueryCondition + "%' or ";
                strSelect += " ex.ccdefine1 like'%" + QueryCondition + "%' order by  cus.cCusCode ";

                QueryCondition = null;
                return;
            }

            //如果查询条件和操作符均未设置，则默认为查询全部
            if ((this.cboCondition.Text == null || this.cboCondition.Text.Trim() == "") &&
                (this.cboRelation.Text == null || this.cboRelation.Text.Trim() == ""))
            {
                strSelect = strbSelect.ToString() + "order by  cus.cCusCode";
                return;
            }

            //选择查询条件
            if (this.cboCondition.Text.Trim() == "")
            {
                MessageBox.Show("请选择查询条件。");
                this.cboCondition.Focus();
                return;
            }
            else if (this.cboCondition.Text.Substring(1, 1) == "1")
            {
                strbSelect.Append("where  cus.cCusCode");
            }
            else if (this.cboCondition.Text.Substring(1, 1) == "2")
            {
                strbSelect.Append("where  cus.cCusName");
            }
            else if (this.cboCondition.Text.Substring(1, 1) == "3")
            {
                strbSelect.Append("where ex.ccdefine1");
            }

            //选择操作条件
            if (this.cboRelation.Text.Trim() == "")
            {
                MessageBox.Show("请选择相关操作。");
                this.cboRelation.Focus();
                return;
            }
            else if (this.cboRelation.Text.Substring(1, 1) == "1")
            {
                strbSelect.Append(" = '" + this.txtValue.Text.Trim() + "'");
            }
            else if (this.cboRelation.Text.Substring(1, 1) != "1")
            {
                strbSelect.Append(" like '%" + this.txtValue.Text.Trim() + "%'");
            }

            strSelect = strbSelect.ToString() + "order by  cus.cCusCode";
        }

        //执行查询
        private void ExcuteQuery()
        {
            SqlConnection conn;
            SqlDataAdapter da;
            DataSet ds;


            try
            {
                conn = new SqlConnection(Program.ConnectionString);
                this.BuildSqlSelect();

                //如果Select语句构建失败，则不执行查询
                if (this.strSelect == null || this.strSelect == "")
                    return;

                da = new SqlDataAdapter(this.strSelect, conn);
                ds = new DataSet();
                da.Fill(ds);

                this.dgvShow.DataSource = ds.Tables[0];
            }
            catch (Exception exQuery)
            {
                MessageBox.Show("查询错误：" + exQuery.Message.ToString());
                return;
            }
            finally
            {

            }
        }

        //确认查询
        private void ReturnResult()
        {
            if (this.dgvShow.SelectedRows.Count == 0)
            {
                MessageBox.Show("未选择任何一个医院编码。");
                return;
            }

           hwID = this.dgvShow.SelectedRows[0].Cells[2].Value.ToString().Trim();
           hwName = this.dgvShow.SelectedRows[0].Cells[1].Value.ToString().Trim();

            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        //取消DataGridView控件的排序功能
        private void InitControlState()
        {
            for (int i = 1; i < this.dgvShow.ColumnCount; i++)
            {
                this.dgvShow.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}