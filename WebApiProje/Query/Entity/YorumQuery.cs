namespace WebApiProje.Query.Entity
{
    public class YorumQuery
    {
        public const string GetYorumSql = @"
            SELECT * FROM Yorum WHERE 1=1";

        public const string GetYorumCountSql = @"
            SELECT COUNT(Id) FROM Yorum WHERE 1=1";

        public const string InsertYorumSql = @"
            INSERT INTO Yorum (KullaniciId, Icerik, YorumTarihi, AktifMi)
            OUTPUT INSERTED.*
            VALUES (@KullaniciId, @Icerik, @YorumTarihi, @AktifMi)";

        public const string UpdateYorumSql = @"
            UPDATE Yorum
            SET KullaniciId = @KullaniciId,
                Icerik = @Icerik,
                YorumTarihi = @YorumTarihi,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteYorumSql = @"
            DELETE FROM Yorum WHERE Id = @Id";
    }
}
