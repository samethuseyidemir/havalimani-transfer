namespace WebApiProje.Query.Entity
{
    public class FaturaQuery
    {
        public const string GetFaturaSql = @"
            SELECT * FROM Fatura WHERE 1=1";

        public const string GetFaturaCountSql = @"
            SELECT COUNT(Id) FROM Fatura WHERE 1=1";

        public const string InsertFaturaSql = @"
            INSERT INTO Fatura (RezervasyonId, Tutar, FaturaTarihi, AktifMi)
            OUTPUT INSERTED.*
            VALUES (@RezervasyonId, @Tutar, @FaturaTarihi, @AktifMi)";

        public const string UpdateFaturaSql = @"
            UPDATE Fatura
            SET RezervasyonId = @RezervasyonId,
                Tutar = @Tutar,
                FaturaTarihi = @FaturaTarihi,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteFaturaSql = @"
            DELETE FROM Fatura WHERE Id = @Id";
    }
}
