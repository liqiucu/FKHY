using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FKHY.Web.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Center(string isUpdateClick="false")
        {
            if (isUpdateClick=="true")
            {
                ViewBag.ClickUpdate = "true";
            }

            return View();
        }
    }
}