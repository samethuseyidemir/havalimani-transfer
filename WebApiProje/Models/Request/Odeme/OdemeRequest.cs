using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Odeme
{
    public class OdemeRequest : SearchDto
    {
        public int? Id { get; set; }
        public int? RezervasyonId { get; set; }
        public bool? AktifMi { get; set; }
    }

    public class OdemeBaseRequest
    {
        public int Id { get; set; }
        public int RezervasyonId { get; set; }
        public decimal Tutar { get; set; }
        public string OdemeYontemi { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
