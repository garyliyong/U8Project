using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginUserControl
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public static string UserPwd { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public static DateTime LoginDate { get; set; }

    }
}
