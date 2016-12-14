using System;
using System.Collections.Generic;
using System.Text;

namespace SHYSInterface
{
    class ClsSystem
    {
        public static String gnvl(Object objvalue1, Object objvalue2)
        {
            try
            {
                if (objvalue1.ToString() == "" || objvalue1 == null)
                    return ChangeComment(objvalue2.ToString());
                return ChangeComment(objvalue1.ToString());
            }
            catch (Exception)
            {
                return objvalue2.ToString();
            }

        }
        public static Boolean  isnull(Object objvalue)
        {
            try
            {
                if (objvalue == null)
                    return true;
                if (objvalue.ToString() == "")
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        //得到objDateTime(时间)　加上　intspan(数值)　个 Type(yyyy,mm,dd)　 后的值
        public static String getDatetime(DateTime objDateTime, Double intspan, String Type)
        {
            String getDatetime = "";
            try
            {
                switch (Type)
                {
                    case "y":
                        getDatetime = objDateTime.AddYears(Convert.ToInt16(intspan)).ToString();
                        break;
                    case "M":
                        getDatetime = objDateTime.AddMonths(Convert.ToInt16(intspan)).ToString();
                        break;
                    case "d":
                        getDatetime = objDateTime.AddDays(intspan).ToString();
                        break;
                    case "h":
                        getDatetime = objDateTime.AddHours(intspan).ToString();
                        break;
                    case "m":
                        getDatetime = objDateTime.AddMinutes(intspan).ToString();
                        break;
                    case "s":
                        getDatetime = objDateTime.AddSeconds(intspan).ToString();
                        break;
                }
                return getDatetime;
            }
            catch (Exception)
            {
                return "";
            }
        }
        //得到两个时间的差
        public static double   DateDiff(DateTime DateTime1, DateTime DateTime2, string Type)
        {
            double dateDiff = 0;

            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                switch (Type)
                {
                    case "y":
                        dateDiff = Math.Floor(ts.TotalDays/365);
                        break;
                    case "M":
                        dateDiff = Math.Floor((ts.TotalDays / 365-Math.Floor(ts.TotalDays / 365))*12);
                        break;
                    case "d":
                        dateDiff = ts.TotalDays;
                        break;
                    case "h":
                        dateDiff = ts.TotalHours;
                        break;
                    case "m":
                        dateDiff = ts.TotalMinutes;
                        break;
                    case "s":
                        dateDiff = ts.TotalSeconds;
                        break;
                }
                return dateDiff;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static Boolean checkdate(string objdate)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(objdate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static String ChangeComment(String strDat)
        {

            int li;
            String  strss="";
            
            for( li = 0;li<=strDat.Length-1;li++)
            {
                if (strDat.Substring(li, 1).ToString() == "'")
                {
                        strss = strss + "''";
                }
                else
                {
                    strss = strss + strDat.Substring(li, 1).ToString();
                }
            }
            return strss;

        }
    }
}
