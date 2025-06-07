using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Odeme;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class OdemeQueryBuilderExtensions
    {
        public static string BuildOdemeSqlQuery(this OdemeRequest request)
        {
            var stringBuilder = new StringBuilder(OdemeQuery.GetOdemeSql);
            var countStringBuilder = new StringBuilder(OdemeQuery.GetOdemeCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Odeme.Id = '{request.Id}'");

            if (request.RezervasyonId.IsNotNull() && request.RezervasyonId.IsNotDefault())
                whereClauses.Add($"Odeme.RezervasyonId = '{request.RezervasyonId}'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"Odeme.AktifMi = '{request.AktifMi}'");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Odeme.OdemeTarihi DESC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
