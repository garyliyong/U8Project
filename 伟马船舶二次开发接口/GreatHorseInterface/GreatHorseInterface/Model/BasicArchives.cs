using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LoginUserControl.Model;

namespace GreatHorseInterface.Model
{
    /// <summary>
    /// 基础档案
    /// </summary>
    [XmlRoot("BasicArchive")]
    public class BasicArchive
    {
        /// <summary>
        /// 船舶号
        /// </summary>
        public string VesselCode { get; set; }

        /// <summary>
        /// U8帐套号
        /// </summary>
        public string U8Account { get; set; }

        /// <summary>
        /// U8帐套名称
        /// </summary>
        public string U8AccountName { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public string Year { get; set; }

        [XmlIgnore]
        public string DataBase
        {
            get
            {
                return "UFDATA_" + U8Account + "_" + Year;
            }
        }
    }

    /// <summary>
    /// 基础档案列表
    /// </summary>
    [XmlRoot("BasicArchives")]
    public class BasicArchives : List<BasicArchive>
    {
        /// <summary>
        /// 判断船舶号与基础档案信息是否匹配
        /// </summary>
        /// <param name="vesselCode"></param>
        /// <returns></returns>
        public bool FindByVesselCode(string vesselCode)
        {
            for (int index = 0; index < Count; ++index)
            {
                if (this.ElementAt(index).VesselCode == vesselCode &&
                    this.ElementAt(index).U8Account == Account.Current.cAcc_Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
