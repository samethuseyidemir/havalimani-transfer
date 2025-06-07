using System;
using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Sirket
{
    public class SirketRequest : SearchDto
    {
        public int? Id { get; set; }
        public string? Ad { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }
        public string? Adres { get; set; }
        public bool? AktifMi { get; set; }
        public int? KullaniciId { get; set; }
    }

    public class SirketBaseRequest
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
        public string? LogoPath { get; set; }
        public string? FaaliyetBelgesiPath { get; set; }
    }

    public class SirketAdiRequest
    {
        public string Ad { get; set; }
    }
}
