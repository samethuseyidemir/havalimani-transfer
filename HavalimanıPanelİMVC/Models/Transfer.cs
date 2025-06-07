namespace HavalimaniPanelMVC.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public string BaslangicNoktasi { get; set; }
        public string BitisNoktasi { get; set; }
        public DateTime TarihSaat { get; set; }
        public decimal Ucret { get; set; }
        public int AracId { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }

        // Görselde kolaylık için
        public string AracPlaka { get; set; }
        public string TransferBilgisi => $"{BaslangicNoktasi} - {BitisNoktasi}";
    }
}
