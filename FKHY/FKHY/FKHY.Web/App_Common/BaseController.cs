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

namespace FKHY.Web
{
    public class BaseController : Controller
    {
        //protected AdminDataAccess dbAdmin = new AdminDataAccess();
        //protected PaymentDataAccess dbPayment = new PaymentDataAccess();
        //protected WebDataAccess dbWeb = new WebDataAccess();
        //protected MessageDataAccess dbMessage = new MessageDataAccess();
        
        public long? UserId
        {
            get
            {
                if (Session == null || Session["UserId"] == null)
                {
                    return null;
                }
                return long.Parse(Session["UserId"].ToString());
            }
            set
            {
                Session["UserId"] = value;
            }
        }

        public int? UserTypeId
        {
            get
            {
                if (Session == null || Session["UserTypeId"] == null)
                {
                    return null;
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
                    //string UserName = ZhiNangTuan.Framework.BusinessLogic.UserLogic.GetUserName(UserId, UserTypeId.Value);
                    //if (!string.IsNullOrEmpty(UserName))
                    //    Session["UserName"] = UserName;
                    return "";
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
                if (Session == null || Session["ReturnUrl"] == null)
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
            if (UserId.HasValue)
            {
                ViewBag.BaseUserId = UserId;
                ViewBag.BaseUserName = UserName;
            }
            base.OnActionExecuting(context);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected void ClearSession()
        {
            UserId = null;
            UserName = null;
            UserTypeId = null;
            ReturnUrl = null;
        }

        [HttpPost]
        public ActionResult Logout()
        {
            ClearSession();
            return Redirect(ReturnUrl);
        }

        #region 用户登录

        [HttpPost]
        public JsonResult UserLogIn(string loginName, string password, bool rememberMe)//loginName mobile or email
        {
            dynamic login = "";//UserLogic.Logon(loginName, password, rememberMe, null);//change

            if (login.status == 0)
            {
                return Json(new
                {
                    status = 0
                });
            }
            else
            {
                return Json(new
                {
                    status = login.status
                });
            }
        }
        #endregion
    }
}
