using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NoteApp.Server.Entities;

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
                    });
                })
                .CreateClient();
        }

        [Fact]
        public void GetAllAsync_WithoutParameters_ReturnsOk()
        {

        }
    }
}