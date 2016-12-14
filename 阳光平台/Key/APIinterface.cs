using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using UFIDA.U8.U8APIFramework;
using UFIDA.U8.U8APIFramework.Parameter;
using UFIDA.U8.U8MOMAPIFramework;
using MSXML2;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ADODB;
using System.Data.SqlClient;

namespace SHYSInterface
{
    class APIinterface
    {
        public static string InSO(U8Login.clsLogin u8Login,  string BWB, DataTable dt,  int iRow)
        {

            string errMsg = "";
            #region 销售订单
            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            //销售所有接口均支持内部独立事务和外部事务，默认内部事务
            //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
            //envContext.BizDbConnection = conn;
            //envContext.IsIndependenceTransaction = false;

            //设置上下文参数
            envContext.SetApiContext("VoucherType", 12); //上下文数据类型：int，含义：单据类型：12

            //第三步：设置API地址标识(Url)
            //当前API错误：新增或修改的地址标识为：U8API/SaleOrder/Save
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/SaleOrder/Save");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给BO表头参数domHead赋值，此BO参数的业务类型为销售订单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数domHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domHead = broker.GetBoParam("domHead");
            domHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            decimal iTaxRate = 17M;
            string cCusCode="";

            /****************************** 以下是必输字段 ****************************/
            domHead[0]["id"] = "0"; //主关键字段，int类型
            domHead[0]["csocode"] = Public.GetParentCode("17", "", Program.ConnectionString);  //订 单 号，string类型
         //   string rq1 = Public.GetStr("/", ClsSystem.gnvl(dt.Rows[iRow]["DDTJRQ"], ""), 1);
            string rq1 = ClsSystem.gnvl(dt.Rows[iRow]["DDTJRQ"], "").Substring(0, 8);
            rq1 = rq1.Insert(4, "/");
            rq1 = rq1.Insert(7, "/");
            domHead[0]["ddate"] = Convert.ToDateTime(rq1).ToShortDateString(); //订单日期，DateTime类型
            domHead[0]["cbustype"] = "普通销售"; //业务类型，int类型
            //domHead[0]["cstname"] = ""; //销售类型，string类型
            //domHead[0]["ccusabbname"] = ""; //客户简称，string类型
            //domHead[0]["cdepname"] = ""; //销售部门，string类型
            domHead[0]["itaxrate"] = iTaxRate; //税率，double类型
            domHead[0]["cexch_name"] = BWB; //币种，string类型
            domHead[0]["cmaker"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cUser_Name from ua_user  with(nolock)  where cUser_Id='" + Program.userCode + "'", Program.ConnectionString), ""); //制单人，string类型
            //domHead[0]["breturnflag"] = ""; //退货标志，string类型
            //domHead[0]["ufts"] = ""; //时间戳，string类型
            domHead[0]["cstcode"] = "01"; //销售类型编号，string类型
          
            cCusCode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusCode  from Customer_extradefine  with(nolock)  where ccdefine1='" + ClsSystem.gnvl(dt.Rows[iRow]["YYBM"], "") + "'", Program.ConnectionString), "");
            if (ClsSystem.gnvl(cCusCode, "") == "")
            {
                errMsg = "API错误:未查询到该医院编码：" + ClsSystem.gnvl(dt.Rows[iRow]["YYBM"], "") + "对应的客户，请查实";
                return errMsg;
            }
            domHead[0]["ccuscode"] = cCusCode;//客户编码，string类型
           string cpersoncode=   ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusPPerson    from customer  with(nolock)  where cCusCode='" + cCusCode + "'", Program.ConnectionString), "");
           if (cpersoncode == "")
           {
               errMsg = "API错误:未查询到该医院编码对应的客户中维护了专管业务员，请查实";
               return errMsg;
           }
            domHead[0]["cdepcode"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cDepCode     from Person  with(nolock)  where cpersoncode='" + cpersoncode + "'", Program.ConnectionString), ""); //部门编码，string类型
            /***************************** 以下是非必输字段 ****************************/
            //domHead[0]["fstockquanO"] = ""; //现存件数，double类型
            //domHead[0]["fcanusequanO"] = ""; //可用件数，double类型
            //domHead[0]["iverifystate"] = ""; //iverifystate，string类型
            //domHead[0]["ireturncount"] = ""; //ireturncount，string类型
            //domHead[0]["icreditstate"] = ""; //icreditstate，string类型
            //domHead[0]["iswfcontrolled"] = ""; //iswfcontrolled，string类型
            domHead[0]["dpredatebt"] = Convert.ToDateTime(rq1).ToShortDateString(); //预发货日期，DateTime类型
            domHead[0]["dpremodatebt"] = Convert.ToDateTime(rq1).ToShortDateString(); //预完工日期，DateTime类型
            //domHead[0]["caddcode"] = ""; //收货地址编码，string类型
            //domHead[0]["cdeliverunit"] = ""; //收货单位，string类型
            //domHead[0]["ccontactname"] = ""; //收货联系人，string类型
            //domHead[0]["cofficephone"] = ""; //收货联系电话，string类型
            //domHead[0]["cmobilephone"] = ""; //收货联系人手机，string类型
            //domHead[0]["cpayname"] = ""; //付款条件，string类型
            //domHead[0]["cpersonname"] = ""; //业 务 员，string类型
            domHead[0]["iexchrate"] = "1"; //汇率，double类型
            //domHead[0]["cmemo"] = ""; //备    注，string类型
            //domHead[0]["cverifier"] = Convert.ToString(dt.Rows[iRow]["cMaker"]); //审核人，string类型
            //domHead[0]["ccloser"] = ""; //关闭人，string类型
            //domHead[0]["clocker"] = ""; //锁定人，string类型
            //domHead[0]["ivtid"] = ""; //单据模版号，int类型
            //domHead[0]["istatus"] = "1"; //订单状态，string类型
            //domHead[0]["ccrechppass"] = ""; //信用审核口令，string类型
            //domHead[0]["clowpricepass"] = ""; //最低售价口令，string类型
            //domHead[0]["bcontinue"] = ""; //是否继续，string类型
            //domHead[0]["isumx"] = ""; //价税合计，double类型
            //domHead[0]["zdsum"] = ""; //整单合计，double类型
            domHead[0]["ccusname"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusName   from customer  with(nolock)  where cCusCode='" + cCusCode + "'", Program.ConnectionString), "");  //客户名称，string类型
            //domHead[0]["ccusphone"] = ""; //联系电话，string类型
            //domHead[0]["csccode"] = ""; //发运方式编码，string类型
            //domHead[0]["cpaycode"] = ""; //付款条件编码，string类型
            //domHead[0]["ccusperson"] = ""; //联系人，string类型
            //domHead[0]["coppcode"] = ""; //商机编码，string类型
            //domHead[0]["ccusaddress"] = ""; //客户地址，string类型
            domHead[0]["cpersoncode"] = cpersoncode; //业务员编码，string类型
            //domHead[0]["iarmoney"] = ""; //客户应收余额，double类型
            //domHead[0]["ccusoaddress"] = ""; //发货地址，string类型
            //domHead[0]["imoney"] = ""; //定    金，double类型
            //domHead[0]["cscname"] = ""; //发运方式，string类型
            //domHead[0]["cchanger"] = ""; //变更人，string类型
            domHead[0]["dcreatesystime"] = DateTime.Today.ToString(); //制单时间，DateTime类型
            //domHead[0]["dverifysystime"] = ""; //审核时间，DateTime类型
            //domHead[0]["dmodifysystime"] = ""; //修改时间，DateTime类型
            //domHead[0]["cmodifier"] = ""; //修改人，string类型
            //domHead[0]["dmoddate"] = ""; //修改日期，DateTime类型
            //domHead[0]["dverifydate"] = Convert.ToString(dt.Rows[iRow]["dDate"]); //审核日期，DateTime类型
            //domHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            //domHead[0]["ccrechpname"] = ""; //信用审核人，string类型
            //domHead[0]["ccusdefine1"] = ""; //客户自定义项1，string类型
            //domHead[0]["ccusdefine2"] = ""; //客户自定义项2，string类型
            //domHead[0]["ccusdefine3"] = ""; //客户自定义项3，string类型
            //domHead[0]["ccusdefine4"] = ""; //客户自定义项4，string类型
            //domHead[0]["zdsumdx"] = ""; //整单合计（大写），string类型
            //domHead[0]["isumdx"] = ""; //价税合计（大写），string类型
            //domHead[0]["ccusdefine5"] = ""; //客户自定义项5，string类型
            //domHead[0]["ccusdefine6"] = ""; //客户自定义项6，string类型
            //domHead[0]["ccusdefine7"] = ""; //客户自定义项7，string类型
            //domHead[0]["ccusdefine8"] = ""; //客户自定义项8，string类型
            //domHead[0]["ccusdefine9"] = ""; //客户自定义项9，string类型
            //domHead[0]["ccusdefine10"] = ""; //客户自定义项10，string类型
            //domHead[0]["ccusdefine11"] = ""; //客户自定义项11，string类型
            //domHead[0]["ccusdefine12"] = ""; //客户自定义项12，string类型
            //domHead[0]["ccusdefine13"] = ""; //客户自定义项13，string类型
            //domHead[0]["ccusdefine14"] = ""; //客户自定义项14，string类型
            //domHead[0]["ccusdefine15"] = ""; //客户自定义项15，string类型
            //domHead[0]["ccusdefine16"] = ""; //客户自定义项16，string类型
            //domHead[0]["icuscreline"] = ""; //用户信用度，double类型
            //domHead[0]["fstockquan"] = ""; //现存数量，double类型
            //domHead[0]["fcanusequan"] = ""; //可用数量，double类型
            //domHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            //domHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            //domHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
            //domHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
            //domHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            //domHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            //domHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            //domHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            //domHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            //domHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            domHead[0]["cdefine11"] = ClsSystem.gnvl(dt.Rows[iRow]["YYBM"], ""); //表头自定义项11，string类型  医院编码
            //domHead[0]["cdefine12"] = ClsSystem.gnvl(dt.Rows[iRow]["PSDBM"], "");//表头自定义项12，string类型 医院配送点编码
            //domHead[0]["cdefine13"] = ClsSystem.gnvl(dt.Rows[iRow]["PSDZ"], ""); //表头自定义项13，string类型  配送地址
            domHead[0]["cdefine14"] = ClsSystem.gnvl(dt.Rows[iRow]["YYBM"], "") + "-" + ClsSystem.gnvl(dt.Rows[iRow]["PSDBM"], "") + "-" + ClsSystem.gnvl(dt.Rows[iRow]["DDTJRQ"], ""); //表头自定义项14，string类型  配送点编码
            //domHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            //domHead[0]["ccreditcuscode"] = ""; //信用单位编码，string类型
            //domHead[0]["ccreditcusname"] = ""; //信用单位名称，string类型
            //domHead[0]["cgatheringplan"] = ""; //收付款协议编码，string类型
            //domHead[0]["cgatheringplanname"] = ""; //收付款协议名称，string类型

            //给BO表体参数domBody赋值，此BO参数的业务类型为销售订单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");

            DataView rowfilter = new DataView(dt);
            rowfilter.RowFilter = " YYBM='" + ClsSystem.gnvl(dt.Rows[iRow]["YYBM"], "") + "' and PSDBM='" + ClsSystem.gnvl(dt.Rows[iRow]["PSDBM"], "") + "' and DDTJRQ='" + ClsSystem.gnvl(dt.Rows[iRow]["DDTJRQ"], "") + "'";
            rowfilter.RowStateFilter = DataViewRowState.CurrentRows;
            DataTable dts = rowfilter.ToTable();

            domBody.RowCount = dts.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            string sql="";
            string cinvcode="";
            string ZXSPBM="";
            string cgroupcode = "";
            string igrouptype = "";
            decimal iInvExchRate = 0;
            decimal iTaxUnitPrice = 0;
            string result = "";
            for (int i = 0; i < dts.Rows.Count; i++)
            {
                  ZXSPBM = Convert.ToString(dts.Rows[i]["ZXSPBM"]);
                  sql = "select  cinvcode from inventory_extradefine where cidefine1='" + ZXSPBM + "'";
                  cinvcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "");
                  if (cinvcode == "")
                 {
                     errMsg = "API错误:统编代码为:" + ZXSPBM + "对应的药品未取到，请查明！";
                     return errMsg;
                 }
                //sql=" select cInvCode from Inventory_extradefine  with(nolock)  where cidefine1='"+ZXSPBM+"'";
                //cinvcode= ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "");
             
                decimal iQuantity = Public.GetNum(dts.Rows[i]["CGSL"]);//数量
                
             //   decimal iTaxUnitPrice = Public.GetNum(dts.Rows[i]["iTaxUnitPrice"]);//原币含税单价
                sql = " select isnull(iInvNowCost,0)   from SA_CusUPrice where   cInvCode ='" + cinvcode + "'  and cCusCode  ='" + cCusCode + "' and dStartDate =(select MAX(dStartDate) from SA_CusUPrice where cinvcode='" + cinvcode + "' and  cCusCode ='" + cCusCode + "')";
           
                 iTaxUnitPrice =Public.GetNum( ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "0"));
                 if (iTaxUnitPrice == 0)
                 {
                    sql = " select iUPrice1  from SA_InvUPrice where cInvCode ='" + cinvcode + "' and dStartDate =(select MAX(dStartDate) from SA_InvUPrice where cInvCode ='" + cinvcode + "')";
                 }
                 iTaxUnitPrice = Public.GetNum(ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.ConnectionString), "0"));
                 if (iTaxUnitPrice == 0)
                 {
                     errMsg = "API错误:" + cinvcode + "价格未取到，请查明！";
                     return errMsg;
                 }
                //decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                //decimal iSum = Public.GetNum(dts.Rows[i]["iSum"]);//原币含税金额
                //decimal iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + iTaxRate / 100M), 4);//原币无税单价
                //decimal iMoney = Public.ChinaRound(iSum / (1M + iTaxRate / 100M), 2);//原币无税金额
                //decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                //decimal iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
                //decimal iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
                //decimal iNatMoney = Public.ChinaRound(iNatSum / (1 + iTaxRate / 100M), 2);//本币无税金额
                //decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

               decimal    iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
               decimal    iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + iTaxRate / 100M), 4);//原币无税单价
               decimal    iMoney = Public.ChinaRound(iSum / (1M + iTaxRate / 100M), 2);//原币无税金额

               decimal    iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

               decimal    iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
               decimal    iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
               decimal    iNatMoney = Public.ChinaRound(iNatSum / (1 + iTaxRate / 100M), 2);//本币无税金额
               decimal    iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                    cgroupcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cgroupcode  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", Program.ConnectionString), "");
                    igrouptype = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  igrouptype  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", Program.ConnectionString), ""); 

                /****************************** 以下是必输字段 ****************************/
                //domBody[i]["isosid"] = ""; //主关键字段，int类型
              //  domBody[i]["cinvname"] = Convert.ToString(dts.Rows[i]["cInvName"]); //存货名称，string类型
                domBody[i]["cinvcode"] = cinvcode; //存货编码，string类型
                //domBody[i]["autoid"] = ""; //销售订单 2，int类型
                domBody[i]["iquantity"] = iQuantity; //数量，double类型
                domBody[i]["dpredate"] = Convert.ToDateTime(rq1).ToShortDateString(); //预发货日期，DateTime类型
                domBody[i]["dpremodate"] = Convert.ToDateTime(rq1).ToShortDateString(); //预完工日期，DateTime类型
                domBody[i]["borderbom"] = "0"; //是否订单BOM，int类型
                domBody[i]["borderbomover"] = "0"; //订单BOM是否完成，int类型
                domBody[i]["id"] = "0"; //主表id，int类型
                //domBody[i]["iinvexchrate"] = ""; //换算率，double类型
                iInvExchRate = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(c.iChangRate,0)  from Inventory a with(nolock) join ComputationUnit c with(nolock) on a.cSAComUnitCode  =c.cComunitCode where a.cinvcode='" + cinvcode + "'", Program.ConnectionString));
                domBody[i]["iinvexchrate"] = iInvExchRate; //换算率，double类型


               
                domBody[i]["cunitid"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cSAComUnitCode    from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", Program.ConnectionString), "");; //销售单位编码，string类型
                domBody[i]["cinva_unit"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cSAComUnitCode    from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", Program.ConnectionString), ""); //销售单位，string类型
                domBody[i]["cinvm_unit"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cComUnitCode   from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", Program.ConnectionString), "");  //主计量单位，string类型
                domBody[i]["igrouptype"] = igrouptype; //单位类型，uint类型
                domBody[i]["cgroupcode"] = cgroupcode; //计量单位组，string类型
                //domBody[i]["dreleasedate"] = ""; //预留失效日期，DateTime类型
                domBody[i]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
                domBody[i]["bsaleprice"] = "1"; //报价含税，string类型
                /***************************** 以下是非必输字段 ****************************/
                //domBody[i]["natoseqid"] = ""; //ato行id，string类型
                //domBody[i]["natostatus"] = ""; //ato行编辑状态，string类型
                //domBody[i]["iquoid"] = ""; //报价id，string类型
                //domBody[i]["cscloser"] = ""; //行关闭人，string类型
                domBody[i]["irowno"] = i + 1; //行号，string类型
                //domBody[i]["cconfigstatus"] = ""; //选配标志，int类型
                //domBody[i]["ippartseqid"] = ""; //选配序号，string类型
                //domBody[i]["cquocode"] = ""; //报价单号，string类型
                //domBody[i]["cinvstd"] = ""; //规格型号，string类型
                //domBody[i]["ccontractid"] = ""; //合同编码，string类型
                //domBody[i]["ccontractrowguid"] = ""; //合同标的RowGuid，string类型
                //domBody[i]["ccontracttagcode"] = ""; //合同标的编码，string类型
                //domBody[i]["icusbomid"] = ""; //客户BomID，string类型
                //domBody[i]["ippartqty"] = ""; //母件数量，string类型
                //domBody[i]["ippartid"] = ""; //母件物料ID，string类型
                //domBody[i]["imoquantity"] = ""; //下达生产量，double类型
                //domBody[i]["batomodel"] = ""; //是否ATO件，int类型
                if (iInvExchRate != 0)
                {
                    domBody[i]["inum"] = iQuantity / iInvExchRate; //件数，double类型
                }
                else
                {
                    domBody[i]["inum"] = ""; //件数，double类型
                }
                //domBody[i]["fsalecost"] = ""; //零售单价，double类型
                domBody[i]["itaxunitprice"] = iTaxUnitPrice; //含税单价，double类型
                domBody[i]["iquotedprice"] = iTaxUnitPrice; //报价，double类型
                domBody[i]["iunitprice"] = iUnitPrice; //无税单价，double类型
                domBody[i]["imoney"] = iMoney; //无税金额，double类型
                domBody[i]["itax"] = iTax; //税额，double类型
                domBody[i]["isum"] = iSum; //价税合计，double类型
                //domBody[i]["fsaleprice"] = ""; //零售金额，double类型
                domBody[i]["inatunitprice"] = iNatUnitPrice; //本币单价，double类型
                domBody[i]["inatmoney"] = iNatMoney; //本币金额，double类型
                domBody[i]["inattax"] = iNatTax; //本币税额，double类型
                domBody[i]["inatsum"] = iNatSum; //本币价税合计，double类型
                //domBody[i]["inatdiscount"] = ""; //本币折扣额，double类型
                //domBody[i]["idiscount"] = ""; //折扣额，double类型
                //domBody[i]["ifhquantity"] = ""; //发货数量，double类型
                //domBody[i]["ifhnum"] = ""; //发货件数，double类型
                //domBody[i]["ifhmoney"] = ""; //发货金额，double类型
                //domBody[i]["ikpquantity"] = ""; //开票数量，double类型
                //domBody[i]["ikpnum"] = ""; //开票件数，double类型
                //domBody[i]["ikpmoney"] = ""; //开票金额，double类型
                //domBody[i]["iinvlscost"] = ""; //最低售价，double类型
                //domBody[i]["cfree1"] = ""; //自由项1，string类型
                //domBody[i]["cfree2"] = ""; //自由项2，string类型
                //domBody[i]["bservice"] = ""; //是否应税劳务，string类型
                //domBody[i]["bfree1"] = ""; //是否有自由项1，string类型
                //domBody[i]["bfree2"] = ""; //是否有自由项2，string类型
                //domBody[i]["bfree3"] = ""; //是否有自由项3，string类型
                //domBody[i]["bfree4"] = ""; //是否有自由项4，string类型
                //domBody[i]["bfree5"] = ""; //是否有自由项5，string类型
                //domBody[i]["bfree6"] = ""; //是否有自由项6，string类型
                //domBody[i]["bfree7"] = ""; //是否有自由项7，string类型
                //domBody[i]["bfree8"] = ""; //是否有自由项8，string类型
                //domBody[i]["bfree9"] = ""; //是否有自由项9，string类型
                //domBody[i]["bfree10"] = ""; //是否有自由项10，string类型
                //domBody[i]["cmemo"] = ""; //备注，string类型
                //domBody[i]["cinvdefine1"] = ""; //存货自定义项1，string类型
                //domBody[i]["cinvdefine4"] = ""; //存货自定义项4，string类型
                //domBody[i]["cinvdefine5"] = ""; //存货自定义项5，string类型
                //domBody[i]["cinvdefine6"] = ""; //存货自定义项6，string类型
                //domBody[i]["cinvdefine7"] = ""; //存货自定义项7，string类型
                //domBody[i]["bsalepricefree1"] = ""; //是否自由项定价1，string类型
                //domBody[i]["bsalepricefree2"] = ""; //是否自由项定价2，string类型
                //domBody[i]["bsalepricefree3"] = ""; //是否自由项定价3，string类型
                //domBody[i]["bsalepricefree4"] = ""; //是否自由项定价4，string类型
                //domBody[i]["bsalepricefree5"] = ""; //是否自由项定价5，string类型
                //domBody[i]["bsalepricefree6"] = ""; //是否自由项定价6，string类型
                //domBody[i]["bsalepricefree7"] = ""; //是否自由项定价7，string类型
                //domBody[i]["bsalepricefree8"] = ""; //是否自由项定价8，string类型
                //domBody[i]["bsalepricefree9"] = ""; //是否自由项定价9，string类型
                //domBody[i]["bsalepricefree10"] = ""; //是否自由项定价10，string类型
                //domBody[i]["iaoids"] = ""; //預訂單子表id，int类型
                //domBody[i]["cpreordercode"] = ""; //预订单号，int类型
                //domBody[i]["idemandtype"] = ""; //需求跟踪方式，int类型
                //domBody[i]["cdemandcode"] = ""; //需求分类代号，string类型
                //domBody[i]["cdemandmemo"] = ""; //需求分类说明，string类型
                //domBody[i]["cinvdefine8"] = ""; //存货自定义项8，string类型
                //domBody[i]["cinvdefine9"] = ""; //存货自定义项9，string类型
                //domBody[i]["cinvdefine10"] = ""; //存货自定义项10，string类型
                //domBody[i]["cinvdefine11"] = ""; //存货自定义项11，string类型
                //domBody[i]["cinvdefine12"] = ""; //存货自定义项12，string类型
                //domBody[i]["cinvdefine13"] = ""; //存货自定义项13，string类型
                //domBody[i]["cinvdefine14"] = ""; //存货自定义项14，string类型
                //domBody[i]["cinvdefine15"] = ""; //存货自定义项15，string类型
                //domBody[i]["cinvdefine16"] = ""; //存货自定义项16，string类型
                //domBody[i]["cinvdefine2"] = ""; //存货自定义项2，string类型
                //domBody[i]["cinvdefine3"] = ""; //存货自定义项3，string类型
                //domBody[i]["binvtype"] = ""; //存货类型，string类型
                //domBody[i]["cdefine22"] = Convert.ToString(dgvMain.Rows[i].Cells["AutoID"].Value); //表体自定义项1，string类型
                //domBody[i]["cdefine23"] = Convert.ToString(dgvMain.Rows[i].Cells["iPOsID"].Value); //表体自定义项2，string类型
                //domBody[i]["cdefine24"] = ""; //表体自定义项3，string类型
                //domBody[i]["cdefine25"] = ""; //表体自定义项4，string类型
                //domBody[i]["cdefine26"] = ""; //表体自定义项5，double类型
                domBody[i]["cdefine27"] = iQuantity; //表体自定义项6，double类型
                domBody[i]["itaxrate"] = iTaxRate; //税率（％），double类型
                domBody[i]["kl2"] = "100"; //扣率2（％），double类型
                //domBody[i]["citemcode"] = ""; //项目编码，string类型
                //domBody[i]["citem_class"] = ""; //项目大类编码，string类型
                //domBody[i]["dkl1"] = ""; //倒扣1（％），double类型
                //domBody[i]["dkl2"] = ""; //倒扣2（％），double类型
                //domBody[i]["citemname"] = ""; //项目名称，string类型
                //domBody[i]["citem_cname"] = ""; //项目大类名称，string类型
                //domBody[i]["cfree3"] = ""; //自由项3，string类型
                //domBody[i]["cfree4"] = ""; //自由项4，string类型
                //domBody[i]["cfree5"] = ""; //自由项5，string类型
                //domBody[i]["cfree6"] = ""; //自由项6，string类型
                //domBody[i]["cfree7"] = ""; //自由项7，string类型
                //domBody[i]["cfree8"] = ""; //自由项8，string类型
                //domBody[i]["cfree9"] = ""; //自由项9，string类型
                //domBody[i]["cfree10"] = ""; //自由项10，string类型
                domBody[i]["cdefine28"] = ZXSPBM; //表体自定义项7，string类型  统编代码
                domBody[i]["cdefine29"] =ClsSystem.gnvl( dts.Rows[i]["DDMXBH"],""); //表体自定义项8，string类型 订单明细编号
                domBody[i]["cdefine30"] = ClsSystem.gnvl(dts.Rows[i]["CGJLDW"], ""); //表体自定义项9，string类型 采购计量单位
                //domBody[i]["cdefine31"] = ""; //表体自定义项10，string类型
                domBody[i]["cdefine32"] = ClsSystem.gnvl(dts.Rows[i]["PSDBM"], "");// 医院配送点编码 //表体自定义项11，string类型
                //domBody[i]["corufts"] = ""; //对应单据时间戳，string类型
                domBody[i]["cdefine33"] = ClsSystem.gnvl(dts.Rows[i]["PSDZ"], ""); // 配送地址//表体自定义项12，string类型
                //domBody[i]["cdefine34"] = ""; //表体自定义项13，int类型
                //domBody[i]["cdefine35"] = ""; //表体自定义项14，int类型
                //domBody[i]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                //domBody[i]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                //domBody[i]["binvmodel"] = ""; //是否模型件，int类型
                //domBody[i]["csrpolicy"] = ""; //供需政策，string类型
                //domBody[i]["iprekeepquantity"] = ""; //预留数量，double类型
                //domBody[i]["iprekeepnum"] = ""; //预留件数，double类型
                //domBody[i]["iprekeeptotquantity"] = ""; //预留母件和子件数量，double类型
                //domBody[i]["iprekeeptotnum"] = ""; //预留母件子件件数，double类型
                //domBody[i]["fcusminprice"] = ""; //客户最低售价，double类型
                //domBody[i]["ccusinvcode"] = ""; //客户存货编码，string类型
                //domBody[i]["ccusinvname"] = ""; //客户存货名称，string类型
                //domBody[i]["cinvaddcode"] = ""; //存货代码，string类型
                //domBody[i]["dbclosedate"] = ""; //关闭日期，DateTime类型
                //domBody[i]["dbclosesystime"] = ""; //关闭时间，DateTime类型
                domBody[i]["kl"] = "100"; //扣率（％），double类型
                //domBody[i]["cAssComUnitCode"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cAssComUnitCode     from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", Program.ConnectionString), ""); ; //扣率（％），double类型
            }
            //给普通参数VoucherState赋值。此参数的数据类型为int，此参数按值传递，表示状态:0增加;1修改
            broker.AssignNormalValue("VoucherState", 0);

            //该参数vNewID为INOUT型普通参数。此参数的数据类型为string，此参数按值传递。在API调用返回时，可以通过GetResult("vNewID")获取其值
            broker.AssignNormalValue("vNewID", "");

#if DEBUG
            domHead.ToXmlDoc().Save(@"c:\order_head.xml");
            domBody.ToXmlDoc().Save(@"c:\order_body.xml");
#endif
            //给普通参数DomConfig赋值。此参数的数据类型为MSXML2.IXMLDOMDocument2，此参数按引用传递，表示ATO,PTO选配
            MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("DomConfig", domMsg);

            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        throw new Exception("系统异常：" + sysEx.Message);
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        throw new Exception("API异常：" + bizEx.Message);
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        throw new Exception("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
                return "API错误:" + apiEx.Message;
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.String，此参数按值传递，表示成功为空串
            result = broker.GetReturnValue() as System.String;
            if (result == null || result == "")
            {
            }
            else
            {
                broker.Release();
               return "API错误:"+result;
            }

            //获取out/inout参数值

            //获取普通INOUT参数vNewID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
            string vNewIDRet = broker.GetResult("vNewID") as string;
            //DataTable dtRet = AdoAccess.ExecuteDT("select cSOCode from SO_SOMain where ID = '" + vNewIDRet + "'", conn);
            //if (dtRet.Rows.Count > 0)
            //{
            //    dt.Rows[iRow]["cSOCode"] = Convert.ToString(dtRet.Rows[0]["cSOCode"]);
            //}

            //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");
            ////MSXML2.DOMDocument domResultHead = (MSXML2.DOMDocumentClass)broker.GetResult("domHead");

            ////IXMLDOMNodeList listHead = domResultBody.selectNodes("//rs:data/z:row");

            //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");

            //for (int i = 0; i < list.length; i++)
            //{
            //    IXMLDOMNode node = list[i].attributes.getNamedItem("isosid");
            //    dts.Rows[i]["iSOsID"] = Convert.ToString(node.nodeValue);
            //    //node = list[i].attributes.getNamedItem("irowno");
            //    //dts.Rows[i]["iOrderRowNo"] = Convert.ToString(node.nodeValue);


            //}


            //结束本次调用，释放API资源
            broker.Release();
            return vNewIDRet;
            #endregion         

        }

        public static string InDis(U8Login.clsLogin u8Login, string connstring, string BWB, string RunSheetNo, DataSet dt)
        {

          //  ADODB.Connection conn = new ADODB.Connection();
            try
            {           

            #region 发货单
            string BarCode = "";

            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            //销售所有接口均支持内部独立事务和外部事务，默认内部事务
            //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
            //envContext.BizDbConnection = conn;
            //envContext.IsIndependenceTransaction = false;

            //conn.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
            //conn.BeginTrans();


            //设置上下文参数
            envContext.SetApiContext("VoucherType", 9); //上下文数据类型：int，含义：单据类型：9

            //第三步：设置API地址标识(Url)
            //当前API错误：新增或修改的地址标识为：U8API/Consignment/Save
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/Consignment/Save");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给BO表头参数domHead赋值，此BO参数的业务类型为发货单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数domHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domHead = broker.GetBoParam("domHead");
            domHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            decimal iTaxRate = 17;// Public.GetNum(dt.Rows[iRow]["iTaxRate"]);
            
         //   string cSOCode = "";
            string cdlCode = DateTime.Today.ToString("yyyyMMdd") + RunSheetNo; ////新的单号发货单

//            string sql = @"select m.cSOCode,m.iTaxRate   from SO_SODetails s with(nolock)
//                     join SO_SOMain m with(nolock) on s.ID=m.ID     
//                     LEFT join SO_SODetails_extradefine  as ex with(nolock) on s.iSOsID=ex.iSOsID  where ex.cbdefine1='" + ClsSystem.gnvl(dgvList.Rows[0].Cells["cBarCode"].Value, "") + "'";

            //DataTable sodb = SqlAccess.ExecuteSqlDataTable(sql, Program.objConnection);

            //if (sodb.Rows.Count > 0)
            //{
            //    cSOCode = ClsSystem.gnvl(sodb.Rows[0]["cSOCode"], "");
            //    iTaxRate = Convert.ToDecimal(sodb.Rows[0]["iTaxRate"]);
            //}
            //else
            //{

            //    return "APIAPI:请确定条码" + ClsSystem.gnvl(dgvList.Rows[0].Cells["cBarCode"].Value, "") + "对应的销售订单数据是否正确";
            //}
            //string  cSOCode = ClsSystem.gnvl(SqlAccess.ExecuteScalar(sql, Program.objConnection), "");

            /****************************** 以下是必输字段 ****************************/
            domHead[0]["dlid"] = "0"; //主关键字段，int类型
            domHead[0]["cdlcode"] = cdlCode; //发货单号，string类型
            domHead[0]["ddate"] = DateTime.Today.ToShortDateString(); //发货日期，DateTime类型
            domHead[0]["cbustype"] = "普通销售"; //业务类型，int类型
            domHead[0]["cstname"] = ""; //销售类型，string类型
            domHead[0]["ccusabbname"] = ""; //客户简称，string类型
            domHead[0]["cdepname"] = ""; //销售部门，string类型
            domHead[0]["cstcode"] = "01"; //销售类型编码，string类型

            /***************************** 以下是非必输字段 ****************************/
            //domHead[0]["caddcode"] = ""; //收货地址编码，string类型
            //domHead[0]["cdeliverunit"] = ""; //收货单位，string类型
            //domHead[0]["ccontactname"] = ""; //收货联系人，string类型
            //domHead[0]["cofficephone"] = ""; //收货联系电话，string类型
            //domHead[0]["cmobilephone"] = ""; //收货联系人手机，string类型
            //domHead[0]["fstockquanO"] = ""; //现存件数，double类型
            //domHead[0]["fcanusequanO"] = ""; //可用件数，double类型
            //domHead[0]["iverifystate"] = "0"; //iverifystate，string类型
            //domHead[0]["ireturncount"] = "0"; //ireturncount，string类型
            //domHead[0]["icreditstate"] = ""; //icreditstate，string类型
            //domHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，string类型
            domHead[0]["csocode"] = ""; //订单号，string类型
            //domHead[0]["csbvcode"] = ""; //发票号，string类型
            //domHead[0]["cpersonname"] = ""; //业 务 员，string类型
            //domHead[0]["cshipaddress"] = ""; //发货地址，string类型
            //domHead[0]["cscname"] = ""; //发运方式，string类型
            //domHead[0]["cpayname"] = ""; //付款条件，string类型
            domHead[0]["itaxrate"] = iTaxRate; //税率，double类型
            domHead[0]["cexch_name"] = BWB; //币种，string类型
            domHead[0]["iexchrate"] = "1"; //汇率，double类型
            //domHead[0]["cmemo"] = ""; //备    注，string类型
            domHead[0]["cmaker"] = "姚梁"; //制单人，string类型
            //      domHead[0]["cverifier"] = ""; //审核人，string类型
            //domHead[0]["ccloser"] = ""; //关闭人，string类型
            //domHead[0]["ccuspaycond"] = ""; //客户付款条件，string类型
            //domHead[0]["sbvid"] = ""; //销售发票ID，string类型
            //domHead[0]["isale"] = ""; //是否先发货，string类型
            domHead[0]["ivtid"] = "71"; //单据模版号，int类型
            domHead[0]["ccusname"] = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cCusName from customer where cCusCode ='20072'", Public.ufconnstr_111), ""); //客户名称，string类型
            //domHead[0]["ccusphone"] = ""; //联系电话，string类型
            //domHead[0]["ccusperson"] = ""; //联系人，string类型
            //domHead[0]["ccuspostcode"] = ""; //邮政编码，string类型
            //domHead[0]["icuscreline"] = ""; //用户信用度，double类型
            //domHead[0]["ccusaddress"] = ""; //客户地址，string类型
            //domHead[0]["iarmoney"] = ""; //客户应收余额，double类型
            //domHead[0]["cpersoncode"] = ""; //业务员编码，string类型
            //domHead[0]["bfirst"] = ""; //期初标志，string类型
            domHead[0]["cdepcode"] = "0700"; //部门编码，string类型
            //domHead[0]["cvouchname"] = "发货单"; //单据类型名称，int类型
            domHead[0]["cvouchtype"] = "05"; //单据类型编码，string类型
            //domHead[0]["cmodifier"] = ""; //修改人，string类型
            //domHead[0]["dmoddate"] = ""; //修改日期，DateTime类型
            //   domHead[0]["dverifydate"] =""; //审核日期，DateTime类型
            //domHead[0]["csvouchtype"] = ""; //csvouchtype，string类型
            domHead[0]["dcreatesystime"] = DateTime.Now.ToShortDateString(); //制单时间，DateTime类型
            //   domHead[0]["dverifysystime"] = ""; //审核时间，DateTime类型
            //domHead[0]["dmodifysystime"] = ""; //修改时间，DateTime类型
            domHead[0]["ccuscode"] = "20072"; //客户编码，string类型
            //domHead[0]["csccode"] = ""; //发运方式编码，string类型
            //domHead[0]["cpaycode"] = ""; //付款条件编码，string类型
            domHead[0]["breturnflag"] = "0"; //退货标识，string类型
            //domHead[0]["brefdisp"] = ""; //单据来源，string类型
            //domHead[0]["ccrechpname"] = ""; //信用审核人，string类型
            //domHead[0]["fstockquan"] = ""; //现存数量，double类型
            //domHead[0]["fcanusequan"] = ""; //可用数量，double类型
            //domHead[0]["ccusdefine1"] = ""; //客户自定义项1，string类型
            //domHead[0]["ccusdefine2"] = ""; //客户自定义项2，string类型
            //domHead[0]["ccusdefine3"] = ""; //客户自定义项3，string类型
            //domHead[0]["ccusdefine4"] = ""; //客户自定义项4，string类型
            //domHead[0]["ccusdefine5"] = ""; //客户自定义项5，string类型
            //domHead[0]["ccusdefine6"] = ""; //客户自定义项6，string类型
            //domHead[0]["ccusdefine7"] = ""; //客户自定义项7，string类型
            //domHead[0]["ccusdefine8"] = ""; //客户自定义项8，string类型
            //domHead[0]["ccusdefine9"] = ""; //客户自定义项9，string类型
            //domHead[0]["ccusdefine10"] = ""; //客户自定义项10，string类型
            //domHead[0]["ccusdefine11"] = ""; //客户自定义项11，string类型
            //domHead[0]["ccusdefine12"] = ""; //客户自定义项12，string类型
            //domHead[0]["ccusdefine13"] = ""; //客户自定义项13，string类型
            //domHead[0]["ccusdefine14"] = ""; //客户自定义项14，string类型
            //domHead[0]["ccusdefine15"] = ""; //客户自定义项15，string类型
            //domHead[0]["ccusdefine16"] = ""; //客户自定义项16，string类型
            domHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            domHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            //domHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
            //domHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
            //domHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            //domHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            //domHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            domHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            //domHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            //domHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            domHead[0]["cdefine11"] = RunSheetNo; //表头自定义项11，string类型
            //domHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            //domHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            //domHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            //domHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            //domHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            //domHead[0]["ufts"] = ""; //时间戳，string类型
            //domHead[0]["zdsumdx"] = ""; //整单合计（大写），string类型
            //domHead[0]["isumdx"] = ""; //价税合计（大写），string类型
            //domHead[0]["zdsum"] = ""; //整单合计，double类型
            //domHead[0]["isumx"] = ""; //价税合计，double类型
            //domHead[0]["ccrechppass"] = ""; //信用审核口令，string类型
            //domHead[0]["clowpricepass"] = ""; //最低售价口令，string类型
            domHead[0]["bcredit"] = "0"; //是否为立账单据，int类型
            //domHead[0]["ccreditcuscode"] = ""; //信用单位编码，string类型
            //domHead[0]["ccreditcusname"] = ""; //信用单位名称，string类型
            //domHead[0]["cgatheringplan"] = ""; //收付款协议编码，string类型
            //domHead[0]["cgatheringplanname"] = ""; //收付款协议名称，string类型
            //domHead[0]["dcreditstart"] = ""; //立账日，DateTime类型
            //domHead[0]["dgatheringdate"] = ""; //到期日，DateTime类型
            //domHead[0]["icreditdays"] = ""; //账期，int类型
            //domHead[0]["bcontinue"] = ""; //是否继续，string类型

            //给BO表体参数domBody赋值，此BO参数的业务类型为发货单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");
       //     domBody.RowCount = dgvList.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            decimal iTaxUnitPrice = 0;
            decimal iSum = 0;//原币含税金额
            decimal iUnitPrice = 0;//原币无税单价
            decimal iMoney = 0;//原币无税金额
            decimal iTax = 0;//原币税额

            decimal iNatUnitPrice = 0;//本币无税单价
            decimal iNatSum = 0;//本币价税合计
            decimal iNatMoney = 0;//本币无税金额
            decimal iNatTax = 0;//本币税额
            string cinvcode = "";
            string cinvname = "";
            string iSOsID = "";
            string cgroupcode = "";
            string igrouptype = "";
            string sql = "";
            for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
            {
                cinvcode = ClsSystem.gnvl(dt.Tables[1].Rows[i]["part_code"],"");
                decimal required_part_count = Public.GetNum(dt.Tables[1].Rows[i]["required_part_count"]);
                decimal required_pack_count = Public.GetNum(dt.Tables[1].Rows[i]["required_pack_count"]);

                decimal iQuantity = required_pack_count * required_part_count;//数量

//                sql = @"select  top 1 s.cSOCode ,s.cInvCode,s.iTaxUnitPrice,s.iSOsID,m.cDepCode,s.cInvCode,inv.cInvName,inv.cComUnitCode ,inv.cgroupcode,inv.igrouptype     
//        from SO_SODetails s with(nolock)     
//  
//        join SO_SOMain m with(nolock) on s.ID=m.ID
//        left join inventory inv on inv.cInvCode=s.cInvCode     
//        LEFT join SO_SODetails_extradefine  as ex with(nolock) on s.iSOsID=ex.iSOsID
//         where ex.cbdefine1='" + BarCode + "'";

//                DataTable db = SqlAccess.ExecuteSqlDataTable(sql, Program.objConnection);

//                if (db.Rows.Count > 0)
//                {
                iTaxRate = 17;

                 iUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
               


                iTaxUnitPrice = Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价

                iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额

                 iMoney = Public.ChinaRound(iUnitPrice * iQuantity, 2);//原币无税金额

                    iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                    iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
                    iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
                    iNatMoney = Public.ChinaRound(iNatSum / (1 + iTaxRate / 100M), 2);//本币无税金额
                    iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                    cgroupcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cgroupcode  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring), "");
                    igrouptype = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  igrouptype  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring), ""); 

                //}

                //else
                //{

                //    return "错误API:请确定条码" + BarCode + "对应的销售订单数据是否正确";
                //}


                /****************************** 以下是必输字段 ****************************/
                //domBody[i]["idlsid"] = ""; //主关键字段，0类型
                domBody[i]["cinvname"] = cinvname; //存货名称，string类型
                domBody[i]["cinvcode"] = cinvcode; //存货编码，string类型
                domBody[i]["iquantity"] = iQuantity; //数量，double类型
                domBody[i]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                /***************************** 以下是非必输字段 ****************************/
                //domBody[i]["cwhname"] = ""; //仓库名称，string类型
                //domBody[i]["autoid"] = ""; //自动编号，string类型
                //domBody[i]["ccontractid"] = ""; //合同编码，string类型
                //domBody[i]["ccontractrowguid"] = ""; //合同标的RowGuid，string类型
                //domBody[i]["ccontracttagcode"] = ""; //合同标的编码，string类型
                //domBody[i]["csettleall"] = ""; //关闭标志，int类型
                //domBody[i]["cinvstd"] = ""; //规格型号，string类型
                //domBody[i]["ippartqty"] = ""; //母件数量，string类型
                //domBody[i]["ippartid"] = ""; //母件物料ID，string类型
                //domBody[i]["batomodel"] = ""; //是否ATO件，int类型
                //domBody[i]["ippartseqid"] = ""; //选配序号，string类型
                //domBody[i]["cmassunit"] = ""; //保质期单位，int类型
                domBody[i]["cwhcode"] = "31"; //仓库编码，string类型
                //domBody[i]["inum"] = ""; //件数，double类型
                domBody[i]["itaxunitprice"] = iTaxUnitPrice; //含税单价，double类型
                domBody[i]["iunitprice"] = iUnitPrice; //无税单价，double类型
                //domBody[i]["isettlenum"] = ""; //开票金额，double类型
                //domBody[i]["isettlequantity"] = ""; //开票数量，double类型
                domBody[i]["iquotedprice"] = iTaxUnitPrice; //报价，double类型
                domBody[i]["imoney"] = iMoney; //无税金额，double类型
                domBody[i]["itax"] = iTax; //税额，double类型
                domBody[i]["isum"] = iSum; //价税合计，double类型
                //domBody[i]["cfree1"] = ""; //自由项1，string类型
                //domBody[i]["cfree2"] = ""; //自由项2，string类型
                //domBody[i]["idiscount"] = ""; //折扣额，double类型
                domBody[i]["dlid"] = "0"; //发货单 38，int类型
                //domBody[i]["icorid"] = ""; //原发货单ID，int类型
                domBody[i]["inatunitprice"] = iNatUnitPrice; //本币单价，double类型
                domBody[i]["inatmoney"] = iNatMoney; //本币金额，double类型
                domBody[i]["inattax"] = iNatTax; //本币税额，double类型
                domBody[i]["inatsum"] = iNatSum; //本币价税合计，double类型
                //domBody[i]["inatdiscount"] = ""; //本币折扣额，double类型
                //domBody[i]["iinvlscost"] = ""; //最低售价，double类型
                //domBody[i]["ibatch"] = ""; //批次，string类型
                domBody[i]["itaxrate"] = iTaxRate; //税率（％），double类型
                //domBody[i]["bfree1"] = ""; //是否有自由项1，string类型
                //domBody[i]["bfree2"] = ""; //是否有自由项2，string类型
                //domBody[i]["bfree3"] = ""; //是否有自由项3，string类型
                //domBody[i]["bfree4"] = ""; //是否有自由项4，string类型
                //domBody[i]["bfree5"] = ""; //是否有自由项5，string类型
                //domBody[i]["bfree6"] = ""; //是否有自由项6，string类型
                //domBody[i]["bfree7"] = ""; //是否有自由项7，string类型
                //domBody[i]["bfree8"] = ""; //是否有自由项8，string类型
                //domBody[i]["bfree9"] = ""; //是否有自由项9，string类型
                //domBody[i]["bfree10"] = ""; //是否有自由项10，string类型
                //domBody[i]["cbatch"] = Convert.ToString(dts.Rows[i]["cBatch"]); //批号，string类型
                //domBody[i]["cinvdefine1"] = ""; //存货自定义项1，string类型
                //domBody[i]["cexpirationdate"] = ""; //有效期至，string类型
                //domBody[i]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                //domBody[i]["dexpirationdate"] = ""; //有效期计算项，string类型
                //domBody[i]["bsalepricefree1"] = ""; //是否自由项定价1，string类型
                //domBody[i]["bsalepricefree2"] = ""; //是否自由项定价2，string类型
                //domBody[i]["bsalepricefree3"] = ""; //是否自由项定价3，string类型
                //domBody[i]["bsalepricefree4"] = ""; //是否自由项定价4，string类型
                //domBody[i]["bsalepricefree5"] = ""; //是否自由项定价5，string类型
                //domBody[i]["bsalepricefree6"] = ""; //是否自由项定价6，string类型
                //domBody[i]["bsalepricefree7"] = ""; //是否自由项定价7，string类型
                //domBody[i]["bsalepricefree8"] = ""; //是否自由项定价8，string类型
                //domBody[i]["bsalepricefree9"] = ""; //是否自由项定价9，string类型
                //domBody[i]["bsalepricefree10"] = ""; //是否自由项定价10，string类型
                //domBody[i]["idemandtype"] = ""; //需求跟踪方式，int类型
                //domBody[i]["cdemandcode"] = ""; //需求跟踪号，string类型
                //domBody[i]["cdemandmemo"] = ""; //需求分类说明，string类型
                //domBody[i]["cdemandid"] = ""; //需求跟踪id，string类型
                //domBody[i]["idemandseq"] = ""; //需求跟踪行号，string类型
                //domBody[i]["cbatchproperty1"] = ""; //批次属性1，double类型
                //domBody[i]["cbatchproperty2"] = ""; //批次属性2，double类型
                //domBody[i]["cbatchproperty3"] = ""; //批次属性3，double类型
                //domBody[i]["cbatchproperty4"] = ""; //批次属性4，double类型
                //domBody[i]["cbatchproperty5"] = ""; //批次属性5，double类型
                //domBody[i]["cbatchproperty6"] = ""; //批次属性6，string类型
                //domBody[i]["cbatchproperty7"] = ""; //批次属性7，string类型
                //domBody[i]["cbatchproperty8"] = ""; //批次属性8，string类型
                //domBody[i]["cbatchproperty9"] = ""; //批次属性9，string类型
                //domBody[i]["cbatchproperty10"] = ""; //批次属性10，DateTime类型
                //domBody[i]["bbatchproperty1"] = ""; //是否启用批次属性1，string类型
                //domBody[i]["bbatchproperty2"] = ""; //是否启用批次属性2，string类型
                //domBody[i]["bbatchproperty3"] = ""; //是否启用批次属性3，string类型
                //domBody[i]["bbatchproperty4"] = ""; //是否启用批次属性4，string类型
                //domBody[i]["bbatchproperty5"] = ""; //是否启用批次属性5，string类型
                //domBody[i]["bbatchproperty6"] = ""; //是否启用批次属性6，string类型
                //domBody[i]["bbatchproperty7"] = ""; //是否启用批次属性7，string类型
                //domBody[i]["bbatchproperty8"] = ""; //是否启用批次属性8，string类型
                //domBody[i]["bbatchproperty9"] = ""; //是否启用批次属性9，string类型
                //domBody[i]["bbatchproperty10"] = ""; //是否启用批次属性10，string类型
                //domBody[i]["bbatchcreate"] = ""; //批次属性是否建档，string类型
                //domBody[i]["cinvdefine4"] = ""; //存货自定义项4，string类型
                //domBody[i]["cinvdefine5"] = ""; //存货自定义项5，string类型
                //domBody[i]["cinvdefine6"] = ""; //存货自定义项6，string类型
                //domBody[i]["cinvdefine7"] = ""; //存货自定义项7，string类型
                //domBody[i]["cinvdefine8"] = ""; //存货自定义项8，string类型
                //domBody[i]["cinvdefine9"] = ""; //存货自定义项9，string类型
                //domBody[i]["cinvdefine10"] = ""; //存货自定义项10，string类型
                //domBody[i]["cinvdefine11"] = ""; //存货自定义项11，string类型
                //domBody[i]["cinvdefine12"] = ""; //存货自定义项12，string类型
                //domBody[i]["cinvdefine13"] = ""; //存货自定义项13，string类型
                //domBody[i]["cinvdefine14"] = ""; //存货自定义项14，string类型
                //domBody[i]["cinvdefine15"] = ""; //存货自定义项15，string类型
                //domBody[i]["cinvdefine16"] = ""; //存货自定义项16，string类型
                //domBody[i]["cinvdefine2"] = ""; //存货自定义项2，string类型
                //domBody[i]["cinvdefine3"] = ""; //存货自定义项3，string类型
                //domBody[i]["binvtype"] = ""; //存货类型，string类型
                //domBody[i]["itb"] = ""; //退补标志，int类型
                //domBody[i]["dvdate"] = Convert.ToString(dts.Rows[i]["dVDate"]); //失效日期，DateTime类型
                domBody[i]["cdefine22"] = ""; //表体自定义项1，string类型
                //domBody[i]["cdefine23"] = Convert.ToString(dgvMain.Rows[i].Cells["iPOsID"].Value); //表体自定义项2，string类型
                //domBody[i]["cdefine24"] = ""; //表体自定义项3，string类型
                //domBody[i]["cdefine25"] = ""; //表体自定义项4，string类型
                //domBody[i]["cdefine26"] = ""; //表体自定义项5，double类型
                //domBody[i]["cdefine27"] = ""; //表体自定义项6，double类型
                domBody[i]["kl2"] = "100"; //扣率2（％），double类型
                domBody[i]["isosid"] = ""; //对应订单子表ID，int类型
                //domBody[i]["citemcode"] = ""; //项目编码，string类型
                //domBody[i]["citem_class"] = ""; //项目大类编码，string类型
                domBody[i]["csocode"] = ""; //订单号，string类型
                //domBody[i]["iinvweight"] = ""; //单位重量，double类型
                //domBody[i]["dkl1"] = ""; //倒扣1（％），double类型
                //domBody[i]["dkl2"] = ""; //倒扣2（％），double类型
                //domBody[i]["cvenabbname"] = ""; //产地，string类型
                //domBody[i]["fsalecost"] = ""; //零售单价，double类型
                //domBody[i]["fsaleprice"] = ""; //零售金额，double类型
                //domBody[i]["citemname"] = ""; //项目名称，string类型
                //domBody[i]["citem_cname"] = ""; //项目大类名称，string类型
                //domBody[i]["cfree3"] = ""; //自由项3，string类型
                //domBody[i]["cfree4"] = ""; //自由项4，string类型
                //domBody[i]["cfree5"] = ""; //自由项5，string类型
                //domBody[i]["cfree6"] = ""; //自由项6，string类型
                //domBody[i]["cfree7"] = ""; //自由项7，string类型
                //domBody[i]["cfree8"] = ""; //自由项8，string类型
                //domBody[i]["cfree9"] = ""; //自由项9，string类型
                //domBody[i]["cfree10"] = ""; //自由项10，string类型
                //domBody[i]["corufts"] = ""; //对应单据时间戳，string类型
                //domBody[i]["inufts"] = ""; //入库单时间戳，string类型
                //domBody[i]["iretquantity"] = ""; //退货数量，double类型
                //domBody[i]["iinvexchrate"] = ""; //换算率，double类型
                //domBody[i]["cunitid"] = ""; //销售单位编码，string类型
                //domBody[i]["cinva_unit"] = ""; //销售单位，string类型
                //domBody[i]["cinvm_unit"] = ""; //主计量单位，string类型
                domBody[i]["cgroupcode"] = cgroupcode; //计量单位组，string类型
                domBody[i]["igrouptype"] = igrouptype; //单位类型，uint类型
                //domBody[i]["cdefine28"] = ""; //表体自定义项7，string类型
                //domBody[i]["cdefine29"] = ""; //表体自定义项8，string类型
                //domBody[i]["cdefine30"] = ""; //表体自定义项9，string类型
                //domBody[i]["cdefine31"] = ""; //表体自定义项10，string类型
                //domBody[i]["cdefine32"] = ""; //表体自定义项11，string类型
                //domBody[i]["cdefine33"] = ""; //表体自定义项12，string类型
                //domBody[i]["fsumsignquantity"] = ""; //累计签回数量，double类型
                //domBody[i]["cvmivencode"] = ""; //供货商编码，string类型
                //domBody[i]["cvmivenname"] = ""; //供货商名称，string类型
                domBody[i]["cordercode"] = ""; //订单号，string类型
                domBody[i]["iorderrowno"] = i + 1; //订单行号，string类型
                //domBody[i]["fcusminprice"] = ""; //客户最低售价，double类型
                //domBody[i]["imoneysum"] = ""; //累计本币收款金额，double类型
                //domBody[i]["iexchsum"] = ""; //累计原币收款金额，double类型
                //domBody[i]["cdefine34"] = ""; //表体自定义项13，int类型
                //domBody[i]["fsumsignnum"] = ""; //累计签回件数，double类型
                //domBody[i]["cdefine35"] = ""; //表体自定义项14，int类型
                //domBody[i]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                //domBody[i]["funsignquantity"] = ""; //可签收数量，double类型
                //domBody[i]["funsignnum"] = ""; //可签收件数，double类型
                //domBody[i]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                //domBody[i]["dmdate"] = ""; //生产日期，DateTime类型
                //domBody[i]["bgsp"] = ""; //是否gsp检验，int类型
                //domBody[i]["imassdate"] = Convert.ToString(dts.Rows[i]["iMassDate"]); //保质期，int类型
                //domBody[i]["binvquality"] = ""; //是否保质期管理，int类型
                //domBody[i]["ccode"] = ""; //入库单号，string类型
                //domBody[i]["btrack"] = ""; //是否追踪，int类型
                //domBody[i]["bproxywh"] = ""; //是否代管仓，int类型
                //domBody[i]["bisstqc"] = ""; //库存期初，int类型
                //domBody[i]["csrpolicy"] = ""; //供需政策，string类型
                //domBody[i]["cinvaddcode"] = ""; //存货代码，string类型
                //domBody[i]["iqanum"] = ""; //检验合格件数，double类型
                //domBody[i]["iqaquantity"] = ""; //检验合格数量，double类型
                //domBody[i]["ccusinvcode"] = ""; //客户存货编码，string类型
                //domBody[i]["ccusinvname"] = ""; //客户存货名称，string类型
                //domBody[i]["bqachecking"] = ""; //是否在检，int类型
                //domBody[i]["bqaneedcheck"] = ""; //是否质量检验，int类型
                //domBody[i]["bqachecked"] = ""; //是否报检，int类型
                //domBody[i]["bqaurgency"] = ""; //是否急料，int类型
                //domBody[i]["cbaccounter"] = ""; //记账人，string类型
                //domBody[i]["binvbatch"] = ""; //是否批次管理，string类型
                //domBody[i]["bsettleall"] = ""; //结算标志，string类型
                //domBody[i]["bservice"] = ""; //是否应税劳务，string类型
                domBody[i]["kl"] = "100"; //扣率（％），double类型
            }

            //给普通参数VoucherState赋值。此参数的数据类型为int，此参数按值传递，表示状态:0增加;1修改
            broker.AssignNormalValue("VoucherState", 0);

            //该参数vNewID为INOUT型普通参数。此参数的数据类型为string，此参数按值传递。在API调用返回时，可以通过GetResult("vNewID")获取其值
            broker.AssignNormalValue("vNewID", "");

            //给普通参数DomConfig赋值。此参数的数据类型为MSXML2.IXMLDOMDocument2，此参数按引用传递，表示ATO,PTO选配
            MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("DomConfig", domMsg);

            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        throw new Exception("系统异常：" + sysEx.Message);
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        throw new Exception("API异常：" + bizEx.Message);
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        throw new Exception("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
            //    conn.RollbackTrans();
                return "U8API错误:";
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.String，此参数按值传递，表示成功返回空串
            System.String result = broker.GetReturnValue() as System.String;
            if (result == null || result == "")
            {
            }
            else
            {

                broker.Release();
             //   conn.RollbackTrans();
                //    throw new Exception(result);
                return "U8API错误：" + result;
            }

            //获取out/inout参数值

            //获取普通INOUT参数vNewID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
            string vNewIDRet = broker.GetResult("vNewID") as string;
            //DataTable dtRet = AdoAccess.ExecuteDT("select cDLCode from DispatchList where DLID = '" + vNewIDRet + "'", conn);
            //if (dtRet.Rows.Count > 0)
            //{
            //    dgvMain.Rows[iRow].Cells["cDLCode"].Value = Convert.ToString(dtRet.Rows[0]["cDLCode"]);
            //}



            //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");
            //MSXML2.DOMDocument domResultHead = (MSXML2.DOMDocumentClass)broker.GetResult("domHead");

            //IXMLDOMNodeList listHead = domResultBody.selectNodes("//rs:data/z:row");

            //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");

            //for (int i = 0; i < list.length; i++)
            //{
            //    IXMLDOMNode node = list[i].attributes.getNamedItem("idlsid");
            //    dts.Rows[i]["iDLsID"] = Convert.ToString(node.nodeValue);

            //}

            //结束本次调用，释放API资源
            broker.Release();
        //    conn.CommitTrans();
            return vNewIDRet;

            #endregion

            }
            catch (Exception)
            {
             //   conn.RollbackTrans();
                throw;
            }

        }

        private void InPU(U8Login.clsLogin u8Login, string connstring, string BWB, ref DataTable dt, ref DataTable dts, int iRow, string ExAction)
        {
            #region 请购单
            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            //采购所有接口均支持内部独立事务和外部事务，默认内部事务
            //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
            //envContext.BizDbConnection = conn;
            //envContext.IsIndependenceTransaction = false;

            //设置上下文参数
            envContext.SetApiContext("VoucherType", 0); //上下文数据类型：int，含义：单据类型，采购请购单 0 
            envContext.SetApiContext("bPositive", true); //上下文数据类型：bool，含义：红蓝标识：True,蓝字
            envContext.SetApiContext("sBillType", ""); //上下文数据类型：string，含义：为空串
            envContext.SetApiContext("sBusType", "普通采购"); //上下文数据类型：string，含义：业务类型：普通采购,直运采购,受托代销

            //第三步：设置API地址标识(Url)
            //当前API错误：新增或修改的地址标识为：U8API/PurchaseRequisition/VoucherSave
            U8ApiAddress myApiAddress = null;
            if (ExAction == "C")
            {
                myApiAddress = new U8ApiAddress("U8API/PurchaseRequisition/VoucherSave");
            }
            else
            {
                myApiAddress = new U8ApiAddress("U8API/PurchaseRequisition/Delete");
            }

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给BO表头参数DomHead赋值，此BO参数的业务类型为采购请购单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数DomHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject DomHead = broker.GetBoParam("DomHead");
            DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            /****************************** 以下是必输字段 ****************************/
            if (ExAction == "C")
            {
                DomHead[0]["id"] = Convert.ToString(dt.Rows[iRow]["ID"]); //主关键字段，int类型
            }
            else
            {
                DomHead[0]["id"] = Convert.ToString(dt.Rows[iRow]["APPID"]); //主关键字段，int类型
            }
            DomHead[0]["ccode"] = Convert.ToString(dt.Rows[iRow]["cCode"]); //单据号，string类型
            DomHead[0]["ddate"] = Convert.ToDateTime(dt.Rows[iRow]["dDate"]); //日期，DateTime类型

            /***************************** 以下是非必输字段 ****************************/
            DomHead[0]["ipresent"] = ""; //现存量，string类型
            DomHead[0]["cmaketime"] = Convert.ToDateTime(dt.Rows[iRow]["cMakeTime"]); //制单时间，DateTime类型
            //DomHead[0]["cmodifytime"] = ""; //修改时间，DateTime类型
            //DomHead[0]["caudittime"] = ""; //审核时间，DateTime类型
            //DomHead[0]["cauditdate"] = ""; //审核日期，DateTime类型
            //DomHead[0]["cmodifydate"] = ""; //修改日期，DateTime类型
            //DomHead[0]["creviser"] = ""; //修改人，string类型
            DomHead[0]["cbustype"] = "普通采购"; //业务类型，int类型
            //DomHead[0]["cdepname"] = ""; //请购部门，string类型
            //DomHead[0]["cpersonname"] = ""; //请购人员，string类型
            //DomHead[0]["iverifystateex"] = ""; //审核状态，string类型
            //DomHead[0]["ireturncount"] = ""; //打回次数，string类型
            DomHead[0]["iswfcontrolled"] = "0"; //是否启用工作流，string类型
            //DomHead[0]["cptname"] = ""; //采购类型，string类型
            DomHead[0]["cmaker"] = Convert.ToString(dt.Rows[iRow]["cMaker"]); //制单人，string类型
            //DomHead[0]["cverifier"] = ""; //审核人，string类型
            //DomHead[0]["ccloser"] = ""; //关闭人，string类型
            DomHead[0]["ivtid"] = "8171"; //单据模版号，int类型
            //DomHead[0]["cdepcode"] = ""; //请购部门编码，string类型
            //DomHead[0]["cptcode"] = ""; //采购类型编码，string类型
            //DomHead[0]["clocker"] = ""; //锁定人，string类型
            //DomHead[0]["cpersoncode"] = ""; //请购员编码，string类型
            //DomHead[0]["ufts"] = Convert.ToString(dt.Rows[iRow]["ufts"]); //时间戳，string类型
            DomHead[0]["cdefine1"] = Convert.ToString(dt.Rows[iRow]["cCusCode"]); //表头自定义项1，string类型
            //DomHead[0]["cdefine2"] = Convert.ToString(dt.Rows[iRow]["cWhCode"]); //表头自定义项2，string类型
            //DomHead[0]["cdefine3"] = cCode; //表头自定义项3，string类型
            //DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
            //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            //DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
            //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型

            //给BO表体参数domBody赋值，此BO参数的业务类型为采购请购单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())
            //子表



            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");
            domBody.RowCount = dts.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            for (int j = 0; j < dts.Rows.Count; j++)
            {

                /****************************** 以下是必输字段 ****************************/
                //domBody[j]["autoid"] = ""; //主关键字段，int类型
                domBody[j]["cinvcode"] = Convert.ToString(dts.Rows[j]["cInvCode"]); //存货编码，string类型
                domBody[j]["fquantity"] = Convert.ToDouble(dts.Rows[j]["fQuantity"]); //数量，double类型
                domBody[j]["drequirdate"] = Convert.ToDateTime(dts.Rows[j]["dRequirDate"]); //需求日期，DateTime类型
                domBody[j]["cexch_name"] = BWB; //币种，string类型
                domBody[j]["iexchrate"] = 1; //汇率，double类型
                if (ExAction == "C")
                {
                    domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
                    domBody[j]["id"] = Convert.ToString(dt.Rows[iRow]["ID"]); //主表id，int类型
                }
                else
                {
                    domBody[j]["editprop"] = "D";
                    domBody[j]["id"] = Convert.ToString(dt.Rows[iRow]["APPID"]); //主表id，int类型
                }

                /***************************** 以下是非必输字段 ****************************/
                //domBody[j]["cinvstd"] = ""; //规格型号，string类型
                //domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                //domBody[j]["cfree1"] = ""; //自由项1，string类型
                //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                //domBody[j]["cfree2"] = ""; //自由项2，string类型
                //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                //domBody[j]["cdefine22"] = ""; //表体自定义项1，string类型
                //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                //domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                //domBody[j]["cdefine24"] = ""; //表体自定义项3，string类型
                //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                //domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                //domBody[j]["cdefine26"] = ""; //表体自定义项5，double类型
                //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                //domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                //domBody[j]["citemcode"] = ""; //项目编码，string类型
                //domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                //domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                //domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                //domBody[j]["citemname"] = ""; //项目名称，string类型
                //domBody[j]["citem_name"] = ""; //项目大类名称，string类型
                //domBody[j]["cfree3"] = ""; //自由项3，string类型
                //domBody[j]["cfree4"] = ""; //自由项4，string类型
                //domBody[j]["cfree5"] = ""; //自由项5，string类型
                //domBody[j]["cfree6"] = ""; //自由项6，string类型
                //domBody[j]["cfree7"] = ""; //自由项7，string类型
                //domBody[j]["cfree8"] = ""; //自由项8，string类型
                //domBody[j]["cfree9"] = ""; //自由项9，string类型
                //domBody[j]["cfree10"] = ""; //自由项10，string类型
                //domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
                //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                //domBody[j]["cdefine34"] = Convert.ToString(dts.Rows[j]["iID"]); //表体自定义项13，int类型
                //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                //domBody[j]["ftaxprice"] = ""; //本币含税单价，double类型
                //domBody[j]["funitprice"] = ""; //本币单价，double类型
                //domBody[j]["fmoney"] = ""; //本币价税合计，double类型
                domBody[j]["ipertaxrate"] = 17; //税率，double类型
                //domBody[j]["darrivedate"] = ""; //建议订货日期，DateTime类型

                domBody[j]["btaxcost"] = 1; //单价标准，int类型
                domBody[j]["cvencode"] = Convert.ToString(dts.Rows[j]["cVenCode"]); //供应商编码，string类型
                //domBody[j]["cvenabbname"] = ""; //供应商，string类型
                //domBody[j]["cinvname"] = ""; //存货名称，string类型
                //domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                //domBody[j]["cinvm_unit"] = ""; //主计量，string类型
                //domBody[j]["imrpsid"] = ""; //mrp子表id，int类型
                //domBody[j]["csource"] = ""; //来源标志，string类型
                //domBody[j]["ippartid"] = ""; //母件Id，int类型
                //domBody[j]["ipquantity"] = ""; //母件数量，int类型
                //domBody[j]["iptoseq"] = ""; //选配序号，int类型
                //domBody[j]["sotype"] = ""; //需求跟踪方式，int类型
                //domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                //domBody[j]["irowno"] = j + 1; //需求跟踪行号，string类型
                //domBody[j]["sodid"] = ""; //需求跟踪子表ID，string类型
                //domBody[j]["fnum"] = ""; //件数，double类型
                //domBody[j]["cunitid"] = ""; //单位编码，string类型
                //domBody[j]["cinva_unit"] = ""; //采购单位，string类型
                //domBody[j]["iinvexchrate"] = ""; //换算率，double类型
                //domBody[j]["igrouptype"] = ""; //分组类型，string类型
                //domBody[j]["iropsid"] = ""; //rop子表id，int类型
                //domBody[j]["cpersoncodeexec"] = ""; //执行采购员，string类型
                //domBody[j]["cpersonnameexec"] = ""; //执行采购员名称，string类型
                //domBody[j]["cdepcodeexec"] = ""; //执行部门，string类型
                //domBody[j]["cdepnameexec"] = ""; //执行部门名称，string类型
                //domBody[j]["ioricost"] = ""; //原币单价，double类型
                //domBody[j]["iorimoney"] = ""; //原币金额，double类型
                //domBody[j]["ioritaxprice"] = ""; //原币税额，double类型
                //domBody[j]["imoney"] = ""; //本币金额，double类型
                //domBody[j]["itaxprice"] = ""; //本币税额，double类型
                //domBody[j]["iorisum"] = ""; //原币价税合计，double类型
                //domBody[j]["ioritaxcost"] = ""; //含税单价，double类型
                //domBody[j]["cbcloser"] = ""; //行关闭人，string类型
                //domBody[j]["iinvmpcost"] = ""; //最高进价，double类型
                //domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                //domBody[j]["cdetailsdemandmemo"] = ""; //子件需求分类说明 ，string类型
                //domBody[j]["cdemandcode"] = ""; //子件需求分类代号 ，string类型


            }



            if (ExAction == "C")
            {
                //给普通参数VoucherState赋值。此参数的数据类型为int，此参数按值传递，表示单据状态：2新增，1修改，0非编辑
                broker.AssignNormalValue("VoucherState", 2);
                //给普通参数UserMode赋值。此参数的数据类型为int，此参数按值传递，表示模式，0：CS;1:BS
                broker.AssignNormalValue("UserMode", 0);
            }

            //该参数curID为OUT型参数，由于其数据类型为string，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("curID")获取其值

            //该参数CurDom为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
            //MSXML2.IXMLDOMDocument2 CurDom = new MSXML2.IXMLDOMDocument2();
            MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("CurDom", domMsg);



            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        throw new Exception("系统异常：" + sysEx.Message);
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        throw new Exception("API异常：" + bizEx.Message);
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        throw new Exception("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.String，此参数按值传递，表示API描述：空，正确；非空，API
            System.String result = broker.GetReturnValue() as System.String;
            if (result == null || result == "")
            {
            }
            else
            {
                broker.Release();
                throw new Exception(result);
            }

            //获取out/inout参数值

            //获取普通OUT参数curID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
            string curIDRet = broker.GetResult("curID") as string;

            dt.Rows[iRow]["iAppID"] = curIDRet;

            //BusinessObject domHeadRet = broker.GetBoParam("DomHead");
            //获取普通OUT参数CurDom。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
            //MSXML2.DOMDocument CurDomRet = (MSXML2.DOMDocument)(broker.GetResult("CurDom"));

            MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");

            IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");

            for (int i = 0; i < list.length; i++)
            {
                IXMLDOMNode node = list[i].attributes.getNamedItem("autoid");
                dts.Rows[i]["iAppsID"] = Convert.ToString(node.nodeValue);
                //node = list[i].attributes.getNamedItem("irowno");
                //dts.Rows[i]["iOrderRowNo"] = Convert.ToString(node.nodeValue);


            }


            //结束本次调用，释放API资源
            broker.Release();
            #endregion

        }

        private void InSA(U8Login.clsLogin u8Login, string connstring, string BWB, DataTable dt, DataTable dts, int iRow)
        {
            #region 普通发票

            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            //销售所有接口均支持内部独立事务和外部事务，默认内部事务
            //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
            //envContext.BizDbConnection = conn;
            //envContext.IsIndependenceTransaction = false;

            //设置上下文参数
            envContext.SetApiContext("VoucherType", 2); //上下文数据类型：int，含义：单据类型：2

            //第三步：设置API地址标识(Url)
            //当前API错误：新增或修改的地址标识为：U8API/NormalInvoice/Save
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/NormalInvoice/Save");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给BO表头参数domHead赋值，此BO参数的业务类型为销售普通发票，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数domHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domHead = broker.GetBoParam("domHead");
            domHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            decimal iTaxRate = Public.GetNum(dt.Rows[iRow]["iTaxRate"]);


            /****************************** 以下是必输字段 ****************************/
            domHead[0]["sbvid"] = "0"; //主关键字段，int类型
            domHead[0]["csbvcode"] = Convert.ToString(dt.Rows[iRow]["cSOCode"]); //发票号，string类型
            domHead[0]["ddate"] = Convert.ToString(dt.Rows[iRow]["dDate"]); //开票日期，DateTime类型
            //domHead[0]["cstname"] = ""; //销售类型，string类型
            //domHead[0]["ccusabbname"] = ""; //客户简称，string类型
            //domHead[0]["cdepname"] = ""; //销售部门，string类型
            domHead[0]["cstcode"] = "01"; //销售类型编号，string类型

            /***************************** 以下是非必输字段 ****************************/
            //domHead[0]["fstockquanO"] = ""; //现存件数，double类型
            //domHead[0]["fcanusequanO"] = ""; //可用件数，double类型
            domHead[0]["iverifystate"] = "0"; //iverifystate，string类型
            //domHead[0]["ireturncount"] = ""; //ireturncount，string类型
            //domHead[0]["icreditstate"] = ""; //icreditstate，string类型
            domHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，string类型
            //domHead[0]["caddcode"] = ""; //收货地址编码，string类型
            //domHead[0]["cdeliverunit"] = ""; //收货单位，string类型
            //domHead[0]["ccontactname"] = ""; //收货联系人，string类型
            //domHead[0]["cofficephone"] = ""; //收货联系电话，string类型
            //domHead[0]["cmobilephone"] = ""; //收货联系人手机，string类型
            domHead[0]["cbustype"] = "普通销售"; //业务类型，int类型
            domHead[0]["csocode"] = Convert.ToString(dt.Rows[iRow]["cSOCode"]); //订单号，string类型
            domHead[0]["cdlcode"] = Convert.ToString(dt.Rows[iRow]["cSOCode"]); //发货单号，string类型
            //domHead[0]["cpersonname"] = ""; //业 务 员，string类型
            //domHead[0]["cpayname"] = ""; //付款条件，string类型
            //domHead[0]["ccusaddress"] = ""; //客户地址，string类型
            //domHead[0]["ccusphone"] = ""; //联系电话，string类型
            //domHead[0]["ccusbank"] = ""; //开户银行，string类型
            //domHead[0]["ccusaccount"] = ""; //银行账号，string类型
            domHead[0]["itaxrate"] = iTaxRate; //税率，double类型
            domHead[0]["cexch_name"] = BWB; //币种，string类型
            domHead[0]["iexchrate"] = "1"; //汇率，double类型
            //domHead[0]["cmemo"] = ""; //备    注，string类型
            //domHead[0]["cunitname"] = ""; //单位名称，string类型
            //domHead[0]["cmyregcode"] = ""; //本单位税号，string类型
            //domHead[0]["cbname"] = ""; //本单位开户银行，string类型
            domHead[0]["cmaker"] = Convert.ToString(dt.Rows[iRow]["cMaker"]); //制单人，string类型
            //domHead[0]["cchecker"] = ""; //复核人，string类型
            //domHead[0]["cbaccount"] = ""; //银行账号，string类型
            domHead[0]["breturnflag"] = "0"; //退货标志，string类型
            domHead[0]["idisp"] = "1"; //是否先发货，string类型
            domHead[0]["ivtid"] = "17"; //单据模版号，int类型
            //domHead[0]["citemcode"] = ""; //项目编号，string类型
            //domHead[0]["citem_class"] = ""; //项目大类编号，string类型
            //domHead[0]["isumdx"] = ""; //价税合计（大写），string类型
            //domHead[0]["zdsumdx"] = ""; //整单合计（大写），string类型
            //domHead[0]["ccuspaycond"] = ""; //客户付款条件，string类型
            //domHead[0]["ccusperson"] = ""; //联系人，string类型
            //domHead[0]["cshipaddress"] = ""; //发货地址，string类型
            //domHead[0]["cscname"] = ""; //发运方式，string类型
            //domHead[0]["csccode"] = ""; //发运方式代码，string类型
            //domHead[0]["bfirst"] = ""; //期初标志，string类型
            //domHead[0]["isumx"] = ""; //价税合计，double类型
            //domHead[0]["zdsum"] = ""; //整单合计，double类型
            //domHead[0]["ufts"] = ""; //时间戳，string类型
            //domHead[0]["bpayment"] = ""; //现结号，string类型
            //domHead[0]["ccrechppass"] = ""; //信用审核口令，string类型
            //domHead[0]["cmodifier"] = ""; //修改人，string类型
            //domHead[0]["dmoddate"] = ""; //修改日期，DateTime类型
            //domHead[0]["dverifydate"] = ""; //复核日期，DateTime类型
            //domHead[0]["clowpricepass"] = ""; //最低售价口令，string类型
            //domHead[0]["dcreatesystime"] = ""; //制单时间，DateTime类型
            //domHead[0]["dverifysystime"] = ""; //复核时间，DateTime类型
            //domHead[0]["dmodifysystime"] = ""; //修改时间，DateTime类型
            //domHead[0]["bcontinue"] = ""; //是否继续，string类型
            domHead[0]["ccusname"] = "上海巴贝拉意舟餐饮管理有限公司"; //客户名称，string类型
            //domHead[0]["caddph"] = ""; //单位地址、电话，string类型
            //domHead[0]["ccusfax"] = ""; //客户传真号，string类型
            //domHead[0]["ccusregcode"] = ""; //税号，string类型
            //domHead[0]["icuscreline"] = ""; //用户信用度，double类型
            //domHead[0]["iarmoney"] = ""; //客户应收余额，double类型
            //domHead[0]["cbcode"] = ""; //本单位开户行账号编号，string类型
            domHead[0]["cvouchtype"] = "27"; //发票类型，string类型
            //domHead[0]["cinvalider"] = ""; //作废人，string类型
            domHead[0]["ccuscode"] = "001"; //客户编号，string类型
            domHead[0]["cdepcode"] = "01"; //部门编号，string类型
            //domHead[0]["cpaycode"] = ""; //付款条件编码，string类型
            //domHead[0]["cbillver"] = ""; //发票版别，string类型
            //domHead[0]["cpersoncode"] = ""; //业务员编码，string类型
            //domHead[0]["ccrechpname"] = ""; //信用审核人，string类型
            //domHead[0]["fcanusequan"] = ""; //可用数量，double类型
            //domHead[0]["cverifier"] = Convert.ToString(dt.Rows[iRow]["cMaker"]); //审核人，string类型
            //domHead[0]["ccusdefine1"] = ""; //客户自定义项1，string类型
            //domHead[0]["ccusdefine2"] = ""; //客户自定义项2，string类型
            //domHead[0]["ccusdefine3"] = ""; //客户自定义项3，string类型
            //domHead[0]["ccusdefine4"] = ""; //客户自定义项4，string类型
            //domHead[0]["ccusdefine5"] = ""; //客户自定义项5，string类型
            //domHead[0]["ccusdefine6"] = ""; //客户自定义项6，string类型
            //domHead[0]["ccusdefine7"] = ""; //客户自定义项7，string类型
            //domHead[0]["ccusdefine8"] = ""; //客户自定义项8，string类型
            //domHead[0]["ccusdefine9"] = ""; //客户自定义项9，string类型
            //domHead[0]["ccusdefine10"] = ""; //客户自定义项10，string类型
            //domHead[0]["ccusdefine11"] = ""; //客户自定义项11，string类型
            //domHead[0]["ccusdefine12"] = ""; //客户自定义项12，string类型
            //domHead[0]["ccusdefine13"] = ""; //客户自定义项13，string类型
            //domHead[0]["ccusdefine14"] = ""; //客户自定义项14，string类型
            //domHead[0]["ccusdefine15"] = ""; //客户自定义项15，string类型
            //domHead[0]["ccusdefine16"] = ""; //客户自定义项16，string类型
            //domHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            //domHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            //domHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
            //domHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
            //domHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            //domHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            //domHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            domHead[0]["bcredit"] = "0"; //是否为立账单据，int类型
            //domHead[0]["ccreditcuscode"] = ""; //信用单位编码，string类型
            //domHead[0]["ccreditcusname"] = ""; //信用单位名称，string类型
            //domHead[0]["cgatheringplan"] = ""; //收付款协议编码，string类型
            //domHead[0]["cgatheringplanname"] = ""; //收付款协议名称，string类型
            //domHead[0]["dcreditstart"] = ""; //立账日，DateTime类型
            //domHead[0]["dgatheringdate"] = ""; //到期日，DateTime类型
            //domHead[0]["icreditdays"] = ""; //账期，int类型
            //domHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            //domHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            //domHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            //domHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
            //domHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            //domHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            //domHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            //domHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            //domHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            domHead[0]["csource"] = "销售"; //来源，int类型
            //domHead[0]["fstockquan"] = ""; //现存数量，double类型




            //给BO表体参数domBody赋值，此BO参数的业务类型为发货单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");
            domBody.RowCount = dts.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            for (int i = 0; i < dts.Rows.Count; i++)
            {

                decimal iQuantity = Public.GetNum(dts.Rows[i]["iQuantity"]);//数量

                decimal iTaxUnitPrice = Public.GetNum(dts.Rows[i]["iTaxUnitPrice"]);//原币含税单价
                //decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                decimal iSum = Public.GetNum(dts.Rows[i]["iSum"]);//原币含税金额
                decimal iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + iTaxRate / 100M), 4);//原币无税单价
                decimal iMoney = Public.ChinaRound(iSum / (1M + iTaxRate / 100M), 2);//原币无税金额
                decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                decimal iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
                decimal iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
                decimal iNatMoney = Public.ChinaRound(iNatSum / (1 + iTaxRate / 100M), 2);//本币无税金额
                decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                /****************************** 以下是必输字段 ****************************/
                //domBody[i]["autoid"] = ""; //主关键字段，0类型
                domBody[i]["cinvcode"] = Convert.ToString(dts.Rows[i]["cInvCode"]); //存货编码，string类型
                domBody[i]["cinvname"] = Convert.ToString(dts.Rows[i]["cInvName"]); //存货名称，string类型
                domBody[i]["iquantity"] = iQuantity; //数量，double类型
                domBody[i]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                /***************************** 以下是非必输字段 ****************************/
                //domBody[i]["cwhname"] = ""; //仓库名称，string类型
                domBody[i]["cwhcode"] = Convert.ToString(dt.Rows[iRow]["cWHCode"]); //仓库编码，string类型
                //domBody[i]["ippartqty"] = ""; //母件数量，string类型
                //domBody[i]["ippartid"] = ""; //母件物料ID，string类型
                //domBody[i]["ippartseqid"] = ""; //选配序号，string类型
                //domBody[i]["cmassunit"] = ""; //保质期单位，int类型
                //domBody[i]["cinvstd"] = ""; //规格型号，string类型
                //domBody[i]["ccontractid"] = ""; //合同编码，string类型
                //domBody[i]["ccontractrowguid"] = ""; //合同标的RowGuid，string类型
                //domBody[i]["ccontracttagcode"] = ""; //合同标的编码，string类型
                //domBody[i]["batomodel"] = ""; //是否ATO件，int类型
                //domBody[i]["inum"] = ""; //件数，double类型
                domBody[i]["itaxunitprice"] = iTaxUnitPrice; //含税单价，double类型
                domBody[i]["iunitprice"] = iUnitPrice; //无税单价，double类型
                domBody[i]["imoney"] = iMoney; //无税金额，double类型
                domBody[i]["isum"] = iSum; //价税合计，double类型
                domBody[i]["itax"] = iTax; //税额，double类型
                //domBody[i]["cfree1"] = ""; //自由项1，string类型
                //domBody[i]["cfree2"] = ""; //自由项2，string类型
                //domBody[i]["isbvid"] = ""; //发货单号，string类型
                domBody[i]["sbvid"] = "0"; //销售发票ID，string类型
                domBody[i]["iquotedprice"] = iTaxUnitPrice; //报价，double类型
                //domBody[i]["idiscount"] = ""; //折扣额，double类型
                domBody[i]["inatmoney"] = iNatMoney; //本币金额，double类型
                domBody[i]["inattax"] = iNatTax; //本币税额，double类型
                domBody[i]["inatsum"] = iNatSum; //本币价税合计，double类型
                //domBody[i]["inatdiscount"] = ""; //本币折扣额，double类型
                //domBody[i]["cclue"] = ""; //凭证线索号，string类型
                //domBody[i]["iinvlscost"] = ""; //最低售价，double类型
                domBody[i]["inatunitprice"] = iNatUnitPrice; //本币单价，double类型
                //domBody[i]["ibatch"] = ""; //批次，string类型
                //domBody[i]["bfree1"] = ""; //是否有自由项1，string类型
                //domBody[i]["bfree2"] = ""; //是否有自由项2，string类型
                //domBody[i]["bfree3"] = ""; //是否有自由项3，string类型
                //domBody[i]["bfree4"] = ""; //是否有自由项4，string类型
                //domBody[i]["bfree5"] = ""; //是否有自由项5，string类型
                //domBody[i]["bfree6"] = ""; //是否有自由项6，string类型
                //domBody[i]["bfree7"] = ""; //是否有自由项7，string类型
                //domBody[i]["bfree8"] = ""; //是否有自由项8，string类型
                //domBody[i]["bfree9"] = ""; //是否有自由项9，string类型
                //domBody[i]["bfree10"] = ""; //是否有自由项10，string类型
                //domBody[i]["cbatch"] = ""; //批号，string类型
                //domBody[i]["cinvdefine1"] = ""; //存货自定义项1，string类型
                //domBody[i]["cinvdefine4"] = ""; //存货自定义项4，string类型
                //domBody[i]["cinvdefine5"] = ""; //存货自定义项5，string类型
                //domBody[i]["cexpirationdate"] = ""; //有效期至，string类型
                //domBody[i]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                //domBody[i]["dexpirationdate"] = ""; //有效期计算项，string类型
                domBody[i]["cbdlcode"] = Convert.ToString(dt.Rows[iRow]["cSOCode"]); //发货单号，string类型
                //domBody[i]["bsalepricefree1"] = ""; //是否自由项定价1，string类型
                //domBody[i]["bsalepricefree2"] = ""; //是否自由项定价2，string类型
                //domBody[i]["bsalepricefree3"] = ""; //是否自由项定价3，string类型
                //domBody[i]["bsalepricefree4"] = ""; //是否自由项定价4，string类型
                //domBody[i]["bsalepricefree5"] = ""; //是否自由项定价5，string类型
                //domBody[i]["bsalepricefree6"] = ""; //是否自由项定价6，string类型
                //domBody[i]["bsalepricefree7"] = ""; //是否自由项定价7，string类型
                //domBody[i]["bsalepricefree8"] = ""; //是否自由项定价8，string类型
                //domBody[i]["bsalepricefree9"] = ""; //是否自由项定价9，string类型
                //domBody[i]["bsalepricefree10"] = ""; //是否自由项定价10，string类型
                //domBody[i]["idemandtype"] = ""; //需求跟踪方式，int类型
                //domBody[i]["cdemandcode"] = ""; //需求跟踪号，string类型
                //domBody[i]["cdemandmemo"] = ""; //需求分类说明，string类型
                //domBody[i]["cdemandid"] = ""; //需求跟踪id，string类型
                //domBody[i]["idemandseq"] = ""; //需求跟踪行号，string类型
                //domBody[i]["cbatchproperty1"] = ""; //批次属性1，double类型
                //domBody[i]["cbatchproperty2"] = ""; //批次属性2，double类型
                //domBody[i]["cbatchproperty3"] = ""; //批次属性3，double类型
                //domBody[i]["cbatchproperty4"] = ""; //批次属性4，double类型
                //domBody[i]["cbatchproperty5"] = ""; //批次属性5，double类型
                //domBody[i]["cbatchproperty6"] = ""; //批次属性6，string类型
                //domBody[i]["cbatchproperty7"] = ""; //批次属性7，string类型
                //domBody[i]["cbatchproperty8"] = ""; //批次属性8，string类型
                //domBody[i]["cbatchproperty9"] = ""; //批次属性9，string类型
                //domBody[i]["cbatchproperty10"] = ""; //批次属性10，DateTime类型
                //domBody[i]["bbatchproperty1"] = ""; //是否启用批次属性1，string类型
                //domBody[i]["bbatchproperty2"] = ""; //是否启用批次属性2，string类型
                //domBody[i]["bbatchproperty3"] = ""; //是否启用批次属性3，string类型
                //domBody[i]["bbatchproperty4"] = ""; //是否启用批次属性4，string类型
                //domBody[i]["bbatchproperty5"] = ""; //是否启用批次属性5，string类型
                //domBody[i]["bbatchproperty6"] = ""; //是否启用批次属性6，string类型
                //domBody[i]["bbatchproperty7"] = ""; //是否启用批次属性7，string类型
                //domBody[i]["bbatchproperty8"] = ""; //是否启用批次属性8，string类型
                //domBody[i]["bbatchproperty9"] = ""; //是否启用批次属性9，string类型
                //domBody[i]["bbatchproperty10"] = ""; //是否启用批次属性10，string类型
                //domBody[i]["bbatchcreate"] = ""; //批次属性是否建档，string类型
                //domBody[i]["cinvdefine6"] = ""; //存货自定义项6，string类型
                //domBody[i]["cinvdefine7"] = ""; //存货自定义项7，string类型
                //domBody[i]["cinvdefine8"] = ""; //存货自定义项8，string类型
                //domBody[i]["cinvdefine9"] = ""; //存货自定义项9，string类型
                //domBody[i]["cinvdefine10"] = ""; //存货自定义项10，string类型
                //domBody[i]["cinvdefine11"] = ""; //存货自定义项11，string类型
                //domBody[i]["cinvdefine12"] = ""; //存货自定义项12，string类型
                //domBody[i]["cinvdefine13"] = ""; //存货自定义项13，string类型
                //domBody[i]["cinvdefine14"] = ""; //存货自定义项14，string类型
                //domBody[i]["cinvdefine15"] = ""; //存货自定义项15，string类型
                //domBody[i]["cinvdefine16"] = ""; //存货自定义项16，string类型
                //domBody[i]["cinvdefine2"] = ""; //存货自定义项2，string类型
                //domBody[i]["cinvdefine3"] = ""; //存货自定义项3，string类型
                //domBody[i]["binvtype"] = ""; //存货类型，string类型
                //domBody[i]["itb"] = ""; //退补标志，int类型
                //domBody[i]["dvdate"] = ""; //失效日期，DateTime类型
                //domBody[i]["cdefine22"] = ""; //表体自定义项1，string类型
                //domBody[i]["cdefine23"] = ""; //表体自定义项2，string类型
                //domBody[i]["cdefine24"] = ""; //表体自定义项3，string类型
                //domBody[i]["cdefine25"] = ""; //表体自定义项4，string类型
                //domBody[i]["cdefine26"] = ""; //表体自定义项5，double类型
                //domBody[i]["cdefine27"] = ""; //表体自定义项6，double类型
                domBody[i]["itaxrate"] = iTaxRate; //税率（％），double类型
                domBody[i]["kl2"] = "100"; //扣率2（％），double类型
                domBody[i]["isosid"] = Convert.ToString(dts.Rows[i]["iSOsID"]); //对应订单子表ID，int类型
                domBody[i]["idlsid"] = Convert.ToString(dts.Rows[i]["iDLsID"]); //子表id，int类型
                //domBody[i]["citemcode"] = ""; //项目编码，string类型
                //domBody[i]["citem_class"] = ""; //项目大类编码，string类型
                domBody[i]["csocode"] = Convert.ToString(dt.Rows[iRow]["cSOCode"]); //订单号，string类型
                //domBody[i]["dkl1"] = ""; //倒扣1（％），double类型
                //domBody[i]["dkl2"] = ""; //倒扣2（％），double类型
                //domBody[i]["fsalecost"] = ""; //零售单价，double类型
                //domBody[i]["fsaleprice"] = ""; //零售金额，double类型
                //domBody[i]["citemname"] = ""; //项目名称，string类型
                //domBody[i]["cvenabbname"] = ""; //产地，string类型
                //domBody[i]["citem_cname"] = ""; //项目大类名称，string类型
                //domBody[i]["cfree3"] = ""; //自由项3，string类型
                //domBody[i]["cfree4"] = ""; //自由项4，string类型
                //domBody[i]["cfree5"] = ""; //自由项5，string类型
                //domBody[i]["cfree6"] = ""; //自由项6，string类型
                //domBody[i]["cfree7"] = ""; //自由项7，string类型
                //domBody[i]["cfree8"] = ""; //自由项8，string类型
                //domBody[i]["cfree9"] = ""; //自由项9，string类型
                //domBody[i]["cfree10"] = ""; //自由项10，string类型
                //domBody[i]["corufts"] = ""; //对应单据时间戳，string类型
                //domBody[i]["inufts"] = ""; //入库单时间戳，string类型
                //domBody[i]["iinvexchrate"] = ""; //换算率，double类型
                //domBody[i]["cunitid"] = ""; //销售单位编码，string类型
                //domBody[i]["cinva_unit"] = ""; //销售单位，string类型
                //domBody[i]["cinvm_unit"] = ""; //主计量单位，string类型
                domBody[i]["igrouptype"] = Convert.ToString(dts.Rows[i]["iGroupType"]); //单位类型，uint类型
                domBody[i]["cgroupcode"] = Convert.ToString(dts.Rows[i]["cGroupCode"]); //计量单位组，string类型
                //domBody[i]["cdefine28"] = ""; //表体自定义项7，string类型
                //domBody[i]["cdefine29"] = ""; //表体自定义项8，string类型
                //domBody[i]["cdefine30"] = ""; //表体自定义项9，string类型
                //domBody[i]["cdefine31"] = ""; //表体自定义项10，string类型
                //domBody[i]["cdefine32"] = ""; //表体自定义项11，string类型
                //domBody[i]["cdefine33"] = ""; //表体自定义项12，string类型
                //domBody[i]["cdefine34"] = ""; //表体自定义项13，int类型
                //domBody[i]["cdefine35"] = ""; //表体自定义项14，int类型
                //domBody[i]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                //domBody[i]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                //domBody[i]["cvmivencode"] = ""; //供货商编码，string类型
                //domBody[i]["cvmivenname"] = ""; //供货商名称，string类型
                domBody[i]["cordercode"] = Convert.ToString(dt.Rows[iRow]["cSOCode"]); //订单号，string类型
                domBody[i]["iorderrowno"] = i + 1; //订单行号，string类型
                //domBody[i]["fcusminprice"] = ""; //客户最低售价，double类型
                //domBody[i]["imoneysum"] = ""; //累计本币收款金额，double类型
                //domBody[i]["iexchsum"] = ""; //累计原币收款金额，double类型
                //domBody[i]["dmdate"] = ""; //生产日期，DateTime类型
                //domBody[i]["binvquality"] = ""; //是否保质期管理，int类型
                //domBody[i]["imassdate"] = ""; //保质期，int类型
                //domBody[i]["ipbvsid"] = ""; //采购发票子表id，int类型
                //domBody[i]["ccode"] = ""; //入库单号，string类型
                //domBody[i]["bgsp"] = ""; //是否gsp检验，int类型
                //domBody[i]["btrack"] = ""; //是否追踪，int类型
                //domBody[i]["csrpolicy"] = ""; //供需政策，string类型
                //domBody[i]["bproxywh"] = ""; //是否代管仓，int类型
                //domBody[i]["cinvaddcode"] = ""; //存货代码，string类型
                //domBody[i]["ccusinvcode"] = ""; //客户存货编码，string类型
                //domBody[i]["ccusinvname"] = ""; //客户存货名称，string类型
                //domBody[i]["bqaurgency"] = ""; //是否急料，int类型
                //domBody[i]["bqaneedcheck"] = ""; //是否质量检验，int类型
                //domBody[i]["cbaccounter"] = ""; //记账人，string类型
                //domBody[i]["bsettleall"] = ""; //结算标志，string类型
                //domBody[i]["binvbatch"] = ""; //是否批次管理，string类型
                //domBody[i]["bservice"] = ""; //是否应税劳务，string类型
                domBody[i]["kl"] = "100"; //扣率（％），double类型

            }

            //给普通参数VoucherState赋值。此参数的数据类型为int，此参数按值传递，表示状态:0增加;1修改
            broker.AssignNormalValue("VoucherState", 0);

            //该参数vNewID为INOUT型普通参数。此参数的数据类型为string，此参数按值传递。在API调用返回时，可以通过GetResult("vNewID")获取其值
            broker.AssignNormalValue("vNewID", "");

            //给普通参数DomConfig赋值。此参数的数据类型为MSXML2.IXMLDOMDocument2，此参数按引用传递，表示ATO,PTO选配
            MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("DomConfig", domMsg);


            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        throw new Exception("系统异常：" + sysEx.Message);
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        throw new Exception("API异常：" + bizEx.Message);
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        throw new Exception("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.String，此参数按值传递，表示成功返回空串
            System.String result = broker.GetReturnValue() as System.String;
            if (result == null || result == "")
            {
            }
            else
            {
                broker.Release();
                throw new Exception(result);
            }

            //获取out/inout参数值

            //获取普通INOUT参数vNewID。此返回值数据类型为string，在使用该参数之前，请判断是否为空
            string vNewIDRet = broker.GetResult("vNewID") as string;
            //DataTable dtRet = AdoAccess.ExecuteDT("select cDLCode from DispatchList where DLID = '" + vNewIDRet + "'", conn);
            //if (dtRet.Rows.Count > 0)
            //{
            //    dgvMain.Rows[iRow].Cells["cDLCode"].Value = Convert.ToString(dtRet.Rows[0]["cDLCode"]);
            //}
            //dt.Rows[iRow]["DLID"] = vNewIDRet;


            //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");
            //MSXML2.DOMDocument domResultHead = (MSXML2.DOMDocumentClass)broker.GetResult("domHead");

            //IXMLDOMNodeList listHead = domResultBody.selectNodes("//rs:data/z:row");

            //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");

            //for (int i = 0; i < list.length; i++)
            //{
            //    IXMLDOMNode node = list[i].attributes.getNamedItem("idlsid");
            //    dts.Rows[i]["iDLsID"] = Convert.ToString(node.nodeValue);

            //}

            //结束本次调用，释放API资源
            broker.Release();

            #endregion
        }

        public static string InRD32(U8Login.clsLogin u8Login, string connstring, DataTable dHead, DataTable dBody, DataTable dpicks, DataTable dFb, int bredvouch, string crdcode)
        {
              System.String VouchNO = "";
            ADODB.Connection ado = new ADODB.Connection();
              try
            {                 
            #region 销售出库单
                string cCode = "";

                cCode = Convert.ToString(dHead.Rows[0]["EXTERNORDERKEY"]);

            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
            //ado.BeginTrans();


            //第三步：设置API地址标识(Url)
            //当前API错误：添加新单据的地址标识为：U8API/saleout/Add
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/saleout/Add");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：32
            broker.AssignNormalValue("sVouchType", "32");

            //给BO表头参数DomHead赋值，此BO参数的业务类型为销售出库单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数DomHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject DomHead = broker.GetBoParam("DomHead");
            DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            Int64 DLID = 0;
            string ccuscode = "", cdepcode = "", cstcode = "", BWB="";
          
                DataTable dtcx = SqlAccess.ExecuteSqlDataTable("select ccuscode,cdepcode,cstcode,DLID,cexch_name  from DispatchList with(nolock) where cdlcode='" + cCode + "'", connstring);
                if (cCode != "")
                {
                    if (dtcx.Rows.Count > 0)
                    {
                        //cpoid = Convert.ToString(dtcx.Rows[0]["cpocode"]);
                        cdepcode = Convert.ToString(dtcx.Rows[0]["cdepcode"]);
                        cstcode = Convert.ToString(dtcx.Rows[0]["cstcode"]);
                        ccuscode = Convert.ToString(dtcx.Rows[0]["ccuscode"]);
                        DLID = Convert.ToInt64(dtcx.Rows[0]["DLID"]);
                        BWB = Convert.ToString(dtcx.Rows[0]["cexch_name"]);

                    }
                    else
                    {
                        return "API错误:此单没有对应的销售发货单";
                    }
                }
              
            /****************************** 以下是必输字段 ****************************/
            DomHead[0]["id"] = "0"; //主关键字段，int类型
            DomHead[0]["ccode"] = cCode; // Public.GetParentCode("0303", "", connstring); //出库单号，string类型    = 销售发货单 单号

            string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["EDITDATE"], ""), 1);
            //string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 0);
            rq1 = rq1.Insert(4, "/");
            rq1 = rq1.Insert(7, "/");

          //  string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");// ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where wmsbm='" + ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "") + "'", connstring), "");
            string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");

            if (cwhcode == "")
            {
                cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and wmsbm='" + Convert.ToString(dFb.Rows[0]["LOC"]) + "'", connstring), "");
            }

            if (cdepcode == "")
            {
                cdepcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR2"], "");
                if (cdepcode == "")
                {
                    cdepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
                }

            }
            DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToString(); //出库日期，DateTime类型
            //DomHead[0]["cwhname"] = ""; //仓库，string类型
            DomHead[0]["cbustype"] = "普通销售"; //业务类型，int类型
            DomHead[0]["iverifystate"] = "0"; //iverifystate，int类型
            DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，int类型
            //DomHead[0]["ccusabbname"] = ""; //客户，string类型
            DomHead[0]["cmaker"] = ClsSystem.gnvl(dHead.Rows[0]["EDITWHO"], ""); //制单人，string类型
            //DomHead[0]["ufts"] = ""; //时间戳，string类型
            DomHead[0]["cvouchtype"] = "32"; //单据类型，string类型

            DomHead[0]["cwhcode"] = cwhcode;//ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], ""); //仓库编码，string类型
            DomHead[0]["csource"] = "发货单"; //单据来源，int类型
            DomHead[0]["brdflag"] = "0"; //收发标志，int类型
            DomHead[0]["ccuscode"] = ccuscode; //客户编码，string类型
            DomHead[0]["bisstqc"] = "0"; //库存期初标识，string类型

            /***************************** 以下是非必输字段 ****************************/
            //DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
            //DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
            DomHead[0]["dnmaketime"] = DateTime.Now; //制单时间，DateTime类型
            //DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
          //  DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
            //DomHead[0]["crdname"] = ""; //出库类别，string类型
            //DomHead[0]["ireturncount"] = ""; //ireturncount，string类型
            DomHead[0]["cbuscode"] = cCode; //业务号，string类型
            //DomHead[0]["cdepname"] = ""; //销售部门，string类型
            //DomHead[0]["cpersonname"] = ""; //业务员，string类型
          //  DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
            DomHead[0]["cmemo"] =ClsSystem.gnvl (dHead. Rows[0]["ORDERKEY"],""); //备注，string类型
          //  DomHead[0]["chandler"] = ""; //审核人，string类型
            //DomHead[0]["caccounter"] = ""; //记账人，string类型
            //DomHead[0]["ipresent"] = ""; //现存量，string类型
            //DomHead[0]["ccusperson"] = ""; //客户联系人，string类型
            //DomHead[0]["ccusphone"] = ""; //客户电话，string类型
            //DomHead[0]["ccushand"] = ""; //客户手机，string类型
            //DomHead[0]["ccusaddress"] = ""; //客户地址，string类型
            //DomHead[0]["contactphone"] = ""; //客户联系人电话，string类型
            //DomHead[0]["contactmobile"] = ""; //客户联系人手机，string类型
            //DomHead[0]["cdeliverunit"] = ""; //收货单位，string类型
            //DomHead[0]["ccontactname"] = ""; //收货单位联系人，string类型
            //DomHead[0]["cofficephone"] = ""; //收货单位联系人电话，string类型
            //DomHead[0]["cmobilephone"] = ""; //收货单位联系人手机，string类型
            //DomHead[0]["cpsnophone"] = ""; //业务员电话，string类型
            //DomHead[0]["cpsnmobilephone"] = ""; //业务员手机，string类型
            //DomHead[0]["cstname"] = ""; //销售类型，string类型
            DomHead[0]["cdepcode"] = cdepcode; //部门编码，string类型
            DomHead[0]["crdcode"] = crdcode; //出库类别编码，string类型
            DomHead[0]["cstcode"] = cstcode; //销售类型编码，string类型
            //DomHead[0]["cvouchname"] = ""; //单据类名，int类型
            DomHead[0]["cdlcode"] = DLID; //发货单id，string类型
            //DomHead[0]["cvenabbname"] = ""; //供应商，string类型
            //DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型
            //DomHead[0]["cbillcode"] = ""; //发票id，string类型
            //DomHead[0]["cvencode"] = ""; //供应商代码，string类型
            //DomHead[0]["iavaquantity"] = ""; //可用量，string类型
            //DomHead[0]["iavanum"] = ""; //可用件数，string类型
            //DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
            //DomHead[0]["gspcheck"] = ""; //gsp复核标志，string类型
            //DomHead[0]["isalebillid"] = ""; //发票号，string类型
            //DomHead[0]["iarriveid"] = ""; //发货单号，string类型
            //DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
            //DomHead[0]["cchkperson"] = ""; //检验员，string类型
            DomHead[0]["vt_id"] = "87"; //模版号，int类型
            //DomHead[0]["cshipaddress"] = ""; //发货地址，string类型
            //DomHead[0]["caddcode"] = ""; //发货地址编码，string类型
            //DomHead[0]["cchkcode"] = ""; //检验单号，string类型
            //DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
            //DomHead[0]["isafesum"] = ""; //安全库存量，string类型
            //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            //DomHead[0]["itopsum"] = ""; //最高库存量，string类型
            //DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            //DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            //DomHead[0]["cdefine3"] = Convert.ToString(dgvMain.Rows[iRow].Cells["cCode"].Value); //表头自定义项3，string类型
            //DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
            //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            //DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
            //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型


            //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            if (dBody.Rows.Count * dFb.Rows.Count < 1)
            {
                return "API错误：表体无数据";
            }
                  
            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");
            domBody.RowCount = dBody.Rows.Count * dFb.Rows.Count; //设置BO对象行数

            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            string  iDLsID = "";
            int bInvBatch = 0;
            string cinvcode = "";
            string cPdID = "";
            int j = 0;
            for (int k = 0; k < dBody.Rows.Count; k++)
            {
                cinvcode = ClsSystem.gnvl(dBody.Rows[k]["SKU"], "");
                cPdID = ClsSystem.gnvl(dBody.Rows[k]["ShipmentOrderDetail_ID"], "");

                DataView rowfilter2 = new DataView(dpicks);
                rowfilter2.RowFilter = "ShipmentOrderDetail_ID =" + cPdID;
                rowfilter2.RowStateFilter = DataViewRowState.CurrentRows;
                DataTable dpick = rowfilter2.ToTable();

                if (dpick.Rows.Count > 0)
                {

                    string cpicksid = ClsSystem.gnvl(dpick.Rows[0]["PickDetails_ID"], "");

                    DataView rowfilter1 = new DataView(dFb);
                    rowfilter1.RowFilter = "PickDetails_ID =" + cpicksid;
                    rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                    DataTable dts = rowfilter1.ToTable();
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {

                        decimal iQuantity = Public.GetNum(dts.Rows[i]["QTY"]);//数量

                        //decimal iTaxUnitPrice = Public.GetNum(dts.Rows[i]["iTaxUnitPrice"]);//原币含税单价
                        //decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                        //decimal iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + 17M / 100M), 4);//原币无税单价
                        //decimal iMoney = Public.ChinaRound(iSum / (1M + 17M / 100M), 2);//原币无税金额
                        //decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                        //decimal iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
                        //decimal iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
                        //decimal iNatMoney = Public.ChinaRound(iNatSum / (1 + 17M / 100M), 2);//本币无税金额
                        //decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额


                        if (DLID != 0)
                        {
                            dtcx = SqlAccess.ExecuteSqlDataTable("select iSOsID,cSoCode ,iDLsID   from DispatchLists with(nolock) where DLID=" + DLID + " and cinvcode='" + cinvcode + "'", connstring);
                            if (dtcx.Rows.Count > 0)
                            {
                                //cpoid = Convert.ToString(dtcx.Rows[0]["cpocode"]);
                                //iSOsID = Convert.ToString(dtcx.Rows[0]["iSOsID"]);
                                //cSoCode = Convert.ToString(dtcx.Rows[0]["cSoCode"]);
                                iDLsID = Convert.ToString(dtcx.Rows[0]["iDLsID"]);

                                //decimal iTaxRate = Convert.ToDecimal(dtcx.Rows[0]["iTaxRate"]);

                                //decimal iUnitPrice = Public.GetNum(dtcx.Rows[0]["iUnitPrice"]);

                                //decimal iTaxUnitPrice = Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价

                                //decimal iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                                //decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                //decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                                //decimal iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                                //decimal iNatSum = Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                                //decimal iNatMoney = Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                                //decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                            }
                            else
                            {
                                return "API错误:此单没有对应的销售发货单表体id";
                            }
                        }

                        decimal iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                        decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                        /****************************** 以下是必输字段 ****************************/
                        //domBody[j]["autoid"] = ""; //主关键字段，int类型
                        //domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                        domBody[j]["iquantity"] = iQuantity; //数量，double类型
                        domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                        domBody[j]["id"] = "0"; //ID，int类型
                        domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                        /***************************** 以下是非必输字段 ****************************/
                        if (cwhcode == "06")
                        {
                            domBody[j]["cposition"] = "06";
                        }
                        //domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                        //domBody[j]["cinvname"] = ""; //存货名称，string类型
                        //domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                        //   domBody[j]["isodid"] = iSOsID; //销售订单子表ID，string类型
                        //domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                        //domBody[j]["cvenname"] = ""; //供应商，string类型
                        //domBody[j]["imassdate"] = ""; //保质期，int类型
                        //domBody[j]["bgsp"] = ""; //是否质检，int类型
                        //domBody[j]["cgspstate"] = ""; //检验状态，double类型
                        //domBody[j]["cassunit"] = ""; //库存单位码，string类型
                        //domBody[j]["cposname"] = ""; //货位，string类型
                        //domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                        //domBody[j]["scrapufts"] = ""; //不合格品时间戳，string类型
                        //domBody[j]["cmassunit"] = Convert.ToString(dts.Rows[i]["cMassUnit"]); //保质期单位，int类型
                        //domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                        //domBody[j]["csocode"] = ""; //需求跟踪号，string类型

                        //domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                        //domBody[j]["bvmiused"] = "0"; //代管消耗标识，int类型

                        //domBody[j]["cvmivenname"] = ""; //代管商，string类型

                        //domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                        //domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                        //domBody[j]["iinvexchrate"] = ""; //换算率，double类型
                        //domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                        //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                        //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                        //domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                        //domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                        domBody[j]["inquantity"] = iQuantity; //应发数量，double类型
                        //domBody[j]["ipprice"] = ""; //计划金额/售价金额，double类型
                        domBody[j]["cbdlcode"] = cCode; //发货单号，string类型 
                        //domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                        //   domBody[j]["iordertype"] = "1"; //销售订单类别，int类型
                        //domBody[j]["iorderdid"] = iSOsID; //iorderdid，int类型
                        //domBody[j]["iordercode"] = cSoCode; //销售订单号，string类型
                        //      domBody[j]["iorderseq"] = i + 1; //销售订单行号，string类型
                        //domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                        //domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                        //domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                        //domBody[j]["cciqbookcode"] = ""; //手册号，string类型
                        //domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                        //domBody[j]["strcontractid"] = ""; //合同号，string类型
                        //domBody[j]["strcode"] = ""; //合同标的编码，string类型
                        //domBody[j]["ccusinvcode"] = ""; //客户存货编码，string类型
                        //domBody[j]["ccusinvname"] = ""; //客户存货名称，string类型
                        //domBody[j]["cbaccounter"] = ""; //记账人，string类型
                        domBody[j]["bcosting"] = "1"; //是否核算，string类型
                        domBody[j]["isotype"] = "0"; //需求跟踪方式，int类型
                        //domBody[j]["stockupid"] = ""; //stockupid，string类型
                        //domBody[j]["iavaquantity"] = ""; //可用量，double类型
                        //domBody[j]["iavanum"] = ""; //可用件数，double类型
                        //domBody[j]["ipresent"] = ""; //现存量，double类型
                        //domBody[j]["ipresentnum"] = ""; //现存件数，double类型
                        //domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                        domBody[j]["cdefine22"] = ClsSystem.gnvl(dHead.Rows[0]["ORDERKEY"], ""); //表体自定义项1，string类型
                        //domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
                        //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                        //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                        //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                        //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                        //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                        //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                        //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                        //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                        //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                        //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                        //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                        //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                        //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                        //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                        //domBody[j]["cbarcode"] = ""; //条形码，string类型
                        //domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                        domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[i]["ID"], ""); //表体自定义项3，string类型
                        //domBody[j]["cinvstd"] = ""; //规格型号，string类型
                        //domBody[j]["cposition"] = ""; //货位编码，string类型
                        //domBody[j]["creplaceitem"] = ""; //替换件，string类型
                        //domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                        //domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                        //domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                        //domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                        domBody[j]["cdefine26"] = ClsSystem.gnvl(dBody.Rows[k]["SUSR2"], ""); //表体自定义项5，double类型
                        //domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                        //domBody[j]["inum"] = ""; //件数，double类型
                        //domBody[j]["citemcode"] = ""; //项目编码，string类型
                        //domBody[j]["innum"] = ""; //应发件数，double类型
                        //domBody[j]["cname"] = ""; //项目，string类型
                        //domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
                        //domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                        domBody[j]["idlsid"] = iDLsID; //发货单子表ID，int类型
                        //domBody[j]["isbsid"] = ""; //发票子表ID，int类型
                        //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                        //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                        //domBody[j]["isendquantity"] = ""; //发货数量，int类型
                        //domBody[j]["isendnum"] = ""; //发货件数，int类型
                        //domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                        //domBody[j]["iensid"] = ""; //委托子表id，double类型
                        //domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                        //domBody[j]["cbatchproperty1"] = ""; //属性1，double类型
                        //domBody[j]["cbatchproperty3"] = ""; //属性3，double类型
                        //domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                        //domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                        //domBody[j]["cbatchproperty4"] = ""; //属性4，double类型
                        //domBody[j]["cbatchproperty5"] = ""; //属性5，double类型
                        //domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                        //domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                       
                        //domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                        //domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                        //domBody[j]["cbatchproperty8"] = ""; //属性8，string类型
                        //domBody[j]["cbatchproperty9"] = ""; //属性9，string类型
                        //domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                        //domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                        //domBody[j]["cbatchproperty10"] = ""; //属性10，DateTime类型
                        //domBody[j]["dvdate"] = Convert.ToString(dts.Rows[i]["dVDate"]); //失效日期，DateTime类型
                        //domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                        //domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                        //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                        //domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                        //domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                        //domBody[j]["cbatchproperty2"] = ""; //属性2，double类型
                        //domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                        domBody[j]["iunitcost"] = iTaxUnitPrice; //单价，double类型
                        domBody[j]["iprice"] = iSum; //金额，double类型
                        //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                        //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                        //domBody[j]["ipunitcost"] = ""; //计划单价/售价，double类型
                        //domBody[j]["igrossweight"] = ""; //毛重，string类型
                        //domBody[j]["inetweight"] = ""; //净重，string类型
                        bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + Convert.ToString(dBody.Rows[k]["SKU"]) + "'", connstring));
                        if (bInvBatch == 1)
                        {
                            domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[i]["LOTTABLE02"]); //属性6，string类型 炉号
                            domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[i]["LOTTABLE03"]);//属性7，string类型  母卷号
                            domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[i]["ID"], ""); //批号，string类型
                        }
                        else
                        {
                            domBody[j]["cbatchproperty7"] =""; //属性6，string类型 炉号
                            domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号
                            domBody[j]["cbatch"] = ""; //批号，string类型
                        }
                        //  domBody[j]["cbatch"] = Convert.ToString(dts.Rows[i]["cBatch"]); //批号，string类型
                        //domBody[j]["iinvsncount"] = ""; //库存序列号，int类型

                        j++;
                    }
                }
            }

            //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
            broker.AssignNormalValue("domPosition", null);

            //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

            //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
            broker.AssignNormalValue("cnnFrom", null);

            //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
            broker.AssignNormalValue("VouchId", "");

            //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
            MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("domMsg", domMsg);

            //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
            broker.AssignNormalValue("bCheck", true);

            //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
            broker.AssignNormalValue("bBeforCheckStock", true);

            //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
            broker.AssignNormalValue("bIsRedVouch", false);

            //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
            broker.AssignNormalValue("sAddedState", "");

            //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
            broker.AssignNormalValue("bReMote", false);


            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        throw new Exception("系统异常：" + sysEx.Message);
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        throw new Exception("API异常：" + bizEx.Message);
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        throw new Exception("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
                //ado.RollbackTrans();
                return "API错误：" + apiEx.ToString().Substring(0, 100);
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
            System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


            //获取out/inout参数值

            //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            System.String errMsgRet = broker.GetResult("errMsg") as System.String;

            if (!result)
            {
                broker.Release();
                //ado.RollbackTrans();
                  return "API错误：" + errMsgRet;
            }

            //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            VouchNO = broker.GetResult("VouchId") as System.String;

            //DataTable dtRet = AdoAccess.ExecuteDT("select cCode from rdrecord32 where ID = '" + VouchIdRet + "'", conn);
            //if (dtRet.Rows.Count > 0)
            //{
            //    dgvMain.Rows[iRow].Cells["cCKCode"].Value = Convert.ToString(dtRet.Rows[0]["cCode"]);
            //}

            //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
            //MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

            //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");

            //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");


            //for (int i = 0; i < list.length; i++)
            //{
            //    IXMLDOMNode node = list[i].attributes.getNamedItem("autoid");
            //    dts.Rows[i]["RD32sID"] = Convert.ToString(node.nodeValue);
            //}

            //DataTable dtRet = AdoAccess.ExecuteDT("select AutoID,iDLsID from rdrecords32 where ID = " + VouchIdRet + " ", conn);
            //for (int i = 0; i < dtRet.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dts.Rows.Count; j++)
            //    {
            //        if (Convert.ToString(dts.Rows[j]["iDLsID"]) == Convert.ToString(dtRet.Rows[i]["iDLsID"]))
            //        {
            //            dts.Rows[j]["RD32sID"] = Convert.ToString(dtRet.Rows[i]["AutoID"]);
            //        }

            //    }
            //}


            //结束本次调用，释放API资源
            broker.Release();

            StringBuilder sql = new StringBuilder();
            sql.Append(" update DispatchList set cSaleOut='ST' where dlid=" + DLID);
            //myCommand.CommandText = sql.ToString();
            //myCommand.ExecuteNonQuery();
            SqlAccess.ExecuteSql(sql.ToString(), connstring);

            //ado.CommitTrans();

            #endregion
            }
              catch (Exception ex)
              {

                 // ado.RollbackTrans();
                  return "API错误：" + ex.ToString().Substring(0, 50);
              }
              return VouchNO; 

        }

        //销售出库单红字 入库形式
        public static string InRD32_HZ(U8Login.clsLogin u8Login, string connstring, DataTable dHead, DataTable dBody,  int bredvouch, string crdcode)
        {
            System.String VouchNO = "";
            ADODB.Connection ado = new ADODB.Connection();
            try
            {
                #region 销售出库单红字 入库形式
                string cCode = "";

                cCode = Convert.ToString(dHead.Rows[0]["POKEY"]);

                //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                U8EnvContext envContext = new U8EnvContext();
                envContext.U8Login = u8Login;

                //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
                //ado.BeginTrans();


                //第三步：设置API地址标识(Url)
                //当前API错误：添加新单据的地址标识为：U8API/saleout/Add
                U8ApiAddress myApiAddress = new U8ApiAddress("U8API/saleout/Add");

                //第四步：构造APIBroker
                U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

                //第五步：API参数赋值

                //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：32
                broker.AssignNormalValue("sVouchType", "32");

                //给BO表头参数DomHead赋值，此BO参数的业务类型为销售出库单，属表头参数。BO参数均按引用传递
                //提示：给BO表头参数DomHead赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject DomHead = broker.GetBoParam("DomHead");
                DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
                //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
                Int64 DLID = 0;
                string ccuscode = "", cdepcode = "0700", cstcode = "01", BWB = "人民币";
                ccuscode = ClsSystem.gnvl(dHead.Rows[0]["SUSR2"], "");

                DataTable dtcx = SqlAccess.ExecuteSqlDataTable("select ccuscode,cdepcode,cstcode,DLID,cexch_name  from DispatchList with(nolock) where cdlcode='" + cCode + "'", connstring);
                if (cCode != "")
                {
                    if (dtcx.Rows.Count > 0)
                    {
                        //cpoid = Convert.ToString(dtcx.Rows[0]["cpocode"]);
                        cdepcode = Convert.ToString(dtcx.Rows[0]["cdepcode"]);
                        cstcode = Convert.ToString(dtcx.Rows[0]["cstcode"]);
                        ccuscode = Convert.ToString(dtcx.Rows[0]["ccuscode"]);
                        DLID = Convert.ToInt64(dtcx.Rows[0]["DLID"]);
                        BWB = Convert.ToString(dtcx.Rows[0]["cexch_name"]);

                    }
                    else
                    {
                        return "API错误:此单没有对应的销售发货单";
                    }
                }

                /****************************** 以下是必输字段 ****************************/
                DomHead[0]["id"] = "0"; //主关键字段，int类型
                DomHead[0]["ccode"] = cCode;// Public.GetParentCode("0303", "", connstring); //出库单号，string类型
                string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 1);
                //string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 0);
                rq1 = rq1.Insert(4, "/");
                rq1 = rq1.Insert(7, "/");

                //  string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");// ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where wmsbm='" + ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "") + "'", connstring), "");
                string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");

                if (cwhcode == "")
                {
                    cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and wmsbm='" + Convert.ToString(dBody.Rows[0]["TOLOC"]) + "'", connstring), "");
                }
                DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToString(); //出库日期，DateTime类型
                //DomHead[0]["cwhname"] = ""; //仓库，string类型
                DomHead[0]["cbustype"] = "普通销售"; //业务类型，int类型
                DomHead[0]["iverifystate"] = "0"; //iverifystate，int类型
                DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，int类型
                //DomHead[0]["ccusabbname"] = ""; //客户，string类型
                DomHead[0]["cmaker"] = ClsSystem.gnvl(dHead.Rows[0]["EDITWHO"], ""); //制单人，string类型
                //DomHead[0]["ufts"] = ""; //时间戳，string类型
                DomHead[0]["cvouchtype"] = "32"; //单据类型，string类型

                DomHead[0]["cwhcode"] = cwhcode;//ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], ""); //仓库编码，string类型
                DomHead[0]["csource"] = "库存"; //单据来源，int类型
                DomHead[0]["brdflag"] = "0"; //收发标志，int类型
                DomHead[0]["ccuscode"] = ccuscode; //客户编码，string类型
                DomHead[0]["bisstqc"] = "0"; //库存期初标识，string类型

                /***************************** 以下是非必输字段 ****************************/
                //DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
                //DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
                DomHead[0]["dnmaketime"] = DateTime.Now; //制单时间，DateTime类型
                //DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
                //  DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
                //DomHead[0]["crdname"] = ""; //出库类别，string类型
                //DomHead[0]["ireturncount"] = ""; //ireturncount，string类型
                DomHead[0]["cbuscode"] = cCode; //业务号，string类型
                //DomHead[0]["cdepname"] = ""; //销售部门，string类型
                //DomHead[0]["cpersonname"] = ""; //业务员，string类型
                //  DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
                DomHead[0]["cmemo"] = ClsSystem.gnvl(dHead.Rows[0]["RECEIPTKEY"], ""); //备注，string类型
                //  DomHead[0]["chandler"] = ""; //审核人，string类型
                //DomHead[0]["caccounter"] = ""; //记账人，string类型
                //DomHead[0]["ipresent"] = ""; //现存量，string类型
                //DomHead[0]["ccusperson"] = ""; //客户联系人，string类型
                //DomHead[0]["ccusphone"] = ""; //客户电话，string类型
                //DomHead[0]["ccushand"] = ""; //客户手机，string类型
                //DomHead[0]["ccusaddress"] = ""; //客户地址，string类型
                //DomHead[0]["contactphone"] = ""; //客户联系人电话，string类型
                //DomHead[0]["contactmobile"] = ""; //客户联系人手机，string类型
                //DomHead[0]["cdeliverunit"] = ""; //收货单位，string类型
                //DomHead[0]["ccontactname"] = ""; //收货单位联系人，string类型
                //DomHead[0]["cofficephone"] = ""; //收货单位联系人电话，string类型
                //DomHead[0]["cmobilephone"] = ""; //收货单位联系人手机，string类型
                //DomHead[0]["cpsnophone"] = ""; //业务员电话，string类型
                //DomHead[0]["cpsnmobilephone"] = ""; //业务员手机，string类型
                //DomHead[0]["cstname"] = ""; //销售类型，string类型
                DomHead[0]["cdepcode"] = cdepcode; //部门编码，string类型
                DomHead[0]["crdcode"] = crdcode; //出库类别编码，string类型
                DomHead[0]["cstcode"] = cstcode; //销售类型编码，string类型
                //DomHead[0]["cvouchname"] = ""; //单据类名，int类型
                DomHead[0]["cdlcode"] = DLID; //发货单id，string类型
                //DomHead[0]["cvenabbname"] = ""; //供应商，string类型
                //DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型
                //DomHead[0]["cbillcode"] = ""; //发票id，string类型
                //DomHead[0]["cvencode"] = ""; //供应商代码，string类型
                //DomHead[0]["iavaquantity"] = ""; //可用量，string类型
                //DomHead[0]["iavanum"] = ""; //可用件数，string类型
                //DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
                //DomHead[0]["gspcheck"] = ""; //gsp复核标志，string类型
                //DomHead[0]["isalebillid"] = ""; //发票号，string类型
                //DomHead[0]["iarriveid"] = ""; //发货单号，string类型
                //DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
                //DomHead[0]["cchkperson"] = ""; //检验员，string类型
                DomHead[0]["vt_id"] = "87"; //模版号，int类型
                //DomHead[0]["cshipaddress"] = ""; //发货地址，string类型
                //DomHead[0]["caddcode"] = ""; //发货地址编码，string类型
                //DomHead[0]["cchkcode"] = ""; //检验单号，string类型
                //DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
                //DomHead[0]["isafesum"] = ""; //安全库存量，string类型
                //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
                //DomHead[0]["itopsum"] = ""; //最高库存量，string类型
                //DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
                //DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
                //DomHead[0]["cdefine3"] = Convert.ToString(dgvMain.Rows[iRow].Cells["cCode"].Value); //表头自定义项3，string类型
                //DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
                //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
                //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
                //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
                //DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
                //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
                //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
                //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
                //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
                //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
                //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
                //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型


                //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
                //提示：给BO表体参数domBody赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

                if (dBody.Rows.Count  < 1)
                {
                    return "API错误：表体无数据";
                }

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject domBody = broker.GetBoParam("domBody");
                domBody.RowCount = dBody.Rows.Count ; //设置BO对象行数

                //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
                //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
                string iDLsID = "";
                int bInvBatch = 0;
                string cinvcode = "";
                string cPdID = "";
                int j = 0;
                for (int k = 0; k < dBody.Rows.Count; k++)
                {
                    cinvcode = ClsSystem.gnvl(dBody.Rows[k]["SKU"], "");
                    //cPdID = ClsSystem.gnvl(dBody.Rows[k]["ShipmentOrderDetail_ID"], "");

                  
                  

                    //    string cpicksid = ClsSystem.gnvl(dpick.Rows[0]["PickDetails_ID"], "");

                    //    DataView rowfilter1 = new DataView(dFb);
                    //    rowfilter1.RowFilter = "PickDetails_ID =" + cpicksid;
                    //    rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                    //    DataTable dts = rowfilter1.ToTable();
                        //for (int i = 0; i < dts.Rows.Count; i++)
                        //{

                    decimal iQuantity = Public.GetNum(dBody.Rows[k]["QTYRECEIVED"]);//数量
                    if (bredvouch == 1)
                    {
                        iQuantity = -iQuantity;
                    }

                            //decimal iTaxUnitPrice = Public.GetNum(dts.Rows[i]["iTaxUnitPrice"]);//原币含税单价
                            //decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                            //decimal iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + 17M / 100M), 4);//原币无税单价
                            //decimal iMoney = Public.ChinaRound(iSum / (1M + 17M / 100M), 2);//原币无税金额
                            //decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                            //decimal iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
                            //decimal iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
                            //decimal iNatMoney = Public.ChinaRound(iNatSum / (1 + 17M / 100M), 2);//本币无税金额
                            //decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额


                    if (DLID != 0)
                    {
                        dtcx = SqlAccess.ExecuteSqlDataTable("select iSOsID,cSoCode ,iDLsID   from DispatchLists with(nolock) where DLID=" + DLID + " and cinvcode='" + cinvcode + "'", connstring);
                        if (dtcx.Rows.Count > 0)
                        {
                            //cpoid = Convert.ToString(dtcx.Rows[0]["cpocode"]);
                            //iSOsID = Convert.ToString(dtcx.Rows[0]["iSOsID"]);
                            //cSoCode = Convert.ToString(dtcx.Rows[0]["cSoCode"]);
                            iDLsID = Convert.ToString(dtcx.Rows[0]["iDLsID"]);

                            //decimal iTaxRate = Convert.ToDecimal(dtcx.Rows[0]["iTaxRate"]);

                            //decimal iUnitPrice = Public.GetNum(dtcx.Rows[0]["iUnitPrice"]);

                            //decimal iTaxUnitPrice = Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价

                            //decimal iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                            //decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                            //decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                            //decimal iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                            //decimal iNatSum = Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                            //decimal iNatMoney = Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                            //decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                        }
                        else
                        {
                            return "API错误:此单没有对应的销售发货单表体id";
                        }
                    }

                            decimal iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                            decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                            /****************************** 以下是必输字段 ****************************/
                            //domBody[j]["autoid"] = ""; //主关键字段，int类型
                            //domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                            domBody[j]["iquantity"] = iQuantity; //数量，double类型
                            domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                            domBody[j]["id"] = "0"; //ID，int类型
                            domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                            /***************************** 以下是非必输字段 ****************************/
                            if (cwhcode == "06")
                            {
                                domBody[j]["cposition"] = "06";
                            }
                            //domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                            //domBody[j]["cinvname"] = ""; //存货名称，string类型
                            //domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                            //   domBody[j]["isodid"] = iSOsID; //销售订单子表ID，string类型
                            //domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                            //domBody[j]["cvenname"] = ""; //供应商，string类型
                            //domBody[j]["imassdate"] = ""; //保质期，int类型
                            //domBody[j]["bgsp"] = ""; //是否质检，int类型
                            //domBody[j]["cgspstate"] = ""; //检验状态，double类型
                            //domBody[j]["cassunit"] = ""; //库存单位码，string类型
                            //domBody[j]["cposname"] = ""; //货位，string类型
                            //domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                            //domBody[j]["scrapufts"] = ""; //不合格品时间戳，string类型
                            //domBody[j]["cmassunit"] = Convert.ToString(dts.Rows[i]["cMassUnit"]); //保质期单位，int类型
                            //domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                            //domBody[j]["csocode"] = ""; //需求跟踪号，string类型

                            //domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                            //domBody[j]["bvmiused"] = "0"; //代管消耗标识，int类型

                            //domBody[j]["cvmivenname"] = ""; //代管商，string类型

                            //domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                            //domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                            //domBody[j]["iinvexchrate"] = ""; //换算率，double类型
                            //domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                            //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                            //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                            //domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                            //domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                            domBody[j]["inquantity"] = iQuantity; //应发数量，double类型
                            //domBody[j]["ipprice"] = ""; //计划金额/售价金额，double类型
                            domBody[j]["cbdlcode"] = cCode; //发货单号，string类型 
                            //domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                            //   domBody[j]["iordertype"] = "1"; //销售订单类别，int类型
                            //domBody[j]["iorderdid"] = iSOsID; //iorderdid，int类型
                            //domBody[j]["iordercode"] = cSoCode; //销售订单号，string类型
                            //      domBody[j]["iorderseq"] = i + 1; //销售订单行号，string类型
                            //domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                            //domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                            //domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                            //domBody[j]["cciqbookcode"] = ""; //手册号，string类型
                            //domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                            //domBody[j]["strcontractid"] = ""; //合同号，string类型
                            //domBody[j]["strcode"] = ""; //合同标的编码，string类型
                            //domBody[j]["ccusinvcode"] = ""; //客户存货编码，string类型
                            //domBody[j]["ccusinvname"] = ""; //客户存货名称，string类型
                            //domBody[j]["cbaccounter"] = ""; //记账人，string类型
                            domBody[j]["bcosting"] = "1"; //是否核算，string类型
                            domBody[j]["isotype"] = "0"; //需求跟踪方式，int类型
                            //domBody[j]["stockupid"] = ""; //stockupid，string类型
                            //domBody[j]["iavaquantity"] = ""; //可用量，double类型
                            //domBody[j]["iavanum"] = ""; //可用件数，double类型
                            //domBody[j]["ipresent"] = ""; //现存量，double类型
                            //domBody[j]["ipresentnum"] = ""; //现存件数，double类型
                            //domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                            domBody[j]["cdefine22"] = ClsSystem.gnvl(dHead.Rows[0]["RECEIPTKEY"], ""); //表体自定义项1，string类型
                            //domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
                            //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                            //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                            //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                            //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                            //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                            //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                            //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                            //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                            //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                            //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                            //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                            //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                            //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                            //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                            //domBody[j]["cbarcode"] = ""; //条形码，string类型
                            //domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                            domBody[j]["cdefine24"] = ClsSystem.gnvl(dBody.Rows[k]["TOID"], ""); //表体自定义项3，string类型
                            //domBody[j]["cinvstd"] = ""; //规格型号，string类型
                            //domBody[j]["cposition"] = ""; //货位编码，string类型
                            //domBody[j]["creplaceitem"] = ""; //替换件，string类型
                            //domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                            //domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                            //domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                            //domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                            domBody[j]["cdefine26"] = ClsSystem.gnvl(dBody.Rows[k]["SUSR2"], ""); //表体自定义项5，double类型
                            //domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                            //domBody[j]["inum"] = ""; //件数，double类型
                            //domBody[j]["citemcode"] = ""; //项目编码，string类型
                            //domBody[j]["innum"] = ""; //应发件数，double类型
                            //domBody[j]["cname"] = ""; //项目，string类型
                            //domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
                            //domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                            domBody[j]["idlsid"] = iDLsID; //发货单子表ID，int类型
                            //domBody[j]["isbsid"] = ""; //发票子表ID，int类型
                            //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                            //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                            //domBody[j]["isendquantity"] = ""; //发货数量，int类型
                            //domBody[j]["isendnum"] = ""; //发货件数，int类型
                            //domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                            //domBody[j]["iensid"] = ""; //委托子表id，double类型
                            //domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                            //domBody[j]["cbatchproperty1"] = ""; //属性1，double类型
                            //domBody[j]["cbatchproperty3"] = ""; //属性3，double类型
                            //domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                            //domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                            //domBody[j]["cbatchproperty4"] = ""; //属性4，double类型
                            //domBody[j]["cbatchproperty5"] = ""; //属性5，double类型
                            //domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                            //domBody[j]["cfree6"] = ""; //存货自由项6，string类型

                            //domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                            //domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                            //domBody[j]["cbatchproperty8"] = ""; //属性8，string类型
                            //domBody[j]["cbatchproperty9"] = ""; //属性9，string类型
                            //domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                            //domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                            //domBody[j]["cbatchproperty10"] = ""; //属性10，DateTime类型
                            //domBody[j]["dvdate"] = Convert.ToString(dts.Rows[i]["dVDate"]); //失效日期，DateTime类型
                            //domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                            //domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                            //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                            //domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                            //domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                            //domBody[j]["cbatchproperty2"] = ""; //属性2，double类型
                            //domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                            domBody[j]["iunitcost"] = iTaxUnitPrice; //单价，double类型
                            domBody[j]["iprice"] = iSum; //金额，double类型
                            //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                            //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                            //domBody[j]["ipunitcost"] = ""; //计划单价/售价，double类型
                            //domBody[j]["igrossweight"] = ""; //毛重，string类型
                            //domBody[j]["inetweight"] = ""; //净重，string类型
                            bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + Convert.ToString(dBody.Rows[k]["SKU"]) + "'", connstring));
                            if (bInvBatch == 1)
                            {
                                domBody[j]["cbatchproperty7"] = Convert.ToString(dBody.Rows[k]["LOTTABLE02"]); //属性6，string类型 炉号
                                domBody[j]["cbatchproperty6"] = Convert.ToString(dBody.Rows[k]["LOTTABLE03"]);//属性7，string类型  母卷号
                                domBody[j]["cbatch"] = ClsSystem.gnvl(dBody.Rows[k]["ID"], ""); //批号，string类型
                            }
                            else
                            {
                                domBody[j]["cbatchproperty7"] = ""; //属性6，string类型 炉号
                                domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号
                                domBody[j]["cbatch"] = ""; //批号，string类型
                            }
                            //  domBody[j]["cbatch"] = Convert.ToString(dts.Rows[i]["cBatch"]); //批号，string类型
                            //domBody[j]["iinvsncount"] = ""; //库存序列号，int类型

                            //j++;
                       // }
                    
                }

                //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
                broker.AssignNormalValue("domPosition", null);

                //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
                broker.AssignNormalValue("cnnFrom", null);

                //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
                broker.AssignNormalValue("VouchId", "");

                //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
                broker.AssignNormalValue("domMsg", domMsg);

                //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
                broker.AssignNormalValue("bCheck", true);

                //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                broker.AssignNormalValue("bBeforCheckStock", true);

                //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
                broker.AssignNormalValue("bIsRedVouch", false);

                //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
                broker.AssignNormalValue("sAddedState", "");

                //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
                broker.AssignNormalValue("bReMote", false);


                //第六步：调用API
                if (!broker.Invoke())
                {
                    //API处理
                    Exception apiEx = broker.GetException();
                    if (apiEx != null)
                    {
                        if (apiEx is MomSysException)
                        {
                            MomSysException sysEx = apiEx as MomSysException;
                            throw new Exception("系统异常：" + sysEx.Message);
                            //todo:异常处理
                        }
                        else if (apiEx is MomBizException)
                        {
                            MomBizException bizEx = apiEx as MomBizException;
                            throw new Exception("API异常：" + bizEx.Message);
                            //todo:异常处理
                        }
                        //异常原因
                        String exReason = broker.GetExceptionString();
                        if (exReason.Length != 0)
                        {
                            throw new Exception("异常原因：" + exReason);
                        }
                    }
                    //结束本次调用，释放API资源
                    broker.Release();
                    //ado.RollbackTrans();
                    return "API错误：" + apiEx.ToString().Substring(0, 100);
                }

                //第七步：获取返回结果

                //获取返回值
                //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
                System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


                //获取out/inout参数值

                //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                System.String errMsgRet = broker.GetResult("errMsg") as System.String;

                if (!result)
                {
                    broker.Release();
                    //ado.RollbackTrans();
                    return "API错误：" + errMsgRet;
                }

                //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                VouchNO = broker.GetResult("VouchId") as System.String;

                //DataTable dtRet = AdoAccess.ExecuteDT("select cCode from rdrecord32 where ID = '" + VouchIdRet + "'", conn);
                //if (dtRet.Rows.Count > 0)
                //{
                //    dgvMain.Rows[iRow].Cells["cCKCode"].Value = Convert.ToString(dtRet.Rows[0]["cCode"]);
                //}

                //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                //MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

                //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");

                //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");


                //for (int i = 0; i < list.length; i++)
                //{
                //    IXMLDOMNode node = list[i].attributes.getNamedItem("autoid");
                //    dts.Rows[i]["RD32sID"] = Convert.ToString(node.nodeValue);
                //}

                //DataTable dtRet = AdoAccess.ExecuteDT("select AutoID,iDLsID from rdrecords32 where ID = " + VouchIdRet + " ", conn);
                //for (int i = 0; i < dtRet.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dts.Rows.Count; j++)
                //    {
                //        if (Convert.ToString(dts.Rows[j]["iDLsID"]) == Convert.ToString(dtRet.Rows[i]["iDLsID"]))
                //        {
                //            dts.Rows[j]["RD32sID"] = Convert.ToString(dtRet.Rows[i]["AutoID"]);
                //        }

                //    }
                //}


                //结束本次调用，释放API资源
                broker.Release();

                StringBuilder sql = new StringBuilder();
                sql.Append(" update DispatchList set cSaleOut='ST' where dlid=" + DLID);
                //myCommand.CommandText = sql.ToString();
                //myCommand.ExecuteNonQuery();
                SqlAccess.ExecuteSql(sql.ToString(), connstring);

                //ado.CommitTrans();

                #endregion
            }
            catch (Exception ex)
            {

                // ado.RollbackTrans();
                return "API错误：" + ex.ToString().Substring(0, 50);
            }
            return VouchNO;

        }


        //产成品入库单
        public static string InRD10(U8Login.clsLogin u8Login, string connstring, DataTable dHead, DataTable dBody, int bredvouch, string crdcode)
        {
            System.String VouchNO = "";
            ADODB.Connection ado = new ADODB.Connection();
              try
            {

               

            #region 产成品入库单

            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
            //ado.BeginTrans();

            //第三步：设置API地址标识(Url)
            //当前API错误：添加新单据的地址标识为：U8API/ProductIn/Add
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/ProductIn/Add");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：10
            broker.AssignNormalValue("sVouchType", "10");

            //给BO表头参数DomHead赋值，此BO参数的业务类型为产成品入库单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数DomHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject DomHead = broker.GetBoParam("DomHead");
            DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

         string    cCode = Convert.ToString(dHead.Rows[0]["POKEY"]);
         Int64 MoId = 0;

            /****************************** 以下是必输字段 ****************************/
            DomHead[0]["id"] = "0"; //主关键字段，int类型
            DomHead[0]["ccode"] = Public.GetParentCode("0411", "", connstring); //入库单号，string类型
            string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 1);
              string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 0);
            rq1 = rq1.Insert(4, "/");
            rq1 = rq1.Insert(7, "/");
            DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToString(); //入库日期，DateTime类型
           // string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], ""); //ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where wmsbm='" + ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "") + "'", connstring), "");
            string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");

            if (cwhcode == "")
            {
                cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and  wmsbm='" + Convert.ToString(dBody.Rows[0]["TOLOC"]) + "'", connstring), "");
            }
            DomHead[0]["cwhname"] = Convert.ToString(SqlAccess.ExecuteScalar(" select cwhname from warehouse  with(nolock) where cWhCode='" + cwhcode+"'",connstring)); //仓库，string类型

            string codepcode = "";
          
                codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select MDeptCode  from mom_orderdetail  with(nolock) where moid in(select moid from mom_order where MoCode='" + cCode + "')", connstring), "");
                if (codepcode == "")
                { 
                    codepcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR2"], "");
                    if (codepcode == "")
                    {
                        codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
                    }
                }
           
            /***************************** 以下是非必输字段 ****************************/
            //DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
            //DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
            DomHead[0]["dnmaketime"] = DateTime.Now; //制单时间，DateTime类型
            //DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
            //DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
            //DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
            //DomHead[0]["iavaquantity"] = ""; //可用量，string类型
            //DomHead[0]["iavanum"] = ""; //可用件数，string类型
            //DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
            DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString(); //时间戳，string类型
          //  DomHead[0]["cpspcode"] = ""; //产品，string类型
            if (cCode != "")
            {
                MoId = Convert.ToInt64(ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select MoId  from mom_order  with(nolock) where MoCode='" + cCode + "'", connstring), "0"));
                if (ClsSystem.gnvl(MoId, "0") == "0")
                {
                    return "API错误:该单没有对应的生产订单";
                }
            }

            DomHead[0]["iproorderid"] = MoId; //生产订单ID，string类型
            DomHead[0]["cmpocode"] = cCode; //生产订单号，string类型
           // DomHead[0]["cprobatch"] = ""; //生产批号，string类型
            DomHead[0]["iverifystate"] = "0"; //iverifystate，string类型
            DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，string类型
            DomHead[0]["ireturncount"] = "0"; //ireturncount，string类型
        //    DomHead[0]["cdepname"] = ""; //部门，string类型
            //DomHead[0]["crdname"] = ""; //入库类别，string类型
            //DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
            DomHead[0]["cmemo"] = ClsSystem.gnvl(dHead.Rows[0]["RECEIPTKEY"], "");//备注，string类型
            //DomHead[0]["cchkperson"] = ""; //检验员，string类型
            DomHead[0]["cmaker"] = ClsSystem.gnvl(dHead.Rows[0]["EDITWHO"], ""); //制单人，string类型
            //DomHead[0]["chandler"] = ""; //审核人，string类型
            //DomHead[0]["itopsum"] = ""; //最高库存量，string类型
            //DomHead[0]["caccounter"] = ""; //记账人，string类型
            //DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
            //DomHead[0]["ipresent"] = ""; //现存量，string类型
            //DomHead[0]["isafesum"] = ""; //安全库存量，string类型
            DomHead[0]["cbustype"] = "成品入库"; //业务类型，int类型
         //   DomHead[0]["cpersonname"] = ""; //业务员，string类型
            //DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
            //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            //DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            //DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
            DomHead[0]["csource"] = "生产订单"; //单据来源，int类型
            //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            DomHead[0]["brdflag"] = "1"; //收发标志，string类型
            //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            //DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            DomHead[0]["cvouchtype"] = "10"; //单据类型，string类型
            DomHead[0]["cwhcode"] =cwhcode; //仓库编码，string类型
            DomHead[0]["crdcode"] = crdcode; //入库类别编码，string类型
            DomHead[0]["cdepcode"] = codepcode;//ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select top 1  MDeptCode from mom_orderdetail  with(nolock) where modid=" + ClsSystem.gnvl( dBody.Rows[0]["SUSR4"], ""), connstring), "");  //部门编码，string类型
       //     DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型
            DomHead[0]["vt_id"] = "63"; //模版号，int类型
        //    DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型

            //给BO表体参数domBody赋值，此BO参数的业务类型为产成品入库单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");


            DataTable dts = dBody;
           
            //DataTable dts = Public.CheckDataTable(dBody, "rdrecords10", ClsSystem.gnvl(dHead.Rows[0]["SERIALKEY"], ""), "0");

            //if (dts.Rows.Count < 1)
            //{
            //    return "API错误：表体无数据";
            //}
            domBody.RowCount = dts.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            string cinvcode = "";
            decimal iQuantity = 0;
            decimal iTaxUnitPrice = 0;
            decimal iSum = 0;
            int bInvBatch = 0;
            for (int j = 0; j < dts.Rows.Count; j++)
            {
                cinvcode = ClsSystem.gnvl(dts.Rows[j]["SKU"], "");
                iQuantity = Public.GetNum(dts.Rows[j]["QTYRECEIVED"]);//数量

                iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                 iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                 //iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + 17M / 100M), 4);//原币无税单价
                 //iMoney = Public.ChinaRound(iSum / (1M + 17M / 100M), 2);//原币无税金额
                 //iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                 //iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
                 //iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
                 //iNatMoney = Public.ChinaRound(iNatSum / (1 + 17M / 100M), 2);//本币无税金额
                 //iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                /****************************** 以下是必输字段 ****************************/
                domBody[j]["autoid"] = "0"; //主关键字段，int类型
                domBody[j]["cinvcode"] = cinvcode; //产品编码，string类型
                domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                /***************************** 以下是非必输字段 ****************************/
                if (cwhcode == "06")
                {
                    domBody[j]["cposition"] = "06";
                }
                domBody[j]["id"] = "0"; //与主表关联项，int类型
                //domBody[j]["cinvaddcode"] = ""; //产品代码，string类型
                //domBody[j]["cinvname"] = ""; //产品名称，string类型
                //domBody[j]["cinvstd"] = ""; //规格型号，string类型
                //domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                //domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                //domBody[j]["creplaceitem"] = ""; //替换件，string类型
                //domBody[j]["cposition"] = ""; //货位编码，string类型
                //domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                //domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                //domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                //domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                //domBody[j]["cbatchproperty1"] = ""; //批次属性1，double类型
                //domBody[j]["cbatchproperty2"] = ""; //批次属性2，double类型
                //domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                if (bInvBatch == 1)
                {

                    domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], ""); //批号，string类型
                }
                else
                {
                    domBody[j]["cbatch"] = "";
                }
                //domBody[j]["iinvexchrate"] = ""; //换算率，double类型
               // domBody[j]["inum"] = ""; //件数，double类型
                domBody[j]["iquantity"] = iQuantity; //数量，double类型
                domBody[j]["iunitcost"] = iTaxUnitPrice; //单价，double类型
                domBody[j]["iprice"] = iSum; //金额，double类型
                domBody[j]["ipunitcost"] = iTaxUnitPrice; //计划单价，double类型
                domBody[j]["ipprice"] = iSum; //计划金额，double类型
                //domBody[j]["dvdate"] = ""; //失效日期，DateTime类型
                //domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                //domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                //domBody[j]["dsdate"] = ""; //结算日期，DateTime类型
                //domBody[j]["ifquantity"] = ""; //实际数量，double类型
                //domBody[j]["ifnum"] = ""; //实际件数，double类型
               // domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                //domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                //domBody[j]["cbatchproperty3"] = ""; //批次属性3，double类型
                //domBody[j]["cbatchproperty4"] = ""; //批次属性4，double类型
                //domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                //domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                //domBody[j]["cbatchproperty5"] = ""; //批次属性5，double类型
                //domBody[j]["cbatchproperty6"] = ""; //批次属性6，string类型
                //domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                //domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                //domBody[j]["cbatchproperty7"] = ""; //批次属性7，string类型
                //domBody[j]["cbatchproperty8"] = ""; //批次属性8，string类型
                //domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                //domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                //domBody[j]["cbatchproperty9"] = ""; //批次属性9，string类型
                //domBody[j]["cbatchproperty10"] = ""; //批次属性10，DateTime类型
                //domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                //domBody[j]["inquantity"] = ""; //应收数量，double类型
                //domBody[j]["innum"] = ""; //应收件数，double类型
                //domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                domBody[j]["impoids"] = ClsSystem.gnvl(dts.Rows[j]["SUSR4"], ""); //生产订单子表ID，int类型
                //domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                domBody[j]["isodid"] = "";  //销售订单子表ID，string类型
                //domBody[j]["brelated"] = ""; //是否联副产品，int类型
                //domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                //domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                //domBody[j]["cvenname"] = ""; //供应商，string类型
                //domBody[j]["imassdate"] = ""; //保质期，int类型
                //domBody[j]["cassunit"] = ""; //库存单位码，string类型
                //domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                //domBody[j]["cposname"] = ""; //货位，string类型
                //domBody[j]["cmolotcode"] = ""; //生产批号，string类型
                //domBody[j]["cmassunit"] = ""; //保质期单位，int类型
                domBody[j]["csocode"] ="";//需求跟踪号，string类型
                domBody[j]["cmocode"] = cCode; //生产订单号，string类型
                //domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                //domBody[j]["cvmivenname"] = ""; //代管商，string类型
                //domBody[j]["bvmiused"] = ""; //代管消耗标识，int类型
                //domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                //domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
            //    domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                //domBody[j]["iordertype"] = ClsSystem.gnvl(dt.Rows[0]["OrderType"],""); //销售订单类别，int类型
                //domBody[j]["iorderdid"] = ClsSystem.gnvl(dt.Rows[0]["OrderDId"],""); //iorderdid，int类型
                //domBody[j]["iordercode"] =  ClsSystem.gnvl(dt.Rows[0]["OrderCode"],""); //销售订单号，string类型
                //domBody[j]["iorderseq"] =  ClsSystem.gnvl(dt.Rows[0]["OrderSeq"],""); //销售订单行号，string类型
                //domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                //domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                //domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                //domBody[j]["cciqbookcode"] = ""; //手册号，string类型
                //domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                //domBody[j]["copdesc"] = ""; //工序说明，string类型
                //domBody[j]["cmworkcentercode"] = ""; //工作中心编码，string类型
                //domBody[j]["cmworkcenter"] = ""; //工作中心，string类型
                domBody[j]["isotype"] = 0; //需求跟踪方式，int类型
                //domBody[j]["cbaccounter"] = ""; //记账人，string类型
              //  domBody[j]["bcosting"] = ""; //是否核算，string类型
              //  domBody[j]["isoseq"] = ClsSystem.gnvl(dt.Rows[0]["SoSeq"],""); //需求跟踪行号，string类型
                 Decimal imoseq=0;
                if (ClsSystem.gnvl(dts.Rows[j]["SUSR4"], "") != "")
                {
                    imoseq = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(sortseq ,0)  from mom_orderdetail a with(nolock)  where ModId =" + ClsSystem.gnvl(dts.Rows[j]["SUSR4"], ""), connstring));
                    if (imoseq == 0)
                    {
                        return "API错误:此单对应的生产订单表体找不到对应的sortseq行号";
                    }
                }
                domBody[j]["imoseq"] = imoseq;  //生产订单行号，string类型
                //domBody[j]["iopseq"] = ""; //工序行号，string类型
                //domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                domBody[j]["cdefine22"] = ClsSystem.gnvl(dHead.Rows[0]["RECEIPTKEY"], "");//表体自定义项1，string类型
                domBody[j]["cdefine28"] = ClsSystem.gnvl(dHead.Rows[0]["SERIALKEY"], "") + ClsSystem.gnvl(dts.Rows[j]["SERIALKEY"], ""); //表体自定义项7，string类型
                //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                //domBody[j]["cbarcode"] = ""; //条形码，string类型
                //domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], ""); ; //表体自定义项3，string类型
                //domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                //domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
                domBody[j]["cdefine26"] = ClsSystem.gnvl(dts.Rows[j]["SUSR2"], ""); //表体自定义项5，double类型
                //domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                //domBody[j]["citemcode"] = ""; //项目编码，string类型
                //domBody[j]["cname"] = ""; //项目，string类型
                //domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                //domBody[j]["citemcname"] = ""; //项目大类名称，string类型
            }

            //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
            broker.AssignNormalValue("domPosition",null);

            //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

            //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
            broker.AssignNormalValue("cnnFrom", null);

            //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
            broker.AssignNormalValue("VouchId","");

            //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
         //   MSXML2.IXMLDOMDocument2 domMsg = new MSXML2.IXMLDOMDocument2();
            MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("domMsg", domMsg);

            //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
            broker.AssignNormalValue("bCheck", true);

            //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
            broker.AssignNormalValue("bBeforCheckStock", true);

            //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
            broker.AssignNormalValue("bIsRedVouch", false);

            //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
            broker.AssignNormalValue("sAddedState", "");

            //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
            broker.AssignNormalValue("bReMote", false);

            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        Console.WriteLine("系统异常：" + sysEx.Message);
                     
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        Console.WriteLine("API异常：" + bizEx.Message);
                      
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        Console.WriteLine("异常原因：" + exReason);
                       
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
                //ado.RollbackTrans();
                return "API错误：" + apiEx.ToString().Substring(0, 100);
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
            System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());

            //获取out/inout参数值

            //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            System.String errMsgRet = broker.GetResult("errMsg") as System.String;
            if (result == false)
            {
                broker.Release();
                //ado.RollbackTrans();
                return "API错误：" + errMsgRet;
            }

            //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            VouchNO  = broker.GetResult("VouchId") as System.String;

            //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
           // MSXML2.IXMLDOMDocument2 domMsgRet = Convert.ToObject(broker.GetResult("domMsg"));
            MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

            //结束本次调用，释放API资源
            broker.Release();

            //ado.CommitTrans();
          
            #endregion
                  
               }
            catch (Exception  ex )
            {
                
                //ado.RollbackTrans();
                return "API错误：" + ex.ToString().Substring(0, 50);
            }
            return VouchNO;     
        }

        // 红字产成品入库单
        public static string InRD10_HZ(U8Login.clsLogin u8Login, string connstring, DataTable dt, DataTable dBody, DataTable dpicks, DataTable dFb, int bredvouch, string crdcode, string cWh, string cType, string cvoucode)
        {
            System.String VouchNO = "";
            //   ADODB.Connection ado = new ADODB.Connection();
            //    conn.BeginTrans();
            string BWB = "人民币";

            try
            {


                #region 产成品入库单

                string iPOsID = "";
                //string cCode = "";

                string cinvcode = "";
                string iarrsid = "";
                string cvencode = "";
                string darvdate = "";
                string ID = "";

                string cdepcode = "";
                string cptcode = "";
                string strxml = "";
                decimal iTaxUnitPrice = 0;
                decimal iSum = 0;
                decimal iUnitPrice = 0;
                decimal iMoney = 0;
                decimal iTax = 0;

                decimal iNatUnitPrice = 0;
                decimal iNatSum = 0;
                decimal iNatMoney = 0;
                decimal iNatTax = 0;
                decimal iInvExchRate = 0;
                decimal iTaxRate = 0;
                int bInvBatch = 0;




                //第一步：构造u8login对象并登陆(引用U8API类库中的Interop.U8Login.dll)
                //如果当前环境中有login对象则可以省去第一步


                //     string ado_connection = "PROVIDER=SQLOLEDB;" + connstring;
                //  WriteLog.writeLog("ado");

                //U8Login.clsLogin u8Login = new U8Login.clsLogin();

                //String sSubId = "AS";
                //String sAccID = "999";
                //String sYear = "2013";
                //String sUserID = "demo";
                //String sPassword = "";
                //String sDate = "2013-01-20";
                //String sServer = "localhost";
                //String sSerial = "";
                //if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial))
                //{
                //    Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                //    Marshal.FinalReleaseComObject(u8Login);
                //    return null;
                //}

                //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                U8EnvContext envContext = new U8EnvContext();
                envContext.U8Login = u8Login;
                //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
                //ado.BeginTrans();

                //销售所有接口均支持内部独立事务和外部事务，默认内部事务
                //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
                //envContext.BizDbConnection = conn;
                //envContext.IsIndependenceTransaction = false;


                //第三步：设置API地址标识(Url)
                //当前API错误：添加新单据的地址标识为：U8API/PuStoreIn/Add
                U8ApiAddress myApiAddress = new U8ApiAddress("U8API/ProductIn/Add");

                //第四步：构造APIBroker
                U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

                //第五步：API参数赋值

                //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：10
                broker.AssignNormalValue("sVouchType", "10");

                //给BO表头参数DomHead赋值，此BO参数的业务类型为采购入库单，属表头参数。BO参数均按引用传递
                //提示：给BO表头参数DomHead赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject DomHead = broker.GetBoParam("DomHead");

                DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
                //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义


                 //     cCode  = ClsSystem.gnvl(dt.Rows[0]["SUSR2"], "");
                  Int64 MoId = 0;
                /****************************** 以下是必输字段 ****************************/
                //     string    crdCode = Public.GetParentCode("24", "", connstring);
                DomHead[0]["id"] = "0"; //主关键字段，int类型
               

                DomHead[0]["ccode"] = Public.GetParentCode("0411", "", connstring); //入库单号，string类型

                string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dt.Rows[0]["ORDERDATE"], ""), 1);
                //  string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dt.Tables[0].Rows[0]["RECEIPTDATE"], ""), 0);
                rq1 = rq1.Insert(4, "/");
                rq1 = rq1.Insert(7, "/");

                DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToShortDateString(); //入库日期，DateTime类型
                //DomHead[0]["iverifystate"] = "0"; //iverifystate，int类型
                //DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，int类型
                //     DomHead[0]["cvenabbname"] = "上海用友"; //供货单位，string类型
                //if (cWh == "W")
                //{
                //    DomHead[0]["cbustype"] = "委外加工"; //业务类型，int类型
                //}
                //else
                //{
                //    DomHead[0]["cbustype"] = "普通采购"; //业务类型，int类型

                //}
                //DomHead[0]["cdefine2"] = "";

                DomHead[0]["cmaker"] = ClsSystem.gnvl(dt.Rows[0]["EDITWHO"], ""); //制单人，string类型
                //DomHead[0]["iexchrate"] = "1"; //汇率，double类型
                //DomHead[0]["cexch_name"] = "人民币"; //币种，string类型
                ////    DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString();  //时间戳，string类型
                //DomHead[0]["bpufirst"] = "0"; //采购期初标志，string类型
          //      DomHead[0]["cvencode"] = cvencode; //供货单位编码，string类型
               // DomHead[0]["cvouchtype"] = "10"; //单据类型，string类型
                //两边不一致  需要转换
                string cwhcode = ClsSystem.gnvl(dt.Rows[0]["SUSR1"], "");

                if (cwhcode == "")
                {
                    cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and  wmsbm='" + Convert.ToString(dFb.Rows[0]["LOC"]) + "'", connstring), "");
                }
                //if (cType == "102")
                //{
                //    if (ClsSystem.gnvl(dtst.Rows[0]["cflag"], "") == "1")
                //    {
                //        cwhcode = "03";
                //    }
                //}
                DomHead[0]["cwhname"] = Convert.ToString(SqlAccess.ExecuteScalar(" select cwhname from warehouse  with(nolock) where cWhCode='" + cwhcode + "'", connstring)); //仓库，string类型


                DomHead[0]["cwhcode"] = cwhcode;// ClsSystem.gnvl(dt.Rows[0]["cwhcode"], ""); //仓库编码，string类型

            //    DomHead[0]["brdflag"] = "1"; //收发标志，int类型

                string codepcode = "";
              //  codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select MDeptCode  from mom_orderdetail  with(nolock) where moid in(select moid from mom_order where MoCode='" + cCode + "')", connstring), "");
                
                    if (codepcode == "")
                    {
                        codepcode = ClsSystem.gnvl(dt.Rows[0]["SUSR2"], "");
                        if (codepcode == "")
                        {
                            codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
                        }

                    }
                   
                
                /***************************** 以下是非必输字段 ****************************/

                //DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
                //DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
                DomHead[0]["dnmaketime"] = DateTime.Now; //制单时间，DateTime类型
                //DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
               // DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
                //        DomHead[0]["cwhname"] = "测试仓"; //仓库，string类型
                DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString(); //时间戳，string类型

                DomHead[0]["iproorderid"] = ""; //生产订单ID，string类型
                DomHead[0]["cmpocode"] = ""; //生产订单号，string类型
                // DomHead[0]["cprobatch"] = ""; //生产批号，string类型
                DomHead[0]["iverifystate"] = "0"; //iverifystate，string类型
                DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，string类型
                DomHead[0]["ireturncount"] = "0"; //ireturncount，string类型
                //    DomHead[0]["cdepname"] = ""; //部门，string类型
                //DomHead[0]["crdname"] = ""; //入库类别，string类型
                //DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
                DomHead[0]["cmemo"] = ClsSystem.gnvl(dt.Rows[0]["ORDERKEY"], "");//备注，string类型
                //DomHead[0]["cchkperson"] = ""; //检验员，string类型
                DomHead[0]["cmaker"] = ClsSystem.gnvl(dt.Rows[0]["EDITWHO"], ""); //制单人，string类型
                //DomHead[0]["chandler"] = ""; //审核人，string类型
                //DomHead[0]["itopsum"] = ""; //最高库存量，string类型
                //DomHead[0]["caccounter"] = ""; //记账人，string类型
                //DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
                //DomHead[0]["ipresent"] = ""; //现存量，string类型
                //DomHead[0]["isafesum"] = ""; //安全库存量，string类型
                DomHead[0]["cbustype"] = "成品入库"; //业务类型，int类型
                //   DomHead[0]["cpersonname"] = ""; //业务员，string类型
                //DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
                //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
                //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
                //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
                //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
                //DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
                //DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
                DomHead[0]["csource"] = "库存"; //单据来源，int类型
                //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
                //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
                //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
                DomHead[0]["brdflag"] = "1"; //收发标志，string类型
                //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
                //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
                //DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
                //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
                //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
                DomHead[0]["cvouchtype"] = "10"; //单据类型，string类型
                DomHead[0]["cwhcode"] = cwhcode; //仓库编码，string类型
                DomHead[0]["crdcode"] = crdcode; //入库类别编码，string类型
                DomHead[0]["cdepcode"] = codepcode;
                //     DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型
                DomHead[0]["vt_id"] = "63"; //模版号，int类型



                //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
                //提示：给BO表体参数domBody赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject domBody = broker.GetBoParam("domBody");

                ////过滤重复数据
                //DataTable dts = Public.CheckDataTable(dtst, "rdrecords01", ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], ""), "0");

                domBody.RowCount = dBody.Rows.Count * dFb.Rows.Count; //设置BO对象行数

                //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
                //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
                DataTable dtx = null;

                string cPdID = "";
                int j = 0;


                for (int k = 0; k < dBody.Rows.Count; k++)
                {
                    cinvcode = ClsSystem.gnvl(dBody.Rows[k]["SKU"], "");
                    cPdID = ClsSystem.gnvl(dBody.Rows[k]["ShipmentOrderDetail_ID"], "");

                    DataView rowfilter2 = new DataView(dpicks);
                    rowfilter2.RowFilter = "ShipmentOrderDetail_ID =" + cPdID;
                    rowfilter2.RowStateFilter = DataViewRowState.CurrentRows;
                    DataTable dpick = rowfilter2.ToTable();

                    if (dpick.Rows.Count > 0)
                    {

                        string cpicksid = ClsSystem.gnvl(dpick.Rows[0]["PickDetails_ID"], "");

                        DataView rowfilter1 = new DataView(dFb);
                        rowfilter1.RowFilter = "PickDetails_ID =" + cpicksid;
                        rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                        DataTable dts = rowfilter1.ToTable();
                        if (dts.Rows.Count > 0)
                        {
                            for (int i = 0; i < dts.Rows.Count; i++)
                            {

                                //判断是不是寄存品

                                decimal iQuantity = Public.GetNum(dts.Rows[i]["QTY"]);//数量
                                if (bredvouch == 1)
                                {
                                    iQuantity = -iQuantity;
                                }


                                //   dtx = SqlAccess.ExecuteSqlDataTable("select top 1 d.ID,isnull(d.iTaxPrice,0) as iTaxPrice ,isnull(d.iUnitPrice,0) as iUnitPrice,isnull(iTax,0) as iTax,isnull(d.iPerTaxRate,0) as iPerTaxRate ,isnull(d.iNatUnitPrice,0) as iNatUnitPrice     from PO_Podetails d with(nolock) join PO_Pomain m with(nolock) on d.POID=m.POID  where m.cPOID ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring);

                                //if (dtx.Rows.Count > 0)
                                //{
                                //    iPOsID = Convert.ToString(dtx.Rows[0]["ID"]);
                                //    iTaxRate = Convert.ToDecimal(dtx.Rows[0]["iPerTaxRate"]);
                                //    //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));

                                //    iUnitPrice = Public.GetNum(dtx.Rows[0]["iUnitPrice"]);

                                //    iTaxUnitPrice = Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价

                                //    iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                                //    iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                //    iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                                //    iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                                //    iNatSum = Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                                //    iNatMoney = Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                                //    iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额



                                //}

                                iTaxRate = 17;

                                iUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价

                                iTaxUnitPrice = Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价

                                iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                                iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                                iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                                iNatSum = Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                                iNatMoney = Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                                iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                                iInvExchRate = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(c.iChangRate,0)  from Inventory a with(nolock) join ComputationUnit c with(nolock) on a.cSTComUnitCode =c.cComunitCode where a.cinvcode='" + cinvcode + "'", connstring));
                                //if (iInvExchRate == 0)
                                //{
                                //    iInvExchRate = 1;
                                //}
                                /****************************** 以下是必输字段 ****************************/
                                domBody[j]["autoid"] = "0"; //主关键字段，int类型
                                domBody[j]["id"] = "0"; //与收发记录主表关联项，int类型
                                domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                                //   domBody[j]["cinvm_unit"] = Convert.ToString(SqlAccess.ExecuteScalar("select cComUnitCode  from inventory with(nolock) where cinvcode='" + cinvcode + "'", connstring)); //主计量单位，string类型
                                domBody[j]["iquantity"] = iQuantity; //数量，double类型
                                domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
                               // domBody[j]["iMatSettleState"] = "0"; //iMatSettleState，int类型
                                if (cwhcode == "06")
                                {
                                    domBody[j]["cposition"] = "06";
                                }
                                else
                                {
                                    domBody[j]["cposition"] = "";
                                }
                                /***************************** 以下是非必输字段 ****************************/
                                //domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                                //domBody[j]["cinvname"] = ""; //存货名称，string类型
                                //domBody[j]["cinvstd"] = ""; //规格型号，string类型
                                //domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                                //            domBody[j]["bservice"] = "0"; //是否费用，string类型
                                //domBody[j]["cinvccode"] = ""; //所属分类码，string类型
                                domBody[j]["iinvexchrate"] = iInvExchRate; //换算率，double类型


                                if (iInvExchRate != 0)
                                {
                                    domBody[j]["inum"] = iQuantity / iInvExchRate; //件数，double类型
                                }
                                else
                                {
                                    domBody[j]["inum"] = ""; //件数，double类型
                                }

                                bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                                if (bInvBatch == 1)
                                {
                                  //  domBody[j]["binvbatch"] = "1"; //批次管理，string类型
                                    domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[j]["ID"], ""); //批号，string类型
                                    domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[j]["LOTTABLE02"]); //属性6，string类型 炉号
                                    domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[j]["LOTTABLE03"]);//属性7，string类型  母卷号
                                }
                                else
                                {
                                  //  domBody[j]["binvbatch"] = "0"; //批次管理，string类型
                                    domBody[j]["cbatch"] = "";
                                    domBody[j]["cbatchproperty7"] = ""; //属性6，string类型 炉号
                                    domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号
                                }
                                domBody[j]["iunitcost"] = iTaxUnitPrice; //单价，double类型
                                domBody[j]["iprice"] = iSum; //金额，double类型
                                domBody[j]["ipunitcost"] = iTaxUnitPrice; //计划单价，double类型
                                domBody[j]["ipprice"] = iSum; //计划金额，double类型
                                //domBody[j]["dvdate"] = ""; //失效日期，DateTime类型
                                //domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                                //domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                                //domBody[j]["dsdate"] = ""; //结算日期，DateTime类型
                                //domBody[j]["ifquantity"] = ""; //实际数量，double类型
                                //domBody[j]["ifnum"] = ""; //实际件数，double类型
                                // domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                                //domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                                //domBody[j]["cbatchproperty3"] = ""; //批次属性3，double类型
                                //domBody[j]["cbatchproperty4"] = ""; //批次属性4，double类型
                                //domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                                //domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                                //domBody[j]["cbatchproperty5"] = ""; //批次属性5，double类型
                                //domBody[j]["cbatchproperty6"] = ""; //批次属性6，string类型
                                //domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                                //domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                                //domBody[j]["cbatchproperty7"] = ""; //批次属性7，string类型
                                //domBody[j]["cbatchproperty8"] = ""; //批次属性8，string类型
                                //domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                                //domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                                //domBody[j]["cbatchproperty9"] = ""; //批次属性9，string类型
                                //domBody[j]["cbatchproperty10"] = ""; //批次属性10，DateTime类型
                                //domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                                //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                                //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                                //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                                //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                                //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                                //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                                //domBody[j]["inquantity"] = ""; //应收数量，double类型
                                //domBody[j]["innum"] = ""; //应收件数，double类型
                                //domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                                domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                                //domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                                domBody[j]["isodid"] = "";  //销售订单子表ID，string类型
                                //domBody[j]["brelated"] = ""; //是否联副产品，int类型
                                //domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                                //domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                                //domBody[j]["cvenname"] = ""; //供应商，string类型
                                //domBody[j]["imassdate"] = ""; //保质期，int类型
                                //domBody[j]["cassunit"] = ""; //库存单位码，string类型
                                //domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                                //domBody[j]["cposname"] = ""; //货位，string类型
                                //domBody[j]["cmolotcode"] = ""; //生产批号，string类型
                                //domBody[j]["cmassunit"] = ""; //保质期单位，int类型
                                domBody[j]["csocode"] = "";//需求跟踪号，string类型
                                domBody[j]["cmocode"] = ""; //生产订单号，string类型
                                //domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                                //domBody[j]["cvmivenname"] = ""; //代管商，string类型
                                //domBody[j]["bvmiused"] = ""; //代管消耗标识，int类型
                                //domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                                //domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                                //    domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                                //domBody[j]["iordertype"] = ClsSystem.gnvl(dt.Rows[0]["OrderType"],""); //销售订单类别，int类型
                                //domBody[j]["iorderdid"] = ClsSystem.gnvl(dt.Rows[0]["OrderDId"],""); //iorderdid，int类型
                                //domBody[j]["iordercode"] =  ClsSystem.gnvl(dt.Rows[0]["OrderCode"],""); //销售订单号，string类型
                                //domBody[j]["iorderseq"] =  ClsSystem.gnvl(dt.Rows[0]["OrderSeq"],""); //销售订单行号，string类型
                                //domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                                //domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                                //domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                                //domBody[j]["cciqbookcode"] = ""; //手册号，string类型
                                //domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                                //domBody[j]["copdesc"] = ""; //工序说明，string类型
                                //domBody[j]["cmworkcentercode"] = ""; //工作中心编码，string类型
                                //domBody[j]["cmworkcenter"] = ""; //工作中心，string类型
                                domBody[j]["isotype"] = 0; //需求跟踪方式，int类型
                                //domBody[j]["cbaccounter"] = ""; //记账人，string类型
                                //  domBody[j]["bcosting"] = ""; //是否核算，string类型
                                //  domBody[j]["isoseq"] = ClsSystem.gnvl(dt.Rows[0]["SoSeq"],""); //需求跟踪行号，string类型
                                //Decimal imoseq = 0;
                                //if (ClsSystem.gnvl(dts.Rows[j]["SUSR4"], "") != "")
                                //{
                                //    imoseq = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(sortseq ,0)  from mom_orderdetail a with(nolock)  where ModId =" + ClsSystem.gnvl(dts.Rows[j]["SUSR4"], ""), connstring));
                                //    if (imoseq == 0)
                                //    {
                                //        return "API错误:此单对应的生产订单表体找不到对应的sortseq行号";
                                //    }
                                //}
                                domBody[j]["imoseq"] = "";  //生产订单行号，string类型
                                //domBody[j]["iopseq"] = ""; //工序行号，string类型
                                //domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                                //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                                domBody[j]["cdefine22"] = ClsSystem.gnvl(dt.Rows[0]["ORDERKEY"], ""); //表体自定义项1，string类型
                                domBody[j]["cdefine28"] = ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], "") + ClsSystem.gnvl(dts.Rows[j]["SERIALKEY"], ""); //表体自定义项7，string类型
                                //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                                //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                                //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                                //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                                //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                                //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                                //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                                //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                                //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                                //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                                //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                                //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                                //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                                //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                                //domBody[j]["cbarcode"] = ""; //条形码，string类型
                                //domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                                domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[j]["ID"], ""); //表体自定义项3，string类型
                                //domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                                //domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
                                domBody[j]["cdefine26"] = ClsSystem.gnvl(dBody.Rows[k]["SUSR2"], ""); //表体自定义项5，double类型
                                j++;
                            }
                        }
                    }
                }
                //     Public.WriteLog("c:\\wwww.txt", "5 DOM结束");

                //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
                broker.AssignNormalValue("domPosition", null);

                //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
                broker.AssignNormalValue("cnnFrom", null);

                //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
                broker.AssignNormalValue("VouchId", "");

                //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
                broker.AssignNormalValue("domMsg", domMsg);

                //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
                broker.AssignNormalValue("bCheck", true);

                //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                broker.AssignNormalValue("bBeforCheckStock", true);

                //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
                if (bredvouch == 0)
                {
                    broker.AssignNormalValue("bIsRedVouch", false);
                }
                else
                {
                    broker.AssignNormalValue("bIsRedVouch", true);
                }

                //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
                broker.AssignNormalValue("sAddedState", "");

                //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
                broker.AssignNormalValue("bReMote", false);

                //    Public.WriteLog("c:\\wwww.txt", "6调用API");


                //第六步：调用API
                if (!broker.Invoke())
                {
                    //API处理
                    Exception apiEx = broker.GetException();
                    if (apiEx != null)
                    {
                        if (apiEx is MomSysException)
                        {
                            MomSysException sysEx = apiEx as MomSysException;

                            //strxml = @"<Response>";
                            //strxml = strxml + "<success>false</success> ";
                            //strxml = strxml + "<desc>" + sysEx.Message + "</desc>";
                            //strxml = strxml + "</Response>";
                            ////conn.RollbackTrans();
                            ////    myTrans.Rollback();
                            //return strxml;
                            throw new Exception("系统异常：" + sysEx.Message);
                            //todo:异常处理
                        }
                        else if (apiEx is MomBizException)
                        {
                            MomBizException bizEx = apiEx as MomBizException;
                            //strxml = @"<Response>";
                            //strxml = strxml + "<success>false</success> ";
                            //strxml = strxml + "<desc>" + bizEx.Message + "</desc>";
                            //strxml = strxml + "</Response>";
                            ////conn.RollbackTrans();
                            ////  myTrans.Rollback();
                            //return strxml;
                            throw new Exception("API异常：" + bizEx.Message);
                            //todo:异常处理
                        }
                        //异常原因
                        String exReason = broker.GetExceptionString();
                        if (exReason.Length != 0)
                        {
                            //strxml = @"<Response>";
                            //strxml = strxml + "<success>false</success> ";
                            //strxml = strxml + "<desc>" + exReason + "</desc>";
                            //strxml = strxml + "</Response>";
                            ////conn.RollbackTrans();
                            ////   myTrans.Rollback();
                            //return strxml;
                            throw new Exception("异常原因：" + exReason);
                        }
                    }
                    //结束本次调用，释放API资源
                    broker.Release();
                    //      ado.RollbackTrans();
                    return "API错误：" + apiEx.ToString().Substring(0, 100);
                }

                //第七步：获取返回结果

                //获取返回值
                //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
                System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


                //获取out/inout参数值

                //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                System.String errMsgRet = broker.GetResult("errMsg") as System.String;

                if (!result)
                {
                    broker.Release();
                    //   ado.RollbackTrans();
                    return "U8错误API:" + errMsgRet;

                }

                //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                VouchNO = broker.GetResult("VouchId") as System.String;



                //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));


                //结束本次调用，释放API资源
                broker.Release();

                //   ado.CommitTrans();

                //   VouchNO = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select cCode  from RdRecord01 where id=" + VouchIdRet, connstring), "");

                #endregion


            }
            catch (Exception ex)
            {

                //  ado.RollbackTrans();
                return "API错误：" + ex.ToString().Substring(0, 50);
            }
            return VouchNO;

        }
        //采购入库单
        public static string InRD01(U8Login.clsLogin u8Login, string connstring, DataTable dt, DataTable dBody, int bredvouch, string crdcode, string cWh, string cType, string cvoucode)
        {
            System.String VouchNO = "";
            ADODB.Connection ado = new ADODB.Connection();
            //    conn.BeginTrans();
            string BWB = "人民币";

            try
            {             
           
            
            #region 采购入库单

            string iPOsID = "";
            string cCode = "";
         
            string cinvcode = "";
            string iarrsid = "";
            string cvencode = "";
            string darvdate = "";
            string ID = "";
          
            string cdepcode = "";
            string cptcode = "";
            string strxml = "";
            decimal iTaxUnitPrice = 0;
            decimal iSum = 0;
            decimal iUnitPrice = 0;
            decimal iMoney = 0;
            decimal iTax = 0;

            decimal iNatUnitPrice = 0;
            decimal iNatSum = 0;
            decimal iNatMoney = 0;
            decimal iNatTax = 0;
            decimal iInvExchRate = 0;
            decimal iTaxRate = 0;
            int bInvBatch = 0;


          

            //第一步：构造u8login对象并登陆(引用U8API类库中的Interop.U8Login.dll)
            //如果当前环境中有login对象则可以省去第一步

           
       //     string ado_connection = "PROVIDER=SQLOLEDB;" + connstring;
          //  WriteLog.writeLog("ado");
           
            //U8Login.clsLogin u8Login = new U8Login.clsLogin();
           
            //String sSubId = "AS";
            //String sAccID = "999";
            //String sYear = "2013";
            //String sUserID = "demo";
            //String sPassword = "";
            //String sDate = "2013-01-20";
            //String sServer = "localhost";
            //String sSerial = "";
            //if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial))
            //{
            //    Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
            //    Marshal.FinalReleaseComObject(u8Login);
            //    return null;
            //}
           
            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;
           // ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
           // ado.BeginTrans();

            //销售所有接口均支持内部独立事务和外部事务，默认内部事务
            //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
            //envContext.BizDbConnection = conn;
            //envContext.IsIndependenceTransaction = false;


            //第三步：设置API地址标识(Url)
            //当前API错误：添加新单据的地址标识为：U8API/PuStoreIn/Add
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/PuStoreIn/Add");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：01
            broker.AssignNormalValue("sVouchType", "01");

            //给BO表头参数DomHead赋值，此BO参数的业务类型为采购入库单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数DomHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject DomHead = broker.GetBoParam("DomHead");
            DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            //for (int i = 0; i < dthead.Rows.Count; i++)
            //{
            cCode = Convert.ToString(dt.Rows[0]["POKEY"]);
          //  cCode = "0000000050";
         //   bredvouch = "0";            //Convert.ToString(dt.Rows[0]["bredvouch"]);
         
           // darvdate = Convert.ToString(dt.Rows[iRow]["cvoudate"]);

                //得到表体数据

            DataView rowfilter = new DataView(dBody);
            rowfilter.RowFilter = "SUSR1= '" + cvoucode + "'";
            rowfilter.RowStateFilter = DataViewRowState.CurrentRows;
            DataTable dtst = rowfilter.ToTable();
            if (dtst.Rows.Count < 1 || dtst == null)
            {
                return "API错误:当收货类型为：钢材采购入库101、钢材委外完工调拨入库103，当除了以上2种收货类型以外的收货类型，该字段为空";
            }
           // string cpoid = "";
            if (cCode != "")
            {
                if (cWh != "W")
                {
                    DataTable dtcx = SqlAccess.ExecuteSqlDataTable("select cVenCode,cdepcode,cptcode,dPODate ,POID,cexch_name  from PO_Pomain with(nolock) where cPOID='" + cCode + "'", connstring);
                    if (dtcx.Rows.Count > 0)
                    {
                        // cpoid = Convert.ToString(dtcx.Rows[0]["cpocode"]);
                        cdepcode = Convert.ToString(dtcx.Rows[0]["cdepcode"]);
                        cptcode = Convert.ToString(dtcx.Rows[0]["cptcode"]);
                        cvencode = Convert.ToString(dtcx.Rows[0]["cVenCode"]);
                        ID = Convert.ToString(dtcx.Rows[0]["POID"]);
                        BWB = Convert.ToString(dtcx.Rows[0]["cexch_name"]);

                    }
                    else
                    {

                        return "API错误:此单没有对应的采购订单";
                    }
                }
                else
                {
                    DataTable dtcx = SqlAccess.ExecuteSqlDataTable("select cVenCode,cdepcode,cptcode,dDate ,MOID,cexch_name  from OM_MOMain with(nolock) where ccode='" + cCode + "'", connstring);
                    if (dtcx.Rows.Count > 0)
                    {
                        // cpoid = Convert.ToString(dtcx.Rows[0]["cpocode"]);
                        cdepcode = Convert.ToString(dtcx.Rows[0]["cdepcode"]);
                        cptcode = Convert.ToString(dtcx.Rows[0]["cptcode"]);
                        cvencode = Convert.ToString(dtcx.Rows[0]["cVenCode"]);
                        ID = Convert.ToString(dtcx.Rows[0]["MOID"]);
                        BWB = Convert.ToString(dtcx.Rows[0]["cexch_name"]);

                    }
                    else
                    {

                        return "API错误:此单没有对应的委外订单";
                    }
                }
            }

            
               if (cdepcode == "")
                 {
                     cdepcode = ClsSystem.gnvl(dt.Rows[0]["SUSR2"], "");
                     //if (cdepcode == "")
                     //{
                     //    cdepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
                     //}

                 }
            /****************************** 以下是必输字段 ****************************/
            //     string    crdCode = Public.GetParentCode("24", "", connstring);
            DomHead[0]["id"] = "0"; //主关键字段，int类型
            DomHead[0]["bomfirst"] = "0"; //委外期初标志，string类型
            string flag = "";
            if (cType == "101")
            {
                int id = 0;
                //给采购入库单的单号格式
                flag = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cDefine2   from rdrecord01  with(nolock)  where cDefine2='" + Convert.ToString(dtst.Rows[0]["SUSR1"]) + "'", connstring), "");
                if (flag == "")
                {
                    DomHead[0]["ccode"] = Convert.ToString(dtst.Rows[0]["SUSR1"]) + "_01";
                }
                else
                {
                    string flag1 = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select max(ccode)   from rdrecord01  with(nolock)  where cDefine2='" + Convert.ToString(dtst.Rows[0]["SUSR1"]) + "'", connstring), "");
                    string[] Result = flag1.Split('_');
                    if (Result.Length > 1)
                    {
                        id = Convert.ToInt32(Result[1].ToString()) + 1;
                    }
                    else
                    {
                        id = 1;
                    }
                    if (id.ToString().Length > 1)
                    {
                        DomHead[0]["ccode"] = Convert.ToString(dtst.Rows[0]["SUSR1"]) + "_" + id;
                    }
                    else
                    {
                        DomHead[0]["ccode"] = Convert.ToString(dtst.Rows[0]["SUSR1"]) + "_0" + id;
                    }
                }
            }
            else
            {
                DomHead[0]["ccode"] = Public.GetParentCode("24", "", connstring); //入库单号，string类型
            }
           string rq1= Public.GetStr(" ", ClsSystem.gnvl(dt.Rows[0]["RECEIPTDATE"], ""), 1);
         //  string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dt.Tables[0].Rows[0]["RECEIPTDATE"], ""), 0);
           rq1 = rq1.Insert(4, "/");
           rq1 = rq1.Insert(7, "/");
         
           DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToShortDateString(); //入库日期，DateTime类型
            DomHead[0]["iverifystate"] = "0"; //iverifystate，int类型
            DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，int类型
            //     DomHead[0]["cvenabbname"] = "上海用友"; //供货单位，string类型
            if (cWh == "W")
            {
                DomHead[0]["cbustype"] = "委外加工"; //业务类型，int类型
            }
            else
            {
                DomHead[0]["cbustype"] = "普通采购"; //业务类型，int类型
               
            }
            DomHead[0]["cdefine2"] = ClsSystem.gnvl(dtst.Rows[0]["SUSR1"], "");

            DomHead[0]["cmaker"] = ClsSystem.gnvl(dt.Rows[0]["EDITWHO"], ""); //制单人，string类型
            DomHead[0]["iexchrate"] = "1"; //汇率，double类型
            DomHead[0]["cexch_name"] = BWB; //币种，string类型
            DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString();  //时间戳，string类型
            DomHead[0]["bpufirst"] = "0"; //采购期初标志，string类型
            DomHead[0]["cvencode"] = cvencode; //供货单位编码，string类型
            DomHead[0]["cvouchtype"] ="01"; //单据类型，string类型
                //两边不一致  需要转换
            string cwhcode = ClsSystem.gnvl(dt.Rows[0]["SUSR1"], "");
           
            if (cwhcode == "")
            {
                 cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and  wmsbm='" + Convert.ToString(dtst.Rows[0]["TOLOC"]) + "'", connstring), "");
            }
            if (cType == "102")
            {
                if (ClsSystem.gnvl(dtst.Rows[0]["cflag"], "") == "1")
                {
                    cwhcode = "03";
                }
            }
              
            DomHead[0]["cwhcode"] = cwhcode;// ClsSystem.gnvl(dt.Rows[0]["cwhcode"], ""); //仓库编码，string类型

            DomHead[0]["brdflag"] = "1"; //收发标志，int类型
            if (cWh == "W")
            {
                DomHead[0]["csource"] = "委外订单"; //单据来源，int类型
            }
            else
            {
                DomHead[0]["csource"] = "采购订单"; //单据来源，int类型}
            }
            

            /***************************** 以下是非必输字段 ****************************/

            //DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
            //DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
            DomHead[0]["dnmaketime"] = DateTime.Now; //制单时间，DateTime类型
            //DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
            //       DomHead[0]["dnverifytime"] = Convert.ToString(dt.Rows[0]["dnverifytime"]); //审核时间，DateTime类型
            //        DomHead[0]["cwhname"] = "测试仓"; //仓库，string类型



            DomHead[0]["cordercode"] = cCode; //订单号，string类型
            DomHead[0]["carvcode"] = ""; //到货单号，string类型
            DomHead[0]["ireturncount"] = "0"; //ireturncount，string类型
            //DomHead[0]["cbuscode"] = ""; //业务号，string类型
            //DomHead[0]["cdepname"] = ""; //部门，string类型
            //DomHead[0]["cpersonname"] = ""; //业务员，string类型
            DomHead[0]["darvdate"] = ""; //到货日期，DateTime类型
            //DomHead[0]["cptname"] = ""; //采购类型，string类型
            //DomHead[0]["crdname"] = ""; //入库类别，string类型
            //         DomHead[0]["dveridate"] = Convert.ToString(dt.Rows[0]["dVeriDate"]); //审核日期，DateTime类型
            //DomHead[0]["cvenpuomprotocol"] = ""; //收付款协议编码，string类型
            //DomHead[0]["cvenpuomprotocolname"] = ""; //收付款协议名称，string类型
            //DomHead[0]["dcreditstart"] = ""; //立账日，DateTime类型
            //DomHead[0]["icreditperiod"] = ""; //账期，int类型
            //DomHead[0]["dgatheringdate"] = ""; //到期日，DateTime类型
            DomHead[0]["bcredit"] = "0"; //是否为立账单据，int类型
            //DomHead[0]["cvenfullname"] = ""; //供应商全称，string类型
            DomHead[0]["cmemo"] = ClsSystem.gnvl(dt.Rows[0]["RECEIPTKEY"], ""); //备注，string类型
            //        DomHead[0]["chandler"] = Convert.ToString(dt.Rows[0]["cHandler"]); //审核人，string类型
            //DomHead[0]["caccounter"] = ""; //记账人，string类型
            //DomHead[0]["ipresent"] = ""; //现存量，string类型
            DomHead[0]["itaxrate"] = "17"; //税率，double类型
            //DomHead[0]["isalebillid"] = ""; //发票号，string类型
            DomHead[0]["ipurorderid"] = ID; //采购订单ID，string类型
            DomHead[0]["ipurarriveid"] = ""; //采购到货单ID，string类型
            //DomHead[0]["iarriveid"] = ""; //到货单ID，string类型
            //DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
            //DomHead[0]["iavaquantity"] = ""; //可用量，string类型
            //DomHead[0]["iavanum"] = ""; //可用件数，string类型
            //DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
            //DomHead[0]["gspcheck"] = ""; //gsp复核标志，string类型
            //DomHead[0]["cchkperson"] = ""; //检验员，string类型
            //DomHead[0]["cchkcode"] = ""; //检验单号，string类型
            DomHead[0]["vt_id"] = "27"; //模版号，int类型
            DomHead[0]["cdepcode"] = cdepcode; //部门编码，string类型
            //DomHead[0]["cbillcode"] = ""; //发票id，string类型
            DomHead[0]["cptcode"] = cptcode; //采购类型编码，string类型
            //DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型

            DomHead[0]["crdcode"] = crdcode; //入库类别编码，string类型-----------------------------------------------------
           
            //DomHead[0]["isafesum"] = ""; //安全库存量，string类型
            //DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
            //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            //DomHead[0]["itopsum"] = ""; //最高库存量，string类型
            //DomHead[0]["iproorderid"] = ""; //生产订单Id，string类型
            //         DomHead[0]["bisstqc"] = "0"; //库存期初标记，string类型
            //DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            //DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            //DomHead[0]["cdefine3"] = Convert.ToString(dt.Rows[i]["cCode"]); //表头自定义项3，string类型
            //DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
            //           DomHead[0]["idiscounttaxtype"] = "0"; //扣税类别，int类型
            //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            // DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
            //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            //  }
            //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");

                //过滤重复数据
       //     DataTable dts = Public.CheckDataTable(dtst, "rdrecords01", ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], ""), "0");
            DataTable dts = dtst;
          
            domBody.RowCount = dts.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            DataTable dtx = null;
                DataTable dtbefore=null;
                if (dts.Rows.Count > 0)
                {
                    for (int j = 0; j < dts.Rows.Count; j++)
                    {

                        //判断是不是寄存品
                        cinvcode = Convert.ToString(dts.Rows[j]["SKU"]);
                        decimal iQuantity = Public.GetNum(dts.Rows[j]["QTYRECEIVED"]);//数量
                        if (bredvouch == 1)
                        {
                            iQuantity = -iQuantity;
                        }

                        if (cCode != "")
                        {
                            if (cWh != "W")
                            {
                                dtx = SqlAccess.ExecuteSqlDataTable("select top 1 d.ID,isnull(d.iTaxPrice,0) as iTaxPrice ,isnull(d.iUnitPrice,0) as iUnitPrice,isnull(iTax,0) as iTax,isnull(d.iPerTaxRate,0) as iPerTaxRate ,isnull(d.iNatUnitPrice,0) as iNatUnitPrice     from PO_Podetails d with(nolock) join PO_Pomain m with(nolock) on d.POID=m.POID  where m.cPOID ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring);

                                if (dtx.Rows.Count > 0)
                                {
                                    iPOsID = Convert.ToString(dtx.Rows[0]["ID"]);
                                    iTaxRate = Convert.ToDecimal(dtx.Rows[0]["iPerTaxRate"]);
                                    //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));

                                    iUnitPrice = Public.GetNum(dtx.Rows[0]["iUnitPrice"]);

                                    iTaxUnitPrice = Public.GetNum(dtx.Rows[0]["iTaxPrice"]); //Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价
                                    ////yuyang chag====================================
                                    iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                    iMoney = Public.ChinaRound(iSum / (1M + iTaxRate / 100M), 2);
                                    iTax = Public.ChinaRound(iSum - iMoney, 2);
                                    //===============================================
                                    //iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                                    //iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                    //iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                                    iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                                    iNatSum = iSum; //Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                                    iNatMoney = iMoney;// Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                                    iNatTax = iTax;// Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额



                                }
                                else
                                {
                                    return "API错误此单没有对应的采购订单表体id";
                                }
                            }
                            else
                            {
                                dtx = SqlAccess.ExecuteSqlDataTable(@" select top 1 imoney, d.MODetailsID ID,isnull(d.iTaxPrice,0) as iTaxPrice ,isnull(d.iUnitPrice,0) as 
 iUnitPrice,isnull(iTax,0) as iTax,isnull(d.iPerTaxRate,0) as iPerTaxRate ,
 isnull(d.iNatUnitPrice,0) as iNatUnitPrice     from OM_MODetails d with(nolock) 
 join OM_MOMain m with(nolock) on d.moid=m.moid  where m.cCode ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring);

                                if (dtx.Rows.Count > 0)
                                {
                                    iPOsID = Convert.ToString(dtx.Rows[0]["ID"]);
                                    iTaxRate = Convert.ToDecimal(dtx.Rows[0]["iPerTaxRate"]);
                                    //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));

                                    iUnitPrice = Public.GetNum(dtx.Rows[0]["iUnitPrice"]);

                                    iTaxUnitPrice = Public.GetNum(dtx.Rows[0]["iTaxPrice"]); //Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价
                                    ////yuyang chag====================================
                                    iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                    iMoney = Public.ChinaRound(iSum / (1M + iTaxRate / 100M), 2);
                                    iTax = Public.ChinaRound(iSum - iMoney, 2);
                                    //===============================================
                                    //iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                                    //iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                    //iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                                    iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                                    iNatSum = iSum; //Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                                    iNatMoney = iMoney;// Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                                    iNatTax = iTax;// Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额



                                }
                                else
                                {
                                    return "API错误此单没有对应的委外订单表体id";
                                }
                            }
                        }
                        iInvExchRate = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(c.iChangRate,0)  from Inventory a with(nolock) join ComputationUnit c with(nolock) on a.cSTComUnitCode =c.cComunitCode where a.cinvcode='" + cinvcode + "'", connstring));
                        //if (iInvExchRate == 0)
                        //{
                        //    iInvExchRate = 1;
                        //}
                        /****************************** 以下是必输字段 ****************************/
                        //domBody[j]["autoid"] = ""; //主关键字段，int类型
                        domBody[j]["id"] = "0"; //与收发记录主表关联项，int类型
                        domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                        domBody[j]["cinvm_unit"] = Convert.ToString(SqlAccess.ExecuteScalar("select cComUnitCode  from inventory with(nolock) where cinvcode='" + cinvcode + "'", connstring)); //主计量单位，string类型
                        domBody[j]["iquantity"] = iQuantity; //数量，double类型
                        domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
                        domBody[j]["iMatSettleState"] = "0"; //iMatSettleState，int类型
                        if (cwhcode == "06")
                        {
                            domBody[j]["cposition"] = "06";
                        }
                        /***************************** 以下是非必输字段 ****************************/
                        //domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                        //domBody[j]["cinvname"] = ""; //存货名称，string类型
                        //domBody[j]["cinvstd"] = ""; //规格型号，string类型
                        //domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                        //            domBody[j]["bservice"] = "0"; //是否费用，string类型
                        //domBody[j]["cinvccode"] = ""; //所属分类码，string类型
                        domBody[j]["iinvexchrate"] = iInvExchRate; //换算率，double类型
                        //            domBody[j]["binvbatch"] = "0"; //批次管理，string类型

                        if (iInvExchRate != 0)
                        {
                            domBody[j]["inum"] = iQuantity / iInvExchRate; //件数，double类型
                        }
                        else
                        {
                            domBody[j]["inum"] = null; //件数，double类型
                        }

                        bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                        if (bInvBatch == 1)
                        {

                            domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], ""); //批号，string类型
                            domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[j]["LOTTABLE02"]); //属性6，string类型 炉号
                            domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[j]["LOTTABLE03"]);//属性7，string类型  母卷号
                        }
                        else
                        {
                            domBody[j]["cbatch"] = "";
                            domBody[j]["cbatchproperty7"] =""; //属性6，string类型 炉号
                            domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号
                        }
                        //domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                        // domBody[j]["cbatchproperty1"] = Convert.ToString(dts.Rows[j]["LOTTABLE01"]); //属性1，double类型
                        //  domBody[j]["cbatchproperty2"] = Convert.ToString(dts.Rows[j]["LOTTABLE02"]); //属性2，double类型
                        //domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                    
                        //domBody[j]["ipunitcost"] = ""; //计划单价/售价，double类型
                        //domBody[j]["ipprice"] = ""; //计划金额/售价金额，double类型
                        //domBody[j]["dvdate"] ="2013-04-28"; //失效日期，DateTime类型
                        //domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                      
                        //            domBody[j]["iflag"] = "0"; //标志，string类型
                        //domBody[j]["dsdate"] = ""; //结算日期，DateTime类型
                        //domBody[j]["itax"] = ""; //税额，double类型
                        domBody[j]["isnum"] = "0"; //累计结算件数，double类型
                        domBody[j]["imoney"] = "0"; //累计结算金额，double类型
                        //domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                        //domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                        //domBody[j]["ifnum"] = ""; //实际件数，double类型
                        //domBody[j]["ifquantity"] = ""; //实际数量，double类型
                    
                        //              domBody[j]["binvtype"] = "0"; //折扣类型，string类型
                        domBody[j]["cdefine22"] = ClsSystem.gnvl(dt.Rows[0]["RECEIPTKEY"], ""); //表体自定义项1，string类型
                        //domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                        domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], ""); //表体自定义项3，string类型
                        //domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                        domBody[j]["cdefine26"] = ClsSystem.gnvl(dts.Rows[j]["SUSR2"], ""); //表体自定义项5，double类型
                        //domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                        //            domBody[j]["isquantity"] = "0"; //累计结算数量，double类型
                        if (cWh != "W")
                        {
                            domBody[j]["iposid"] = iPOsID; //订单子表ID，int类型
                        }
                    

                        //domBody[j]["citemcode"] = ""; //项目编码，string类型
                        //domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                        //domBody[j]["cbatchproperty3"] = ""; //属性3，double类型
                        //domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                        //domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                        //domBody[j]["cbatchproperty4"] = ""; //属性4，double类型
                        //domBody[j]["cbatchproperty5"] = ""; //属性5，double类型
                        //domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                        //domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                       
                        //domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                        //domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                        //domBody[j]["cbatchproperty8"] = ""; //属性8，string类型
                        //domBody[j]["cbatchproperty9"] = ""; //属性9，string类型
                        //domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                        //domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                        //domBody[j]["cbatchproperty10"] = ""; //属性10，DateTime类型
                        domBody[j]["cdefine28"] = ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], "") + ClsSystem.gnvl(dts.Rows[j]["SERIALKEY"], ""); //表体自定义项7，string类型
                        //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                        //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                        //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                        //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                        //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                        //domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                        //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                        //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                        //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                        //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                        //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                        //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                        //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                        //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                        //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                        //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                        //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                        //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                        //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                        //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                        //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                        //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                        //domBody[j]["cbarcode"] = ""; //条形码，string类型
                        domBody[j]["iquantity"] = iQuantity;
                        domBody[j]["inquantity"] = iQuantity; //应收数量，double类型
                        //domBody[j]["innum"] = ""; //应收件数，double类型
                        //domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                        //domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                        if (cWh == "W")
                        {
                            domBody[j]["iomodid"] = iPOsID; //委外订单子表ID，int类型
                          
                        }
                        //domBody[j]["isodid"] = ""; //销售订单子表ID，string类型
                        //domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                        //domBody[j]["cvenname"] = ""; //供应商，string类型
                        //domBody[j]["imassdate"] ="2013-04-28"; //保质期，int类型
                        //domBody[j]["dmadedate"] ="2013-04-28"; //生产日期，DateTime类型
                        domBody[j]["cassunit"] = Convert.ToString(SqlAccess.ExecuteScalar("select cSTComUnitCode  from inventory with(nolock) where cinvcode='" + cinvcode + "'", connstring)); //库存单位码，string类型
                        domBody[j]["iarrsid"] = ""; //采购到货单子表标识，string类型
                        //domBody[j]["corufts"] = ""; //时间戳，string类型
                        //       domBody[j]["cposname"] = ""; //货位，string类型
                        //domBody[j]["cgspstate"] = ""; //检验状态，double类型
                        //domBody[j]["scrapufts"] = ""; //不合格品时间戳，string类型
                        //domBody[j]["icheckidbaks"] = ""; //检验单子表id，string类型
                        //domBody[j]["irejectids"] = ""; //不良品处理单id，string类型
                        //domBody[j]["dcheckdate"] = ""; //检验日期，DateTime类型
                        //domBody[j]["dmsdate"] = ""; //核销日期，DateTime类型
                        //domBody[j]["cmassunit"] =""; //保质期单位，int类型
                        //domBody[j]["ccheckcode"] = ""; //检验单号，string类型
                        //domBody[j]["crejectcode"] = ""; //不良品处理单号，string类型
                        //domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                        //domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                        //domBody[j]["cvmivenname"] = ""; //代管商，string类型
                        //             domBody[j]["bvmiused"] = "0"; //代管消耗标识，int类型
                        //domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                        //domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                        //domBody[j]["cbarvcode"] = ""; //到货单号，string类型
                        //domBody[j]["dbarvdate"] = ""; //到货日期，DateTime类型
                        //domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                        //             domBody[j]["iordertype"] = "0"; //销售订单类别，int类型
                        //domBody[j]["iorderdid"] = ""; //iorderdid，int类型
                        //domBody[j]["iordercode"] = ""; //销售订单号，string类型
                        //domBody[j]["iorderseq"] = ""; //销售订单行号，string类型
                        //              domBody[j]["iexpiratdatecalcu"] = "0"; //有效期推算方式，int类型
                        //domBody[j]["cexpirationdate"] = "2013-04-28"; //有效期至，string类型
                        //domBody[j]["dexpirationdate"] = "2013-04-28"; //有效期计算项，string类型
                        //domBody[j]["cciqbookcode"] = ""; //手册号，string类型
                        //domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                        //domBody[j]["iimosid"] = ""; //iimosid，string类型
                        //domBody[j]["iimbsid"] = ""; //iimbsid，string类型
                        //domBody[j]["ccheckpersonname"] = ""; //检验员，string类型
                        //domBody[j]["ccheckpersoncode"] = ""; //检验员编码，string类型
                        domBody[j]["cpoid"] = cCode; //订单号，string类型
                        //domBody[j]["strcontractid"] = ""; //合同号，string类型
                        //domBody[j]["strcode"] = ""; //合同标的编码，string类型
                        //domBody[j]["cveninvcode"] = ""; //供应商存货编码，string类型
                        //domBody[j]["cveninvname"] = ""; //供应商存货名称，string类型
                        domBody[j]["isotype"] = "0"; //需求跟踪方式，int类型
                        //domBody[j]["isumbillquantity"] = ""; //累计开票数量，double类型
                        //domBody[j]["cbaccounter"] = ""; //记账人，string类型
                        //              domBody[j]["bcosting"] = "0"; //是否核算，string类型
                        //domBody[j]["impcost"] = ""; //最高进价，string类型
                     
                        domBody[j]["btaxcost"] = "1"; //单价标准，double类型
                        //domBody[j]["imaterialfee"] = ""; //材料费，double类型
                        if (cWh == "W")
                        {
                            domBody[j]["iprocesscost"] = iUnitPrice; //加工费单价，double类型
                            domBody[j]["iprocessfee"] = iMoney; //加工费，double类型
                            //domBody[j]["iaprice"] = null; //暂估金额，double类型
                            //domBody[j]["facost"] = null; //暂估单价，double类型
                            //domBody[j]["iprice"] = null; //本币金额，double类型
                            //domBody[j]["iunitcost"] = null; //本币单价，double类型
                            //domBody[j]["ioritaxcost"] = null; //原币含税单价，double类型
                            //domBody[j]["ioricost"] = null; //原币单价，double类型
                            //domBody[j]["iorimoney"] = null; //原币金额，double类型
                            //domBody[j]["ioritaxprice"] = null; //原币税额，double类型
                            //domBody[j]["iorisum"] = null; //原币价税合计，double类型
                            domBody[j]["itaxrate"] = iTaxRate; //税率，double类型
                            //domBody[j]["itaxprice"] = null; //本币税额，double类型
                            //domBody[j]["isum"] = null; //本币价税合计，double类型
                        }
                        else
                        {
                            domBody[j]["iprocesscost"] = ""; //加工费单价，double类型
                            domBody[j]["iprocessfee"] = ""; //加工费，double类型
                            domBody[j]["iaprice"] = iNatMoney; //暂估金额，double类型
                            domBody[j]["facost"] = iUnitPrice; //暂估单价，double类型
                            domBody[j]["iprice"] = iNatMoney; //本币金额，double类型
                            domBody[j]["iunitcost"] = iUnitPrice; //本币单价，double类型
                            domBody[j]["ioritaxcost"] = iTaxUnitPrice; //原币含税单价，double类型
                            domBody[j]["ioricost"] = iUnitPrice; //原币单价，double类型
                            domBody[j]["iorimoney"] = iMoney; //原币金额，double类型
                            domBody[j]["ioritaxprice"] = iTax; //原币税额，double类型
                            domBody[j]["iorisum"] = iSum; //原币价税合计，double类型
                            domBody[j]["itaxrate"] = iTaxRate; //税率，double类型
                            domBody[j]["itaxprice"] = iNatTax; //本币税额，double类型
                            domBody[j]["isum"] = iNatSum; //本币价税合计，double类型
                        }
                        //domBody[j]["ismaterialfee"] = ""; //累计结算材料费，double类型
                        //domBody[j]["isprocessfee"] = ""; //累计结算加工费，double类型
                        //domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                        //domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                        //domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                        //domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                        //domBody[j]["creplaceitem"] = ""; //替换件，string类型
                        //         domBody[j]["cposition"] =ClsSystem.gnvl(dts.Rows[j]["TOLOC"],""); //货位编码，string类型
                        //domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
                        //domBody[j]["cname"] = ""; //项目名称，string类型
                        //domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                        //domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                        //       domBody[j]["iinvsncount"] = "3"; //存库序列号，int类型
                    }
                }
            //     Public.WriteLog("c:\\wwww.txt", "5 DOM结束");

            //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
            broker.AssignNormalValue("domPosition", null);

            //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

            //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
            broker.AssignNormalValue("cnnFrom", null);

            //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
            broker.AssignNormalValue("VouchId", "");

            //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
            MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("domMsg", domMsg);

            //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
            broker.AssignNormalValue("bCheck", false);

            //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
            broker.AssignNormalValue("bBeforCheckStock", false);

            //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
            if (bredvouch == 0)
            {
                broker.AssignNormalValue("bIsRedVouch", false);
            }
            else
            {
                broker.AssignNormalValue("bIsRedVouch", true);
            }

            //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
            broker.AssignNormalValue("sAddedState", "");

            //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
            broker.AssignNormalValue("bReMote", false);

            //    Public.WriteLog("c:\\wwww.txt", "6调用API");


            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;

                        //strxml = @"<Response>";
                        //strxml = strxml + "<success>false</success> ";
                        //strxml = strxml + "<desc>" + sysEx.Message + "</desc>";
                        //strxml = strxml + "</Response>";
                        ////conn.RollbackTrans();
                        ////    myTrans.Rollback();
                        //return strxml;
                        throw new Exception("系统异常：" + sysEx.Message);
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        //strxml = @"<Response>";
                        //strxml = strxml + "<success>false</success> ";
                        //strxml = strxml + "<desc>" + bizEx.Message + "</desc>";
                        //strxml = strxml + "</Response>";
                        ////conn.RollbackTrans();
                        ////  myTrans.Rollback();
                        //return strxml;
                        throw new Exception("API异常：" + bizEx.Message);
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        //strxml = @"<Response>";
                        //strxml = strxml + "<success>false</success> ";
                        //strxml = strxml + "<desc>" + exReason + "</desc>";
                        //strxml = strxml + "</Response>";
                        ////conn.RollbackTrans();
                        ////   myTrans.Rollback();
                        //return strxml;
                        throw new Exception("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
                //ado.RollbackTrans();
                return "API错误：" + apiEx.ToString().Substring(0, 100);
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
            System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


            //获取out/inout参数值

            //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            System.String errMsgRet = broker.GetResult("errMsg") as System.String;

            if (!result)
            {
                broker.Release();
                //ado.RollbackTrans();
                 return "U8错误API:"+errMsgRet;

            }

            //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            VouchNO = broker.GetResult("VouchId") as System.String;
          


            //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
            MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));


            //结束本次调用，释放API资源
            broker.Release();

              // ado.CommitTrans();

            //   VouchNO = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select cCode  from RdRecord01 where id=" + VouchIdRet, connstring), "");
           
            #endregion                   


            }
            catch (Exception  ex )
            {
                
               // ado.RollbackTrans();
                return "API错误：" + ex.ToString().Substring(0, 50);
            }
            return VouchNO;     

        }


        // 红字采购入库单
        public static string InRD01_HZ(U8Login.clsLogin u8Login, string connstring, DataTable dt, DataTable dBody, DataTable dpicks, DataTable dFb, int bredvouch, string crdcode, string cWh, string cType, string cvoucode)
        {
            System.String VouchNO = "";
         //   ADODB.Connection ado = new ADODB.Connection();
            //    conn.BeginTrans();
            string BWB = "人民币";

            try
            {


                #region 采购入库单

                string iPOsID = "";
                string cCode = "";

                string cinvcode = "";
                string iarrsid = "";
                string cvencode = "";
                string darvdate = "";
                string ID = "";

                string cdepcode = "";
                string cptcode = "";
                string strxml = "";
                decimal iTaxUnitPrice = 0;
                decimal iSum = 0;
                decimal iUnitPrice = 0;
                decimal iMoney = 0;
                decimal iTax = 0;

                decimal iNatUnitPrice = 0;
                decimal iNatSum = 0;
                decimal iNatMoney = 0;
                decimal iNatTax = 0;
                decimal iInvExchRate = 0;
                decimal iTaxRate = 0;
                int bInvBatch = 0;




                //第一步：构造u8login对象并登陆(引用U8API类库中的Interop.U8Login.dll)
                //如果当前环境中有login对象则可以省去第一步


                //     string ado_connection = "PROVIDER=SQLOLEDB;" + connstring;
                //  WriteLog.writeLog("ado");

                //U8Login.clsLogin u8Login = new U8Login.clsLogin();

                //String sSubId = "AS";
                //String sAccID = "999";
                //String sYear = "2013";
                //String sUserID = "demo";
                //String sPassword = "";
                //String sDate = "2013-01-20";
                //String sServer = "localhost";
                //String sSerial = "";
                //if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial))
                //{
                //    Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                //    Marshal.FinalReleaseComObject(u8Login);
                //    return null;
                //}

                //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                U8EnvContext envContext = new U8EnvContext();
                envContext.U8Login = u8Login;
                //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
                //ado.BeginTrans();

                //销售所有接口均支持内部独立事务和外部事务，默认内部事务
                //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
                //envContext.BizDbConnection = conn;
                //envContext.IsIndependenceTransaction = false;


                //第三步：设置API地址标识(Url)
                //当前API错误：添加新单据的地址标识为：U8API/PuStoreIn/Add
                U8ApiAddress myApiAddress = new U8ApiAddress("U8API/PuStoreIn/Add");

                //第四步：构造APIBroker
                U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

                //第五步：API参数赋值

                //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：01
                broker.AssignNormalValue("sVouchType", "01");

                //给BO表头参数DomHead赋值，此BO参数的业务类型为采购入库单，属表头参数。BO参数均按引用传递
                //提示：给BO表头参数DomHead赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject DomHead = broker.GetBoParam("DomHead");

                DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
                //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义


                cvencode = ClsSystem.gnvl(dt.Rows[0]["SUSR2"], "");

                /****************************** 以下是必输字段 ****************************/
                //     string    crdCode = Public.GetParentCode("24", "", connstring);
                DomHead[0]["id"] = "0"; //主关键字段，int类型
                DomHead[0]["bomfirst"] = "0"; //委外期初标志，string类型
             
                    DomHead[0]["ccode"] = Public.GetParentCode("24", "", connstring); //入库单号，string类型

                    string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dt.Rows[0]["ORDERDATE"], ""), 1);
                //  string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dt.Tables[0].Rows[0]["RECEIPTDATE"], ""), 0);
                rq1 = rq1.Insert(4, "/");
                rq1 = rq1.Insert(7, "/");

                DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToShortDateString(); //入库日期，DateTime类型
                DomHead[0]["iverifystate"] = "0"; //iverifystate，int类型
                DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，int类型
                //     DomHead[0]["cvenabbname"] = "上海用友"; //供货单位，string类型
                if (cWh == "W")
                {
                    DomHead[0]["cbustype"] = "委外加工"; //业务类型，int类型
                }
                else
                {
                    DomHead[0]["cbustype"] = "普通采购"; //业务类型，int类型

                }
                DomHead[0]["cdefine2"] = "";

                DomHead[0]["cmaker"] = ClsSystem.gnvl(dt.Rows[0]["EDITWHO"], ""); //制单人，string类型
                DomHead[0]["iexchrate"] = "1"; //汇率，double类型
                DomHead[0]["cexch_name"] = "人民币"; //币种，string类型
            //    DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString();  //时间戳，string类型
                DomHead[0]["bpufirst"] = "0"; //采购期初标志，string类型
                DomHead[0]["cvencode"] = cvencode; //供货单位编码，string类型
                DomHead[0]["cvouchtype"] = "01"; //单据类型，string类型
                //两边不一致  需要转换
                string cwhcode = ClsSystem.gnvl(dt.Rows[0]["SUSR1"], "");

                if (cwhcode == "")
                {
                    cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and  wmsbm='" + Convert.ToString(dFb.Rows[0]["LOC"]) + "'", connstring), "");
                }
                //if (cType == "102")
                //{
                //    if (ClsSystem.gnvl(dtst.Rows[0]["cflag"], "") == "1")
                //    {
                //        cwhcode = "03";
                //    }
                //}

                DomHead[0]["cwhcode"] = cwhcode;// ClsSystem.gnvl(dt.Rows[0]["cwhcode"], ""); //仓库编码，string类型

                DomHead[0]["brdflag"] = "1"; //收发标志，int类型
                if (cWh == "W")
                {
                    DomHead[0]["csource"] = "委外订单"; //单据来源，int类型
                }
                else
                {
                    DomHead[0]["csource"] = "库存"; //单据来源，int类型}
                }


                /***************************** 以下是非必输字段 ****************************/

                //DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
                //DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
                DomHead[0]["dnmaketime"] = DateTime.Now; //制单时间，DateTime类型
                //DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
                DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
                //        DomHead[0]["cwhname"] = "测试仓"; //仓库，string类型



                DomHead[0]["cordercode"] = ""; //订单号，string类型
                DomHead[0]["carvcode"] = ""; //到货单号，string类型
                DomHead[0]["ireturncount"] = "0"; //ireturncount，string类型
                //DomHead[0]["cbuscode"] = ""; //业务号，string类型
                //DomHead[0]["cdepname"] = ""; //部门，string类型
                //DomHead[0]["cpersonname"] = ""; //业务员，string类型
                DomHead[0]["darvdate"] = ""; //到货日期，DateTime类型
                //DomHead[0]["cptname"] = ""; //采购类型，string类型
                //DomHead[0]["crdname"] = ""; //入库类别，string类型
                DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
                //DomHead[0]["cvenpuomprotocol"] = ""; //收付款协议编码，string类型
                //DomHead[0]["cvenpuomprotocolname"] = ""; //收付款协议名称，string类型
                //DomHead[0]["dcreditstart"] = ""; //立账日，DateTime类型
                //DomHead[0]["icreditperiod"] = ""; //账期，int类型
                //DomHead[0]["dgatheringdate"] = ""; //到期日，DateTime类型
                DomHead[0]["bcredit"] = "0"; //是否为立账单据，int类型
                //DomHead[0]["cvenfullname"] = ""; //供应商全称，string类型
                DomHead[0]["cmemo"] = ClsSystem.gnvl(dt.Rows[0]["ORDERKEY"], ""); //备注，string类型
                DomHead[0]["chandler"] = ""; //审核人，string类型
                //DomHead[0]["caccounter"] = ""; //记账人，string类型
                //DomHead[0]["ipresent"] = ""; //现存量，string类型
                DomHead[0]["itaxrate"] = "17"; //税率，double类型
                //DomHead[0]["isalebillid"] = ""; //发票号，string类型
                DomHead[0]["ipurorderid"] = ""; //采购订单ID，string类型
                DomHead[0]["ipurarriveid"] = ""; //采购到货单ID，string类型
                //DomHead[0]["iarriveid"] = ""; //到货单ID，string类型
                //DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
                //DomHead[0]["iavaquantity"] = ""; //可用量，string类型
                //DomHead[0]["iavanum"] = ""; //可用件数，string类型
                //DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
                //DomHead[0]["gspcheck"] = ""; //gsp复核标志，string类型
                //DomHead[0]["cchkperson"] = ""; //检验员，string类型
                //DomHead[0]["cchkcode"] = ""; //检验单号，string类型
                DomHead[0]["vt_id"] = "27"; //模版号，int类型
                DomHead[0]["cdepcode"] = "0700"; //部门编码，string类型
                //DomHead[0]["cbillcode"] = ""; //发票id，string类型
                if (cType == "1041")
                {
                    DomHead[0]["cptcode"] = "01"; //采购类型编码，string类型
                }
                else
                {
                    DomHead[0]["cptcode"] = "03"; //采购类型编码，string类型
                }
                //DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型

                DomHead[0]["crdcode"] = crdcode; //入库类别编码，string类型-----------------------------------------------------

                //DomHead[0]["isafesum"] = ""; //安全库存量，string类型
                //DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
                //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
                //DomHead[0]["itopsum"] = ""; //最高库存量，string类型
                //DomHead[0]["iproorderid"] = ""; //生产订单Id，string类型
                //         DomHead[0]["bisstqc"] = "0"; //库存期初标记，string类型
                //DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
                //DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
                //DomHead[0]["cdefine3"] = Convert.ToString(dt.Rows[i]["cCode"]); //表头自定义项3，string类型
                //DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
                //           DomHead[0]["idiscounttaxtype"] = "0"; //扣税类别，int类型
                //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
                //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
                //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
                // DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
                //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
                //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
                //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
                //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
                //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
                //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
                //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
                //  }
                //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
                //提示：给BO表体参数domBody赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject domBody = broker.GetBoParam("domBody");

                ////过滤重复数据
                //DataTable dts = Public.CheckDataTable(dtst, "rdrecords01", ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], ""), "0");

                domBody.RowCount =dBody.Rows.Count* dFb.Rows.Count; //设置BO对象行数

                //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
                //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
                DataTable dtx = null;
           
                string cPdID="";
                int j = 0;


                for (int k = 0; k < dBody.Rows.Count; k++)
                {
                    cinvcode = ClsSystem.gnvl(dBody.Rows[k]["SKU"], "");
                    cPdID = ClsSystem.gnvl(dBody.Rows[k]["ShipmentOrderDetail_ID"], "");

                    DataView rowfilter2 = new DataView(dpicks);
                    rowfilter2.RowFilter = "ShipmentOrderDetail_ID =" + cPdID;
                    rowfilter2.RowStateFilter = DataViewRowState.CurrentRows;
                    DataTable dpick = rowfilter2.ToTable();

                    if (dpick.Rows.Count > 0)
                    {

                        string cpicksid = ClsSystem.gnvl(dpick.Rows[0]["PickDetails_ID"], "");

                        DataView rowfilter1 = new DataView(dFb);
                        rowfilter1.RowFilter = "PickDetails_ID =" + cpicksid;
                        rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                        DataTable dts = rowfilter1.ToTable();
                        if (dts.Rows.Count > 0)
                        {
                            for (int i = 0; i < dts.Rows.Count; i++)
                            {

                                //判断是不是寄存品

                                decimal iQuantity = Public.GetNum(dts.Rows[i]["QTY"]);//数量
                                if (bredvouch == 1)
                                {
                                    iQuantity = -iQuantity;
                                }


                                //   dtx = SqlAccess.ExecuteSqlDataTable("select top 1 d.ID,isnull(d.iTaxPrice,0) as iTaxPrice ,isnull(d.iUnitPrice,0) as iUnitPrice,isnull(iTax,0) as iTax,isnull(d.iPerTaxRate,0) as iPerTaxRate ,isnull(d.iNatUnitPrice,0) as iNatUnitPrice     from PO_Podetails d with(nolock) join PO_Pomain m with(nolock) on d.POID=m.POID  where m.cPOID ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring);

                                //if (dtx.Rows.Count > 0)
                                //{
                                //    iPOsID = Convert.ToString(dtx.Rows[0]["ID"]);
                                //    iTaxRate = Convert.ToDecimal(dtx.Rows[0]["iPerTaxRate"]);
                                //    //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));

                                //    iUnitPrice = Public.GetNum(dtx.Rows[0]["iUnitPrice"]);

                                //    iTaxUnitPrice = Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价

                                //    iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                                //    iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                //    iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                                //    iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                                //    iNatSum = Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                                //    iNatMoney = Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                                //    iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额



                                //}

                                iTaxRate = 17;

                                iUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价

                                iTaxUnitPrice = Public.ChinaRound(iUnitPrice * (1M + iTaxRate / 100M), 4);//原币无税单价

                                iMoney = Public.GetNum(Public.ChinaRound(iUnitPrice * iQuantity, 2));//原币含税金额
                                iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币无税金额
                                iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

                                iNatUnitPrice = iTaxUnitPrice;// Public.GetNum(dtx.Rows[0]["iNatUnitPrice"]);//本币无税单价

                                iNatSum = Public.ChinaRound(iQuantity * iTaxUnitPrice, 2);//本币价税合计
                                iNatMoney = Public.ChinaRound(iUnitPrice * iQuantity, 2);//本币无税金额
                                iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

                                iInvExchRate = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(c.iChangRate,0)  from Inventory a with(nolock) join ComputationUnit c with(nolock) on a.cSTComUnitCode =c.cComunitCode where a.cinvcode='" + cinvcode + "'", connstring));
                                //if (iInvExchRate == 0)
                                //{
                                //    iInvExchRate = 1;
                                //}
                                /****************************** 以下是必输字段 ****************************/
                                //domBody[j]["autoid"] = ""; //主关键字段，int类型
                                domBody[j]["id"] = "0"; //与收发记录主表关联项，int类型
                                domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                                //   domBody[j]["cinvm_unit"] = Convert.ToString(SqlAccess.ExecuteScalar("select cComUnitCode  from inventory with(nolock) where cinvcode='" + cinvcode + "'", connstring)); //主计量单位，string类型
                                domBody[j]["iquantity"] = iQuantity; //数量，double类型
                                domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
                                domBody[j]["iMatSettleState"] = "0"; //iMatSettleState，int类型
                                if (cwhcode == "06")
                                {
                                    domBody[j]["cposition"] = "06";
                                }
                                else
                                {
                                    domBody[j]["cposition"] = "";
                                }
                                /***************************** 以下是非必输字段 ****************************/
                                //domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                                //domBody[j]["cinvname"] = ""; //存货名称，string类型
                                //domBody[j]["cinvstd"] = ""; //规格型号，string类型
                                //domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                                //            domBody[j]["bservice"] = "0"; //是否费用，string类型
                                //domBody[j]["cinvccode"] = ""; //所属分类码，string类型
                                domBody[j]["iinvexchrate"] = iInvExchRate; //换算率，double类型


                                if (iInvExchRate != 0)
                                {
                                    domBody[j]["inum"] = iQuantity / iInvExchRate; //件数，double类型
                                }
                                else
                                {
                                    domBody[j]["inum"] = ""; //件数，double类型
                                }

                                bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                                if (bInvBatch == 1)
                                {
                                    domBody[j]["binvbatch"] = "1"; //批次管理，string类型
                                    domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[j]["ID"], ""); //批号，string类型
                                    domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[j]["LOTTABLE02"]); //属性6，string类型 炉号
                                    domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[j]["LOTTABLE03"]);//属性7，string类型  母卷号
                                }
                                else
                                {
                                    domBody[j]["binvbatch"] = "0"; //批次管理，string类型
                                    domBody[j]["cbatch"] = "";
                                    domBody[j]["cbatchproperty7"] =""; //属性6，string类型 炉号
                                    domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号
                                }
                                //domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                                // domBody[j]["cbatchproperty1"] = Convert.ToString(dts.Rows[j]["LOTTABLE01"]); //属性1，double类型
                                //  domBody[j]["cbatchproperty2"] = Convert.ToString(dts.Rows[j]["LOTTABLE02"]); //属性2，double类型
                                //domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                                domBody[j]["iaprice"] = iNatMoney; //暂估金额，double类型
                                //domBody[j]["ipunitcost"] = ""; //计划单价/售价，double类型
                                //domBody[j]["ipprice"] = ""; //计划金额/售价金额，double类型
                                domBody[j]["dvdate"] = ""; //失效日期，DateTime类型
                                //domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                                domBody[j]["iunitcost"] = iUnitPrice; //本币单价，double类型
                                //            domBody[j]["iflag"] = "0"; //标志，string类型
                                //domBody[j]["dsdate"] = ""; //结算日期，DateTime类型
                                //domBody[j]["itax"] = ""; //税额，double类型
                                domBody[j]["isnum"] = "0"; //累计结算件数，double类型
                                domBody[j]["imoney"] = "0"; //累计结算金额，double类型
                                //domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                                //domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                                //domBody[j]["ifnum"] = ""; //实际件数，double类型
                                //domBody[j]["ifquantity"] = ""; //实际数量，double类型
                                domBody[j]["iprice"] = iNatMoney; //本币金额，double类型
                                //              domBody[j]["binvtype"] = "0"; //折扣类型，string类型
                                domBody[j]["cdefine22"] = ClsSystem.gnvl(dt.Rows[0]["ORDERKEY"], ""); //表体自定义项1，string类型
                                domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[i]["ID"], "");  //表体自定义项3，string类型
                                domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                                domBody[j]["cdefine26"] = ClsSystem.gnvl(dBody.Rows[k]["SUSR2"], ""); //表体自定义项5，double类型
                                //domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                                //            domBody[j]["isquantity"] = "0"; //累计结算数量，double类型

                                domBody[j]["iposid"] = iPOsID; //订单子表ID，int类型

                                domBody[j]["facost"] = "";// iUnitPrice; //暂估单价，double类型

                                //domBody[j]["citemcode"] = ""; //项目编码，string类型
                                //domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                                //domBody[j]["cbatchproperty3"] = ""; //属性3，double类型
                                //domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                                //domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                                //domBody[j]["cbatchproperty4"] = ""; //属性4，double类型
                                //domBody[j]["cbatchproperty5"] = ""; //属性5，double类型
                                //domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                                //domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                               
                                //domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                                //domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                                //domBody[j]["cbatchproperty8"] = ""; //属性8，string类型
                                //domBody[j]["cbatchproperty9"] = ""; //属性9，string类型
                                //domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                                //domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                                //domBody[j]["cbatchproperty10"] = ""; //属性10，DateTime类型
                                domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
                                //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                                //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                                //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                                //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                                //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                                //domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                                //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                                //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                                //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                                //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                                //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                                //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                                //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                                //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                                //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                                //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                                //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                                //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                                //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                                //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                                //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                                //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                                //domBody[j]["cbarcode"] = ""; //条形码，string类型
                                domBody[j]["iquantity"] = iQuantity;
                                domBody[j]["inquantity"] = iQuantity; //应收数量，double类型
                                //domBody[j]["innum"] = ""; //应收件数，double类型
                                //domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                                //domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                                //domBody[j]["iomodid"] = ""; //委外订单子表ID，int类型
                                //domBody[j]["isodid"] = ""; //销售订单子表ID，string类型
                                //domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                                //domBody[j]["cvenname"] = ""; //供应商，string类型
                                //domBody[j]["imassdate"] ="2013-04-28"; //保质期，int类型
                                //domBody[j]["dmadedate"] ="2013-04-28"; //生产日期，DateTime类型
                                domBody[j]["cassunit"] = Convert.ToString(SqlAccess.ExecuteScalar("select cSTComUnitCode  from inventory with(nolock) where cinvcode='" + cinvcode + "'", connstring)); //库存单位码，string类型
                                domBody[j]["iarrsid"] = ""; //采购到货单子表标识，string类型
                                //domBody[j]["corufts"] = ""; //时间戳，string类型
                                //       domBody[j]["cposname"] = ""; //货位，string类型
                                //domBody[j]["cgspstate"] = ""; //检验状态，double类型
                                //domBody[j]["scrapufts"] = ""; //不合格品时间戳，string类型
                                //domBody[j]["icheckidbaks"] = ""; //检验单子表id，string类型
                                //domBody[j]["irejectids"] = ""; //不良品处理单id，string类型
                                //domBody[j]["dcheckdate"] = ""; //检验日期，DateTime类型
                                //domBody[j]["dmsdate"] = ""; //核销日期，DateTime类型
                                //domBody[j]["cmassunit"] =""; //保质期单位，int类型
                                //domBody[j]["ccheckcode"] = ""; //检验单号，string类型
                                //domBody[j]["crejectcode"] = ""; //不良品处理单号，string类型
                                //domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                                //domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                                //domBody[j]["cvmivenname"] = ""; //代管商，string类型
                                //             domBody[j]["bvmiused"] = "0"; //代管消耗标识，int类型
                                //domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                                //domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                                //domBody[j]["cbarvcode"] = ""; //到货单号，string类型
                                //domBody[j]["dbarvdate"] = ""; //到货日期，DateTime类型
                                //domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                                //             domBody[j]["iordertype"] = "0"; //销售订单类别，int类型
                                //domBody[j]["iorderdid"] = ""; //iorderdid，int类型
                                //domBody[j]["iordercode"] = ""; //销售订单号，string类型
                                //domBody[j]["iorderseq"] = ""; //销售订单行号，string类型
                                domBody[j]["iexpiratdatecalcu"] = "0"; //有效期推算方式，int类型
                                //domBody[j]["cexpirationdate"] = "2013-04-28"; //有效期至，string类型
                                //domBody[j]["dexpirationdate"] = "2013-04-28"; //有效期计算项，string类型
                                //domBody[j]["cciqbookcode"] = ""; //手册号，string类型
                                //domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                                //domBody[j]["iimosid"] = ""; //iimosid，string类型
                                //domBody[j]["iimbsid"] = ""; //iimbsid，string类型
                                //domBody[j]["ccheckpersonname"] = ""; //检验员，string类型
                                //domBody[j]["ccheckpersoncode"] = ""; //检验员编码，string类型
                                domBody[j]["cpoid"] = ""; //订单号，string类型
                                //domBody[j]["strcontractid"] = ""; //合同号，string类型
                                //domBody[j]["strcode"] = ""; //合同标的编码，string类型
                                //domBody[j]["cveninvcode"] = ""; //供应商存货编码，string类型
                                //domBody[j]["cveninvname"] = ""; //供应商存货名称，string类型
                                domBody[j]["isotype"] = "0"; //需求跟踪方式，int类型
                                //domBody[j]["isumbillquantity"] = ""; //累计开票数量，double类型
                                //domBody[j]["cbaccounter"] = ""; //记账人，string类型
                                domBody[j]["bcosting"] = "0"; //是否核算，string类型
                                //domBody[j]["impcost"] = ""; //最高进价，string类型
                                domBody[j]["ioritaxcost"] = iTaxUnitPrice; //原币含税单价，double类型
                                domBody[j]["ioricost"] = iUnitPrice; //原币单价，double类型
                                domBody[j]["iorimoney"] = iMoney; //原币金额，double类型
                                domBody[j]["ioritaxprice"] = iTax; //原币税额，double类型
                                domBody[j]["iorisum"] = iSum; //原币价税合计，double类型
                                domBody[j]["itaxrate"] = iTaxRate; //税率，double类型
                                domBody[j]["itaxprice"] = iNatTax; //本币税额，double类型
                                domBody[j]["isum"] = iNatSum; //本币价税合计，double类型
                                domBody[j]["btaxcost"] = "1"; //单价标准，double类型
                                //domBody[j]["imaterialfee"] = ""; //材料费，double类型

                                domBody[j]["iprocesscost"] = ""; //加工费单价，double类型
                                domBody[j]["iprocessfee"] = ""; //加工费，double类型

                                //domBody[j]["ismaterialfee"] = ""; //累计结算材料费，double类型
                                //domBody[j]["isprocessfee"] = ""; //累计结算加工费，double类型
                                //domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                                //domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                                //domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                                //domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                                //domBody[j]["creplaceitem"] = ""; //替换件，string类型
                                //         domBody[j]["cposition"] =ClsSystem.gnvl(dts.Rows[j]["TOLOC"],""); //货位编码，string类型
                                //domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
                                //domBody[j]["cname"] = ""; //项目名称，string类型
                                //domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                                //domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                                //       domBody[j]["iinvsncount"] = "3"; //存库序列号，int类型
                                j++;
                            }
                        }
                    }
                }
                //     Public.WriteLog("c:\\wwww.txt", "5 DOM结束");

                //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
                broker.AssignNormalValue("domPosition", null);

                //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
                broker.AssignNormalValue("cnnFrom", null);

                //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
                broker.AssignNormalValue("VouchId", "");

                //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
                broker.AssignNormalValue("domMsg", domMsg);

                //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
                broker.AssignNormalValue("bCheck", true);

                //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                broker.AssignNormalValue("bBeforCheckStock", true);

                //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
                if (bredvouch == 0)
                {
                    broker.AssignNormalValue("bIsRedVouch", false);
                }
                else
                {
                    broker.AssignNormalValue("bIsRedVouch", true);
                }

                //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
                broker.AssignNormalValue("sAddedState", "");

                //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
                broker.AssignNormalValue("bReMote", false);

                //    Public.WriteLog("c:\\wwww.txt", "6调用API");


                //第六步：调用API
                if (!broker.Invoke())
                {
                    //API处理
                    Exception apiEx = broker.GetException();
                    if (apiEx != null)
                    {
                        if (apiEx is MomSysException)
                        {
                            MomSysException sysEx = apiEx as MomSysException;

                            //strxml = @"<Response>";
                            //strxml = strxml + "<success>false</success> ";
                            //strxml = strxml + "<desc>" + sysEx.Message + "</desc>";
                            //strxml = strxml + "</Response>";
                            ////conn.RollbackTrans();
                            ////    myTrans.Rollback();
                            //return strxml;
                            throw new Exception("系统异常：" + sysEx.Message);
                            //todo:异常处理
                        }
                        else if (apiEx is MomBizException)
                        {
                            MomBizException bizEx = apiEx as MomBizException;
                            //strxml = @"<Response>";
                            //strxml = strxml + "<success>false</success> ";
                            //strxml = strxml + "<desc>" + bizEx.Message + "</desc>";
                            //strxml = strxml + "</Response>";
                            ////conn.RollbackTrans();
                            ////  myTrans.Rollback();
                            //return strxml;
                            throw new Exception("API异常：" + bizEx.Message);
                            //todo:异常处理
                        }
                        //异常原因
                        String exReason = broker.GetExceptionString();
                        if (exReason.Length != 0)
                        {
                            //strxml = @"<Response>";
                            //strxml = strxml + "<success>false</success> ";
                            //strxml = strxml + "<desc>" + exReason + "</desc>";
                            //strxml = strxml + "</Response>";
                            ////conn.RollbackTrans();
                            ////   myTrans.Rollback();
                            //return strxml;
                            throw new Exception("异常原因：" + exReason);
                        }
                    }
                    //结束本次调用，释放API资源
                    broker.Release();
              //      ado.RollbackTrans();
                    return "API错误：" + apiEx.ToString().Substring(0, 100);
                }

                //第七步：获取返回结果

                //获取返回值
                //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
                System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


                //获取out/inout参数值

                //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                System.String errMsgRet = broker.GetResult("errMsg") as System.String;

                if (!result)
                {
                    broker.Release();
                 //   ado.RollbackTrans();
                    return "U8错误API:" + errMsgRet;

                }

                //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                VouchNO = broker.GetResult("VouchId") as System.String;



                //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));


                //结束本次调用，释放API资源
                broker.Release();

             //   ado.CommitTrans();

                //   VouchNO = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select cCode  from RdRecord01 where id=" + VouchIdRet, connstring), "");

                #endregion


            }
            catch (Exception ex)
            {

              //  ado.RollbackTrans();
                return "API错误：" + ex.ToString().Substring(0, 50);
            }
            return VouchNO;

        }


        //材料出库单
        public static string InRD11(U8Login.clsLogin u8Login, string connstring, DataTable dHead, DataTable dBody, DataTable dpicks, DataTable dFb, int bredvouch, string crdcode, string cType, string cWh)
        {
              System.String VouchNO = "";
                 ADODB.Connection ado = new ADODB.Connection();
                   
            string ID="";
            try
            {                 
           
            #region 材料出库单
            string cCode = "";

            cCode = Convert.ToString(dHead.Rows[0]["EXTERNORDERKEY"]);

            //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;

            //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
            //ado.BeginTrans();

            //第三步：设置API地址标识(Url)
            //当前API错误：添加新单据的地址标识为：U8API/MaterialOut/Add
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/MaterialOut/Add");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：11
            broker.AssignNormalValue("sVouchType","11");

            //给BO表头参数DomHead赋值，此BO参数的业务类型为材料出库单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数DomHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject DomHead = broker.GetBoParam("DomHead");
            DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
            string cdepcode = "", cvencode = "";
            string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");
            DataTable dtcx = null;
            if (cWh == "W")
            {
                if (cCode != "")
                {
                    dtcx = SqlAccess.ExecuteSqlDataTable("select cVenCode,cdepcode,ID  from MaterialAppVouch with(nolock) where ccode='" + cCode + "'", connstring);
                    if (dtcx.Rows.Count > 0)
                    {
                        //cpoid = Convert.ToString(dtcx.Rows[0]["cpocode"]);
                        cdepcode = Convert.ToString(dtcx.Rows[0]["cdepcode"]);
                        //  cptcode = Convert.ToString(dtcx.Rows[0]["cptcode"]);
                        cvencode = Convert.ToString(dtcx.Rows[0]["cVenCode"]);
                        ID = Convert.ToString(dtcx.Rows[0]["ID"]);
                        //BWB = Convert.ToString(dtcx.Rows[0]["cexch_name"]);

                    }
                    else
                    {
                        return "API错误:此单没有对应的领料申请单";
                    }
                }
            }
            else
            {
                cdepcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR2"], "");
                if (cdepcode == "")
                {
                    cdepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
                }
            }

            /****************************** 以下是必输字段 ****************************/
            DomHead[0]["id"] = "0"; //主关键字段，int类型
            DomHead[0]["ccode"] =  Public.GetParentCode("0412", "", connstring); //出库单号，string类型
            string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["EDITDATE"], ""), 1);
            //string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 0);
            rq1 = rq1.Insert(4, "/");
            rq1 = rq1.Insert(7, "/");
            DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToShortDateString(); //出库日期，DateTime类型
          //  string cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where wmsbm='" + ClsSystem.gnvl(dFb.Rows[0]["LOC"], "") + "'", connstring), "");
          

            if (cwhcode == "")
            {
                cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and wmsbm='" + Convert.ToString(dFb.Rows[0]["LOC"]) + "'", connstring), "");
            }
            DomHead[0]["cwhname"] = Convert.ToString(SqlAccess.ExecuteScalar(" select cwhname from warehouse  with(nolock) where cWhCode='" + cwhcode + "'", connstring));//仓库，string类型

           // string codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
            /***************************** 以下是非必输字段 ****************************/
            DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
            DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
            DomHead[0]["dnmaketime"] = DateTime.Now; //制单时间，DateTime类型
            DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
            DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
            DomHead[0]["hcinvdefine1"] = ""; //存货自定义项1，string类型
            DomHead[0]["hcinvdefine2"] = ""; //存货自定义项2，string类型
            DomHead[0]["hcinvdefine3"] = ""; //存货自定义项3，string类型
            DomHead[0]["hcinvdefine4"] = ""; //存货自定义项3，string类型
            DomHead[0]["hcinvdefine5"] = ""; //存货自定义项5，string类型
            DomHead[0]["hcinvdefine6"] = ""; //存货自定义项3，string类型
            DomHead[0]["hcinvdefine7"] = ""; //存货自定义项7，string类型
            DomHead[0]["hcinvdefine8"] = ""; //存货自定义项8，string类型
            DomHead[0]["hcinvdefine9"] = ""; //存货自定义项9，string类型
            DomHead[0]["hcinvdefine10"] = ""; //存货自定义项10，string类型
            DomHead[0]["hcinvdefine11"] = ""; //存货自定义项11，int类型
            DomHead[0]["hcinvdefine12"] = ""; //存货自定义项12，int类型
            DomHead[0]["hcinvdefine13"] = ""; //存货自定义项13，double类型
            DomHead[0]["hcinvdefine14"] = ""; //存货自定义项14，double类型
            DomHead[0]["hcinvdefine15"] = ""; //存货自定义项15，DateTime类型
            DomHead[0]["hcinvdefine16"] = ""; //存货自定义项16，DateTime类型
            DomHead[0]["cinvstd"] = ""; //规格型号，string类型
            DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
            DomHead[0]["iavaquantity"] = ""; //可用量，string类型
            DomHead[0]["iavanum"] = ""; //可用件数，string类型
            DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
         //   DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString() ; //时间戳，string类型
            DomHead[0]["iproorderid"] = ""; //生产订单ID，string类型
            if (cWh == "W")
            {
                if (ID != "")
                {
                    string cmpocode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select top 1 comcode from MaterialAppVouchs  with(nolock)  where id=" + ID, connstring), "");
                    if (cmpocode == "")
                    {
                        return "API错误:此单没有对应的领料申请单表体单号";
                    }
                    DomHead[0]["cmpocode"] = cmpocode; //订单号，string类型
                }
            }
            else
            {
                DomHead[0]["cmpocode"] = "";
            }
            DomHead[0]["cpspcode"] = ""; //产品编码，string类型
            DomHead[0]["cproinvaddcode"] = ""; //存货代码，string类型
            DomHead[0]["ireturncount"] = ""; //ireturncount，string类型
            DomHead[0]["iverifystate"] = ""; //iverifystate，string类型
            DomHead[0]["iswfcontrolled"] = ""; //iswfcontrolled，string类型
            DomHead[0]["imquantity"] = ""; //产量，double类型
            DomHead[0]["cprobatch"] = ""; //生产批号，string类型

            if (cWh == "W")
            {
                DomHead[0]["cbustype"] = "委外发料"; //业务类型，int类型
                DomHead[0]["csource"] = "领料申请单"; //单据来源，int类型
            }
            else
            {
                DomHead[0]["cbustype"] = "领料"; //业务类型，int类型
                DomHead[0]["csource"] = "库存"; //单据来源，int类型
            }
            DomHead[0]["cbuscode"] = cCode; //业务号，string类型
            DomHead[0]["cchkperson"] = ""; //检验员，string类型
            DomHead[0]["crdname"] = ""; //出库类别，string类型
            DomHead[0]["cdepname"] = ""; //部门，string类型
            DomHead[0]["itopsum"] = ""; //最高库存量，string类型
            DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
            DomHead[0]["cvenabbname"] = ""; //委外商，string类型
            DomHead[0]["bomfirst"] = ""; //bomfirst，string类型
            DomHead[0]["isafesum"] = ""; //安全库存量，string类型
            DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
            DomHead[0]["crdcode"] = crdcode; //出库类别编码，string类型
            DomHead[0]["cmemo"] = ClsSystem.gnvl(dHead.Rows[0]["ORDERKEY"],""); //备注，string类型
            DomHead[0]["cmaker"] = ClsSystem.gnvl(dHead.Rows[0]["EDITWHO"], ""); //制单人，string类型
            DomHead[0]["chandler"] = ""; //审核人，string类型
            DomHead[0]["caccounter"] = ""; //记账人，string类型
            DomHead[0]["ipresent"] = ""; //现存量，string类型
            DomHead[0]["cinvname"] = ""; //产品名称，string类型
            DomHead[0]["biscomplement"] = ""; //补料标志，int类型
            DomHead[0]["cpersonname"] = ""; //业务员，string类型
            DomHead[0]["bpositive"] = ""; //红蓝标识，string类型
            DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
            DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
         
            DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            DomHead[0]["brdflag"] = "0"; //收发标志，string类型
            DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            DomHead[0]["cvencode"] = cvencode; //委外商编码，string类型
            DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            DomHead[0]["cvouchtype"] = "11"; //单据类型，string类型
            DomHead[0]["cwhcode"] = cwhcode; //仓库编码，string类型
            DomHead[0]["cdepcode"] = cdepcode; //部门编码，string类型
            DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型
            DomHead[0]["vt_id"] = "65"; //模版号，int类型
            DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型

            //给BO表体参数domBody赋值，此BO参数的业务类型为材料出库单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：



            //DataView rowfilter = new DataView(dt);
            //rowfilter.RowFilter = "cvoucode = '" + cvoucode + "'";
            //rowfilter.RowStateFilter = DataViewRowState.CurrentRows;

            if (dBody.Rows.Count * dFb.Rows.Count < 1)
            {
                return "API错误：表体无数据";
            }
            BusinessObject domBody = broker.GetBoParam("domBody");
            domBody.RowCount = dBody.Rows.Count * dFb.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            string cinvcode="";
            //decimal iTaxUnitPrice = 0;
            //decimal iSum = 0;
            //decimal iUnitPrice = 0;
            //decimal iMoney = 0;
            //decimal iTax = 0;

            //decimal iNatUnitPrice = 0;
            //decimal iNatSum = 0;
            //decimal iNatMoney = 0;
            //decimal iNatTax = 0;
            decimal iInvExchRate = 0;
            string iPOsID = "";
            string cassunit = "";
            int bInvBatch = 0;
            string cPdID = "";
            int j = 0;
            decimal iTaxUnitPrice = 0;
            decimal iSum = 0;
       //     DataTable dtcx = new DataTable();
            for (int k = 0; k < dBody.Rows.Count; k++)
            {
                cinvcode = ClsSystem.gnvl(dBody.Rows[k]["SKU"], "");
                cPdID = ClsSystem.gnvl(dBody.Rows[k]["ShipmentOrderDetail_ID"], "");

                //DataView rowfilter1 = new DataView(dFb);
                //rowfilter1.RowFilter = "PickDetails_ID =" + cPdID;
                //rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                //DataTable dts = rowfilter1.ToTable();
                DataView rowfilter2 = new DataView(dpicks);
                rowfilter2.RowFilter = "ShipmentOrderDetail_ID =" + cPdID;
                rowfilter2.RowStateFilter = DataViewRowState.CurrentRows;
                DataTable dpick = rowfilter2.ToTable();

                if (dpick.Rows.Count > 0)
                {

                    string cpicksid = ClsSystem.gnvl(dpick.Rows[0]["PickDetails_ID"], "");

                    DataView rowfilter1 = new DataView(dFb);
                    rowfilter1.RowFilter = "PickDetails_ID =" + cpicksid;
                    rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                    DataTable dts = rowfilter1.ToTable();
                    string iOMoMID = "", iOMoDID = "", comcode = "", invcode = "",autoid="";
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {

                        decimal iQuantity = Public.GetNum(dts.Rows[i]["QTY"]);//数量
                        if (bredvouch == 1)
                        {
                            iQuantity = -iQuantity;
                        }
                        iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                        iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));
                        if (cWh == "W")
                        {
                            dtcx = SqlAccess.ExecuteSqlDataTable(" select iOMoMID,iOMoDID,comcode,invcode,autoid from MaterialAppVouchs where id=" + ID + " and cInvCode='" + cinvcode + "'", connstring);

                            if (dtcx.Rows.Count > 0)
                            {
                                iOMoMID = ClsSystem.gnvl(dtcx.Rows[0]["iOMoMID"], "");

                                iOMoDID = ClsSystem.gnvl(dtcx.Rows[0]["iOMoDID"], "");
                                comcode = ClsSystem.gnvl(dtcx.Rows[0]["comcode"], "");
                                invcode = ClsSystem.gnvl(dtcx.Rows[0]["invcode"], "");
                                autoid = ClsSystem.gnvl(dtcx.Rows[0]["autoid"], "");

                            }
                            else
                            {
                                return "API错误此单没有对应的采购订单表体id";
                            }
                        }
                        iInvExchRate = Convert.ToDecimal(SqlAccess.ExecuteScalar("select isnull(c.iChangRate,1)  from Inventory a with(nolock) join ComputationUnit c with(nolock) on a.cSTComUnitCode =c.cComunitCode where a.cinvcode='" + cinvcode + "'", connstring));
                        if (iInvExchRate == 0)
                        {
                            iInvExchRate = 1;
                        }
                        cassunit = Convert.ToString(SqlAccess.ExecuteScalar("select cSTComUnitCode from Inventory  with(nolock) where  cinvcode='" + cinvcode + "'", connstring));
                        /****************************** 以下是必输字段 ****************************/
                        domBody[j]["autoid"] = "0"; //主关键字段，int类型
                        domBody[j]["cinvcode"] = cinvcode; //材料编码，string类型
                        domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                        /***************************** 以下是非必输字段 ****************************/
                        if (cwhcode == "06")
                        {
                            domBody[j]["cposition"] = "06";
                        }
                        domBody[j]["id"] = "0"; //与主表关联项，int类型
                        domBody[j]["cinvaddcode"] = ""; //材料代码，string类型
                        domBody[j]["cinvname"] = ""; //材料名称，string类型
                        domBody[j]["cinvstd"] = ""; //规格型号，string类型
                        domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                        domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                        domBody[j]["creplaceitem"] = ""; //替换件，string类型
                        // domBody[j]["cposition"] = ""; //货位编码，string类型
                        domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                        domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                        domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                        domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                        domBody[j]["cbatchproperty1"] = ""; //批次属性1，double类型
                        domBody[j]["cbatchproperty2"] = ""; //批次属性2，double类型
                        domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                        bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                        if (bInvBatch == 1)
                        {
                            domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[i]["LOTTABLE02"]); //属性6，string类型 炉号
                            domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[i]["LOTTABLE03"]);//属性7，string类型  母卷号
                            domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[i]["ID"], ""); //批号，string类型
                        }
                        else
                        {
                            domBody[j]["cbatchproperty7"] = ""; //属性6，string类型 炉号
                            domBody[j]["cbatchproperty6"] ="";//属性7，string类型  母卷号
                            domBody[j]["cbatch"] = ""; //批号，string类型
                        }

                        if (cWh == "W")
                        {
                            domBody[j]["iomomid"] = iOMoMID; //委外用料表ID，int类型
                            domBody[j]["iomodid"] = iOMoDID; //委外订单子表ID，int类型
                            domBody[j]["comcode"] = comcode; //委外订单号，string类型
                            domBody[j]["invcode"] = invcode; //产品编码，string类型
                        }
                        else
                        {
                            domBody[j]["iomomid"] = ""; //委外用料表ID，int类型
                            domBody[j]["iomodid"] = ""; //委外订单子表ID，int类型
                            domBody[j]["comcode"] = ""; //委外订单号，string类型
                            domBody[j]["invcode"] = ""; //产品编码，string类型
                        }
                        domBody[j]["iinvexchrate"] = iInvExchRate; //换算率，double类型
                        //    domBody[j]["inum"] = iQuantity / iInvExchRate; //件数，double类型
                        domBody[j]["inum"] = "";
                        domBody[j]["iquantity"] = iQuantity; //数量，double类型
                        domBody[j]["iunitcost"] = iTaxUnitPrice; //单价，double类型
                        domBody[j]["iprice"] = iSum; //金额，double类型
                        domBody[j]["ipunitcost"] = iTaxUnitPrice; //计划单价，double类型
                        domBody[j]["ipprice"] = iSum; //计划金额，double类型
                        domBody[j]["dvdate"] = ""; //失效日期，DateTime类型
                        domBody[j]["cobjcode"] = ""; //成本对象编码，string类型
                        domBody[j]["cname"] = ""; //项目，string类型
                        domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                        domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                        domBody[j]["dsdate"] = ""; //结算日期，DateTime类型
                        domBody[j]["ifquantity"] = ""; //实际数量，double类型
                        domBody[j]["ifnum"] = ""; //实际件数，double类型
                        domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                        domBody[j]["cbatchproperty3"] = ""; //批次属性3，double类型
                        domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                        domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                        domBody[j]["cbatchproperty4"] = ""; //批次属性4，double类型
                        domBody[j]["cbatchproperty5"] = ""; //批次属性5，double类型
                        domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                        domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                     
                        domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                        domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                        domBody[j]["cbatchproperty8"] = ""; //批次属性8，string类型
                        domBody[j]["cbatchproperty9"] = ""; //批次属性9，string类型
                        domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                        domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                        domBody[j]["cbatchproperty10"] = ""; //批次属性10，DateTime类型
                        domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                        domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                        domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                        domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                        domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                        domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                        domBody[j]["inquantity"] = ""; //应发数量，double类型
                        domBody[j]["innum"] = ""; //应发件数，double类型
                        domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                        domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                        domBody[j]["isodid"] = ""; //销售订单子表ID，string类型

                        domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                        domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                        domBody[j]["imassdate"] = ""; //保质期，int类型
                        domBody[j]["cassunit"] = cassunit; //库存单位码，string类型
                        domBody[j]["cvenname"] = ""; //供应商，string类型
                        domBody[j]["cposname"] = ""; //货位，string类型
                        domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                        domBody[j]["cmolotcode"] = ""; //生产批号，string类型
                        domBody[j]["dmsdate"] = ""; //核销日期，DateTime类型
                        domBody[j]["cmassunit"] = ""; //保质期单位，int类型
                        domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                        domBody[j]["cmocode"] = ""; //生产订单号，string类型

                        domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                        domBody[j]["cvmivenname"] = ""; //代管商，string类型
                        domBody[j]["bvmiused"] = ""; //代管消耗标识，int类型
                        domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                        domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                        domBody[j]["productinids"] = ""; //productinids，int类型
                        domBody[j]["crejectcode"] = ""; //在库不良品处理单号，string类型
                        domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                        domBody[j]["iordertype"] = "0"; //销售订单类别，int类型
                        domBody[j]["iorderdid"] = ""; //iorderdid，int类型
                        domBody[j]["iordercode"] = ""; //销售订单号，string类型
                        domBody[j]["iorderseq"] = ""; //销售订单行号，string类型
                        domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                        domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                        domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                        domBody[j]["cciqbookcode"] = ""; //手册号，string类型
                        domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                        domBody[j]["copdesc"] = ""; //工序说明，string类型
                        domBody[j]["cmworkcentercode"] = ""; //工作中心编码，string类型
                        domBody[j]["cmworkcenter"] = ""; //工作中心，string类型

                        domBody[j]["invname"] = ""; //产品，string类型
                        domBody[j]["cwhpersonname"] = ""; //库管员名称，string类型
                        domBody[j]["cbaccounter"] = ""; //记账人，string类型
                        domBody[j]["bcosting"] = ""; //是否核算，string类型
                        domBody[j]["isotype"] = "0"; //需求跟踪方式，int类型
                        domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                        domBody[j]["imoseq"] = ""; //生产订单行号，string类型
                        domBody[j]["iopseq"] = ""; //工序行号，string类型
                        domBody[j]["isquantity"] = ""; //累计核销数量，double类型
                        domBody[j]["ismaterialfee"] = ""; //累计核销金额，double类型
                        domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                        domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                        domBody[j]["cwhpersoncode"] = ""; //库管员编码，string类型
                        domBody[j]["cdefine22"] = ClsSystem.gnvl(dHead.Rows[0]["ORDERKEY"], "");  //表体自定义项1，string类型
                        domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
                        domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                        domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                        domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                        domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                        domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                        domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                        domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                        domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                        domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                        domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                        domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                        domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                        domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                        domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                        domBody[j]["cbarcode"] = ""; //条形码，string类型
                        domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                        domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[i]["ID"], ""); //表体自定义项3，string类型
                        domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
                        domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                        domBody[j]["cdefine26"] = ClsSystem.gnvl(dBody.Rows[k]["SUSR2"], ""); //表体自定义项5，double类型
                        domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                        domBody[j]["citemcode"] = ""; //项目编码，string类型
                        domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                        domBody[j]["citemcname"] = ""; //项目大类名称，string类型

                        domBody[j]["imaids"] = autoid; 


                        j++;
                    }
                }
            }

            //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
                  if (cwhcode == "06")
                  {
                       broker.AssignNormalValue("domPosition",null );
                  } else 
                  {
                       broker.AssignNormalValue("domPosition","" );
                  }

            //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

            //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
            broker.AssignNormalValue("cnnFrom", null);

            //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
            broker.AssignNormalValue("VouchId", "");

            //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
            MSXML2.IXMLDOMDocument2 domMsg =new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("domMsg", domMsg);

            //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
            broker.AssignNormalValue("bCheck",true);

            //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
            broker.AssignNormalValue("bBeforCheckStock", true);

            //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
            if (bredvouch == 1)
            {
                broker.AssignNormalValue("bIsRedVouch", true);
            }
            else
            {
                broker.AssignNormalValue("bIsRedVouch", false);
            }

            //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
            broker.AssignNormalValue("sAddedState","");

            //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
            broker.AssignNormalValue("bReMote", false);

            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        Console.WriteLine("系统异常：" + sysEx.Message);
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        Console.WriteLine("API异常：" + bizEx.Message);
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        Console.WriteLine("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
               // ado.RollbackTrans();
                return "API错误:" + apiEx.ToString().Substring(0, 100);
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
            System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


            //获取out/inout参数值

            //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            System.String errMsgRet = broker.GetResult("errMsg") as System.String;
            if (!result)
            {
                broker.Release();
                //ado.RollbackTrans();
                return "API错误:" + errMsgRet;

            }


            //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空

            VouchNO = broker.GetResult("VouchId") as System.String;
          //  VouchNO = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select cCode  from rdrecord11 where id=" + VouchIdRet, connstring), "");

            //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
            MSXML2.IXMLDOMDocument2 domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

            //结束本次调用，释放API资源
            broker.Release();

            //return VouchIdRet;
            #endregion


           // ado.CommitTrans();

            }
            catch (Exception ex)
            {

               // ado.RollbackTrans();
                return "API错误：" + ex.ToString().Substring(0, 50);
            }
            return VouchNO;     
        }
        //其它入库单
        public static string InRD08(U8Login.clsLogin u8Login, string connstring, DataTable dHead, DataTable dBody, int bredvouch, string crdcode)
        {
            System.String VouchNO = "";
            ADODB.Connection ado = new ADODB.Connection();
            try
            {    
                #region 其它入库单

            string cCode = Convert.ToString(dHead.Rows[0]["POKEY"]);

            //第二步：构造环境上下文对象，传入login，connstring
            U8EnvContext envContext = new U8EnvContext();
            envContext.U8Login = u8Login;
            //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
           // ado.BeginTrans();

            //第三步：设置API地址标识(Url)
            //当前API错误：添加新单据的地址标识为：U8API/otherin/Add
            U8ApiAddress myApiAddress = new U8ApiAddress("U8API/otherin/Add");

            //第四步：构造APIBroker
            U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

            //第五步：API参数赋值

            //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：08
            broker.AssignNormalValue("sVouchType", "08");

            //给BO表头参数DomHead赋值，此BO参数的业务类型为其他入库单，属表头参数。BO参数均按引用传递
            //提示：给BO表头参数DomHead赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject DomHead = broker.GetBoParam("DomHead");
            DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
            //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            /****************************** 以下是必输字段 ****************************/
            DomHead[0]["id"] = "0"; //主关键字段，int类型
            DomHead[0]["ccode"] = Public.GetParentCode("0301", "", connstring); //入库单号，string类型
            string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 1);
            //  string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 0);
            rq1 = rq1.Insert(4, "/");
            rq1 = rq1.Insert(7, "/");
            DomHead[0]["ddate"] =Convert.ToDateTime(rq1).ToString(); //入库日期，DateTime类型

            string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");          

            if (cwhcode == "")
            {
                cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and  wmsbm='" + Convert.ToString(dBody.Rows[0]["TOLOC"]) + "'", connstring), "");
            }
        
            DomHead[0]["cwhname"] = Convert.ToString(SqlAccess.ExecuteScalar(" select cwhname from warehouse  with(nolock) where cWhCode='" + cwhcode + "'", connstring));//仓库，string类型
            string codepcode = "";
            codepcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR2"], "");
            if (codepcode == "")
            {
                codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
            }

            /***************************** 以下是非必输字段 ****************************/
            DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
            DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
            DomHead[0]["dnmaketime"] = DateTime.Today; //制单时间，DateTime类型
            DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
            DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
            DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
            DomHead[0]["iavaquantity"] = ""; //可用量，double类型
            DomHead[0]["iavanum"] = ""; //可用件数，double类型
            DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
            DomHead[0]["cchkcode"] = ""; //检验单号，string类型
            DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString(); //时间戳，string类型
            DomHead[0]["crdname"] = ""; //入库类别，string类型
            DomHead[0]["cbustype"] = "其他入库"; //业务类型，int类型
            DomHead[0]["ireturncount"] = "0"; //ireturncount，string类型
            DomHead[0]["iverifystate"] = "0"; //iverifystate，string类型
            DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，string类型
            DomHead[0]["cbuscode"] = ""; //业务号，string类型
            DomHead[0]["cdepname"] = ""; //部门，string类型
            DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
            DomHead[0]["cmemo"] = ClsSystem.gnvl(dHead.Rows[0]["RECEIPTKEY"], ""); //备注，string类型
            DomHead[0]["cmaker"] = ClsSystem.gnvl(dHead.Rows[0]["EDITWHO"], ""); //制单人，string类型
            DomHead[0]["chandler"] = ""; //审核人，string类型
            DomHead[0]["cchkperson"] = ""; //检验员，string类型
            DomHead[0]["caccounter"] = ""; //记账人，string类型
            DomHead[0]["ipresent"] = ""; //现存量，string类型
            DomHead[0]["isafesum"] = ""; //安全库存量，string类型
            DomHead[0]["itopsum"] = ""; //最高库存量，string类型
            DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
            DomHead[0]["cvouchname"] = ""; //单据类型，string类型
            DomHead[0]["cpersonname"] = ""; //业务员，string类型
            DomHead[0]["cvenabbname"] = ""; //供应商，string类型
            DomHead[0]["csource"] = "库存"; //单据来源，int类型
            DomHead[0]["brdflag"] = "1"; //收发标志，string类型
            DomHead[0]["cvouchtype"] = "08"; //单据类型编码，string类型
            DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
            DomHead[0]["crdcode"] = crdcode; //入库类别编码，string类型
            DomHead[0]["cwhcode"] = cwhcode; //仓库编码，string类型
            DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
            DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
            DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
            DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
            DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
            DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
            DomHead[0]["bisstqc"] = "0"; //库存期初标记，string类型
            DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
            DomHead[0]["vt_id"] = "67"; //模版号，int类型
            DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
            DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
            DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
            DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
            DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
            DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
            DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
            DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
            DomHead[0]["cvencode"] = ClsSystem.gnvl(dHead.Rows[0]["SUPPLIERCODE"], ""); //供应商编码，string类型
            DomHead[0]["cdepcode"] = codepcode; //部门编码，string类型
            DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型

            //给BO表体参数domBody赋值，此BO参数的业务类型为其他入库单，属表体参数。BO参数均按引用传递
            //提示：给BO表体参数domBody赋值有两种方法

            //方法一是直接传入MSXML2.DOMDocumentClass对象
            //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

            //方法二是构造BusinessObject对象，具体方法如下：
            BusinessObject domBody = broker.GetBoParam("domBody");


            //过滤重复数据
            DataTable dts = dBody;
            //DataTable dts = Public.CheckDataTable(dBody, "rdrecords08", ClsSystem.gnvl(dHead.Rows[0]["SERIALKEY"], ""), "0");

            //if (dts.Rows.Count < 1)
            //{
            //    return "API错误：表体无数据";
            //}

            domBody.RowCount = dts.Rows.Count; //设置BO对象行数
            //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
            //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
            //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

            string cinvcode = "";
            string cassunit = "";
            decimal iInvExchRate=0;
            decimal iTaxUnitPrice=0;
            decimal iSum=0;
             decimal iQuantity=0;
             int bInvBatch = 0;
            DataTable dtcx= new DataTable();
            for (int j = 0; j < dts.Rows.Count; j++)
            {

               

                    cinvcode = Convert.ToString(dts.Rows[j]["SKU"]);
                    iQuantity = Public.GetNum(dts.Rows[j]["QTYRECEIVED"]);//数量

                    //dtcx = SqlAccess.ExecuteSqlDataTable("select top 1 d.ID,d.iTaxPrice ,d.iSum   from PO_Podetails d with(nolock) join PO_Pomain m with(nolock) on d.POID=m.POID  where m.cPOID ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring);

                    //if (dtcx.Rows.Count > 0)
                    //{
                    //    iPOsID = Convert.ToString(dtcx.Rows[0]["ID"]);
                    //    //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));

                    //iTaxUnitPrice = Public.GetNum(dtcx.Rows[0]["iTaxPrice"]);
                    //iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                    //    iUnitPrice = Public.GetNum(dtcx.Rows[0]["iUnitPrice"]);//原币无税单价
                    //    iMoney = Public.GetNum(dtcx.Rows[0]["iMoney"]);//原币无税金额
                    //    iTax = Public.GetNum(dtcx.Rows[0]["iTax"]);//原币税额

                    //    iNatUnitPrice = Public.GetNum(dtcx.Rows[0]["iNatUnitPrice"]);//本币无税单价
                    //    iNatSum = Public.GetNum(dtcx.Rows[0]["iNatSum"]);//本币价税合计
                    //    iNatMoney = Public.GetNum(dtcx.Rows[0]["iNatMoney"]);//本币无税金额
                    //    iNatTax = Public.GetNum(dtcx.Rows[0]["iNatTax"]);//本币税额
                    //  }
                    iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                    iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                    iInvExchRate = Convert.ToDecimal(SqlAccess.ExecuteScalar("select c.iChangRate  from Inventory a with(nolock) join ComputationUnit c with(nolock) on a.cSTComUnitCode =c.cComunitCode where a.cinvcode='" + cinvcode + "'", connstring));
                    cassunit = Convert.ToString(SqlAccess.ExecuteScalar("select cSTComUnitCode from Inventory  with(nolock) where  cinvcode='" + cinvcode + "'", connstring));

                    /****************************** 以下是必输字段 ****************************/
                    domBody[j]["autoid"] = "0"; //主关键字段，int类型
                    domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                    domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                    /***************************** 以下是非必输字段 ****************************/
                    if (cwhcode == "06")
                    {
                        domBody[j]["cposition"] = "06";
                    }
                    domBody[j]["isodid"] = ""; //销售订单子表ID，string类型
                    domBody[j]["irejectids"] = ""; //不良品处理单子表id，int类型
                    domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                    domBody[j]["cinvname"] = ""; //存货名称，string类型
                    domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                    domBody[j]["cinvstd"] = ""; //规格型号，string类型
                    domBody[j]["cmassunit"] = ""; //保质期单位，int类型
                    domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                    domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                    domBody[j]["ccheckcode"] = ""; //检验单号，string类型
                    domBody[j]["ccheckpersoncode"] = ""; //检验员编码，string类型
                    domBody[j]["ccheckpersonname"] = ""; //检验员，string类型
                    domBody[j]["crejectcode"] = ""; //不良品处理单号，string类型
                    domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                    domBody[j]["cvmivenname"] = ""; //代管商，string类型
                    domBody[j]["bvmiused"] = ""; //代管消耗标识，int类型
                    domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                    domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                    domBody[j]["cassunit"] = cassunit; //库存单位码，string类型
                    domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                    domBody[j]["innum"] = ""; //应收件数，double类型
                    domBody[j]["inum"] = ""; //件数，double类型
                    domBody[j]["iinvexchrate"] = iInvExchRate; //换算率，double类型
                    domBody[j]["inquantity"] = iQuantity; //应收数量，double类型
                    domBody[j]["iquantity"] = iQuantity; //数量，double类型
                    domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                    domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                    domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                    domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                    domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                    domBody[j]["iunitcost"] = iTaxUnitPrice; //单价，double类型
                    domBody[j]["iprice"] = iSum; //金额，double类型
                    domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                    domBody[j]["isotype"] = ""; //需求跟踪方式，int类型
                    domBody[j]["cbaccounter"] = ""; //记账人，string类型
                    domBody[j]["bcosting"] = ""; //是否核算，string类型
                    domBody[j]["ipunitcost"] = iTaxUnitPrice; //计划单价／售价，double类型
                    domBody[j]["ipprice"] = iSum; //计划金额／售价金额，double类型
                    bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                    if (bInvBatch == 1)
                    {
                        domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[j]["LOTTABLE02"]); //属性6，string类型 炉号
                        domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[j]["LOTTABLE03"]);//属性7，string类型  母卷号
                        domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], ""); //批号，string类型
                    }
                    else
                    {
                        domBody[j]["cbatchproperty7"] = ""; //属性6，string类型 炉号
                        domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号
                        domBody[j]["cbatch"] = "";
                    }

                    domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                    domBody[j]["imassdate"] = ""; //保质期，int类型
                    domBody[j]["ifquantity"] = ""; //实际数量，double类型
                    domBody[j]["ifnum"] = ""; //实际件数，double类型
                    domBody[j]["dvdate"] = ""; //失效日期，DateTime类型
                    domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                    domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                    domBody[j]["cbvencode"] = ClsSystem.gnvl(dHead.Rows[0]["SUPPLIERCODE"], ""); //供应商编码，string类型
                    domBody[j]["cvenname"] = ""; //供应商，string类型
                    domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                    domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                  //  domBody[j]["cposition"] = ""; //货位编码，string类型
                    domBody[j]["creplaceitem"] = ""; //替换件，string类型
                    domBody[j]["cposname"] = ""; //货位，string类型
                    domBody[j]["citemcode"] = ""; //项目编码，string类型
                    domBody[j]["cname"] = ""; //项目，string类型
                    domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                    domBody[j]["itrids"] = ""; //其他业务AUTOID，double类型
                    domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                    domBody[j]["id"] = "0"; //ID，int类型
                    domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                    domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                    domBody[j]["cbarcode"] = ""; //条形码，string类型
                    domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                    domBody[j]["cbatchproperty1"] = ""; //批次属性1，double类型
                    domBody[j]["cbatchproperty2"] = ""; //批次属性2，double类型
                    domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                    domBody[j]["cbatchproperty3"] = ""; //批次属性3，double类型
                    domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                    domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                    domBody[j]["cbatchproperty4"] = ""; //批次属性4，double类型
                    domBody[j]["cbatchproperty5"] = ""; //批次属性5，double类型
                    domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                    domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                   
                    domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                    domBody[j]["cbatchproperty8"] = ""; //批次属性8，string类型
                    domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                    domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                    domBody[j]["cbatchproperty9"] = ""; //批次属性9，string类型
                    domBody[j]["cbatchproperty10"] = ""; //批次属性10，DateTime类型
                    domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                    domBody[j]["cdefine22"] = ClsSystem.gnvl(dHead.Rows[0]["RECEIPTKEY"], ""); //表体自定义项1，string类型
                    domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                    domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], ""); //表体自定义项3，string类型
                    domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                    domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                    domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                    domBody[j]["cdefine26"] = ClsSystem.gnvl(dts.Rows[j]["SUSR2"], ""); //表体自定义项5，double类型
                    domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                    domBody[j]["cdefine28"] = ClsSystem.gnvl(dHead.Rows[0]["SERIALKEY"], "") + ClsSystem.gnvl(dts.Rows[j]["SERIALKEY"], ""); //表体自定义项7，string类型
                    domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                    domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                    domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                    domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                    domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                    domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                    domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                    domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                    domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                    domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                    domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                    domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                    domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                    domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                    domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                    domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                    domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                    domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                    domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                    domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                    domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                    domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                    domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型

            
                
            }

            //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
            broker.AssignNormalValue("domPosition", "");

            //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

            //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
            broker.AssignNormalValue("cnnFrom", null);

            //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
            broker.AssignNormalValue("VouchId", "");

            //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
            MSXML2.IXMLDOMDocument2 domMsg = new MSXML2.DOMDocumentClass();
            broker.AssignNormalValue("domMsg", domMsg);

            //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
            broker.AssignNormalValue("bCheck", false);

            //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
            broker.AssignNormalValue("bBeforCheckStock",false);

            //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
            broker.AssignNormalValue("bIsRedVouch",false);

            //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
            broker.AssignNormalValue("sAddedState", "");

            //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
            broker.AssignNormalValue("bReMote", false);

            //第六步：调用API
            if (!broker.Invoke())
            {
                //API处理
                Exception apiEx = broker.GetException();
                if (apiEx != null)
                {
                    if (apiEx is MomSysException)
                    {
                        MomSysException sysEx = apiEx as MomSysException;
                        Console.WriteLine("系统异常：" + sysEx.Message);
                        //todo:异常处理
                    }
                    else if (apiEx is MomBizException)
                    {
                        MomBizException bizEx = apiEx as MomBizException;
                        Console.WriteLine("API异常：" + bizEx.Message);
                        //todo:异常处理
                    }
                    //异常原因
                    String exReason = broker.GetExceptionString();
                    if (exReason.Length != 0)
                    {
                        Console.WriteLine("异常原因：" + exReason);
                    }
                }
                //结束本次调用，释放API资源
                broker.Release();
               // ado.RollbackTrans();
                return "API错误：" + apiEx.ToString().Substring(0, 100);
            }

            //第七步：获取返回结果

            //获取返回值
            //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
            System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());

            //获取out/inout参数值

            //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            System.String errMsgRet = broker.GetResult("errMsg") as System.String;
            if (!result)
            {
                broker.Release();

                //ado.RollbackTrans();
                return "API错误：" + errMsgRet;

            }


            //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
            VouchNO = broker.GetResult("VouchId") as System.String;

            //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
            MSXML2.IXMLDOMDocument2 domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

            //结束本次调用，释放API资源
            broker.Release();
            //ado.CommitTrans();


          
            #endregion

            }
            catch (Exception ex)
            {

                //ado.RollbackTrans();
                return "API错误：" + ex.ToString().Substring(0, 50);
            }
            return VouchNO;     
         }
        //其它出库单
        public static string InRD09(U8Login.clsLogin u8Login, string connstring, DataTable dHead, DataTable dBody, DataTable dpicks, DataTable dFb, int bredvouch, string crdcode)
        {
              string VouchNO = ""; 
            ADODB.Connection ado = new ADODB.Connection();


            try
            {
                #region 其它出库单

                //  string cCode = Convert.ToString(dt.Rows[iRow]["csocode"]);
                //第二步：构造环境上下文对象，传入login，connstring
                U8EnvContext envContext = new U8EnvContext();
                envContext.U8Login = u8Login;
                //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
                //ado.BeginTrans();

                //第三步：设置API地址标识(Url)
                //当前API错误：添加新单据的地址标识为：U8API/otherout/Add
                U8ApiAddress myApiAddress = new U8ApiAddress("U8API/otherout/Add");

                //第四步：构造APIBroker
                U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

                //第五步：API参数赋值

                //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：09
                broker.AssignNormalValue("sVouchType", "09");

                //给BO表头参数DomHead赋值，此BO参数的业务类型为其他出库单，属表头参数。BO参数均按引用传递
                //提示：给BO表头参数DomHead赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject DomHead = broker.GetBoParam("DomHead");
                DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
                //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                /****************************** 以下是必输字段 ****************************/
                DomHead[0]["id"] = "0"; //主关键字段，int类型
                DomHead[0]["ccode"] = Public.GetParentCode("0302", "", connstring); ; //出库单号，string类型
                string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["EDITDATE"], ""), 1);
                //string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 0);
                rq1 = rq1.Insert(4, "/");
                rq1 = rq1.Insert(7, "/");
                DomHead[0]["ddate"] = Convert.ToDateTime(rq1).ToShortDateString(); //出库日期，DateTime类型
                // string cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where wmsbm='" + ClsSystem.gnvl(dFb.Rows[0]["LOC"], "") + "'", connstring), "");
                string cwhcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");

                if (cwhcode == "")
                {
                    cwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and wmsbm='" + Convert.ToString(dFb.Rows[0]["LOC"]) + "'", connstring), "");
                }

                DomHead[0]["cwhname"] = Convert.ToString(SqlAccess.ExecuteScalar(" select cwhname from warehouse  with(nolock) where cWhCode='" + cwhcode + "'", connstring));//仓库，string类型
                string codepcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR2"], "");
                if (codepcode == "")
               {
                   codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cwhcode + "'", connstring), "");
               }
                /***************************** 以下是非必输字段 ****************************/
                DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
                DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
                DomHead[0]["dnmaketime"] = DateTime.Today; //制单时间，DateTime类型
                DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
                DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
                DomHead[0]["cchkcode"] = ""; //检验单号，string类型
                DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
                DomHead[0]["iavaquantity"] = ""; //可用量，string类型
                DomHead[0]["iavanum"] = ""; //可用件数，string类型
                DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
                DomHead[0]["ufts"] = DateTime.Now.ToFileTimeUtc().ToString(); //时间戳，string类型
                DomHead[0]["crdname"] = ""; //出库类别，string类型
                DomHead[0]["cbustype"] = "其他出库"; //业务类型，int类型
                DomHead[0]["iverifystate"] = "0"; //iverifystate，string类型
                DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，string类型
                DomHead[0]["controlresult"] = "0"; //controlresult，string类型
                DomHead[0]["ibg_overflag"] = ""; //预算审批状态，int类型
                DomHead[0]["cbg_auditor"] = ""; //预算审批人，string类型
                DomHead[0]["cbg_audittime"] = ""; //预算审批时间，string类型
                DomHead[0]["ireturncount"] = "0"; //ireturncount，string类型
                DomHead[0]["cbuscode"] = ""; //业务号，string类型
                DomHead[0]["cdepname"] = ""; //部门，string类型
                DomHead[0]["dveridate"] = ""; //审核日期，DateTime类型
                DomHead[0]["cmemo"] = ClsSystem.gnvl(dHead.Rows[0]["ORDERKEY"], ""); //备注，string类型
                DomHead[0]["cmaker"] = ClsSystem.gnvl(dHead.Rows[0]["EDITWHO"], ""); //制单人，string类型
                DomHead[0]["chandler"] = ""; //审核人，string类型
                DomHead[0]["cchkperson"] = ""; //检验员，string类型
                DomHead[0]["caccounter"] = ""; //记账人，string类型
                DomHead[0]["ipresent"] = ""; //现存量，string类型
                DomHead[0]["isafesum"] = ""; //安全库存量，string类型
                DomHead[0]["itopsum"] = ""; //最高库存量，string类型
                DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
                DomHead[0]["ccusperson"] = ""; //客户联系人，string类型
                DomHead[0]["ccusphone"] = ""; //客户电话，string类型
                DomHead[0]["ccushand"] = ""; //客户手机，string类型
                DomHead[0]["ccusaddress"] = ""; //客户地址，string类型
                DomHead[0]["contactphone"] = ""; //客户联系人电话，string类型
                DomHead[0]["contactmobile"] = ""; //客户联系人手机，string类型
                DomHead[0]["cdeliverunit"] = ""; //收货单位，string类型
                DomHead[0]["ccontactname"] = ""; //收货单位联系人，string类型
                DomHead[0]["cofficephone"] = ""; //收货单位联系人电话，string类型
                DomHead[0]["cmobilephone"] = ""; //收货单位联系人手机，string类型
                DomHead[0]["cpsnophone"] = ""; //业务员电话，string类型
                DomHead[0]["cpsnmobilephone"] = ""; //业务员手机，string类型
                DomHead[0]["cvouchname"] = ""; //单据类型，string类型
                DomHead[0]["cshipaddress"] = ""; //发货地址，string类型
                DomHead[0]["cpersonname"] = ""; //业务员，string类型
                DomHead[0]["ccusabbname"] = ""; //客户，string类型
                DomHead[0]["ccuscode"] = ""; //客户编码，string类型
                DomHead[0]["csource"] = "库存"; //单据来源，int类型
                DomHead[0]["brdflag"] = "0"; //收发标志，string类型
                DomHead[0]["cvouchtype"] = "09"; //单据类型编码，string类型
                DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
                DomHead[0]["crdcode"] = crdcode; //出库类别编码，string类型
                DomHead[0]["cwhcode"] = cwhcode; //仓库编码，string类型
                DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
                DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
                DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
                DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
                DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
                DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
                DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
                DomHead[0]["vt_id"] = "85"; //模版号，int类型
                DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
                DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
                DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型
                DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
                DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
                DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
                DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
                DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
                DomHead[0]["cdepcode"] = codepcode; //部门编码，string类型
                DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型

                //给BO表体参数domBody赋值，此BO参数的业务类型为其他出库单，属表体参数。BO参数均按引用传递
                //提示：给BO表体参数domBody赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject domBody = broker.GetBoParam("domBody");


                if (dBody.Rows.Count * dFb.Rows.Count < 1)
                {
                    return "API错误：表体无数据";
                }


                domBody.RowCount = dBody.Rows.Count * dFb.Rows.Count; //设置BO对象行数
                //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
                //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                string cinvcode = "";
                string cassunit = "";
                decimal iInvExchRate = 0;
                decimal iTaxUnitPrice = 0;
                decimal iSum = 0;
                decimal iQuantity = 0;
                int bInvBatch = 0;
                string cPdID = "";
                DataTable dtcx = new DataTable();
                int j = 0;
                for (int k = 0; k < dBody.Rows.Count; k++)
                {
                    cinvcode = ClsSystem.gnvl(dBody.Rows[k]["SKU"], "");
                    cPdID = ClsSystem.gnvl(dBody.Rows[k]["ShipmentOrderDetail_ID"], "");
                    DataView rowfilter2 = new DataView(dpicks);
                    rowfilter2.RowFilter = "ShipmentOrderDetail_ID =" + cPdID;
                    rowfilter2.RowStateFilter = DataViewRowState.CurrentRows;
                    DataTable dpick = rowfilter2.ToTable();

                    if (dpick.Rows.Count > 0)
                    {

                        string cpicksid = ClsSystem.gnvl(dpick.Rows[0]["PickDetails_ID"], "");

                        DataView rowfilter1 = new DataView(dFb);
                        rowfilter1.RowFilter = "PickDetails_ID =" + cpicksid;
                        rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                        DataTable dts = rowfilter1.ToTable();
                        for (int i = 0; i < dts.Rows.Count; i++)
                        {

                            iQuantity = Public.GetNum(dts.Rows[i]["QTY"]);//数量

                            //dtcx = SqlAccess.ExecuteSqlDataTable("select top 1 d.ID,d.iTaxPrice ,d.iSum   from PO_Podetails d with(nolock) join PO_Pomain m with(nolock) on d.POID=m.POID  where m.cPOID ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring);

                            //if (dtcx.Rows.Count > 0)
                            //{
                            //    iPOsID = Convert.ToString(dtcx.Rows[0]["ID"]);
                            //    //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));
                            iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                            iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                            //iTaxUnitPrice = Public.GetNum(dtcx.Rows[0]["iTaxPrice"]);
                            //iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                            //    iUnitPrice = Public.GetNum(dtcx.Rows[0]["iUnitPrice"]);//原币无税单价
                            //    iMoney = Public.GetNum(dtcx.Rows[0]["iMoney"]);//原币无税金额
                            //    iTax = Public.GetNum(dtcx.Rows[0]["iTax"]);//原币税额

                            //    iNatUnitPrice = Public.GetNum(dtcx.Rows[0]["iNatUnitPrice"]);//本币无税单价
                            //    iNatSum = Public.GetNum(dtcx.Rows[0]["iNatSum"]);//本币价税合计
                            //    iNatMoney = Public.GetNum(dtcx.Rows[0]["iNatMoney"]);//本币无税金额
                            //    iNatTax = Public.GetNum(dtcx.Rows[0]["iNatTax"]);//本币税额
                            //  }
                            iInvExchRate = Convert.ToDecimal(SqlAccess.ExecuteScalar("select c.iChangRate  from Inventory a with(nolock) join ComputationUnit c with(nolock) on a.cSTComUnitCode =c.cComunitCode where a.cinvcode='" + cinvcode + "'", connstring));
                            cassunit = Convert.ToString(SqlAccess.ExecuteScalar("select cSTComUnitCode from Inventory  with(nolock) where  cinvcode='" + cinvcode + "'", connstring));


                            /****************************** 以下是必输字段 ****************************/
                            domBody[j]["autoid"] = "0"; //主关键字段，int类型
                            domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                            domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                            /***************************** 以下是非必输字段 ****************************/
                            if (cwhcode == "06")
                            {
                                domBody[j]["cposition"] = "06";
                            }
                            domBody[j]["ieqdid"] = ""; //作业单子表ID，int类型
                            domBody[j]["isodid"] = ""; //销售订单子表ID，string类型
                            domBody[j]["irejectids"] = ""; //不良品处理单子表id，int类型
                            domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                            domBody[j]["cinvname"] = ""; //存货名称，string类型
                            domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                            domBody[j]["scrapufts"] = ""; //不合格品时间戳，string类型
                            domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                            domBody[j]["cmassunit"] = ""; //保质期单位，int类型
                            domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                            domBody[j]["cinvstd"] = ""; //规格型号，string类型
                            domBody[j]["ccheckcode"] = ""; //检验单号，string类型
                            domBody[j]["ccheckpersoncode"] = ""; //检验员编码，string类型
                            domBody[j]["ccheckpersonname"] = ""; //检验员，string类型
                            domBody[j]["crejectcode"] = ""; //不良品处理单号，string类型
                            domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                            domBody[j]["cvmivenname"] = ""; //代管商，string类型
                            domBody[j]["bvmiused"] = ""; //代管消耗标识，int类型
                            domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
                            domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
                            domBody[j]["cbg_itemcode"] = ""; //预算项目编码，string类型
                            domBody[j]["cbg_itemname"] = ""; //预算项目，string类型
                            domBody[j]["cbg_caliberkey1"] = ""; //口径1类型编码，string类型
                            domBody[j]["cbg_caliberkeyname1"] = ""; //口径1类型名称，string类型
                            domBody[j]["cbg_caliberkey2"] = ""; //口径2类型编码，string类型
                            domBody[j]["cbg_caliberkeyname2"] = ""; //口径2类型名称，string类型
                            domBody[j]["cbg_caliberkey3"] = ""; //口径3类型编码，string类型
                            domBody[j]["cbg_caliberkeyname3"] = ""; //口径3类型名称，string类型
                            domBody[j]["cbg_calibercode1"] = ""; //口径1编码，string类型
                            domBody[j]["cbg_calibername1"] = ""; //口径1名称，string类型
                            domBody[j]["cbg_calibercode2"] = ""; //口径2编码，string类型
                            domBody[j]["cbg_calibername2"] = ""; //口径2名称，string类型
                            domBody[j]["cbg_calibercode3"] = ""; //口径3编码，string类型
                            domBody[j]["cbg_calibername3"] = ""; //口径3名称，string类型
                            domBody[j]["cbg_auditopinion"] = ""; //审批意见，string类型
                            domBody[j]["ibgstsum"] = ""; //库存预算金额，string类型
                            domBody[j]["ibgiasum"] = ""; //存货预算金额，string类型
                            domBody[j]["ibg_ctrl"] = ""; //是否预算控制，int类型
                            domBody[j]["cassunit"] = cassunit; //库存单位码，string类型
                            domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                            domBody[j]["innum"] = ""; //应发件数，double类型
                            domBody[j]["inum"] = ""; //件数，double类型
                            domBody[j]["iinvexchrate"] = iInvExchRate; //换算率，double类型
                            domBody[j]["inquantity"] = iQuantity; //应发数量，double类型
                            domBody[j]["ifquantity"] = ""; //实际数量，double类型
                            domBody[j]["ifnum"] = ""; //实际件数，double类型
                            domBody[j]["iquantity"] = iQuantity; //数量，double类型
                            domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
                            domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                            domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                            domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                            domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
                            domBody[j]["iunitcost"] = iTaxUnitPrice; //单价，double类型
                            domBody[j]["cserviceoid"] = ""; //cserviceoid，string类型
                            domBody[j]["cbserviceoid"] = ""; //cbserviceoid，string类型
                            domBody[j]["iprice"] = iSum; //金额，double类型
                            domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                            domBody[j]["isotype"] = ""; //需求跟踪方式，int类型
                            domBody[j]["cbaccounter"] = ""; //记账人，string类型
                            domBody[j]["bcosting"] = ""; //是否核算，string类型
                            domBody[j]["ipunitcost"] = iTaxUnitPrice; //计划单价／售价，double类型
                            domBody[j]["ipprice"] = iSum; //计划金额／售价金额，double类型
                            bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                            if (bInvBatch == 1)
                            {
                                domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[i]["LOTTABLE02"]); //属性6，string类型 炉号
                                domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[i]["LOTTABLE03"]);//属性7，string类型  母卷号
                                domBody[j]["cbatch"] = ClsSystem.gnvl(dts.Rows[i]["ID"], ""); //批号，string类型
                            }
                            else
                            {
                                domBody[j]["cbatchproperty7"] = ""; //属性6，string类型 炉号
                                domBody[j]["cbatchproperty6"] ="";//属性7，string类型  母卷号
                                domBody[j]["cbatch"] = "";
                            }
                            // domBody[j]["cbatch"] = ""; //批号，string类型
                            domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                            domBody[j]["imassdate"] = ""; //保质期，int类型
                            domBody[j]["dvdate"] = ""; //失效日期，DateTime类型
                            domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                            domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
                            domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                            domBody[j]["cvenname"] = ""; //供应商，string类型
                            domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
                            domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
                            //    domBody[j]["cposition"] = ""; //货位编码，string类型
                            domBody[j]["cposname"] = ""; //货位，string类型
                            domBody[j]["creplaceitem"] = ""; //替换件，string类型
                            domBody[j]["citemcode"] = ""; //项目编码，string类型
                            domBody[j]["cname"] = ""; //项目，string类型
                            domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                            domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                            domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
                            domBody[j]["id"] = "0"; //ID，int类型
                            domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
                            domBody[j]["cbarcode"] = ""; //条形码，string类型
                            domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                            domBody[j]["cbatchproperty1"] = ""; //批次属性1，double类型
                            domBody[j]["cbatchproperty2"] = ""; //批次属性2，double类型
                            domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                            domBody[j]["cbatchproperty3"] = ""; //批次属性3，double类型
                            domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                            domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                            domBody[j]["cbatchproperty4"] = ""; //批次属性4，double类型
                            domBody[j]["cbatchproperty5"] = ""; //批次属性5，double类型
                            domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                            domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                           
                            domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                            domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                            domBody[j]["cbatchproperty8"] = ""; //批次属性8，string类型
                            domBody[j]["cbatchproperty9"] = ""; //批次属性9，string类型
                            domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                            domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                            domBody[j]["cbatchproperty10"] = ""; //批次属性10，DateTime类型
                            domBody[j]["cdefine22"] = ClsSystem.gnvl(dHead.Rows[0]["ORDERKEY"], ""); //表体自定义项1，string类型
                            domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                            domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[i]["ID"], ""); //表体自定义项3，string类型
                            domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                            domBody[j]["cdefine26"] = ClsSystem.gnvl(dBody.Rows[k]["SUSR2"], "");//表体自定义项5，double类型
                            domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                            domBody[j]["itrids"] = ""; //自定义项6，double类型
                            domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
                            domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                            domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                            domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                            domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                            domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                            domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                            domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                            domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                            domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                            domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
                            domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                            domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                            domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                            domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                            domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                            domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                            domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                            domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                            domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                            domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                            domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                            domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                            domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                            domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                            domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型

                            j++;
                        }
                    }
                }
                //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
                //if (cwhcode == "06")
                //{
                broker.AssignNormalValue("domPosition", null);
                //}
                //else
                //{
                //    broker.AssignNormalValue("domPosition", "");
                //}

                //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
                broker.AssignNormalValue("cnnFrom", null);//

                //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
                broker.AssignNormalValue("VouchId", "");

                //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                MSXML2.IXMLDOMDocument2 domMsg = new MSXML2.DOMDocumentClass();
                broker.AssignNormalValue("domMsg", domMsg);

                //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
                broker.AssignNormalValue("bCheck", true);

                //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                broker.AssignNormalValue("bBeforCheckStock", true);

                //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
                broker.AssignNormalValue("bIsRedVouch", false);

                //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
                broker.AssignNormalValue("sAddedState", "");

                //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
                broker.AssignNormalValue("bReMote", false);

                //第六步：调用API
                if (!broker.Invoke())
                {
                    //API处理
                    Exception apiEx = broker.GetException();
                    if (apiEx != null)
                    {
                        if (apiEx is MomSysException)
                        {
                            MomSysException sysEx = apiEx as MomSysException;
                            Console.WriteLine("系统异常：" + sysEx.Message);
                            //todo:异常处理
                        }
                        else if (apiEx is MomBizException)
                        {
                            MomBizException bizEx = apiEx as MomBizException;
                            Console.WriteLine("API异常：" + bizEx.Message);
                            //todo:异常处理
                        }
                        //异常原因
                        String exReason = broker.GetExceptionString();
                        if (exReason.Length != 0)
                        {
                            Console.WriteLine("异常原因：" + exReason);
                        }
                    }
                    //结束本次调用，释放API资源
                    broker.Release();
                    //ado.RollbackTrans();

                    return "API错误：" + apiEx.ToString().Substring(0, 100);

                }

                //第七步：获取返回结果

                //获取返回值
                //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
                System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());

                //获取out/inout参数值

                //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                System.String errMsgRet = broker.GetResult("errMsg") as System.String;
                if (!result)
                {
                    broker.Release();
                    // ado.RollbackTrans();
                    return "API错误：" + errMsgRet;

                }


                //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                VouchNO = broker.GetResult("VouchId") as System.String;

                //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                MSXML2.IXMLDOMDocument2 domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

                //结束本次调用，释放API资源
                broker.Release();
                // ado.CommitTrans();

                return VouchNO;
                #endregion
            }
            catch (Exception ex)
            {
               // ado.RollbackTrans();
                string sErr = ex.Message.Replace("'", "''");
                if (sErr.Length > 50)
                    sErr = "API错误：" + sErr.Substring(0, 50);
                if (sErr.IndexOf("错误") > 0)
                {
                }
                else
                {
                    sErr = "API错误:" + sErr;
                }
                return sErr;


            }


        }

        //入库调拨单
        public static string POSDBDDR(U8Login.clsLogin u8Login, string connstring, DataTable dt, DataTable dtst, string cType, string cvoucode)
        {
            string VouchNO = ""; 
            ADODB.Connection ado = new ADODB.Connection();
            string sMsg = "";
            string cTVCode = "";
           
              DataTable dts1 = null;

              DataView rowfilter = new DataView(dtst);
              rowfilter.RowFilter = "SUSR1= '" + cvoucode + "'";
              rowfilter.RowStateFilter = DataViewRowState.CurrentRows;
               dts1 = rowfilter.ToTable();
               if (dtst.Rows.Count < 1 || dtst == null)
               {
                   return "API错误:当收货类型为：钢材采购入库101、钢材委外完工调拨入库103，当除了以上2种收货类型以外的收货类型，该字段为空";
               }
                    try
                    {

                        string cTranRequestCode = Convert.ToString(dt.Rows[0]["SOURCEEXTERNORDERKEY"]);  //wms外部单号作为调拨申请单传入

                        DataTable dtcx= new DataTable();

                        #region 调拨单

                        //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                        U8EnvContext envContext = new U8EnvContext();
                        envContext.U8Login = u8Login;
                        //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
                        //ado.BeginTrans();


                        //第三步：设置API地址标识(Url)
                        //当前API错误：添加新单据的地址标识为：U8API/TransVouch/Add
                        U8ApiAddress myApiAddress = new U8ApiAddress("U8API/TransVouch/Add");

                        //第四步：构造APIBroker
                        U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

                        //第五步：API参数赋值

                        //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：12
                        broker.AssignNormalValue("sVouchType", "12");

                        //给BO表头参数DomHead赋值，此BO参数的业务类型为调拨单，属表头参数。BO参数均按引用传递
                        //提示：给BO表头参数DomHead赋值有两种方法

                        //方法一是直接传入MSXML2.DOMDocumentClass对象
                        //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

                        //方法二是构造BusinessObject对象，具体方法如下：
                        BusinessObject DomHead = broker.GetBoParam("DomHead");
                        DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
                        //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
                        //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                        cTVCode = Public.GetParentCode("0304", "", connstring); //GetParentCode();

                        /****************************** 以下是必输字段 ****************************/
                        DomHead[0]["id"] = "0"; //主关键字段，int类型
                        if (cType == "103")
                        {
                            int id = 0;
                            string flag = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select  cDefine2   from TransVouch  with(nolock)  where cDefine2='" + Convert.ToString(dts1.Rows[0]["SUSR1"]) + "'", connstring), "");
                            if (flag == "")
                            {
                                DomHead[0]["ctvcode"] = ClsSystem.gnvl(dts1.Rows[0]["SUSR1"], "") +"_01";
                            }
                            else
                            {
                                string flag1 = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select max(cTVCode)   from TransVouch  with(nolock)  where cDefine2='" + Convert.ToString(dts1.Rows[0]["SUSR1"]) + "'", connstring), "");
                                string[] Result = flag1.Split('_');

                                if (Result.Length > 1)
                                {
                                     id = Convert.ToInt32(Result[1].ToString()) + 1;
                                }
                                else
                                {
                                    id =  1;
                                }
                                if (id.ToString().Length > 1)
                                {
                                    DomHead[0]["ctvcode"] = ClsSystem.gnvl(dts1.Rows[0]["SUSR1"], "") + "_" + id;
                                }
                                else
                                {
                                    DomHead[0]["ctvcode"] = ClsSystem.gnvl(dts1.Rows[0]["SUSR1"], "") + "_0" + id;
                                }
                            }
                        }
                        else
                        {

                            DomHead[0]["ctvcode"] = cTVCode; //单据号，string类型
                        }
                        // string cwhcode = ClsSystem.gnvl(dt.Tables[0].Rows[0]["SUSR1"], "");------------需要判断是入库还是出库从而决定仓库
                        string cowhcode = "";
                        string ciwhcode = "";
                        if (cType == "000")
                        {
                            cowhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select  cOWhCode  from ST_AppTransVouch with(nolock)  where cTVCode='" + cTranRequestCode + "'", connstring), "");
                            ciwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select  cIWhCode  from ST_AppTransVouch with(nolock)  where cTVCode='" + cTranRequestCode + "'", connstring), "");

                        }
                        else
                        {
                            cowhcode = ClsSystem.gnvl(dt.Rows[0]["SUSR1"], "");
                            // string cowhcode = ClsSystem.gnvl(dt.Tables[0].Rows[0]["SUSR1"], "");// Public.GetStr(",", cwhcode, 1);
                            //string ciwhcode = ClsSystem.gnvl(dts.Rows[0]["TOLOC"], "");// Public.GetStr(",", cwhcode, 0);
                           // ciwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where isnull(isjc,0)=0 and wmsbm='" + ClsSystem.gnvl(dts1.Rows[0]["TOLOC"], "") + "'", connstring), "");
                            ciwhcode = ClsSystem.gnvl(dt.Rows[0]["SUSR3"], "");
                        }

                        string codepcode = "";

                        codepcode = ClsSystem.gnvl(dt.Rows[0]["SUSR2"], "");
                        if (codepcode == "")
                            {
                              codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cowhcode + "'", connstring), "");
                            }
                      
                         
                        string cidepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + ciwhcode + "'", connstring), "");


                        string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dt.Rows[0]["RECEIPTDATE"], ""), 1);
                        string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dt.Rows[0]["RECEIPTDATE"], ""), 0);
           rq1 = rq1.Insert(4, "/");
           rq1 = rq1.Insert(7, "/");
                        DomHead[0]["dtvdate"] = Convert.ToDateTime(rq1).ToShortDateString(); //日期，DateTime类型
                        DomHead[0]["cwhname"] = ""; //转出仓库，string类型
                        DomHead[0]["cwhname_1"] = ""; //转入仓库，string类型

                        /***************************** 以下是非必输字段 ****************************/
                        DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
                        DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
                        DomHead[0]["dnmaketime"] = DateTime.Now.ToString(); //制单时间，DateTime类型
                        DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
                        DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
                        if (cType == "000")
                        {
                            DomHead[0]["ctranrequestcode"] = cTranRequestCode; //调拨申请单号，string类型  cTranRequestCode
                        }
                        else
                        {
                            DomHead[0]["ctranrequestcode"] = "";
                        }
                        DomHead[0]["cdepname_1"] = ""; //转出部门，string类型
                        DomHead[0]["cdepname"] = ""; //转入部门，string类型
                        DomHead[0]["crdname_1"] = ""; //出库类别，string类型
                        DomHead[0]["crdname"] = ""; //入库类别，string类型
                        DomHead[0]["cpersonname"] = ""; //经手人，string类型
                        DomHead[0]["dverifydate"] = ""; //审核日期，DateTime类型
                        DomHead[0]["ctvmemo"] = ClsSystem.gnvl(dt.Rows[0]["RECEIPTKEY"], "");  //备注，string类型
                        DomHead[0]["parentscrp"] = ""; //母件损耗率(％)，double类型
                        DomHead[0]["csource"] = "1"; //单据来源，int类型
                        DomHead[0]["iamount"] = ""; //现存量，string类型
                        DomHead[0]["cmaker"] = ClsSystem.gnvl(dt.Rows[0]["EDITWHO"], ""); //制单人，string类型
                        DomHead[0]["cverifyperson"] = ""; //审核人，string类型
                        DomHead[0]["cfree1"] = ""; //自由项1，string类型
                        DomHead[0]["cfree2"] = ""; //自由项2，string类型
                        DomHead[0]["cfree3"] = ""; //自由项3，string类型
                        DomHead[0]["cfree4"] = ""; //自由项4，string类型
                        DomHead[0]["cfree5"] = ""; //自由项5，string类型
                        DomHead[0]["cfree6"] = ""; //自由项6，string类型
                        DomHead[0]["cfree7"] = ""; //自由项7，string类型
                        DomHead[0]["cfree8"] = ""; //自由项8，string类型
                        DomHead[0]["cfree9"] = ""; //自由项9，string类型
                        DomHead[0]["cfree10"] = ""; //自由项10，string类型
                        DomHead[0]["ufts"] = ""; //时间戳，string类型
                        DomHead[0]["codepcode"] = codepcode; //转出部门编码，string类型
                        DomHead[0]["cidepcode"] = cidepcode; //转入部门编码，string类型
                        DomHead[0]["cpersoncode"] = ""; //经手人编码，string类型
                        DomHead[0]["cordcode"] = "204"; //出库类别编码，string类型
                        DomHead[0]["cirdcode"] = "104"; //入库类别编码，string类型

                    
                      
                        DomHead[0]["ciwhcode"] = ciwhcode; //转入仓库编码，string类型
                        DomHead[0]["cowhcode"] = cowhcode; //转出仓库编码，string类型
                       
                        DomHead[0]["cmpocode"] = ""; //订单号，string类型
                        DomHead[0]["cpspcode"] = ""; //产品结构，string类型
                        DomHead[0]["btransflag"] = ""; //是否传递，string类型
                        DomHead[0]["vt_id"] = "89"; //模版号，int类型
                        DomHead[0]["iquantity"] = ""; //产量，double类型
                        DomHead[0]["iavaquantity"] = ""; //可用量，string类型
                        DomHead[0]["iavanum"] = ""; //可用件数，string类型
                        DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
                        DomHead[0]["iproorderid"] = ""; //生产订单ID，string类型
                        DomHead[0]["cversion"] = ""; //版本号／替代标识，string类型
                        DomHead[0]["bomid"] = ""; //bomid，string类型
                        DomHead[0]["cordertype"] = ""; //订单类型，string类型
                        DomHead[0]["cinvname"] = ""; //产品名称，string类型
                        DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
                        DomHead[0]["itopsum"] = ""; //最高库存量，string类型
                        DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
                        DomHead[0]["isafesum"] = ""; //安全库存量，string类型
                        DomHead[0]["caccounter"] = ""; //记账人，string类型
                        DomHead[0]["ipresent"] = ""; //现存量，string类型
                        DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
                        DomHead[0]["cdefine2"] = Convert.ToString(dts1.Rows[0]["SUSR1"]); //表头自定义项2，string类型
                        DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
                        DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
                        DomHead[0]["itransflag"] = "正向"; //调拨方向，int类型
                        DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
                        DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
                        DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
                        DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
                        DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
                        DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
                        DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
                        DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
                        DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
                        DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
                        DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型


                        //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
                        //提示：给BO表体参数domBody赋值有两种方法

                        //方法一是直接传入MSXML2.DOMDocumentClass对象
                        //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

                        //方法二是构造BusinessObject对象，具体方法如下：
                        BusinessObject domBody = broker.GetBoParam("domBody");


                        DataTable dts = dts1;
                        //过滤重复数据
                        //DataTable dts = Public.CheckDataTable(dts1, "TransVouchs", ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], ""), "0");
                        //if (dts.Rows.Count < 1)
                        //{
                        //    return "API错误：表体无数据";
                        //}
                        domBody.RowCount = dts.Rows.Count; //设置BO对象行数
                        //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
                        //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
                        //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
                        
                        decimal iTaxUnitPrice=0;
                        decimal iSum=0;
                        string  cinvcode = "";
                        int bInvBatch = 0;
                        string iautoID = "";
                        for (int j = 0; j < dts.Rows.Count; j++)
                        {
                            //判断wms重复的数据
                            //string aa = "";
                            //aa = ClsSystem.gnvl(SqlAccess.ExecuteScalar(" select cdefine28 from TransVouchs  with(nolock) where cdefine28='" + ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], "") + ClsSystem.gnvl(dts.Rows[j]["SERIALKEY"], "") + "'", connstring), "");

                            //if (aa == "")
                            //{
                                cinvcode = ClsSystem.gnvl(dts.Rows[j]["SKU"], "");
                                decimal iQuantity = Convert.ToDecimal(dts.Rows[j]["QTYRECEIVED"]);
                                if (cTranRequestCode != "")
                                {
                                    dtcx = SqlAccess.ExecuteSqlDataTable("select top 1 autoID    from ST_AppTransVouchs  with(nolock)  where cTVCode  ='" + cTranRequestCode + "' and cInvCode='" + cinvcode + "'", connstring);

                                    if (dtcx.Rows.Count > 0)
                                    {
                                        iautoID = Convert.ToString(dtcx.Rows[0]["autoID"]);
                                        //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));
                                    }
                                    else
                                    {
                                        return "API错误:此单没有对应的调拨申请单,或者检查存货是否存在此调拨申请单";
                                    }
                                }

                                //iTaxUnitPrice = Public.GetNum(dtcx.Rows[0]["iTaxPrice"]);
                                //iSum = Public.GetNum(dtcx.Rows[0]["iSum"]);//原币含税金额

                                iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                                iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                                //    iUnitPrice = Public.GetNum(dtcx.Rows[0]["iUnitPrice"]);//原币无税单价
                                //    iMoney = Public.GetNum(dtcx.Rows[0]["iMoney"]);//原币无税金额
                                //    iTax = Public.GetNum(dtcx.Rows[0]["iTax"]);//原币税额

                                //    iNatUnitPrice = Public.GetNum(dtcx.Rows[0]["iNatUnitPrice"]);//本币无税单价
                                //    iNatSum = Public.GetNum(dtcx.Rows[0]["iNatSum"]);//本币价税合计
                                //    iNatMoney = Public.GetNum(dtcx.Rows[0]["iNatMoney"]);//本币无税金额
                                //    iNatTax = Public.GetNum(dtcx.Rows[0]["iNatTax"]);//本币税额
                                //  }

                                /****************************** 以下是必输字段 ****************************/
                                domBody[j]["autoid"] = "0"; //主关键字段，int类型
                                domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                                domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                                /***************************** 以下是非必输字段 ****************************/
                                if (ciwhcode == "06")
                                {
                                    domBody[j]["cinposcode"] = "06";//diaoru 
                                }
                                if (cowhcode == "02")
                                {
                                    domBody[j]["coutposcode"] = ClsSystem.gnvl(dts.Rows[j]["SUSR5"], ""); //调chu货位编码，string类型
                                }
                                if (cowhcode == "06")
                                {
                                    domBody[j]["coutposcode"] = "06";
                                }
                                domBody[j]["issodid"] = ""; //销售订单子表ID，string类型
                                domBody[j]["idsodid"] = ""; //目标销售订单子表ID，string类型
                                if (cType == "000")
                                {
                                    domBody[j]["itrids"] = iautoID; //调拨申请单子表ID，int类型
                                }
                                domBody[j]["cbarcode"] = ""; //条形码，string类型
                                domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                                domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                                domBody[j]["cinvname"] = ""; //存货名称，string类型
                                domBody[j]["cvenname"] = ""; //供应商，string类型
                                domBody[j]["imassdate"] = ""; //保质期，int类型
                                domBody[j]["cassunit"] = "";  //库存单位码，string类型
                                domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                                domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                                domBody[j]["cinvstd"] = ""; //规格型号，string类型
                                domBody[j]["cmassunit"] = ""; //保质期单位，int类型
                                domBody[j]["cdsocode"] = ""; //目标需求跟踪号，string类型
                                domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                                domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                                domBody[j]["cinposname"] = ""; //调入货位，string类型
                                
                                domBody[j]["coutposname"] = ""; //调出货位，string类型
                               // domBody[j]["coutposcode"] = ""; //调出货位编码，string类型
                                domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                                domBody[j]["cvmivenname"] = ""; //代管商，string类型
                                domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                                domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                                domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                                domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                                domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                                domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                                domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                                domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                                domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                                domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                                bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                                if (bInvBatch == 1)
                                {
                                    domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[j]["LOTTABLE02"]); //属性6，string类型 炉号
                                    domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[j]["LOTTABLE03"]);//属性7，string类型  母卷号
                                    domBody[j]["ctvbatch"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], ""); //批号，string类型
                                }
                                else
                                {
                                    domBody[j]["cbatchproperty7"] =""; //属性6，string类型 炉号
                                    domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号
                                    domBody[j]["ctvbatch"] = "";
                                }

                               

                                domBody[j]["itvnum"] = ""; //件数，double类型
                                domBody[j]["iinvexchrate"] = ""; //换算率，double类型
                                domBody[j]["itvquantity"] = iQuantity; //数量，double类型
                                domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                                domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                                domBody[j]["itvacost"] = iTaxUnitPrice; //单价，double类型
                                domBody[j]["csdemandmemo"] = ""; //需求分类代号说明，string类型
                                domBody[j]["cddemandmemo"] = ""; //目标需求分类代号说明，string类型
                                domBody[j]["comcode"] = ""; //委外订单号，string类型
                                domBody[j]["cmocode"] = ""; //生产订单号，string类型
                                domBody[j]["invcode"] = ""; //产品编码，string类型
                                domBody[j]["invname"] = ""; //产品，string类型
                                domBody[j]["imoseq"] = ""; //生产订单行号，string类型
                                domBody[j]["iomids"] = ""; //iomids，int类型
                                domBody[j]["imoids"] = ""; //imoids，int类型
                                domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                                domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                                domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                                domBody[j]["itvaprice"] = iSum; //金额，double类型 
                                domBody[j]["itvpcost"] = ""; //计划单价／售价，double类型
                                domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                                domBody[j]["issotype"] = ""; //需求跟踪方式，int类型
                                domBody[j]["idsoseq"] = ""; //目标需求跟踪行号，string类型
                                domBody[j]["idsotype"] = ""; //目标需求跟踪方式，int类型
                                domBody[j]["bcosting"] = ""; //是否核算，string类型
                                domBody[j]["itvpprice"] = ""; //计划金额／售价金额，double类型
                                domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                                domBody[j]["ddisdate"] = ""; //失效日期，DateTime类型
                                domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                                domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                                domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                                domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                                //    domBody[j]["cposition"] = ""; //货位编码，string类型
                                domBody[j]["creplaceitem"] = ""; //替换件，string类型
                                domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型

                                domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                                domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                                domBody[j]["rdsid"] = ""; //对应入库单id，int类型
                                domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                                domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                                domBody[j]["impoids"] = ""; //生产订单子表Id，int类型
                                domBody[j]["ctvcode"] = cTVCode; //单据号，string类型
                                domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                                domBody[j]["cdefine22"] = ""; //表体自定义项1，string类型
                                domBody[j]["cdefine28"] = ClsSystem.gnvl(dt.Rows[0]["SERIALKEY"], "") + ClsSystem.gnvl(dts.Rows[j]["SERIALKEY"], ""); //表体自定义项7，string类型
                                domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                                domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                                domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                                domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                                domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                                domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                                domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                                domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                                domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                                domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                                domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                                domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                                domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                                domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                                domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                                domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[j]["TOID"], "");  //表体自定义项3，string类型
                                domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                                domBody[j]["cdefine26"] = ClsSystem.gnvl(dts.Rows[j]["SUSR2"], "");  //表体自定义项5，double类型
                                domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                                domBody[j]["citemcode"] = ""; //项目编码，string类型
                                domBody[j]["cname"] = ""; //项目，string类型
                                domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                                domBody[j]["fsalecost"] = ""; //零售单价，double类型
                                domBody[j]["fsaleprice"] = ""; //零售金额，double类型
                                domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                                domBody[j]["igrossweight"] = ""; //毛重，string类型
                                domBody[j]["inetweight"] = ""; //净重，string类型
                           // }

                        }

                        //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
                        broker.AssignNormalValue("domPosition", "");

                        //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                        //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
                        broker.AssignNormalValue("cnnFrom", null);

                        //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
                        broker.AssignNormalValue("VouchId", "");

                        //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                        MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
                        broker.AssignNormalValue("domMsg", domMsg);

                        //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
                        broker.AssignNormalValue("bCheck", true);

                        //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                        broker.AssignNormalValue("bBeforCheckStock", true);

                        //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
                        broker.AssignNormalValue("bIsRedVouch", false);

                        //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
                        broker.AssignNormalValue("sAddedState", "");

                        //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
                        broker.AssignNormalValue("bReMote", false);



                        //第六步：调用API
                        if (!broker.Invoke())
                        {
                            //API处理
                            Exception apiEx = broker.GetException();
                            if (apiEx != null)
                            {
                                if (apiEx is MomSysException)
                                {
                                    MomSysException sysEx = apiEx as MomSysException;
                                    throw new Exception("系统异常：" + sysEx.Message);
                                    //todo:异常处理
                                }
                                else if (apiEx is MomBizException)
                                {
                                    MomBizException bizEx = apiEx as MomBizException;
                                    throw new Exception("API异常：" + bizEx.Message);
                                    //todo:异常处理
                                }
                                //异常原因
                                String exReason = broker.GetExceptionString();
                                if (exReason.Length != 0)
                                {
                                    throw new Exception("异常原因：" + exReason);
                                }
                            }
                            //结束本次调用，释放API资源
                            broker.Release();
                           // ado.RollbackTrans();
                            return "API错误：" + apiEx.ToString().Substring(0, 100);

                        }

                        //第七步：获取返回结果

                        //获取返回值
                        //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
                         System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


                        //获取out/inout参数值

                        //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                        System.String errMsgRet = broker.GetResult("errMsg") as System.String;

                        if (!result && !string.IsNullOrEmpty(errMsgRet))
                        {
                            broker.Release();
                             return "API错误：" + errMsgRet;
                        }

                        //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                        VouchNO = broker.GetResult("VouchId") as System.String;



                        //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                        MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

                        //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");

                        //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");

                        //int iFetch = -1;
                        //for (int i = 0; i < list.length; i++)
                        //{
                        //    IXMLDOMNode node = list[i].attributes.getNamedItem("isosid");
                        //    for (int j = iFetch + 1; j < dgvMain.Rows.Count; j++)
                        //    {
                        //        if (Convert.ToString(dgvMain.Rows[j].Cells["选择"].Value) == "Y")
                        //        {
                        //            dgvMain.Rows[j].Cells["iSOsID"].Value = Convert.ToString(node.nodeValue);
                        //            iFetch = j;
                        //            break;
                        //        }
                        //    }
                        //}


                        //结束本次调用，释放API资源
                        broker.Release();

                        #endregion

                        #region //审核调拨单
//                        //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
//                        envContext = new U8EnvContext();
//                        envContext.U8Login = u8Login;

//                        //第三步：设置API地址标识(Url)
//                        //当前API错误：审核单据的地址标识为：U8API/TransVouch/Audit
//                        myApiAddress = new U8ApiAddress("U8API/TransVouch/Audit");

//                        //第四步：构造APIBroker
//                        broker = new U8ApiBroker(myApiAddress, envContext);

//                        //第五步：API参数赋值

//                        //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：12
//                        broker.AssignNormalValue("sVouchType", "12");

//                        //给普通参数VouchId赋值。此参数的数据类型为System.String，此参数按值传递，表示单据Id
//                        broker.AssignNormalValue("VouchId", VouchIdRet);

//                        //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

//                        //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象：调用方控制事务时需要传入连接对象
//                        broker.AssignNormalValue("cnnFrom", conn);

//                        //给普通参数TimeStamp赋值。此参数的数据类型为System.Object，此参数按值传递，表示单据时间戳，用于检查单据是否修改，空串时不检查
//                        broker.AssignNormalValue("TimeStamp", null);

//                        //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
//                        domMsg = new MSXML2.DOMDocumentClass();
//                        broker.AssignNormalValue("domMsg", domMsg);

//                        //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量
//                        broker.AssignNormalValue("bCheck", true);

//                        //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
//                        broker.AssignNormalValue("bBeforCheckStock", true);

//                        //给普通参数bList赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示传入空串
//                        broker.AssignNormalValue("bList", true);

//                        //给普通参数MakeWheres赋值。此参数的数据类型为VBA.Collection，此参数按值传递，表示传空
//                        broker.AssignNormalValue("MakeWheres", null);

//                        //给普通参数sWebXml赋值。此参数的数据类型为System.String，此参数按值传递，表示传入空串
//                        broker.AssignNormalValue("sWebXml", "");

//                        //给普通参数oGenVouchIds赋值。此参数的数据类型为Scripting.IDictionary，此参数按值传递，表示返回审核时自动生成的单据的id列表,传空
//                        broker.AssignNormalValue("oGenVouchIds", null);

//                        //第六步：调用API
//                        if (!broker.Invoke())
//                        {
//                            //API处理
//                            Exception apiEx = broker.GetException();
//                            if (apiEx != null)
//                            {
//                                if (apiEx is MomSysException)
//                                {
//                                    MomSysException sysEx = apiEx as MomSysException;
//                                    throw new Exception("系统异常：" + sysEx.Message);
//                                    //todo:异常处理
//                                }
//                                else if (apiEx is MomBizException)
//                                {
//                                    MomBizException bizEx = apiEx as MomBizException;
//                                    throw new Exception("API异常：" + bizEx.Message);
//                                    //todo:异常处理
//                                }
//                                //异常原因
//                                String exReason = broker.GetExceptionString();
//                                if (exReason.Length != 0)
//                                {
//                                    throw new Exception("异常原因：" + exReason);
//                                }
//                            }
//                            //结束本次调用，释放API资源
//                            broker.Release();

//                        }

//                        //第七步：获取返回结果

//                        //获取返回值
//                        //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true,成功;false:失败
//                        result = Convert.ToBoolean(broker.GetReturnValue());

//                        //获取out/inout参数值

//                        //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
//                        errMsgRet = broker.GetResult("errMsg") as System.String;
//                        if (!result && !string.IsNullOrEmpty(errMsgRet))
//                        {
                            
//                            broker.Release();
//                            throw new Exception(errMsgRet);
//                        }

////                        DataTable dtRet = AdoAccess.ExecuteDT(@"select distinct a.ID 
////                                        from rdrecords09 a
////                                        join TransVouchs b on a.iTrIds = b.autoID
////                                        where b.ID = '" + VouchIdRet + @"'
////                                        union all
////                                        select distinct a.ID 
////                                        from rdrecords08 a
////                                        join TransVouchs b on a.iTrIds = b.autoID
////                                        where b.ID = '" + VouchIdRet + @"'
////                                        ", conn);
////                        //string ufts = "";

////                        string RKID = "";
////                        string CKID = "";
////                        if (dtRet.Rows.Count == 2)
////                        {
////                            //ufts = Convert.ToString(dtRet.Rows[0]["ufts"]);
////                            //dgvMain.Rows[iRow].Cells["cRKCode"].Value = Convert.ToString(dtRet.Rows[j]["cCode"]);
////                            CKID = Convert.ToString(dtRet.Rows[0]["ID"]);
////                            RKID = Convert.ToString(dtRet.Rows[1]["ID"]);
////                        }

//                        //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
//                        //MSXML2.IXMLDOMDocument2 domMsgRet = Convert.ToObject(broker.GetResult("domMsg"));

//                        //结束本次调用，释放API资源
//                        broker.Release();

                        #endregion

                        #region //审核出库单
                   //     //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                   //     envContext = new U8EnvContext();
                   //     envContext.U8Login = u8Login;

                   //     //第三步：设置API地址标识(Url)
                   //     //当前API错误：审核单据的地址标识为：U8API/otherout/Audit
                   //     myApiAddress = new U8ApiAddress("U8API/otherout/Audit");

                   //     //第四步：构造APIBroker
                   //     broker = new U8ApiBroker(myApiAddress, envContext);

                   //     //第五步：API参数赋值

                   //     //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：09
                   //     broker.AssignNormalValue("sVouchType", "09");

                   //     //给普通参数VouchId赋值。此参数的数据类型为System.String，此参数按值传递，表示单据Id
                   ////     broker.AssignNormalValue("VouchId", CKID);

                   //     //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                   //     //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象：调用方控制事务时需要传入连接对象
                   //     broker.AssignNormalValue("cnnFrom", conn);

                   //     //给普通参数TimeStamp赋值。此参数的数据类型为System.Object，此参数按值传递，表示单据时间戳，用于检查单据是否修改，空串时不检查
                   //     broker.AssignNormalValue("TimeStamp", null);

                   //     //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                   //     domMsg = new MSXML2.DOMDocumentClass();
                   //     broker.AssignNormalValue("domMsg", domMsg);

                   //     //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量
                   //     broker.AssignNormalValue("bCheck", true);

                   //     //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                   //     broker.AssignNormalValue("bBeforCheckStock", true);

                   //     //给普通参数bList赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示传入空串
                   //     broker.AssignNormalValue("bList", true);

                   //     //给普通参数MakeWheres赋值。此参数的数据类型为VBA.Collection，此参数按值传递，表示传空
                   //     broker.AssignNormalValue("MakeWheres", null);

                   //     //给普通参数sWebXml赋值。此参数的数据类型为System.String，此参数按值传递，表示传入空串
                   //     broker.AssignNormalValue("sWebXml", "");

                   //     //给普通参数oGenVouchIds赋值。此参数的数据类型为Scripting.IDictionary，此参数按值传递，表示返回审核时自动生成的单据的id列表,传空
                   //     broker.AssignNormalValue("oGenVouchIds", null);

                   //     //第六步：调用API
                   //     if (!broker.Invoke())
                   //     {
                   //         //API处理
                   //         Exception apiEx = broker.GetException();
                   //         if (apiEx != null)
                   //         {
                   //             if (apiEx is MomSysException)
                   //             {
                   //                 MomSysException sysEx = apiEx as MomSysException;
                   //                 throw new Exception("系统异常：" + sysEx.Message);
                   //                 //todo:异常处理
                   //             }
                   //             else if (apiEx is MomBizException)
                   //             {
                   //                 MomBizException bizEx = apiEx as MomBizException;
                   //                 throw new Exception("API异常：" + bizEx.Message);
                   //                 //todo:异常处理
                   //             }
                   //             //异常原因
                   //             String exReason = broker.GetExceptionString();
                   //             if (exReason.Length != 0)
                   //             {
                   //                 throw new Exception("异常原因：" + exReason);
                   //             }
                   //         }
                   //         //结束本次调用，释放API资源
                   //         broker.Release();

                   //     }

                   //     //第七步：获取返回结果

                   //     //获取返回值
                   //     //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true,成功;false:失败
                   //     result = Convert.ToBoolean(broker.GetReturnValue());

                   //     //获取out/inout参数值

                   //     //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                   //     errMsgRet = broker.GetResult("errMsg") as System.String;
                   //     if (!result && !string.IsNullOrEmpty(errMsgRet))
                   //     {
                   //         broker.Release();
                   //         throw new Exception(errMsgRet);
                   //     }

                   //     //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                   //     //MSXML2.IXMLDOMDocument2 domMsgRet = Convert.ToObject(broker.GetResult("domMsg"));

                   //     //结束本次调用，释放API资源
                   //     broker.Release();

                        #endregion

                        #region //审核入库单
                     //   //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                     //   envContext = new U8EnvContext();
                     //   envContext.U8Login = u8Login;

                     //   //第三步：设置API地址标识(Url)
                     //   //当前API错误：审核单据的地址标识为：U8API/otherout/Audit
                     //   myApiAddress = new U8ApiAddress("U8API/otherout/Audit");

                     //   //第四步：构造APIBroker
                     //   broker = new U8ApiBroker(myApiAddress, envContext);

                     //   //第五步：API参数赋值

                     //   //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：09
                     //   broker.AssignNormalValue("sVouchType", "08");

                     //   //给普通参数VouchId赋值。此参数的数据类型为System.String，此参数按值传递，表示单据Id
                     ////   broker.AssignNormalValue("VouchId", RKID);

                     //   //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                     //   //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象：调用方控制事务时需要传入连接对象
                     //   broker.AssignNormalValue("cnnFrom", conn);

                     //   //给普通参数TimeStamp赋值。此参数的数据类型为System.Object，此参数按值传递，表示单据时间戳，用于检查单据是否修改，空串时不检查
                     //   broker.AssignNormalValue("TimeStamp", null);

                     //   //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                     //   domMsg = new MSXML2.DOMDocumentClass();
                     //   broker.AssignNormalValue("domMsg", domMsg);

                     //   //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量
                     //   broker.AssignNormalValue("bCheck", true);

                     //   //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                     //   broker.AssignNormalValue("bBeforCheckStock", true);

                     //   //给普通参数bList赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示传入空串
                     //   broker.AssignNormalValue("bList", true);

                     //   //给普通参数MakeWheres赋值。此参数的数据类型为VBA.Collection，此参数按值传递，表示传空
                     //   broker.AssignNormalValue("MakeWheres", null);

                     //   //给普通参数sWebXml赋值。此参数的数据类型为System.String，此参数按值传递，表示传入空串
                     //   broker.AssignNormalValue("sWebXml", "");

                     //   //给普通参数oGenVouchIds赋值。此参数的数据类型为Scripting.IDictionary，此参数按值传递，表示返回审核时自动生成的单据的id列表,传空
                     //   broker.AssignNormalValue("oGenVouchIds", null);

                     //   //第六步：调用API
                     //   if (!broker.Invoke())
                     //   {
                     //       //API处理
                     //       Exception apiEx = broker.GetException();
                     //       if (apiEx != null)
                     //       {
                     //           if (apiEx is MomSysException)
                     //           {
                     //               MomSysException sysEx = apiEx as MomSysException;
                     //               throw new Exception("系统异常：" + sysEx.Message);
                     //               //todo:异常处理
                     //           }
                     //           else if (apiEx is MomBizException)
                     //           {
                     //               MomBizException bizEx = apiEx as MomBizException;
                     //               throw new Exception("API异常：" + bizEx.Message);
                     //               //todo:异常处理
                     //           }
                     //           //异常原因
                     //           String exReason = broker.GetExceptionString();
                     //           if (exReason.Length != 0)
                     //           {
                     //               throw new Exception("异常原因：" + exReason);
                     //           }
                     //       }
                     //       //结束本次调用，释放API资源
                     //       broker.Release();

                     //   }

                     //   //第七步：获取返回结果

                     //   //获取返回值
                     //   //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true,成功;false:失败
                     //   result = Convert.ToBoolean(broker.GetReturnValue());

                     //   //获取out/inout参数值

                     //   //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                     //   errMsgRet = broker.GetResult("errMsg") as System.String;
                     //   if (!result && !string.IsNullOrEmpty(errMsgRet))
                     //   {
                     //       broker.Release();
                     //       throw new Exception(errMsgRet);
                     //   }

                     //   //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                     //   //MSXML2.IXMLDOMDocument2 domMsgRet = Convert.ToObject(broker.GetResult("domMsg"));

                     //   //结束本次调用，释放API资源
                     //   broker.Release();

                        #endregion

                       // ado.CommitTrans();

                        return VouchNO;
                    }
                    catch (Exception ex)
                    {
                        //ado.RollbackTrans();
                        string sErr = ex.Message.Replace("'", "''");
                        if (sErr.Length > 50)
                            sErr = "API错误：" + sErr.Substring(0, 50);
                        if (sErr.IndexOf("错误") > 0)
                        {
                        }
                        else
                        {
                            sErr = "API错误:" + sErr;
                        }
                        return sErr;
                     

                    }
                    finally
                    {
                       
                    }
               // }

                 

        }

        //出库调拨单
        public static string POSDBDDR_CK(U8Login.clsLogin u8Login, string connstring, DataTable dHead, DataTable dBody, DataTable dpicks, DataTable dFb, string cType)
        {
            string VouchNO = "";
            ADODB.Connection ado = new ADODB.Connection();
            string sMsg = "";
            string cTVCode = "";

            //DataTable dts1 = null;

            //DataView rowfilter = new DataView(dtst);
            //rowfilter.RowFilter = "SUSR1= '" + cvoucode + "'";
            //rowfilter.RowStateFilter = DataViewRowState.CurrentRows;
            //dts1 = rowfilter.ToTable();

            try
            {

                //string cTranRequestCode = Convert.ToString(dt.Rows[0]["EXTERNRECEIPTKEY"]);  //wms外部单号作为调拨申请单传入

                DataTable dtcx = new DataTable();

                #region 调拨单

                //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
                U8EnvContext envContext = new U8EnvContext();
                envContext.U8Login = u8Login;
                //ado.Open(u8Login.UfDbName, "sa", Program.dataPwd, 0);
                //ado.BeginTrans();


                //第三步：设置API地址标识(Url)
                //当前API错误：添加新单据的地址标识为：U8API/TransVouch/Add
                U8ApiAddress myApiAddress = new U8ApiAddress("U8API/TransVouch/Add");

                //第四步：构造APIBroker
                U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

                //第五步：API参数赋值

                //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：12
                broker.AssignNormalValue("sVouchType", "12");

                //给BO表头参数DomHead赋值，此BO参数的业务类型为调拨单，属表头参数。BO参数均按引用传递
                //提示：给BO表头参数DomHead赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject DomHead = broker.GetBoParam("DomHead");
                DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
                //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                cTVCode = Public.GetParentCode("0304", "", connstring); //GetParentCode();

                /****************************** 以下是必输字段 ****************************/
                DomHead[0]["id"] = "0"; //主关键字段，int类型
             

                    DomHead[0]["ctvcode"] = cTVCode; //单据号，string类型

                    string cowhcode = "";
                    string ciwhcode = "";
                    //if (cType.Length < 4)
                    //{
                    //    cowhcode = ClsSystem.gnvl(dt.Rows[0]["SUSR1"], "");
                    //    // string cowhcode = ClsSystem.gnvl(dt.Tables[0].Rows[0]["SUSR1"], "");// Public.GetStr(",", cwhcode, 1);
                    //    //string ciwhcode = ClsSystem.gnvl(dts.Rows[0]["TOLOC"], "");// Public.GetStr(",", cwhcode, 0);
                    //    ciwhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where wmsbm='" + ClsSystem.gnvl(dts1.Rows[0]["TOLOC"], "") + "'", connstring), "");
                    //}
                    //else if (cType.Length > 3)
                    //{
                     ciwhcode= ClsSystem.gnvl(dHead.Rows[0]["SUSR1"], "");

                    //ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where wmsbm='" + ClsSystem.gnvl(dt.Tables[0].Rows[0]["SUSR1"], "") + "'", connstring), "");
                    // string cowhcode = ClsSystem.gnvl(dt.Tables[0].Rows[0]["SUSR1"], "");// Public.GetStr(",", cwhcode, 1);
                    //string ciwhcode = ClsSystem.gnvl(dts.Rows[0]["TOLOC"], "");// Public.GetStr(",", cwhcode, 0);
                     cowhcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cwhcode from U8_Cus_TN_WareHoude  with(nolock)  where  isnull(isjc,0)=0 and  wmsbm='" + ClsSystem.gnvl(dFb.Rows[0]["LOC"], "") + "'", connstring), "");
                    //  }

                    string codepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + cowhcode + "'", connstring), "");
                    string cidepcode = "";
                    cidepcode = ClsSystem.gnvl(dHead.Rows[0]["SUSR2"], "");
                    if (cidepcode == "")
                    {
                        cidepcode = ClsSystem.gnvl(SqlAccess.ExecuteScalar("select cdepcode from U8_Cus_TN_WareHoude  with(nolock)  where cwhcode='" + ciwhcode + "'", connstring), "");
                    }


                    string rq1 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["EDITDATE"], ""), 1);
                 //   string rq2 = Public.GetStr(" ", ClsSystem.gnvl(dHead.Rows[0]["RECEIPTDATE"], ""), 0);
                rq1 = rq1.Insert(4, "/");
                rq1 = rq1.Insert(7, "/");
                DomHead[0]["dtvdate"] = Convert.ToDateTime(rq1).ToShortDateString(); //日期，DateTime类型
                DomHead[0]["cwhname"] = ""; //转出仓库，string类型
                DomHead[0]["cwhname_1"] = ""; //转入仓库，string类型

                /***************************** 以下是非必输字段 ****************************/
                DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
                DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
                DomHead[0]["dnmaketime"] = DateTime.Now.ToString(); //制单时间，DateTime类型
                DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
                DomHead[0]["dnverifytime"] = ""; //审核时间，DateTime类型
                DomHead[0]["ctranrequestcode"] = ""; //调拨申请单号，string类型  cTranRequestCode
                DomHead[0]["cdepname_1"] = ""; //转出部门，string类型
                DomHead[0]["cdepname"] = ""; //转入部门，string类型
                DomHead[0]["crdname_1"] = ""; //出库类别，string类型
                DomHead[0]["crdname"] = ""; //入库类别，string类型
                DomHead[0]["cpersonname"] = ""; //经手人，string类型
                DomHead[0]["dverifydate"] = ""; //审核日期，DateTime类型
                DomHead[0]["ctvmemo"] = ClsSystem.gnvl(dHead.Rows[0]["ORDERKEY"], ""); //备注，string类型
                DomHead[0]["parentscrp"] = ""; //母件损耗率(％)，double类型
                DomHead[0]["csource"] = "1"; //单据来源，int类型
                DomHead[0]["iamount"] = ""; //现存量，string类型
                DomHead[0]["cmaker"] = ClsSystem.gnvl(dHead.Rows[0]["EDITWHO"], ""); //制单人，string类型
                DomHead[0]["cverifyperson"] = ""; //审核人，string类型
                DomHead[0]["cfree1"] = ""; //自由项1，string类型
                DomHead[0]["cfree2"] =""; //自由项2，string类型
                DomHead[0]["cfree3"] = ""; //自由项3，string类型
                DomHead[0]["cfree4"] = ""; //自由项4，string类型
                DomHead[0]["cfree5"] = ""; //自由项5，string类型
                DomHead[0]["cfree6"] = ""; //自由项6，string类型
                DomHead[0]["cfree7"] = ""; //自由项7，string类型
                DomHead[0]["cfree8"] = ""; //自由项8，string类型
                DomHead[0]["cfree9"] = ""; //自由项9，string类型
                DomHead[0]["cfree10"] = ""; //自由项10，string类型
                DomHead[0]["ufts"] = ""; //时间戳，string类型
                DomHead[0]["codepcode"] = codepcode; //转出部门编码，string类型
                DomHead[0]["cidepcode"] = cidepcode; //转入部门编码，string类型
                DomHead[0]["cpersoncode"] = ""; //经手人编码，string类型
                DomHead[0]["cordcode"] = "204"; //出库类别编码，string类型
                DomHead[0]["cirdcode"] = "104"; //入库类别编码，string类型

                // string cwhcode = ClsSystem.gnvl(dt.Tables[0].Rows[0]["SUSR1"], "");------------需要判断是入库还是出库从而决定仓库
            

                DomHead[0]["ciwhcode"] = ciwhcode; //转入仓库编码，string类型
                DomHead[0]["cowhcode"] = cowhcode; //转出仓库编码，string类型

                DomHead[0]["cmpocode"] = ""; //订单号，string类型
                DomHead[0]["cpspcode"] = ""; //产品结构，string类型
                DomHead[0]["btransflag"] = ""; //是否传递，string类型
                DomHead[0]["vt_id"] = "89"; //模版号，int类型
                DomHead[0]["iquantity"] = ""; //产量，double类型
                DomHead[0]["iavaquantity"] = ""; //可用量，string类型
                DomHead[0]["iavanum"] = ""; //可用件数，string类型
                DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
                DomHead[0]["iproorderid"] = ""; //生产订单ID，string类型
                DomHead[0]["cversion"] = ""; //版本号／替代标识，string类型
                DomHead[0]["bomid"] = ""; //bomid，string类型
                DomHead[0]["cordertype"] = ""; //订单类型，string类型
                DomHead[0]["cinvname"] = ""; //产品名称，string类型
                DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
                DomHead[0]["itopsum"] = ""; //最高库存量，string类型
                DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
                DomHead[0]["isafesum"] = ""; //安全库存量，string类型
                DomHead[0]["caccounter"] = ""; //记账人，string类型
                DomHead[0]["ipresent"] = ""; //现存量，string类型
                DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
                DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
                DomHead[0]["cdefine3"] = ""; //表头自定义项3，string类型
                DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
                DomHead[0]["itransflag"] = "正向"; //调拨方向，int类型
                DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
                DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
                DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
                DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
                DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
                DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
                DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
                DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
                DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
                DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
                DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型


                //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
                //提示：给BO表体参数domBody赋值有两种方法

                //方法一是直接传入MSXML2.DOMDocumentClass对象
                //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

                //方法二是构造BusinessObject对象，具体方法如下：
                BusinessObject domBody = broker.GetBoParam("domBody");



                if (dBody.Rows.Count * dFb.Rows.Count < 1)
                {
                    return "API错误：表体无数据";
                }

                domBody.RowCount = dBody.Rows.Count * dFb.Rows.Count; //设置BO对象行数
                //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
                //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
                //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

                decimal iTaxUnitPrice = 0;
                decimal iSum = 0;
                string cinvcode = "";
                int bInvBatch = 0;
                  string cPdID = "";
                  int j = 0;
                  for (int k = 0; k < dBody.Rows.Count; k++)
                  {
                      cinvcode = ClsSystem.gnvl(dBody.Rows[k]["SKU"], "");
                      cPdID = ClsSystem.gnvl(dBody.Rows[k]["ShipmentOrderDetail_ID"], "");



                      DataView rowfilter2 = new DataView(dpicks);
                      rowfilter2.RowFilter = "ShipmentOrderDetail_ID =" + cPdID;
                      rowfilter2.RowStateFilter = DataViewRowState.CurrentRows;
                      DataTable dpick = rowfilter2.ToTable();

                      if (dpick.Rows.Count > 0)
                      {

                          string cpicksid = ClsSystem.gnvl(dpick.Rows[0]["PickDetails_ID"], "");

                          DataView rowfilter1 = new DataView(dFb);
                          rowfilter1.RowFilter = "PickDetails_ID =" + cpicksid;
                          rowfilter1.RowStateFilter = DataViewRowState.CurrentRows;
                          DataTable dts = rowfilter1.ToTable();

                          for (int i = 0; i < dts.Rows.Count; i++)
                          {

                              decimal iQuantity = Public.GetNum(dts.Rows[i]["QTY"]);//数量

                              //dtcx = SqlAccess.ExecuteSqlDataTable("select top 1 d.ID,d.iTaxPrice ,d.iSum   from PO_Podetails d with(nolock) join PO_Pomain m with(nolock) on d.POID=m.POID  where m.cPOID ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring);

                              //if (dtcx.Rows.Count > 0)
                              //{
                              //    iPOsID = Convert.ToString(dtcx.Rows[0]["ID"]);
                              //    //    iarrsid = Convert.ToString(SqlAccess.ExecuteScalar("select max(Autoid)  from pu_arrivalvouchs d with(nolock) join pu_arrivalvouch m with(nolock) on d.ID =m.ID   where m.cCode  ='" + cCode + "' and d.cInvCode='" + cinvcode + "'", connstring));

                              //iTaxUnitPrice = Public.GetNum(dtcx.Rows[0]["iTaxPrice"]);
                              //iSum = Public.GetNum(dtcx.Rows[0]["iSum"]);//原币含税金额

                              iTaxUnitPrice = Convert.ToDecimal(SqlAccess.ExecuteScalar("select  isnull(iInvRCost ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring)); //原币含税单价
                              iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
                              //    iUnitPrice = Public.GetNum(dtcx.Rows[0]["iUnitPrice"]);//原币无税单价
                              //    iMoney = Public.GetNum(dtcx.Rows[0]["iMoney"]);//原币无税金额
                              //    iTax = Public.GetNum(dtcx.Rows[0]["iTax"]);//原币税额

                              //    iNatUnitPrice = Public.GetNum(dtcx.Rows[0]["iNatUnitPrice"]);//本币无税单价
                              //    iNatSum = Public.GetNum(dtcx.Rows[0]["iNatSum"]);//本币价税合计
                              //    iNatMoney = Public.GetNum(dtcx.Rows[0]["iNatMoney"]);//本币无税金额
                              //    iNatTax = Public.GetNum(dtcx.Rows[0]["iNatTax"]);//本币税额
                              //  }

                              /****************************** 以下是必输字段 ****************************/
                              domBody[j]["autoid"] = "0"; //主关键字段，int类型
                              domBody[j]["cinvcode"] = cinvcode; //存货编码，string类型
                              domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型

                              /***************************** 以下是非必输字段 ****************************/
                              if (cowhcode == "06")
                              {
                                  domBody[j]["coutposcode"] = "06";
                              }
                              else
                              {
                                  domBody[j]["coutposcode"] = "";
                              }
                              if (ciwhcode == "06")
                              {
                                  domBody[j]["cinposcode"] = "06";
                              }
                              domBody[j]["issodid"] = ""; //销售订单子表ID，string类型
                              domBody[j]["idsodid"] = ""; //目标销售订单子表ID，string类型
                              domBody[j]["itrids"] = ""; //调拨申请单子表ID，int类型
                              domBody[j]["cbarcode"] = ""; //条形码，string类型
                              domBody[j]["cbvencode"] = ""; //供应商编码，string类型
                              domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
                              domBody[j]["cinvname"] = ""; //存货名称，string类型
                              domBody[j]["cvenname"] = ""; //供应商，string类型
                              domBody[j]["imassdate"] = ""; //保质期，int类型
                              domBody[j]["cassunit"] = "";  //库存单位码，string类型
                              domBody[j]["dmadedate"] = ""; //生产日期，DateTime类型
                              domBody[j]["corufts"] = ""; //对应单据时间戳，string类型
                              domBody[j]["cinvstd"] = ""; //规格型号，string类型
                              domBody[j]["cmassunit"] = ""; //保质期单位，int类型
                              domBody[j]["cdsocode"] = ""; //目标需求跟踪号，string类型
                              domBody[j]["csocode"] = ""; //需求跟踪号，string类型
                              domBody[j]["cinvm_unit"] = ""; //主计量单位，string类型
                              //domBody[j]["cinposname"] = ""; //调入货位，string类型
                              //domBody[j]["cinposcode"] = ""; //调入货位编码，string类型
                              //domBody[j]["coutposname"] = ""; //调出货位，string类型
                              //domBody[j]["coutposcode"] = ""; //调出货位编码，string类型
                              domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
                              domBody[j]["cvmivenname"] = ""; //代管商，string类型
                              domBody[j]["cfree1"] = ""; //存货自由项1，string类型
                              domBody[j]["cfree3"] = ""; //存货自由项3，string类型
                              domBody[j]["cfree4"] = ""; //存货自由项4，string类型
                              domBody[j]["cfree5"] = ""; //存货自由项5，string类型
                              domBody[j]["cfree6"] = ""; //存货自由项6，string类型
                              domBody[j]["cfree7"] = ""; //存货自由项7，string类型
                              domBody[j]["cfree8"] = ""; //存货自由项8，string类型
                              domBody[j]["cfree9"] = ""; //存货自由项9，string类型
                              domBody[j]["cfree10"] = ""; //存货自由项10，string类型
                              domBody[j]["cfree2"] = ""; //存货自由项2，string类型
                              bInvBatch = Convert.ToInt16(SqlAccess.ExecuteScalar("select  isnull(bInvBatch ,0)  from Inventory a with(nolock)  where cinvcode='" + cinvcode + "'", connstring));
                              if (bInvBatch == 1)
                              {
                                  domBody[j]["cbatchproperty7"] = Convert.ToString(dts.Rows[i]["LOTTABLE02"]); //属性6，string类型 炉号

                                  domBody[j]["cbatchproperty6"] = Convert.ToString(dts.Rows[i]["LOTTABLE03"]);//属性7，string类型  母卷号

                                  domBody[j]["ctvbatch"] = ClsSystem.gnvl(dts.Rows[i]["ID"], ""); //批号，string类型
                              }
                              else
                              {
                                  domBody[j]["cbatchproperty7"] = ""; //属性6，string类型 炉号

                                  domBody[j]["cbatchproperty6"] = "";//属性7，string类型  母卷号

                                  domBody[j]["ctvbatch"] = "";
                              }

                           
                              domBody[j]["itvnum"] = ""; //件数，double类型
                              domBody[j]["iinvexchrate"] = ""; //换算率，double类型
                              domBody[j]["itvquantity"] = iQuantity; //数量，double类型
                              domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
                              domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
                              domBody[j]["itvacost"] = iTaxUnitPrice; //单价，double类型
                              domBody[j]["csdemandmemo"] = ""; //需求分类代号说明，string类型
                              domBody[j]["cddemandmemo"] = ""; //目标需求分类代号说明，string类型
                              domBody[j]["comcode"] = ""; //委外订单号，string类型
                              domBody[j]["cmocode"] = ""; //生产订单号，string类型
                              domBody[j]["invcode"] = ""; //产品编码，string类型
                              domBody[j]["invname"] = ""; //产品，string类型
                              domBody[j]["imoseq"] = ""; //生产订单行号，string类型
                              domBody[j]["iomids"] = ""; //iomids，int类型
                              domBody[j]["imoids"] = ""; //imoids，int类型
                              domBody[j]["iexpiratdatecalcu"] = ""; //有效期推算方式，int类型
                              domBody[j]["cexpirationdate"] = ""; //有效期至，string类型
                              domBody[j]["dexpirationdate"] = ""; //有效期计算项，string类型
                              domBody[j]["itvaprice"] = iSum; //金额，double类型 
                              domBody[j]["itvpcost"] = ""; //计划单价／售价，double类型
                              domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
                              domBody[j]["issotype"] = ""; //需求跟踪方式，int类型
                              domBody[j]["idsoseq"] = ""; //目标需求跟踪行号，string类型
                              domBody[j]["idsotype"] = ""; //目标需求跟踪方式，int类型
                              domBody[j]["bcosting"] = ""; //是否核算，string类型
                              domBody[j]["itvpprice"] = ""; //计划金额／售价金额，double类型
                              domBody[j]["cinva_unit"] = ""; //库存单位，string类型
                              domBody[j]["ddisdate"] = ""; //失效日期，DateTime类型
                              domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
                              domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
                              domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
                              domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
                              //    domBody[j]["cposition"] = ""; //货位编码，string类型
                              domBody[j]["creplaceitem"] = ""; //替换件，string类型
                              domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型

                              domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
                              domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
                              domBody[j]["rdsid"] = ""; //对应入库单id，int类型
                              domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
                              domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
                              domBody[j]["impoids"] = ""; //生产订单子表Id，int类型
                              domBody[j]["ctvcode"] = cTVCode; //单据号，string类型
                              domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
                              domBody[j]["cdefine22"] = ""; //表体自定义项1，string类型
                              domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
                              domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
                              domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
                              domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
                              domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
                              domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
                              domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
                              domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
                              domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
                              domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
                              domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
                              domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
                              domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
                              domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
                              domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
                              domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
                              domBody[j]["cdefine24"] = ClsSystem.gnvl(dts.Rows[i]["ID"], "");  //表体自定义项3，string类型
                              domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
                              domBody[j]["cdefine26"] = ClsSystem.gnvl(dBody.Rows[k]["SUSR2"], ""); //表体自定义项5，double类型
                              domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
                              domBody[j]["citemcode"] = ""; //项目编码，string类型
                              domBody[j]["cname"] = ""; //项目，string类型
                              domBody[j]["citem_class"] = ""; //项目大类编码，string类型
                              domBody[j]["fsalecost"] = ""; //零售单价，double类型
                              domBody[j]["fsaleprice"] = ""; //零售金额，double类型
                              domBody[j]["citemcname"] = ""; //项目大类名称，string类型
                              domBody[j]["igrossweight"] = ""; //毛重，string类型
                              domBody[j]["inetweight"] = ""; //净重，string类型

                              j++;
                          }
                      }

                  }

                //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
                broker.AssignNormalValue("domPosition", "");

                //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

                //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
                broker.AssignNormalValue("cnnFrom", null);

                //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
                broker.AssignNormalValue("VouchId", "");

                //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
                MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
                broker.AssignNormalValue("domMsg", domMsg);

                //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
                broker.AssignNormalValue("bCheck", true);

                //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
                broker.AssignNormalValue("bBeforCheckStock", true);

                //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
                broker.AssignNormalValue("bIsRedVouch", false);

                //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
                broker.AssignNormalValue("sAddedState", "");

                //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
                broker.AssignNormalValue("bReMote", false);



                //第六步：调用API
                if (!broker.Invoke())
                {
                    //API处理
                    Exception apiEx = broker.GetException();
                    if (apiEx != null)
                    {
                        if (apiEx is MomSysException)
                        {
                            MomSysException sysEx = apiEx as MomSysException;
                            throw new Exception("系统异常：" + sysEx.Message);
                            //todo:异常处理
                        }
                        else if (apiEx is MomBizException)
                        {
                            MomBizException bizEx = apiEx as MomBizException;
                            throw new Exception("API异常：" + bizEx.Message);
                            //todo:异常处理
                        }
                        //异常原因
                        String exReason = broker.GetExceptionString();
                        if (exReason.Length != 0)
                        {
                            throw new Exception("异常原因：" + exReason);
                        }
                    }
                    //结束本次调用，释放API资源
                    broker.Release();
                   // ado.RollbackTrans();
                    return "API错误：" + apiEx.ToString().Substring(0, 100);

                }

                //第七步：获取返回结果

                //获取返回值
                //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
                System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


                //获取out/inout参数值

                //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                System.String errMsgRet = broker.GetResult("errMsg") as System.String;

                if (!result && !string.IsNullOrEmpty(errMsgRet))
                {
                    broker.Release();
                    return "API错误：" + errMsgRet;
                }

                //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
                VouchNO = broker.GetResult("VouchId") as System.String;



                //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
                MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

                //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");

                //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");

                //int iFetch = -1;
                //for (int i = 0; i < list.length; i++)
                //{
                //    IXMLDOMNode node = list[i].attributes.getNamedItem("isosid");
                //    for (int j = iFetch + 1; j < dgvMain.Rows.Count; j++)
                //    {
                //        if (Convert.ToString(dgvMain.Rows[j].Cells["选择"].Value) == "Y")
                //        {
                //            dgvMain.Rows[j].Cells["iSOsID"].Value = Convert.ToString(node.nodeValue);
                //            iFetch = j;
                //            break;
                //        }
                //    }
                //}


                //结束本次调用，释放API资源
                broker.Release();

                #endregion

              

              //  ado.CommitTrans();

                return VouchNO;
            }
            catch (Exception ex)
            {
               // ado.RollbackTrans();
                string sErr = ex.Message.Replace("'", "''");
                if (sErr.Length > 50)
                    sErr = "API错误：" + sErr.Substring(0, 50);
                if (sErr.IndexOf("错误") > 0)
                {
                }
                else
                {
                    sErr = "API错误:" + sErr;
                }
                return sErr;


            }
            finally
            {

            }
            // }



        }

        public static U8Login.clsLogin GetU8Login()
        {
            U8Login.clsLogin u8Login = new U8Login.clsLogin();
            String sSubId = "AS";
            String sAccID = Program.cacc_id;
            String sYear = DateTime.Today.Year.ToString();
            String sUserID = Program.userCode;
            String sPassword = Program.userPwd;
            String sDate = DateTime.Today.ToShortDateString();
            String sServer = Program.server;
            String sSerial = "";
            
            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial))
            {
                MessageBox.Show("登陆帐套" + sAccID + "失败，原因：" + u8Login.ShareString);
                Marshal.FinalReleaseComObject(u8Login);
                return null;
            }
            return u8Login;
        }

    } 
}
