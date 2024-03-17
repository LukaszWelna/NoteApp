using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Xml.Serialization;

namespace NoteApp.IntegrationTests
{
    public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly List<Type> _controllerTypes;

        // Prepare WebAPI for tests
        public ProgramTests(WebApplicationFactory<Program> factory)
        {
            _controllerTypes = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
                .ToList();

            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        _controllerTypes.ForEach(c => services.AddScoped(c));
                    });
                });
        }

        // Check if all dependencies for controllers are properly registered in DI container 
        [Fact]
        public void ConfigureServices_ForControllers_RegistersAllDependencies()
        {
            // Arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory?.CreateScope();

            // Act

            // Assert
            _controllerTypes.ForEach(t =>
            {
                var controller = scope?.ServiceProvider.GetService(t);
                controller.Should().NotBeNull();
            });
        }
    }
}
