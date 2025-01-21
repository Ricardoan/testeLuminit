using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MsComercio.Repository.RotaViagemBanco.RotaViagem
{
    public class DbConnectionWrapper : IDbConnectionWrapper
    {
        private readonly IDbConnection _connection;

        public DbConnectionWrapper(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null)
        {
            return await _connection.QueryAsync<T>(sql, param);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }

}
