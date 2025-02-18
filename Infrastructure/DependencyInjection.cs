using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                "Server=DESKTOP-ROTOGEN;Database=CleanArchitectureEFCoreDb;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
        });
        
        return services;
    }
}