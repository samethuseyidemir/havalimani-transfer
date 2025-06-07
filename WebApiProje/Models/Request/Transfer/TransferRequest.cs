using Core.Toolkit.Search;

namespace WebApiProje.Models.Request.Transfer
{
    public class TransferRequest : SearchDto
    {
        public int? Id { get; set; }
        public string? BaslangicNoktasi { get; set; }
        public string? BitisNoktasi { get; set; }
        public int? AracId { get; set; }
        public bool? AktifMi { get; set; }
    }

    public class TransferBaseRequest
    {
        public int Id { get; set; } 
        public string BaslangicNoktasi { get; set; }
        public string BitisNoktasi { get; set; }
        public DateTime TarihSaat { get; set; }
        public decimal Ucret { get; set; }
        public int AracId { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
    public class TransferAdiRequest
    {
        public string BaslangicNoktasi { get; set; }
    }

}
