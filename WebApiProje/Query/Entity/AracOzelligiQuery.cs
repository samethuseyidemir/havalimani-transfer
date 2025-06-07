namespace WebApiProje.Query.Entity
{
    public class AracOzelligiQuery
    {
        public const string GetAracOzelligiSql = @"SELECT * FROM AracOzelligi WHERE 1=1";

        public const string GetAracOzelligiCountSql = @"SELECT COUNT(Id) FROM AracOzelligi WHERE 1=1";

        public const string InsertAracOzelligiSql = @"
            INSERT INTO AracOzelligi (OzellikAdi, AktifMi)
            OUTPUT INSERTED.*
            VALUES (@OzellikAdi, @AktifMi)";

        public const string UpdateAracOzelligiSql = @"
            UPDATE AracOzelligi
            SET OzellikAdi = @OzellikAdi,
                AktifMi = @AktifMi
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteAracOzelligiSql = @"DELETE FROM AracOzelligi WHERE Id = @Id";
    }
}
