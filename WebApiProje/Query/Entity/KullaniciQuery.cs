namespace WebApiProje.Query.Entity
{
    public class KullaniciQuery
    {
        public const string GetKullaniciSql = @"
            SELECT * FROM [dbo].[Kullanici]
            WHERE 1=1";

        public const string GetKullaniciCountSql = @"
            SELECT COUNT(Id)
            FROM [dbo].[Kullanici]
            WHERE 1=1";

        public const string InsertKullaniciSql = @"
            INSERT INTO [dbo].[Kullanici] (Ad, Soyad, Email, Sifre, Telefon, RolId, AktifMi, KayitTarihi)
            OUTPUT INSERTED.*
            VALUES (@Ad, @Soyad, @Email, @Sifre, @Telefon, @RolId, @AktifMi, @KayitTarihi)";

        public const string UpdateKullaniciSql = @"
            UPDATE [dbo].[Kullanici]
            SET Ad = @Ad,
                Soyad = @Soyad,
                Email = @Email,
                Sifre = @Sifre,
                Telefon = @Telefon,
                RolId = @RolId,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteKullaniciSql = @"
            DELETE FROM [dbo].[Kullanici]
            WHERE Id = @Id";
    }
}
