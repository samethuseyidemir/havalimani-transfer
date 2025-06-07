using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Mesaj
{
    public class MesajRequest : SearchDto
    {
        public int? Id { get; set; }
        public int? GonderenId { get; set; }
        public int? AliciId { get; set; }
        public bool? OkunduMu { get; set; }
    }

    public class MesajBaseRequest
    {
        public int Id { get; set; }
        public int GonderenId { get; set; }
        public int AliciId { get; set; }
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; }
        public bool OkunduMu { get; set; }
    }
    public class MesajAdiRequest
    {
        public string Icerik { get; set; }
    }

}
