using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BaistInterface.Model
{
    /// <summary>
    /// 供应商档案
    /// </summary>
    [XmlRoot("VendorArchive")]
    public class VendorArchive
    {
        /// <summary>
        /// ERP供应商编码
        /// </summary>
        public string ERPVendor { get; set; }

        /// <summary>
        /// U8供应商编码
        /// </summary>
        public string U8Vendor { get; set; }

        /// <summary>
        /// U8供应商名称
        /// </summary>
        public string U8VendorName { get; set; }
    }

    [XmlRoot("VendorArchives")]
    public class VendorArchives : List<VendorArchive>
    {
        /// <summary>
        /// 判断ERP供应商编码与U8供应商编码是否匹配
        /// </summary>
        /// <param name="vendorCode"></param>
        /// <returns></returns>
        public string FindByVendorCode(string vendorCode)
        {
            string u8code = string.Empty;
            for (int index = 0; index < Count; ++index)
            {
                if (this.ElementAt(index).ERPVendor == vendorCode)
                {
                    u8code = this.ElementAt(index).U8Vendor;
                    break;
                }
            }
            if (u8code.Length == 0)
            {
                throw new Exception("未找到对应的供应商编码");
            }
            return u8code;
        }
    }
}
