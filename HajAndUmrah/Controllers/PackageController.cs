using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HajAndUmrah.Models;

namespace HajAndUmrah.Controllers
{
    public class PackageController : Controller
    {
        Models.HajAndUmrahDBEntities1 db=new HajAndUmrahDBEntities1();

        // GET: Package
        public ActionResult Index(string Search)
        {
            var a = from c in db.Package_tbl select c;

            if (!String.IsNullOrEmpty(Search))
            {
                a = db.Package_tbl.Where(x => x.PackageGroup_tbl.PackageGroupName == Search);

            }

            return View(a.ToList());
        }

        // GET: Package/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package_tbl package_tbl = db.Package_tbl.Find(id);
            if (package_tbl == null)
            {
                return HttpNotFound();
            }
            return View(package_tbl);
        }

        // GET: Package/Create
        public ActionResult CustomizeCreate()
        {
            ViewBag.HotelName = new SelectList(db.HotelName_tbl, "HotelNameId", "HotelName");
            ViewBag.PackageGroup = new SelectList(db.PackageGroup_tbl, "PackageGroupId", "PackageGroupName");
            ViewBag.StarRate = new SelectList(db.StarRate_tbl, "StarRateId", "StarRate");
            ViewBag.PackageType = new SelectList(db.PackageType_tbl, "PackageTypeId", "PackageTypeName");
            ViewBag.RoomType = new SelectList(db.RoomType_tbl, "RoomTypeId", "RoomType");
            ViewBag.TravelClass = new SelectList(db.TravelClass_tbl, "TravelClassId", "TravelClassName");
            return View();
        }

        [HttpPost]
        public ActionResult CustomizeCreate(Package_tbl package_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Package_tbl.Add(package_tbl);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Create", "Customer", new { @id = package_tbl.PackageId });
            }

            ViewBag.HotelName = new SelectList(db.HotelName_tbl, "HotelNameId", "HotelName", package_tbl.HotelName);
            ViewBag.PackageGroup = new SelectList(db.PackageGroup_tbl, "PackageGroupId", "PackageGroupName", package_tbl.PackageGroup);
            ViewBag.StarRate = new SelectList(db.StarRate_tbl, "StarRateId", "StarRate", package_tbl.StarRate);
            ViewBag.PackageType = new SelectList(db.PackageType_tbl, "PackageTypeId", "PackageTypeName", package_tbl.PackageType);
            ViewBag.RoomType = new SelectList(db.RoomType_tbl, "RoomTypeId", "RoomType", package_tbl.RoomType);
            ViewBag.TravelClass = new SelectList(db.TravelClass_tbl, "TravelClassId", "TravelClassName", package_tbl.TravelClass);
            return View(package_tbl);
        }

        // GET: Package/Create
        public ActionResult Create()
        {
            ViewBag.HotelName = new SelectList(db.HotelName_tbl, "HotelNameId", "HotelName");
            ViewBag.PackageGroup = new SelectList(db.PackageGroup_tbl, "PackageGroupId", "PackageGroupName");
            ViewBag.StarRate = new SelectList(db.StarRate_tbl, "StarRateId", "StarRate");
            ViewBag.PackageType = new SelectList(db.PackageType_tbl, "PackageTypeId", "PackageTypeName");
            ViewBag.RoomType = new SelectList(db.RoomType_tbl, "RoomTypeId", "RoomType");
            ViewBag.TravelClass = new SelectList(db.TravelClass_tbl, "TravelClassId", "TravelClassName");
            return View();
        }

        // POST: Package/Create
        [HttpPost]
        public ActionResult Create(Package_tbl package_tbl, HttpPostedFileBase imageName)
        {
            var image = System.IO.Path.GetFileName(imageName.FileName);
            package_tbl.Image = image.ToString();

            if (ModelState.IsValid)
            {
                db.Package_tbl.Add(package_tbl);
                db.SaveChanges();
                imageName.SaveAs(Server.MapPath("../Upload/Picture/" + package_tbl.PackageId.ToString() + "_" + package_tbl.Image));
                return RedirectToAction("Index");
            }

            ViewBag.HotelName = new SelectList(db.HotelName_tbl, "HotelNameId", "HotelName", package_tbl.HotelName);
            ViewBag.PackageGroup = new SelectList(db.PackageGroup_tbl, "PackageGroupId", "PackageGroupName", package_tbl.PackageGroup);
            ViewBag.StarRate = new SelectList(db.StarRate_tbl, "StarRateId", "StarRate", package_tbl.StarRate);
            ViewBag.PackageType = new SelectList(db.PackageType_tbl, "PackageTypeId", "PackageTypeName", package_tbl.PackageType);
            ViewBag.RoomType = new SelectList(db.RoomType_tbl, "RoomTypeId", "RoomType", package_tbl.RoomType);
            ViewBag.TravelClass = new SelectList(db.TravelClass_tbl, "TravelClassId", "TravelClassName", package_tbl.TravelClass);
            return View(package_tbl);
        }

        // GET: Package/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package_tbl package_tbl = db.Package_tbl.Find(id);
            if (package_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelName = new SelectList(db.HotelName_tbl, "HotelNameId", "HotelName", package_tbl.HotelName);
            ViewBag.PackageGroup = new SelectList(db.PackageGroup_tbl, "PackageGroupId", "PackageGroupName", package_tbl.PackageGroup);
            ViewBag.StarRate = new SelectList(db.StarRate_tbl, "StarRateId", "StarRate", package_tbl.StarRate);
            ViewBag.PackageType = new SelectList(db.PackageType_tbl, "PackageTypeId", "PackageTypeName", package_tbl.PackageType);
            ViewBag.RoomType = new SelectList(db.RoomType_tbl, "RoomTypeId", "RoomType", package_tbl.RoomType);
            ViewBag.TravelClass = new SelectList(db.TravelClass_tbl, "TravelClassId", "TravelClassName", package_tbl.TravelClass);
            return View(package_tbl);
        }

        // POST: Package/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Package_tbl package_tbl, HttpPostedFileBase imageName)
        {
            ViewBag.HotelName = new SelectList(db.HotelName_tbl, "HotelNameId", "HotelName", package_tbl.HotelName);
            ViewBag.PackageGroup = new SelectList(db.PackageGroup_tbl, "PackageGroupId", "PackageGroupName", package_tbl.PackageGroup);
            ViewBag.StarRate = new SelectList(db.StarRate_tbl, "StarRateId", "StarRate", package_tbl.StarRate);
            ViewBag.PackageType = new SelectList(db.PackageType_tbl, "PackageTypeId", "PackageTypeName", package_tbl.PackageType);
            ViewBag.RoomType = new SelectList(db.RoomType_tbl, "RoomTypeId", "RoomType", package_tbl.RoomType);
            ViewBag.TravelClass = new SelectList(db.TravelClass_tbl, "TravelClassId", "TravelClassName", package_tbl.TravelClass);
            
            if (ModelState.IsValid)
            {
                db.Entry(package_tbl).State = EntityState.Modified;
                db.SaveChanges();        
                return RedirectToAction("Index");
            }
           
            return View(package_tbl);
        }

        // GET: Package/Delete/5S
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package_tbl package_tbl = db.Package_tbl.Find(id);
            if (package_tbl == null)
            {
                return HttpNotFound();
            }
            return View(package_tbl);
        }

        // POST: Package/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Package_tbl package_tbl = db.Package_tbl.Find(id);
            db.Package_tbl.Remove(package_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
