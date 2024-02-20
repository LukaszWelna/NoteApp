using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NoteApp.Server.Entities;
using NoteApp.Server.Middleware;
using NoteApp.Server.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NoteAppContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("NoteAppConnectionString")));

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NoteApp API");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
