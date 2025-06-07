using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Yorum
{
    public class YorumRequest : SearchDto
    {
        public int? Id { get; set; }
        public int? KullaniciId { get; set; }
        public bool? AktifMi { get; set; }
    }

    public class YorumBaseRequest
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Icerik { get; set; }
        public DateTime YorumTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
    public class YorumAdiRequest
    {
        public string Icerik { get; set; }
    }

}
