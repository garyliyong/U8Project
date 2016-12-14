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
    /// ��������CRM��WebService
    /// </summary>
    public class CRMWebServiceControl
    {
        /// <summary>
        /// ����WS_System��Web����,ʵ��ϵͳ��¼����
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <param name="PassWord">����</param>
        /// <param name="OrgCode">��֯���(crm)</param>
        /// <param name="HostName">��������(SD:8080)</param>
        /// <returns>Web���񷵻�ֵ</returns>
        public static string InvokeWeb_LoginIn(string UserName, string PassWord, string OrgCode, string HostName)
        {
            string SeiviceClassName = "WS_System";

            CompilerResults result = InvokeWebMethod(OrgCode, HostName, SeiviceClassName);

            // 5. ʹ�� Reflection ���� WebService��
            if (!result.Errors.HasErrors)
            {
                Assembly asm = result.CompiledAssembly;

                Type t = asm.GetType("WS_System"); // �����ǰ��Ϊ����������������ռ䣬�˴���Ҫ�������ռ���ӵ�����ǰ�档

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
        /// ����WS_System��Web����,ʵ��ϵͳ�ǳ�����
        /// </summary>
        /// <param name="UserName">�û���</param>
        /// <param name="PassWord">����</param>
        /// <param name="OrgCode">��֯���(crm)</param>
        /// <param name="HostName">��������(SD:8080)</param>
        /// <returns>Web���񷵻�ֵ</returns>
        public static string InvokeWeb_LoginOut(string UserName, string PassWord, string OrgCode, string HostName)
        {
            string SeiviceClassName = "WS_System";

            CompilerResults result = InvokeWebMethod(OrgCode, HostName, SeiviceClassName);

            // 5. ʹ�� Reflection ���� WebService��
            if (!result.Errors.HasErrors)
            {
                Assembly asm = result.CompiledAssembly;

                Type t = asm.GetType("WS_System"); // �����ǰ��Ϊ����������������ռ䣬�˴���Ҫ�������ռ���ӵ�����ǰ�档

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
        /// ����WS_EAI��Web����,ʵ�ִ���XML���Ĺ���
        /// </summary>
        /// <param name="XMLString">��Ҫ���ݵ�XML��</param>
        /// <param name="OrgCode">��֯���(crm)</param>
        /// <param name="HostName">��������(SD:8080)</param>
        /// <param name="SoapHeadString">SOAPͷ,Session��֤</param>
        /// <returns>Web���񷵻�ֵ</returns>
        public static string InvokeWeb_EAI(string XMLString, string OrgCode, string HostName, string SoapHeadString)
        {
            string SeiviceClassName = "WS_EAI";  //"WS_EAI";

            CompilerResults result = InvokeWebMethod(OrgCode, HostName, SeiviceClassName);

            // 5. ʹ�� Reflection ���� WebService��
            if (!result.Errors.HasErrors)
            {
                Assembly asm = result.CompiledAssembly;

                Type soapHeader = asm.GetType("WSO_SOAPHeader"); // �����ǰ��Ϊ����������������ռ䣬�˴���Ҫ�������ռ���ӵ�����ǰ�档
                object objHeader = Activator.CreateInstance(soapHeader);
                PropertyInfo HeaderPropertyInfo = soapHeader.GetProperty("PHPSESSID");
                HeaderPropertyInfo.SetValue(objHeader, SoapHeadString, null);

                Type wsEai = asm.GetType(SeiviceClassName); //"WS_EAI"); // �����ǰ��Ϊ����������������ռ䣬�˴���Ҫ�������ռ���ӵ�����ǰ�档
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
        /// ����WebService��������
        /// </summary>
        /// <param name="OrgCode">��֯���(crm)</param>
        /// <param name="HostName">��������(SD:8080)</param>
        /// <param name="SeiviceClassName">��������</param>
        /// <returns>����Web������õ���</returns>
        private static CompilerResults InvokeWebMethod(string OrgCode, string HostName, string SeiviceClassName)
        {
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(@"http://" + HostName + "/webservice/service.php?orgcode=" + OrgCode + "&class=" + SeiviceClassName + "&wsdl");

            // 2. �����͸�ʽ�� WSDL �ĵ���
            ServiceDescription description = ServiceDescription.Read(stream);

            // 3. �����ͻ��˴�������ࡣ
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();

            importer.ProtocolName = "Soap"; // ָ������Э�顣
            importer.Style = ServiceDescriptionImportStyle.Client; // ���ɿͻ��˴���
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;

            importer.AddServiceDescription(description, null, null); // ��� WSDL �ĵ���

            // 4. ʹ�� CodeDom ����ͻ��˴����ࡣ
            CodeNamespace nmspace = new CodeNamespace(); // Ϊ��������������ռ䣬ȱʡΪȫ�ֿռ䡣
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

