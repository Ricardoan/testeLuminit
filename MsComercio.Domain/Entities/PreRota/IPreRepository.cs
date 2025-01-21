using MsComercio.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsComercio.Domain.Entities.PreRota
{
    public interface IPreRepository
    {
        
        Task<ResponseMaximumValidity> GetMelhorRota(string origem, string destino);
        Task<ResponseMaximumValidity> InserirRotaNova(string origem, string destino,decimal custo);
        
    }
}

