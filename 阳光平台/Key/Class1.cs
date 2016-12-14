using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Key
{
    class Class1
    {

        #region 采购入库单  测试

        //           U8Login.clsLogin u8Login = new U8Login.clsLogin();
        //           String sSubId = "SA";
        //           String sAccID = "(default)@003";
        //           String sYear = "2013";
        //           String sUserID = "demo";
        //           String sPassword = "1";
        //           String sDate = "2013-4-18";
        //           String sServer = "localhost";
        //           String sSerial = "";
        //           if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate, ref sServer, ref sSerial))
        //           {
        //               Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
        //               Marshal.FinalReleaseComObject(u8Login);
        //               return "";
        //           }
        //           //第二步：构造环境上下文对象，传入login，并按需设置其它上下文参数
        //           U8EnvContext envContext = new U8EnvContext();
        //           envContext.U8Login = u8Login;

        //           //销售所有接口均支持内部独立事务和外部事务，默认内部事务
        //           //如果是外部事务，则需要传递ADO.Connection对象，并将IsIndependenceTransaction属性设置为false
        //           //envContext.BizDbConnection = conn;
        //           //envContext.IsIndependenceTransaction = false;

        //           //第三步：设置API地址标识(Url)
        //           //当前API：添加新单据的地址标识为：U8API/PuStoreIn/Add
        //           U8ApiAddress myApiAddress = new U8ApiAddress("U8API/PuStoreIn/Add");

        //           //第四步：构造APIBroker
        //           U8ApiBroker broker = new U8ApiBroker(myApiAddress, envContext);

        //           //第五步：API参数赋值

        //           //给普通参数sVouchType赋值。此参数的数据类型为System.String，此参数按值传递，表示单据类型：01
        //           broker.AssignNormalValue("sVouchType", "01");

        //           //给BO表头参数DomHead赋值，此BO参数的业务类型为采购入库单，属表头参数。BO参数均按引用传递
        //           //提示：给BO表头参数DomHead赋值有两种方法

        //           //方法一是直接传入MSXML2.DOMDocumentClass对象
        //           //broker.AssignNormalValue("DomHead", new MSXML2.DOMDocumentClass())

        //           //方法二是构造BusinessObject对象，具体方法如下：
        //           BusinessObject DomHead = broker.GetBoParam("DomHead");
        //           DomHead.RowCount = 1; //设置BO对象(表头)行数，只能为一行
        //           //给BO对象(表头)的字段赋值，值可以是真实类型，也可以是无类型字符串
        //           //以下代码示例只设置第一行值。各字段定义详见API服务接口定义

        //           /****************************** 以下是必输字段 ****************************/
        //           DomHead[0]["id"] = "0"; //主关键字段，int类型
        //           DomHead[0]["bomfirst"] = "0"; //委外期初标志，string类型
        //           DomHead[0]["ccode"] = "0000000002"; //入库单号，string类型
        //           DomHead[0]["ddate"] = "2013-04-22"; //Convert.ToString(dt.Tables[0].Rows[0]["dDate"]); //入库日期，DateTime类型
        //           DomHead[0]["iverifystate"] = "0"; //iverifystate，int类型
        //           DomHead[0]["iswfcontrolled"] = "0"; //iswfcontrolled，int类型
        //      //     DomHead[0]["cvenabbname"] = "上海用友"; //供货单位，string类型
        //           DomHead[0]["cbustype"] = "普通采购"; //业务类型，int类型
        //           DomHead[0]["cmaker"] = "demo"; //Convert.ToString(dt.Tables[0].Rows[0]["cMaker"]); //制单人，string类型
        //           DomHead[0]["iexchrate"] = "1"; //汇率，double类型
        //           DomHead[0]["cexch_name"] ="人民币"; //币种，string类型
        //      //     DomHead[0]["ufts"] = "2013-04-22"; //Convert.ToString(dt.Rows[0]["RKufts"]); //时间戳，string类型
        //           DomHead[0]["bpufirst"] = "0"; //采购期初标志，string类型
        //           DomHead[0]["cvencode"] = "001";// Convert.ToString(dt.Tables[0].Rows[0]["cVenCode"]); //供货单位编码，string类型
        //           DomHead[0]["cvouchtype"] = "01"; //单据类型，string类型
        //           DomHead[0]["cwhcode"] = "01";// Convert.ToString(dt.Tables[0].Rows[0]["cWhCode"]); //仓库编码，string类型
        //           DomHead[0]["brdflag"] = "1"; //收发标志，int类型
        //           DomHead[0]["csource"] = "库存"; //单据来源，int类型

        //           /***************************** 以下是非必输字段 ****************************/

        //           //DomHead[0]["cmodifyperson"] = ""; //修改人，string类型
        //           //DomHead[0]["dmodifydate"] = ""; //修改日期，DateTime类型
        //            DomHead[0]["dnmaketime"] = "2013-04-22";// Convert.ToString(dt.Tables[0].Rows[0]["dnmaketime"]); //制单时间，DateTime类型
        //           //DomHead[0]["dnmodifytime"] = ""; //修改时间，DateTime类型
        //    //       DomHead[0]["dnverifytime"] = Convert.ToString(dt.Tables[0].Rows[0]["dnverifytime"]); //审核时间，DateTime类型
        //   //        DomHead[0]["cwhname"] = "测试仓"; //仓库，string类型
        //   //        DomHead[0]["cordercode"] = Convert.ToString(dt.Tables[0].Rows[0]["cPOID"]); //订单号，string类型
        //           //DomHead[0]["carvcode"] = ""; //到货单号，string类型
        //           DomHead[0]["ireturncount"] = "0"; //ireturncount，string类型
        //           //DomHead[0]["cbuscode"] = ""; //业务号，string类型
        //           //DomHead[0]["cdepname"] = ""; //部门，string类型
        //           //DomHead[0]["cpersonname"] = ""; //业务员，string类型
        //           //DomHead[0]["darvdate"] = ""; //到货日期，DateTime类型
        //           //DomHead[0]["cptname"] = ""; //采购类型，string类型
        //           //DomHead[0]["crdname"] = ""; //入库类别，string类型
        //  //         DomHead[0]["dveridate"] = Convert.ToString(dt.Tables[0].Rows[0]["dVeriDate"]); //审核日期，DateTime类型
        //           //DomHead[0]["cvenpuomprotocol"] = ""; //收付款协议编码，string类型
        //           //DomHead[0]["cvenpuomprotocolname"] = ""; //收付款协议名称，string类型
        //           //DomHead[0]["dcreditstart"] = ""; //立账日，DateTime类型
        //           //DomHead[0]["icreditperiod"] = ""; //账期，int类型
        //           //DomHead[0]["dgatheringdate"] = ""; //到期日，DateTime类型
        // //          DomHead[0]["bcredit"] = "0"; //是否为立账单据，int类型
        //           //DomHead[0]["cvenfullname"] = ""; //供应商全称，string类型
        //           //DomHead[0]["cmemo"] = ""; //备注，string类型
        //   //        DomHead[0]["chandler"] = Convert.ToString(dt.Tables[0].Rows[0]["cHandler"]); //审核人，string类型
        //           //DomHead[0]["caccounter"] = ""; //记账人，string类型
        //           //DomHead[0]["ipresent"] = ""; //现存量，string类型
        //           DomHead[0]["itaxrate"] = "17"; //税率，double类型
        //           //DomHead[0]["isalebillid"] = ""; //发票号，string类型
        //  //         DomHead[0]["ipurorderid"] = Convert.ToString(dt.Tables[0].Rows[0]["POID"]); //采购订单ID，string类型
        //           //DomHead[0]["ipurarriveid"] = ""; //采购到货单ID，string类型
        //           //DomHead[0]["iarriveid"] = ""; //到货单ID，string类型
        //           //DomHead[0]["dchkdate"] = ""; //检验日期，DateTime类型
        //           //DomHead[0]["iavaquantity"] = ""; //可用量，string类型
        //           //DomHead[0]["iavanum"] = ""; //可用件数，string类型
        //           //DomHead[0]["ipresentnum"] = ""; //现存件数，string类型
        //           //DomHead[0]["gspcheck"] = ""; //gsp复核标志，string类型
        //           //DomHead[0]["cchkperson"] = ""; //检验员，string类型
        //           //DomHead[0]["cchkcode"] = ""; //检验单号，string类型
        //           DomHead[0]["vt_id"] = "27"; //模版号，int类型
        //           //DomHead[0]["cdepcode"] = ""; //部门编码，string类型
        //           //DomHead[0]["cbillcode"] = ""; //发票id，string类型
        //           //DomHead[0]["cptcode"] = ""; //采购类型编码，string类型
        //           //DomHead[0]["cpersoncode"] = ""; //业务员编码，string类型
        //           //DomHead[0]["crdcode"] = ""; //入库类别编码，string类型
        //           //DomHead[0]["isafesum"] = ""; //安全库存量，string类型
        //           //DomHead[0]["ilowsum"] = ""; //最低库存量，string类型
        //           //DomHead[0]["cdefine16"] = ""; //表头自定义项16，double类型
        //           //DomHead[0]["itopsum"] = ""; //最高库存量，string类型
        //           //DomHead[0]["iproorderid"] = ""; //生产订单Id，string类型
        //  //         DomHead[0]["bisstqc"] = "0"; //库存期初标记，string类型
        //           //DomHead[0]["cdefine1"] = ""; //表头自定义项1，string类型
        //           //DomHead[0]["cdefine2"] = ""; //表头自定义项2，string类型
        //           //DomHead[0]["cdefine3"] = Convert.ToString(dt.Rows[i]["cCode"]); //表头自定义项3，string类型
        //           //DomHead[0]["cdefine4"] = ""; //表头自定义项4，DateTime类型
        ////           DomHead[0]["idiscounttaxtype"] = "0"; //扣税类别，int类型
        //           //DomHead[0]["cdefine5"] = ""; //表头自定义项5，int类型
        //           //DomHead[0]["cdefine6"] = ""; //表头自定义项6，DateTime类型
        //           //DomHead[0]["cdefine7"] = ""; //表头自定义项7，double类型
        //           //DomHead[0]["cdefine8"] = ""; //表头自定义项8，string类型
        //           //DomHead[0]["cdefine9"] = ""; //表头自定义项9，string类型
        //           //DomHead[0]["cdefine10"] = ""; //表头自定义项10，string类型
        //           //DomHead[0]["cdefine11"] = ""; //表头自定义项11，string类型
        //           //DomHead[0]["cdefine12"] = ""; //表头自定义项12，string类型
        //           //DomHead[0]["cdefine13"] = ""; //表头自定义项13，string类型
        //           //DomHead[0]["cdefine14"] = ""; //表头自定义项14，string类型
        //           //DomHead[0]["cdefine15"] = ""; //表头自定义项15，int类型

        //           //给BO表体参数domBody赋值，此BO参数的业务类型为采购入库单，属表体参数。BO参数均按引用传递
        //           //提示：给BO表体参数domBody赋值有两种方法

        //           //方法一是直接传入MSXML2.DOMDocumentClass对象
        //           //broker.AssignNormalValue("domBody", new MSXML2.DOMDocumentClass())

        //           //方法二是构造BusinessObject对象，具体方法如下：
        //           BusinessObject domBody = broker.GetBoParam("domBody");
        //           domBody.RowCount = 1;// dts.Rows.Count; //设置BO对象行数
        //           //可以自由设置BO对象行数为大于零的整数，也可以不设置而自动增加行数
        //           //给BO对象的字段赋值，值可以是真实类型，也可以是无类型字符串
        //           //以下代码示例只设置第一行值。各字段定义详见API服务接口定义
        //           //for (int j = 0; j < dts.Rows.Count; j++)
        //           //{

        //               //if (Convert.ToString(dts.Rows[j]["cInvCode"]) == "")
        //               //{
        //               //    throw new Exception("U8里没有这个存货编码" + Convert.ToString(dts.Rows[j]["cInvCode"]));
        //               //}
        //               //if (Public.GetNum(dts.Rows[j]["iQuantity"]) <= 0)
        //               //{
        //               //    throw new Exception("数量不能小于等于0");
        //               //}

        //               //if (Convert.ToString(dts.Rows[j]["cgdh"]) == "")
        //               //{
        //               //    throw new Exception("没有对应采购订单" + Convert.ToString(dts.Rows[j]["iPOsID"]));
        //               //}

        //               //decimal iQuantity = Public.GetNum(dts.Rows[j]["iQuantity"]);//数量

        //               //decimal iTaxUnitPrice = Public.GetNum(dts.Rows[j]["iOriTaxCost"]);//原币含税单价
        //               //decimal iSum = Public.GetNum(Public.ChinaRound(iTaxUnitPrice * iQuantity, 2));//原币含税金额
        //               //decimal iUnitPrice = Public.ChinaRound(iTaxUnitPrice / (1M + 17M / 100M), 4);//原币无税单价
        //               //decimal iMoney = Public.ChinaRound(iSum / (1M + 17M / 100M), 2);//原币无税金额
        //               //decimal iTax = Public.ChinaRound(iSum - iMoney, 2);//原币税额

        //               //decimal iNatUnitPrice = Public.ChinaRound(iUnitPrice * 1, 4);//本币无税单价
        //               //decimal iNatSum = Public.ChinaRound(iSum * 1, 4);//本币价税合计
        //               //decimal iNatMoney = Public.ChinaRound(iNatSum / (1 + 17M / 100M), 2);//本币无税金额
        //               //decimal iNatTax = Public.ChinaRound(iNatSum - iNatMoney, 2);//本币税额

        //               /****************************** 以下是必输字段 ****************************/
        //               //domBody[j]["autoid"] = ""; //主关键字段，int类型
        //               domBody[j]["id"] = "0"; //与收发记录主表关联项，int类型
        //               domBody[j]["cinvcode"] = "001";// Convert.ToString(dts.Rows[j]["cInvCode"]); //存货编码，string类型
        //          //     domBody[j]["cinvm_unit"] = "001"; //主计量单位，string类型
        //               domBody[j]["iquantity"] = "3"; //数量，double类型
        //               domBody[j]["editprop"] = "A"; //编辑属性：A表新增，M表修改，D表删除，string类型
        //               domBody[j]["iMatSettleState"] = "0"; //iMatSettleState，int类型

        //               /***************************** 以下是非必输字段 ****************************/
        //               //domBody[j]["cinvaddcode"] = ""; //存货代码，string类型
        //               //domBody[j]["cinvname"] = ""; //存货名称，string类型
        //               //domBody[j]["cinvstd"] = ""; //规格型号，string类型
        //               //domBody[j]["cinva_unit"] = ""; //库存单位，string类型
        //   //            domBody[j]["bservice"] = "0"; //是否费用，string类型
        //               //domBody[j]["cinvccode"] = ""; //所属分类码，string类型
        //               //domBody[j]["iinvexchrate"] = ""; //换算率，double类型
        //   //            domBody[j]["binvbatch"] = "0"; //批次管理，string类型
        //               //domBody[j]["inum"] = ""; //件数，double类型
        //    //           domBody[j]["cbatch"] = Convert.ToString(dts.Rows[j]["cBatch"]); //批号，string类型
        //               //domBody[j]["cfree1"] = ""; //存货自由项1，string类型
        //               //domBody[j]["cbatchproperty1"] = ""; //属性1，double类型
        //               //domBody[j]["cbatchproperty2"] = ""; //属性2，double类型
        //               //domBody[j]["cfree2"] = ""; //存货自由项2，string类型
        //                domBody[j]["iaprice"] = 0; //暂估金额，double类型
        //               //domBody[j]["ipunitcost"] = ""; //计划单价/售价，double类型
        //               //domBody[j]["ipprice"] = ""; //计划金额/售价金额，double类型
        //               //domBody[j]["dvdate"] ="2013-04-28"; //失效日期，DateTime类型
        //               //domBody[j]["cvouchcode"] = ""; //对应入库单id，string类型
        //               domBody[j]["iunitcost"] = 0; //本币单价，double类型
        //   //            domBody[j]["iflag"] = "0"; //标志，string类型
        //               //domBody[j]["dsdate"] = ""; //结算日期，DateTime类型
        //               //domBody[j]["itax"] = ""; //税额，double类型
        //               domBody[j]["isnum"] = "0"; //累计结算件数，double类型
        //               domBody[j]["imoney"] = "0"; //累计结算金额，double类型
        //               //domBody[j]["isoutquantity"] = ""; //累计出库数量，double类型
        //               //domBody[j]["isoutnum"] = ""; //累计出库件数，double类型
        //               //domBody[j]["ifnum"] = ""; //实际件数，double类型
        //               //domBody[j]["ifquantity"] = ""; //实际数量，double类型
        //               domBody[j]["iprice"] = 0; //本币金额，double类型
        // //              domBody[j]["binvtype"] = "0"; //折扣类型，string类型
        //               //domBody[j]["cdefine22"] = ""; //表体自定义项1，string类型
        //               //domBody[j]["cdefine23"] = ""; //表体自定义项2，string类型
        //               //domBody[j]["cdefine24"] = ""; //表体自定义项3，string类型
        //               //domBody[j]["cdefine25"] = ""; //表体自定义项4，string类型
        //               //domBody[j]["cdefine26"] = ""; //表体自定义项5，double类型
        //               //domBody[j]["cdefine27"] = ""; //表体自定义项6，double类型
        //   //            domBody[j]["isquantity"] = "0"; //累计结算数量，double类型
        //     //          domBody[j]["iposid"] = Convert.ToString(dts.Rows[j]["iPOsID"]); //订单子表ID，int类型
        //               domBody[j]["facost"] = 0; //暂估单价，double类型
        //               //domBody[j]["citemcode"] = ""; //项目编码，string类型
        //               //domBody[j]["citem_class"] = ""; //项目大类编码，string类型
        //               //domBody[j]["cbatchproperty3"] = ""; //属性3，double类型
        //               //domBody[j]["cfree3"] = ""; //存货自由项3，string类型
        //               //domBody[j]["cfree4"] = ""; //存货自由项4，string类型
        //               //domBody[j]["cbatchproperty4"] = ""; //属性4，double类型
        //               //domBody[j]["cbatchproperty5"] = ""; //属性5，double类型
        //               //domBody[j]["cfree5"] = ""; //存货自由项5，string类型
        //               //domBody[j]["cfree6"] = ""; //存货自由项6，string类型
        //   //            domBody[j]["cbatchproperty6"] = Convert.ToString(dt.Rows[i]["cVenDefine1"]); //属性6，string类型
        //   //            domBody[j]["cbatchproperty7"] = Convert.ToString(dt.Rows[i]["cVenCode"]); //属性7，string类型
        //               //domBody[j]["cfree7"] = ""; //存货自由项7，string类型
        //               //domBody[j]["cfree8"] = ""; //存货自由项8，string类型
        //               //domBody[j]["cbatchproperty8"] = ""; //属性8，string类型
        //               //domBody[j]["cbatchproperty9"] = ""; //属性9，string类型
        //               //domBody[j]["cfree9"] = ""; //存货自由项9，string类型
        //               //domBody[j]["cfree10"] = ""; //存货自由项10，string类型
        //               //domBody[j]["cbatchproperty10"] = ""; //属性10，DateTime类型
        //               //domBody[j]["cdefine28"] = ""; //表体自定义项7，string类型
        //               //domBody[j]["cdefine29"] = ""; //表体自定义项8，string类型
        //               //domBody[j]["cdefine30"] = ""; //表体自定义项9，string类型
        //               //domBody[j]["cdefine31"] = ""; //表体自定义项10，string类型
        //               //domBody[j]["cdefine32"] = ""; //表体自定义项11，string类型
        //               //domBody[j]["cdefine33"] = ""; //表体自定义项12，string类型
        //               //domBody[j]["cdefine34"] = ""; //表体自定义项13，int类型
        //               //domBody[j]["cdefine35"] = ""; //表体自定义项14，int类型
        //               //domBody[j]["cdefine36"] = ""; //表体自定义项15，DateTime类型
        //               //domBody[j]["cdefine37"] = ""; //表体自定义项16，DateTime类型
        //               //domBody[j]["cinvdefine4"] = ""; //存货自定义项4，string类型
        //               //domBody[j]["cinvdefine5"] = ""; //存货自定义项5，string类型
        //               //domBody[j]["cinvdefine6"] = ""; //存货自定义项6，string类型
        //               //domBody[j]["cinvdefine7"] = ""; //存货自定义项7，string类型
        //               //domBody[j]["cinvdefine8"] = ""; //存货自定义项8，string类型
        //               //domBody[j]["cinvdefine9"] = ""; //存货自定义项9，string类型
        //               //domBody[j]["cinvdefine10"] = ""; //存货自定义项10，string类型
        //               //domBody[j]["cinvdefine11"] = ""; //存货自定义项11，string类型
        //               //domBody[j]["cinvdefine12"] = ""; //存货自定义项12，string类型
        //               //domBody[j]["cinvdefine13"] = ""; //存货自定义项13，string类型
        //               //domBody[j]["cinvdefine14"] = ""; //存货自定义项14，string类型
        //               //domBody[j]["cinvdefine15"] = ""; //存货自定义项15，string类型
        //               //domBody[j]["cinvdefine16"] = ""; //存货自定义项16，string类型
        //               //domBody[j]["cbarcode"] = ""; //条形码，string类型
        //               domBody[j]["inquantity"] = "3"; //应收数量，double类型
        //               //domBody[j]["innum"] = ""; //应收件数，double类型
        //               //domBody[j]["impoids"] = ""; //生产订单子表ID，int类型
        //               //domBody[j]["icheckids"] = ""; //检验单子表ID，int类型
        //               //domBody[j]["iomodid"] = ""; //委外订单子表ID，int类型
        //               //domBody[j]["isodid"] = ""; //销售订单子表ID，string类型
        //               //domBody[j]["cbvencode"] = ""; //供应商编码，string类型
        //               //domBody[j]["cvenname"] = ""; //供应商，string类型
        //               //domBody[j]["imassdate"] ="2013-04-28"; //保质期，int类型
        //               //domBody[j]["dmadedate"] ="2013-04-28"; //生产日期，DateTime类型
        //               //domBody[j]["cassunit"] = ""; //库存单位码，string类型
        //               //domBody[j]["iarrsid"] = ""; //采购到货单子表标识，string类型
        //               //domBody[j]["corufts"] = ""; //时间戳，string类型
        //        //       domBody[j]["cposname"] = ""; //货位，string类型
        //               //domBody[j]["cgspstate"] = ""; //检验状态，double类型
        //               //domBody[j]["scrapufts"] = ""; //不合格品时间戳，string类型
        //               //domBody[j]["icheckidbaks"] = ""; //检验单子表id，string类型
        //               //domBody[j]["irejectids"] = ""; //不良品处理单id，string类型
        //               //domBody[j]["dcheckdate"] = ""; //检验日期，DateTime类型
        //               //domBody[j]["dmsdate"] = ""; //核销日期，DateTime类型
        //               //domBody[j]["cmassunit"] =""; //保质期单位，int类型
        //               //domBody[j]["ccheckcode"] = ""; //检验单号，string类型
        //               //domBody[j]["crejectcode"] = ""; //不良品处理单号，string类型
        //               //domBody[j]["csocode"] = ""; //需求跟踪号，string类型
        //               //domBody[j]["cvmivencode"] = ""; //代管商代码，string类型
        //               //domBody[j]["cvmivenname"] = ""; //代管商，string类型
        //  //             domBody[j]["bvmiused"] = "0"; //代管消耗标识，int类型
        //               //domBody[j]["ivmisettlequantity"] = ""; //代管挂账确认单数量，double类型
        //               //domBody[j]["ivmisettlenum"] = ""; //代管挂账确认单件数，double类型
        //               //domBody[j]["cbarvcode"] = ""; //到货单号，string类型
        //               //domBody[j]["dbarvdate"] = ""; //到货日期，DateTime类型
        //               //domBody[j]["cdemandmemo"] = ""; //需求分类代号说明，string类型
        //  //             domBody[j]["iordertype"] = "0"; //销售订单类别，int类型
        //               //domBody[j]["iorderdid"] = ""; //iorderdid，int类型
        //               //domBody[j]["iordercode"] = ""; //销售订单号，string类型
        //               //domBody[j]["iorderseq"] = ""; //销售订单行号，string类型
        // //              domBody[j]["iexpiratdatecalcu"] = "0"; //有效期推算方式，int类型
        //               //domBody[j]["cexpirationdate"] = "2013-04-28"; //有效期至，string类型
        //               //domBody[j]["dexpirationdate"] = "2013-04-28"; //有效期计算项，string类型
        //               //domBody[j]["cciqbookcode"] = ""; //手册号，string类型
        //               //domBody[j]["ibondedsumqty"] = ""; //累计保税处理抽取数量，string类型
        //               //domBody[j]["iimosid"] = ""; //iimosid，string类型
        //               //domBody[j]["iimbsid"] = ""; //iimbsid，string类型
        //               //domBody[j]["ccheckpersonname"] = ""; //检验员，string类型
        //               //domBody[j]["ccheckpersoncode"] = ""; //检验员编码，string类型
        //      //         domBody[j]["cpoid"] = Convert.ToString(dt.Rows[i]["cPOID"]); //订单号，string类型
        //               //domBody[j]["strcontractid"] = ""; //合同号，string类型
        //               //domBody[j]["strcode"] = ""; //合同标的编码，string类型
        //               //domBody[j]["cveninvcode"] = ""; //供应商存货编码，string类型
        //               //domBody[j]["cveninvname"] = ""; //供应商存货名称，string类型
        //               domBody[j]["isotype"] = "0"; //需求跟踪方式，int类型
        //               //domBody[j]["isumbillquantity"] = ""; //累计开票数量，double类型
        //               //domBody[j]["cbaccounter"] = ""; //记账人，string类型
        // //              domBody[j]["bcosting"] = "0"; //是否核算，string类型
        //               //domBody[j]["impcost"] = ""; //最高进价，string类型
        //               domBody[j]["ioritaxcost"] = 0; //原币含税单价，double类型
        //              domBody[j]["ioricost"] = 0; //原币单价，double类型
        //               domBody[j]["iorimoney"] = 0; //原币金额，double类型
        //               domBody[j]["ioritaxprice"] = 0; //原币税额，double类型
        //               domBody[j]["iorisum"] = 0; //原币价税合计，double类型
        //               domBody[j]["itaxrate"] = "17"; //税率，double类型
        //               domBody[j]["itaxprice"] = 0; //本币税额，double类型
        //               domBody[j]["isum"] = 0; //本币价税合计，double类型
        //               domBody[j]["btaxcost"] = "1"; //单价标准，double类型
        //               //domBody[j]["imaterialfee"] = ""; //材料费，double类型
        //               //domBody[j]["iprocesscost"] = ""; //加工费单价，double类型
        //               //domBody[j]["iprocessfee"] = ""; //加工费，double类型
        //               //domBody[j]["ismaterialfee"] = ""; //累计结算材料费，double类型
        //               //domBody[j]["isprocessfee"] = ""; //累计结算加工费，double类型
        //               //domBody[j]["isoseq"] = ""; //需求跟踪行号，string类型
        //               //domBody[j]["cinvdefine1"] = ""; //存货自定义项1，string类型
        //               //domBody[j]["cinvdefine2"] = ""; //存货自定义项2，string类型
        //               //domBody[j]["cinvdefine3"] = ""; //存货自定义项3，string类型
        //               //domBody[j]["creplaceitem"] = ""; //替换件，string类型
        //     //          domBody[j]["cposition"] = ""; //货位编码，string类型
        //               //domBody[j]["itrids"] = ""; //特殊单据子表标识，double类型
        //               //domBody[j]["cname"] = ""; //项目名称，string类型
        //               //domBody[j]["citemcname"] = ""; //项目大类名称，string类型
        //               //domBody[j]["cinvouchcode"] = ""; //对应入库单号，string类型
        //        //       domBody[j]["iinvsncount"] = "3"; //存库序列号，int类型

        //         //  }

        //           //给普通参数domPosition赋值。此参数的数据类型为System.Object，此参数按引用传递，表示货位：传空
        //           broker.AssignNormalValue("domPosition", null);

        //           //该参数errMsg为OUT型参数，由于其数据类型为System.String，为一般值类型，因此不必传入一个参数变量。在API调用返回时，可以通过GetResult("errMsg")获取其值

        //           //给普通参数cnnFrom赋值。此参数的数据类型为ADODB.Connection，此参数按引用传递，表示连接对象,如果由调用方控制事务，则需要设置此连接对象，否则传空
        //           broker.AssignNormalValue("cnnFrom", null);

        //           //该参数VouchId为INOUT型普通参数。此参数的数据类型为System.String，此参数按值传递。在API调用返回时，可以通过GetResult("VouchId")获取其值
        //           broker.AssignNormalValue("VouchId", "");

        //           //该参数domMsg为OUT型参数，由于其数据类型为MSXML2.IXMLDOMDocument2，非一般值类型，因此必须传入一个参数变量。在API调用返回时，可以直接使用该参数
        //           MSXML2.DOMDocument domMsg = new MSXML2.DOMDocumentClass();
        //           broker.AssignNormalValue("domMsg", domMsg);

        //           //给普通参数bCheck赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否控制可用量。
        //           broker.AssignNormalValue("bCheck", true);

        //           //给普通参数bBeforCheckStock赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示检查可用量
        //           broker.AssignNormalValue("bBeforCheckStock", true);

        //           //给普通参数bIsRedVouch赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否红字单据
        //           broker.AssignNormalValue("bIsRedVouch", false);

        //           //给普通参数sAddedState赋值。此参数的数据类型为System.String，此参数按值传递，表示传空字符串
        //           broker.AssignNormalValue("sAddedState", "");

        //           //给普通参数bReMote赋值。此参数的数据类型为System.Boolean，此参数按值传递，表示是否远程：转入false
        //           broker.AssignNormalValue("bReMote", false);


        //           //第六步：调用API
        //           if (!broker.Invoke())
        //           {
        //               //错误处理
        //               Exception apiEx = broker.GetException();
        //               if (apiEx != null)
        //               {
        //                   if (apiEx is MomSysException)
        //                   {
        //                       MomSysException sysEx = apiEx as MomSysException;
        //                       throw new Exception("系统异常：" + sysEx.Message);
        //                       //todo:异常处理
        //                   }
        //                   else if (apiEx is MomBizException)
        //                   {
        //                       MomBizException bizEx = apiEx as MomBizException;
        //                       throw new Exception("API异常：" + bizEx.Message);
        //                       //todo:异常处理
        //                   }
        //                   //异常原因
        //                   String exReason = broker.GetExceptionString();
        //                   if (exReason.Length != 0)
        //                   {
        //                       throw new Exception("异常原因：" + exReason);
        //                   }
        //               }
        //               //结束本次调用，释放API资源
        //               broker.Release();
        //           }

        //           //第七步：获取返回结果

        //           //获取返回值
        //           //获取普通返回值。此返回值数据类型为System.Boolean，此参数按值传递，表示返回值:true:成功,false:失败
        //           System.Boolean result = Convert.ToBoolean(broker.GetReturnValue());


        //           //获取out/inout参数值

        //           //获取普通OUT参数errMsg。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
        //           System.String errMsgRet = broker.GetResult("errMsg") as System.String;

        //           if (!result)
        //           {
        //               broker.Release();
        //               throw new Exception(errMsgRet);
        //           }

        //           //获取普通INOUT参数VouchId。此返回值数据类型为System.String，在使用该参数之前，请判断是否为空
        //           System.String VouchIdRet = broker.GetResult("VouchId") as System.String;

        //           //DataTable dtRet = AdoAccess.ExecuteDT("select cCode from rdrecord01 where ID = '" + VouchIdRet + "'", conn);
        //           //if (dtRet.Rows.Count > 0)
        //           //{
        //           //    dgvMain.Rows[iRow].Cells["cRKCode"].Value = Convert.ToString(dtRet.Rows[0]["cCode"]);
        //           //}

        //           //获取普通OUT参数domMsg。此返回值数据类型为MSXML2.IXMLDOMDocument2，在使用该参数之前，请判断是否为空
        //           MSXML2.DOMDocument domMsgRet = (MSXML2.DOMDocument)(broker.GetResult("domMsg"));

        //           //MSXML2.DOMDocument domResultBody = (MSXML2.DOMDocumentClass)broker.GetResult("domBody");

        //           //IXMLDOMNodeList list = domResultBody.selectNodes("//rs:data/z:row");

        //           //int iFetch = -1;
        //           //for (int i = 0; i < list.length; i++)
        //           //{
        //           //    IXMLDOMNode node = list[i].attributes.getNamedItem("isosid");
        //           //    for (int j = iFetch + 1; j < dgvMain.Rows.Count; j++)
        //           //    {
        //           //        if (Convert.ToString(dgvMain.Rows[j].Cells["选择"].Value) == "Y")
        //           //        {
        //           //            dgvMain.Rows[j].Cells["iSOsID"].Value = Convert.ToString(node.nodeValue);
        //           //            iFetch = j;
        //           //            break;
        //           //        }
        //           //    }
        //           //}


        //           //结束本次调用，释放API资源
        //           broker.Release();
        //           return "成功";
        #endregion
    }
}
