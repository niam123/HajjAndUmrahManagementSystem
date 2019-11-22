using HajAndUmrah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HajAndUmrah.Controllers
{
    public class DashboardController : Controller
    {
        HajAndUmrahDBEntities1 db = new HajAndUmrahDBEntities1();
        // GET: Dashboard
        public ActionResult Index()
        {
            DashBoard ds = new DashBoard();
            ds.Package = db.Package_tbl.ToList();
            ds.Customer = db.Customer_tbl.ToList();
            ds.Payment = db.Payment_tbl.ToList();
            return View(ds);
        }
    }
}