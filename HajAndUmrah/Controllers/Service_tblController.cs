using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HajAndUmrah.Models;

namespace HajAndUmrah.Controllers
{
    public class Service_tblController : Controller
    {
        private HajAndUmrahDBEntities1 db = new HajAndUmrahDBEntities1();

        // GET: Service_tbl
        public ActionResult Index()
        {
            var service_tbl = db.Service_tbl.Include(s => s.Customer_tbl);
            return View(service_tbl.ToList());
        }

        // GET: Service_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service_tbl service_tbl = db.Service_tbl.Find(id);
            if (service_tbl == null)
            {
                return HttpNotFound();
            }
            return View(service_tbl);
        }

        // GET: Service_tbl/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName");
            return View();
        }

        // POST: Service_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,AirlinesName,DepratureDate,DepratureTime,ArrivalDate,ArrivalTime,BusName,BusDepratureDate,BusDepratureTime,BusArrivalDate,BusArrivalTime,CustomerId")] Service_tbl service_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Service_tbl.Add(service_tbl);
                db.SaveChanges();
                Customer_tbl customer = db.Customer_tbl.FirstOrDefault(a=>a.CustomerId==service_tbl.CustomerId);
                 SendEmail(customer.Email, customer.FirstName,service_tbl);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName", service_tbl.CustomerId);
            return View(service_tbl);
        }

        // GET: Service_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service_tbl service_tbl = db.Service_tbl.Find(id);
            if (service_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName", service_tbl.CustomerId);
            return View(service_tbl);
        }

        // POST: Service_tbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,AirlinesName,DepratureDate,DepratureTime,ArrivalDate,ArrivalTime,BusName,BusDepratureDate,BusDepratureTime,BusArrivalDate,BusArrivalTime,CustomerId")] Service_tbl service_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName", service_tbl.CustomerId);
            return View(service_tbl);
        }

        // GET: Service_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service_tbl service_tbl = db.Service_tbl.Find(id);
            if (service_tbl == null)
            {
                return HttpNotFound();
            }
            return View(service_tbl);
        }

        // POST: Service_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service_tbl service_tbl = db.Service_tbl.Find(id);
            db.Service_tbl.Remove(service_tbl);
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
        [NonAction]
        public void SendEmail(string emailID, string FirstName, Service_tbl service_tbl)
        {

            var fromEmail = new MailAddress("cavemanim007@gmail.com", "Caveman International limited");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "caveman2303"; // Replace with actual password
            string subject = "This is your Schedule Time";
      

            string newbody = "<br/>Dear " + FirstName + ",<br/> Please check your Travel schedule time carefully.<br/>  ";
            string newbody1 = "<br/> AirLines Name: " + service_tbl.AirlinesName + "<br/>";
            string newbody2 = "<br/> Deprature Date : " + service_tbl.DepratureDate.ToString("dd-MM-yyyy") + "<br/>";
            string newbody3 = "<br/> Deprature Time : " + service_tbl.DepratureTime + "<br/>";
            string newbody4 = "<br/> Arrival Date : " + service_tbl.ArrivalDate.ToString("dd-MM-yyyy") + "<br/>";
            string newbody5 = "<br/> Arrival Time : " + service_tbl.ArrivalTime + "<br/>";
            string newbody6 = "<br/> Bus Name : " + service_tbl.BusName + "<br/>";
            string newbody7 = "<br/> Bus Deprature Date : " + service_tbl.BusDepratureDate.ToString("dd-MM-yyyy") + "<br/>";
            string newbody8 = "<br/> Bus Deprature Time : " + service_tbl.BusDepratureTime + "<br/>";
            string newbody9 = "<br/> Bus Arrival Date : " + service_tbl.BusArrivalDate.ToString("dd-MM-yyyy") + "<br/>";
            string newbody10 = "<br/> Bus Arrival Time : " + service_tbl.BusArrivalTime + "<br/>";
            string newbodys = "<br/> Thank You <br/>";
            string body = newbody + newbody1 + newbody2 + newbody3 + newbody4 + newbody5 + newbody6 + newbody7 + newbody8 + newbody9 + newbody10 + newbodys;


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);



        }
    }
}
