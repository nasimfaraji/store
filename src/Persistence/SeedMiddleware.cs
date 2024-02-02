using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.SeedData;

namespace Persistence;

public static class SeedMiddleware
{
    public static void RunSeeder(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        UserSeeder.Seed(scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
    }
}