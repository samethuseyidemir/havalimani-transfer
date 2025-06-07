using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Arac;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;
using WebApiProje.Models.Request.Dto;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AracController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public AracController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Arac/search")]
        public async Task<IActionResult> Search(AracRequest request)
        {
            var sql = request.BuildAracSqlQuery();
            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Arac>();
            var count = await records.ReadSingleAsync<int>();
            var result = new SuccessDataResult<IEnumerable<Arac>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Arac/insert")]
        public async Task<IActionResult> Insert(AracBaseRequest request)
        {
            // LOG → gelen değerleri yazdırıyoruz
            Console.WriteLine($"[DEBUG] Insert API - Marka: {request.Marka}, Plaka: {request.Plaka}, SirketId: {request.SirketId}");

            request.KayitTarihi = DateTime.Now;

            if (string.IsNullOrWhiteSpace(request.Plaka) || string.IsNullOrWhiteSpace(request.Model))
                return BadRequest("Plaka ve Model alanları zorunludur.");

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Arac>(AracQuery.InsertAracSql, request);

            var result = new SuccessDataResult<Arac>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Arac/update")]
        public async Task<IActionResult> Update(AracBaseRequest request)
        {
            if (request.Id == 0)
                return BadRequest("Güncellenecek aracın Id'si geçersiz.");

            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Arac>(AracQuery.UpdateAracSql, parameters);

            var result = new SuccessDataResult<Arac>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Arac/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(AracQuery.DeleteAracSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("/Arac/get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Arac>(
                    "SELECT * FROM Arac WHERE Id = @Id", new { Id = id });

            var result = new SuccessDataResult<Arac>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
