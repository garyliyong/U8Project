using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataBaseHelper;
using LoginUserControl;

namespace BaistInterface.Model
{
    /// <summary>
    /// 部门档案
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门编码
        /// </summary>
        public string cDepCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string cDepName { get; set; }

        private static List<Department> departments = null;

        public static List<Department> Departments
        {
            get
            {
                if (departments == null)
                {
                    List<string> keys = new List<string>();
                    keys.Add("cDepCode");
                    keys.Add("cDepName");

                    DataSet ds = SqlHelper.Query(LoginSettingInfo.SqlConnectionString, "Department", keys, null, "cDepCode");
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            departments = new List<Department>();
                            for (int i = 0; i < dt.Rows.Count; ++i)
                            {
                                Department department = new Department();
                                department.cDepCode = dt.Rows[i]["cDepCode"].ToString();
                                department.cDepName = dt.Rows[i]["cDepName"].ToString();
                                departments.Add(department);
                            }
                        }
                    }
                }
                return departments;
            }
        }
    }
}
