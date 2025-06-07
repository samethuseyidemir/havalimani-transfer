using System.ComponentModel.DataAnnotations;

namespace HavalimaniPanelMVC.Models
{
    public class HizmetNoktasiCreateViewModel
    {
        [Required(ErrorMessage = "Başlangıç noktası zorunludur.")]
        public string BaslangicNoktasi { get; set; }

        [Required(ErrorMessage = "Bitiş noktası zorunludur.")]
        public string BitisNoktasi { get; set; }

        [Required(ErrorMessage = "Mesafe kilometre zorunludur.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Mesafe 0'dan büyük olmalıdır.")]
        public decimal MesafeKm { get; set; }

        [Required(ErrorMessage = "Tahmini süre zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Süre 1 dakikadan büyük olmalıdır.")]
        public int TahminiSureDakika { get; set; }
    }
}
