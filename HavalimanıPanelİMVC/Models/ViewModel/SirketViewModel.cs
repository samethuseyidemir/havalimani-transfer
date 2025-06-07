using System;
using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class SirketViewModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string VergiNo { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }

        public int? KullaniciId { get; set; }
        public string? LogoPath { get; set; }
        public string? FaaliyetBelgesiPath { get; set; }
    }

    public class SirketCreateViewModel
    {
        [Required]
        public string Ad { get; set; }

        [Required]
        public string VergiNo { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Adres { get; set; }
    }

    public class SirketUpdateViewModel : SirketCreateViewModel
    {
        public int Id { get; set; }
    }
}
