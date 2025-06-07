namespace WebApiProje.Query.Entity
{
    public class RezervasyonQuery
    {
        public const string GetRezervasyonSql = @"
            SELECT * FROM Rezervasyon WHERE 1=1";

        public const string GetRezervasyonCountSql = @"
            SELECT COUNT(Id) FROM Rezervasyon WHERE 1=1";

        public const string InsertRezervasyonSql = @"
            INSERT INTO Rezervasyon (KullaniciId, TransferId, ReservedAt, AktifMi, KayitTarihi)
            OUTPUT INSERTED.*
            VALUES (@KullaniciId, @TransferId, @ReservedAt, @AktifMi, @KayitTarihi)";

        public const string UpdateRezervasyonSql = @"
            UPDATE Rezervasyon
            SET KullaniciId = @KullaniciId,
                TransferId = @TransferId,
                ReservedAt = @ReservedAt,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteRezervasyonSql = @"
            DELETE FROM Rezervasyon WHERE Id = @Id";
    }
}
