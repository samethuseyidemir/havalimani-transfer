using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HavalimaniPanelMVC.Models;
using Core.Toolkit.Results;

namespace HavalimaniPanelMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SirketController : Controller
    {
        private readonly HttpClient _httpClient;

        public SirketController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7116")
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(bool? aktifMi)
        {
            ViewBag.AktifMiFilter = aktifMi;

            var requestObj = new { startIndex = 0, maxCount = 100, aktifMi };
            var content = new StringContent(JsonSerializer.Serialize(requestObj,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Sirket/search", content);
            if (!response.IsSuccessStatusCode)
                return View(new List<SirketViewModel>());

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<SirketViewModel>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var responseGet = await _httpClient.GetAsync($"/Sirket/get-by-id?id={id}");

            if (!responseGet.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var sirketJson = await responseGet.Content.ReadAsStringAsync();
            var sirket = JsonSerializer.Deserialize<ApiResponse<SirketViewModel>>(sirketJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Data;

            if (sirket == null)
                return RedirectToAction(nameof(Index));

            sirket.AktifMi = true;

            var dto = new
            {
                sirket.Id,
                sirket.KullaniciId,
                sirket.Ad,
                sirket.VergiNo,
                sirket.Telefon,
                sirket.Email,
                sirket.Adres,
                sirket.AktifMi,
                sirket.LogoPath,
                sirket.FaaliyetBelgesiPath
            };

            var content = new StringContent(JsonSerializer.Serialize(dto,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("/Sirket/update", content);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var content = new StringContent(JsonSerializer.Serialize(new { Id = id }),
                Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("/Sirket/delete", content);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(SirketCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new
            {
                Id = 0,
                KullaniciId = (int?)null,
                Ad = model.Ad,
                VergiNo = model.VergiNo,
                Telefon = model.Telefon,
                Email = model.Email,
                Adres = model.Adres,
                AktifMi = false,
                KayitTarihi = DateTime.Now,
                LogoPath = (string?)null,
                FaaliyetBelgesiPath = (string?)null
            };

            var content = new StringContent(JsonSerializer.Serialize(dto,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Sirket/insert", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Oluşturma başarısız.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"/Sirket/get-by-id?id={id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<SirketViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(new SirketUpdateViewModel
            {
                Id = result.Data.Id,
                Ad = result.Data.Ad,
                VergiNo = result.Data.VergiNo,
                Telefon = result.Data.Telefon,
                Email = result.Data.Email,
                Adres = result.Data.Adres
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SirketUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new
            {
                model.Id,
                model.Ad,
                model.VergiNo,
                model.Telefon,
                model.Email,
                model.Adres,
                AktifMi = false
            };

            var content = new StringContent(JsonSerializer.Serialize(dto,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Sirket/update", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Güncelleme başarısız.");
            return View(model);
        }
    }
}