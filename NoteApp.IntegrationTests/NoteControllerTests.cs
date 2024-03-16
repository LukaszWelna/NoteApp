using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NoteApp.IntegrationTests.Helpers;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;
using System.Text;

namespace NoteApp.IntegrationTests
{
    public class NoteControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public NoteControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services
                            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<NoteAppContext>));
                       
                        services.Remove(dbContextOptions);

                        services
                            .AddDbContext<NoteAppContext>(options => options.UseInMemoryDatabase("NoteAppDb"));

                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(options => options.Filters.Add(new FakeUserFilter()));
                    });
                })
                .CreateClient();
        }

        [Fact]
        public async Task GetAllAsync_WithoutParameters_ReturnsOkStatus()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/api/notes");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateNoteAsync_WithValidModel_ReturnsCreatedStatus()
        {
            // Arrange 
            var note = new CreateNoteDto
            {
                Title = "Test title",
                Content = "Test content"
            };

            var httpContent = note.ToJsonHttpContent();

            // Act
            var response = await _httpClient.PostAsync("api/notes", httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateNoteAsync_WithInvalidModel_ReturnsBadRequestStatus()
        {
            // Arrange
            var note = new CreateNoteDto()
            {
                Title = "",
                Content = ""
            };

            // Act
            var httpContent = note.ToJsonHttpContent();

            var response = await _httpClient.PostAsync("/api/notes", httpContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

    }
}