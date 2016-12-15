using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaistInterface.ViewModel;
using LoginUserControl;
using DataBaseHelper;
using LoginUserControl.Model;
using BaistInterface.Model;

namespace BaistInterface
{
    public partial class MainForm : Form
    {
        public MainFormViewModel MainFormViewModel { get; set; }

        public MainForm()
        {
            MainFormViewModel = new MainFormViewModel();
            Init();
            InitializeComponent();
            InitAccount();
            InitCode();
            InitVendor();
            InitCustomer();
            InitDepartment();
        }

        /// <summary>
        /// 窗口初始化，记录登录信息
        /// </summary>
        private void Init()
        {
            LoginSettingInfo.AccId = AppSettingInfo.AccId;
            LoginSettingInfo.Server = AppSettingInfo.Server;
            LoginSettingInfo.User = AppSettingInfo.User;
            LoginSettingInfo.Pwd = AppSettingInfo.Pwd;
            LoginSettingInfo.Year = AppSettingInfo.Year;

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            //保存配置文件
            AppSettingInfo.AccId = LoginSettingInfo.AccId;
            AppSettingInfo.User = LoginSettingInfo.User;
        }

        #region 帐套

        /// <summary>
        /// 初始化帐套档案，显示所有U8帐套，缺省匹配项为空
        /// </summary>
        private void InitAccount()
        {
            dataGridViewAccount.Rows.Clear();
            dataGridViewAccount.AutoGenerateColumns = false;
            try
            {
                //加载配置配置表
                MainFormViewModel.LoadAccount();
                //U8帐套
                int rows = Account.Accounts.Count;
                for (int row = 0; row < rows; ++row)
                {
                    dataGridViewAccount.Rows.Add();
                    dataGridViewAccount.Rows[row].Cells["VesselCode"].Value = MainFormViewModel.FindAccountById(Account.Accounts[row].cAcc_Id);
                    dataGridViewAccount.Rows[row].Cells["U8Account"].Value = Account.Accounts[row].cAcc_Id;
                    dataGridViewAccount.Rows[row].Cells["U8AccountName"].Value = Account.Accounts[row].cAcc_Name;
                    dataGridViewAccount.Rows[row].Cells["Year"].Value = Account.Accounts[row].iYear;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 保存U8帐套档案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAccount_Click(object sender, EventArgs e)
        {
            try
            {
                MainFormViewModel.AccountArchives.Clear();
                for (int row = 0; row < dataGridViewAccount.RowCount; ++row)
                {
                    if (dataGridViewAccount.Rows[row].IsNewRow || dataGridViewAccount.Rows[row].Cells["VesselCode"].Value.ToString().Length == 0)
                    {
                        continue;
                    }
                    AccountArchive accountArchive = new AccountArchive();
                    accountArchive.VesselCode = dataGridViewAccount.Rows[row].Cells["VesselCode"].Value.ToString();
                    accountArchive.U8Account = dataGridViewAccount.Rows[row].Cells["U8Account"].Value.ToString();
                    accountArchive.U8AccountName = dataGridViewAccount.Rows[row].Cells["U8AccountName"].Value.ToString();
                    accountArchive.Year = dataGridViewAccount.Rows[row].Cells["Year"].Value.ToString();
                    MainFormViewModel.AccountArchives.Add(accountArchive);
                }
                MainFormViewModel.SaveAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("帐套档案保存成功!");
        }

        /// <summary>
        /// 帐套号校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewAccount_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        DataGridViewTextBoxCell textCell = dataGridViewAccount.Rows[e.RowIndex].Cells["VesselCode"]
                            as DataGridViewTextBoxCell;
                        if (textCell != null && textCell.IsInEditMode && textCell.EditedFormattedValue != null &&
                            textCell.EditedFormattedValue.ToString().Length > 0 &&
                            textCell.FormattedValue.ToString() != textCell.EditedFormattedValue.ToString())
                        {
                            bool bFind = false;
                            for (int i = 0; i < dataGridViewAccount.Rows.Count; ++i)
                            {
                                if (dataGridViewAccount.Rows[i].IsNewRow || i == e.RowIndex)
                                {
                                    continue;
                                }
                                if (dataGridViewAccount.Rows[i].Cells["VesselCode"].Value != null && dataGridViewAccount.Rows[i].Cells["VesselCode"].Value.ToString() == textCell.EditedFormattedValue.ToString())
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                MessageBox.Show("ERP帐套号" + textCell.EditedFormattedValue.ToString() + "已存在", "佰斯特凭证导入接口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Cancel = true;
                                dataGridViewAccount.CancelEdit();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion 帐套

        #region 会计科目

        /// <summary>
        /// 初始化会计科目编码
        /// </summary>
        private void InitCode()
        {
            dataGridViewCode.Rows.Clear();
            dataGridViewCode.AutoGenerateColumns = false;
            try
            {
                MainFormViewModel.LoadCode();
                if (Code.Codes != null)
                {
                    int rows = Code.Codes.Count;
                    for (int row = 0; row < rows; ++row)
                    {
                        dataGridViewCode.Rows.Add();
                        dataGridViewCode.Rows[row].Cells["ERPCode"].Value = MainFormViewModel.FindCodeById(Code.Codes[row].ccode);
                        dataGridViewCode.Rows[row].Cells["U8Code"].Value = Code.Codes[row].ccode;
                        dataGridViewCode.Rows[row].Cells["U8CodeName"].Value = Code.Codes[row].ccode_Name;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 保存会计科目档案信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCode_Click(object sender, EventArgs e)
        {
            try
            {
                MainFormViewModel.CodeArchives.Clear();
                for (int row = 0; row < dataGridViewCode.RowCount; ++row)
                {
                    if (dataGridViewCode.Rows[row].IsNewRow)
                    {
                        continue;
                    }
                    CodeArchive codeArchive = new CodeArchive();
                    codeArchive.ERPCode = dataGridViewCode.Rows[row].Cells["ERPCode"].Value.ToString();
                    codeArchive.U8Code = dataGridViewCode.Rows[row].Cells["U8Code"].Value.ToString();
                    codeArchive.U8CodeName = dataGridViewCode.Rows[row].Cells["U8CodeName"].Value.ToString();
                    MainFormViewModel.CodeArchives.Add(codeArchive);
                }
                MainFormViewModel.SaveCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("会计科目档案保存成功!");
        }

        /// <summary>
        /// 会计科目校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCode_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        DataGridViewTextBoxCell textCell = dataGridViewCode.Rows[e.RowIndex].Cells["ERPCode"]
                            as DataGridViewTextBoxCell;
                        if (textCell != null && textCell.IsInEditMode && textCell.EditedFormattedValue != null &&
                            textCell.EditedFormattedValue.ToString().Length > 0 &&
                            textCell.FormattedValue.ToString() != textCell.EditedFormattedValue.ToString())
                        {
                            bool bFind = false;
                            for (int i = 0; i < dataGridViewCode.Rows.Count; ++i)
                            {
                                if (dataGridViewCode.Rows[i].IsNewRow || i == e.RowIndex)
                                {
                                    continue;
                                }
                                if (dataGridViewCode.Rows[i].Cells["ERPCode"].Value != null && dataGridViewCode.Rows[i].Cells["ERPCode"].Value.ToString() == textCell.EditedFormattedValue.ToString())
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                MessageBox.Show("ERP会计科目编号" + textCell.EditedFormattedValue.ToString() + "已存在", "佰斯特凭证导入接口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Cancel = true;
                                dataGridViewCode.CancelEdit();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        #endregion 会计科目

        #region 供应商

        private void InitVendor()
        {
            dataGridViewVendor.Rows.Clear();
            dataGridViewVendor.AutoGenerateColumns = false;
            try
            {
                MainFormViewModel.LoadVendor();
                if (Vendor.Vendors != null)
                {
                    int rows = Vendor.Vendors.Count;
                    for (int row = 0; row < rows; ++row)
                    {
                        dataGridViewVendor.Rows.Add();
                        dataGridViewVendor.Rows[row].Cells["ERPVendor"].Value = MainFormViewModel.FindVendorById(Vendor.Vendors[row].cVenCode);
                        dataGridViewVendor.Rows[row].Cells["U8Vendor"].Value = Vendor.Vendors[row].cVenCode;
                        dataGridViewVendor.Rows[row].Cells["U8VendorName"].Value = Vendor.Vendors[row].cVenName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewVendor_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        DataGridViewTextBoxCell textCell = dataGridViewVendor.Rows[e.RowIndex].Cells["ERPVendor"]
                            as DataGridViewTextBoxCell;
                        if (textCell != null && textCell.IsInEditMode && textCell.EditedFormattedValue != null &&
                            textCell.EditedFormattedValue.ToString().Length > 0 &&
                            textCell.FormattedValue.ToString() != textCell.EditedFormattedValue.ToString())
                        {
                            bool bFind = false;
                            for (int i = 0; i < dataGridViewVendor.Rows.Count; ++i)
                            {
                                if (dataGridViewVendor.Rows[i].IsNewRow || i == e.RowIndex)
                                {
                                    continue;
                                }
                                if (dataGridViewVendor.Rows[i].Cells["ERPVendor"].Value != null && dataGridViewVendor.Rows[i].Cells["ERPVendor"].Value.ToString() == textCell.EditedFormattedValue.ToString())
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                MessageBox.Show("供应商编码" + textCell.EditedFormattedValue.ToString() + "已存在", "佰斯特凭证导入接口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Cancel = true;
                                dataGridViewVendor.CancelEdit();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnSaveVendor_Click(object sender, EventArgs e)
        {
            try
            {
                MainFormViewModel.VendorArchives.Clear();
                for (int row = 0; row < dataGridViewVendor.RowCount; ++row)
                {
                    if (dataGridViewVendor.Rows[row].IsNewRow)
                    {
                        continue;
                    }
                    VendorArchive vendorArchive = new VendorArchive();
                    vendorArchive.ERPVendor = dataGridViewVendor.Rows[row].Cells["ERPVendor"].Value.ToString();
                    vendorArchive.U8Vendor = dataGridViewVendor.Rows[row].Cells["U8Vendor"].Value.ToString();
                    vendorArchive.U8VendorName = dataGridViewVendor.Rows[row].Cells["U8VendorName"].Value.ToString();
                    MainFormViewModel.VendorArchives.Add(vendorArchive);
                }
                MainFormViewModel.SaveVendor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("供应商档案保存成功!");
        }

        #endregion 供应商

        #region 客户

        private void InitCustomer()
        {
            dataGridViewCustomer.Rows.Clear();
            dataGridViewCustomer.AutoGenerateColumns = false;
            try
            {
                MainFormViewModel.LoadCustomer();
                if (Customer.Customers != null)
                {
                    int rows = Customer.Customers.Count;
                    for (int row = 0; row < rows; ++row)
                    {
                        dataGridViewCustomer.Rows.Add();
                        dataGridViewCustomer.Rows[row].Cells["ERPCustomer"].Value = MainFormViewModel.FindCustomerById(Customer.Customers[row].cCusCode);
                        dataGridViewCustomer.Rows[row].Cells["U8Customer"].Value = Customer.Customers[row].cCusCode;
                        dataGridViewCustomer.Rows[row].Cells["U8CustomerName"].Value = Customer.Customers[row].cCusName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewCustomer_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        DataGridViewTextBoxCell textCell = dataGridViewCustomer.Rows[e.RowIndex].Cells["ERPCustomer"]
                            as DataGridViewTextBoxCell;
                        if (textCell != null && textCell.IsInEditMode && textCell.EditedFormattedValue != null &&
                            textCell.EditedFormattedValue.ToString().Length > 0 &&
                            textCell.FormattedValue.ToString() != textCell.EditedFormattedValue.ToString())
                        {
                            bool bFind = false;
                            string message = textCell.EditedFormattedValue.ToString();
                            for (int i = 0; i < dataGridViewCustomer.Rows.Count; ++i)
                            {
                                if (dataGridViewCustomer.Rows[i].IsNewRow || i == e.RowIndex)
                                {
                                    continue;
                                }
                                if (dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value != null && 
                                    dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value.ToString() == textCell.EditedFormattedValue.ToString())
                                {
                                    bFind = true;
                                    break;
                                }
                                string [] customers = textCell.EditedFormattedValue.ToString().Split(';');
                                foreach (string value in customers)
                                {
                                    if (dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value != null &&
                                    dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value.ToString().Contains(value))
                                    {
                                        bFind = true;
                                        message = value;
                                        break;
                                    }
                                }
                                if (bFind)
                                {
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                MessageBox.Show("客户编码" + message + "已存在", "佰斯特凭证导入接口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Cancel = true;
                                dataGridViewCustomer.CancelEdit();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                MainFormViewModel.CustomerArchives.Clear();
                for (int row = 0; row < dataGridViewCustomer.RowCount; ++row)
                {
                    if (dataGridViewCustomer.Rows[row].IsNewRow)
                    {
                        continue;
                    }
                    CustomerArchive customerArchive = new CustomerArchive();
                    customerArchive.ERPCustomer = dataGridViewCustomer.Rows[row].Cells["ERPCustomer"].Value.ToString();
                    customerArchive.U8Customer = dataGridViewCustomer.Rows[row].Cells["U8Customer"].Value.ToString();
                    customerArchive.U8CustomerName = dataGridViewCustomer.Rows[row].Cells["U8CustomerName"].Value.ToString();
                    MainFormViewModel.CustomerArchives.Add(customerArchive);
                }
                MainFormViewModel.SaveCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("客户档案保存成功!");
        }
        

        #endregion 客户

        #region 部门

        private void InitDepartment()
        {
            dataGridViewDepartment.AutoGenerateColumns = false;
            try
            {
                MainFormViewModel.LoadDepartment();
                if (Department.Departments != null)
                {
                    int rows = Department.Departments.Count;
                    for (int row = 0; row < rows; ++row)
                    {
                        dataGridViewDepartment.Rows.Add();
                        dataGridViewDepartment.Rows[row].Cells["ERPDepartment"].Value = MainFormViewModel.FindDepartmentById(Department.Departments[row].cDepCode);
                        dataGridViewDepartment.Rows[row].Cells["U8Department"].Value = Department.Departments[row].cDepCode;
                        dataGridViewDepartment.Rows[row].Cells["U8DepartmentName"].Value = Department.Departments[row].cDepName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewDepartment_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                    {
                        DataGridViewTextBoxCell textCell = dataGridViewDepartment.Rows[e.RowIndex].Cells["ERPDepartment"]
                            as DataGridViewTextBoxCell;
                        if (textCell != null && textCell.IsInEditMode && textCell.EditedFormattedValue != null &&
                            textCell.EditedFormattedValue.ToString().Length > 0 &&
                            textCell.FormattedValue.ToString() != textCell.EditedFormattedValue.ToString())
                        {
                            bool bFind = false;
                            for (int i = 0; i < dataGridViewDepartment.Rows.Count; ++i)
                            {
                                if (dataGridViewDepartment.Rows[i].IsNewRow || i == e.RowIndex)
                                {
                                    continue;
                                }
                                if (dataGridViewDepartment.Rows[i].Cells["ERPDepartment"].Value != null && dataGridViewDepartment.Rows[i].Cells["ERPDepartment"].Value.ToString() == textCell.EditedFormattedValue.ToString())
                                {
                                    bFind = true;
                                    break;
                                }
                            }
                            if (bFind)
                            {
                                MessageBox.Show("部门编码" + textCell.EditedFormattedValue.ToString() + "已存在", "佰斯特凭证导入接口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                e.Cancel = true;
                                dataGridViewDepartment.CancelEdit();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                MainFormViewModel.DepartmentArchives.Clear();
                for (int row = 0; row < dataGridViewDepartment.RowCount; ++row)
                {
                    if (dataGridViewDepartment.Rows[row].IsNewRow)
                    {
                        continue;
                    }
                    DepartmentArchive departmentArchive = new DepartmentArchive();
                    departmentArchive.ERPDepartment = dataGridViewDepartment.Rows[row].Cells["ERPDepartment"].Value.ToString();
                    departmentArchive.U8Department = dataGridViewDepartment.Rows[row].Cells["U8Department"].Value.ToString();
                    departmentArchive.U8DepartmentName = dataGridViewDepartment.Rows[row].Cells["U8DepartmentName"].Value.ToString();
                    MainFormViewModel.DepartmentArchives.Add(departmentArchive);
                }
                MainFormViewModel.SaveDepartment();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("部门档案保存成功!");
        }

        #endregion 部门

        /// <summary>
        /// 导入凭证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportAccvouch_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> infos = new List<string>();
                foreach (var filePath in openFileDialog.FileNames)
                {
                    MainFormViewModel.ImportAccvouchByExcel(filePath, infos);
                }
                if (infos.Count > 0)
                {
                    InfoForm infoForm = new InfoForm();
                    infoForm.Infos = infos;
                    infoForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("凭证导入成功！");
                }
            }
        }

        private bool FindCustomer(string customerCode,string customerName,List<string> infos)
        {
            bool bFind = false;
            for (int i = 0; i < dataGridViewCustomer.Rows.Count; ++i)
            {
                //如果完全一样
                if (dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value != null && 
                    dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value.ToString() == customerCode)
                {
                    bFind = true;
                    infos.Add(customerCode + " " + customerName + " 客户编码已存在; " + "客户档案导入失败! ");
                    break;
                }
                string [] customerCodes = customerCode.Split(';');
                foreach (string value in customerCodes)
                {
                    if (dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value != null &&
                    dataGridViewCustomer.Rows[i].Cells["ERPCustomer"].Value.ToString().Contains(value))
                    {
                        bFind = true;
                        customerCode = value;
                        infos.Add(customerCode + " " + customerName + " 客户编码已存在; " + "客户档案导入失败! ");
                        break;
                    }
                }
                if (bFind)
                {
                    break;
                }
            }
            return bFind;
        }

        /// <summary>
        /// 导入客户档案(ERP)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportCustom_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> infos = new List<string>();
                try
                {
                    DataSet ds = ExcelHelper.QueryByODBC(openFileDialog.FileName, "Sheet1");
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        bool? isOverride = null;
                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            string customerName = dt.Rows[i]["客户名称"].ToString();
                            string customerCode = dt.Rows[i]["客户编码"].ToString();
                            //判断ERP帐套号与U8账号是否对应
                            if (!MainFormViewModel.AccountArchives.FindByVesselCode(dt.Rows[i]["ERP帐套号"].ToString()))
                            {
                                infos.Add("客户名称：" + customerName + "导入失败! " + "错误提示：对应的ERP帐套号与U8帐套号不匹配");
                                continue;
                            }
                            if (FindCustomer(customerCode, customerName, infos))
                            {
                                continue;
                            }
                            
                            for (int j = 0; j < dataGridViewCustomer.Rows.Count; ++j)
                            {
                                if (dataGridViewCustomer.Rows[j].Cells["U8CustomerName"].Value != null &&
                                    dataGridViewCustomer.Rows[j].Cells["U8CustomerName"].Value.ToString() == customerName)
                                {
                                    if (dataGridViewCustomer.Rows[j].Cells["ERPCustomer"].Value != null &&
                                    dataGridViewCustomer.Rows[j].Cells["ERPCustomer"].Value.ToString().Length > 0)
                                    {
                                        if (isOverride == null)
                                        {
                                            if(MessageBox.Show(customerName + "对应的编码已存在，是否需要替换?\n是:替换原有客户编码 \n否：在原有客户编码后附加新的客户编码",
                                        "佰斯特凭证接口", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {
                                                isOverride = true;
                                            }
                                            else 
                                            { 
                                                isOverride = false;
                                            }
                                        }
                                        if (isOverride == true)
                                        {
                                            dataGridViewCustomer.Rows[j].Cells["ERPCustomer"].Value = customerCode;
                                        }
                                        else
                                        {
                                            dataGridViewCustomer.Rows[j].Cells["ERPCustomer"].Value += ";"+customerCode;
                                        }
                                    }
                                    else
                                    {
                                        dataGridViewCustomer.Rows[j].Cells["ERPCustomer"].Value = customerCode; 
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (infos.Count > 0)
                {
                    InfoForm infoForm = new InfoForm();
                    infoForm.Infos = infos;
                    infoForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("客户档案导入成功！");
                }
            }
        }
        
    }
}
