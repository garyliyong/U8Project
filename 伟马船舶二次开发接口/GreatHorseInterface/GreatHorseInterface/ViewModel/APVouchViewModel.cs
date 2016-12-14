using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseHelper;
using System.Data;
using U8ApiInterface;
using LoginUserControl;
using LoginUserControl.Model;

namespace GreatHorseInterface.ViewModel
{
    /// <summary>
    /// 应付款管理
    /// </summary>
    public class APVouchViewModel
    {
        MainForm mainForm = null;

        public APVouchViewModel(MainForm form)
        {
            mainForm = form;
        }

        /// <summary>
        /// 导入excel文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool ImportByExcel(string filePath)
        {
            try
            {
                DataSet ds = ExcelHelper.QueryByODBC(filePath,"Sheet1");
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    //判断船舶号与账号是否对应
                    if (!mainForm.basicarchViewModel.Archives.FindByVesselCode(""))
                    {
                        throw new Exception("导入Excel表格中的船舶号与U8帐套号不对应");
                    }
                    IApiInvoke apiInvoke = new PurbillInvoke();
                    apiInvoke.Login(Account.Current.cAcc_Id, LoginInfo.UserName, LoginInfo.UserPwd, LoginInfo.LoginDate, LoginSettingInfo.Server);
                    //apiInvoke.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "   ,应付发票导入失败!");
            }
            return false;
        }
    }
}
