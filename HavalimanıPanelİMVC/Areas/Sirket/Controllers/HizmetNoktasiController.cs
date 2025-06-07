using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HavalimaniPanelMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HavalimaniPanelMVC.Areas.Sirket.Controllers
{
    [Area("Sirket")]
    public class HizmetNoktasiController : Controller
    {
        private readonly HttpClient _httpClient;

        public HizmetNoktasiController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7116") };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: /Sirket/HizmetNoktasi
        public async Task<IActionResult> Index()
        {
            var sirketId = int.Parse(HttpContext.Session.GetString("SirketId")!);

            var requestObj = new { startIndex = 0, maxCount = 100, SirketId = sirketId };
            var content = new StringContent(JsonSerializer.Serialize(requestObj), Encoding.UTF8, "application/json");

            var resp = await _httpClient.PostAsync("/HizmetNoktasi/search", content);
            if (!resp.IsSuccessStatusCode)
            {
                ViewBag.Hata = "Hizmet noktaları yüklenirken bir hata oluştu.";
                return View(new List<HizmetNoktasiViewModel>());
            }

            var json = await resp.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<HizmetNoktasiViewModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(result.Data);
        }

        // GET: /Sirket/HizmetNoktasi/Create
        [HttpGet]
        public IActionResult Create() => View();

        // POST: /Sirket/HizmetNoktasi/Create
        [HttpPost]
        public async Task<IActionResult> Create(HizmetNoktasiCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var sirketId = int.Parse(HttpContext.Session.GetString("SirketId")!);

            var dto = new
            {
                BaslangicNoktasi = model.BaslangicNoktasi,
                BitisNoktasi = model.BitisNoktasi,
                MesafeKm = model.MesafeKm,
                TahminiSureDakika = model.TahminiSureDakika,
                SirketId = sirketId,
                AktifMi = true,
                KayitTarihi = DateTime.Now
            };

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("/HizmetNoktasi/create", content);

            if (resp.IsSuccessStatusCode)
            {
                TempData["Basarili"] = "Hizmet noktası başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hata = "Ekleme sırasında bir hata oluştu.";
            return View(model);
        }

        // GET: /Sirket/HizmetNoktasi/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var resp = await _httpClient.GetAsync($"/HizmetNoktasi/get-by-id?id={id}");
            if (!resp.IsSuccessStatusCode) return RedirectToAction(nameof(Index));

            var json = await resp.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<HizmetNoktasiViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var model = new HizmetNoktasiCreateViewModel
            {
                BaslangicNoktasi = result.Data.BaslangicNoktasi,
                BitisNoktasi = result.Data.BitisNoktasi,
                MesafeKm = result.Data.MesafeKm ?? 0,
                TahminiSureDakika = result.Data.TahminiSureDakika ?? 0
            };

            ViewBag.Id = result.Data.Id;
            return View(model);
        }

        // POST: /Sirket/HizmetNoktasi/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, HizmetNoktasiCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Id = id;
                return View(model);
            }

            var sirketId = int.Parse(HttpContext.Session.GetString("SirketId")!);

            var dto = new
            {
                Id = id,
                BaslangicNoktasi = model.BaslangicNoktasi,
                BitisNoktasi = model.BitisNoktasi,
                MesafeKm = model.MesafeKm,
                TahminiSureDakika = model.TahminiSureDakika,
                SirketId = sirketId,
                AktifMi = true,
                KayitTarihi = DateTime.Now
            };

            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var resp = await _httpClient.PutAsync("/HizmetNoktasi/update", content);

            if (resp.IsSuccessStatusCode)
            {
                TempData["Basarili"] = "Hizmet noktası güncellemesi başarılı.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hata = "Güncelleme sırasında bir hata oluştu.";
            ViewBag.Id = id;
            return View(model);
        }

        // GET: /Sirket/HizmetNoktasi/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var resp = await _httpClient.DeleteAsync($"/HizmetNoktasi/delete/{id}");

            if (resp.IsSuccessStatusCode)
                TempData["Basarili"] = "Hizmet noktası başarıyla silindi.";
            else
                TempData["Hata"] = "Silme sırasında bir hata oluştu.";

            return RedirectToAction(nameof(Index));
        }
    }
}
