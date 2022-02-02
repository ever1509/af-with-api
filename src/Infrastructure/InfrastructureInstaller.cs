using Application.Common.Interfaces;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<Context>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("ContextConnection"),
                b => b.MigrationsAssembly(typeof(Context).Assembly.FullName)));


        services.AddScoped<IContext>(provider => provider.GetRequiredService<Context>());

        services.AddScoped<Context>(
            sp => sp.GetRequiredService<IDbContextFactory<Context>>()
                .CreateDbContext());

        return services;
    }
}

