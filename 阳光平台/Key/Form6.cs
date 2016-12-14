using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Management;
using System.Net;
using SHYSInterface.退货单;

namespace SHYSInterface
{
    public partial class Form6 : Form
    {

        
        public Form6()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {


            FrmYQ001 uc = new FrmYQ001();
            uc.ShowDialog();
            
        }

    
      

        private void button2_Click(object sender, EventArgs e)
        {
//            string xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>
//<XMLDATA>
//	<HEAD>
//		<IP>10.1.21.40</IP>
//		<MAC>122334455667</MAC>
//		<BZXX>这里是备注</BZXX>
//	</HEAD>
//	<MAIN>
//		<KCCBSJ>20141120/115150/</KCCBSJ>
//		<JLS>3</JLS>
//	</MAIN>
//	<DETAIL>
//		<STRUCT>
//			<SPLX>1</SPLX>
//			<ZXSPBM>X03831250030010</ZXSPBM>
//			<YPKCL>1</YPKCL>
//			<KCSL>0</KCSL>
//			<KCDW>盒</KCDW>
//		</STRUCT>
//		<STRUCT>
//			<SPLX>1</SPLX>
//			<ZXSPBM>X01130080010010</ZXSPBM>
//			<YPKCL>2</YPKCL>
//			<KCSL>0</KCSL>
//			<KCDW>盒</KCDW>
//		</STRUCT>
//		<STRUCT>
//			<SPLX>1</SPLX>
//			<ZXSPBM>X01299400010010</ZXSPBM>
//			<YPKCL>3</YPKCL>
//			<KCSL>0</KCSL>
//			<KCDW>盒</KCDW>
//		</STRUCT>
//	</DETAIL>
//</XMLDATA>";

//            richTextBox1.Text = SendMessage.SetMessage( "YQ002", xmlData);

            FrmYQ002 uc = new FrmYQ002();
            uc.ShowDialog();
        }
        //计算消息摘要
        public static String getMessageDigest(String str, String encName)
        {

          
          //  IsNullOrWhiteSpace
            byte[] digest = null;
          //  if (String.IsNullOrWhiteSpace(encName))
            if (ClsSystem.gnvl(encName, "")=="")
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

        private void button9_Click(object sender, EventArgs e)
        {
         
        }

        private void button10_Click(object sender, EventArgs e)
        {
//            string xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>
//<XMLDATA>
//<HEAD>
//<IP>10.1.21.40</IP>
//<MAC>122334455667</MAC>
//<BZXX>这里是备注</BZXX>
//</HEAD>
//<MAIN>
//<SFBHZGS>1</SFBHZGS>
//<YYBM></YYBM>
//<QSRQ>20150315</QSRQ>
//<JZRQ>20150323</JZRQ>
//<YPLX></YPLX>
//<CGLX></CGLX>
//<DDLX></DDLX>
//<SPLX></SPLX>
//<DDTJFS></DDTJFS>
//<DDCLZT></DDCLZT>
//<DDMXBH></DDMXBH>
//</MAIN>
//</XMLDATA>";
//       //     string result = SendMessage.SetMessage("YQ010", xmlData);
//            DataSet ds = SendMessage.QuryData("YQ010", xmlData);


            FrmYQ010 uc = new FrmYQ010();
            uc.ShowDialog();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try  
           {
               FrmYQ016 uc = new FrmYQ016();
               uc.ShowDialog();
           }  
          catch (Exception ex)  
           { 

          }  

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
                //System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;这个过时   
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

        private void button3_Click(object sender, EventArgs e)
        {
//            string xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>
//<XMLDATA>
//<HEAD>
//  <IP>10.1.21.40</IP> 
//  <MAC>122334455667</MAC> 
//  <BZXX>1个品规</BZXX> 
//</HEAD>
//<MAIN>
//  <CZLX>1</CZLX> 
//  <PSDH>141012120123</PSDH> 
//  <YQBM>GYSNB001</YQBM> 
//  <YYBM>42500853400</YYBM> 
//  <PSDBM>20510000</PSDBM> 
//  <CJRQ>20141124</CJRQ> 
//  <SDRQ>20141229</SDRQ> 
//  <ZXS>1</ZXS> 
//  <JLS>2</JLS>
//</MAIN>
//<DETAIL>
//<STRUCT>
//  <PSDTM>00000453824</PSDTM>
//  <ZXLX>1</ZXLX>
//  <SPLX>1</SPLX>
//  <ZXSPBM>X03831250030010</ZXSPBM>
//  <SCPH>1312282161</SCPH>
//  <SCRQ>20140813</SCRQ>
//  <YXRQ>20151227</YXRQ>
//  <XSDDH>123</XSDDH>
//  <WLPTDDH>114001868715</WLPTDDH>
//  <DDMXBH>20141205010000000301</DDMXBH>
//  <PSL>200</PSL>
//  <CGJLDW>1</CGJLDW>
//</STRUCT>
//<STRUCT>
//  <PSDTM>00000453855</PSDTM>
//  <ZXLX>1</ZXLX>
//  <SPLX>1</SPLX>
//  <ZXSPBM>X01130080010010</ZXSPBM>
//  <SCPH>1312282161</SCPH>
//  <SCRQ>20140813</SCRQ>
//  <YXRQ>20151227</YXRQ>
//  <XSDDH>124</XSDDH>
//  <WLPTDDH>114001868715</WLPTDDH>
//  <DDMXBH>20141205010000000302</DDMXBH>
//  <PSL>84</PSL>
//  <CGJLDW>1</CGJLDW>
//</STRUCT>
//</DETAIL>
//</XMLDATA>";

//       DataSet ds =SendMessage.QuryData("YQ003", xmlData);

            FrmYQ003 uc = new FrmYQ003();
            uc.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
//            string xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>
//<XMLDATA>
// <HEAD>
//  <IP>10.1.21.40</IP> 
//  <MAC>122334455667</MAC> 
//  <BZXX>发票</BZXX> 
// </HEAD>
// <MAIN>
//  <CZLX>1</CZLX>
//  <YQBM>GYSNB001</YQBM>
//  <YYBM>42500853400</YYBM>
//  <PSDBM>20510000</PSDBM>
//  <JLS>2</JLS>
// </MAIN>
//<DETAIL>
//  <STRUCT>
//    <FPH>07106825</FPH>
//    <FPRQ>20140303</FPRQ>
//    <FPHSZJE>30016</FPHSZJE>
//    <DLCGBZ>0</DLCGBZ>
//    <FPBZ>003479  批号:1312272165 效期:2015-12-26 数量:400  中标 含税单价: 75.04 零: 86.30  王建峰,  203308 包装数量: 20  瓶  辰欣药业股份有限公司  114001830704</FPBZ>
//    <GLMXBH>20141205010000000302</GLMXBH>
//    <XSDDH>124</XSDDH>
//    <SPLX>1</SPLX>
//    <SFCH>0</SFCH>
//    <GLBZ>1</GLBZ>
//    <WFGLSM>1</WFGLSM>
//    <ZXSPBM>X01130080010010</ZXSPBM>
//    <SCPH>1312282161</SCPH>
//    <PZWH>国药准字H20064829</PZWH>
//    <SPSL>84</SPSL>
//    <CGJLDW>1</CGJLDW>
//    <SCRQ>20131227</SCRQ>
//    <YXRQ>20151226</YXRQ>
//    <WSDJ>64.136752</WSDJ>
//    <HSDJ>75.04</HSDJ>
//    <SL>0.17</SL>
//    <SE>4361.30</SE>
//    <WSJE>100</WSJE>		
//    <HSJE>30016</HSJE>
//    <PFJ>75.0438</PFJ>
//    <LSJ>86.30</LSJ>
//  </STRUCT>
// <STRUCT>
//    <FPH>07106825</FPH>
//    <FPRQ>20140303</FPRQ>
//    <FPHSZJE>30016</FPHSZJE>
//    <DLCGBZ>0</DLCGBZ>
//    <FPBZ>003479  批号:1312272165 效期:2015-12-26 数量:400  中标 含税单价: 75.04 零: 86.30  王建峰,  203308 包装数量: 20  瓶  辰欣药业股份有限公司  114001830704</FPBZ>
//    <GLMXBH>20141205010000000301</GLMXBH>
//    <XSDDH>123</XSDDH>
//    <SPLX>1</SPLX>
//    <SFCH>0</SFCH>
//    <GLBZ>1</GLBZ>
//    <WFGLSM>1</WFGLSM>
//    <ZXSPBM>X03831250030010</ZXSPBM>
//    <SCPH>1312282161</SCPH>
//    <PZWH>国药准字H20064829</PZWH>
//    <SPSL>200</SPSL>
//    <CGJLDW>1</CGJLDW>
//    <SCRQ>20131227</SCRQ>
//    <YXRQ>20151226</YXRQ>
//    <WSDJ>64.136752</WSDJ>
//    <HSDJ>75.04</HSDJ>
//    <SL>0.17</SL>
//    <SE>4361.30</SE>
//    <WSJE>100</WSJE>		
//    <HSJE>30016</HSJE>
//    <PFJ>75.0438</PFJ>
//    <LSJ>86.30</LSJ>
//  </STRUCT>
//</DETAIL>
//</XMLDATA>";

//       //     richTextBox1.Text = SetMessage("YQ004", xmlData);

            FrmYQ004 uc = new FrmYQ004();
            uc.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmYQ005 uc = new FrmYQ005();
            uc.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
//         string    xmlData = "";
//            xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>
//<XMLDATA>
//	<HEAD>
//		<IP>10.1.21.40</IP>
//		<MAC>122334455667</MAC>
//		<BZXX>这里是备注</BZXX>
//	</HEAD>
//	<MAIN>
//		<CXLX>03</CXLX>
//		<CXBH>05719458</CXBH>
//	</MAIN>
//</XMLDATA>";

//            richTextBox1.Text = SendMessage.SetMessage("YQ015", xmlData);
        }

        private void button14_Click(object sender, EventArgs e)
        {
//            string    xmlData = "";
//            xmlData = @"<?xml version=""1.0""  encoding=""utf-8""?>
//<XMLDATA>
//<HEAD>
//<JSSJ>接收时间</JSSJ>
//<ZTCLJG>消息主体处理结果</ZTCLJG>
//<CWXX>错误提示内容</CWXX>
//<BZXX>备注信息</BZXX>
//</HEAD>
//<MAIN>
//<CLZT>处理状态</CLZT>
//<ZXBJ>询价单最新报价</ZXBJ>
//<PSDTM>配送单条码</PSDTM>
//<ZXSPBM>统编代码</ZXSPBM>
//</MAIN>
//</XMLDATA>";
//            TextReader tdr = new StringReader(xmlData);
//            DataSet ds = new DataSet();
//            ds.ReadXml(tdr);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FrmYQ017 uc = new FrmYQ017();
            uc.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FrmYQ018 uc = new FrmYQ018();
            uc.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FrmYQ013 uc = new FrmYQ013();
            uc.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //退货单查询
        private void button11_Click(object sender, EventArgs e)
        {
            FormReturnOrderQuery formReturnOrderQuery = new FormReturnOrderQuery();
            formReturnOrderQuery.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void InsertYYBM(string yymc,string yybm)
        {
            List<string> keys = new List<string>();
            keys.Add("YYMC");
            Dictionary<string,string> wheres = new Dictionary<string,string>();
            wheres.Add("YYMC", yymc);
            DataSet ds =  SqlHelper.Query(Program.ConnectionString, "ysxt_YyBM", keys,wheres,null);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return;
            }
            Dictionary<string,string> values = new Dictionary<string,string>();
            values.Add("YYMC",yymc);
            values.Add("YYBM",yybm);
            SqlHelper.Insert(Program.ConnectionString, "ysxt_YyBM", values);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Microsoft Excel(*.xls)|*.xls;*.xlsx";
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = ExcelHelper.QueryByODBC(fileDlg.FileName, "sheet1");
                if (ds.Tables.Count > 0)
                {
                    DataTable db = ds.Tables[0];
                    try
                    {
                        for (int i = 0; i < db.Rows.Count; ++i)
                        {
                            InsertYYBM(db.Rows[i][0].ToString(),db.Rows[i][1].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    MessageBox.Show("导入成功!");
                }
            }

        }



    }
}
