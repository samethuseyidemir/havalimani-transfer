using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Rezervasyon
{
    public class RezervasyonRequest : SearchDto
    {
        public int? Id { get; set; }
        public int? KullaniciId { get; set; }
        public int? TransferId { get; set; }
        public bool? AktifMi { get; set; }

        // 🔽 EKLENMESİ GEREKENLER
        public string KullaniciAdSoyad { get; set; }
        public string TransferBilgisi { get; set; }
        public DateTime? MinTarih { get; set; }
        public DateTime? MaxTarih { get; set; }
    }

    public class RezervasyonBaseRequest
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int TransferId { get; set; }
        public DateTime ReservedAt { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
