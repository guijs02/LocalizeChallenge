using Localiza.Core.Responses;

namespace LocalizeApi.Services.Interfaces
{
    public interface IReceitaWsService
    {
        Task<Response<ReceitaWsResponse>> GetCompanyDataAsync(string cnpj);
    }
}
