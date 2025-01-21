
using MsComercio.Domain.Model;

namespace MsComercio.Domain.Business
{
    public interface IPreBusiness
    {

        Task<ResponseMaximumValidity> GetMelhorRota(string origem, string destino);
        Task<ResponseMaximumValidity> InserirRotaNova(string origem, string destino, decimal custo);
       
    }
}