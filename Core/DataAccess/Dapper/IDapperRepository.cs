﻿using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.DataAccess.Dapper
{
    public interface IDapperRepository
    {
        Task<TDto> QueryFirstOrDefaultAsync<TDto>(string sql, object param = null);
        Task<IEnumerable<TDto>> QueryAsync<TDto>(string sql, object param = null);
        Task<SqlMapper.GridReader> GetMultipleAsync(string sql, object param = null);
        Task<int> ExecuteAsync(string sql, object param = null);
    }
}
