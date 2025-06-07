namespace WebApiProje.Query.Entity
{
    public class MesajQuery
    {
        public const string GetMesajSql = @"SELECT * FROM Mesaj WHERE 1=1";

        public const string GetMesajCountSql = @"SELECT COUNT(Id) FROM Mesaj WHERE 1=1";

        public const string InsertMesajSql = @"
            INSERT INTO Mesaj (GonderenId, AliciId, Icerik, Tarih, OkunduMu)
            OUTPUT INSERTED.*
            VALUES (@GonderenId, @AliciId, @Icerik, @Tarih, @OkunduMu)";

        public const string UpdateMesajSql = @"
            UPDATE Mesaj
            SET GonderenId = @GonderenId,
                AliciId = @AliciId,
                Icerik = @Icerik,
                Tarih = @Tarih,
                OkunduMu = @OkunduMu
            OUTPUT INSERTED.*
            WHERE Id = @Id";

        public const string DeleteMesajSql = @"DELETE FROM Mesaj WHERE Id = @Id";
    }
}
