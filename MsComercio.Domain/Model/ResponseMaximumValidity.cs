

namespace MsComercio.Domain.Model
{
   public class ResponseMaximumValidity
    {
        public string MelhorRota { get; set; }
        public decimal Custo { get; set; }
        public string Mensagem { get; set; }
        public List<Rota> UltimaRota { get; set; }
    }
}
