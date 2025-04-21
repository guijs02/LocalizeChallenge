using Localiza.Core.Responses;
using Localize.Core.Requests;
using Localize.Core.Responses;

namespace LocalizeApi.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Response<bool>> CreateAsync(ReceitaWsResponse receitaWs, Guid userId);
        Task<PagedResponse<IEnumerable<CompanyResponse>>> GetAllAsyncPaged(GetAllCompanyPagedRequest request, Guid userId);
    }
}
