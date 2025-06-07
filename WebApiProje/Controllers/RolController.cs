using Core.DataAccess.Dapper;
using Core.Toolkit.Results;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiProje.Models.Entity;
using WebApiProje.Models.Request.Rol;
using WebApiProje.Models.Request.Dto;
using WebApiProje.Query.Entity;
using WebApiProje.Extensions.QueryBuilder;

namespace WebApiProje.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public RolController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost("/Rol/search")]
        public async Task<IActionResult> Search(RolRequest request)
        {
            var sql = request.BuildRolSqlQuery();
            var records = await new DapperRepository(_dbConnection).GetMultipleAsync(sql);
            var datas = await records.ReadAsync<Rol>();
            var count = await records.ReadSingleAsync<int>();

            var result = new SuccessDataResult<IEnumerable<Rol>>(datas, count);
            return Ok(result);
        }

        [HttpPost("/Rol/insert")]
        public async Task<IActionResult> Insert(RolAdiRequest request)
        {
            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Rol>(RolQuery.InsertRolSql, request);

            var result = new SuccessDataResult<Rol>(record);
            return Ok(result);
        }

        [HttpPost("/Rol/update")]
        public async Task<IActionResult> Update(RolBaseRequest request)
        {
            var parameters = new DynamicParameters(request);
            parameters.Add("Id", request.Id);

            var record = await new DapperRepository(_dbConnection)
                .QueryFirstOrDefaultAsync<Rol>(RolQuery.UpdateRolSql, parameters);

            var result = new SuccessDataResult<Rol>(record);
            return Ok(result);
        }

        [HttpPost("/Rol/delete")]
        public async Task<IActionResult> Delete(IdRequest request)
        {
            int rowsAffected = await new DapperRepository(_dbConnection)
                .ExecuteAsync(RolQuery.DeleteRolSql, request);

            var result = new SuccessDataResult<int>(rowsAffected);
            return Ok(result);
        }
    }
}
