using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using HavalimaniPanelMVC.Models;

namespace HavalimaniPanelMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly HttpClient _httpClient;

        public DashboardController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.KullaniciSayisi = await GetSayisi("/Kullanici/search");
            ViewBag.SirketSayisi = await GetSayisi("/Sirket/search");
            ViewBag.TransferSayisi = await GetSayisi("/Transfer/search");
            ViewBag.BugunkuRezervasyon = await GetBugunkuRezervasyonSayisi();

            return View();
        }

        private async Task<int> GetSayisi(string endpoint)
        {
            var response = await _httpClient.PostAsync(endpoint,
                new StringContent(JsonSerializer.Serialize(new { startIndex = 0, maxCount = 1 }),
                System.Text.Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return 0;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<object>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.TotalCount ?? 0;
        }

        private async Task<int> GetBugunkuRezervasyonSayisi()
        {
            var response = await _httpClient.PostAsync("/Rezervasyon/search",
                new StringContent(JsonSerializer.Serialize(new
                {
                    startIndex = 0,
                    maxCount = 1,
                    MinTarih = DateTime.Today,
                    MaxTarih = DateTime.Today.AddDays(1)
                }), System.Text.Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return 0;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<object>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.TotalCount ?? 0;
        }
    }
}
