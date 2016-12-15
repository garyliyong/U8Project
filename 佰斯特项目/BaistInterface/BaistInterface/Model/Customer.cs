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
    /// 客户
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 客户编码
        /// </summary>
        public string cCusCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string cCusName { get; set; }

        private static List<Customer> customers = null;

        public static List<Customer> Customers
        {
            get
            {
                if (customers == null)
                {
                    List<string> keys = new List<string>();
                    keys.Add("cCusCode");
                    keys.Add("cCusName");

                    DataSet ds = SqlHelper.Query(LoginSettingInfo.SqlConnectionString, "Customer", keys, null, "cCusCode");
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            customers = new List<Customer>();
                            for (int i = 0; i < dt.Rows.Count; ++i)
                            {
                                Customer customer = new Customer();
                                customer.cCusCode = dt.Rows[i]["cCusCode"].ToString();
                                customer.cCusName = dt.Rows[i]["cCusName"].ToString();
                                customers.Add(customer);
                            }
                        }
                    }
                }
                return customers;
            }
        }
    }
}
