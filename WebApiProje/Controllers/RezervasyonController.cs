using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Rezervasyon;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;
using WebApiProje.Query.Dto;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezervasyonController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public RezervasyonController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Rezervasyon/search")]
        public async Task<IActionResult> Search(RezervasyonRequest request)
        {
            var sql = request.BuildRezervasyonSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Rezervasyon>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Rezervasyon>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Rezervasyon/insert")]
        public async Task<IActionResult> Insert(RezervasyonBaseRequest request)
        {
            request.KayitTarihi = DateTime.Now;

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Rezervasyon>(RezervasyonQuery.InsertRezervasyonSql, request);

            var result = new SuccessDataResult<Rezervasyon>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Rezervasyon/update")]
        public async Task<IActionResult> Update(RezervasyonBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Rezervasyon>(RezervasyonQuery.UpdateRezervasyonSql, parameters);

            var result = new SuccessDataResult<Rezervasyon>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Rezervasyon/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(RezervasyonQuery.DeleteRezervasyonSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpGet("/Rezervasyon/detay-listele")]
        public async Task<IActionResult> RezervasyonDetayListele()
        {
            var sql = RezervasyonDetayDtoSearchQuery.Sql;

            var records = await new DapperRepository(_dbConnection)
                .QueryAsync<RezervasyonDetayRequest>(sql);

            var result = new SuccessDataResult<IEnumerable<RezervasyonDetayRequest>>(records);
            return Ok(result);
        }
        [HttpGet("/Rezervasyon/get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Rezervasyon>(
                    "SELECT * FROM Rezervasyon WHERE Id = @Id", new { Id = id });

            var result = new SuccessDataResult<Rezervasyon>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

    }
}
