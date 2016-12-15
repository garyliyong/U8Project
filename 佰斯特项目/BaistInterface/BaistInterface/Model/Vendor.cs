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
    /// 供应商档案
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string cVenCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string cVenName { get; set; }

        private static List<Vendor> vendors = null;

        public static List<Vendor> Vendors
        {
            get
            {
                if (vendors == null)
                {
                    List<string> keys = new List<string>();
                    keys.Add("cVenCode");
                    keys.Add("cVenName");

                    DataSet ds = SqlHelper.Query(LoginSettingInfo.SqlConnectionString, "Vendor", keys, null, "cVenCode");
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            vendors = new List<Vendor>();
                            for (int i = 0; i < dt.Rows.Count; ++i)
                            {
                                Vendor vendor = new Vendor();
                                vendor.cVenCode = dt.Rows[i]["cVenCode"].ToString();
                                vendor.cVenName = dt.Rows[i]["cVenName"].ToString();
                                vendors.Add(vendor);
                            }
                        }
                    }
                }
                return vendors;
            }
        }
    }
}
