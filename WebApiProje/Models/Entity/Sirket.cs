using System;

namespace WebApiProje.Models.Entity
{
    public class Sirket
    {
        public int Id { get; set; }
        public int? KullaniciId { get; set; }
        public string Ad { get; set; }
        public string VergiNo { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }

        // Profil tamamlama ile yüklenecek dosya yolları
        public string? LogoPath { get; set; }
        public string? FaaliyetBelgesiPath { get; set; }
    }
}
