using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Rol;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class RolQueryBuilderExtensions
    {
        public static string BuildRolSqlQuery(this RolRequest request)
        {
            var sql = new StringBuilder(RolQuery.GetRolSql);
            var countSql = new StringBuilder(RolQuery.GetRolCountSql);
            var where = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                where.Add($"Rol.Id = '{request.Id}'");

            if (!string.IsNullOrEmpty(request.Adi))
                where.Add($"Rol.Adi LIKE '%{request.Adi}%'");

            if (where.Any())
            {
                foreach (var w in where)
                {
                    sql.Append(" AND " + w);
                    countSql.Append(" AND " + w);
                }
            }

            sql.Append(" ORDER BY Rol.Id DESC");
            sql.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount > 0 ? request.MaxCount : 25)} ROWS ONLY;");
            sql.Append(countSql);

            return sql.ToString();
        }
    }
}
