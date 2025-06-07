using Microsoft.AspNetCore.Mvc;

namespace HavalimaniPanelMVC.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Giriş yapan kullanıcının adı ViewBag'e gönderilebilir (örnek).
            ViewBag.AdSoyad = HttpContext.Session.GetString("AdSoyad") ?? "Kullanıcı";
            return View();
        }
    }
}
