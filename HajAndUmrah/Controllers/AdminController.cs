using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;

namespace HajAndUmrah.Controllers
{
    public class AdminController : Controller
    {
         Models.HajAndUmrahDBEntities1 db = new Models.HajAndUmrahDBEntities1();
        // GET: Admin
        LoginController login = new LoginController();
        public ActionResult Index()
        {
            return View();
        }

        // Start Registration for Manager/admin
        public ActionResult Registration()
        {
            string name = Session["Name"].ToString();
            if (login.Admin(name))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Registration(Models.Admin_tbl admin_Tbl)
        {
            if (ModelState.IsValid)
            {
                admin_Tbl.Password= Encrypt(admin_Tbl.Password, "sblw-3hn8-sqoy19");
                admin_Tbl.RepeatPassword= Encrypt(admin_Tbl.RepeatPassword, "sblw-3hn8-sqoy19");
                db.Admin_tbl.Add(admin_Tbl);
                db.SaveChanges();
                return RedirectToAction("ManagerList");
            }

            else
            {
                return View();
            }
        }
        public ActionResult Edit(int id = 0)
        {

            Models.Admin_tbl tbl = new Models.Admin_tbl();

            if (tbl == null)
            {
                return HttpNotFound();
            }
            return View(tbl);


        }

        public ActionResult ManagerList()
        {
            return View(db.Admin_tbl.ToList());
        }


        [HttpGet]
        public ActionResult UpdateManager(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Models.Admin_tbl admin = db.Admin_tbl.Find(id);
            admin.Password = Decrypt(admin.Password, "sblw-3hn8-sqoy19");
            admin.RepeatPassword = Decrypt(admin.RepeatPassword, "sblw-3hn8-sqoy19");
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        [HttpPost]
        public ActionResult UpdateManager([Bind(Include = "AdminId,Name,Contact,Address,Email,Password,RepeatPassword")] Models.Admin_tbl admin)
        {
            if (ModelState.IsValid)
            {
                admin.Password = Encrypt(admin.Password, "sblw-3hn8-sqoy19");
                admin.RepeatPassword = Encrypt(admin.RepeatPassword, "sblw-3hn8-sqoy19");
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManagerList");
            }
            return View(admin);
        }

        [HttpGet]
        public ActionResult DeleteManager(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Models.Admin_tbl admin = db.Admin_tbl.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        [HttpPost]
        public ActionResult DeleteManager(int id)
        {
            Models.Admin_tbl admin = db.Admin_tbl.Find(id);
            db.Admin_tbl.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("ManagerList");
        }
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = System.Text.Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = System.Text.Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return System.Text.Encoding.UTF8.GetString(resultArray);
        }
        // End Registration for Manager/admin
    }
}