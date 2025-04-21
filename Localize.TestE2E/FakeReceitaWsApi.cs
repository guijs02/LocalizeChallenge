using Localiza.Core.Responses;
using LocalizeApi.Services.Interfaces;

namespace Localize.TestE2E
{
    internal class FakeReceitaWsApi : IReceitaWsService
    {
        public Task<Response<ReceitaWsResponse>> GetCompanyDataAsync(string cnpj)
        {
            return Task.FromResult(new Response<ReceitaWsResponse>(
                new ReceitaWsResponse
                (
                    "Empresa Exemplo Ltda",
                    "Exemplo",
                    "12.345.678/0001-99",
                    "ATIVA",
                    "10/01/2010",
                    "MATRIZ",
                    "206-2 - Sociedade Empresária Limitada",
                    new List<AtividadePrincipal> { new AtividadePrincipal { Text = "Desenvolvimento de software", Code = "62.01-5-01" } },
                    "Rua das Acácias",
                    "123",
                    "Sala 5",
                    "Centro",
                    "São Paulo",
                    "SP",
                    "01001-000"
                )
            ));
        }

    }
}
