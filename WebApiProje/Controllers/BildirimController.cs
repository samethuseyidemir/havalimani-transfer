using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Bildirim;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BildirimController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public BildirimController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Bildirim/search")]
        public async Task<IActionResult> Search(BildirimRequest request)
        {
            var sql = request.BuildBildirimSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Bildirim>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Bildirim>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Bildirim/insert")]
        public async Task<IActionResult> Insert(BildirimBaseRequest request)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Bildirim>(BildirimQuery.InsertBildirimSql, request);

            var result = new SuccessDataResult<Bildirim>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Bildirim/update")]
        public async Task<IActionResult> Update(BildirimBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Bildirim>(BildirimQuery.UpdateBildirimSql, parameters);

            var result = new SuccessDataResult<Bildirim>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Bildirim/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(BildirimQuery.DeleteBildirimSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
