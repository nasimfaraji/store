namespace Application.Constants;

public static class CacheKeys
{
    public static string GetProductByIdCacheKey(int id) => $"product-id-{id}";
}