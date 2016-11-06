using FKHY.BizLogic.Login;
using FKHY.Models.CustomerModels;
using FKHY.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FKHY.Web.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(UserRegisterViewModel a)
        {
            using (var db = new DataAccess())
            {
                var model = db.Students.FirstOrDefault(p => p.Phone == a.Phone);

                if (model != null && model.Code == a.Code)
                {
                    model.Valid = true;
                    model.Password = a.Password;
                    model.DataChange_LastTime = DateTime.Now;
                    db.SaveChanges();
                    return Json(true);
                }
            }
            return Json(false);
        }

        public ActionResult Logout()
        {
            ClearSession();
            return Redirect(ReturnUrl);
        }

        [HttpPost]
        public JsonResult UserLogIn(UserRegisterViewModel a)
        {
            var loginType = a.Phone.StartsWith("t");
            var userType = 0;

            if (loginType)
            {
                userType = 1;
            }

            dynamic login = UserLogic.Logon(a.Phone, a.Password, a.Rem, userType);

            if (login.status == 0)
            {
                ViewBag.UserTypeId = this.UserTypeId = userType;
                ViewBag.UserId = this.UserId = login.userid;
                ViewBag.UserName = this.UserName = login.name;

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

        [HttpPost]
        public JsonResult SendVerifyMobileCode(UserRegisterViewModel a)
        {
            var code = UserLogic.SendValidCode(a.Phone);
            return Json(code);
        }
    }
}