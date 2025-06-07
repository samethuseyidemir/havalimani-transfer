using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class RezervasyonCreateViewModel
    {
        public int TransferId { get; set; }

        [Required]
        public string AdSoyad { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefon { get; set; }

        // Eksik olan alanlar:
        public int KullaniciId { get; set; }
        public DateTime ReservedAt { get; set; }
        public bool AktifMi { get; set; } = true;
    }
}
