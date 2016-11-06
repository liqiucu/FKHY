using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKHY.Common
{
    public class Constants
    {
        public static string WebUrl = System.Configuration.ConfigurationManager.AppSettings["WebUrl"];
        public static string FileServer = System.Configuration.ConfigurationManager.AppSettings["FileServer"];
        public static string WebLoginUrl = WebUrl + "/Home/userlogin?userType=1";
        public static string WebLogoutUrl = WebUrl + "/base/logout";
        public static string WebCheckCodeUrl = WebUrl + "/account/checkcode";

        public static int 老师 = 1;
        public static int 学生 = 2;

        public enum ConfirmPayStatus
        {
            未确认付款 = 0,
            确认付款 = 1
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        public enum TradeType
        {
            VIP = 0,
            购买套餐 = 1
        }

        public enum PackageType
        {
            基础 = 16,
            初级 = 48,
            中级 = 96,
            高级 = 128,
            套餐1 = 150,
            套餐2 = 180,
            套餐3 = 200,
            套餐4 = 220
        }
    }
}
