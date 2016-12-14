using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace SHYSInterface
{
    class SendMessage
    {
        public static string SetMessage(string sXxlx, string xmlData)
        {
            try
            {
                //测试用户名：zdyc001
                //测试密码：zdyc001001
                string sUser = "zdyc001";
                //string sPwd = "zdyc001001";  //测试密码
                string sPwd = "zszdyc001001";//正式密码
                string sJgbm = "ZDYC0001";
                string sVersion = "1.0.0.0";
                //string sXxlx = "";
                string sSign = "";
            //    SMPA.YsxtMainServiceClient sc = new SMPA.YsxtMainServiceClient();
                ZSSMPA.YsxtMainServiceClient sc = new ZSSMPA.YsxtMainServiceClient();
                sSign = getMessageDigest(xmlData, "SHA-1");

              //  NJPT.BaseWebServiceReq  ww= new SHYSInterface.NJPT.BaseWebServiceReq();
                
              ////NJPT.SupplyOrderReturn Return = new SHYSInterface.NJPT.SupplyOrderReturn();
               

              //  NJPT.SupplyServicePortTypeClient dd = new SHYSInterface.NJPT.SupplyServicePortTypeClient();
              //   NJPT.SupplyOrderReturn Return =dd.downloadOrders4Dely(ww);
              //   NJPT.SupplOrder[] rr = Return.orderList;
              //   foreach (NJPT.SupplOrder tt in rr)
              //   {
              //       tt.pa
              //   }
                


                //  sSign = getMessageDigest(xmlData, "SHA-1");
                string dsstr = sc.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, xmlData);
                return dsstr;

                //  richTextBox1.Text = dsstr;
                //TextReader tdr = new StringReader(dsstr);
                //DataSet ds = new DataSet();
                //ds.ReadXml(tdr);
            }
            catch (Exception ex)
            {
                return ex.ToString();
                throw;
            }
        }
        //计算消息摘要
        public static String getMessageDigest(String str, String encName)
        {


            //  IsNullOrWhiteSpace
            byte[] digest = null;
          //  if (String.IsNullOrWhiteSpace(encName))
            if ( ClsSystem.gnvl(encName,"")=="")
            {
                encName = "SHA-1";
            }
            try
            {
                switch (encName)
                {
                    case "SHA-1":
                        digest = SHA_1(str);
                        break;
                    default:
                        digest = SHA_1(str);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            return byteArrayToHex(digest);
        }

        // 字节数组转换为16 进制的字符串
        private static String byteArrayToHex(byte[] byteArray)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            char[] resultCharArray = new char[byteArray.Length * 2];
            int index = 0;
            foreach (byte b in byteArray)
            {
                resultCharArray[index++] = hexDigits[b >> 4 & 0xf];
                resultCharArray[index++] = hexDigits[b & 0xf];
            }
            return new String(resultCharArray);
        }
        public static byte[] SHA_1(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return SHA1.Create().ComputeHash(bytes);

        }
        public static string GetMAC()
        {
            try
            {
                string mac = "";
                string strmac = "";

                ManagementClass mc;
                string hostInfo = Dns.GetHostName();
                //mac地址   
                mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo["IPEnabled"].ToString() == "True")
                    {
                        mac = mo["MacAddress"].ToString();
                    }
                }
                string[] ReadText = mac.Split(':');

                for (int i = 0; i < ReadText.Length; i++)
                {
                    strmac = strmac + ReadText[i].ToString();
                }
                return strmac;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }
        public static string GetIP()
        {
            try
            {
                string ip = "";

                ManagementClass mc;
                string hostInfo = Dns.GetHostName();
                //IP地址   
                //System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;//这个过时   
                System.Net.IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                for (int i = 0; i < addressList.Length; i++)
                {
                    if (addressList[i].AddressFamily.ToString() == "InterNetwork")
                    {
                        ip = addressList[i].ToString();
                    }
                }
                return ip;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public static DataSet QuryData(string cType, string xmlData)
        {
            DataSet ds = new DataSet();
            try
            {

                string ResultXml = SendMessage.SetMessage(cType, xmlData);
                TextReader tdr = new StringReader(ResultXml);

                ds.ReadXml(tdr);

                return ds;
            }
            catch (Exception ex)
            {
                return ds;
                throw;
            }

        }
        //读取xml
        public static string ReadXMl(string xml, string XmlNode, string parm)
        {

            string str = "";
            try
            {
                XmlDocument myname = new XmlDocument();

                myname.LoadXml(xml);
                XmlNodeList node = myname.SelectSingleNode("XMLDATA").ChildNodes;
                str = myname.InnerText;
                foreach (XmlNode xn in node)
                {
                    XmlElement xe = (XmlElement)xn;
                    XmlNodeList nodech = xe.ChildNodes;
                    if (xe.LocalName == XmlNode)
                    {
                        foreach (XmlNode xnch in nodech)
                        {
                            XmlElement xech = (XmlElement)xnch;
                            if (xech.LocalName == parm)
                            {
                                str = xech.InnerText;
                                break;
                            }
                        }
                    }

                }
                return str;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "0";

            }

        }




    }
}
