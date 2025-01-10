using SampleApp.Components;
using SampleApp.Services;

namespace SampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddHttpClient<ChatbotService>();

            // Add HttpClient support for both WebAssembly and Server.
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddHttpClient("DefaultClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:44340"); // Development base URL
                });
            }
            else
            {
                builder.Services.AddHttpClient("DefaultClient", client =>
                {
                    client.BaseAddress = new Uri("https://yourproductionurl.com"); // Production base URL
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery(); // Ensure proper antiforgery token handling.

            // Map Razor Components with support for WebAssembly and Server-side.
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
