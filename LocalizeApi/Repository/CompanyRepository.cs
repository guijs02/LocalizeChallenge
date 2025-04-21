using Domain.Entites;
using Localiza.Core.Responses;
using Localize.Core.Requests;
using Localize.Core.Responses;
using LocalizeApi.Context;
using LocalizeApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LocalizeApi.Repository
{
    public class CompanyRepository(LocalizeDbContext context) : ICompanyRepository
    {
        public async Task<Response<bool>> CreateAsync(ReceitaWsResponse receitaWs, Guid userId)
        {
            try
            {
                var company = new Company()
                {
                    LegalName = receitaWs.Nome,
                    AddressComplement = receitaWs.Complemento,
                    Cnpj = receitaWs.Cnpj,
                    City = receitaWs.Municipio,
                    Number = receitaWs.Numero,
                    OpeningDate = receitaWs.Abertura,
                    PrimaryActivity = receitaWs.Atividade_Principal.FirstOrDefault().Text,
                    LegalNature = receitaWs.Natureza_Juridica,
                    State = receitaWs.Uf,
                    Type = receitaWs.Tipo,
                    ZipCode = receitaWs.Cep,
                    Street = receitaWs.Logradouro,
                    TradeName = receitaWs.Fantasia,
                    Status = receitaWs.Situacao,
                    UserId = userId
                };

                await context.Companies.AddAsync(company);

                await context.SaveChangesAsync();

                return new Response<bool>(true, message: "Dados salvos com sucesso!");
            }
            catch (Exception)
            {
                return new Response<bool>
                    (false, StatusCodes.Status500InternalServerError, "Ocorreu um erro ao salvar as informações");
            }
        }

        public async Task<PagedResponse<IEnumerable<CompanyResponse>>> GetAllAsyncPaged(GetAllCompanyPagedRequest request, Guid userId)
        {
            try
            {
                var query = context
                             .Companies
                             .AsNoTracking()
                             .Where(x => x.UserId == userId);

                var companies = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(c => new CompanyResponse
                {
                    Cnpj = c.Cnpj,
                    LegalName = c.LegalName,
                    Status = c.Status
                })
                .AsEnumerable();

                var count = await query.CountAsync();

                return new PagedResponse<IEnumerable<CompanyResponse>>(
                     companies,
                     count,
                     request.PageNumber,
                     request.PageSize);
            }
            catch (Exception)
            {
                return new PagedResponse<IEnumerable<CompanyResponse>>
                                    (null, StatusCodes.Status500InternalServerError, "Erro ao obter as empresas!");
            }
        }
    }
}
