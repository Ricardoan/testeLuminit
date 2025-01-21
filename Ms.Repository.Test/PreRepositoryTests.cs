using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MsComercio.Domain.Model;
using MsComercio.Repository.RotaViagem;
using Xunit;

namespace MsComercio.Repository.Tests
{
    public class PreRepositoryTests
    {
        private readonly Mock<ILogger<PreRepository>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly PreRepository _repository;

        public PreRepositoryTests()
        {
            _mockLogger = new Mock<ILogger<PreRepository>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c.GetSection("ConnectionStrings:RotaConnection").Value)
                .Returns("YourConnectionStringHere");

            _repository = new PreRepository(_mockConfiguration.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetMelhorRota_ShouldReturnExpectedResult()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();
            var rotas = new List<Rota>
            {
                new Rota { RotaIni = "A", RotaProx = "B", Preco = 10 },
                new Rota { RotaIni = "B", RotaProx = "C", Preco = 15 }
            };
            mockConnection.Setup(c => c.QueryAsync<Rota>(It.IsAny<string>())).ReturnsAsync(rotas);

            // Act
            var result = await _repository.GetMelhorRota("A", "C");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("A - B - C", result.MelhorRota);
            Assert.Equal(25, result.Custo);
        }

        [Fact]
        public async Task InserirRotaNova_ShouldReturnExpectedResult()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();
            var ultimaRotaInserida = new List<Rota>
            {
                new Rota { RotaIni = "A", RotaProx = "B", Preco = 10 }
            };
            mockConnection.Setup(c => c.QueryAsync<Rota>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(ultimaRotaInserida);

            // Act
            var result = await _repository.InserirRotaNova("A", "B", 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Rota inserida com sucesso!", result.Mensagem);
            Assert.Single(result.UltimaRota);
            Assert.Equal("A", result.UltimaRota.First().RotaIni);
            Assert.Equal("B", result.UltimaRota.First().RotaProx);
            Assert.Equal(10, result.UltimaRota.First().Preco);
        }
    }
}
