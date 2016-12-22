using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DataBaseHelper
{
    /// <summary>
    /// xml工具类
    /// </summary>
    public class XMLHelper
    {
        public static object UnSerializer(XmlReader reader, Type type)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            return xmlSerializer.Deserialize(reader);
            
        }

        public static void Serializer(XmlWriter writer, object obj, Type type)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);
            xmlSerializer.Serialize(writer, obj);
        }
    }
}
