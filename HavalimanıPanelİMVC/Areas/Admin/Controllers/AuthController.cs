using Microsoft.AspNetCore.Mvc;

namespace HavalimaniPanelMVC.Areas.Sirket.Controllers
{
    [Area("Sirket")]
    public class SirketAuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            // Geçici demo giriş kontrolü
            if (email == "sirket@test.com" && sifre == "123")
            {
                HttpContext.Session.SetInt32("SirketId", 1); // oturum açılıyor
                return RedirectToAction("Index", "SirketPanel", new { area = "Sirket" });
            }

            ViewBag.Hata = "Geçersiz giriş bilgisi.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
