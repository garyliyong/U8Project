using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreatHorseInterface.Model;
using DataBaseHelper;
using System.Xml;
using System.Windows.Forms;
using System.Data;

namespace GreatHorseInterface.ViewModel
{
    /// <summary>
    /// 基础档案视图模型
    /// </summary>
    public class BasicArchiveViewModel
    {
        private BasicArchives archives = null;

        public BasicArchives Archives 
        {
            get
            {
                if (archives == null)
                {
                    archives = new BasicArchives();
                }
                return archives;
            }
        }

        public string BasicArchivePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"\BasicArchive.xml";
            }
        }

        public void Load()
        {
            XmlReader reader = new XmlTextReader(BasicArchivePath);
            try
            {
                BasicArchives basicArchives = XMLHelper.UnSerializer(reader, typeof(BasicArchives)) as BasicArchives;
                if (basicArchives != null)
                {
                    foreach (var value in basicArchives)
                    {
                        Archives.Add(value);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        public void Save()
        {
            XmlWriter writer = new XmlTextWriter(BasicArchivePath,Encoding.UTF8);
            try
            {
                XMLHelper.Serializer(writer, Archives, typeof(BasicArchives));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer.Close();
            }
            
        }
    }
}
