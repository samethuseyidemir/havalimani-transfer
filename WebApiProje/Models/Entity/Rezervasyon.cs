namespace WebApiProje.Models.Entity
{
    public class Rezervasyon
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int TransferId { get; set; }
        public DateTime ReservedAt { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
