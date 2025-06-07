using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Mesaj;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MesajController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public MesajController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Mesaj/search")]
        public async Task<IActionResult> Search(MesajRequest request)
        {
            var sql = request.BuildMesajSqlQuery();

            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Mesaj>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Mesaj>>(datas, count);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Mesaj/insert")]
        public async Task<IActionResult> Insert(MesajBaseRequest request)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Mesaj>(MesajQuery.InsertMesajSql, request);

            var result = new SuccessDataResult<Mesaj>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Mesaj/update")]
        public async Task<IActionResult> Update(MesajBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Mesaj>(MesajQuery.UpdateMesajSql, parameters);

            var result = new SuccessDataResult<Mesaj>(record);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("/Mesaj/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(MesajQuery.DeleteMesajSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
