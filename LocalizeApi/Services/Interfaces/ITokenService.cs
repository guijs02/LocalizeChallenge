using Localiza.Core.Requests;

namespace LocalizeApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginUserRequest user);
    }
}