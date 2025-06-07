using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HavalimaniPanelMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaturaController : Controller
    {
        private readonly HttpClient _httpClient;

        public FaturaController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116"); // API adresin
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            var request = new { startIndex = 0, maxCount = 100 };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Fatura/search", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Fatura>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(result.Data);
            }

            ViewBag.Error = "Faturalar getirilemedi.";
            return View(new List<Fatura>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Fatura { FaturaTarihi = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Fatura model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Fatura/insert", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Ekleme işlemi başarısız.";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.PostAsync("/Fatura/search", new StringContent(
                JsonSerializer.Serialize(new { Id = id, startIndex = 0, maxCount = 1 }),
                Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<Fatura>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(result.Data.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Fatura model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Fatura/update", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ViewBag.Error = "Güncelleme işlemi başarısız.";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Id = id }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Fatura/delete", content);

            return RedirectToAction("Index");
        }
    }
}
