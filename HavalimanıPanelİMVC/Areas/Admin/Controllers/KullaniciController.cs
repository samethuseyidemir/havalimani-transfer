using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

// Kullanici sınıfı ile namespace çakışmasını önlemek için alias tanımı:
using KullaniciModel = HavalimaniPanelMVC.Models.Kullanici;

namespace HavalimaniPanelMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KullaniciController : Controller
    {
        private readonly HttpClient _httpClient;

        public KullaniciController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.PostAsync("/Kullanici/search", new StringContent(
                JsonSerializer.Serialize(new { startIndex = 0, maxCount = 100 }),
                Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<KullaniciModel>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(result.Data);
            }

            return View(new List<KullaniciModel>());
        }

        // Yeni kullanıcı formu (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni kullanıcı kaydı (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KullaniciModel model)
        {
            model.KayitTarihi = DateTime.Now;

            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("/Kullanici/add", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Hata = "Kayıt sırasında bir hata oluştu.";
            return View(model);
        }

        // Güncelleme sayfası (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/Kullanici/get-by-id?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<KullaniciModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(result.Data);
            }

            return NotFound();
        }

        // Güncelleme kaydı (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KullaniciModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Kullanici/update", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Hata = "Güncelleme sırasında hata oluştu.";
            return View(model);
        }

        // Silme (GET)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Id = id }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Kullanici/delete", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Silme işlemi başarısız.";
            return RedirectToAction("Index");
        }
    }
}
