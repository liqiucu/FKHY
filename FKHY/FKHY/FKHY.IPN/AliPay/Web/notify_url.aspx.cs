using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using FKHY.Common;
using FKHY.Models.DBModels;

/// <summary>
/// 功能：服务器异步通知页面
/// 版本：3.3
/// 日期：2012-07-10
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// ///////////////////页面功能说明///////////////////
/// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
/// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
/// 该页面调试工具请使用写文本函数logResult。
/// 如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
/// </summary>
namespace FKHY.AliPay.Web
{
    /// 
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码
                    DataAccess db = new DataAccess();
                    AliPayIPN pay = new AliPayIPN
                    {
                        notify_time = Request.Form["notify_time"],
                        notify_type = Request.Form["notify_type"],
                        notify_id = Request.Form["notify_id"],
                        sign_type = Request.Form["sign_type"],
                        sign = Request.Form["sign"],
                        out_trade_no = Request.Form["out_trade_no"],
                        subject = Request.Form["subject"],
                        payment_type = Request.Form["payment_type"],
                        trade_no = Request.Form["trade_no"],
                        trade_status = Request.Form["trade_status"],
                        seller_id = Request.Form["seller_id"],
                        seller_email = Request.Form["seller_email"],
                        buyer_id = Request.Form["buyer_id"],
                        buyer_email = Request.Form["buyer_email"],
                        total_fee = Request.Form["total_fee"],
                        quantity = Request.Form["quantity"],
                        price = Request.Form["price"],
                        body = Request.Form["body"],
                        gmt_create = Request.Form["gmt_create"],
                        gmt_payment = Request.Form["gmt_payment"],
                        is_total_fee_adjust = Request.Form["is_total_fee_adjust"],
                        use_coupon = Request.Form["use_coupon"],
                        discount = Request.Form["discount"],
                        refund_status = Request.Form["refund_status"],
                        gmt_refund = Request.Form["gmt_refund"],
                        gmt_close = Request.Form["gmt_close"],
                        extra_common_param = Request.Form["extra_common_param"],
                        out_channel_type = Request.Form["out_channel_type"],
                        out_channel_amount = Request.Form["out_channel_amount"],
                        out_channel_inst = Request.Form["out_channel_inst"],
                        business_scene = Request.Form["business_scene"],
                        note = "verifyResult from web",
                        date_created = DateTime.Now
                    };
                    db.AliPayIPNs.Add(pay);
                    db.SaveChanges();

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.Form["out_trade_no"];

                    //支付宝交易号

                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    string trade_status = Request.Form["trade_status"];


                    if (Request.Form["trade_status"] == "TRADE_FINISHED")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                    }
                    else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        db.alipay_trade_confirmed(out_trade_no.ToLong());

                        //注意：
                        //付款完成后，支付宝系统发送该交易状态通知
                    }
                    else
                    {
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    Response.Write("success");  //请不要修改或删除

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}