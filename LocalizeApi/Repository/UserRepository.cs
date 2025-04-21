using Localiza.Core.Requests;
using Localiza.Core.Responses;
using LocalizeApi.Context;
using LocalizeApi.Models;
using LocalizeApi.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LocalizeApi.Repository
{
    public class UserRepository(UserManager<User> userManager,
                             RoleManager<IdentityRole<Guid>> roleManager,
                             SignInManager<User> signInManager)
                             : IUserRepository
    {
        public async Task<Response<bool>> CreateAsync(CreateUserRequest user)
        {
            try
            {

                var model = new User
                {
                    UserName = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                };

                var result = await userManager.CreateAsync(model, model.Password);

                return !result.Succeeded ?
                        new Response<bool>(false, StatusCodes.Status409Conflict, "Ocorreu um erro ao criar o usuário!")
                        : new Response<bool>(result.Succeeded);

            }
            catch (Exception)
            {
                return new Response<bool>
                        (false, StatusCodes.Status500InternalServerError, "Ocorreu um erro inesperado ao criar o usuário!");
            }
        }
        public async Task<Response<bool>> LoginAsync(LoginUserRequest login)
        {
            try
            {
                var model = new User
                {
                    UserName = login.Name,
                    Email = login.Email,
                    Password = login.Password,
                };

                var result = await signInManager.PasswordSignInAsync(
                  login.Name,
                  login.Password,
                  false,
                  false
                );

                if (!result.Succeeded)
                    return new Response<bool>(result.Succeeded, code: StatusCodes.Status401Unauthorized, message: "Usuario invalido");

                var user = signInManager.UserManager.Users.FirstOrDefault(
                    u => u.NormalizedUserName == login.Name.ToUpper()
                );

                login.UserId = user.Id;

                return new Response<bool>(result.Succeeded);
            }
            catch (Exception)
            {
                return new Response<bool>
                    (false, code: StatusCodes.Status500InternalServerError, message: "Ocorreu um erro ao logar o usuário");
            }
        }

    }
}
