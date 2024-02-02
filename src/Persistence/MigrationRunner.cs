using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class MigrationRunner
{
    public static IServiceCollection RunMigrations(this IServiceCollection services)
    {
        using var scoped = services.BuildServiceProvider().CreateScope();
        var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        return services;
    }
}