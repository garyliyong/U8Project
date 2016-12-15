using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseHelper;
using LoginUserControl;
using System.Data;

namespace BaistInterface.Model
{
    /// <summary>
    /// 总账（U8数据库表 GL_accvouch）
    /// </summary>
    public class Accvouch
    {
        public Accvouch()
        {
            md_f = mc_f = nfrat = 0.0;
            idoc = nd_s = nc_s = 0;
            ibook = "0";
            //csettle = "5";//缺省结算方式现金
        }

        /// <summary>
        /// 会计期间
        /// </summary>
        public string iperiod
        {
            get
            {
                return dbill_date.Month.ToString();
            }
        }

        /// <summary>
        /// 凭证字类别(dsign)
        /// </summary>
        public string csign { get; set; }

        /// <summary>
        /// 凭证类别排序号
        /// </summary>
        public string isignseq 
        {
            get
            {
                string isignseq = string.Empty;
                Dictionary<string,string> keys = new Dictionary<string,string>();
                keys.Add("csign",csign);
                DataSet ds =  SqlHelper.Query(LoginSettingInfo.SqlConnectionString, "dsign", "isignseq", keys, null);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    isignseq = ds.Tables[0].Rows[0]["isignseq"].ToString();
                }
                return isignseq;
            }
        }

        /// <summary>
        /// 凭证编号
        /// </summary>
        public int ino_id { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int inid { get; set; }

        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime dbill_date { get; set; }

        /// <summary>
        /// 付单据数
        /// </summary>
        public int idoc { get; set; }

        /// <summary>
        /// 记账标志
        /// </summary>
        public string ibook { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string cdigest { get; set; }

        /// <summary>
        /// 科目编码
        /// </summary>
        public string ccode { get; set; }

        /// <summary>
        /// 借方
        /// </summary>
        public double md { get; set; }

        /// <summary>
        /// 贷方
        /// </summary>
        public double mc { get; set; }

        /// <summary>
        /// 外币借方
        /// </summary>
        public double md_f { get; set; }

        /// <summary>
        /// 外币贷方
        /// </summary>
        public double mc_f { get; set; }

        /// <summary>
        /// 汇率
        /// </summary>
        public double nfrat { get; set; }

        /// <summary>
        /// 数量借方
        /// </summary>
        public int nd_s { get; set; }

        /// <summary>
        /// 数量贷方
        /// </summary>
        public int nc_s { get; set; }

        /// <summary>
        /// 结算方式编码
        /// </summary>
        public string csettle { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string cdept_id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        //public string cdept_name { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string csup_id { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        //public string csup_name { get; set; }

        /// <summary>
        /// 对方科目编码
        /// </summary>
        public string ccode_equal
        {
            get
            {
                return ccode;
            }
        }

        /// <summary>
        /// 凭证的会计年度
        /// </summary>
        public string iyear
        {
            get
            {
                return dbill_date.Year.ToString();
            }
        }

        /// <summary>
        /// 包括年度的会计期间
        /// </summary>
        public string iYPeriod
        {
            get
            {
                return iyear + dbill_date.Month.ToString();
            }
        }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string citem_id { get; set; }

        /// <summary>
        /// 项目大类编码
        /// </summary>
        public string citem_class { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string cbill { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public string cname { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public string ccus_id  { get; set; }

        /// <summary>
        /// 职员编码
        /// </summary>
        public string cperson_id { get; set; }

        /// <summary>
        /// ERP凭证号
        /// </summary>
        public string cDefine1 { get; set; }


        /// <summary>
        /// 是否已经导入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Find(string id)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("cDefine1", id);
            DataSet ds = SqlHelper.Query(LoginSettingInfo.SqlConnectionString, "GL_accvouch", "cDefine1", keys,null);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
