using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LoginUserControl.Model;
using DataBaseHelper;

namespace LoginUserControl
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            //设置帐套号选项
            comboBoxAccId.Items.Clear();
            foreach (Account account in Account.Accounts)
            {
                comboBoxAccId.Items.Add(account.GetInfo());
            }
            comboBoxAccId.SelectedItem = LoginSettingInfo.AccId;
            textBoxUserName.Text = LoginSettingInfo.User;
        }

        private Account FindAccountById(string id)
        {
            int start = id.IndexOf("[");
            int end = id.IndexOf("]");
            string accid = id.Substring(start+1, end - start-1);
            foreach (Account account in Account.Accounts)
            {
                if (account.cAcc_Id == accid)
                {
                    return account;
                }
            }
            return null;
        }

        //登录，不对用户名和密码进行校验
        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginInfo.UserName = textBoxUserName.Text;
            LoginInfo.UserPwd = textBoxUserPwd.Text;
            LoginInfo.LoginDate = dateTimePickerLogin.Value;

            if (comboBoxAccId.SelectedItem == null || comboBoxAccId.SelectedItem.ToString().Length == 0)
            {
                MessageBox.Show("请选择U8帐套");
                return;
            }
            Account.Current = FindAccountById(comboBoxAccId.SelectedItem.ToString());

            LoginSettingInfo.AccId = Account.Current.GetInfo();
            LoginSettingInfo.User = LoginInfo.UserName;
            if (LoginSettingInfo.User.Length == 0 )
            {
                MessageBox.Show("请输入用户名！");
                return;
            }
            this.Close();
        }
    }


}
