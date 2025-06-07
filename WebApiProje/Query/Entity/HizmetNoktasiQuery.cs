namespace WebApiProje.Query.Entity
{
    public class HizmetNoktasiQuery
    {
        public const string GetHizmetNoktasiSql = @"
            SELECT * FROM HizmetNoktasi
            WHERE 1=1";

        public const string GetHizmetNoktasiCountSql = @"
            SELECT COUNT(Id) FROM HizmetNoktasi
            WHERE 1=1";

        public const string InsertHizmetNoktasiSql = @"
            INSERT INTO HizmetNoktasi
            (SirketId, BaslangicNoktasi, BitisNoktasi, MesafeKm, TahminiSureDakika, AktifMi, KayitTarihi)
            OUTPUT INSERTED.*
            VALUES (@SirketId, @BaslangicNoktasi, @BitisNoktasi, @MesafeKm, @TahminiSureDakika, @AktifMi, @KayitTarihi)";

        public const string UpdateHizmetNoktasiSql = @"
            UPDATE HizmetNoktasi SET
                SirketId = @SirketId,
                BaslangicNoktasi = @BaslangicNoktasi,
                BitisNoktasi = @BitisNoktasi,
                MesafeKm = @MesafeKm,
                TahminiSureDakika = @TahminiSureDakika,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteHizmetNoktasiSql = @"
            DELETE FROM HizmetNoktasi
            WHERE Id = @Id";
    }
}
