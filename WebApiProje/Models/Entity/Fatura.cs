namespace WebApiProje.Models.Entity
{
    public class Fatura
    {
        public int Id { get; set; }
        public int RezervasyonId { get; set; }
        public decimal Tutar { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
