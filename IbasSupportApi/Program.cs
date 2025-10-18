using IbasSupportApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 👇 Sørg for at appsettings.json bliver læst
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// 👇 Tilføj CORS så Blazor må kalde API’et
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
app.MapControllers();

app.Run();
