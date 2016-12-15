using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaistInterface.Model;
using DataBaseHelper;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using LoginUserControl;
using LoginUserControl.Model;
using System.IO;

namespace BaistInterface.ViewModel
{
    public class MainFormViewModel
    {
        public MainFormViewModel()
        {
            
        }

        public string BaseDirectory 
        {
            get
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory + Account.Current.cAcc_Id;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                return directory;
            }
        }

        #region 帐套

        private AccountArchives accountArchives = null;

        public AccountArchives AccountArchives
        {
            get
            {
                if (accountArchives == null)
                {
                    accountArchives = new AccountArchives();
                }
                return accountArchives;
            }
        }

        private string AccountArchivePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"\AccountArchive.xml";
            }
        }

        public string FindAccountById(string id)
        {
            string vesselCode = string.Empty;
            foreach (AccountArchive accountArchive in AccountArchives)
            {
                if (accountArchive.U8Account == id)
                {
                    vesselCode = accountArchive.VesselCode;
                    break;
                }
            }
            return vesselCode;
        }

        /// <summary>
        /// 加载帐套档案
        /// </summary>
        public void LoadAccount()
        {
            if (AccountArchives != null)
            {
                AccountArchives.Clear();
            }
            XmlReader reader = new XmlTextReader(AccountArchivePath);
            try
            {
                AccountArchives accountArchives = XMLHelper.UnSerializer(reader, typeof(AccountArchives)) as AccountArchives;
                if (accountArchives != null)
                {
                    foreach (var value in accountArchives)
                    {
                        AccountArchives.Add(value);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// 保存帐套档案
        /// </summary>
        public void SaveAccount()
        {
            XmlWriter writer = new XmlTextWriter(AccountArchivePath, Encoding.UTF8);
            try
            {
                XMLHelper.Serializer(writer, AccountArchives, typeof(AccountArchives));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer.Close();
            }
        }

        #endregion 帐套


        #region 会计科目

        private CodeArchives codeArchives = null;

        public CodeArchives CodeArchives
        {
            get
            {
                if (codeArchives == null)
                {
                    codeArchives = new CodeArchives();
                }
                return codeArchives;
            }
        }

        public string FindCodeById(string id)
        {
            string erpId = string.Empty;
            foreach (CodeArchive codeArchive in CodeArchives)
            {
                if (codeArchive.U8Code == id)
                {
                    erpId = codeArchive.ERPCode;
                    break;
                }
            }
            return erpId;
        }

        private string CodeArchivePath
        {
            get
            {
                return BaseDirectory + @"\CodeArchive.xml";
            }
        }

        /// <summary>
        /// 加载会计科目编码档案
        /// </summary>
        public void LoadCode()
        {
            if (CodeArchives != null)
            {
                CodeArchives.Clear();
            }
            XmlReader reader = new XmlTextReader(CodeArchivePath);
            try
            {
                CodeArchives codeArchives = XMLHelper.UnSerializer(reader, typeof(CodeArchives)) as CodeArchives;
                if (codeArchives != null)
                {
                    foreach (var value in codeArchives)
                    {
                        CodeArchives.Add(value);
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// 保存会计科目档案
        /// </summary>
        public void SaveCode()
        {
            XmlWriter writer = new XmlTextWriter(CodeArchivePath, Encoding.UTF8);
            try
            {
                XMLHelper.Serializer(writer, CodeArchives, typeof(CodeArchives));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer.Close();
            }
        }


        #endregion 会计科目


        #region 供应商

        private VendorArchives vendorArchives = null;

        public VendorArchives VendorArchives
        {
            get
            {
                if (vendorArchives == null)
                {
                    vendorArchives = new VendorArchives();
                }
                return vendorArchives;
            }
        }


        private string VendorArchivePath
        {
            get
            {
                return BaseDirectory + @"\VendorArchive.xml";
            }
        }

        public string FindVendorById(string id)
        {
            string erpId = string.Empty;
            foreach (VendorArchive vendorArchive in VendorArchives)
            {
                if (vendorArchive.U8Vendor == id)
                {
                    erpId = vendorArchive.ERPVendor;
                    break;
                }
            }
            return erpId;
        }

        public void LoadVendor()
        {
            if (VendorArchives != null)
            {
                VendorArchives.Clear();
            }
            XmlReader reader = new XmlTextReader(VendorArchivePath);
            try
            {
                VendorArchives vendorArchives = XMLHelper.UnSerializer(reader, typeof(VendorArchives)) as VendorArchives;
                if (vendorArchives != null)
                {
                    foreach (var value in vendorArchives)
                    {
                        VendorArchives.Add(value);
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        public void SaveVendor()
        {
            XmlWriter writer = new XmlTextWriter(VendorArchivePath, Encoding.UTF8);
            try
            {
                XMLHelper.Serializer(writer, VendorArchives, typeof(VendorArchives));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer.Close();
            }
        }

        #endregion 供应商

        #region 部门

        private DepartmentArchives departmentArchives = null;

        public DepartmentArchives DepartmentArchives
        {
            get
            {
                if (departmentArchives == null)
                {
                    departmentArchives = new DepartmentArchives();
                }
                return departmentArchives;
            }
        }


        private string DepartmentArchivePath
        {
            get
            {
                return BaseDirectory + @"\DepartmentArchive.xml";
            }
        }

        public string FindDepartmentById(string id)
        {
            string erpId = string.Empty;
            foreach (DepartmentArchive departmentArchive in DepartmentArchives)
            {
                if (departmentArchive.U8Department == id)
                {
                    erpId = departmentArchive.ERPDepartment;
                    break;
                }
            }
            return erpId;
        }

        public void LoadDepartment()
        {
            if (DepartmentArchives != null)
            {
                DepartmentArchives.Clear();
            }
            XmlReader reader = new XmlTextReader(DepartmentArchivePath);
            try
            {
                DepartmentArchives departmentArchives = XMLHelper.UnSerializer(reader, typeof(DepartmentArchives)) as DepartmentArchives;
                if (departmentArchives != null)
                {
                    foreach (var value in departmentArchives)
                    {
                        DepartmentArchives.Add(value);
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        public void SaveDepartment()
        {
            XmlWriter writer = new XmlTextWriter(DepartmentArchivePath, Encoding.UTF8);
            try
            {
                XMLHelper.Serializer(writer, DepartmentArchives, typeof(DepartmentArchives));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer.Close();
            }
        }
        #endregion 部门


        #region 客户

        private CustomerArchives customerArchives = null;

        public CustomerArchives CustomerArchives
        {
            get
            {
                if (customerArchives == null)
                {
                    customerArchives = new CustomerArchives();
                }
                return customerArchives;
            }
        }


        private string CustomerArchivePath
        {
            get
            {
                return BaseDirectory + @"\CustomerArchive.xml";
            }
        }

        public string FindCustomerById(string id)
        {
            string erpId = string.Empty;
            foreach (CustomerArchive customerArchive in CustomerArchives)
            {
                if (customerArchive.U8Customer == id)
                {
                    erpId = customerArchive.ERPCustomer;
                    break;
                }
            }
            return erpId;
        }

        public void LoadCustomer()
        {
            if (CustomerArchives != null)
            {
                CustomerArchives.Clear();
            }
            XmlReader reader = new XmlTextReader(CustomerArchivePath);
            try
            {
                CustomerArchives customerArchives = XMLHelper.UnSerializer(reader, typeof(CustomerArchives)) as CustomerArchives;
                if (customerArchives != null)
                {
                    foreach (var value in customerArchives)
                    {
                        CustomerArchives.Add(value);
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        public void SaveCustomer()
        {
            XmlWriter writer = new XmlTextWriter(CustomerArchivePath, Encoding.UTF8);
            try
            {
                XMLHelper.Serializer(writer, CustomerArchives, typeof(CustomerArchives));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer.Close();
            }
        }
        #endregion 客户


        private void ImportAccvochToU8(Accvouch accvouch, List<string> infos)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("iperiod", accvouch.iperiod);
            values.Add("csign", accvouch.csign);
            values.Add("isignseq", accvouch.isignseq);
            values.Add("ino_id", accvouch.ino_id.ToString());
            values.Add("inid", accvouch.inid.ToString());
            values.Add("dbill_date", accvouch.dbill_date.ToString());
            values.Add("idoc", accvouch.idoc.ToString());
            values.Add("ibook", accvouch.ibook);
            values.Add("cdigest", accvouch.cdigest);
            values.Add("ccode", accvouch.ccode);
            values.Add("md", accvouch.md.ToString());
            values.Add("mc", accvouch.mc.ToString());
            values.Add("md_f", accvouch.md_f.ToString());
            values.Add("mc_f", accvouch.mc_f.ToString());
            values.Add("nfrat", accvouch.nfrat.ToString());
            values.Add("nd_s", accvouch.nd_s.ToString());
            values.Add("nc_s", accvouch.nc_s.ToString());
            if (accvouch.csettle != null && accvouch.csettle.ToString().Length > 0)
            {
                values.Add("csettle", accvouch.csettle);
            }
            if (accvouch.cdept_id != null && accvouch.cdept_id.Length > 0)
            {
                values.Add("cdept_id", accvouch.cdept_id);
            }
            if (accvouch.csup_id != null && accvouch.csup_id.Length > 0)
            {
                values.Add("csup_id", accvouch.csup_id);
            }
            values.Add("ccode_equal", accvouch.ccode_equal);
            values.Add("iyear", accvouch.iyear);
            values.Add("iYPeriod", accvouch.iYPeriod);
            if (accvouch.citem_id != null && accvouch.citem_id.Length > 0)
            {
                values.Add("citem_id", accvouch.citem_id);
            }
            if (accvouch.citem_class != null && accvouch.citem_class.Length > 0)
            {
                values.Add("citem_class", accvouch.citem_class);
            }
            values.Add("cbill", accvouch.cbill);
            if (accvouch.cname != null && accvouch.cname.Length > 0)
            {
                values.Add("cname", accvouch.cname);
            }
            if (accvouch.ccus_id != null && accvouch.ccus_id.Length > 0)
            {
                values.Add("ccus_id", accvouch.ccus_id);
            }
            if (accvouch.cperson_id != null && accvouch.cperson_id.Length > 0)
            {
                values.Add("cperson_id", accvouch.cperson_id);
            }
            values.Add("cDefine1", accvouch.cDefine1);
            SqlHelper.Insert(LoginSettingInfo.SqlConnectionString, "GL_accvouch", values);
        }

        private void ImportAccvoch(DataRow[] dataRow, string filePath,List<string> infos)
        {
            string id = string.Empty;
            try
            {
                int ino_id  = 0;
                Dictionary<string, string> keys = new Dictionary<string, string>();
                keys.Add("csign", dataRow[0]["凭证类别"].ToString());
                keys.Add("ibook", "0");
                DataSet ds = SqlHelper.Query(LoginSettingInfo.SqlConnectionString, "GL_accvouch", "ino_id", keys, "ino_id");
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    ino_id = 1;
                }
                else
                {
                    DataTable dt = ds.Tables[0];
                    ino_id = int.Parse(dt.Rows[dt.Rows.Count - 1]["ino_id"].ToString()) + 1;
                }
                int count = dataRow.Length;
                for (int i = 0; i < count; ++i)
                {
                    Accvouch accvouch = new Accvouch();
                    accvouch.ino_id = ino_id;
                    accvouch.cDefine1 = dataRow[i]["凭证ID"].ToString();
                    id = accvouch.cDefine1;
                    accvouch.csign = dataRow[i]["凭证类别"].ToString();
                    accvouch.cdigest = dataRow[i]["摘要"].ToString();
                    accvouch.ccode = CodeArchives.FindByCode(dataRow[i]["科目编码"].ToString());
                    if (dataRow[i]["借方金额"] != null && dataRow[i]["借方金额"].ToString().Length > 0)
                    {
                        accvouch.md = double.Parse(dataRow[i]["借方金额"].ToString());
                    }
                    if (dataRow[i]["贷方金额"] != null && dataRow[i]["贷方金额"].ToString().Length > 0)
                    {
                        accvouch.mc = double.Parse(dataRow[i]["贷方金额"].ToString());
                    }
                    if (dataRow[i]["所附单据数"] != null && dataRow[i]["所附单据数"].ToString().Length > 0)
                    {
                        accvouch.idoc = int.Parse(dataRow[i]["所附单据数"].ToString());
                    }
                    if (dataRow[i]["结算方式编码"] != null && dataRow[i]["结算方式编码"].ToString().Length > 0)
                    {
                        accvouch.csettle = dataRow[i]["结算方式编码"].ToString();
                    }
                    if (dataRow[i]["部门编码"] != null && dataRow[i]["部门编码"].ToString().Length > 0)
                    {
                        accvouch.cdept_id = DepartmentArchives.FindByDepartmentCode(dataRow[i]["部门编码"].ToString());
                    }
                    if (dataRow[i]["职员编码"] != null && dataRow[i]["职员编码"].ToString().Length> 0)
                    {
                        accvouch.cperson_id = dataRow[i]["职员编码"].ToString();
                    }
                    if (dataRow[i]["客户编码"] != null && dataRow[i]["客户编码"].ToString().Length > 0)
                    {
                        accvouch.ccus_id = CustomerArchives.FindByCustomerCode(dataRow[i]["客户编码"].ToString());
                    }
                    if (dataRow[i]["供应商编码"] != null && dataRow[i]["供应商编码"].ToString().Length > 0)
                    {
                        accvouch.csup_id = VendorArchives.FindByVendorCode(dataRow[i]["供应商编码"].ToString());
                    }
                    if (dataRow[i]["项目大类编码"] != null && dataRow[i]["项目大类编码"].ToString().Length > 0)
                    {
                        accvouch.citem_class = dataRow[i]["项目大类编码"].ToString();
                    }
                    if (dataRow[i]["项目编码"] != null && dataRow[i]["项目编码"].ToString().Length > 0)
                    {
                        accvouch.citem_id = dataRow[i]["项目编码"].ToString();
                    }
                    if (dataRow[i]["业务员"] != null && dataRow[i]["业务员"].ToString().Length > 0)
                    {
                        accvouch.cname = dataRow[i]["业务员"].ToString();
                    }
                    accvouch.dbill_date = DateTime.Parse(LoginInfo.LoginDate.ToShortDateString());
                    accvouch.cbill = LoginInfo.UserName;
                    accvouch.inid = i + 1;
                    ImportAccvochToU8(accvouch, infos);
                }
            }
            catch (Exception ex)
            {
                infos.Add("凭证ID：" + id + "导入失败!" + "错误提示：" + ex.Message);
                Dictionary<string,string> wheres = new Dictionary<string,string>();
                wheres.Add("cDefine1", id);
                SqlHelper.Delete(LoginSettingInfo.SqlConnectionString, "GL_accvouch", wheres);
                Dictionary<string,string> values = new Dictionary<string,string>();
                values.Add("备注",ex.Message);
                wheres.Clear();
                wheres.Add("凭证ID",id);
                ExcelHelper.UpdateByODBC(filePath, "Sheet1", values, wheres);
            }
        }

        /// <summary>
        /// 导入凭证
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void ImportAccvouchByExcel(string filePath,List<string> infos)
        {
            try
            {
                DataSet ds = ExcelHelper.QueryByODBC(filePath, "Sheet1");
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    string curId = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        if (dt.Rows[i]["凭证ID"].ToString() != curId)
                        {
                            curId = dt.Rows[i]["凭证ID"].ToString();
                            if (Accvouch.Find(curId))
                            {
                                infos.Add("凭证ID：" + curId + "导入失败！错误提示：该凭证ID已导入");
                                continue;
                            }
                            //判断ERP帐套号与U8账号是否对应
                            if (!AccountArchives.FindByVesselCode(dt.Rows[i]["ERP帐套号"].ToString()))
                            {
                                infos.Add("凭证ID：" + curId + "导入失败! " + "错误提示：对应的ERP帐套号与U8帐套号不匹配");
                                continue;
                            }
                            ImportAccvoch(dt.Select("凭证ID=" + "'" + curId + "'"), filePath, infos);
                        }
                    }
                }
                else
                {
                    infos.Add("读取Excel文件失败,请检查该文件是否已打开或表Sheet1是否存在!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ",凭证导入失败!");
                infos.Add(ex.Message);
            }
        }
    }
}
