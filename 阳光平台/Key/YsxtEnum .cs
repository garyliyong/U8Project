using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SHYSInterface
{
    class YsxtEnum
    {
        //4.1.3 验收情况
        public enum YSQK
        {

            //[Description("验收通过，货品已入库")]
            //YStG = 1          ,
            //[Description("验收未通过，货品全退")]
            //YSwtg = 2,
            //[Description("验收未通过或未验收，货品搁置")]
            //Wys = 3
        }
        public enum DDLX
        {
            医院自行订单=1,
            托管药库订单=2
        }

    }
}
