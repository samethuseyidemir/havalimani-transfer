using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Fatura;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FaturaController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public FaturaController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Fatura/search")]
        public async Task<IActionResult> Search(FaturaRequest request)
        {
            var sql = request.BuildFaturaSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Fatura>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Fatura>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Fatura/insert")]
        public async Task<IActionResult> Insert(FaturaBaseRequest request)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Fatura>(FaturaQuery.InsertFaturaSql, request);

            var result = new SuccessDataResult<Fatura>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Fatura/update")]
        public async Task<IActionResult> Update(FaturaBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Fatura>(FaturaQuery.UpdateFaturaSql, parameters);

            var result = new SuccessDataResult<Fatura>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Fatura/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(FaturaQuery.DeleteFaturaSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
