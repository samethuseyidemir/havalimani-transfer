using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HavalimaniPanelMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AracController : Controller
    {
        private readonly HttpClient _httpClient;

        public AracController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // LISTE
        public async Task<IActionResult> Index()
        {
            var request = new { startIndex = 0, maxCount = 100 };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Arac/search", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Arac>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(result.Data);
            }

            ViewBag.Error = "Araçlar listelenemedi.";
            return View(new List<Arac>());
        }

        // CREATE - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var sirketListesi = await GetSirketListesiAsync();

            var viewModel = new AracCreateUpdateViewModel
            {
                Arac = new Arac(),
                SirketListesi = sirketListesi
            };

            return View(viewModel);
        }

        // CREATE - POST
        [HttpPost]
        public async Task<IActionResult> Create(AracCreateUpdateViewModel model)
        {
            model.Arac.KayitTarihi = DateTime.Now;

            var content = new StringContent(JsonSerializer.Serialize(model.Arac), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Arac/insert", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Ekleme başarısız.";
            model.SirketListesi = await GetSirketListesiAsync();
            return View(model);
        }

        // EDIT - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/Arac/get-by-id?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<Arac>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var sirketler = await GetSirketListesiAsync();

                var viewModel = new AracCreateUpdateViewModel
                {
                    Arac = result.Data,
                    SirketListesi = sirketler
                };

                return View(viewModel);
            }

            return NotFound();
        }

        // EDIT - POST
        [HttpPost]
        public async Task<IActionResult> Edit(AracCreateUpdateViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model.Arac), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Arac/update", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Güncelleme başarısız.";
            model.SirketListesi = await GetSirketListesiAsync();
            return View(model);
        }

        // DELETE - POST
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Id = id }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Arac/delete", content);

            return RedirectToAction("Index");
        }

        // HELPER: Şirket Listesi (Dropdown için)
        private async Task<List<HavalimaniPanelMVC.Models.SirketModel>> GetSirketListesiAsync()
        {
            var request = new { startIndex = 0, maxCount = 100 };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Sirket/search", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<HavalimaniPanelMVC.Models.SirketModel>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result.Data;
            }

            return new List<HavalimaniPanelMVC.Models.SirketModel>();
        }
    }
}
