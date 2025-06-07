using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.HizmetNoktasi
{
    public class HizmetNoktasiRequest : SearchDto
    {
        public int? Id { get; set; }
        public int? SirketId { get; set; }
        public string? BaslangicNoktasi { get; set; }
        public string? BitisNoktasi { get; set; }
        public bool? AktifMi { get; set; }
    }

    public class HizmetNoktasiBaseRequest
    {
        public int Id { get; set; }
        public int SirketId { get; set; }
        public string BaslangicNoktasi { get; set; }
        public string BitisNoktasi { get; set; }
        public decimal? MesafeKm { get; set; }
        public int? TahminiSureDakika { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
    }

    public class HizmetNoktasiSirketRequest
    {
        public int SirketId { get; set; }
    }
}
