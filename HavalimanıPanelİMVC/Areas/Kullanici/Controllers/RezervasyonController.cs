using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HavalimaniPanelMVC.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    public class RezervasyonController : Controller
    {
        private readonly HttpClient _httpClient;

        public RezervasyonController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public IActionResult Create(int transferId)
        {
            var model = new RezervasyonCreateViewModel
            {
                TransferId = transferId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RezervasyonCreateViewModel model)
        {
            // Session’dan giriş yapan kullanıcıyı al
            var sessionValue = HttpContext.Session.GetString("KullaniciId");

            if (string.IsNullOrEmpty(sessionValue))
            {
                ViewBag.Hata = "Kullanıcı giriş yapmamış!";
                return View(model);
            }

            model.KullaniciId = Convert.ToInt32(sessionValue);
            model.ReservedAt = DateTime.Now;
            model.AktifMi = true;

            var json = JsonSerializer.Serialize(model);
            Console.WriteLine("Giden JSON: " + json); // 🟡 Konsola yazdır

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Rezervasyon/insert", content);

            var resultJson = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API'den Gelen Yanıt: " + resultJson); // 🔥 API cevabı log

            if (response.IsSuccessStatusCode)
            {
                TempData["Basarili"] = "Rezervasyon başarıyla oluşturuldu.";
                return RedirectToAction("Index", "Transfer");
            }

            ViewBag.Hata = "Rezervasyon oluşturulamadı. Detay: " + resultJson;
            return View(model);
        }
    }
}
