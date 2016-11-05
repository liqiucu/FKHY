using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc; 

namespace FKHY.Common
{
    public static class Extensions
    {
        public static int? ToInt(this string str)
        {
            int val;
            if (int.TryParse(str, out val))
                return val;
            return null;
        }

        public static long? ToLong(this string str)
        {
            long val;
            if (long.TryParse(str, out val))
                return val;
            return null;
        }
        public static decimal? ToDecimal(this string str)
        {
            decimal val;
            if (decimal.TryParse(str, out val))
                return val;
            return null;
        }

        public static double? ToDouble(this string str)
        {
            double val;
            if (double.TryParse(str, out val))
                return val;
            return null;
        }

        public static bool? ToBool(this string value)
        {
            bool val;
            if (value == null) return null;
            if (value == "on") return true;
            if (value == "off") return false;
            if (value.ToLower().Trim().StartsWith("y")) return true;
            if (value.ToLower().Trim().StartsWith("n")) return false;
            if (value.ToLower().Trim().StartsWith("t")) return true;
            if (value.ToLower().Trim().StartsWith("f")) return false;
            if (value.ToLower().Trim().StartsWith("1")) return true;
            if (value.ToLower().Trim().StartsWith("0")) return false;
            if (bool.TryParse(value, out val))
                return val;
            return null;
        }

        public static DateTime? ToDateTime(this string value)
        {
            if (value == null) return null;
            DateTime val;
            if (DateTime.TryParse(value, out val))
                return val;
            return null;
        }

        public static string Left(this string value, int Number)
        {
            if (value.Length > Number)
                return value.Substring(0, Number);
            else
                return value;
        }

        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }

        public static string ToBase64String(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                return Convert.ToBase64String(bytes);
            }
            return string.Empty;
        }

        public static string FromBase64String(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                byte[] data = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(data);
            }
            return string.Empty;
        }

      
        private readonly static string reservedCharacters = "!*'();:@&=+$,/?%#[]";
        public static string UrlEncode(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return String.Empty;

            var sb = new StringBuilder();

            foreach (char @char in str)
            {
                if (reservedCharacters.IndexOf(@char) == -1)
                    sb.Append(@char);
                else
                    sb.AppendFormat("%{0:X2}", (int)@char);
            }
            return sb.ToString();
        }

        public static string UrlDecode(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return HttpUtility.UrlDecode(str);
            }
            return string.Empty;
        }

        public static bool Contains(this string[] source, string value, bool ignoreCase)
        {
            bool result = false;
            foreach (var s in source)
            {
                if (ignoreCase && s.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    result = true;
                else if (s.Equals(value))
                    result = true;
            }
            return result;
        }

        public static string StripHtml(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return Regex.Replace(str, @"<[^>]*>", String.Empty);
            return string.Empty;
        }

        public static string ToTimePassed(this DateTime dt)
        {
            if (DateTime.Now > dt)
            {
                if ((DateTime.Now - dt).TotalMinutes < 60)
                    return (int)(DateTime.Now - dt).TotalMinutes + "分钟前";
                else if ((DateTime.Now - dt).TotalHours < 24)
                    return (int)(DateTime.Now - dt).TotalHours + "小时前";
                else if (DateTime.Now.MonthDifference(dt) < 1)
                    return (int)(DateTime.Now - dt).TotalDays + "天前";
                else
                    return DateTime.Now.MonthDifference(dt) + "月前";
            }
            else
            {
                if ((dt - DateTime.Now).TotalMinutes < 60)
                    return (int)(dt - DateTime.Now).TotalMinutes + "分钟后";
                else if ((dt - DateTime.Now).TotalHours < 24)
                    return (int)(dt - DateTime.Now).TotalHours + "小时后";
                else if (dt.MonthDifference(DateTime.Now) < 1)
                    return (int)(dt - DateTime.Now).TotalDays + "天后";
                else
                    return dt.MonthDifference(DateTime.Now) + "月后";
            }
        }

        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }


        public static Stream ToStream(this Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
        public static string AllowHtmlTag(this string str)
        {
            // 千万别单独替换<标签 
            if (!string.IsNullOrEmpty(str))
                return str
                    .Replace("&lt;p", "<p").Replace("&lt;/p&gt;", "</p>")
                    .Replace("&lt;a", "<a").Replace("&lt;/a&gt;", "</a>")
                    .Replace("&lt;b", "<b").Replace("&lt;/b&gt;", "</b>")
                    .Replace("&lt;i", "<i").Replace("&lt;/i&gt;", "</i>")
                    .Replace("&lt;div", "<div").Replace("&lt;/div&gt;", "</div>")
                    .Replace("&lt;span", "<span").Replace("&lt;/span&gt;", "</span>")
                    .Replace("&lt;strong", "<strong").Replace("&lt;/strong&gt;", "</strong>")
                    .Replace("&lt;img", "<img").Replace("&lt;/img&gt;", "</img>")
                    .Replace("&gt;", ">")
                    .Replace("&lt;hr", "<hr")
                    .Replace("&nbsp;", " ")
                    .Replace("&amp;nbsp;", " ")
                    .Replace("&quot;", "\"")
                    .Replace("&uarr;", "↑");
            return str;
        }

        public static string RemoveScriptTag(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return Regex.Replace(str, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            }
            return string.Empty;
        }
        public static string ChangeHtmlTag(this string Htmlstring)
        {
            if (!string.IsNullOrEmpty(Htmlstring))
            {
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<div", "<p", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"</div>", "</p>", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&uarr;", "↑", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&darr;", "↓", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "“", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "”", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&hellip;", "…", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&mdash;", "—", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&radic;", "√", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&times;", "×", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&middot;", "·", RegexOptions.IgnoreCase);

                Htmlstring.Replace("\r\n", "");
                return Htmlstring;
            }
            return null;
        }

        public static string PartialView(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
    }
}