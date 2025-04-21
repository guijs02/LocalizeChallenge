using Localiza.Core.Requests;
using Localiza.Core.Responses;
using System.Net;
using System.Net.Http.Json;

namespace Localize.TestE2E
{
    public class CompanyTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CompanyTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_Create_Company_WithSuccess()
        {

            // Buscar os cursos cadastrados
            var token = await CreateLoginUser();

            string cnpj = "68243096000233";

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token?.Data);

            var response = await _client.PostAsJsonAsync("api/v1/company", new CreateCompanyRequest() { Cnpj = cnpj });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content?.Headers?.ContentType?.MediaType);
        }

        [Theory]
        [InlineData("bad")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("6824309600023309")]
        [InlineData("68243096000231")]
        [InlineData("68243096")]
        public async Task Should_NOT_Create_Company_With_Wrong_CNPJ(string cnpj)
        {

            // Buscar os cursos cadastrados
            var token = await CreateLoginUser();

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token?.Data);

            var response = await _client.PostAsJsonAsync("api/v1/company", cnpj);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Should_GetAll_Companies_WithSuccess()
        {
            var token = await CreateLoginUser();
            // Buscar os cursos cadastrados
            string cnpj = "68243096000233";

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token?.Data);

            await _client.PostAsJsonAsync("api/v1/company", cnpj);

            var response = await _client.GetAsync("api/v1/company");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content?.Headers?.ContentType?.MediaType);
        }

        public async Task<Response<string>> CreateLoginUser()
        {
            var user = new CreateUserRequest
            {
                Name = "gui",
                Email = "gui@email.com",
                Password = "123",
            };

            // Buscar os cursos cadastrados
            await _client.PostAsJsonAsync("api/v1/user/register", user);

            var login = new LoginUserRequest
            {
                Name = "gui",
                Email = "gui@email.com",
                Password = "123",
            };

            // Buscar os cursos cadastrados
            var responseLogin = await _client.PostAsJsonAsync("api/v1/user/login", login);

            return await responseLogin.Content.ReadFromJsonAsync<Response<string>>();
        }
    }
}
