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

        public static string TempFolder = "/upload/temp/";
        public const string StudentFolder = "/Students/";
        public const string TeacherFolder = "/Teachers/";
        public const string AdminFolder = "/Admin/";
        public const string TimeShortFormat = "yyyy-MM-dd";

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
            中级 = 48,
            高级 = 52,
            套餐1 = 96,
            套餐2 = 100,
            套餐3 = 148,
            套餐4 = 164
        }
    }
}
