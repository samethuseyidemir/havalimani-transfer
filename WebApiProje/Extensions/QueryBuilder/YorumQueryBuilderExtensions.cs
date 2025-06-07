using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Yorum;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class YorumQueryBuilderExtensions
    {
        public static string BuildYorumSqlQuery(this YorumRequest request)
        {
            var stringBuilder = new StringBuilder(YorumQuery.GetYorumSql);
            var countStringBuilder = new StringBuilder(YorumQuery.GetYorumCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Yorum.Id = '{request.Id}'");

            if (request.KullaniciId.IsNotNull() && request.KullaniciId.IsNotDefault())
                whereClauses.Add($"Yorum.KullaniciId = '{request.KullaniciId}'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"Yorum.AktifMi = '{request.AktifMi}'");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Yorum.YorumTarihi DESC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
