using IbasSupportApp;
using IbasSupportApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Tilføj HTTP Client og SupportApiService
builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<SupportApiService>();

await builder.Build().RunAsync();
