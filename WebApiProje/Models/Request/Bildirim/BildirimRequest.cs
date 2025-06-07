using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Bildirim
{
    public class BildirimRequest : SearchDto
    {
        public int? Id { get; set; }
        public int? KullaniciId { get; set; }
        public bool? OkunduMu { get; set; }
    }

    public class BildirimBaseRequest
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string Baslik { get; set; }
        public string Mesaj { get; set; }
        public string Tip { get; set; }
        public bool OkunduMu { get; set; }
        public DateTime GonderimTarihi { get; set; }
    }
    public class BildirimAdiRequest
    {
        public string Baslik { get; set; }
    }

}
