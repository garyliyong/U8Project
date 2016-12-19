using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using UFIDA.U8.U8APIFramework;
using UFIDA.U8.U8APIFramework.Parameter;
using MSXML2;
using System.Collections;

namespace SHYSInterface.退货单
{
    public class OrderInfo
    {
        //获取商品类型编码
        public static string GetSPLXCode(string splxName)
        {
            int splx = 9;
            if (splxName == "药品类")
            {
                splx = 1;
            }
            else if (splxName == "医用耗材器械类")
            {
                splx = 2;
            }
            return splx.ToString();
        }

        //获取商品类型名称
        public static string GetSPLXName(string splx)
        {
            string splxName = "其他";
            if (splx == "1")
            {
                splxName = "药品类";
            }
            else if (splx == "2")
            {
                splxName = "医用耗材器械类";
            }
            return splxName;
        }

        /// <summary>
        /// 获取药品类型代码
        /// </summary>
        /// <param name="yplx"></param>
        /// <returns></returns>
        public static string GetYPLXCode(string yplx)
        {
            if (yplx != "")
            {
                string sql = @" select ypjxmc,ypjxbm from ysxt_ypjx";
                DataTable db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
                DataRow[] rows = db.Select("ypjxmc='" + yplx + "'");
                return rows[0]["ypjxbm"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        //获取药品类型名称
        public static string GetYPLXName(string yplx)
        {
            if (yplx != "")
            {
                string sql = @" select CGBM,CGMC from ysxt_thdclzt";
                DataTable db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
                DataRow[] rows = db.Select("ypjxbm='" + yplx + "'");
                return rows[0]["ypjxmc"].ToString();
            }
            else
            {
                return string.Empty;
            }
            
        }

        //退货单处理状态编码
        public static string GetTHDCLZTCode(string yplx)
        {
            if (yplx != "")
            {
                string sql = @" select CGBM,CGMC from ysxt_thdclzt";
                DataTable db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
                DataRow[] rows = db.Select("CGMC='" + yplx + "'");
                return rows[0]["CGBM"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        //退货单处理状态名称
        public static string GetTHDCLZTName(string yplx)
        {
            if (yplx != "")
            {
                string sql = @"select CGBM,CGMC from ysxt_thdclzt";
                DataTable db = SqlAccess.ExecuteSqlDataTable(sql, Program.ConnectionString);
                DataRow[] rows = db.Select("CGBM='" + yplx + "'");
                return rows[0]["CGMC"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        //提交方式代码
        public static string GetTJFSCode(string tjfsName)
        {
            int tjfs = 0;
            if (tjfsName == "医院填报")
            {
                tjfs = 1;
            }
            else if (tjfsName == "药企代填")
            {
                tjfs = 2;
            }
            return tjfs.ToString();
        }

        //提交方式名称
        public static string GetTJFSName(string tjfsCode)
        {
            string tjfsName = "";
            if (tjfsCode == "1")
            {
                tjfsName = "医院填报";
            }
            else if (tjfsCode == "2")
            {
                tjfsName = "药企代填";
            }
            return tjfsName;
        }

        //获取医院名称
        public static string GetYYMC(string yybm)
        {
            return ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cus.cCusName from Customer_extradefine  ex join Customer cus on cus.cCusCode=ex.cCusCode where ex.ccdefine1='" + ClsSystem.gnvl(yybm, "") + "'", Program.ConnectionString), "");
        }

        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构  
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中  
            return tmp;
        }  

        public static DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                if (dgv.Rows[count].IsNewRow)
                    continue;
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = dgv.Rows[count].Cells[countsub].Value.ToString();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


        public static string ConnString
        {
            get
            {
                string connstring = "";
                string loginInfo = Program.ConnectionString;
                string[] arrays = loginInfo.Split(';');
                for (int loop = 0; loop < arrays.Length; ++loop)
                {
                    if (arrays[loop].Contains("database"))
                    {
                        int index = arrays[loop].IndexOf("=");
                        if (index > 0)
                        {
                            string left = arrays[loop].Substring(0, index);
                            arrays[loop] = left + "=ufsystem";
                        }
                    }
                    if (loop == arrays.Length - 1)
                    {
                        connstring += arrays[loop];
                    }
                    else
                    {
                        connstring += arrays[loop] + ";";
                    }
                }
                return connstring;
            }
        }

        //表头序列号
        public static int GetHeadID()
        {
            //cAcc_id = '003' AND
            string script = @"SELECT iFatherId FROM ua_identity WHERE  cVouchType = 'DISPATCH'";
            Hashtable table = new Hashtable();
            SqlHelperUtility.SqlHelpQueryOneLine(ConnString, script, table);
            return int.Parse(table["iFatherId"].ToString());
        }

        //表体序列号
        public static int GetBodyID()
        {
            string script = @"SELECT iChildId FROM ua_identity WHERE cVouchType = 'DISPATCH'";
            Hashtable table = new Hashtable();
            SqlHelperUtility.SqlHelpQueryOneLine(ConnString, script, table);
            return int.Parse(table["iChildId"].ToString());
        }

        //存货名称
        private static string Getcinvname(string cinvcode)
        {
            string script = @"SELECT cInvName FROM Inventory WHERE cInvCode ='" + cinvcode+"'";
            Hashtable table = new Hashtable();
            SqlHelperUtility.SqlHelpQueryOneLine(Program.ConnectionString, script, table);
            return table["cInvName"].ToString();
        }

        private static DateTime GetDateTime(string date)
        {
            string datetime = string.Empty;
            datetime = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
            return DateTime.Parse(datetime);
        }


        //导入退货单到U8中
//        public static void  ImportReturnOrderAPI(DataRow [] rows)
//        {
//            try
//            {
//                //导入退货单
//                U8EnvContext envContext = new U8EnvContext();
//                envContext.U8Login = APIinterface.GetU8Login();

//                //设置上下文参数
//                envContext.SetApiContext("VoucherType", 10); //上下文数据类型：int，含义：单据类型：10

//                //第三步：设置API地址标识(Url)
//                //当前API：新增或修改的地址标识为：U8API/ReturnOrder/Save
//                U8ApiAddress myApiAddress = new U8ApiAddress("U8API/ReturnOrder/Save");

//                //第四步：构造APIBroker
//                U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

//                //第五步：API参数赋值

//                //给BO表头参数domHead赋值，此BO参数的业务类型为退货单，属表头参数。BO参数均按引用传递
//                //提示：给BO表头参数domHead赋值有两种方法

//                //方法二是构造BusinessObject对象，具体方法如下：
//                BusinessObject domHead = broker.GetBoParam("domHead");
//                domHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
//                //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
//                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                
//                /****************************** 以下是必输字段 ****************************/
//                domHead[0]["dlid"] = "0"; //主关键字段，int类型
//                domHead[0]["cdlcode"] = "0"; //退货单号，string类型
//                domHead[0]["cvouchtype"] = "05"; //单据类型编码，string类型
//                domHead[0]["cstcode"] = "01"; //销售类型编码，string类型
//                domHead[0]["ddate"] = GetDateTime(ClsSystem.gnvl(rows[0]["THDTJRQ"], "")).ToShortDateString(); //退货日期，DateTime类型
//                domHead[0]["cbustype"] = "普通销售"; //业务类型，int类型

//                string cCusCode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusCode  from Customer_extradefine  with(nolock)  where ccdefine1='" +
//                    ClsSystem.gnvl(rows[0]["YYBM"], "") + "'", Program.ConnectionString), "");
//                domHead[0]["ccuscode"] = cCusCode;//客户编码，string类型
//                string cpersoncode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusPPerson    from customer  with(nolock)  where cCusCode='" +
//                    cCusCode + "'", Program.ConnectionString), "");
//                domHead[0]["cdepcode"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cDepCode     from Person  with(nolock)  where cpersoncode='" +
//                    cpersoncode + "'", Program.ConnectionString), ""); //部门编码，string类型


//                /***************************** 以下是非必输字段 ****************************/
//                decimal iTaxRate = 17M;
//                domHead[0]["breturnflag"] = "1"; //退货标识，string类型
//                domHead[0]["ccusname"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusName   from customer  with(nolock)  where cCusCode='" +
//                    cCusCode + "'", Program.ConnectionString), "");  //客户名称，string类型
//                domHead[0]["cpersoncode"] = cpersoncode; //业务员编码，string类型
//                domHead[0]["dcreatesystime"] = DateTime.Today.ToShortDateString(); //制单时间，DateTime类型
//                domHead[0]["cexch_name"] = "人民币"; //币种，string类型
//                domHead[0]["iexchrate"] = "1"; //汇率，double类型
//                domHead[0]["itaxrate"] = iTaxRate; //税率，double类型
//                //制单人
//                domHead[0]["cmaker"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cUser_Name from ua_user  with(nolock)  where cUser_Id='" +
//                    Program.userCode + "'", Program.ConnectionString), ""); ; //制单人，string类型
//                //医院编码
//                domHead[0]["ccusdefine11"] = ClsSystem.gnvl(rows[0]["YYBM"], ""); //客户自定义项11，string类型
//                //计划单号
//                domHead[0]["ccusdefine14"] = ClsSystem.gnvl(rows[0]["YYBM"], "") + "-" +
//                    ClsSystem.gnvl(rows[0]["PSDBM"], "") + "-" +
//                    ClsSystem.gnvl(rows[0]["THDBH"], ""); //客户自定义项14，string类型
//                domHead[0]["ivtid"] = 75; //单据模版号，int类型

//                //给BO表体参数domBody赋值，此BO参数的业务类型为退货单，属表体参数。BO参数均按引用传递
//                //提示：给BO表体参数domBody赋值有两种方法
//                BusinessObject domBody = broker.GetBoParam("domBody");
//                int count = rows.Length;
//                domBody.RowCount = count; //设置BO对象行数

//                string ZXSPBM = rows[0]["ZXSPBM"].ToString();
//                //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
//                //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
//                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
//                int bodyId = GetBodyID();
//                for (int i = 0; i < count; ++i)
//                {
//                    decimal iQuantity = -Public.GetNum(rows[i]["THSL"]);//数量

//                    decimal iTaxUnitPrice = 0;//原币含税单价
//                    //统编代码查找存货编码
//                    string cinvcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cinvcode from inventory_extradefine where cidefine1='"
//                        + ZXSPBM + "'", Program.ConnectionString), "");
//                    string sql = " select isnull(iInvNowCost,0)   from SA_CusUPrice where   cInvCode ='" + cinvcode + "'  and cCusCode  ='" +
//                        cCusCode + "' and dStartDate =(select MAX(dStartDate) from SA_CusUPrice where cinvcode='" +
//                        cinvcode + "' and  cCusCode ='" + cCusCode + "')";
//                    iTaxUnitPrice = Public.GetNum(ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "0"));
//                    if (iTaxUnitPrice == 0)
//                    {
//                        sql = " select iUPrice1  from SA_InvUPrice where cInvCode ='" + cinvcode +
//                            "' and dStartDate =(select MAX(dStartDate) from SA_InvUPrice where cInvCode ='" + cinvcode + "')";
//                        iTaxUnitPrice = Public.GetNum(ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "0"));
//                    }
//                    decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
//                    decimal iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + iTaxRate / 100M), 4);//原币无税单价
//                    decimal iMoney = Public.ChinaRound(iSum / (1M + iTaxRate / 100M), 2);//原币无税金额

//                    decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

//                    decimal iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
//                    decimal iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
//                    decimal iNatMoney = Public.ChinaRound(iNatSum / (1 + iTaxRate / 100M), 2);//本币无税金额
//                    decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

//                    string cgroupcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cgroupcode  from Inventory a with(nolock)  where cinvcode='" +
//                        cinvcode + "'", Program.ConnectionString), "");
//                    string igrouptype = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  igrouptype  from Inventory a with(nolock)  where cinvcode='" +
//                        cinvcode + "'", Program.ConnectionString), "");
                   
        
//                    /****************************** 以下是必输字段 ****************************/
//                    domBody[i]["dlid"] = "0";
//                    domBody[i]["idlsid"] = "0"; //主关键字段，0类型
//                    domBody[i]["cinvcode"] = cinvcode;//存货编码，string类型
//                    domBody[i]["cinvname"] = Getcinvname(cinvcode); //存货名称，string类型
//                    domBody[i]["iquantity"] = iQuantity; //数量，double类型
//                    domBody[i]["inum"] = iQuantity; //数量，double类型
//                    domBody[i]["creasonname"] = rows[i]["THYY"];
//                    domBody[i]["irowno"] = i + 1;
//                    //domBody[i]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
//                    domBody[i]["bsaleprice"] = "1";//报价含税，string类型
//                    /***************************** 以下是非必输字段 ****************************/
//                    domBody[i]["itaxrate"] = iTaxRate; //税率（％），double类型
//                    domBody[i]["iquotedprice"] = rows[i]["THDJ"]; //报价，double类型
//                    domBody[i]["itaxunitprice"] = iTaxUnitPrice; //含税单价，double类型
//                    domBody[i]["iunitprice"] = iUnitPrice; //无税单价，double类型
//                    domBody[i]["imoney"] = iMoney; //无税金额，double类型
//                    domBody[i]["itax"] = iTax; //税额，double类型
//                    domBody[i]["inatmoney"] = iNatMoney; //本币金额，double类型
//                    domBody[i]["inattax"] = iNatTax; //本币税额，double类型
//                    domBody[i]["inatsum"] = iNatSum; //本币价税合计，double类型
//                    domBody[i]["cgroupcode"] = cgroupcode; //计量单位组，string类型
//                    domBody[i]["igrouptype"] = igrouptype; //单位类型，uint类型
//                    domBody[i]["cbatch"] = rows[i]["SCPH"]; //批号，string类型
//                    domBody[i]["cdefine28"] = ZXSPBM; //表体自定义项7，string类型  统编代码
//                    domBody[i]["cdefine29"] = ClsSystem.gnvl(rows[i]["THDBH"], ""); //表体自定义项8，string类型 退货单编号
//                    domBody[i]["cdefine30"] = ClsSystem.gnvl(rows[i]["CGJLDW"], ""); //表体自定义项9，string类型 采购计量单位
//                    domBody[i]["cdefine32"] = ClsSystem.gnvl(rows[i]["PSDBM"], "");// 医院配送点编码 //表体自定义项11，string类型
//                    domBody[i]["cdefine33"] = ClsSystem.gnvl(rows[i]["PSDZ"], ""); // 配送地址//表体自定义项12，string类型
//                    domBody[i]["kl2"] = "100"; //扣率2（％），double类型
//                    domBody[i]["kl"] = "100"; //扣率（％），double类型
//                    //domBody[i]["dmdate"] = DateTime.Today.ToShortDateString(); //生产日期，DateTime类型
//                    //domBody[i]["dvdate"] = DateTime.Today.ToShortDateString(); //失效日期，DateTime类型

//                    //domBody[i]["cinva_unit"] = ""; //销售单位，string类型
//                    //domBody[i]["cinvm_unit"] = ""; //主计量单位，string类型

//                }
//                //给普通参数VoucherState赋值。此参数的数据类型为int，此参数按值传递，表示状态:0增加;1修改
//                broker.AssignNormalValue("VoucherState", 0);
//                //该参数vNewID为INOUT型普通参数。此参数的数据类型为string，此参数按值传递。在API调用返回时，可以通过GetResult("vNewID")获取其值
//#if DEBUG
//                domHead.ToXmlDoc().Save(@"c:\returnorder_head.xml");
//                domBody.ToXmlDoc().Save(@"c:\returnorder_body.xml");
//#endif
//                string vNewIDRet = string.Empty;
//                broker.AssignNormalValue("vNewID", vNewIDRet);

//                //给普通参数DomConfig赋值。此参数的数据类型为MSXML2.IXMLDOMDocument2，此参数按引用传递，表示ATO,PTO选配
//                DOMDocument domMsg = new DOMDocument();
//                broker.AssignNormalValue("DomConfig", domMsg);
//                //第六步：调用API
//                if (!broker.Invoke())
//                {
//                    Exception apiEx = broker.GetException();
//                    broker.Release();
//                    throw apiEx;
//                }
//                string result = broker.GetReturnValue() as System.String;
//                if (result != "")
//                {
//                    broker.Release();
//                    throw new Exception(result);
//                }
//                //获取普通INOUT参数vNewID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
//                vNewIDRet = broker.GetResult("vNewID") as string;
//                //结束本次调用，释放API资源
//                broker.Release();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("导入退货单失败！" + ex.Message);
//            }
//        }

        //导入退货单到U8中，直接写数据库

        //获取退货单号
        private static string GetcDLCodeMax()
        {
            string code = string.Empty;
            List<string> values = new List<string>();
            values.Add("cDLCode");
            DataTable dt = SqlHelperUtility.SqlHelpQuery("DispatchList", values, Program.ConnectionString, null, "cDLCode");
            if (dt.Rows.Count == 0)
            {
                throw new Exception("查询退货单号失败");
            } 
            code = dt.Rows[dt.Rows.Count - 1]["cDLCode"].ToString();
            code = "00000" + (int.Parse(code) + 1).ToString();
            return code;
        }


        public static int GetiDLsIDCodeMax()
        {
            List<string> values = new List<string>();
            values.Add("iDLsID");
            DataTable dt = SqlHelperUtility.SqlHelpQuery("DispatchLists_extradefine", values, Program.ConnectionString, null, "iDLsID");
            if (dt.Rows.Count == 0)
            {
                throw new Exception("查询退货单号失败");
            }
            string code = dt.Rows[dt.Rows.Count - 1]["iDLsID"].ToString();
            return int.Parse(code) + 1;
        }

        public static void ImportReturnOrder(DataRow[] rows)
        {
            string id = string.Empty;
            try
            {
                List<string> values = new List<string>();
                values.Add("*");
                Dictionary<string, string> keys = new Dictionary<string, string>();
                keys.Add("cDefine14", ClsSystem.gnvl(rows[0]["YYBM"], "") + "-" +
                        ClsSystem.gnvl(rows[0]["PSDBM"], "") + "-" +
                        ClsSystem.gnvl(rows[0]["THDBH"], ""));
                DataSet ds =  SqlHelper.Query(Program.ConnectionString,"DispatchList", values, keys,null);
                if (ds.Tables.Count > 0 &ds.Tables[0].Rows.Count > 0)
                {
                    return;
                }
                //表头
                DispatchList dispatchList = new DispatchList();
                dispatchList.DLID = "10000" + (GetHeadID() + 1).ToString();
                //查询DispatchList中cDLCode最大值
                dispatchList.cDLCode = GetcDLCodeMax();
                id = dispatchList.DLID;
                string cCusCode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusCode  from Customer_extradefine  with(nolock)  where ccdefine1='" +
                        ClsSystem.gnvl(rows[0]["YYBM"], "") + "'", Program.ConnectionString), "");
                string cpersoncode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusPPerson    from customer  with(nolock)  where cCusCode='" +
                        cCusCode + "'", Program.ConnectionString), "");
                dispatchList.cDepCode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cDepCode     from Person  with(nolock)  where cpersoncode='" +
                        cpersoncode + "'", Program.ConnectionString), "");
                dispatchList.cPersonCode = cpersoncode;
                dispatchList.cCusCode = cCusCode;
                dispatchList.cCusName = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusName   from customer  with(nolock)  where cCusCode='" +
                        cCusCode + "'", Program.ConnectionString), "");
                dispatchList.cDefine11 = ClsSystem.gnvl(rows[0]["YYBM"], "");
                dispatchList.cDefine12 = ClsSystem.gnvl(rows[0]["PSDBM"], "");
                dispatchList.cDefine13 = ClsSystem.gnvl(rows[0]["PSDZ"], "");
                dispatchList.cDefine14 = ClsSystem.gnvl(rows[0]["YYBM"], "") + "-" +
                        ClsSystem.gnvl(rows[0]["PSDBM"], "") + "-" +
                        ClsSystem.gnvl(rows[0]["THDBH"], "");
                //制单人
                dispatchList.cMaker = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cUser_Name from ua_user  with(nolock)  where cUser_Id='" +
                                    Program.userCode + "'", Program.ConnectionString), "");
                dispatchList.WriteToSql();
                //表体
                int count = rows.Length;
                for (int i = 0; i < count; ++i)
                {
                    decimal iTaxRate = 17M;
                    string ZXSPBM = rows[i]["ZXSPBM"].ToString();
                    //2016-12-14 TODO: 修改数量
                    //decimal iQuantity = -(Public.GetNum(rows[i]["THSL"]) * Public.GetNum(rows[i]["BZNHSL"]));//数量
                    decimal iQuantity = -Public.GetNum(rows[i]["THSL"]);//数量
                    decimal iTaxUnitPrice = 0;//原币含税单价
                    //统编代码查找存货编码
                    string cinvcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cinvcode from inventory_extradefine where cidefine1='"
                        + ZXSPBM + "'", Program.ConnectionString), "");
                    string sql = " select isnull(iInvNowCost,0)   from SA_CusUPrice where   cInvCode ='" + cinvcode + "'  and cCusCode  ='" +
                        cCusCode + "' and dStartDate =(select MAX(dStartDate) from SA_CusUPrice where cinvcode='" +
                        cinvcode + "' and  cCusCode ='" + cCusCode + "')";
                    iTaxUnitPrice = Public.GetNum(rows[i]["THDJ"]);
                    //iTaxUnitPrice = Public.GetNum(ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "0"));
                    //if (iTaxUnitPrice == 0)
                    //{
                    //    sql = " select iUPrice1  from SA_InvUPrice where cInvCode ='" + cinvcode +
                    //        "' and dStartDate =(select MAX(dStartDate) from SA_InvUPrice where cInvCode ='" + cinvcode + "')";
                    //    iTaxUnitPrice = Public.GetNum(ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "0"));
                    //}
                    decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                    decimal iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + iTaxRate / 100M), 4);//原币无税单价
                    decimal iMoney = Public.ChinaRound(iSum / (1M + iTaxRate / 100M), 2);//原币无税金额
                    decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额
                    decimal iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
                    decimal iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
                    decimal iNatMoney = Public.ChinaRound(iNatSum / (1 + iTaxRate / 100M), 2);//本币无税金额
                    decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                    DispatchLists dispatchLists = new DispatchLists();
                    dispatchLists.AutoID = (GetBodyID() + i + 1).ToString();
                    dispatchLists.DLID = dispatchList.DLID;
                    //在DispatchLists_extradefine表中查询
                    dispatchLists.iDLsID = GetiDLsIDCodeMax().ToString();
                    //通过存货编码查找仓库编码
                    dispatchLists.cWhCode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cDefWareHouse from Inventory where cInvCode='"
                        + cinvcode + "'", Program.ConnectionString), "");
                    dispatchLists.cInvCode = cinvcode;
                    dispatchLists.cInvName = Getcinvname(cinvcode);
                    dispatchLists.iQuantity = iQuantity.ToString();
                    dispatchLists.iNum = dispatchLists.iQuantity;
                    dispatchLists.iSettleQuantity = "0";
                    dispatchLists.iQuotedPrice = rows[i]["THDJ"].ToString();
                    dispatchLists.iUnitPrice = iUnitPrice.ToString();
                    dispatchLists.iTaxUnitPrice = iTaxUnitPrice.ToString();
                    dispatchLists.iMoney = iMoney.ToString();
                    dispatchLists.iTax = iTax.ToString();
                    dispatchLists.iSum = iSum.ToString();
                    dispatchLists.iNatUnitPrice = iNatUnitPrice.ToString();
                    dispatchLists.iNatMoney = iNatMoney.ToString();
                    dispatchLists.iNatTax = iNatTax.ToString();
                    dispatchLists.iNatSum = dispatchLists.iSettleNum = iNatSum.ToString();
                    dispatchLists.cBatch = rows[i]["SCPH"].ToString();
                    dispatchLists.cDefine28 = ZXSPBM;
                    dispatchLists.cDefine29 = ClsSystem.gnvl(rows[i]["THDBH"], "");
                    dispatchLists.cDefine30 = ClsSystem.gnvl(rows[i]["CGJLDW"], "");
                    dispatchLists.cDefine32 = ClsSystem.gnvl(rows[i]["PSDBM"], "");
                    dispatchLists.cDefine33 = ClsSystem.gnvl(rows[i]["PSDZ"], "");
                    dispatchLists.cUnitID = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cSAComUnitCode    from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", Program.ConnectionString), ""); //计量单位编码
                    dispatchLists.dMDate = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  dMDate from CurrentStock where cInvCode='"
                        + cinvcode + "' and cBatch='" + rows[i]["SCPH"].ToString() + "'", Program.ConnectionString), "");
                    //根据CurrentStock表按照批次号查找生产日期和失效日期
                    dispatchLists.dvDate = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  dvDate from CurrentStock where cInvCode='"
                        + cinvcode + "' and cBatch = '" + rows[i]["SCPH"].ToString() + "'", Program.ConnectionString), "");
                    dispatchLists.cExpirationdate = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cExpirationdate from CurrentStock where cInvCode='"
                        + cinvcode + "' and cBatch = '" + rows[i]["SCPH"].ToString() + "'", Program.ConnectionString), "");
                    dispatchLists.irowno = (i + 1).ToString();

                    dispatchLists.WriteToSql();
                }
            }
            catch (Exception ex)
            {
                Dictionary<string,string> keys = new Dictionary<string,string>();
                keys.Add("DLID",id);
                SqlHelper.Delete(Program.ConnectionString,"DispatchList",keys);
                SqlHelper.Delete(Program.ConnectionString, "DispatchLists", keys);
                throw ex;
            }
            
        }
    }
}
