using Localiza.Core.Requests;
using Localiza.Core.Responses;
using LocalizeApi.Repository.Interfaces;
using LocalizeApi.Services.Interfaces;

namespace LocalizeApi.Services
{
    public class UserService(IUserRepository userRepository, ITokenService tokenService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<Response<bool>> CreateAsync(CreateUserRequest request)
        {
            return await _userRepository.CreateAsync(request);
        }

        public async Task<Response<string>> LoginAsync(LoginUserRequest request)
        {
            var result = await _userRepository.LoginAsync(request);

            return result.IsSuccess ?
             new Response<string>(_tokenService.GenerateToken(request))
            : new Response<string>(null, code: result.StatusCode, message: result.Message);
        }
    }
}
