using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Kullanici
{
 
    public class KullaniciRequest : SearchDto
    {
        public int? Id { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? Email { get; set; }
        public int? RolId { get; set; }
        public bool? AktifMi { get; set; }
    }


    public class KullaniciBaseRequest
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string Telefon { get; set; }
        public int RolId { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; } // ❗️EN ÖNEMLİ
    }



    public class KullaniciIdRequest
    {
        public int Id { get; set; }
    }
}
