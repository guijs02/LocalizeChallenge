using Localiza.Core.Requests;
using Localiza.Core.Responses;

namespace LocalizeApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<Response<bool>> CreateAsync(CreateUserRequest request);
        Task<Response<bool>> LoginAsync(LoginUserRequest request);
    }
}
