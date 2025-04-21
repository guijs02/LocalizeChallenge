using Localiza.Core.Requests;
using Localize.Core.Requests;
using Localize.Core.Utils;
using LocalizeApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LocalizeApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController(ICompanyService companyService) : Controller
    {
        private readonly ICompanyService _companyService = companyService;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
        {
            var req = Cnpj.Validate(request.Cnpj);

            if (!req.IsValid)
                return BadRequest(req.Message);

            var userId = User.FindFirstValue("id");

            var result = await _companyService.CreateAsync(request.Cnpj, Guid.Parse(userId));

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCompanyPagedRequest request)
        {
            var userId = User.FindFirstValue("id");
            var result = await _companyService.GetAllAsyncPaged(request, Guid.Parse(userId));

            return StatusCode(result.StatusCode, result.Data);
        }
    }
}
