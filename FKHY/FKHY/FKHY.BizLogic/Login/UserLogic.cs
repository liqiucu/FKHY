using FKHY.Common;
using FKHY.Models.DBModels;
using FKHY.Web.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FKHY.BizLogic.Login
{
    public class UserLogic
    {
        public static void RefreshLogon(long userId, int userTypeId, bool rememberPassword)
        {
            DeleteOldLogons(userId, userTypeId);
            CreateLogon(userId, userTypeId, rememberPassword);
        }
        public static void DeleteOldLogons(long userId, int userTypeId)
        {
            //删除所有旧登陆记录
            CookieHelper.DeleteAuthCookie();
            using (DataAccess db = new DataAccess())
            {
                db.WebLogons.Where(r => r.UserId == userId && r.UserTypeId == userTypeId).ToList().ForEach(r => db.WebLogons.Remove(r));
                db.SaveChanges();
            }
        }

        private static void CreateLogon(long userId, int userTypeId, bool rememberPassword)
        {
            using (DataAccess db = new DataAccess())
            {
                var newLogon = new WebLogon()
                {
                    UserId = userId,
                    UserTypeId = userTypeId,
                    Token = Guid.NewGuid().ToString(),
                    TokenExpiryDate = DateTime.Now.AddMonths(1),
                    IPAddress = HttpContext.Current.Request.UserHostAddress,
                    DateLogon = DateTime.Now
                };

                db.WebLogons.Add(newLogon);
                db.SaveChanges();

                CookieHelper.DeleteAuthCookie();
                if (rememberPassword)
                {
                    CookieHelper.SetAuthCookie(userId, userTypeId, newLogon.Token);
                }
                else
                {
                    CookieHelper.SetAuthCookie(userId, userTypeId, string.Empty);
                }
                HttpContext.Current.Session["UserId"] = userId;
                HttpContext.Current.Session["UserTypeId"] = userTypeId;
            }
        }

        public static dynamic Logon(string loginName, string password, bool rememberPassword, int userTypeId)
        {
            loginName = loginName.Trim();
            dynamic returdynamic = new ExpandoObject();
            using (DataAccess db = new DataAccess())
            {
                if (userTypeId == Constants.学生)
                {
                    var user = db.Students.SingleOrDefault(r => r.Phone == loginName);

                    if (user != null)
                    {
                        string salt = user.Salt;
                        string hashedPswd = user.Password;
                        if (FKHYCommon.MD5(password) == hashedPswd)//判断是否登录合法站点。
                        {
                            //用户手动登陆后,必须从新赋予COOKIE和TOKEN
                            RefreshLogon(user.UserId, Constants.学生, rememberPassword);
                            returdynamic.status = 0;
                            return returdynamic;
                        }
                        else
                        {
                            returdynamic.status = 2;
                            return returdynamic;
                        }
                    }
                }
                else
                {
                    var user = db.Teachers.SingleOrDefault(r => r.Phone == loginName);
                    if (user != null)
                    {
                        string salt = user.Salt;
                        string hashedPswd = user.Password;
                        if (FKHYCommon.MD5(password) == hashedPswd)//判断是否登录合法站点。
                        {
                            var userLogons = db.WebLogons.Where(r => r.UserId == user.TeacherId && r.UserTypeId == userTypeId).OrderByDescending(r => r.DateLogon).FirstOrDefault();
                            //用户手动登陆后,必须从新赋予COOKIE和TOKEN
                            RefreshLogon(user.TeacherId, Constants.老师, rememberPassword);
                            returdynamic.status = 0;
                            return returdynamic;
                        }
                        else
                        {
                            returdynamic.status = 2;
                            return returdynamic;
                        }
                    }
                }
            }
            returdynamic.status = 1;
            return returdynamic;
        }

        public static Student GetStudent(long userId)
        {
            if (userId> 0)
            {
                using (DataAccess db = new DataAccess())
                {
                    var c = db.Students.Single(r => r.UserId == userId);
                    return c;
                }
            }
            return null;
        }

        public static Teacher GetTeacher(long teacherId)
        {
            if (teacherId > 0)
            {
                using (DataAccess db = new DataAccess())
                {
                    var c = db.Teachers.Single(r => r.TeacherId == teacherId);
                    return c;
                }
            }
            return null;
        }
    }
}
