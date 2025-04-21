using Localiza.Core.Responses;
using Localize.Core.Requests;
using Localize.Core.Responses;

namespace LocalizeApi.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Response<bool>> CreateAsync(string cnpj, Guid guid);
        Task<PagedResponse<IEnumerable<CompanyResponse>>> GetAllAsyncPaged(GetAllCompanyPagedRequest request, Guid userId);
    }
}
