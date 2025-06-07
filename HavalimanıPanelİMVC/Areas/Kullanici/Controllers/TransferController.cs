using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace HavalimaniPanelMVC.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    public class TransferController : Controller
    {
        private readonly HttpClient _httpClient;

        public TransferController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7116");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new TransferAramaModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TransferAramaModel model)
        {
            var requestBody = new
            {
                BaslangicNoktasi = model.BaslangicNoktasi,
                BitisNoktasi = model.BitisNoktasi,
                TarihSaat = model.TarihSaat
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Transfer/search", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse<List<Transfer>>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                model.Sonuclar = result.Data;
            }
            else
            {
                model.Sonuclar = new List<Transfer>();
            }

            return View(model);
        }
    }
}
