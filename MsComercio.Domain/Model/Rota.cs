using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsComercio.Domain.Model
{
    public class Rota
    {
        public int IdRota { get; set; }
        public string RotaIni { get; set; }
        public string RotaProx { get; set; }
        public decimal Preco { get; set; }
    }
}
