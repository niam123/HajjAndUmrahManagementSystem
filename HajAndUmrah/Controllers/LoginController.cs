using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;

namespace HajAndUmrah.Controllers
{
    public class LoginController : Controller
    {

       Models.HajAndUmrahDBEntities1 db = new Models.HajAndUmrahDBEntities1();
        // GET: Login
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String email, String password)
        {
            int er = 0;
            if (email == "")
            {
                er++;
                ViewBag.email = "Email required";
            }
            if (password == "")
            {
                er++;
                ViewBag.password = "Password required";
            }
            if (er > 0)
            {
                return View();
            }
            if (email == "niamulim@gmail.com" && password == "12345678")
            {
                Session["Name"] = "Admin";
                return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                var Login = db.Admin_tbl.Where(x => x.Email == email).FirstOrDefault();
                if (Login != null)
                {
                    Login.Password = Decrypt(Login.Password, "sblw-3hn8-sqoy19");
                    if (Login.Password != password)
                    {
                        ViewBag.message = "Email or Password is incorrect..";
                        return View("Login");
                    }
                    else
                    {
                        Login.RepeatPassword = Decrypt(Login.RepeatPassword, "sblw-3hn8-sqoy19");
                        string name = Login.Name;
                        Session["id"] = Login.AdminId.ToString();
                        Session["Name"] = name;
                        // FormsAuthentication.SetAuthCookie(Login.AdminId.ToString(), false);
                        return RedirectToAction("Index", "DashBoard");
                    }
                }

                else
                {
                    ViewBag.message = "Email or Password is incorrect..";
                    return View("Login");
                }
            }
           // return View();
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

        public Boolean Admin(string name)
        {
            bool admin;
            
            if (name=="Admin")
            {
                admin = true;
            }
            else
            {
                admin = false;
            }
            return admin;
        }
    }
}