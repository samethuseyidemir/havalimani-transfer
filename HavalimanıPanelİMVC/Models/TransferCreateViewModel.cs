namespace HavalimaniPanelMVC.Models
{
    public class TransferCreateViewModel
    {
        public string BaslangicNoktasi { get; set; }
        public string BitisNoktasi { get; set; }
        public DateTime TarihSaat { get; set; }
        public decimal Ucret { get; set; }
        public int AracId { get; set; }
        public bool AktifMi { get; set; }

        public List<Arac> Araclar { get; set; }
    }
}
