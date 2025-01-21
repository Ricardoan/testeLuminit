using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using MsComercio.Domain.Applications;
using MsComercio.Business.PreRota;
using MsComercio.Domain.Model;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IPreApplication _preApplication;
     
        public ProdutoController(IPreApplication preApplication)
               
        {
            _preApplication = preApplication;           
        }

     
        [ProducesResponseType(typeof(ResponseMaximumValidity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), (int)HttpStatusCode.NoContent)]
        [HttpGet("GetMelhorRota")]
        public async Task<ActionResult<ResponseMaximumValidity>> GetMelhorRota([FromQuery] string origem, [FromQuery] string destino)
        {
            var result = await _preApplication.GetMelhorRota(origem, destino);
            if (string.IsNullOrWhiteSpace(result.MelhorRota))            
                return NotFound(new { message = "Rota não encontrada" });            
            return Ok(result);

        }

        [ProducesResponseType(typeof(ResponseMaximumValidity), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Notification>), (int)HttpStatusCode.NoContent)]
        [HttpGet("InserirNovaRota")]
        public async Task<ActionResult<ResponseMaximumValidity>> InserirRotaNova([FromQuery] string origem, [FromQuery] string destino, [FromQuery] decimal custo)
        {
            var result = await _preApplication.InserirRotaNova(origem, destino, custo);
            if (result.UltimaRota == null || !result.UltimaRota.Any())             
                return NotFound(new { message = "Rota não encontrada" });             
            return Ok(result.UltimaRota);

        }

     }
}




  
