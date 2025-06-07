using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class Fatura
    {
        public int Id { get; set; }

        [Required]
        public int RezervasyonId { get; set; }

        [Required]
        [Display(Name = "Tutar (₺)")]
        public decimal Tutar { get; set; }

        [Required]
        [Display(Name = "Fatura Tarihi")]
        public DateTime FaturaTarihi { get; set; }

        public bool AktifMi { get; set; }
    }
}
