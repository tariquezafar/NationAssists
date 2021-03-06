using DataEngine;
using DataServices;
using NationAssists.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class BuyOnlineController : Controller
    {
        mBuyOnline obj = new mBuyOnline();
        // GET: BuyOnline
        public ActionResult Index()
        {
            if (Session["CustomerId"] != null && Session["CPRNumber"] != null)
            {
                obj.PackageList = GetAllPackages();
                return View(obj);
            }
            else
            {
                return Redirect("../Home");
            }
        }

        public List<ServicePrice> GetAllPackages()
        {
            MethodOutput<ServicePrice> objMO = new MethodOutput<ServicePrice>();
            ServicePriceService obj = new ServicePriceService();
            objMO = obj.GetAllPackage();
            return objMO.DataList;
        }
        [HttpPost]
        public ActionResult SendPackageRequest(Service objService)
        {
            TempData["Service"] = objService;
            //     return Redirect("/BuyPackageRequest");
            return Json(true, JsonRequestBehavior.AllowGet) ;
        }
    }
}