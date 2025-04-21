using LocalizeApi.Context;
using LocalizeApi.Models;
using LocalizeApi.Repository;
using LocalizeApi.Repository.Interfaces;
using LocalizeApi.Services;
using LocalizeApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LocalizeApi.Common
{
    public static class BuildExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection service)
        {
            service.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT com a palavra 'Bearer' antes do token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            service.AddEndpointsApiExplorer();
            return service;
        }
        public static AuthenticationBuilder ConfigJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
                        ),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
        }
        public static IServiceCollection ConfigIdentityOptions(this IServiceCollection service)
        {
            return service.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 3;
            });
        }

        public static IServiceCollection AddContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<LocalizeDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return service;
        }
        public static IdentityBuilder AddIdentityRole(this IServiceCollection service)
        {
            return service
                .AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<LocalizeDbContext>()
                .AddDefaultTokenProviders();
        }

        public static IServiceCollection AddDependencies(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ICompanyRepository, CompanyRepository>();
            service.AddScoped<ICompanyService, CompanyService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IReceitaWsService, ReceitaWsService>().AddHttpClient("ReceitaWs", c => c.BaseAddress = new Uri("https://www.receitaws.com.br/v1/"));

            return service;
        }

    }
}
