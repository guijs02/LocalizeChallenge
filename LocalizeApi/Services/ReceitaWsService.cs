using Localiza.Core.Responses;
using LocalizeApi.Services.Interfaces;

namespace LocalizeApi.Services
{
    public class ReceitaWsService(IHttpClientFactory httpClientFactory) : IReceitaWsService
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ReceitaWs");

        public async Task<Response<ReceitaWsResponse>> GetCompanyDataAsync(string cnpj)
        {
            //_httpClient.BaseAddress = new Uri("https://www.receitaws.com.br/v1/");
            var response = await _httpClient.GetAsync($"cnpj/{cnpj}");

            if (!response.IsSuccessStatusCode)
            {
                return new Response<ReceitaWsResponse>
                            (null, StatusCodes.Status502BadGateway, "Ocorreu um erro ao consultar serviço!");
            }

            var content = await response.Content.ReadFromJsonAsync<ReceitaWsResponse>();
            return new Response<ReceitaWsResponse>(content);
        }
    }
}
