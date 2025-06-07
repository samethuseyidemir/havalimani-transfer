namespace WebApiProje.Models.Entity
{
    public class Yorum
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Icerik { get; set; }
        public DateTime YorumTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
