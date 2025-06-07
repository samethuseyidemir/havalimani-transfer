namespace WebApiProje.Models.Entity
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
    }
}
