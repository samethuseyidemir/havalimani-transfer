using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Fatura;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class FaturaQueryBuilderExtensions
    {
        public static string BuildFaturaSqlQuery(this FaturaRequest request)
        {
            var stringBuilder = new StringBuilder(FaturaQuery.GetFaturaSql);
            var countStringBuilder = new StringBuilder(FaturaQuery.GetFaturaCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Fatura.Id = '{request.Id}'");

            if (request.RezervasyonId.IsNotNull() && request.RezervasyonId.IsNotDefault())
                whereClauses.Add($"Fatura.RezervasyonId = '{request.RezervasyonId}'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"Fatura.AktifMi = {(request.AktifMi.Value ? 1 : 0)}");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Fatura.FaturaTarihi DESC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
