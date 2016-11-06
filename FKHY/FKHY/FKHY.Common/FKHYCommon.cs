using FKHY.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
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

        public static string FromByteArrayToString(byte[] array)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < array.Length; i++)
            {
                output.Append(array[i].ToString("X2"));
            }
            return output.ToString();
        }

        //加密方法
        public static string GenerateHashWithSalt(string password, string salt)
        {
            // merge password and salt together
            string sHashWithSalt = password + salt;
            // convert this merged value to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // return the has as a base 64 encoded string
            
            return Convert.ToBase64String(hash);
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            byte[] data = new byte[8];
            RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] four_bytes = new byte[4];
                Rand.GetBytes(four_bytes);

                // Convert that into an uint.
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int)(min + (max - min) * (scale / (double)uint.MaxValue));
        }

        public static string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
        public static string JsonString(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        public static DataSet ExcelToDataSet(string fileName)
        {
            string connectionString = string.Format("provider=Microsoft.Jet.OLEDB.4.0; data source={0};Extended Properties=Excel 8.0;", fileName);
            if (fileName.ToLower().IndexOf(".xlsx") > 0)
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            DataSet data = new DataSet();

            foreach (var sheetName in GetExcelSheetNames(connectionString))
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    var dataTable = new DataTable();
                    string query = string.Format("SELECT * FROM [{0}]", sheetName);
                    con.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, con);
                    adapter.Fill(dataTable);
                    data.Tables.Add(dataTable);
                }
            }

            return data;
        }
        static string[] GetExcelSheetNames(string connectionString)
        {
            OleDbConnection con = null;
            DataTable dt = null;
            con = new OleDbConnection(connectionString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }
        public static string MD5(string strText)
        {
            // return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "MD5");
            //new
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //获取密文字节数组
            byte[] bytResult = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));

            //转换成字符串，并取9到25位
            //string strResult = BitConverter.ToString(bytResult, 4, 8); 
            //转换成字符串，32位

            string strResult = BitConverter.ToString(bytResult);

            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
            strResult = strResult.Replace("-", "");

            return strResult.ToLower();
        }
    }
}