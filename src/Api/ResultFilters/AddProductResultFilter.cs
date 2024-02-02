using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.ResultFilters;

public sealed class AddProductResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is Product value)
        {
            result.Value = new
            {
                ProductId = value.Id,
                Title = value.Title
            };
        }

        await next();
    }
}