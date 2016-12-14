using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Key
{
    /// <summary>
    /// 用来调用CRM的WebService
    /// </summary>
    public class CRMWebServiceControl
    {
        /// <summary>
        /// 调用WS_System的Web服务,实现系统登录功能
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="OrgCode">组织编号(crm)</param>
        /// <param name="HostName">主机名称(SD:8080)</param>
        /// <returns>Web服务返回值</returns>
        public static string InvokeWeb_LoginIn(string UserName, string PassWord, string OrgCode, string HostName)
        {
            string SeiviceClassName = "WS_System";

            CompilerResults result = InvokeWebMethod(OrgCode, HostName, SeiviceClassName);

            // 5. 使用 Reflection 调用 WebService。
            if (!result.Errors.HasErrors)
            {
                Assembly asm = result.CompiledAssembly;

                Type t = asm.GetType("WS_System"); // 如果在前面为代理类添加了命名空间，此处需要将命名空间添加到类型前面。

                object o = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod("login");
                MethodInfo[] methods = t.GetMethods();
                try
                {
                    object LoginResult = method.Invoke(o, new object[] { UserName, PassWord, 0 }).ToString();
                    return LoginResult.ToString();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
                return "";
        }

        /// <summary>
        /// 调用WS_System的Web服务,实现系统登出功能
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="OrgCode">组织编号(crm)</param>
        /// <param name="HostName">主机名称(SD:8080)</param>
        /// <returns>Web服务返回值</returns>
        public static string InvokeWeb_LoginOut(string UserName, string PassWord, string OrgCode, string HostName)
        {
            string SeiviceClassName = "WS_System";

            CompilerResults result = InvokeWebMethod(OrgCode, HostName, SeiviceClassName);

            // 5. 使用 Reflection 调用 WebService。
            if (!result.Errors.HasErrors)
            {
                Assembly asm = result.CompiledAssembly;

                Type t = asm.GetType("WS_System"); // 如果在前面为代理类添加了命名空间，此处需要将命名空间添加到类型前面。

                object o = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod("logout");
                MethodInfo[] methods = t.GetMethods();
                try
                {
                    method.Invoke(o, null);
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
                return "";
        }

        /// <summary>
        /// 调用WS_EAI的Web服务,实现传递XML串的功能
        /// </summary>
        /// <param name="XMLString">需要传递的XML串</param>
        /// <param name="OrgCode">组织编号(crm)</param>
        /// <param name="HostName">主机名称(SD:8080)</param>
        /// <param name="SoapHeadString">SOAP头,Session验证</param>
        /// <returns>Web服务返回值</returns>
        public static string InvokeWeb_EAI(string XMLString, string OrgCode, string HostName, string SoapHeadString)
        {
            string SeiviceClassName = "WS_EAI";  //"WS_EAI";

            CompilerResults result = InvokeWebMethod(OrgCode, HostName, SeiviceClassName);

            // 5. 使用 Reflection 调用 WebService。
            if (!result.Errors.HasErrors)
            {
                Assembly asm = result.CompiledAssembly;

                Type soapHeader = asm.GetType("WSO_SOAPHeader"); // 如果在前面为代理类添加了命名空间，此处需要将命名空间添加到类型前面。
                object objHeader = Activator.CreateInstance(soapHeader);
                PropertyInfo HeaderPropertyInfo = soapHeader.GetProperty("PHPSESSID");
                HeaderPropertyInfo.SetValue(objHeader, SoapHeadString, null);

                Type wsEai = asm.GetType(SeiviceClassName); //"WS_EAI"); // 如果在前面为代理类添加了命名空间，此处需要将命名空间添加到类型前面。
                object objEAI = Activator.CreateInstance(wsEai);
                PropertyInfo EAIPropertyInfo = wsEai.GetProperty("WSO_SOAPHeaderValue");
                EAIPropertyInfo.SetValue(objEAI, objHeader, null);

                MethodInfo method = wsEai.GetMethod("process"); //add processU9
                try
                {

                    //XMLString = XMLString.Replace("\r", "");
                    //XMLString = XMLString.Replace(Convert.ToChar(10).ToString() , ",");
                    //XMLString = XMLString.Replace(Convert.ToChar(13).ToString(), ",");

                    
                    return method.Invoke(objEAI, new object[] { XMLString }).ToString();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
                return "";
        }

        /// <summary>
        /// 调用WebService公共方法
        /// </summary>
        /// <param name="OrgCode">组织编号(crm)</param>
        /// <param name="HostName">主机名称(SD:8080)</param>
        /// <param name="SeiviceClassName">服务名称</param>
        /// <returns>返回Web服务调用的类</returns>
        private static CompilerResults InvokeWebMethod(string OrgCode, string HostName, string SeiviceClassName)
        {
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(@"http://" + HostName + "/webservice/service.php?orgcode=" + OrgCode + "&class=" + SeiviceClassName + "&wsdl");

            // 2. 创建和格式化 WSDL 文档。
            ServiceDescription description = ServiceDescription.Read(stream);

            // 3. 创建客户端代理代理类。
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();

            importer.ProtocolName = "Soap"; // 指定访问协议。
            importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;

            importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。

            // 4. 使用 CodeDom 编译客户端代理类。
            CodeNamespace nmspace = new CodeNamespace(); // 为代理类添加命名空间，缺省为全局空间。
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);

            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.GenerateInMemory = true;

            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");

            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            return result;
        }
    }
}

