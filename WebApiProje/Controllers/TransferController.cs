using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Transfer;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public TransferController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Transfer/search")]
        public async Task<IActionResult> Search(TransferRequest request)
        {
            var sql = request.BuildTransferSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Transfer>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Transfer>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Transfer/insert")]
        public async Task<IActionResult> Insert(TransferBaseRequest request)
        {
            request.KayitTarihi = DateTime.Now;

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Transfer>(TransferQuery.InsertTransferSql, request);

            var result = new SuccessDataResult<Transfer>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Transfer/update")]
        public async Task<IActionResult> Update(TransferBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Transfer>(TransferQuery.UpdateTransferSql, parameters);

            var result = new SuccessDataResult<Transfer>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Transfer/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(TransferQuery.DeleteTransferSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
        [HttpGet("/Transfer/get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Transfer>(
                    "SELECT * FROM Transfer WHERE Id = @Id", new { Id = id });

            var result = new SuccessDataResult<Transfer>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

    }
}
