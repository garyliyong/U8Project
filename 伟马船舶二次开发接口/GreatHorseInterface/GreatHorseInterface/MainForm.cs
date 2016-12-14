using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreatHorseInterface.ViewModel;
using GreatHorseInterface.Model;
using LoginUserControl;
using LoginUserControl.Model;

namespace GreatHorseInterface
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 基础档案
        /// </summary>
        public BasicArchiveViewModel basicarchViewModel = new BasicArchiveViewModel();

        public MainForm()
        {
            Init();
            InitializeComponent();
        }

        private void Init()
        {
            LoginSettingInfo.AccId = AppSettingInfo.AccId;
            LoginSettingInfo.User = AppSettingInfo.User;
            LoginSettingInfo.Server = AppSettingInfo.Server;
            LoginSettingInfo.Pwd = AppSettingInfo.Pwd;

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            AppSettingInfo.AccId = LoginSettingInfo.AccId;
            AppSettingInfo.User = LoginSettingInfo.User;
        }

        /// <summary>
        /// 加载基础档案XML配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridViewComboBoxColumn cboColumn = dataGridViewBase.Columns["U8Account"] as DataGridViewComboBoxColumn;
                if (cboColumn != null)
                {
                    cboColumn.DataSource = Account.GetAccountIds();
                }
                basicarchViewModel.Load();
                for (int row = 0; row < basicarchViewModel.Archives.Count; ++row)
                {
                    dataGridViewBase.Rows.Add();
                    dataGridViewBase.Rows[row].Cells["VesselCode"].Value = basicarchViewModel.Archives[row].VesselCode;
                    dataGridViewBase.Rows[row].Cells["U8Account"].Value = basicarchViewModel.Archives[row].U8Account;
                    dataGridViewBase.Rows[row].Cells["U8AccountName"].Value = basicarchViewModel.Archives[row].U8AccountName;
                    dataGridViewBase.Rows[row].Cells["Year"].Value = basicarchViewModel.Archives[row].Year;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        /// <summary>
        /// 保存基础档案信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                basicarchViewModel.Archives.Clear();
                for (int row = 0; row < dataGridViewBase.RowCount; ++row)
                {
                    if (dataGridViewBase.Rows[row].IsNewRow)
                    {
                        continue;
                    }
                    BasicArchive basicArchive = new BasicArchive();
                    basicArchive.VesselCode = dataGridViewBase.Rows[row].Cells["VesselCode"].Value.ToString();
                    basicArchive.U8Account = dataGridViewBase.Rows[row].Cells["U8Account"].Value.ToString();
                    basicArchive.U8AccountName = dataGridViewBase.Rows[row].Cells["U8AccountName"].Value.ToString();
                    basicArchive.Year = dataGridViewBase.Rows[row].Cells["Year"].Value.ToString();
                    basicarchViewModel.Archives.Add(basicArchive);
                }
                basicarchViewModel.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewBase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewComboBoxCell cboCell = dataGridViewBase.Rows[e.RowIndex].Cells["U8Account"] as DataGridViewComboBoxCell;
            if (cboCell != null && cboCell.EditedFormattedValue.ToString().Length > 0)
            {
                //根据帐套号自动显示帐套名称和日期
                string u8Account = cboCell.EditedFormattedValue.ToString();
                Account account = null;
                foreach (Account value in Account.Accounts)
                {
                    if (value.cAcc_Id == u8Account)
                    {
                        account = value;
                        break;
                    }
                }
                if (account == null)
                {
                    return;
                }
                DataGridViewTextBoxCell textCell = dataGridViewBase.Rows[e.RowIndex].Cells["U8AccountName"] as DataGridViewTextBoxCell;
                if (textCell != null)
                {

                    textCell.Value = account.cAcc_Name;
                }
                textCell = dataGridViewBase.Rows[e.RowIndex].Cells["Year"] as DataGridViewTextBoxCell;
                if (textCell != null)
                {
                    textCell.Value = account.iYear;
                }
            }
        }

        /// <summary>
        /// 校验是否有相同的船舶号或U8帐套号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewBase_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        DataGridViewTextBoxCell textCell = dataGridViewBase.Rows[e.RowIndex].Cells["VesselCode"] 
                            as DataGridViewTextBoxCell;
                        if (textCell != null && textCell.IsInEditMode && textCell.EditedFormattedValue != null)
                        {
                            var result = from s in basicarchViewModel.Archives where s.VesselCode == textCell.EditedFormattedValue.ToString() select s;
                            if (result != null && result.Count() > 0)
                            {
                                MessageBox.Show("船舶号"+textCell.EditedFormattedValue.ToString() +"已存在","伟马U8二次开发接口",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                e.Cancel = true;
                                dataGridViewBase.CancelEdit();
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        DataGridViewComboBoxCell cboCell = dataGridViewBase.Rows[e.RowIndex].Cells["U8Account"]
                            as DataGridViewComboBoxCell;
                        if (cboCell != null && cboCell.IsInEditMode && cboCell.EditedFormattedValue != null)
                        {
                            var result = from s in basicarchViewModel.Archives where s.U8Account == cboCell.EditedFormattedValue.ToString() select s;
                            if (result != null && result.Count() > 0)
                            {
                                MessageBox.Show("U8帐套号" + cboCell.EditedFormattedValue.ToString() + "已存在", "伟马U8二次开发接口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Cancel = true;
                                dataGridViewBase.CancelEdit();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 应付发票导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportToU8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> filePaths = new List<string>();
                foreach (var filePath in openFileDialog.FileNames)
                {
                    APVouchViewModel apViewModel = new APVouchViewModel(this);
                    if (!apViewModel.ImportByExcel(filePath))
                    {
                        filePaths.Add(filePath);
                    }
                    else
                    {
                        MessageBox.Show("应付发票导入失败!");
                    }
                }
                if (filePaths.Count > 0)
                {
                    InfoForm infoForm = new InfoForm();
                    infoForm.Infos = filePaths;
                    infoForm.ShowDialog();
                }
            }
        }
    }
}
