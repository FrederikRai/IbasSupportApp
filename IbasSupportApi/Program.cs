using IbasSupportApi.Services;        // <-- VIGTIGT (dette var årsagen til fejlen)
using IbasSupportApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Load appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        policy => policy
            .WithOrigins("http://localhost:5111")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Register controllers
builder.Services.AddControllers();

// Register Cosmos DB service (DI)
builder.Services.AddSingleton<CosmosDbService>();

var app = builder.Build();

// CORS
app.UseCors("AllowBlazorApp");

// Authorization (kan stå her – men du bruger det ikke nu)
app.UseAuthorization();

// Map API controllers
app.MapControllers();

app.Run();
