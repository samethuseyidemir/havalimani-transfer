namespace WebApiProje.Models.Entity
{
    public class Bildirim
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public string Tip { get; set; } // "Email", "SMS", "Sistem" gibi
        public bool OkunduMu { get; set; }
        public DateTime GonderimTarihi { get; set; }
    }
}
