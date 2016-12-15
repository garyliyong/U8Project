using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BaistInterface.Model
{
    /// <summary>
    /// 会计科目档案
    /// </summary>
    [XmlRoot("CodeArchive")]
    public class CodeArchive
    {
        /// <summary>
        /// ERP会计科目编号
        /// </summary>
        public string ERPCode { get; set; }

        /// <summary>
        /// U8会计科目编号
        /// </summary>
        public string U8Code{ get; set; }

        /// <summary>
        /// U8会计科目编号名称
        /// </summary>
        public string U8CodeName { get; set; }
    }

    /// <summary>
    /// 会计科目档案列表
    /// </summary>
    [XmlRoot("CodeArchives")]
    public class CodeArchives : List<CodeArchive>
    {
        /// <summary>
        /// 判断ERP会计科目编码与会计科目档案信息是否匹配
        /// </summary>
        /// <param name="vesselCode"></param>
        /// <returns></returns>
        public string FindByCode(string code)
        {
            string u8code = string.Empty;
            for (int index = 0; index < Count; ++index)
            {
                if (this.ElementAt(index).ERPCode == code)
                {
                    u8code = this.ElementAt(index).U8Code;
                    break;
                }
            }
            if (u8code.Length == 0)
            {
                throw new Exception("未找到对应的科目编码");
            }
            return u8code;
        }
    }
}
