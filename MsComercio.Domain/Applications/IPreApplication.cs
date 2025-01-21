using MsComercio.Domain.Entities.PreRota;
using MsComercio.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsComercio.Domain.Applications
{
    public interface IPreApplication
    {
        Task<ResponseMaximumValidity> GetMelhorRota(string origem, string destino); 
        Task<ResponseMaximumValidity> InserirRotaNova(string origem, string destino, decimal custo);
               

    }
}