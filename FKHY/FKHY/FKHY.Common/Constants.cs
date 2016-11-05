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
    }
}
