using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ResultFilters;

public sealed class BuyProductResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is Order value)
        {
            result.Value = new
            {
                OrderId = value.Id,
                CreationDate = value.CreationDate
            };
        }

        await next();
    }
}