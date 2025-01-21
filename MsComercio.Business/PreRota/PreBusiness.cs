

using Microsoft.Extensions.DependencyInjection;
using MsComercio.Domain.Entities.PreRota;
using MsComercio.Domain.Applications;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MsComercio.Domain.Business;
using MsComercio.Domain.Model;

namespace MsComercio.Business.PreBusiness
{
    public partial class PreBusiness : IPreBusiness
    {
 
        public readonly IPreRepository _preRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        #region Constructor 
        public PreBusiness(

     
        IPreRepository preRepository,   
        IConfiguration configuration,
        IMapper mapper)
        {
                   
            _preRepository = preRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        #endregion

      
        public async Task<ResponseMaximumValidity> GetMelhorRota(string origem, string destino)
        {

            var listaRotas = await _preRepository.GetMelhorRota(origem, destino);

            return listaRotas;
        }
        public async Task<ResponseMaximumValidity> InserirRotaNova(string origem, string destino, decimal custo)
        {

            var listaRotas = await _preRepository.InserirRotaNova(origem, destino, custo);

            return listaRotas;
        }
             
    }
}
