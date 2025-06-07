using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.AracOzelligi;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AracOzelligiController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public AracOzelligiController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/AracOzelligi/search")]
        public async Task<IActionResult> Search(AracOzelligiRequest request)
        {
            var sql = request.BuildAracOzelligiSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<AracOzelligi>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<AracOzelligi>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/AracOzelligi/insert")]
        public async Task<IActionResult> Insert(AracOzelligiBaseRequest request)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<AracOzelligi>(AracOzelligiQuery.InsertAracOzelligiSql, request);

            var result = new SuccessDataResult<AracOzelligi>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/AracOzelligi/update")]
        public async Task<IActionResult> Update(AracOzelligiBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<AracOzelligi>(AracOzelligiQuery.UpdateAracOzelligiSql, parameters);

            var result = new SuccessDataResult<AracOzelligi>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/AracOzelligi/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(AracOzelligiQuery.DeleteAracOzelligiSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
