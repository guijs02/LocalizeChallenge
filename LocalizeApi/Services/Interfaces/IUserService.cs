using Localiza.Core.Requests;
using Localiza.Core.Responses;

namespace LocalizeApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response<bool>> CreateAsync(CreateUserRequest request);
        Task<Response<string>> LoginAsync(LoginUserRequest request);
    }
}
