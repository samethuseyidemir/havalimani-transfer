using System;
using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class SirketGuncelleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; }

        [Required]
        public string VergiNo { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Adres { get; set; }

        public bool AktifMi { get; set; }

        public DateTime KayitTarihi { get; set; }
    }
}
