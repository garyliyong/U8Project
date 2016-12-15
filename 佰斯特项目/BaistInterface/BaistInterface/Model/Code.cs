using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseHelper;
using LoginUserControl;
using LoginUserControl.Model;
using System.Data;

namespace BaistInterface.Model
{
    /// <summary>
    /// U8会计科目表
    /// </summary>
    public class Code
    {
        /// <summary>
        /// 会计科目编码
        /// </summary>
        public string ccode { get; set; }

        /// <summary>
        /// 会计科目名称
        /// </summary>
        public string ccode_Name { get; set; }

        private static List<Code> codes = null;

        public static List<Code> Codes
        {
            get
            {
                if (codes == null)
                {
                    List<string> keys = new List<string>();
                    keys.Add("ccode");
                    keys.Add("ccode_Name");

                    Dictionary<string,string> wheres = new Dictionary<string,string>();
                    wheres.Add("iyear", LoginInfo.LoginDate.Year.ToString());
                    DataSet ds =  SqlHelper.Query(LoginSettingInfo.SqlConnectionString, "code", keys, wheres, "ccode");
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            codes = new List<Code>();
                            for(int i = 0;i < dt.Rows.Count;++i)
                            {
                                Code code = new Code();
                                code.ccode = dt.Rows[i]["ccode"].ToString();
                                code.ccode_Name = dt.Rows[i]["ccode_Name"].ToString();
                                codes.Add(code);
                            }
                        }
                    }
                }
                return codes;
            }
        }
    }
}
