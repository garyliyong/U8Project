using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHYSInterface.退货单
{
    //退货单主表
    public class DispatchList
    {
        public DispatchList()
        {
            cSTCode = "01";
            cVouchType = "05";
            cexch_name = "人民币";
            cBusType = "普通销售";
            bReturnFlag = "1";
            dcreatesystime = dDate = DateTime.Today.ToShortDateString();
            iVTid = "75";
            iTaxRate = "17";
            iExchRate = "1";
        }

        /// <summary>
        /// 退货单主表标识
        /// </summary>
        public string DLID { get; set; }

        /// <summary>
        /// 退货单号
        /// </summary>
        public string cDLCode { get; set; }

        /// <summary>
        /// 表单类型(默认)
        /// </summary>
        public string cVouchType { get; set; }

        /// <summary>
        /// 销售类型编码(默认)
        /// </summary>
        public string cSTCode { get; set; }

        /// <summary>
        /// 制单日期（默认）
        /// </summary>
        public string dDate { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string cDepCode { get; set; }

        /// <summary>
        /// 业务员编码
        /// </summary>
        public string cPersonCode { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public string cCusCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string cCusName { get; set; }

        /// <summary>
        /// 币种(默认)
        /// </summary>
        public string cexch_name { get; set; }

        /// <summary>
        /// 汇率(默认)
        /// </summary>
        public string iExchRate { get; set; }

        /// <summary>
        /// 表头税率(默认)
        /// </summary>
        public string iTaxRate { get; set; }

        /// <summary>
        /// 退货标志(默认)
        /// </summary>
        public string bReturnFlag { get; set; }

        /// <summary>
        /// 制单日期(默认)
        /// </summary>
        public string dcreatesystime { get; set; }

        /// <summary>
        /// 单据模板号(默认)
        /// </summary>
        public string iVTid { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        //public string ufts { get; set; }

        /// <summary>
        /// 业务类型(默认)
        /// </summary>
        public string cBusType { get; set; }

        /// <summary>
        /// 医院编码
        /// </summary>
        public string cDefine11 { get; set; }

        /// <summary>
        /// 配送点编码
        /// </summary>
        public string cDefine12 { get; set; }
        
        /// <summary>
        /// 配送点地址
        /// </summary>
        public string cDefine13 { get; set; }

        /// <summary>
        /// 计划单号
        /// </summary>
        public string cDefine14 { get; set; }

        /// <summary>
        /// 是否需要开票(默认) 
        /// </summary>
        public string bneedbill { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string cMaker { get; set; }
        /// <summary>
        /// 开票单位编码
        /// </summary>
        //public string cinvoicecompany { get; set; }    

        public void WriteToSql()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("DLID", DLID);
            values.Add("cDLCode", cDLCode);
            values.Add("cVouchType", cVouchType);
            values.Add("cSTCode", cSTCode);
            values.Add("dDate", dDate);
            values.Add("cDepCode", cDepCode);
            values.Add("cPersonCode", cPersonCode);
            values.Add("cCusCode", cCusCode);
            values.Add("cCusName", cCusName);
            values.Add("cexch_name", cexch_name);
            values.Add("iExchRate", iExchRate);
            values.Add("iTaxRate", iTaxRate);
            values.Add("bReturnFlag", bReturnFlag);
            values.Add("dcreatesystime", dcreatesystime);
            values.Add("iVTid", iVTid);
            values.Add("cBusType", cBusType);
            values.Add("cDefine11", cDefine11);
            values.Add("cDefine12", cDefine12);
            values.Add("cDefine13", cDefine13);
            values.Add("cDefine14", cDefine14);
            values.Add("bneedbill", bneedbill);
            values.Add("cMaker", cMaker);
            SqlHelper.Insert(Program.ConnectionString, "DispatchList", values);
            values.Clear();
            values.Add("iFatherId", (OrderInfo.GetHeadID() + 1).ToString());
            Dictionary<string, string> wheres = new Dictionary<string, string>();
            wheres.Add("cVouchType", "DISPATCH");
            SqlHelper.Update(OrderInfo.ConnString, "ua_identity", values, wheres);
        }
    }

    //退货单子表
    public class DispatchLists
    {
        public DispatchLists()
        {
            KL = KL2 = "100";
            bGsp = "0";
            iTaxRate = "17";
            fcusminprice = "0";
        }
        /// <summary>
        /// 发货退货单子表标识
        /// </summary>
        public string AutoID { get; set; }
        /// <summary>
        /// 发货退货单主表标识
        /// </summary>
        public string DLID { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string cWhCode { get; set; }
        /// <summary>
        /// 存货编码
        /// </summary>
        public string cInvCode { get; set; }
        /// <summary>
        /// 存货名称
        /// </summary>
        public string cInvName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string iQuantity { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string iNum { get; set; }
        /// <summary>
        /// 报价
        /// </summary>
        public string iQuotedPrice { get; set; }
        /// <summary>
        /// 无税单价
        /// </summary>
        public string iUnitPrice { get; set; }
        /// <summary>
        /// 含税单价
        /// </summary>
        public string iTaxUnitPrice { get; set; }
        /// <summary>
        /// 无税金额
        /// </summary>
        public string iMoney { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        public string iTax { get; set; }
        /// <summary>
        /// 原币价税合计
        /// </summary>
        public string iSum { get; set; }
        /// <summary>
        /// 本币无税单价
        /// </summary>
        public string iNatUnitPrice { get; set; }
        /// <summary>
        /// 本币金额
        /// </summary>
        public string iNatMoney { get; set; }
        /// <summary>
        /// 本币税额
        /// </summary>
        public string iNatTax { get; set; }
        /// <summary>
        /// 本币价税合计
        /// </summary>
        public string iNatSum { get; set; }
        //public string iSettleNum { get; set; }
        //public string iSettleQuantity { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string cBatch { get; set; }
        /// <summary>
        /// 发货退货单子表标识2
        /// </summary>
        public string iDLsID { get; set; }
        /// <summary>
        /// 扣率(默认)
        /// </summary>
        public string KL { get; set; }
        /// <summary>
        /// 扣率2(默认)
        /// </summary>
        public string KL2 { get; set; }
        /// <summary>
        /// 税率(默认)
        /// </summary>
        public string iTaxRate { get; set; }
        /// <summary>
        /// 考核成本
        /// </summary>
        //public string cDefine26 { get; set; }
        /// <summary>
        /// 统编代码
        /// </summary>
        public string cDefine28 { get; set; }
        /// <summary>
        /// 退货单编号
        /// </summary>
        public string cDefine29 { get; set; }
        /// <summary>
        /// 采购计量单位
        /// </summary>
        public string cDefine30 { get; set; }

        //计量单位编码
        public string cUnitID { get; set; }

        /// <summary>
        /// 医院配送点编码
        /// </summary>
        public string cDefine32 { get; set; }
        /// <summary>
        /// 配送地址
        /// </summary>
        public string cDefine33 { get; set; }
        /// <summary>
        ///生产日期
        /// </summary>
        public string dMDate { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public string dvDate { get; set; }
        
        /// <summary>
        /// 是否质量保证(默认)
        /// </summary>
        public string bGsp { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public string irowno { get; set; }
        /// <summary>
        /// 有效期计算项 
        /// </summary>
        //public string dExpirationdate { get; set; }
        /// <summary>
        /// 有效期至
        /// </summary>
        public string cExpirationdate { get; set; }

        public string iSettleQuantity { get; set; }

        public string iSettleNum { get; set; }

        /// <summary>
        /// 客户最低售价
        /// </summary>
        public string fcusminprice { get; set; }

        public void WriteToSql()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            //values.Add("AutoID", AutoID);
            values.Add("DLID", DLID);
            values.Add("cWhCode", cWhCode);
            values.Add("cInvCode", cInvCode);
            values.Add("cInvName", cInvName);
            values.Add("iQuantity", iQuantity);
            values.Add("iNum", iNum);
            values.Add("iQuotedPrice", iQuotedPrice);
            values.Add("iUnitPrice", iUnitPrice);
            values.Add("iTaxUnitPrice", iTaxUnitPrice);
            values.Add("iMoney", iMoney);
            values.Add("iTax", iTax);
            values.Add("iSum", iSum);
            values.Add("iNatUnitPrice", iNatUnitPrice);
            values.Add("iNatMoney", iNatMoney);
            values.Add("iNatTax", iNatTax);
            values.Add("iNatSum", iNatSum);
            values.Add("cBatch", cBatch);
            values.Add("iDLsID", iDLsID);
            values.Add("KL", KL);
            values.Add("KL2", KL2);
            values.Add("iTaxRate", iTaxRate);
            values.Add("cDefine28", cDefine28);
            values.Add("cDefine29", cDefine29);
            values.Add("cDefine30", cDefine30);
            values.Add("cDefine32", cDefine32);
            values.Add("cDefine33", cDefine33);
            values.Add("dMDate", dMDate);
            values.Add("dvDate", dvDate);
            values.Add("bGsp", bGsp);
            values.Add("irowno", irowno);
            values.Add("fcusminprice", fcusminprice);
            values.Add("cExpirationdate", cExpirationdate);
            values.Add("iSettleNum", iSettleNum);
            values.Add("iSettleQuantity", iSettleQuantity);
            values.Add("cUnitID", cUnitID);
            SqlHelper.Insert(Program.ConnectionString, "DispatchLists", values);
            values.Clear();
            values.Add("iChildId", (OrderInfo.GetBodyID() + 1).ToString());
            Dictionary<string, string> wheres = new Dictionary<string, string>();
            wheres.Add("cVouchType", "DISPATCH");
            SqlHelper.Update(OrderInfo.ConnString, "ua_identity", values, wheres);
            values.Clear();
            values.Add("iDLsID", OrderInfo.GetiDLsIDCodeMax().ToString());
            SqlHelper.Insert(Program.ConnectionString, "DispatchLists_extradefine", values);
        }
    }
}
