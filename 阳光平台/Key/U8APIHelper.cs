using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using UFIDA.U8.Portal.Framework.Commands;
using UFIDA.U8.Portal.Proxy.Accessory;
//需要添加以下命名空间
using UFIDA.U8.MomServiceCommon;
using UFIDA.U8.U8MOMAPIFramework;
using UFIDA.U8.U8APIFramework;
using UFIDA.U8.U8APIFramework.Meta;
using UFIDA.U8.U8APIFramework.Parameter;
using MSXML2;
using System.Data.SqlClient;

namespace UFIDA.U8.Cust.OpenNewVouchers.U8API
{
    public class U8APIHelper
    {
        public static void ExcuteJoinQueryU8Voucher(U8Login.clsLogin u8Login, string menuID, string menuName, string subID, string authID, string cardNumber, string pkFieldValue)
        {
            string cmdLine = "";
            cmdLine = "ID:{0}&&&Name:{1}&&&SubSysID:{2}&&&AuthID:{3}&&&CMDLINE:{4}\t{5}";

            cmdLine = string.Format(cmdLine, menuID, menuName, subID, authID, cardNumber, pkFieldValue);
            IPortalCommandOperator portalCommandOperator = new PortalCommandOperator();

            IPortalCommandArgs portalCommandArgs = new PortalCommandArgs(menuID, subID);

            portalCommandArgs.AuthId = authID;

            portalCommandArgs.DocId = string.Empty;

            portalCommandArgs.DocType = string.Empty;

            portalCommandArgs.ID = menuID;



            portalCommandArgs.Name = menuName;

            portalCommandArgs.SubFunction = string.Empty;

            portalCommandArgs.Extenision = string.Empty;

            portalCommandArgs.SubSysID = subID;

            portalCommandArgs.CmdLine = cmdLine;

            portalCommandArgs.FromUserClick = false;

            portalCommandArgs.ExtProperties.Add("recheck", "1");

            portalCommandOperator.RunBusiness(portalCommandArgs);
        }
        public static void ExcuteJoinQueryUAPVoucher(U8Login.clsLogin u8Login, string menuID, string menuName, string subID, string authID, string cardNumber, string pkFieldName, string pkFieldValue)
        {

            //"ID:PUM030101&&&Name:采¨¦购o管¨¹理¤¨ª&&&SubSysID:PU&&&AuthID:PU04100101&&&CMDLINE:27\t0000000005\tPUM030101\t\t001\t2008\t\t";

            string cmdLine = "";

            cmdLine = string.Format("<property cardnum=\"{0}\" type=\"voucher\"><voucherid  key=\"{1}\" value=\"{2}\"/></property>",

        cardNumber, pkFieldName, pkFieldValue);


            IPortalCommandOperator portalCommandOperator = new PortalCommandOperator();

            IPortalCommandArgs portalCommandArgs = new PortalCommandArgs(menuID, subID);

            portalCommandArgs.AuthId = authID;

            portalCommandArgs.DocId = string.Empty;

            portalCommandArgs.DocType = string.Empty;

            portalCommandArgs.ID = menuID;



            portalCommandArgs.Name = menuName;

            portalCommandArgs.SubFunction = string.Empty;

            portalCommandArgs.Extenision = string.Empty;

            portalCommandArgs.SubSysID = subID;

            portalCommandArgs.CmdLine = cmdLine;

            portalCommandArgs.FromUserClick = false;

            portalCommandArgs.ExtProperties.Add("recheck", "1");

            portalCommandOperator.RunBusiness(portalCommandArgs);


        }

        public static void FormatDom(ref MSXML2.DOMDocument SourceDom, int rows, string editprop)
        {
            //IXMLDOMElement element;
            //IXMLDOMElement ele_head;
            IXMLDOMElement ele_body;
            //IXMLDOMNode nd;
            //MSXML2.DOMDocument tempnd;
            IXMLDOMNodeList ndheadlist;
            IXMLDOMNodeList ndbodylist;
            //DistDom.loadXML("SourceDom.xml");
            String Filedname;
            //'格式部分
            ndheadlist = SourceDom.selectNodes("//s:Schema/s:ElementType/s:AttributeType");
            ndbodylist = SourceDom.selectNodes("//rs:data/z:row");
            if (ndbodylist.length == 0)
            {
                ele_body = SourceDom.createElement("z:row");
                //ele_body.setAttribute("id","");
                for (int i = 0; i <= rows; i++)
                {
                    SourceDom.selectSingleNode("//rs:data").appendChild(ele_body);
                }

                //SourceDom.selectSingleNode("//rs:data/z:row").s
            }
            ndbodylist = SourceDom.selectNodes("//rs:data/z:row");
            foreach (IXMLDOMElement body in ndbodylist)
            {
                foreach (IXMLDOMElement head in ndheadlist)
                {
                    Filedname = head.attributes.getNamedItem("name").nodeValue + "";
                    if (body.attributes.getNamedItem(Filedname) == null)
                        //  '若没有当前元素，就增加当前元素
                        body.setAttribute(Filedname, "");
                    switch (head.lastChild.attributes.getNamedItem("dt:type").nodeValue.ToString())
                    {
                        case "number":
                        case "float":
                        case "boolean":
                            if (body.attributes.getNamedItem(Filedname).nodeValue.ToString().ToUpper() == "false".ToUpper())
                                body.setAttribute(Filedname, 0);
                            break;
                        default:


                            if (body.attributes.getNamedItem(Filedname).nodeValue.ToString().ToUpper() == "否".ToUpper())
                                body.setAttribute(Filedname, 0);
                            break;
                    }

                }
                if (editprop != "")
                    body.setAttribute("editprop", editprop);
            }


 
        }

        public static void FormatDom(ref MSXML2.DOMDocument SourceDom, string editprop)
        {
            //IXMLDOMElement element;
            //IXMLDOMElement ele_head;
            IXMLDOMElement ele_body;
            //IXMLDOMNode nd;
            //MSXML2.DOMDocument tempnd;
            IXMLDOMNodeList ndheadlist;
            IXMLDOMNodeList ndbodylist;
            //DistDom.loadXML("SourceDom.xml");
            String Filedname;
            //'格式部分
            ndheadlist = SourceDom.selectNodes("//s:Schema/s:ElementType/s:AttributeType");
            ndbodylist = SourceDom.selectNodes("//rs:data/z:row");
            if (ndbodylist.length == 0)
            {
                ele_body = SourceDom.createElement("z:row");
                SourceDom.selectSingleNode("//rs:data").appendChild(ele_body);
            }
            ndbodylist = SourceDom.selectNodes("//rs:data/z:row");
            foreach (IXMLDOMElement body in ndbodylist)
            {
                foreach (IXMLDOMElement head in ndheadlist)
                {
                    Filedname = head.attributes.getNamedItem("name").nodeValue + "";
                    if (body.attributes.getNamedItem(Filedname) == null)
                        //  '若没有当前元素，就增加当前元素
                        body.setAttribute(Filedname, "");
                    switch (head.lastChild.attributes.getNamedItem("dt:type").nodeValue.ToString())
                    {
                        case "number":
                        case "float":
                        case "boolean":
                            if (body.attributes.getNamedItem(Filedname).nodeValue.ToString().ToUpper() == "false".ToUpper())
                                body.setAttribute(Filedname, 0);
                            break;
                        default:


                            if (body.attributes.getNamedItem(Filedname).nodeValue.ToString().ToUpper() == "否".ToUpper())
                                body.setAttribute(Filedname, 0);
                            break;
                    }

                }
                if (editprop != "")
                    body.setAttribute("editprop", editprop);
            }


 
        }
        
    }
}
