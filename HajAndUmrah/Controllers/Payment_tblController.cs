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
    public class Payment_tblController : Controller
    {
        private HajAndUmrahDBEntities1 db = new HajAndUmrahDBEntities1();

        // GET: Payment_tbl
        public ActionResult Index(string Search)
        {
            var a = from c in db.Payment_tbl select c;

            if (!String.IsNullOrEmpty(Search))
            {
                a = db.Payment_tbl.Where(x => x.Customer_tbl.Email == Search);
            
            }
       
            return View(a.ToList());
         
        }

        // GET: Payment_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_tbl payment_tbl = db.Payment_tbl.Find(id);
            if (payment_tbl == null)
            {
                return HttpNotFound();
            }
            return View(payment_tbl);
        }

        // GET: Payment_tbl/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName");
            return View();
        }

        // POST: Payment_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment_tbl payment_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Payment_tbl.Add(payment_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName", payment_tbl.CustomerId);
            return View(payment_tbl);
        }

        // GET: Payment_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_tbl payment_tbl = db.Payment_tbl.Find(id);
            if (payment_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName", payment_tbl.CustomerId);
            return View(payment_tbl);
        }

        [HttpPost]
        public ActionResult Edit(Payment_tbl payment_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_tbl).State = EntityState.Modified;
                db.SaveChanges();

                if (payment_tbl.Status == 1)
                {
                    Customer_tbl customer = db.Customer_tbl.FirstOrDefault(a => a.CustomerId == payment_tbl.CustomerId);
                    SendEmail(payment_tbl.Customer_tbl.Email, payment_tbl.Customer_tbl.FirstName, payment_tbl.CustomerId, payment_tbl.PaymentId);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CustomerId = new SelectList(db.Customer_tbl, "CustomerId", "FirstName", payment_tbl.CustomerId);
            return View(payment_tbl);
        }

        [NonAction]
        public void SendEmail(string emailID, string FirstName, int customer_id, int payment_id)
        {
            //Payment_tbl payment_tbl=new Payment_tbl();
            //Customer_tbl customer_Tbl=new Customer_tbl();
            //Package_tbl package_Tbl=new Package_tbl();
            var customer_tbl = db.Customer_tbl.Where(x => x.CustomerId == customer_id).FirstOrDefault();
            var payment_tbl = db.Payment_tbl.Where(c => c.PaymentId == payment_id).FirstOrDefault();
            
            var fromEmail = new MailAddress("cavemanim007@gmail.com", "Caveman International limited");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "caveman2303"; // Replace with actual password
            string subject = "This is your package Details information";


            string newbody = "<br/>Dear " + customer_tbl.FirstName + ",<br/> Please check your details information carefully.<br/>  ";
            string newbody1 = "<br/>Customer Information: <br/>";
            string newbody2 = "<br/> FirstName: " + customer_tbl.FirstName + "<br/>";
            string newbody3 = "<br/> LastName: " + customer_tbl.LastName + "<br/>";
            string newbody4 = "<br/> Email: " + customer_tbl.Email + "<br/>";
            string newbody5 = "<br/> City: " + customer_tbl.City + "<br/>";
            string newbody6 = "<br/> PostalCode: " + customer_tbl.PostalCode + "<br/>";
            string newbody7 = "<br/> Passport: " + customer_tbl.Passport + "<br/>";
            string newbody8 = "<br/> Designation: " + customer_tbl.Designation + "<br/>";
            string newbody9 = "<br/> Phone: " + customer_tbl.Phone + "<br/>";
            string newbody10 = "<br/>Package Information: <br/>";
            string newbody11 = "<br/> Date: " + customer_tbl.Package_tbl.Date + "<br/>";
            string newbody12 = "<br/> PackageName: " + customer_tbl.Package_tbl.PackageName + "<br/>";
            string newbody13 = "<br/> PackageType: " + customer_tbl.Package_tbl.PackageType_tbl.PackageTypeName + "<br/>";
            string newbody14 = "<br/> PackageGroup: " + customer_tbl.Package_tbl.PackageGroup_tbl.PackageGroupName + "<br/>";
            string newbody15 = "<br/> NoOfDays: " + customer_tbl.Package_tbl.NoOfDays + "<br/>";
            string newbody16 = "<br/> TravelClass: " + customer_tbl.Package_tbl.TravelClass_tbl.TravelClassName + "<br/>";
            string newbody17 = "<br/> HotelName: " + customer_tbl.Package_tbl.HotelName_tbl.HotelName + "<br/>";
            string newbody18 = "<br/> RoomType: " + customer_tbl.Package_tbl.RoomType_tbl.RoomType + "<br/>";
            string newbody19 = "<br/> StarRate: " + customer_tbl.Package_tbl.StarRate_tbl.StarRate + "<br/>";
            string newbody20 = "<br/>Overall Information: <br/>";
            string newbody21 = "<br/> NoOfAdultTravellers: " + customer_tbl.NoOfAdultTravellers + "<br/>";
            string newbody22 = "<br/> NoOfChildTravellers: " + customer_tbl.NoOfChildTravellers + "<br/>";
            string newbody23 = "<br/> PackagePrice: " + customer_tbl.Package_tbl.PackagePrice + "<br/>";
            string newbody24 = "<br/> TotalPrice: " + customer_tbl.TotalPrice + "<br/>";
            string newbodys = "<br/> Thank You <br/>";
            string body = newbody + newbody1 + newbody2 + newbody3 + newbody4 + newbody5 + newbody6 + newbody7 + newbody8 + newbody9 + newbody10 + newbody11 + newbody12 + newbody13 + newbody14 + newbody15 + newbody16 + newbody17 + newbody18 + newbody19 + newbody20 + newbody21 + newbody22 + newbody23 + newbody24 + newbodys;


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

        // GET: Payment_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_tbl payment_tbl = db.Payment_tbl.Find(id);
            if (payment_tbl == null)
            {
                return HttpNotFound();
            }
            return View(payment_tbl);
        }

        // POST: Payment_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment_tbl payment_tbl = db.Payment_tbl.Find(id);
            db.Payment_tbl.Remove(payment_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ConfirmOrder(string Search)
        {
            var a = from c in db.Payment_tbl select c;

            if (!String.IsNullOrEmpty(Search))
            {
                a = db.Payment_tbl.Where(x => x.Customer_tbl.Email == Search && x.Status == 1);

            }

            return View(a.Where(x=>x.Status==1).ToList());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult SendEmail(int?id)
        {

            return View();
        }
    }
}
