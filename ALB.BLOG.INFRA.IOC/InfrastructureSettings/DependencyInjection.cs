using ALB.BLOG.BLO.Interfaces;
using ALB.BLOG.BLO.Rules;
using ALB.BLOG.BLO.Services;
using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DAL.Querys;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
using ALB.BLOG.INFRA.DbUtilites;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ALB.BLOG.INFRA.IOC.InfrastructureSettings
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
          IConfiguration configuration)
        {
            //Database context configuration.
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnectionString"),
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //Identity configuration.
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/AccessDenied";
            });

            //Dependency injection of initial database configurations.
            services.AddScoped<IDbInitializer, DbInitializer>();

            //Identity dependency injection.
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<SignInManager<ApplicationUser>>();

            //Dependency injection of the DAO layer.
            services.AddScoped<IApplicationUserDAO, ApplicationUserDAO>();
            services.AddScoped<IEmailDAO, EmailDAO>();
            services.AddScoped<IPageDAO, PageDAO>();
            services.AddScoped<IPostDAO, PostDAO>();
            services.AddScoped<ISettingDAO, SettingDAO>();

            //Dependency injection of the BLO layer.
            services.AddScoped<IApplicationUserBLO, ApplicationUserBLO>();
            services.AddScoped<IEmailBLO, EmailBLO>();
            services.AddScoped<IPageBLO, PageBLO>();
            services.AddScoped<IPostBLO, PostBLO>();
            services.AddScoped<ISettingBLO, SettingBLO>();

            //Dependency injection of BLO Serves layer Services.
            services.AddScoped<IGeneralBlogServices, GeneralBlogServices>();

            return services;
        }
    }
}