using Microsoft.EntityFrameworkCore;
using WorkshopHub.Data.Context;
using WorkshopHub.Service;

namespace WorkshopHub.API.Modules
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
               .FromAssembliesOf(typeof(IRequestHandler<>))
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime()
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

            services.AddDbContext<WorkshopHubDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
