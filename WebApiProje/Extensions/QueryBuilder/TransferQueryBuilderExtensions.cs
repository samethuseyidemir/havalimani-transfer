using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Transfer;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class TransferQueryBuilderExtensions
    {
        public static string BuildTransferSqlQuery(this TransferRequest request)
        {
            var stringBuilder = new StringBuilder(TransferQuery.GetTransferSql);
            var countStringBuilder = new StringBuilder(TransferQuery.GetTransferCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Transfer.Id = '{request.Id}'");

            if (!string.IsNullOrEmpty(request.BaslangicNoktasi) && request.BaslangicNoktasi.IsNotDefault())
                whereClauses.Add($"Transfer.BaslangicNoktasi LIKE '%{request.BaslangicNoktasi}%'");

            if (!string.IsNullOrEmpty(request.BitisNoktasi) && request.BitisNoktasi.IsNotDefault())
                whereClauses.Add($"Transfer.BitisNoktasi LIKE '%{request.BitisNoktasi}%'");

            if (request.AracId.IsNotNull() && request.AracId.IsNotDefault())
                whereClauses.Add($"Transfer.AracId = '{request.AracId}'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"Transfer.AktifMi = '{request.AktifMi}'");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Transfer.TarihSaat ASC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
