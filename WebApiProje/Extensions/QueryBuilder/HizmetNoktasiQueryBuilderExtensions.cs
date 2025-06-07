using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.HizmetNoktasi;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class HizmetNoktasiQueryBuilderExtensions
    {
        public static string BuildHizmetNoktasiSqlQuery(this HizmetNoktasiRequest request)
        {
            var stringBuilder = new StringBuilder(HizmetNoktasiQuery.GetHizmetNoktasiSql);
            var countStringBuilder = new StringBuilder(HizmetNoktasiQuery.GetHizmetNoktasiCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"HizmetNoktasi.Id = '{request.Id}'");

            if (request.SirketId.IsNotNull() && request.SirketId.IsNotDefault())
                whereClauses.Add($"HizmetNoktasi.SirketId = '{request.SirketId}'");

            if (!string.IsNullOrEmpty(request.BaslangicNoktasi) && request.BaslangicNoktasi.IsNotDefault())
                whereClauses.Add($"HizmetNoktasi.BaslangicNoktasi LIKE '%{request.BaslangicNoktasi}%'");

            if (!string.IsNullOrEmpty(request.BitisNoktasi) && request.BitisNoktasi.IsNotDefault())
                whereClauses.Add($"HizmetNoktasi.BitisNoktasi LIKE '%{request.BitisNoktasi}%'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"HizmetNoktasi.AktifMi = {(request.AktifMi.Value ? 1 : 0)}");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY HizmetNoktasi.KayitTarihi DESC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
