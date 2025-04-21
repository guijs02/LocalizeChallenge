using Localiza.Core.Responses;
using Localize.Core.Requests;
using Localize.Core.Responses;
using LocalizeApi.Repository.Interfaces;
using LocalizeApi.Services;
using LocalizeApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Localize.Test
{
    public class CompanyServiceTest
    {
        private readonly CompanyService _service;
        private readonly Mock<ICompanyRepository> _repository;
        private readonly Mock<IReceitaWsService> _receitaService;
        public CompanyServiceTest()
        {
            _repository = new Mock<ICompanyRepository>();
            _receitaService = new Mock<IReceitaWsService>();
            _service = new(_repository.Object, _receitaService.Object);
        }

        [Fact]
        public async Task GetAllAsyncPaged_ShouldReturnPagedResponseWithSuccess()
        {
            // Arrange
            var request = new GetAllCompanyPagedRequest
            {
                PageNumber = 1,
                PageSize = 10
            };
            var userId = Guid.NewGuid();
            var companies = new List<CompanyResponse>
            {
                new CompanyResponse { LegalName = "Company A", Cnpj = "12345678000100", Status = "Active" },
            };

            var pagedResponse = new PagedResponse<IEnumerable<CompanyResponse>>(companies, totalCount: 2, currentPage: 1, pageSize: 10);

            _repository.Setup(r => r.GetAllAsyncPaged(It.IsAny<GetAllCompanyPagedRequest>(), It.IsAny<Guid>()))
                       .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetAllAsyncPaged(request, userId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result?.Data?.Count());
            Assert.Equal("Company A", result.Data.First().LegalName);
            Assert.Equal("12345678000100", result.Data.First().Cnpj);
            Assert.Equal("Active", result.Data.First().Status);
        }

        [Fact]
        public async Task GetAllAsyncPaged_ShouldReturnEmptyResponseWhenNoData()
        {
            // Arrange
            var request = new GetAllCompanyPagedRequest
            {
                PageNumber = 1,
                PageSize = 10
            };
            var userId = Guid.NewGuid();
            var emptyResponse = new PagedResponse<IEnumerable<CompanyResponse>>(Enumerable.Empty<CompanyResponse>(), totalCount: 0, currentPage: 1, pageSize: 10);

            _repository.Setup(r => r.GetAllAsyncPaged(It.IsAny<GetAllCompanyPagedRequest>(), It.IsAny<Guid>()))
                       .ReturnsAsync(emptyResponse);

            // Act
            var result = await _service.GetAllAsyncPaged(request, userId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Data);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess_WhenCompanyIsCreated()
        {
            // Arrange
            var cnpj = "12345678000100";
            var userId = Guid.NewGuid();

            var receitaWsResponse = new ReceitaWsResponse(
                "Company A",
                "Company A Fantasia",
                cnpj,
                "Active",
                "2023-01-01",
                "Matriz",
                "Sociedade Limitada",
                new List<AtividadePrincipal>(),
                "Rua Exemplo",
                "123",
                "Sala 1",
                "Centro",
                "São Paulo",
                "SP",
                "01000-000"
            );

            _receitaService.Setup(r => r.GetCompanyDataAsync(It.IsAny<string>()))
                           .ReturnsAsync(new Response<ReceitaWsResponse>(receitaWsResponse));

            _repository.Setup(r => r.CreateAsync(It.IsAny<ReceitaWsResponse>(), It.IsAny<Guid>()))
                       .ReturnsAsync(new Response<bool>(true));

            // Act
            var result = await _service.CreateAsync(cnpj, userId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFailure_WhenReceitaWsFails()
        {
            // Arrange
            var cnpj = "12345678000100";
            var userId = Guid.NewGuid();

            _receitaService.Setup(r => r.GetCompanyDataAsync(It.IsAny<string>()))
                           .ReturnsAsync(new Response<ReceitaWsResponse>(null, code: StatusCodes.Status502BadGateway, message: "Failure request"));

            // Act
            var result = await _service.CreateAsync(cnpj, userId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.False(result.Data);
            Assert.Equal("Failure request", result.Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFailure_WhenRepositoryFails()
        {
            // Arrange
            var cnpj = "12345678000100";
            var userId = Guid.NewGuid();
            var receitaWsResponse = new ReceitaWsResponse
            (
                "Company A",
                "Company A Fantasia",
                cnpj,
                "Active",
                "2023-01-01",
                "Matriz",
                "Sociedade Limitada",
                new List<AtividadePrincipal>(),
                "Rua Exemplo",
                "123",
                "Sala 1",
                "Centro",
                "São Paulo",
                "SP",
                "01000-000"
            );

            _receitaService.Setup(r => r.GetCompanyDataAsync(It.IsAny<string>()))
                           .ReturnsAsync(new Response<ReceitaWsResponse>(receitaWsResponse));

            _repository.Setup(r => r.CreateAsync(It.IsAny<ReceitaWsResponse>(), It.IsAny<Guid>()))
                       .ReturnsAsync(new Response<bool>(false, code: StatusCodes.Status500InternalServerError, message: "Database error"));

            // Act
            var result = await _service.CreateAsync(cnpj, userId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.False(result.Data);
            Assert.Equal("Database error", result.Message);

        }
    }
}
