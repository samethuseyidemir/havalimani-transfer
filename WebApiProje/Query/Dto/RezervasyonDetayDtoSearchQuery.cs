namespace WebApiProje.Query.Dto
{
    public class RezervasyonDetayDtoSearchQuery
    {
        public const string Sql = @"
            SELECT 
                r.Id AS RezervasyonId,
                r.ReservedAt,
                r.KayitTarihi,
                r.AktifMi,

                k.Ad + ' ' + k.Soyad AS KullaniciAdiSoyadi,
                k.Email AS KullaniciEmail,

                t.BaslangicNoktasi,
                t.BitisNoktasi,
                t.TarihSaat,
                t.Ucret,

                a.Plaka,
                a.Model,

                s.Ad AS SirketAdi

            FROM Rezervasyon r
            INNER JOIN Kullanici k ON r.KullaniciId = k.Id
            INNER JOIN Transfer t ON r.TransferId = t.Id
            INNER JOIN Arac a ON t.AracId = a.Id
            INNER JOIN Sirket s ON a.SirketId = s.Id
            WHERE r.AktifMi = 1
            ORDER BY r.KayitTarihi DESC;
        ";
    }
}
