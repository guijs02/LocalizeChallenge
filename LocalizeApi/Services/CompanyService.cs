using LocalizeApi.Repository.Interfaces;
using LocalizeApi.Services.Interfaces;

namespace LocalizeApi.Services
{
    public class CompanyService(ICompanyRepository companyRepository) : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;
    }
}
