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

builder.Services.AddControllers();
builder.Services.AddSingleton<CosmosDbService>();


var app = builder.Build();

app.UseCors("AllowBlazorApp");

app.UseAuthorization(); // <-- DEN MANGLEDE

app.MapControllers();

app.Run();
