using FKHY.Models.DBModels;
using FKHY.Web.Alipay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FKHY.Common;
using System.Collections.Specialized;

namespace FKHY.Web.Controllers
{
    public class PayController : BaseController
    {
        // GET: Pay
        [Auth]
        public ActionResult Return_Url()
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.QueryString["out_trade_no"];

                    //支付宝交易号

                    string trade_no = Request.QueryString["trade_no"];

                    //交易状态
                    string trade_status = Request.QueryString["trade_status"];


                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        using (DataAccess dbPayment = new DataAccess())
                        {
                            dbPayment.alipay_trade_confirmed(out_trade_no.ToLong());
                        }
                    }
                    else
                    {
                        ViewBag.ResultStatus = -1;
                        ViewBag.TradeStatus = Request.QueryString["trade_status"];
                        return View("Return_Url");
                    }
                    ViewBag.ResultStatus = 1;
                    ViewBag.TradeStatus = "付款成功";
                    //打印页面
                    return View("Return_Url");

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    ViewBag.ResultStatus = -1;
                    ViewBag.TradeStatus = "验证失败";
                    return View("Return_Url");
                }
            }
            else
            {
                ViewBag.ResultStatus = -1;
                ViewBag.TradeStatus = "无参数";
                return View("Return_Url");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

        public ActionResult Paid()
        {
            return View();
        }

        public ActionResult Index(long id)//TRADEID
        {
            //判断 这个ID是有效的 TRADEID， 并且没有支付过，并且是属于登录的企业账号的 CompanyId
            using (DataAccess dbPayment = new DataAccess())
            {
                var trade = dbPayment.Trades.SingleOrDefault(r => r.UserId == UserId && r.UserTypeId == Constants.老师 && r.TradeId == id && !r.DatePaymentConfirmed.HasValue && r.Topup > 0);
                if (trade != null)
                {
                    var title = trade.TradeType.Name + "【" + id + "】";
                    return new ContentResult { Content = WebHelper.buildAlipayUrl(id.ToString(), title, trade.Topup.ToString("N1").Replace(",", ""), title) };
                }
                else
                {
                    return Redirect(ReturnUrl);
                }
            }
        }
    }
}