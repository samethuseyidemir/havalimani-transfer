using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class HizmetNoktasi
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Şirket bilgisi zorunludur.")]
        public int SirketId { get; set; }

        [Required(ErrorMessage = "Başlangıç noktası zorunludur.")]
        public string BaslangicNoktasi { get; set; }

        [Required(ErrorMessage = "Bitiş noktası zorunludur.")]
        public string BitisNoktasi { get; set; }

        public decimal? MesafeKm { get; set; }

        public int? TahminiSureDakika { get; set; }

        public bool AktifMi { get; set; }

        public DateTime KayitTarihi { get; set; }
    }
}
