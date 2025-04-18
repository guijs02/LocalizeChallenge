using LocalizeApi.Services.Interfaces;

namespace LocalizeApi.Services
{
    public class ReceitaWsService(HttpClient httpClient) : IReceitaWsService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ReceitaWsService> GetCompanyData(string cnpj)
        {
            var response = await _httpClient.GetAsync($"cnpj/{cnpj}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error fetching company name");
            }

            var content = await response.Content.ReadFromJsonAsync<ReceitaWsService>();
            return content;
        }
    }
}
