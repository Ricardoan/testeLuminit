using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MsComercio.Domain.Entities.PreRota;
using MsComercio.Domain.Model;
using MsComercio.Repository.PreRota;

//using WebService;


namespace MsComercio.Repository.RotaViagem
{
    [ExcludeFromCodeCoverage]
    public class PreRepository : IPreRepository
    {
        private readonly ILogger<PreRepository> _logger;
        private readonly string ConnectionString;

        public PreRepository(IConfiguration configuration, ILogger<PreRepository> logger)
        {
            ConnectionString = configuration.GetSection("ConnectionStrings:RotaConnection").Value;
            _logger = logger;
        }

        public async Task<ResponseMaximumValidity> GetMelhorRota(string origem, string destino)
        {
            try
            { 
                using var connection = new SqlConnection(ConnectionString);
                _logger.LogInformation($"Criou a conexão com a rota {origem}");

                var rotas = (await connection.QueryAsync<Rota>(PreRotaQueries.GetMelhorRota)).ToList();

                var grafo = new Dictionary<string, List<Rota>>();
                foreach (var rota in rotas)
                {
                    if (!grafo.ContainsKey(rota.RotaIni))
                        grafo[rota.RotaIni] = new List<Rota>();
                    grafo[rota.RotaIni].Add(rota);
                }

                var resultado = new ResponseMaximumValidity { MelhorRota = string.Empty, Custo = decimal.MaxValue };

                void DFS(string atual, string destino, decimal custoAtual, string rotaAtual)
                {
                    if (atual == destino)
                    {
                        if (custoAtual < resultado.Custo)
                        {
                            resultado.Custo = custoAtual;
                            resultado.MelhorRota = rotaAtual;
                        }
                        return;
                    }

                    if (!grafo.ContainsKey(atual)) return;

                    foreach (var vizinho in grafo[atual])
                    {
                        DFS(vizinho.RotaProx, destino, custoAtual + vizinho.Preco, rotaAtual + " - " + vizinho.RotaProx);
                    }
                }

                DFS(origem, destino, 0, origem);

                _logger.LogInformation($"Retornou a rota mais barata: {resultado.MelhorRota} ao custo de {resultado.Custo}");
                return resultado;
            }
            catch (Exception e)
            {
                _logger.LogError($"Retornou um erro no acesso a rota: rota {origem}. Erro: {e.Message}");
                return null;
            }
        }


        public async Task<ResponseMaximumValidity> InserirRotaNova(string origem, string destino, decimal custo)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                _logger.LogInformation($"Criou a conexão com a rota {origem}");

                // Insere a nova rota e captura a rota inserida
                var parametros = new { Origem = origem, Destino = destino, Custo = custo };
                var ultimaRotaInserida = (await connection.QueryAsync<Rota>(PreRotaQueries.InserirRota, parametros)).ToList();

                ResponseMaximumValidity result = new ResponseMaximumValidity
                {
                    Mensagem = "Rota inserida com sucesso!",
                    UltimaRota = ultimaRotaInserida
                };

                _logger.LogInformation($"Rota inserida: {origem} -> {destino} ao custo de {custo}");
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao inserir a rota: {origem} -> {destino}. Erro: {e.Message}");
                return new ResponseMaximumValidity
                {
                    Mensagem = $"Erro ao inserir a rota: {e.Message}",
                    UltimaRota = null
                };
            }
        }
              

    }
}