using System.Collections.Generic;
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

namespace MsComercio.Tests
{
    public class PreRepositoryTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<PreRepository>> _loggerMock;
        private readonly PreRepository _preRepository;

        public PreRepositoryTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<PreRepository>>();

            
            _configurationMock.Setup(cfg => cfg.GetSection("ConnectionStrings:RotaConnection").Value)
                              .Returns("");

            _preRepository = new PreRepository(_configurationMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetMelhorRota_Should_Return_Optimal_Route()
        {
            // Arrange
            var origem = "A";
            var destino = "B";
            var rotas = new List<Rota>
            {
                new Rota { RotaIni = "A", RotaProx = "B", Preco = 10 },
                new Rota { RotaIni = "A", RotaProx = "C", Preco = 15 },
                new Rota { RotaIni = "C", RotaProx = "B", Preco = 5 }
            };

            // Mock da conexão 
            var connectionMock = new Mock<SqlConnection>("");
           // connectionMock.Setup(c => c.QueryAsync<Rota>(It.IsAny<string>())).ReturnsAsync(rotas);

            // Act
            var result = await _preRepository.GetMelhorRota(origem, destino);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("A - B", result.MelhorRota);
            Assert.Equal(10, result.Custo);
        }
    }
}
