namespace HavalimaniPanelMVC.Models
{
    public class SirketModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string VergiNo { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public bool AktifMi { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
