using System.Text;
using WebApiProje.Query.Entity;
using Core.Extensions;
using WebApiProje.Models.Request.Rezervasyon;

namespace WebApiProje.Extensions.QueryBuilder
{
    public static class RezervasyonQueryBuilderExtensions
    {
        public static string BuildRezervasyonSqlQuery(this RezervasyonRequest request)
        {
            var stringBuilder = new StringBuilder(RezervasyonQuery.GetRezervasyonSql);
            var whereClauses = new List<string>();

            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Rezervasyon.Id = {request.Id}");

            if (request.KullaniciId.IsNotNull() && request.KullaniciId.IsNotDefault())
                whereClauses.Add($"Rezervasyon.KullaniciId = {request.KullaniciId}");

            if (request.TransferId.IsNotNull() && request.TransferId.IsNotDefault())
                whereClauses.Add($"Rezervasyon.TransferId = {request.TransferId}");

            if (request.AktifMi.HasValue)
                whereClauses.Add($"Rezervasyon.AktifMi = {(request.AktifMi.Value ? 1 : 0)}");

            if (!string.IsNullOrEmpty(request.KullaniciAdSoyad))
                whereClauses.Add($"(Kullanici.Ad + ' ' + Kullanici.Soyad) LIKE '%{request.KullaniciAdSoyad}%'");

            if (!string.IsNullOrEmpty(request.TransferBilgisi))
                whereClauses.Add($"(Transfer.BaslangicNoktasi + ' - ' + Transfer.BitisNoktasi) LIKE '%{request.TransferBilgisi}%'");

            if (request.MinTarih.HasValue)
                whereClauses.Add($"Rezervasyon.ReservedAt >= '{request.MinTarih.Value:yyyy-MM-dd}'");

            if (request.MaxTarih.HasValue)
                whereClauses.Add($"Rezervasyon.ReservedAt <= '{request.MaxTarih.Value:yyyy-MM-dd}'");

            // WHERE koşullarını birleştir
            if (whereClauses.Any())
                stringBuilder.Append(" AND " + string.Join(" AND ", whereClauses));

            stringBuilder.Append(" ORDER BY Rezervasyon.ReservedAt DESC");
            stringBuilder.Append($" OFFSET {request.StartIndex} ROWS FETCH NEXT {(request.MaxCount.IsNotDefault() ? request.MaxCount : 25)} ROWS ONLY");

            return stringBuilder.ToString();
        }

        public static string BuildRezervasyonCountSqlQuery(this RezervasyonRequest request)
        {
            var stringBuilder = new StringBuilder(RezervasyonQuery.GetRezervasyonCountSql);
            var whereClauses = new List<string>();

            // Aynı filtreler
            if (request.Id.IsNotNull() && request.Id.IsNotDefault())
                whereClauses.Add($"Rezervasyon.Id = {request.Id}");

            if (request.KullaniciId.IsNotNull() && request.KullaniciId.IsNotDefault())
                whereClauses.Add($"Rezervasyon.KullaniciId = {request.KullaniciId}");

            if (request.TransferId.IsNotNull() && request.TransferId.IsNotDefault())
                whereClauses.Add($"Rezervasyon.TransferId = {request.TransferId}");

            if (request.AktifMi.HasValue)
                whereClauses.Add($"Rezervasyon.AktifMi = {(request.AktifMi.Value ? 1 : 0)}");

            if (!string.IsNullOrEmpty(request.KullaniciAdSoyad))
                whereClauses.Add($"(Kullanici.Ad + ' ' + Kullanici.Soyad) LIKE '%{request.KullaniciAdSoyad}%'");

            if (!string.IsNullOrEmpty(request.TransferBilgisi))
                whereClauses.Add($"(Transfer.BaslangicNoktasi + ' - ' + Transfer.BitisNoktasi) LIKE '%{request.TransferBilgisi}%'");

            if (request.MinTarih.HasValue)
                whereClauses.Add($"Rezervasyon.ReservedAt >= '{request.MinTarih.Value:yyyy-MM-dd}'");

            if (request.MaxTarih.HasValue)
                whereClauses.Add($"Rezervasyon.ReservedAt <= '{request.MaxTarih.Value:yyyy-MM-dd}'");

            if (whereClauses.Any())
                stringBuilder.Append(" AND " + string.Join(" AND ", whereClauses));

            return stringBuilder.ToString();
        }
    }
}
