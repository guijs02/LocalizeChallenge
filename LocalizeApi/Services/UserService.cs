using Localiza.Core.Requests;
using Localiza.Core.Responses;
using LocalizeApi.Repository.Interfaces;
using LocalizeApi.Services.Interfaces;

namespace LocalizeApi.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task<Response<bool>> CreateAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> LoginAsync(LoginUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
