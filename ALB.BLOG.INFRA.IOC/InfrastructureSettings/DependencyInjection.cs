using ALB.BLOG.DAL.Interfaces;
using ALB.BLOG.DAL.Querys;
using ALB.BLOG.DOMAIN.Models;
using ALB.BLOG.INFRA.DbContextConnections;
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

            //Dependency injection of the DAO layer.
            services.AddScoped<IEmailDAO, EmailDAO>();
            services.AddScoped<IPageDAO, PageDAO>();

            return services;
        }
    }
}