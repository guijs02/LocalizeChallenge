using Localiza.Core.Responses;
using Localize.Core.Requests;
using Localize.Core.Responses;
using LocalizeApi.Repository.Interfaces;
using LocalizeApi.Services.Interfaces;

namespace LocalizeApi.Services
{
    public class CompanyService(ICompanyRepository companyRepository,
                                IReceitaWsService receitaService)
                                : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
        private readonly IReceitaWsService _receitaService = receitaService;

        public async Task<Response<bool>> CreateAsync(string cnpj, Guid userId)
        {
            var result = await _receitaService.GetCompanyDataAsync(cnpj);

            if (!result.IsSuccess)
                return new Response<bool>(false, code: StatusCodes.Status502BadGateway, message: result.Message);

            return await _companyRepository.CreateAsync(result.Data, userId);
        }

        public async Task<PagedResponse<IEnumerable<CompanyResponse>>> GetAllAsyncPaged(GetAllCompanyPagedRequest request, Guid userId)
        {
            return await _companyRepository.GetAllAsyncPaged(request, userId);
        }
    }
}
