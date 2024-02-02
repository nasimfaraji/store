using Application.Behaviors;
using Application.Behaviors.Products;
using Application.Models.Operations;
using Application.Products.Commands.BuyProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProduct;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        services.AddTransient(typeof(IPipelineBehavior<GetProductQuery, OperationResult>),
            typeof(GetProductCachingBehavior<GetProductQuery, OperationResult>));

        services.AddTransient(typeof(IPipelineBehavior<UpdateProductCommand, OperationResult>),
            typeof(InvalidateProductCachingBehavior<UpdateProductCommand, OperationResult>));

        services.AddTransient(typeof(IPipelineBehavior<BuyProductCommand, OperationResult>),
            typeof(InvalidateProductCachingBehavior<BuyProductCommand, OperationResult>));

        services.AddTransient(typeof(IPipelineBehavior<BuyProductCommand, OperationResult>),
            typeof(ValidationBehavior<BuyProductCommand, OperationResult>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}