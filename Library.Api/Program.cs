using Library.Api.Abstractions;
using Library.Api.Configuration;
using Library.Api.Persistence;
using Library.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddSingleton<DbInitializer>();
builder.Services.AddSingleton<IBooksService, BooksService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Initialisation de la base de données
var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.MapDiscoveredEndpoints(typeof(Program).Assembly);

app.Run();
