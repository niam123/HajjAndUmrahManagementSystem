using HajAndUmrah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HajAndUmrah.Controllers
{
    public class LandingpageController : Controller
    {
        Models.HajAndUmrahDBEntities1 db = new Models.HajAndUmrahDBEntities1();
        // GET: Landingpage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult HajjPackage()
        {
            Package_tbl packages = new Package_tbl();
            var list = db.Package_tbl.ToList();
            return View(list);

        }

        public ActionResult UmrahPackage()
        {
            Package_tbl packages = new Package_tbl();
            return View(db.Package_tbl.ToList());


        }

        public ActionResult Hotels()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}