using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
using NoteApp.IntegrationTests.Helpers;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;
using NoteApp.Server.Services;

namespace NoteApp.IntegrationTests
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private Mock<IAccountService> _accountServiceMock = new Mock<IAccountService>();

        // Prepare WebAPI for tests
        public AccountControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<NoteAppContext>));

                        if (dbContextOptions != null)
                        {
                            services.Remove(dbContextOptions);
                        }
                        
                        services.AddDbContext<NoteAppContext>(options => options.UseInMemoryDatabase("NoteAppDb"));

                        services.AddSingleton<IAccountService>(_accountServiceMock.Object);
                    });
                })
                .CreateClient();
        }

        [Fact]
        public async Task RegisterUserAsync_WithValidModel_ReturnsOkStatus()
        {
            // Arrange
            var registerUser = new RegisterUserDto()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "Test@test.com",
                Password = "TestPassword",
                ConfirmPassword = "TestPassword"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/accounts/register", httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUserAsync_WithInvalidModel_ReturnsBadRequestStatus()
        {
            // Arrange
            var registerUser = new RegisterUserDto()
            {
                Password = "TestPassword",
                ConfirmPassword = "Test"
            };

            var httpContent = registerUser.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("/api/accounts/register", httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task LoginAsync_ForRegisteredUser_ReturnsOkStatus()
        {
            // Arrange
            _accountServiceMock
                .Setup(e => e.LoginAsync(It.IsAny<LoginUserDto>()))
                .Returns(Task.FromResult("Jwt"));

            var loginUser = new LoginUserDto()
            {
                Email = "test@test.com",
                Password = "TestPassword"
            };

            var httpContent = loginUser.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("api/accounts/login", httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task LoginAsync_InvalidModel_ReturnsbadRequestStatus()
        {
            // Arrange
            var loginUser = new LoginUserDto()
            {
                Email = "test@test.com"
            };

            var httpContent = loginUser.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("api/accounts/login", httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

    }
}
