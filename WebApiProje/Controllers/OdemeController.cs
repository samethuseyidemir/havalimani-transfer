using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Odeme;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OdemeController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public OdemeController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Odeme/search")]
        public async Task<IActionResult> Search(OdemeRequest request)
        {
            var sql = request.BuildOdemeSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Odeme>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Odeme>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Odeme/insert")]
        public async Task<IActionResult> Insert(OdemeBaseRequest request)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Odeme>(OdemeQuery.InsertOdemeSql, request);

            var result = new SuccessDataResult<Odeme>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Odeme/update")]
        public async Task<IActionResult> Update(OdemeBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Odeme>(OdemeQuery.UpdateOdemeSql, parameters);

            var result = new SuccessDataResult<Odeme>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Odeme/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(OdemeQuery.DeleteOdemeSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
