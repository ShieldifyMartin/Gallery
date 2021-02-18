namespace GalleryServer
{
    using Microsoft.AspNetCore.Builder;    
    using Microsoft.AspNetCore.Hosting;    
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;    
    using GalleryServer.Infrastructure;   
    using GalleryServer.Infrastructure.Extensions;    
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using GalleryServer.Data.Models;
    using GalleryServer.Infrastructure.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        { 
            services
                .AddDatabase(this.Configuration)
                .AddIdentity()
                .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
                .AddApplicationServices()
                .AddCloudinaryService(services.GetApplicationSettings(this.Configuration))
                .AddApiControllers();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }                        

            app.UseRouting();

            app.UseCors(options => options
                .WithOrigins("http://localhost:8080")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRService>("/postHub");
            });

            ApplicationDbInitializer.SeedUsers(userManager, roleManager);

            app.ApplyMigrations();
        }
    }
}
