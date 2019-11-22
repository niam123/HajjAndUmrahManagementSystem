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
    public class CustomerController : Controller
    {
        private HajAndUmrahDBEntities1 db = new HajAndUmrahDBEntities1();

        // GET: Customer
        public ActionResult Index(string Search)
        {
            var a = from c in db.Customer_tbl select c;

            if (!String.IsNullOrEmpty(Search))
            {
                a = db.Customer_tbl.Where(x => x.Email == Search);

            }

            return View(a.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_tbl customer_tbl = db.Customer_tbl.Find(id);
            if (customer_tbl == null)
            {
                return HttpNotFound();
            }
            return View(customer_tbl);
        }

        // GET: Customer/Details/5
        public ActionResult Details1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_tbl customer_tbl = db.Customer_tbl.Find(id);
            if (customer_tbl == null)
            {
                return HttpNotFound();
            }
            return View(customer_tbl);
        }
        // GET: Customer/Create
        public ActionResult Create(int? id)
        {
            ViewBag.PackageId = db.Package_tbl.Where(x => x.PackageId == id).FirstOrDefault().PackageId;
            ViewBag.PackageName = db.Package_tbl.Where(x => x.PackageId == id).FirstOrDefault().PackageName;
            ViewBag.PackagePrice = db.Package_tbl.Where(x => x.PackageId == id).FirstOrDefault().PackagePrice;
            ViewBag.PackageTypeName = db.Package_tbl.Where(x => x.PackageId == id).FirstOrDefault().PackageType_tbl.PackageTypeName;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, string FirstName, string LastName, string Email, string City, string PostalCode, string Passport, string Designation, string PackageName, string PackageType, int PackagePrice, int Phone, int NoOfAdultTravellers, int? NoOfChildTravellers, int? TotalPrice)
        {
            if (Email != null)
            {
                Customer_tbl customer = new Customer_tbl();
                customer.FirstName = FirstName;
                customer.LastName = LastName;
                customer.Email = Email;
                customer.City = City;
                customer.PostalCode = PostalCode;
                customer.Passport = Passport;
                customer.Designation = Designation;
                customer.Phone = Phone;
                customer.NoOfAdultTravellers = NoOfAdultTravellers;
                customer.NoOfChildTravellers = NoOfChildTravellers;
                customer.TotalPrice = TotalPrice;
                customer.PackageId = id;

                db.Customer_tbl.Add(customer);
                db.SaveChanges();             
                Payment_tbl payment = new Payment_tbl();
                payment.Status = 0;
                payment.CustomerId = customer.CustomerId;
                db.Payment_tbl.Add(payment);
                db.SaveChanges();
                SendEmail(Email, FirstName);
                Customer_tbl lastCustomer = db.Customer_tbl.OrderByDescending(a => a.CustomerId).FirstOrDefault();
                return RedirectToAction("Details" + "/" + lastCustomer.CustomerId); ;
            }
            else
            {
                return RedirectToAction("Create" + "/" + id); ;
            }
            //var list = db.Package_tbl.Where(x => x.PackageId == id).FirstOrDefault();
            //return RedirectToAction("Index");
            
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_tbl customer_tbl = db.Customer_tbl.Find(id);
            ViewBag.PackageId = new SelectList(db.Package_tbl, "PackageId", "PackageName", customer_tbl.PackageId);

            if (customer_tbl == null)
            {
                return HttpNotFound();
            }
            return View(customer_tbl);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer_tbl customer_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_tbl).State = EntityState.Modified;
                db.SaveChanges();
               
                SendEmail1(customer_tbl.Email, customer_tbl.FirstName, customer_tbl);
                return RedirectToAction("Index");
            }
            return View(customer_tbl);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_tbl customer_tbl = db.Customer_tbl.Find(id);
            if (customer_tbl == null)
            {
                return HttpNotFound();
            }
            return View(customer_tbl);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_tbl customer_tbl = db.Customer_tbl.Find(id);
            db.Customer_tbl.Remove(customer_tbl);
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
        public void SendEmail(string emailID,string FirstName)
        {

            var fromEmail = new MailAddress("cavemanim007@gmail.com", "Caveman International limited");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "caveman2303"; // Replace with actual password
            string subject = "Your information is successfully submitted";
            Random randomcode = new Random();
            int r = randomcode.Next(100000, 1000000);




            string newbody = "<br/>Dear "+ FirstName + ",<br/> Please pay your full payment at least before 1 month from the journey date, otherwise your order will be rejected.Now this is your payment code...  ";
            string newbodys = "<br/>Thank You";
            string body = newbody + r + newbodys;


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
        [NonAction]
        public void SendEmail1(string emailID, string FirstName, Customer_tbl customer_Tbl)
        {

            var fromEmail = new MailAddress("cavemanim007@gmail.com", "Caveman International limited");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "caveman2303"; // Replace with actual password
            string subject = "Your information is successfully edited by manager";


            string newbody = "<br/>Dear " + FirstName + ",<br/> Please check your full information.";
            string newbody1 = "<br/>  First Name: " + customer_Tbl.FirstName + "<br/>";
            string newbody2 = "<br/>  Last Name: " + customer_Tbl.LastName + "<br/>";
            string newbody3 = "<br/>  Email: " + customer_Tbl.Email + "<br/>";
            string newbody4 = "<br/>  City: " + customer_Tbl.City + "<br/>";
            string newbody5 = "<br/>  Postal Code: " + customer_Tbl.PostalCode + "<br/>";
            string newbody6 = "<br/> Passport: " + customer_Tbl.Passport + "<br/>";
            string newbody7 = "<br/> Designation: " + customer_Tbl.Designation + "<br/>";
            string newbody8 = "<br/> Phone: " + customer_Tbl.Phone + "<br/>";
            string newbody9 = "<br/> Number Of Adult Travellers: " + customer_Tbl.NoOfAdultTravellers + "<br/>";
            string newbody10 = "<br/> Number Of Child Travellers: " + customer_Tbl.NoOfChildTravellers + "<br/>";
            string newbody11 = "<br/> Total Price: " + customer_Tbl.TotalPrice + "<br/>";
            string newbodys = "<br/> Thank You <br/>";
            string body = newbody + newbody1 + newbody2 + newbody3 + newbody4 + newbody5 + newbody6 + newbody7 + newbody8 + newbody9 + newbody10 + newbody11 + newbodys;

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
