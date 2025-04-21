using Localiza.Core.Constants;
using Localiza.Core.Requests;
using LocalizeApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalizeApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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
            var result = await _userService.CreateAsync(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost(Endpoints.LoginUser)]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {

            var result = await _userService.LoginAsync(request);
            return StatusCode(result.StatusCode, result);
        }
    }
}
