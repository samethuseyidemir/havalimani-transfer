using System;
using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class Arac
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Marka alanı zorunludur.")]
        public string Marka { get; set; }

        [Required(ErrorMessage = "Plaka alanı zorunludur.")]
        public string Plaka { get; set; }

        [Required(ErrorMessage = "Model alanı zorunludur.")]
        public string Model { get; set; }

        public int KoltukSayisi { get; set; }
        public int BagajKapasitesi { get; set; }
        public int SirketId { get; set; }
        public string? SirketAdi { get; set; } // Admin tarafında görüntüleme için
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
        public decimal Fiyat { get; set; }

    }
}
