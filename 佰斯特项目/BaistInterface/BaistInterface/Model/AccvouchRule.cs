using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaistInterface.Model
{
    /// <summary>
    /// 凭证导入规则
    /// </summary>
    public class AccvouchRule
    {
        /// <summary>
        /// 摘要
        /// </summary>
        public string cdigest { get; set; }

        /// <summary>
        /// 科目编码
        /// </summary>
        public string ccode { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string ccodename { get; set; }

        /// <summary>
        /// 借/贷
        /// </summary>
        public string cmd { get; set; }

        /// <summary>
        /// 结算方式编码
        /// </summary>
        public string csettle { get; set; }

        /// <summary>
        /// 结算方式名称
        /// </summary>
        public string csettlename { get; set; }

        /// <summary>
        /// 凭证类别
        /// </summary>
        public string csign { get; set; }

        //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 取数规则
        /// </summary>
        public string NumRule { get; set; }

        /// <summary>
        /// 根据取数规则获取列信息
        /// </summary>
        /// <returns></returns>
        public List<int> GetNum()
        {
            List<int> nums = new List<int>();
            string[] strs = NumRule.Split(',');
            foreach (string str in strs)
            {
                nums.Add(int.Parse(str));
            }
            return nums;
        }
    }
}
