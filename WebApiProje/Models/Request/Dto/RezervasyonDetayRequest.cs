namespace WebApiProje.Models.Request.Dto
{
    public class RezervasyonDetayRequest
    {
        public int RezervasyonId { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime KayitTarihi { get; set; }
        public bool AktifMi { get; set; }

        public string KullaniciAdiSoyadi { get; set; }
        public string KullaniciEmail { get; set; }

        public string BaslangicNoktasi { get; set; }
        public string BitisNoktasi { get; set; }
        public DateTime TarihSaat { get; set; }
        public decimal Ucret { get; set; }

        public string Plaka { get; set; }
        public string Model { get; set; }

        public string SirketAdi { get; set; }
    }
}
