using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("../../Home");
            }
        }
    }
}