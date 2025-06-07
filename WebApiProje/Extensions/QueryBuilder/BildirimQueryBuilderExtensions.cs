using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Bildirim;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class BildirimQueryBuilderExtensions
    {
        public static string BuildBildirimSqlQuery(this BildirimRequest request)
        {
            var stringBuilder = new StringBuilder(BildirimQuery.GetBildirimSql);
            var countStringBuilder = new StringBuilder(BildirimQuery.GetBildirimCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Bildirim.Id = '{request.Id}'");

            if (request.KullaniciId.IsNotNull() && request.KullaniciId.IsNotDefault())
                whereClauses.Add($"Bildirim.KullaniciId = '{request.KullaniciId}'");

            if (request.OkunduMu.IsNotNull())
                whereClauses.Add($"Bildirim.OkunduMu = '{request.OkunduMu}'");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Bildirim.GonderimTarihi DESC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
