using Blazored.Modal;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedisReader.Server.Data;
using RedisReader.Server.Services;

namespace RedisReader.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
            services.AddSingleton<WeatherForecastService>();
            services.AddBlazoredModal();
            services.AddBlazorContextMenu();
            
            services.AddScoped<IStoreConnections, ConnectionStore>();
            services.AddSingleton<IStoreConnections, ConnectionStore>();
            services.AddSingleton<IReadFromRedis, Services.DataReader>();
            services.AddSingleton<IManageRedisConnections, ManageRedisConnections>();
            services.AddSingleton<ConnectionStateContainer>();
            services.AddSingleton<KeyStateContainer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
            
            if (HybridSupport.IsElectronActive)
            {
                ElectronBootstrap();
            }
        }

        public static async void ElectronBootstrap()
        {
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Width = 1920,
                Height = 1080,
                Show = false
            });

            await browserWindow.WebContents.Session.ClearCacheAsync();

            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.SetTitle("Blazor in Shell");
        }
    }
}