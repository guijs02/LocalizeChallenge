using System.Net.Http.Json;
using System.Net;
using Localiza.Core.Requests;

namespace Localize.TestE2E
{
    public class UserTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient _client;

        public UserTest(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_Register_User()
        {
            var user = new CreateUserRequest
            {
                Name = "gui123",
                Email = "gui@gmail.com",
                Password = "1234",
            };

            // Buscar os cursos cadastrados
            var response = await _client.PostAsJsonAsync("api/v1/user/register", user);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content?.Headers?.ContentType?.MediaType);
        }

        [Theory]
        [InlineData("bad", "", "" )]
        [InlineData("bad", null, null )]
        [InlineData("bad", "", null )]
        [InlineData("bad@", null, "")]
        [InlineData("", null, "")]
        [InlineData(null, null, null)]
        public async Task Should_NOT_Register_User_With_Bad_Data(string email, string name, string password)
        {
            var user = new CreateUserRequest
            {
                Name = name,
                Email = email,
                Password = password,
            };

            // Buscar os cursos cadastrados
            var response = await _client.PostAsJsonAsync("api/v1/user/register", user);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("application/problem+json", response.Content?.Headers?.ContentType?.MediaType);
        }

        [Fact]
        public async Task Should_Login_User()
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
            var response = await _client.PostAsJsonAsync("api/v1/user/login", login);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content?.Headers?.ContentType?.MediaType);
        }

        [Fact]
        public async Task Should_NOT_Login_User_With_Wrong_Password()
        {
            var user = new CreateUserRequest
            {
                Name = "gui",
                Email = "gui@email.com",
                Password = "123",
            };

            // Buscar os cursos cadastrados
            await _client.PostAsJsonAsync("api/v1/user/create-user", user);
            
            var login = new LoginUserRequest
            {
                Name = "gui",
                Email = "gui@email.com",
                Password = "1233",
            };

            // Buscar os cursos cadastrados
            var response = await _client.PostAsJsonAsync("api/v1/user/login", login);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal("application/json", response.Content?.Headers?.ContentType?.MediaType);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}