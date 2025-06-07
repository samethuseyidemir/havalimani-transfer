using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Arac
{
    public class AracRequest : SearchDto
    {
        public int? Id { get; set; }
        public string? Plaka { get; set; }
        public string? Model { get; set; }
        public int? SirketId { get; set; }
        public bool? AktifMi { get; set; }
    }

    public class AracBaseRequest
    {
        public int Id { get; set; }
        public string Marka { get; set; } // ✔️ EKLE
        public string Plaka { get; set; }
        public string Model { get; set; }
        public int KoltukSayisi { get; set; }
        public int BagajKapasitesi { get; set; } // ✔️ Adını değiştir
        public int SirketId { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
        public decimal Fiyat { get; set; }

    }

    public class AracAdiRequest
    {
        public string Plaka { get; set; }
    }

}
