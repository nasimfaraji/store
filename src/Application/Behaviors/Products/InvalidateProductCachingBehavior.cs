using Application.Constants;
using Application.Models.Operations;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Behaviors.Products;

public class InvalidateProductCachingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, OperationResult>
{
    private readonly IMemoryCache _memoryCache;

    public InvalidateProductCachingBehavior(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<OperationResult> Handle(TRequest request,
        RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (!response.Succeeded)
            return await next();

        var productId = (int)response.OperationValues[OperationValueKeys.ProductId];

        var cacheKey = CacheKeys.GetProductByIdCacheKey(productId);
        _memoryCache.Remove(cacheKey);

        return response;
    }
}