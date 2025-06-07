namespace HavalimaniPanelMVC.Models
{
    public class Rezervasyon
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int TransferId { get; set; }
        public DateTime RezervasyonTarihi { get; set; }

        // Görselde kolaylık için
        public string KullaniciAdSoyad { get; set; }
        public string TransferBilgisi { get; set; }
    }
}
