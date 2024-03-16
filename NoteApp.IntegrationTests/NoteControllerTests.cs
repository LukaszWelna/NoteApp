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
        private readonly WebApplicationFactory<Program> _factory;
        public NoteControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory
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
                });
                
            _httpClient = _factory.CreateClient();
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

        [Fact]
        public async Task DeleteNoteByIdAsync_ForNonExistingNote_ReturnsNotFoundStatus()
        {
            // Arrange

            // Act
            var response = await _httpClient.DeleteAsync("/api/notes/1000");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        private void SeedNote(Note note)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<NoteAppContext>();

            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task DeleteNoteByIdAsync_ForValidaData_ReturnsNoContent()
        {
            // Arrange
            var note = new Note
            {
                Title = "Test title",
                UserId = 1
            };

            // Seed
            SeedNote(note);

            // Act
            var response = await _httpClient.DeleteAsync("/api/notes/" + note.Id);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteNoteByIdAsync_ForNonNoteOwner_ReturnsForbiddenStatus()
        {
            // Arrange
            var note = new Note
            {
                Title = "Test title",
                UserId = 1000
            };

            // Seed
            SeedNote(note);

            // Act
            var response = await _httpClient.DeleteAsync("/api/notes/" + note.Id);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }

    }
}