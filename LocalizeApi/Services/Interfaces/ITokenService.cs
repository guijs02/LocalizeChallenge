using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Localiza.Core.Requests;

namespace LocalizeApi.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginUserRequest user);
    }
}