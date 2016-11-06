using FKHY.Common;
using FKHY.Models.DBModels;
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
                //db.WebLogons.Where(r => r.UserId == userId && r.UserTypeId == userTypeId).ToList().ForEach(r => db.WebLogons.Remove(r));
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

        public static dynamic Logon(string loginName, string password, bool? rememberPassword, int? userTypeId)
        {
            loginName = loginName.Trim();
            dynamic returdynamic = new ExpandoObject();
            using (DataAccess db = new DataAccess())
            {
                if (userTypeId == null)
                {
                    userTypeId = Constants.UserType.企业;
                }
                if (userTypeId == Constants.UserType.企业)
                {
                    var user = db.Companies.SingleOrDefault(r => r.Mobile == loginName || r.Email == loginName);
                    if (user != null)
                    {
                        string salt = user.Salt;
                        string hashedPswd = user.Password;
                        if (Helper.MD5(password) == hashedPswd)
                        //判断是否登录合法站点。
                        {
                            var userLogons = db.WebLogons.Where(r => r.UserId == user.CompanyId && r.UserTypeId == userTypeId).OrderByDescending(r => r.DateLogon).FirstOrDefault();
                            if (userLogons != null)
                            {
                                user.LastLogonDate = userLogons.DateLogon;
                                db.SaveChanges();
                            }
                            //用户手动登陆后,必须从新赋予COOKIE和TOKEN
                            RefreshLogon(user.CompanyId, Constants.UserType.企业, rememberPassword ?? false);
                            returdynamic.status = 0;
                            return returdynamic;
                        }
                        else
                        {
                            returdynamic.status = 2;
                            return returdynamic;
                        }
                    }
                    else
                    {
                        userTypeId = Constants.UserType.服务商;
                    }
                }
                if (userTypeId == Constants.UserType.服务商)
                {
                    var user = db.ServiceProviders.SingleOrDefault(r => r.Mobile == loginName || r.Email == loginName);
                    if (user != null)
                    {
                        string salt = user.Salt;
                        string hashedPswd = user.Password;
                        if (Helper.MD5(password) == hashedPswd)
                        //判断是否登录合法站点。
                        {
                            var userLogons = db.WebLogons.Where(r => r.UserId == user.ServiceProviderId && r.UserTypeId == userTypeId).OrderByDescending(r => r.DateLogon).FirstOrDefault();
                            if (userLogons != null)
                            {
                                user.LastLogonDate = userLogons.DateLogon;
                                db.SaveChanges();
                            }
                            //用户手动登陆后,必须从新赋予COOKIE和TOKEN
                            RefreshLogon(user.ServiceProviderId, Constants.UserType.服务商, rememberPassword ?? false);
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

        public static string GetUserName(long? userId, int userTypeId)
        {
            if (userId.HasValue && userTypeId == Constants.UserType.企业)
            {
                using (WebDataAccess db = new WebDataAccess())
                {
                    var c = db.Companies.Single(r => r.CompanyId == userId);
                    return string.IsNullOrEmpty(c.FullName) ? (string.IsNullOrEmpty(c.Email) ? c.Mobile : c.Email) : c.FullName;
                }
            }
            else if (userId.HasValue && userTypeId == Constants.UserType.服务商)
            {
                using (WebDataAccess db = new WebDataAccess())
                {
                    var c = db.ServiceProviders.Single(r => r.ServiceProviderId == userId);
                    return string.IsNullOrEmpty(c.FullName) ? (string.IsNullOrEmpty(c.Email) ? c.Mobile : c.Email) : c.FullName;
                }
            }
            return string.Empty;
        }
        public static int GetVerified(long? userId, int userTypeId)
        {
            if (userId.HasValue && userTypeId == Constants.UserType.企业)
            {
                using (WebDataAccess db = new WebDataAccess())
                {
                    var c = db.Companies.Single(r => r.CompanyId == userId);

                    if (c.VerifyStatusId == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return c.VerifyStatusId.Value;
                    }
                }
            }
            else if (userId.HasValue && userTypeId == Constants.UserType.服务商)
            {
                using (WebDataAccess db = new WebDataAccess())
                {
                    var c = db.ServiceProviders.Single(r => r.ServiceProviderId == userId);

                    if (c.VerifyStatusId == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return c.VerifyStatusId.Value;
                    }
                }
            }
            return 0;
        }
    }
}
