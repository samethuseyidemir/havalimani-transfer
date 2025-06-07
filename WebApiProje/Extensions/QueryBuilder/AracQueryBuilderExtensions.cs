using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Arac;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class AracQueryBuilderExtensions
    {
        public static string BuildAracSqlQuery(this AracRequest request)
        {
            var stringBuilder = new StringBuilder(AracQuery.GetAracSql);
            var countStringBuilder = new StringBuilder(AracQuery.GetAracCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Arac.Id = '{request.Id}'");

            if (!string.IsNullOrEmpty(request.Plaka) && request.Plaka.IsNotDefault())
                whereClauses.Add($"Arac.Plaka LIKE '%{request.Plaka}%'");

            if (!string.IsNullOrEmpty(request.Model) && request.Model.IsNotDefault())
                whereClauses.Add($"Arac.Model LIKE '%{request.Model}%'");

            if (request.SirketId.IsNotNull() && request.SirketId.IsNotDefault())
                whereClauses.Add($"Arac.SirketId = '{request.SirketId}'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"Arac.AktifMi = {(request.AktifMi.Value ? 1 : 0)}");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Arac.Model ASC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
