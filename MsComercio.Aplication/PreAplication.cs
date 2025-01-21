
using MsComercio.Domain.Business;
using MsComercio.Domain.Applications;
using MsComercio.Domain.Model;
namespace MsComercio.Application
{
    public class PreApplication : IPreApplication
    {
        private readonly IPreBusiness _preBusiness;
     
        #region Constructor 
        public PreApplication(
        IPreBusiness preApprovalBusiness)
        {
            _preBusiness = preApprovalBusiness;           
           
        }
        #endregion
        public async Task<ResponseMaximumValidity> GetMelhorRota(string origem, string destino)
        {
            return await _preBusiness.GetMelhorRota(origem, destino);
        }

        public async Task<ResponseMaximumValidity> InserirRotaNova(string origem, string destino, decimal custo)
        {
            return await _preBusiness.InserirRotaNova(origem, destino, custo);
        }
         

    }
}
