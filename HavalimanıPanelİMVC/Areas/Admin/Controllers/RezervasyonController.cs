using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

// Çakışmayı engellemek için alias
using KullaniciModel = HavalimaniPanelMVC.Models.Kullanici;
using TransferModel = HavalimaniPanelMVC.Models.Transfer;

namespace HavalimaniPanelMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> Index()
        {
            var request = new { startIndex = 0, maxCount = 100 };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Rezervasyon/search", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Rezervasyon>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var model = new RezervasyonSearchModel { Sonuclar = result.Data };
                return View(model);
            }

            return View(new RezervasyonSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(RezervasyonSearchModel model)
        {
            var request = new
            {
                startIndex = 0,
                maxCount = 100,
                kullaniciAdSoyad = model.KullaniciAdSoyad,
                transferBilgisi = model.TransferBilgisi,
                minTarih = model.MinTarih,
                maxTarih = model.MaxTarih
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Rezervasyon/search", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Rezervasyon>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                model.Sonuclar = result.Data;
                return View(model);
            }

            model.Sonuclar = new List<Rezervasyon>();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rezervasyon model)
        {
            var requestBody = new
            {
                KullaniciId = model.KullaniciId,
                TransferId = model.TransferId,
                ReservedAt = model.RezervasyonTarihi
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Rezervasyon/insert", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await LoadDropdowns();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/Rezervasyon/get-by-id?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<Rezervasyon>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                await LoadDropdowns();
                return View(result.Data);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Rezervasyon model)
        {
            var requestBody = new
            {
                Id = model.Id,
                KullaniciId = model.KullaniciId,
                TransferId = model.TransferId,
                ReservedAt = model.RezervasyonTarihi
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Rezervasyon/update", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await LoadDropdowns();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"/Rezervasyon/get-by-id?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<Rezervasyon>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(result.Data);
            }

            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.PostAsync($"/Rezervasyon/delete?id={id}", null);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

        private async Task LoadDropdowns()
        {
            var userResponse = await _httpClient.PostAsync("/Kullanici/search", new StringContent(JsonSerializer.Serialize(new { startIndex = 0, maxCount = 100 }), Encoding.UTF8, "application/json"));
            var transferResponse = await _httpClient.PostAsync("/Transfer/search", new StringContent(JsonSerializer.Serialize(new { startIndex = 0, maxCount = 100 }), Encoding.UTF8, "application/json"));

            if (userResponse.IsSuccessStatusCode)
            {
                var json = await userResponse.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<KullaniciModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.Kullanicilar = result.Data;
            }

            if (transferResponse.IsSuccessStatusCode)
            {
                var json = await transferResponse.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<TransferModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.Transferler = result.Data;
            }
        }
    }
}
