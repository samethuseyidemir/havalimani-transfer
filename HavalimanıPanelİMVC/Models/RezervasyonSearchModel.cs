namespace HavalimaniPanelMVC.Models
{
    public class RezervasyonSearchModel
    {
        public string KullaniciAdSoyad { get; set; }
        public string TransferBilgisi { get; set; }
        public DateTime? MinTarih { get; set; }
        public DateTime? MaxTarih { get; set; }

        public List<Rezervasyon> Sonuclar { get; set; } = new();
    }
}
