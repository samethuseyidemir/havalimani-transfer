using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HavalimaniPanelMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransferController : Controller
    {
        private readonly HttpClient _httpClient;

        public TransferController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116"); // API portuna göre ayarla
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.PostAsync("/Transfer/search", new StringContent(
                JsonSerializer.Serialize(new { startIndex = 0, maxCount = 100 }),
                System.Text.Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Transfer>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(result.Data);
            }

            return View(new List<Transfer>());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new TransferCreateViewModel();

            var aracResponse = await _httpClient.PostAsync("/Arac/search", new StringContent(
                JsonSerializer.Serialize(new { startIndex = 0, maxCount = 100 }),
                System.Text.Encoding.UTF8, "application/json"));

            if (aracResponse.IsSuccessStatusCode)
            {
                var json = await aracResponse.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Arac>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                viewModel.Araclar = result.Data;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransferCreateViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                BaslangicNoktasi = model.BaslangicNoktasi,
                BitisNoktasi = model.BitisNoktasi,
                TarihSaat = model.TarihSaat,
                Ucret = model.Ucret,
                AracId = model.AracId,
                AktifMi = model.AktifMi,
                KayitTarihi = DateTime.Now
            }), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Transfer/insert", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");   

            ViewBag.Error = "Kayıt sırasında hata oluştu.";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Transfer bilgisi getir
            var response = await _httpClient.GetAsync($"/Transfer/get-by-id?id={id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<Transfer>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var model = new TransferCreateViewModel
            {
                BaslangicNoktasi = result.Data.BaslangicNoktasi,
                BitisNoktasi = result.Data.BitisNoktasi,
                TarihSaat = result.Data.TarihSaat,
                Ucret = result.Data.Ucret,
                AracId = result.Data.AracId,
                AktifMi = result.Data.AktifMi
            };

            ViewBag.TransferId = id;

            // Araçları getir
            var aracResponse = await _httpClient.PostAsync("/Arac/search", new StringContent(
                JsonSerializer.Serialize(new { startIndex = 0, maxCount = 100 }),
                System.Text.Encoding.UTF8, "application/json"));

            if (aracResponse.IsSuccessStatusCode)
            {
                var aracJson = await aracResponse.Content.ReadAsStringAsync();
                var aracResult = JsonSerializer.Deserialize<ApiResponse<List<Arac>>>(aracJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                model.Araclar = aracResult.Data;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TransferCreateViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                Id = id,
                BaslangicNoktasi = model.BaslangicNoktasi,
                BitisNoktasi = model.BitisNoktasi,
                TarihSaat = model.TarihSaat,
                Ucret = model.Ucret,
                AracId = model.AracId,
                AktifMi = model.AktifMi
            }), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Transfer/update", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Güncelleme sırasında hata oluştu.";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Id = id }), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Transfer/delete", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            TempData["Error"] = "Silme işlemi başarısız.";
            return RedirectToAction("Index");
        }

    }
}
