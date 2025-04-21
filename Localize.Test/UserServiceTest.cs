using Localiza.Core.Requests;
using Localiza.Core.Responses;
using LocalizeApi.Repository.Interfaces;
using LocalizeApi.Services;
using LocalizeApi.Services.Interfaces;
using Moq;

namespace Localize.Test
{
    public class UserServiceTest
    {
        private readonly UserService _service;
        private readonly Mock<IUserRepository> _repository;
        private readonly Mock<ITokenService> _tokenRepository;
        public UserServiceTest()
        {
            _repository = new Mock<IUserRepository>();
            _tokenRepository = new Mock<ITokenService>();
            _service = new(_repository.Object, _tokenRepository.Object);
        }

        [Fact]
        public async Task CreateUserWithSuccess()
        {
            var user = new CreateUserRequest
            {
                Email = "gui@email.com",
                Name = "gui",
                Password = "123",
            };

            var resultExpected = _repository.Setup(s => s.CreateAsync(It.IsAny<CreateUserRequest>())).ReturnsAsync(new Response<bool>(true));

            var result = await _service.CreateAsync(user);

            Assert.True(result?.Data);
        }
        [Fact]
        public async Task CreateUserShouldBeFalse()
        {
            var user = new CreateUserRequest
            {
                Email = "gui@email.com",
                Name = "gui",
                Password = "123",
            };

            var resultExpected = _repository.Setup(s => s.CreateAsync(It.IsAny<CreateUserRequest>()))
                                            .ReturnsAsync(new Response<bool>(false));

            var result = await _service.CreateAsync(user);

            Assert.False(result?.Data);
        }

        [Fact]
        public async Task LoginUserWithSuccess()
        {
            var user = new LoginUserRequest
            {
                Email = "gui@email.com",
                Name = "gui",
                Password = "123",
            };

            string token = "Token123";

            _repository.Setup(s => s.LoginAsync(It.IsAny<LoginUserRequest>()))
                                            .ReturnsAsync(new Response<bool>(true));

            _tokenRepository.Setup(s => s.GenerateToken(It.IsAny<LoginUserRequest>()))
                                            .Returns(token);

            var result = await _service.LoginAsync(user);

            Assert.True(result?.IsSuccess);
            Assert.Equal(token, result?.Data);
        }

        [Fact]
        public async Task LoginUserShouldBeFalse()
        {
            var user = new LoginUserRequest
            {
                Email = "gui@email.com",
                Name = "gui",
                Password = "123",
            };

            _repository.Setup(s => s.LoginAsync(It.IsAny<LoginUserRequest>()))
                                            .ReturnsAsync(new Response<bool>(false, 500));

            var result = await _service.LoginAsync(user);

            Assert.False(result?.IsSuccess);
            Assert.Null(result?.Data);
        }

    }
}