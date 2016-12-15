using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BaistInterface.Model
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("DepartmentArchive")]
    public class DepartmentArchive
    {
        /// <summary>
        /// ERP部门编码
        /// </summary>
        public string ERPDepartment { get; set; }

        /// <summary>
        /// U8部门编码
        /// </summary>
        public string U8Department { get; set; }

        /// <summary>
        /// U8部门名称
        /// </summary>
        public string U8DepartmentName { get; set; }
    }

    [XmlRoot("DepartmentArchives")]
    public class DepartmentArchives : List<DepartmentArchive>
    {
        /// <summary>
        /// 判断部门编码与U8部门编码是否匹配
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public string FindByDepartmentCode(string departmentCode)
        {
            string u8code = string.Empty;
            for (int index = 0; index < Count; ++index)
            {
                if (this.ElementAt(index).ERPDepartment == departmentCode)
                {
                    u8code = this.ElementAt(index).U8Department;
                    break;
                }
            }
            if (u8code.Length == 0)
            {
                throw new Exception("未找到对应的部门编码");
            }
            return u8code;
        }
    }
}
