using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Models.Request.HizmetNoktasi;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize] // Giriş yapmamış kullanıcı bu controller'a erişemez
    public class HizmetNoktasiController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public HizmetNoktasiController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/HizmetNoktasi/search")]
        public async Task<IActionResult> Search(HizmetNoktasiRequest request)
        {
            var sql = request.BuildHizmetNoktasiSqlQuery();

            var records = await new DapperRepository(_dbConnection)
                .GetMultipleAsync(sql);

            var list = await records.ReadAsync<HizmetNoktasi>();
            var toplam = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<HizmetNoktasi>>(list, toplam);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/HizmetNoktasi/insert")]
        public async Task<IActionResult> Insert(HizmetNoktasiBaseRequest request)
        {
            request.KayitTarihi = DateTime.Now;

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<HizmetNoktasi>(HizmetNoktasiQuery.InsertHizmetNoktasiSql, request);

            var result = new SuccessDataResult<HizmetNoktasi>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/HizmetNoktasi/update")]
        public async Task<IActionResult> Update(HizmetNoktasiBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<HizmetNoktasi>(HizmetNoktasiQuery.UpdateHizmetNoktasiSql, parameters);

            var result = new SuccessDataResult<HizmetNoktasi>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/HizmetNoktasi/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(HizmetNoktasiQuery.DeleteHizmetNoktasiSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("/HizmetNoktasi/get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<HizmetNoktasi>(
                    "SELECT * FROM HizmetNoktasi WHERE Id = @Id", new { Id = id });

            var result = new SuccessDataResult<HizmetNoktasi>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
