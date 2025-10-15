using AppBlazor.Client;
using AppBlazor.Client.Services;
using AppBlazor.Client.Servicios;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
{ BaseAddress = new Uri("http://localhost:5139/") });

builder.Services.AddScoped<LibroService>();

builder.Services.AddScoped<UtilService>();

builder.Services.AddScoped<TipoLibroService>();

builder.Services.AddScoped<AutorService>();

await builder.Build().RunAsync();
