namespace WebApiProje.Models.Entity
{
    public class Odeme
    {
        public int Id { get; set; }
        public int RezervasyonId { get; set; }
        public decimal Tutar { get; set; }
        public string OdemeYontemi { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
