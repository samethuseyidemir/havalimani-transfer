namespace WebApiProje.Query.Entity
{
    public class RolQuery
    {
        public const string GetRolSql = @"SELECT * FROM Rol WHERE 1=1";
        public const string GetRolCountSql = @"SELECT COUNT(Id) FROM Rol WHERE 1=1";

        public const string InsertRolSql = @"
            INSERT INTO Rol (Adi)
            OUTPUT INSERTED.*
            VALUES (@Adi);";

        public const string UpdateRolSql = @"
            UPDATE Rol SET Adi = @Adi
            OUTPUT INSERTED.*
            WHERE Id = @Id;";

        public const string DeleteRolSql = @"DELETE FROM Rol WHERE Id = @Id";
    }
}
