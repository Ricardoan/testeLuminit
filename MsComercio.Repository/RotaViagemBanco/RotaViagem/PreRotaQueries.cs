using System;
using System.Diagnostics.CodeAnalysis;

namespace MsComercio.Repository.PreRota
{
    [ExcludeFromCodeCoverage]
    public class PreRotaQueries
    {
        public static string GetMelhorRota = @"
        select idRota, rotaIni, rotaProx,preco from dbo.rota";

        public static string InserirRota = @" INSERT INTO dbo.rota (rotaIni, rotaProx, preco)
        OUTPUT INSERTED.* VALUES (@Origem, @Destino, @Custo)";
    }
   
}
