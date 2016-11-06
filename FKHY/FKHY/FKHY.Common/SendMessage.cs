using FKHY.Web.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FKHY.Common
{
    public class SendMessage
    {
        public static void Send(string mobile, string message)
        {
            string url = "http://sdkhttp.eucp.b2m.cn/sdkproxy/sendsms.action";
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["cdkey"] = "3SDK-EMY-0130-OJWRM";
                values["password"] = "eTueomvkCV";
                values["phone"] = mobile;
                values["message"] = message;
                values["addserial"] = string.Empty;

                var response = client.UploadValues(url, values);
            }
        }

        /// <summary>
        /// 正式：http://120.55.248.18/smsSend.do
        /// 测试：http://121.41.16.92/smsSend.do
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="message"></param>
        public static void SendNew(string mobile, string message)
        {
            var content = HttpUtility.UrlEncode(message,  Encoding.UTF8);
            var username = "ceshihao";
            var password = "ceshihao123";

            var md5password = FKHYCommon.MD5(password);
            var realPassword= FKHYCommon.MD5(username+ md5password);
            string url = "http://120.55.248.18/smsSend.do?username=" + username+ "&password="+ realPassword + "&mobile="+mobile+ "&content="+ content;
           

            WebRequest request = WebRequest.Create(url);
            request.ContentType = "utf-8";
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusDescription == "OK")
            {
                var dataStream = response.GetResponseStream();
                var reader = new StreamReader(dataStream, Encoding.GetEncoding("GB2312"));
                var responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }
            response.Close();

            //using (var client = new WebClient())
            //{
            //    var values = new NameValueCollection();
            //    values["username"] = "ceshihao";
            //    values["password"] = password;
            //    values["mobile"] = mobile;
            //    values["content"] = content;
            //    var response = client.UploadValues(url, values);
            //}
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
    }
}
