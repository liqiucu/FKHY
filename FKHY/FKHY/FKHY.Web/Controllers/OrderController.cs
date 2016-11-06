using FKHY.Common;
using FKHY.Models;
using FKHY.Models.CustomerModels;
using FKHY.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FKHY.Web.Controllers
{
    [Auth]
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Commit()
        {
            return View();
        }

        public JsonResult Buy(BuyViewModel model)
        {
            DataAccess db = new DataAccess();
            
            if (1==1)
            {
                if (model.OrderId>0)
                {
                    var sucessdata = new
                    {
                        Status = 1,
                        Msg = "下单成功,即将转到支付页面",
                        Url = string.Format("/Company/TradePay?orderId={0}&amount={1}&tradeTypeId={2}", model.OrderId, model.RequestCost, model.TradeType)
                    };
                    return Json(sucessdata);
                }
            }
            return Json(new
            {
                Status = 0,
                Msg = "下单失败",
            });
        }

        public ActionResult TradePay(long orderId, decimal amount, int tradeTypeId)
        {
            // 如果是支付订单，但是余额不对，返回错误
            //decimal unpaid_amount = dbWeb.api_get_order_unpaid_amount(orderId).First().Value;
            //if (amount < unpaid_amount)
            //{
            //    return Redirect(ReturnUrl);
            //}
            DataAccess db = new DataAccess();
            // 调用第三方支付宝支付存储过程
            var result = db.txn_trade_third_party(UserId, Constants.学生, tradeTypeId, orderId,
                1, amount, null, null);

            var trade = db.Trades.SingleOrDefault(r => r.UserId == UserId && r.UserTypeId == Constants.学生 && r.TradeId == result && !r.DatePaymentConfirmed.HasValue && r.Topup > 0);
            if (trade != null)
            {
                var title = trade.TradeType.Name + "【" + result + "】";
                return new ContentResult { Content = WebHelper.buildAlipayUrl(result.ToString(), title, trade.Topup.ToString("N1").Replace(",", ""), title) };
            }
            else
            {
                return Redirect(ReturnUrl);
            }
        }
    }
}