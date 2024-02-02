using Application.Models.Produtcts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ResultFilters;

public sealed class GetProductResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is ProductModel value)
        {
            result.Value = new
            {
                ProductId = value.ProductId,
                Title = value.Title,
                InventoryCount = value.InventoryCount,
                SalePrice = value.SalePrice,
                Discount = value.Discount,
                DiscountedPrice = value.DiscountedPrice
            };
        }

        await next();
    }
}