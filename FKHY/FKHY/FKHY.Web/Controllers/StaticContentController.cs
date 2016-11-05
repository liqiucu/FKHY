using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FKHY.Web.Controllers
{
    public class StaticContentController : Controller
    {
        // GET: StaticContent
        public ActionResult Clause()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Refund()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Benefit()
        {
            return View();
        }
    }
}