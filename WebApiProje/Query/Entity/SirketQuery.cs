namespace WebApiProje.Query.Entity
{
    public static class SirketQuery
    {
        public const string GetSirketSql = @"
            SELECT *
              FROM Sirket
             WHERE 1=1";

        public const string GetSirketCountSql = @"
            SELECT COUNT(Id)
              FROM Sirket
             WHERE 1=1";

        public const string InsertSirketSql = @"
            INSERT INTO Sirket
                (KullaniciId, Ad, VergiNo, Telefon, Email, Adres, AktifMi, KayitTarihi, LogoPath, FaaliyetBelgesiPath)
            OUTPUT INSERTED.*
            VALUES
                (@KullaniciId, @Ad, @VergiNo, @Telefon, @Email, @Adres, @AktifMi, @KayitTarihi, @LogoPath, @FaaliyetBelgesiPath)";

        public const string UpdateSirketSql = @"
            UPDATE Sirket
               SET KullaniciId         = @KullaniciId,
                   Ad                  = @Ad,
                   VergiNo             = @VergiNo,
                   Telefon             = @Telefon,
                   Email               = @Email,
                   Adres               = @Adres,
                   AktifMi             = @AktifMi,
                   LogoPath            = @LogoPath,
                   FaaliyetBelgesiPath = @FaaliyetBelgesiPath
            OUTPUT INSERTED.*
             WHERE Id = @Id";

        public const string DeleteSirketSql = @"
            DELETE
              FROM Sirket
             WHERE Id = @Id";
    }
}
