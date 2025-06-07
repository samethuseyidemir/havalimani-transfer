namespace WebApiProje.Query.Entity
{
    public class TransferQuery
    {
        public const string GetTransferSql = @"
            SELECT * FROM Transfer WHERE 1=1";

        public const string GetTransferCountSql = @"
            SELECT COUNT(Id) FROM Transfer WHERE 1=1";

        public const string InsertTransferSql = @"
            INSERT INTO Transfer (BaslangicNoktasi, BitisNoktasi, TarihSaat, Ucret, AracId, AktifMi, KayitTarihi)
            OUTPUT INSERTED.*
            VALUES (@BaslangicNoktasi, @BitisNoktasi, @TarihSaat, @Ucret, @AracId, @AktifMi, @KayitTarihi)";

        public const string UpdateTransferSql = @"
            UPDATE Transfer
            SET BaslangicNoktasi = @BaslangicNoktasi,
                BitisNoktasi = @BitisNoktasi,
                TarihSaat = @TarihSaat,
                Ucret = @Ucret,
                AracId = @AracId,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteTransferSql = @"
            DELETE FROM Transfer WHERE Id = @Id";
    }
}
