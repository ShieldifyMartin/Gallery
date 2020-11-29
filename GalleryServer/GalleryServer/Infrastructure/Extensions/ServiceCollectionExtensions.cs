namespace GalleryServer.Infrastructure.Extensions
{
    using GalleryServer.Infrastructure.Filters;
    using GalleryServer.Data;
    using GalleryServer.Data.Models;
    using GalleryServer.Data.Models.Repositories;
    using GalleryServer.Features.Category;
    using GalleryServer.Features.Cats;
    using GalleryServer.Features.Cloudinary;
    using GalleryServer.Features.Identity;
    using GalleryServer.Features.Profile;
    using GalleryServer.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
    using System.Text;

    public static class ServiceCollectionExtensions
    {
        public static AppSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppSettings>();
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddScoped(typeof(IDeletableEntityRepository<>), typeof(DeletableEntityRepository<>))
                .AddScoped(typeof(BaseRepository<>), typeof(BaseRepository<>))
                .AddTransient<ICurrentUserService, CurrentUserService>()
                .AddTransient<ICloudinaryService, CloudinaryService>()
                .AddTransient<ICategoriesService, CategoriesService>()
                .AddTransient<IPostsService, PostsService>()
                .AddTransient<IProfilesService, ProfilesService>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ApplicationDbInitializer>();

        public static IServiceCollection AddCloudinaryService(this IServiceCollection services, AppSettings appSettings)
        {
            Account account = new Account(
                appSettings.CloudName,
                appSettings.APIKey,
                appSettings.APISecret
            );

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            return services;
        }

        public static void AddApiControllers(this IServiceCollection services)
            => services
                .AddAntiforgery(options =>
                {
                    options.HeaderName = "X-CSRF-TOKEN";
                })
                .AddControllers(options => options
                    .Filters
                    .Add<ModelOrNotFoundActionFilter>())
                .AddNewtonsoftJson(
                    options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }
}
