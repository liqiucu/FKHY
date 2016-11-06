using FKHY.Common;
using FKHY.Web.Convert;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace FKHY.Web.Common
{
    public class FKHYCommon
    {
        #region 上传
        public static string UploadTempImage(int maxRequest)
        {
            try
            {
                string newPath = Constants.TempFolder; //上传目录
                string path = "";
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpPostedFile Fupload = HttpContext.Current.Request.Files[0];

                    //判断文件格式
                    string sExt = Fupload.FileName.Substring(Fupload.FileName.LastIndexOf(".")).ToLower();
                    if (!CheckValidExt(sExt))
                    {
                        string str = "(原图片文件格式不正确！支持的格式有[ " + AllowExt + " ])";
                        return Json(new { Success = false, Message = str });
                    }

                    //判断文件大小
                    int intFileLength = Fupload.ContentLength;
                    
                    if (intFileLength > maxRequest)
                    {
                        int maxK = maxRequest / 1000;
                        string str = "文件大于" + maxK + "K，不能上传！";
                        return Json(new { Success = false, Message = str });

                    }
                    Random ran = new Random();

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(newPath)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(newPath));
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(newPath)))
                        {
                            return Json(new { Success = false, Message = "系统错误" });
                        }
                    }

                    //上传文件重命名：时间+三位随机数 
                    string fileName = Guid.NewGuid().ToString();
                    fileName = fileName + sExt;
                    path = newPath + "/" + fileName;
                    Fupload.SaveAs(HttpContext.Current.Server.MapPath(path));

                    return Json(new { Success = true, Path = path ,FileName= fileName });
                }
                else
                {
                    return Json(new { Success = false, Message = "请选择您要上传的图片" });
                }

            }
            catch
            {
                return Json(new { Success = false, Message = "系统错误" });
            }
        }

        /// <summary>
        /// 传入临时文件名及路径名，返回新的相对路名
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <param name="userId"></param>
        /// <param name="userTypeId"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public static string UploadByFileAPI(string file, string path)
        {
            file = Constants.TempFolder + file;
            file = HttpContext.Current.Server.MapPath(file);
            string uri = ConfigurationManager.AppSettings["FileServer"] + "/file/upload_image";
            string fileName = Path.GetFileName(file);

            using (var client = new HttpClient())
            {
                var token = "555a39e0-1ea1-449d-bba8-8214f5385fc5";// "555a39e0-1ea1-449d-bba8-8214f5385fc5";
                client.DefaultRequestHeaders.Add("Authorization", token);

                using (var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    content.Add(new StreamContent(System.IO.File.OpenRead(file)), "bilddatei", Path.GetFileName(file));
                    var paraPath = new StringContent(path);
                    paraPath.Headers.Add("Content-Disposition", "form-data; name=\"Path\"");
                    content.Add(paraPath, "Path");

                    var paraFileName = new StringContent(fileName);
                    paraFileName.Headers.Add("Content-Disposition", "form-data; name=\"FileName\"");
                    content.Add(paraFileName, "FileName");

                    var response = client.PostAsync(uri, content).Result;
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var serializer = new JavaScriptSerializer();
                    serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
                    dynamic obj = serializer.Deserialize(jsonString, typeof(object));

                    if (obj.code == 10000) //不成功
                    {
                        return obj.data.path;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType">StudentFolder = "/Students/"</param>
        /// <returns></returns>
        public static string GetNewUrl(string fileName,string fileType)
        {
            if (!string.IsNullOrEmpty(fileName) && !fileName.Contains(ConfigurationManager.AppSettings["FileServer"]))
            {
                var newImgUrl = UploadByFileAPI(fileName, fileType + DateTime.Now.ToString(Constants.TimeShortFormat) + "/");
                return ConfigurationManager.AppSettings["FileServer"] + newImgUrl;
            }
            return "";
        }

        private readonly static string AllowExt = ConfigurationManager.AppSettings["PictureType"].ToString();

        private static bool CheckValidExt(string sExt)
        {
            bool flag = false;
            string[] aExt = AllowExt.Split('|');
            foreach (string filetype in aExt)
            {
                if (filetype.ToLower() == sExt.Replace(".", ""))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private static string Json(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }
        #endregion
    }
}