using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HavalimaniPanelMVC.Models
{
    public class SirketProfileViewModel
    {
        // AuthController’dan redirect ederken bu userId’yi query olarak alacağız
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Vergi numarası zorunludur.")]
        public string VergiNo { get; set; }

        [Required(ErrorMessage = "Telefon zorunludur.")]
        public string Telefon { get; set; }

        [Required, EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz.")]
        public string Email { get; set; }

        public string Adres { get; set; }

        // Dosya yükleme alanları
        [Display(Name = "Logo Dosyası")]
        public IFormFile LogoDosya { get; set; }

        [Display(Name = "Faaliyet Belgesi (PDF)")]
        public IFormFile FaaliyetBelgesiDosya { get; set; }
    }
}
