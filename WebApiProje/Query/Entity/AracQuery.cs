namespace WebApiProje.Query.Entity
{
    public class AracQuery
    {
        public const string GetAracSql = @"
            SELECT * FROM Arac WHERE 1=1";

        public const string GetAracCountSql = @"
            SELECT COUNT(Id) FROM Arac WHERE 1=1";

        public const string InsertAracSql = @"
            INSERT INTO Arac (Marka, Plaka, Model, KoltukSayisi, BagajKapasitesi, SirketId, AktifMi, KayitTarihi)
            OUTPUT INSERTED.*
            VALUES (@Marka, @Plaka, @Model, @KoltukSayisi, @BagajKapasitesi, @SirketId, @AktifMi, @KayitTarihi)";

        public const string UpdateAracSql = @"
            UPDATE Arac
            SET Marka = @Marka,
                Plaka = @Plaka,
                Model = @Model,
                KoltukSayisi = @KoltukSayisi,
                BagajKapasitesi = @BagajKapasitesi,
                SirketId = @SirketId,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteAracSql = @"
            DELETE FROM Arac WHERE Id = @Id";
    }
}
