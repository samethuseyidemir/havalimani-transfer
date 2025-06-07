using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HavalimaniPanelMVC.Areas.Sirket.Controllers
{
    [Area("Sirket")]
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
            var sirketIdString = HttpContext.Session.GetString("SirketId");
            Console.WriteLine($"[DEBUG] Index() - SirketId in Session: {sirketIdString}");

            if (string.IsNullOrEmpty(sirketIdString))
            {
                ViewBag.Hata = "HATA: Session'da SirketId bulunamadı! Lütfen tekrar giriş yapınız.";
                return View(new List<Arac>());
            }

            var sirketId = int.Parse(sirketIdString);

            var request = new { startIndex = 0, maxCount = 100, SirketId = sirketId };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Arac/search", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Hata = "Araçlar yüklenirken bir hata oluştu.";
                return View(new List<Arac>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<Arac>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(result.Data);
        }

        // CREATE - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Arac());
        }

        // CREATE - POST
        [HttpPost]
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create(Arac model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var sirketId = int.Parse(HttpContext.Session.GetString("SirketId")!);

            var dto = new
            {
                model.Marka,
                model.Model,
                model.Plaka,
                model.KoltukSayisi,
                model.BagajKapasitesi,
                SirketId = sirketId,
                AktifMi = true,
                KayitTarihi = DateTime.Now
            };

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            try
            {
                Console.WriteLine("[DEBUG] /Arac/insert çağrılıyor...");
                Console.WriteLine("[DEBUG] DTO: " + JsonSerializer.Serialize(dto));

                var response = await _httpClient.PostAsync("/Arac/insert", content);

                Console.WriteLine("[DEBUG] Response StatusCode: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Basarili"] = "Araç başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Hata = $"API HATASI: {response.StatusCode}";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERROR] Exception oluştu: " + ex.Message);
                ViewBag.Hata = "Bir hata oluştu: " + ex.Message;
                return View(model);
            }
        }

        // EDIT - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/Arac/get-by-id?id={id}");
            if (!response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<Arac>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(result.Data);
        }

        // EDIT - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Arac model)
        {
            if (!ModelState.IsValid) return View(model);

            var sirketId = int.Parse(HttpContext.Session.GetString("SirketId")!);

            var dto = new
            {
                model.Id,
                model.Marka,
                model.Model,
                model.Plaka,
                model.KoltukSayisi,
                model.BagajKapasitesi,
                SirketId = sirketId,
                AktifMi = model.AktifMi,
                KayitTarihi = model.KayitTarihi
            };

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Arac/update", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Basarili"] = "Araç güncellemesi başarılı.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hata = "Güncelleme sırasında bir hata oluştu.";
            return View(model);
        }

        // DELETE - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"/Arac/get-by-id?id={id}");
            if (!response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<Arac>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(result.Data);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Id = id }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Arac/delete", content);

            if (response.IsSuccessStatusCode)
                TempData["Basarili"] = "Araç başarıyla silindi.";
            else
                TempData["Hata"] = "Silme sırasında bir hata oluştu.";

            return RedirectToAction(nameof(Index));
        }
    }
}
