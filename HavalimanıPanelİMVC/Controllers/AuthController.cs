// Controllers/AuthController.cs
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using HavalimaniPanelMVC.Models;
using Core.Toolkit.Results;
using ApiKullanici = WebApiProje.Models.Entity.Kullanici;
using SirketEntity = WebApiProje.Models.Entity.Sirket;

namespace HavalimaniPanelMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthController()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7116") };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // ──────────────────────────────────────────────────────────
        // 1) NORMAL KULLANICI GİRİŞİ
        // ──────────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(KullaniciLoginModel model)
        {
            var resp = await _httpClient.GetAsync($"/Kullanici/get-by-email?email={model.Email}");
            if (!resp.IsSuccessStatusCode)
            {
                ViewBag.Error = "Kullanıcı bulunamadı.";
                return View();
            }

            var json = await resp.Content.ReadAsStringAsync();
            var userRes = JsonSerializer.Deserialize<ApiResponse<ApiKullanici>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var user = userRes.Data;
            if (user != null && user.Sifre.Trim() == model.Sifre.Trim())
            {
                HttpContext.Session.SetString("KullaniciId", user.Id.ToString());
                HttpContext.Session.SetString("RolId", user.RolId.ToString());
                HttpContext.Session.SetString("AdSoyad", user.Ad + " " + user.Soyad);

                if (user.RolId == 2)
                {
                    var sresp = await _httpClient.GetAsync($"/Sirket/get-by-user-id?userId={user.Id}");
                    if (sresp.IsSuccessStatusCode)
                    {
                        var sj = await sresp.Content.ReadAsStringAsync();
                        var sr = JsonSerializer.Deserialize<ApiResponse<SirketEntity>>(sj, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        var shk = sr.Data;
                        if (shk == null)
                            return RedirectToAction("SirketProfile", new { userId = user.Id });
                        if (!shk.AktifMi)
                        {
                            ViewBag.Error = "Şirketiniz henüz Admin tarafından onaylanmamış. Lütfen bekleyiniz.";
                            return View();
                        }
                        HttpContext.Session.SetString("SirketId", shk.Id.ToString());
                    }
                    else
                    {
                        ViewBag.Error = "Şirket bilgileri alınamadı. Lütfen daha sonra tekrar deneyin.";
                        return View();
                    }
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("RolId", user.RolId.ToString()),
                    new Claim("AdSoyad", user.Ad + " " + user.Soyad)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return user.RolId switch
                {
                    1 => RedirectToAction("Index", "Dashboard", new { area = "Admin" }),
                    2 => RedirectToAction("Index", "SirketPanel", new { area = "Sirket" }),
                    3 => RedirectToAction("Index", "Home", new { area = "" }),
                    _ => RedirectToAction("Login")
                };
            }

            ViewBag.Error = "Şifre yanlış.";
            return View();
        }

        // ──────────────────────────────────────────────────────────
        // 2) NORMAL KULLANICI KAYIT (ROLID = 3)
        // ──────────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(KullaniciRegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.AktifMi = true;
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Kullanici/insert", content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"API Hatası: {error} (Kod: {(int)response.StatusCode})";
                return View(model);
            }

            return RedirectToAction("Login");
        }

        // ──────────────────────────────────────────────────────────
        // 3) ŞİRKET KAYIT (ROLID = 2)
        // ──────────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult SirketRegister() => View();

        [HttpPost]
        public async Task<IActionResult> SirketRegister(KullaniciRegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.AktifMi = true;
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Kullanici/insert", content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"API Hatası: {error} (Kod: {(int)response.StatusCode})";
                return View(model);
            }

            var respJson = await response.Content.ReadAsStringAsync();
            var userRes = JsonSerializer.Deserialize<ApiResponse<ApiKullanici>>(respJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return RedirectToAction("SirketProfile", new { userId = userRes.Data.Id });
        }

        // ──────────────────────────────────────────────────────────
        // 4) ŞİRKET PROFİL TAMAMLAMA
        // ──────────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult SirketProfile(int userId)
        {
            var vm = new SirketProfileViewModel { KullaniciId = userId };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SirketProfile(SirketProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.Ad ?? ""), "Ad");
            content.Add(new StringContent(model.VergiNo ?? ""), "VergiNo");
            content.Add(new StringContent(model.Telefon ?? ""), "Telefon");
            content.Add(new StringContent(model.Email ?? ""), "Email");
            content.Add(new StringContent(model.Adres ?? ""), "Adres");
            content.Add(new StringContent(model.KullaniciId.ToString()), "KullaniciId");
            content.Add(new StringContent(false.ToString()), "AktifMi");
            content.Add(new StringContent(DateTime.Now.ToString("o")), "KayitTarihi");

            if (model.LogoDosya?.Length > 0)
            {
                var logoContent = new StreamContent(model.LogoDosya.OpenReadStream());
                logoContent.Headers.ContentType = new MediaTypeHeaderValue(model.LogoDosya.ContentType);
                content.Add(logoContent, nameof(model.LogoDosya), model.LogoDosya.FileName);
            }

            if (model.FaaliyetBelgesiDosya?.Length > 0)
            {
                var belgeContent = new StreamContent(model.FaaliyetBelgesiDosya.OpenReadStream());
                belgeContent.Headers.ContentType = new MediaTypeHeaderValue(model.FaaliyetBelgesiDosya.ContentType);
                content.Add(belgeContent, nameof(model.FaaliyetBelgesiDosya), model.FaaliyetBelgesiDosya.FileName);
            }

            var response = await _httpClient.PostAsync("/Sirket/insert", content);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = $"API Hatası: {error} (Kod: {(int)response.StatusCode})";
                return View(model);
            }

            ViewBag.Success = "Profil bilgileri kaydedildi. Admin onay sürecini bekleyiniz.";
            return View(model);
        }

        // ──────────────────────────────────────────────────────────
        // 5) ÇIKIŞ (LOGOUT)
        // ──────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
