using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace FKHY.Common
{
    public class CookieHelper
    {
        public static void DeleteCookie(string name)
        {
            string cookieKey = name;
            var cookie = new HttpCookie(cookieKey, null)
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public static string GetCookie(string name)
        {
            string cookie = string.Empty;
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(name))
            {
                cookie = HttpContext.Current.Request.Cookies[name].Value;
            }
            return cookie;
        }

        public static void SetCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Expires = DateTime.Now.AddYears(1);
            cookie.Value = value;
            cookie.Domain = FormsAuthentication.CookieDomain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SetAuthCookie(int adminId, string token)
        {
            FormsAuthentication.Initialize();
            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, adminId.ToString(), DateTime.Now, DateTime.Now.AddMonths(1), true, token);
            string cookiestr = FormsAuthentication.Encrypt(tkt);
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            ck.Expires = tkt.Expiration;
            ck.Path = FormsAuthentication.FormsCookiePath;
            ck.Domain = FormsAuthentication.CookieDomain;
            HttpContext.Current.Response.Cookies.Add(ck);
        }
        public static void SetAuthCookie(long userId, int userTypeId, string token)
        {
            FormsAuthentication.Initialize();
            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userId.ToString() + "," + userTypeId, DateTime.Now, DateTime.Now.AddMonths(1), true, token);
            string cookiestr = FormsAuthentication.Encrypt(tkt);
            HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            ck.Expires = tkt.Expiration;
            ck.Path = FormsAuthentication.FormsCookiePath;
            ck.Domain = FormsAuthentication.CookieDomain;
            HttpContext.Current.Response.Cookies.Add(ck);
        }
        public static void DeleteAuthCookie()
        {
            FormsAuthentication.SignOut();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            cookie1.Path = FormsAuthentication.FormsCookiePath;
            cookie1.Domain = FormsAuthentication.CookieDomain;
            HttpContext.Current.Response.Cookies.Add(cookie1);
        }
    }
}
