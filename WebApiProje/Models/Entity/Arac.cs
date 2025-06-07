namespace WebApiProje.Models.Entity
{
    public class Arac
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Plaka { get; set; }
        public string Model { get; set; }
        public int KoltukSayisi { get; set; }
        public int BagajKapasitesi { get; set; }

        public decimal Fiyat { get; set; } // 🌟 EKLEYECEĞİZ

        public int SirketId { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
