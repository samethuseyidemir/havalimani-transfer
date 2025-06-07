// Areas/Sirket/Controllers/SirketPanelController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HavalimaniPanelMVC.Areas.Sirket.Controllers
{
    [Area("Sirket")]
    [Authorize]  // Eğer Authentication varsa
    public class SirketPanelController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Şirket Paneli";
            return View();
        }
    }
}
