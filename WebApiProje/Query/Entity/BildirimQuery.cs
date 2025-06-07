namespace WebApiProje.Query.Entity
{
    public class BildirimQuery
    {
        public const string GetBildirimSql = @"SELECT * FROM Bildirim WHERE 1=1";

        public const string GetBildirimCountSql = @"SELECT COUNT(Id) FROM Bildirim WHERE 1=1";

        public const string InsertBildirimSql = @"
            INSERT INTO Bildirim (KullaniciId, Baslik, Mesaj, Tip, OkunduMu, GonderimTarihi)
            OUTPUT INSERTED.*
            VALUES (@KullaniciId, @Baslik, @Mesaj, @Tip, @OkunduMu, @GonderimTarihi)";

        public const string UpdateBildirimSql = @"
            UPDATE Bildirim
            SET KullaniciId = @KullaniciId,
                Baslik = @Baslik,
                Mesaj = @Mesaj,
                Tip = @Tip,
                OkunduMu = @OkunduMu,
                GonderimTarihi = @GonderimTarihi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteBildirimSql = @"DELETE FROM Bildirim WHERE Id = @Id";
    }
}
