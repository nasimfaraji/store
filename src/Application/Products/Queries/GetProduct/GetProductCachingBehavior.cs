using Application.Constants;
using Application.Models.Operations;
using Application.Models.Produtcts;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Products.Queries.GetProduct;

public sealed class GetProductCachingBehavior<TRequest, TResponse> : IPipelineBehavior<GetProductQuery, OperationResult>
{
    private readonly IMemoryCache _memoryCache;
    private const double GetProductByIdCacheTtlInMinutes = 1;

    public GetProductCachingBehavior(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<OperationResult> Handle(GetProductQuery request,
        RequestHandlerDelegate<OperationResult> next, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.GetProductByIdCacheKey(request.ProductId);

        if (_memoryCache.TryGetValue(cacheKey, out ProductModel cacheValue))
            return new OperationResult(OperationResultStatus.Ok, value: cacheValue);

        var response = await next();

        if (!response.Succeeded || response.Value is not ProductModel)
            return await next();

        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(GetProductByIdCacheTtlInMinutes));

        _memoryCache.Set(cacheKey, response.Value, cacheEntryOptions);

        return response;
    }
}