namespace WebApiProje.Models.Entity
{
    public class Mesaj
    {
        public int Id { get; set; }
        public int GonderenId { get; set; }
        public int AliciId { get; set; }
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; }
        public bool OkunduMu { get; set; }
    }
}
