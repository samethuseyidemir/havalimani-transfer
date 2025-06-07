using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiProje.Query.Entity;
using WebApiProje.Models.Request.Sirket;
using Core.Extensions;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class SirketQueryBuilderExtensions
    {
        public static string BuildSirketSqlQuery(this SirketRequest request)
        {
            var sb = new StringBuilder(SirketQuery.GetSirketSql);
            var countSb = new StringBuilder(SirketQuery.GetSirketCountSql);
            var where = new List<string>();

            if (request.Id.HasValue && request.Id.Value > 0)
                where.Add($"Sirket.Id = {request.Id.Value}");

            if (!string.IsNullOrEmpty(request.Ad))
                where.Add($"Sirket.Ad LIKE '%{request.Ad}%'");

            if (!string.IsNullOrEmpty(request.Email))
                where.Add($"Sirket.Email LIKE '%{request.Email}%'");

            if (!string.IsNullOrEmpty(request.Telefon))
                where.Add($"Sirket.Telefon LIKE '%{request.Telefon}%'");

            if (!string.IsNullOrEmpty(request.Adres))
                where.Add($"Sirket.Adres LIKE '%{request.Adres}%'");

            if (request.AktifMi.HasValue)
                where.Add($"Sirket.AktifMi = {(request.AktifMi.Value ? 1 : 0)}");

            if (request.KullaniciId.HasValue)
                where.Add($"Sirket.KullaniciId = {request.KullaniciId.Value}");

            if (where.Any())
            {
                foreach (var clause in where)
                {
                    sb.Append(" AND ").Append(clause);
                    countSb.Append(" AND ").Append(clause);
                }
            }

            sb.Append(" ORDER BY Sirket.Ad ASC")
              .Append($" OFFSET {request.StartIndex} ROWS")
              .Append($" FETCH NEXT {(request.MaxCount > 0 ? request.MaxCount : 25)} ROWS ONLY;");

            countSb.Append(";");

            return sb.ToString() + countSb;
        }
    }
}
