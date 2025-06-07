using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Fatura
{
    public class FaturaRequest : SearchDto
    {
        public int? Id { get; set; }
        public int? RezervasyonId { get; set; }
        public bool? AktifMi { get; set; }
    }

    public class FaturaBaseRequest
    {
        public int Id { get; set; }
        public int RezervasyonId { get; set; }
        public decimal Tutar { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}
