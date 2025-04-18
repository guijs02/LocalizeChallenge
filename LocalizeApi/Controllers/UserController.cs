using Localiza.Core.Constants;
using Localiza.Core.Requests;
using LocalizeApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalizeApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ICompanyService _companyService;

        public UserController(IUserService userService, ICompanyService companyService)
        {
            _userService = userService;
            _companyService = companyService;
        }

        [HttpPost(Endpoints.CreateUser)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            try
            {

                var result = await _userService.CreateAsync(request);
                return StatusCode(result.StatusCode, result.Data);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message.ToString());
            }
        }

        [HttpPost(Endpoints.LoginUser)]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            try
            {
                var result = await _userService.LoginAsync(request);
                return StatusCode(result.StatusCode, result.Data);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message.ToString());
            }
        }
    }
}
