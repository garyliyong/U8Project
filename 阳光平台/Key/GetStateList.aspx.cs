using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Sq.Common;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using DBUtility;
using System.Collections.Generic;

public partial class GetStateList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Model.A_ExpressLog> list = new BLL.A_ExpressLog().GetList(" top 1 *", null, "LastUpdate desc");
        if (list.Count > 0)
        {
            Bind(list[0].LastUpdate);
        }
    }

    public void Bind(DateTime LastUpdate)
    {
        try
        {
   
            string BatchNo = "P" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string xml = "<Request>";
            xml += "<Method><![CDATA[Order.Status]]></Method>";
            xml += "<Rules>";
            xml += "<Item>";
            xml += "<Name>last_update</Name>";
            xml += "<Type><![CDATA[>=]]></Type>";
            xml += "<Value><![CDATA[" + LastUpdate.ToString("yyyy-MM-dd HH:mm:ss") + "]]></Value>";
            xml += "</Item>";
            xml += "<Item>";
            xml += "<Name>last_update</Name>";
            xml += "<Type><![CDATA[<=]]></Type>";
            xml += " <Value><![CDATA[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]]></Value>";
            xml += "</Item>";
            xml += "<Item>";
            xml += " <Name>sd_code</Name>";
            xml += " <Type><![CDATA[=]]></Type>";
            xml += " <Value><![CDATA[LOVO]]></Value>";
            xml += "</Item>";
            xml += "</Rules>";
            xml += "</Request>";


            if (xml != "")
            {

                string api_url = ConfigurationManager.AppSettings["api_url"].ToString();
                string key = ConfigurationManager.AppSettings["app_key"].ToString();
                string secret = ConfigurationManager.AppSettings["app_secret"].ToString();
                string app_key = "app_key=" + key;
                string app_secret = "app_secret=" + md5(secret);
                string submitUrl = api_url + "?" + app_key + "&" + app_secret;//提交地址

                UTF8Encoding encoding = new UTF8Encoding();

                string postData = "interface=" + xml;
                byte[] data = encoding.GetBytes(postData);

                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(submitUrl);
                myRequest.Method = "POST";


                myRequest.ContentType = "application/x-www-form-urlencoded;";
                myRequest.ContentLength = data.Length;
                Stream newStream = myRequest.GetRequestStream();

                // Send the data. 
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                // Get response 
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                string content = reader.ReadToEnd();
                System.Xml.XmlDocument DOM = new System.Xml.XmlDocument();
                DOM.LoadXml(content);
                XmlNodeList bigxml = null;
                //Reponse

                bigxml = DOM.SelectSingleNode("Response").ChildNodes;


                foreach (XmlNode xnf in bigxml)
                {
                    if (xnf.Name == "Success")
                    {
                        if (xnf.InnerText.ToString().Trim().ToUpper().Equals("TRUE"))
                        {

                        }
                        // Response.Write("Success:" + xnf.InnerText);//显示子节点点文本 
                    }
                    if (xnf.Name == "ErrCode")
                    {
                        Response.Write("ErrCode:" + xnf.InnerText);//显示子节点点文本 
                    }
                    if (xnf.Name == "ErrMsg")
                    {
                        Response.Write("ErrMsg:" + xnf.InnerText);//显示子节点点文本 
                    }

                    if (xnf.Name == "Content")
                    {
                        XmlNode xnq1 = xnf;
                        System.IO.StringReader myStringReader = new System.IO.StringReader(xnq1.OuterXml);
                        DataSet myDataSet = new DataSet();
                        XmlReader myXmlReader = XmlReader.Create(myStringReader);
                        myDataSet.ReadXml(myXmlReader);

                        if (myDataSet != null)
                        {
                            if (myDataSet.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in myDataSet.Tables[0].Rows)
                                {
                                    bool rlt = false;
                                    string shipping_status = dr["shipping_status"].ToString().Trim();//配送状态
                                    string shipping_code = dr["shipping_code"].ToString().Trim();//物流公司代码
                                    string ExpressName = "默认快递";
                                    if (shipping_code.ToLower().Equals("yto"))
                                    {
                                        ExpressName = "圆通快递";
                                    }

                                    if (shipping_code.ToLower().Equals("ems"))
                                    {
                                        ExpressName = "邮局Ems";
                                    }

                                    if (shipping_code.ToLower().Equals("sf"))
                                    {
                                        ExpressName = "顺丰快递";
                                    }

                                    if (shipping_code.ToLower().Equals("sto"))
                                    {
                                        ExpressName = "申通快递";
                                    }

                                    if (shipping_code.ToLower().Equals("pick"))
                                    {
                                        ExpressName = "联邦快递";
                                    }
                                    string order_sn = dr["order_sn"].ToString().Trim();//订单单号
                                    string back_order_sn = dr["back_order_sn"].ToString().Trim();//退单号
                                    string back_reason = dr["back_reason"].ToString().Trim();//退单原因
                                    string ExpressNum = dr["invoice_no"].ToString().Trim();//快递单号
                                    string OrderCode = dr["deal_code"].ToString().Trim();//交易号
                                    string order_status = dr["order_status"].ToString().Trim();//订单状态

                                    string last_update = GetUnixTime(dr["last_update"].ToString().Trim()).ToString();//最后更新时间
                                    string ExpressUrl = "";


                                    Model.A_OrderInfo orderModel = new BLL.A_OrderInfo().GetModel(OrderCode);
                                    if (orderModel != null)
                                    {

                                        if (shipping_status.Equals("1") && Util.IsEmpty(back_order_sn) && Util.IsEmpty(back_reason))//已发货
                                        {

                                            if (orderModel != null)
                                            {
                                                if (orderModel.OrderState != 0 && orderModel.OrderState != 6 && orderModel.OrderState != 15 && orderModel.OrderState != 3)
                                                {
                                                    //包裹配送中
                                                    rlt = UpdateSendState(OrderCode, 2, ExpressName, ExpressNum, ExpressUrl, orderModel);
                                                    if (rlt)
                                                    {
                                                        string Remarks = "包裹配送中";
                                                        OrderLog(OrderCode, "2", last_update, Remarks);
                                                        WriteLog("Status:Success,OrderNo:" + OrderCode + ",OrderState:包裹配送中,ExpressName:" + ExpressName + ",ExpressNum:" + ExpressNum + ",\r\nExpressUrl:" + ExpressUrl + ",LastUpdate:" + last_update + "\r\n", "Success");
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                WriteLog("Status:Error,ErrorDesc:订单不存在,OrderNo:" + OrderCode + ",OrderState:包裹配送中,ExpressName:" + ExpressName + ",ExpressNum:" + ExpressNum + ",\r\nExpressUrl:" + ExpressUrl + ",LastUpdate:" + last_update + "\r\n", "Error");
                                            }

                                        }

                                        //发货状态：已收货? 订单状态：已完成  && shipping_status.Equals("2") 
                                        if (orderModel.OrderState == 2&& order_status.Equals("5") && Util.IsEmpty(back_order_sn) && Util.IsEmpty(back_reason))//已收货 成功订单和部分退货订单
                                        {
                                            if (orderModel != null)
                                            {
                                                //判断是否是客服处理中、成功、失败、取消订单
                                                if (orderModel.OrderState != 0 && orderModel.OrderState != 6 && orderModel.OrderState != 15 && orderModel.OrderState != 3)
                                                {
                                                    rlt = UpdateSuccessState(OrderCode, 6, orderModel);
                                                    if (rlt)
                                                    {
                                                        string Remarks = "配送成功";
                                                        OrderLog(OrderCode, "6", last_update, Remarks);
                                                        WriteLog("Status:Success,OrderNo:" + OrderCode + ",OrderState:配送成功,LastUpdate:" + last_update + "\r\n", "Success");
                                                    }
                                                }
                                            }

                                        }

                                        //只有包裹配送中的才改
                                        if (orderModel.OrderState == 2 && !Util.IsEmpty(back_order_sn) && !Util.IsEmpty(back_reason))//全单退货
                                        {
                                            if (back_reason.Length >= 4)
                                            {
                                                if (back_reason.Substring(0, 4).ToString().Trim() == "物流退货")
                                                {
                                                    rlt = UpdateFailureState(OrderCode, 15, orderModel);
                                                    if (rlt)
                                                    {
                                                        string Remarks = "配送失败";
                                                        OrderLog(OrderCode, "15", last_update, Remarks);
                                                        WriteLog("Status:Failure,OrderNo:" + OrderCode + ",OrderState:配送成功,LastUpdate:" + last_update + "\r\n", "Success");
                                                    }

                                                }
                                                else
                                                {
                                                    rlt = UpdateSuccessState(OrderCode, 6, orderModel);
                                                    if (rlt)
                                                    {
                                                        string Remarks = "配送成功";
                                                        OrderLog(OrderCode, "6", last_update, Remarks);

                                                    }
                                                }

                                            }
                                            else
                                            {
                                                rlt = UpdateFailureState(OrderCode, 15, orderModel);
                                                if (rlt)
                                                {
                                                    string Remarks = "配送失败";
                                                    OrderLog(OrderCode, "15", last_update, Remarks);
                                                }
                                            }
                                        }



                                    }
                                }

                            }
                        }

                    }

                }

                Model.A_ExpressLog ExpressLogModel = new Model.A_ExpressLog();
                ExpressLogModel.CreateDate = DateTime.Now;
                ExpressLogModel.LastUpdate = DateTime.Now;
                ExpressLogModel.BatchNo = BatchNo;
            }
        }
        catch (Exception ex)
        {
            WriteLog("Status:Error,ErrorDesc:" + ex.Message + "|" + ex.Source + ",LastUpdate:" + DateTime.Now.ToString() + "\r\n", "Error");
        }



    }


    //跟踪推荐用户购买
    private void TrackBind(string OrderCode)
    {
        List<Model.A_UserRecommend> RecommendList = new BLL.A_UserRecommend().GetList(null, "OrderCode='" + OrderCode + "' and Status='已下单'", null);
        //查询推荐人是否有注册
        if (RecommendList.Count > 0)
        {
            Model.A_UserRecommend UserRecommendModel = RecommendList[0];
            UserRecommendModel.Remark = "已奖励";
            bool count = new BLL.A_UserRecommend().Update(UserRecommendModel);
            if (count==true)
            {
                if (UserRecommendModel.Integral > 0)
                {
                    Common.OperIntegral(UserRecommendModel.UserId, 0, UserRecommendModel.Integral, 4, "推荐购买奖励", 1, "推荐购买奖励");
                }
            }
        }
    }


    public DateTime GetUnixTime(string UnixTime)
    {
        string timeStamp = UnixTime;
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dtResult = dtStart.Add(toNow);

        return dtResult;

    }

    //订单更新日志
    private void OrderLog(string OrderCode, string OrderState, string LastUpdate, string Remarks)
    {
        Model.A_OrderInfo orderModel_up = new BLL.A_OrderInfo().GetModel(OrderCode);
        Model.A_OrderLog model = new Model.A_OrderLog();
        model.OrderId = orderModel_up.OrderId;
        model.OrderCode = OrderCode;
        model.StateName = OrderState;
        model.LastUpdate = Sq.Common.Util.GetDateTime(LastUpdate).AddHours(8);
        model.Remark = Remarks;
        model.CreateDate = DateTime.Now;
        new BLL.A_OrderLog().Add(model);
    }

    //更新新订单 
    private bool UpdateSendState(string OrderCode, int OrderState, string ExpressName, string ExpressNum, string ExpressUrl, Model.A_OrderInfo orderModel)
    {
        bool rlt = false;
        decimal yifukuan = orderModel.PayAmount;
        //更新订单表状态
        Model.A_OrderInfo orderModel_up = new BLL.A_OrderInfo().GetModel(OrderCode);
        if (orderModel_up != null)
        {
            orderModel_up.OrderId = orderModel.OrderId;
            orderModel_up.OrderState = OrderState;
            orderModel_up.PayState = orderModel.PayState;
            orderModel_up.PayAmount = orderModel.PayAmount;
            orderModel_up.ExpressName = ExpressName;
            orderModel_up.ExpressNum = ExpressNum;
            orderModel_up.ExpressUrl = ExpressUrl;
            rlt = new BLL.A_OrderInfo().Update(orderModel_up);

            //Model.A_Express ExpressModel = new Model.A_Express();
            //ExpressModel.OrderCode = OrderCode;
        }
        return rlt;
    }

    //更新新订单 
    private bool UpdateSuccessState(string OrderCode, int OrderState, Model.A_OrderInfo orderModel)
    {
        //更新订单表状态
        Model.A_OrderInfo orderModel_up = new BLL.A_OrderInfo().GetModel(OrderCode);
        orderModel_up.OrderId = orderModel.OrderId;
        orderModel_up.OrderState = OrderState;
        orderModel_up.PayState = orderModel.PayState;
        orderModel_up.PayAmount = orderModel.PayAmount + orderModel.OrderFreight;
        orderModel_up.NeedPayAmount = orderModel.NeedPayAmount;

        //配送成功
        if (OrderState == 6)
        {
            //是货到付款的时候更新支付时间
            if (orderModel_up.PayState == 0)
            {
                orderModel_up.PayDate = DateTime.Now;
            }

            orderModel_up.PayState = 1;
            orderModel_up.PayAmount = orderModel.PayAmount + orderModel.OrderFreight;
            orderModel_up.NeedPayAmount = 0;

            //更新Cps 成功状态
            //CpsOrder.OverOrder(orderModel.OrderCode, 1, System.DateTime.Now, "");

            //更新用户积分  在没使用优惠券的时候
            if (orderModel.OrderAmount > 0)
            {
                int integral = Convert.ToInt32(orderModel.PayAmount);
                if (integral > 0)
                {
                    //更新用户积分
                    Common.OperIntegral(orderModel.UserID, orderModel.OrderId, orderModel.Integral, 1, "购买商品成功奖励积分", 1, "购买商品成功奖励积分");

                }
            }

            TrackBind(orderModel.OrderCode);//奖励积分给推荐人
        }
        bool rlt = new BLL.A_OrderInfo().Edit(orderModel_up);
        return rlt;

    }
    //更新新订单
    private bool UpdateFailureState(string OrderCode, int OrderState, Model.A_OrderInfo orderModel)
    {

        //配送失败
        if (OrderState == 15)
        {
            orderModel.OrderState = 15;
            if (orderModel.Integral > 0)
            {
                //更新用户积分
                Common.OperIntegral(orderModel.UserID, orderModel.OrderId, orderModel.Integral, 1, "配送失败返还积分", 1, "配送失败返还积分");

            }
            //退还购物券
            if (orderModel.CouponCode != null && orderModel.CouponAmount > 0)
            {
                new BLL.A_CouponLog().DeleteBuOrderCode(ViewState["OrderCode"].ToString());//删除购物卡使用记录
            }
            //退还购物卡金额
            if (orderModel.CardCode != null)
            {
                Model.A_ShopCardCoupon ShopCardCouponModel = new BLL.A_ShopCardCoupon().GetCardCodeModel(orderModel.CardCode);
                if (ShopCardCouponModel != null)
                {
                    ShopCardCouponModel.Amount = ShopCardCouponModel.Amount + orderModel.CardAmount;//退还购物卡抵扣金额
                    bool bo = new BLL.A_ShopCardCoupon().UpdateByCode(ShopCardCouponModel);
                    if (bo)
                    {
                        new BLL.A_ShopCardLog().DeleteBuOrderCode(ViewState["OrderCode"].ToString());//删除购物卡使用记录
                    }
                }
            }




        }

        //配送失败CSR事件
        //string strTitle = "配送失败CSR人员电话跟进:网络订单-订单编号：" + OrderCode;
        //FailCommon(OrderCode, strTitle);

        //横志对接
        //SendUnion(OrderCode, 0);
        //Lovo联盟
        //SendLovo(OrderCode, 0);




        bool rlt = new BLL.A_OrderInfo().Edit(orderModel);
        return rlt;
    }




    static public string XMLDocumentToString(XmlDocument doc)
    {
        MemoryStream stream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(stream, null);
        writer.Formatting = Formatting.Indented;
        doc.Save(writer);

        StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
        stream.Position = 0;
        string xmlString = sr.ReadToEnd();
        sr.Close();
        stream.Close();

        return xmlString.Replace("<?xml version=\"1.0\"?>", "");
    }
    //将字符串中的全角字符转换为半角
    public static string ToBj(string s)
    {
        if (s == null || s.Trim() == string.Empty)
        {
            return s;
        }
        else
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\u3000')
                {
                    sb.Append('\u0020');
                }
                else if (IsQjChar(s[i]))
                {
                    sb.Append((char)((int)s[i] - 65248));
                }
                else
                {
                    sb.Append(s[i]);
                }
            }
            return sb.ToString();
        }
    }

    //判断字符是否英文半角字符或标点
    public static bool IsBjChar(char c)
    {
        //32    空格
        //33-47    标点
        //48-57    0~9
        //58-64    标点
        //65-90    A~Z
        //91-96    标点
        //97-122    a~z
        //123-126  标点
        int i = (int)c;
        return i >= 32 && i <= 126;
    }

    //判断字符是否全角字符或标点
    public static bool IsQjChar(char c)
    {
        if (c == '\u3000')
        {
            return true;
        }
        int i = (int)c - 65248;
        if (i < 32)
        {
            return false;
        }
        else
        {
            return IsBjChar((char)i);
        }
    }

    public static string md5(string str)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
    }
    private static void WriteLog(string WriteLog, string Type)
    {
        if (Type.Equals("Success"))
        {
            LogManager.LogFielPrefix = "Exp ";
            LogManager.LogPath = @"E:\Log\Lovo_Success\";
            LogManager.WriteLog(LogFile.Trace.ToString(), WriteLog);
        }
        if (Type.Equals("Error"))
        {
            LogManager.LogFielPrefix = "Exp ";
            LogManager.LogPath = @"E:\Log\Lovo_Error\";
            LogManager.WriteLog(LogFile.Trace.ToString(), WriteLog);
        }

    }
}
