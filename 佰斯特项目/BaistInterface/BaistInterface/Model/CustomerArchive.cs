using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BaistInterface.Model
{
    /// <summary>
    /// 客户档案
    /// </summary>
    [XmlRoot("CustomerArchive")]
    public class CustomerArchive
    {
        /// <summary>
        /// ERP客户编码
        /// </summary>
        public string ERPCustomer { get; set; }

        /// <summary>
        /// U8客户编码
        /// </summary>
        public string U8Customer { get; set; }

        /// <summary>
        /// U8客户名称
        /// </summary>
        public string U8CustomerName { get; set; }
    }

    [XmlRoot("CustomerArchives")]
    public class CustomerArchives : List<CustomerArchive>
    {
        /// <summary>
        /// 判断ERP客户编码与U8客户编码是否匹配
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public string FindByCustomerCode(string customerCode)
        {
            //佰斯特公司客户编码比较特殊，多个客户编码对应同一个客户
            string u8code = string.Empty;
            for (int index = 0; index < Count; ++index)
            {
                if (this.ElementAt(index).ERPCustomer.Contains(customerCode))
                {
                    u8code = this.ElementAt(index).U8Customer;
                    break;
                }
            }
            if (u8code.Length == 0)
            {
                throw new Exception("未找到对应的客户编码");
            }
            return u8code;
        }
    }
}
