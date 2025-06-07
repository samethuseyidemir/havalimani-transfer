namespace WebApiProje.Query.Entity
{
    public class OdemeQuery
    {
        public const string GetOdemeSql = @"SELECT * FROM Odeme WHERE 1=1";

        public const string GetOdemeCountSql = @"SELECT COUNT(Id) FROM Odeme WHERE 1=1";

        public const string InsertOdemeSql = @"
            INSERT INTO Odeme (RezervasyonId, Tutar, OdemeYontemi, OdemeTarihi, AktifMi)
            OUTPUT INSERTED.*
            VALUES (@RezervasyonId, @Tutar, @OdemeYontemi, @OdemeTarihi, @AktifMi)";

        public const string UpdateOdemeSql = @"
            UPDATE Odeme
            SET RezervasyonId = @RezervasyonId,
                Tutar = @Tutar,
                OdemeYontemi = @OdemeYontemi,
                OdemeTarihi = @OdemeTarihi,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteOdemeSql = @"DELETE FROM Odeme WHERE Id = @Id";
    }
}
