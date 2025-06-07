using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Extensions.QueryBuilder;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Kullanici;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KullaniciController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public KullaniciController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Kullanici/search")]
        public async Task<IActionResult> Search(KullaniciRequest request)
        {
            var sql = request.BuildKullaniciSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Kullanici>();
            var count = await records.ReadFirstOrDefaultAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Kullanici>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Kullanici/insert")]
        public async Task<IActionResult> Insert(KullaniciBaseRequest request)
        {
            request.KayitTarihi = DateTime.Now;

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Kullanici>(KullaniciQuery.InsertKullaniciSql, request);

            var result = new SuccessDataResult<Kullanici>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Kullanici/update")]
        public async Task<IActionResult> Update(KullaniciBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Kullanici>(KullaniciQuery.UpdateKullaniciSql, parameters);

            var result = new SuccessDataResult<Kullanici>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Kullanici/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(KullaniciQuery.DeleteKullaniciSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("/Kullanici/get-by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var sql = @"SELECT * FROM Kullanici WHERE Email = @Email AND AktifMi = 1";

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Kullanici>(sql, new { Email = email });

            return record != null
                ? Ok(new SuccessDataResult<Kullanici>(record))
                : NotFound(new ErrorResult("Kullanıcı bulunamadı."));
        }

        [HttpGet("/Kullanici/get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Kullanici>(
                    "SELECT * FROM Kullanici WHERE Id = @Id", new { Id = id });

            var result = new SuccessDataResult<Kullanici>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
