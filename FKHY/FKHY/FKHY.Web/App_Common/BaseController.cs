using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Caching;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.IO;
using FKHY.Common;
using FKHY.Models.DBModels;
using FKHY.BizLogic.Login;

namespace FKHY.Web
{
    public class BaseController : Controller
    {
        protected DataAccess dbAdmin = new DataAccess();
        public bool RememberMe
        {
            get
            {
                // check cookie and assign it to session
                HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                    if (!string.IsNullOrEmpty(ticket.UserData))
                    {
                        int? userId = ticket.Name.Split(',')[0].ToInt();
                        int? userTypeId = ticket.Name.Split(',')[1].ToInt();
                        Session["UserId"] = userId;
                        Session["UserTypeId"] = userTypeId;
                        return true;
                    }
                }
                return false;
            }
        }
        public long UserId
        {
            get
            {
                if (Session == null || Session["UserId"] == null)
                {
                    if (RememberMe)
                    {
                        return long.Parse(Session["UserId"].ToString());
                    }
                    return 0;
                }
                return long.Parse(Session["UserId"].ToString());
            }
            set
            {
                Session["UserId"] = value;
            }
        }

        public int UserTypeId
        {
            get
            {
                if (Session == null || Session["UserTypeId"] == null)
                {
                    if (RememberMe)
                    {
                        return int.Parse(Session["UserTypeId"].ToString());
                    }
                    return 0;
                }
                return int.Parse(Session["UserTypeId"].ToString());
            }
            set
            {
                Session["UserTypeId"] = value;
            }
        }

        public string UserName
        {
            get
            {
                if (Session == null || Session["UserName"] == null)
                {
                    string UserName = "";
                    if (UserTypeId==0 && UserId>0)
                    {
                        var temp = UserLogic.GetStudent(UserId);
                        
                        if (string.IsNullOrEmpty(temp.UserName))
                        {
                            UserName = temp.Phone;
                        }
                    }
                    if (UserTypeId == 1 && UserId > 0)
                    {
                        var temp = UserLogic.GetTeacher(UserId);

                        if (string.IsNullOrEmpty(temp.TeacherName))
                        {
                            UserName = temp.Phone;
                        }
                    }
                    return UserName;
                }
                return Session["UserName"].ToString();
            }
            set
            {
                Session["UserName"] = value;
            }
        }

        public string ReturnUrl
        {
            get
            {
                if (Session == null || Session["ReturnUrl"]==null)
                {
                    return Constants.WebUrl;
                }
                return Session["ReturnUrl"].ToString();
            }
            set
            {
                Session["ReturnUrl"] = value;
            }
        }

        protected override void Initialize(System.Web.Routing.RequestContext context)
        {
            base.Initialize(context);
            if (!Request.IsAjaxRequest() &&
                !context.HttpContext.Request.Url.AbsoluteUri.ToLower().StartsWith(Constants.WebLoginUrl) &&
                !context.HttpContext.Request.Url.AbsoluteUri.ToLower().StartsWith(Constants.WebLogoutUrl) &&
                !context.HttpContext.Request.Url.AbsoluteUri.ToLower().StartsWith(Constants.WebCheckCodeUrl)
                )
            {
                ReturnUrl = context.HttpContext.Request.Url.AbsoluteUri;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.BaseController = context.RouteData.Values["controller"].ToString().ToLower();
            ViewBag.BaseReturnUrl = ReturnUrl;

            if (UserId>0)
            {
                ViewBag.UserId = UserId;
                ViewBag.UserName = UserName;
            }
            base.OnActionExecuting(context);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected void ClearSession()
        {
            Session["ReturnUrl"] = null;
            Session["UserId"] = null;
            Session["UserTypeI"] = null;
            Session["UserName"] = null;
            ViewBag.ReturnUrl = null;
            ViewBag.UserId = null;
            ViewBag.UserTypeI = null;
            ViewBag.UserName = null;

            UserId = 0;
            UserName = "";
            UserTypeId = 0;
            ReturnUrl = "";
        }
    }
}
