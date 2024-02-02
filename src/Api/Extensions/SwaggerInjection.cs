using Microsoft.OpenApi.Models;

namespace Api.Extensions;

public static class SwaggerInjection
{
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(configs =>
            configs.SwaggerDoc(
                name: "v1",
                info: new OpenApiInfo
                {
                    Title = "Store Service Api",
                    Version = "1"
                }));

        return services;
    }
}