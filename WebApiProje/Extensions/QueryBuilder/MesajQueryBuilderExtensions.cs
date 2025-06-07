using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Mesaj;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class MesajQueryBuilderExtensions
    {
        public static string BuildMesajSqlQuery(this MesajRequest request)
        {
            var stringBuilder = new StringBuilder(MesajQuery.GetMesajSql);
            var countStringBuilder = new StringBuilder(MesajQuery.GetMesajCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Mesaj.Id = '{request.Id}'");

            if (request.GonderenId.IsNotNull() && request.GonderenId.IsNotDefault())
                whereClauses.Add($"Mesaj.GonderenId = '{request.GonderenId}'");

            if (request.AliciId.IsNotNull() && request.AliciId.IsNotDefault())
                whereClauses.Add($"Mesaj.AliciId = '{request.AliciId}'");

            if (request.OkunduMu.IsNotNull())
                whereClauses.Add($"Mesaj.OkunduMu = '{request.OkunduMu}'");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Mesaj.Tarih DESC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
