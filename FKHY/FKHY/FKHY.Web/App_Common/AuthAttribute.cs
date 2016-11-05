using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using FKHY.Common;

namespace FKHY.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 1=企业 =2猎头
        /// </summary>
        public int UserType { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (filterContext.HttpContext.Session["UserId"] != null || IsAuthenticated)
            {
                bool hasPermission = false;

                if (UserType == 0)
                {
                    hasPermission = true;
                }
                else
                {
                    if (filterContext.HttpContext.Session["UserTypeId"] != null && (int)filterContext.HttpContext.Session["UserTypeId"] == UserType)
                    {
                        hasPermission = true;
                    }
                }
                
                if (!hasPermission)
                {
                    if (!filterContext.HttpContext.Response.IsRequestBeingRedirected)
                    {
                        filterContext.Result = new RedirectResult(Constants.WebUrl);
                        return;
                    }
                }
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JavaScriptResult() { Script = "login();" };
                    return;
                }
                else
                {
                    if (!filterContext.HttpContext.Response.IsRequestBeingRedirected)
                    {
                        filterContext.Result = new RedirectResult(Constants.WebUrl);
                        return;
                    }
                }
            }
        }

        private bool IsAuthenticated
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    if (ticket.Name.Split(',').Length > 1)
                    {
                        int? userId = ticket.Name.Split(',')[0].ToInt();
                        int? userTypeId = ticket.Name.Split(',')[1].ToInt();

                        string token = ticket.UserData;

                        if (!string.IsNullOrEmpty(token))
                        {
                            HttpContext.Current.Session["UserId"] = userId;
                            HttpContext.Current.Session["UserTypeId"] = userTypeId;
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
