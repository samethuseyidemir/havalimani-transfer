using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using WebApiProje.Extensions.QueryBuilder;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Sirket;
using WebApiProje.Query.Entity;
using WebApiProje.Models.Request.Dto;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirketController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public SirketController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // 1) Bekleyen Şirketleri Arama
        [HttpPost("search")]
        public async Task<IActionResult> Search(SirketRequest request)
        {
            var sql = request.BuildSirketSqlQuery();
            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Sirket>();
            var count = await records.ReadFirstOrDefaultAsync<int>();
            return Ok(new SuccessDataResult<IEnumerable<Sirket>>(datas, count));
        }

        // 2) Yeni Şirket Ekleme (profil tamamlama ve dosya yükleme)
        [HttpPost("insert")]
        public async Task<IActionResult> Insert(
            [FromForm] SirketBaseRequest request,
            IFormFile logoDosya,
            IFormFile faaliyetBelgesiDosya)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadFolder);

            string logoPath = null, belgePath = null;
            if (logoDosya?.Length > 0)
            {
                var fn = $"{Guid.NewGuid()}{Path.GetExtension(logoDosya.FileName)}";
                using var fs = System.IO.File.Create(Path.Combine(uploadFolder, fn));
                await logoDosya.CopyToAsync(fs);
                logoPath = $"/uploads/{fn}";
            }
            if (faaliyetBelgesiDosya?.Length > 0)
            {
                var fn = $"{Guid.NewGuid()}{Path.GetExtension(faaliyetBelgesiDosya.FileName)}";
                using var fs2 = System.IO.File.Create(Path.Combine(uploadFolder, fn));
                await faaliyetBelgesiDosya.CopyToAsync(fs2);
                belgePath = $"/uploads/{fn}";
            }

            request.KayitTarihi = DateTime.Now;
            request.LogoPath = logoPath;
            request.FaaliyetBelgesiPath = belgePath;

            var parameters = new DynamicParameters(request);
            var inserted = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Sirket>(SirketQuery.InsertSirketSql, parameters);

            return Ok(new SuccessDataResult<Sirket>(inserted));
        }

        // 3) Şirket Güncelleme (admin onayı vb.)
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SirketBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            var updated = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Sirket>(SirketQuery.UpdateSirketSql, parameters);
            return Ok(new SuccessDataResult<Sirket>(updated));
        }

        // 4) Tekil Şirket Bilgisi Getir
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Sirket>(
                    "SELECT * FROM [dbo].[Sirket] WHERE Id = @Id",
                    new { Id = id });
            return Ok(new SuccessDataResult<Sirket>(record));
        }

        // 5) Şirket Silme
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] IdRequest request)
        {
            var rows = await new DapperRepository(_dbConnection)
                .ExecuteAsync(SirketQuery.DeleteSirketSql, request);
            return Ok(new SuccessDataResult<int>(rows));
        }

        // 6) Kullanıcıya Ait Şirket Bilgisi Getir
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserId([FromQuery(Name = "userId")] int userId)
        {
            const string sql = "SELECT * FROM [dbo].[Sirket] WHERE KullaniciId = @KullaniciId";
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Sirket>(sql, new { KullaniciId = userId });
            return Ok(new SuccessDataResult<Sirket>(record));
        }
    }
}
