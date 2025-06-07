using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.AracOzelligi;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class AracOzelligiQueryBuilderExtensions
    {
        public static string BuildAracOzelligiSqlQuery(this AracOzelligiRequest request)
        {
            var stringBuilder = new StringBuilder(AracOzelligiQuery.GetAracOzelligiSql);
            var countStringBuilder = new StringBuilder(AracOzelligiQuery.GetAracOzelligiCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"AracOzelligi.Id = '{request.Id}'");

            if (!string.IsNullOrEmpty(request.OzellikAdi) && request.OzellikAdi.IsNotDefault())
                whereClauses.Add($"AracOzelligi.OzellikAdi LIKE '%{request.OzellikAdi}%'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"AracOzelligi.AktifMi = '{request.AktifMi}'");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY AracOzelligi.OzellikAdi ASC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
