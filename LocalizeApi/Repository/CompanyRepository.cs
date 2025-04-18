using LocalizeApi.Context;
using LocalizeApi.Repository.Interfaces;

namespace LocalizeApi.Repository
{
    public class CompanyRepository(LocalizeDbContext context) : ICompanyRepository
    {
        // For example:
        // public async Task<Company> GetCompanyByIdAsync(Guid id)
        // {
        //     return await _context.Module.FindAsync(id);
        // }
    }
}
