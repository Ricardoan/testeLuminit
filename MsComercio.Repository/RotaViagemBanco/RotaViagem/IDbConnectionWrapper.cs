using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsComercio.Repository.RotaViagemBanco.RotaViagem
{
    public interface IDbConnectionWrapper : IDisposable
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null); // Outras operações que você precisa... }
    }
}
