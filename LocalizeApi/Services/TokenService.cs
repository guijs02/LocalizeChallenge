
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Localiza.Core.Requests;
using LocalizeApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

public class TokenService(IConfiguration config) : ITokenService
    {
        private readonly IConfiguration _config = config;

        public string GenerateToken(LoginUserRequest user)

        {
            var secretKey = _config["Jwt:Key"];

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.Name, user.Name.ToString()),
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim("id", user.UserId.ToString()),
                    ]
                ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            //retorna o token em forma de cadeia de caracteres
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }