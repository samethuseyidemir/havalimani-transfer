using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Kullanici;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class KullaniciQueryBuilderExtensions
    {
        public static string BuildKullaniciSqlQuery(this KullaniciRequest request)
        {
            var stringBuilder = new StringBuilder(KullaniciQuery.GetKullaniciSql);
            var countStringBuilder = new StringBuilder(KullaniciQuery.GetKullaniciCountSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Kullanici.Id = '{request.Id}'");

            if (!string.IsNullOrEmpty(request.Ad) && request.Ad.IsNotDefault())
                whereClauses.Add($"Kullanici.Ad LIKE '%{request.Ad}%'");

            if (!string.IsNullOrEmpty(request.Soyad) && request.Soyad.IsNotDefault())
                whereClauses.Add($"Kullanici.Soyad LIKE '%{request.Soyad}%'");

            // ✅ BURASI DEĞİŞTİRİLDİ: LIKE yerine = (eşitlik) kullanıyoruz
            if (!string.IsNullOrEmpty(request.Email) && request.Email.IsNotDefault())
                whereClauses.Add($"Kullanici.Email = '{request.Email}'");

            if (request.RolId.IsNotNull() && request.RolId.IsNotDefault())
                whereClauses.Add($"Kullanici.RolId = '{request.RolId}'");

            if (request.AktifMi.IsNotNull())
                whereClauses.Add($"Kullanici.AktifMi = {(request.AktifMi.Value ? 1 : 0)}");

            if (whereClauses.Any())
            {
                foreach (var clause in whereClauses)
                {
                    stringBuilder.Append(" AND " + clause);
                    countStringBuilder.Append(" AND " + clause);
                }
            }

            stringBuilder.Append(" ORDER BY Kullanici.Ad ASC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY;");
            stringBuilder.Append(countStringBuilder);

            return stringBuilder.ToString();
        }
    }
}
