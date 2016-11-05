using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FKHY.Web
{
    public class WebHelper
    {
        public static string buildAlipayUrl(string trade_no, string subject, string total_fee, string body)
        {
            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            string notify_url = System.Configuration.ConfigurationManager.AppSettings["AliPayNotifyUrl"];
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            string return_url = System.Configuration.ConfigurationManager.AppSettings["AliPayReturnUrl"];
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //商户订单号
            string out_trade_no = trade_no.Trim();
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            subject = subject.Trim();
            //必填

            //付款金额
            total_fee = total_fee.Trim();
            //必填

            //订单描述

            body = body.Trim();
            //商品展示地址
            string show_url = null;
            //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //若要使用请调用类文件submit中的query_timestamp函数

            //客户端的IP地址
            string exter_invoke_ip = "";
            //非局域网的外网IP地址，如：221.0.0.1


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Alipay.Config.Partner);
            sParaTemp.Add("seller_email", Alipay.Config.Seller_email);
            sParaTemp.Add("_input_charset", Alipay.Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

            //建立请求
            string sHtmlText = Alipay.Submit.BuildRequest(sParaTemp, "get", "确认");
            return sHtmlText;
        }
    }
}