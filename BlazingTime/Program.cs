using BlazingTime.Components;
using BlazingTime.Services.DataFetching;
using BlazingTime.Shared.Interfaces;
using BlazingTime.Shared.Options;

namespace BlazingTime
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .AddEnvironmentVariables();

            builder.Logging.SetMinimumLevel(LogLevel.Information);
            builder.Logging.AddConsole();


            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.Configure<TogglOptions>(
                builder.Configuration.GetSection("Toggl"));


            //builder.Services.AddHttpClient<ITimeEntryFetcher, TogglTrackTimeEntryFetcher>();
            builder.Services.AddScoped<ITimeEntryFetcher, MockTimeEntryFetcher>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
