using Microsoft.AspNetCore.Mvc;
using RouteAttr.Models;
using System.Diagnostics;
using System.Net;

namespace RouteAttr.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }

        public bool CheckLogin()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return false;
            }

            return true;
        }

        public IActionResult GirisYap(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["AuthError"] = "Form eksik veya hatalý.";
                return RedirectToAction("Login");
            }

            if (model.Username == "miraykaragoz" && model.Password == "1")
            {
                HttpContext.Session.SetString("username", "miraykaragoz");
                if(!string.IsNullOrEmpty(model.RedirectUrl))
                {
                    return Redirect(model.RedirectUrl);
                }

                return RedirectToAction("Index");
            }

            TempData["AuthError"] = "Kullanýcý adý veya þifre hatalý";
            return RedirectToAction("Login");
        }

        public IActionResult Login(string? redirectUrl)
        {
            ViewBag.AuthError = TempData["AuthError"] as string;
            ViewBag.RedirectUrl = redirectUrl;
            return View();
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Hakkimizda()
        {
            if (!CheckLogin())
            {
                return RedirectToAction("Login", new { RedirectUrl = "/home/hakkimizda" });
            }

            ViewData["username"] = HttpContext.Session.GetString("username");
            return View();
        }

        /*
        Bu kullaným sayesinde eriþtiðimiz sayfanýn 
        sonuna /duzenle ekleyip doðrudan düzenleme sayfasýný açabiliriz
         */
        // detay-sayfasi/duzenle
        [Route("{slug}/duzenle")]
        public IActionResult Duzenle(string slug) {
            return View();
        }
    }
}
