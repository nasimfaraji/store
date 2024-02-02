using Api.Extensions;
using Api.Models.Requests;
using Api.ResultFilters;
using Application.Products.Commands.AddProduct;
using Application.Products.Commands.BuyProduct;
using Application.Products.Queries.GetProduct;
using Application.Products.Commands.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route(Routes.Products)]
public sealed class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AddProductResultFilter]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
    {
        var operation = await _mediator.Send(new AddProductCommand(
            Title: request.Title,
            Price: request.Price,
            Discount: request.Discount));

        return this.ReturnResponse(operation);
    }

    [HttpGet("{id:int}")]
    [GetProductResultFilter]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
        var operation = await _mediator.Send(new GetProductQuery(ProductId: id));

        return this.ReturnResponse(operation);
    }

    [HttpPut("{id:int}")]
    [UpdateProductResultFilter]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id,
        [FromBody] UpdateProductRequest request)
    {
        var operation = await _mediator.Send(new UpdateProductCommand(
            ProductId: id,
            Title: request.Title,
            Price: request.Price,
            Discount: request.Discount,
            AddedNumberToInventoryCount: request.AddedNumberToInventoryCount));

        return this.ReturnResponse(operation);
    }

    [HttpPatch("{id:int}/buy")]
    [BuyProductResultFilter]
    public async Task<IActionResult> BuyProduct([FromRoute] int id,
        [FromBody] BuyProductRequest request)
    {
        var operation = await _mediator.Send(new BuyProductCommand(
            ProductId: id,
            UserId: request.UserId,
            RequestedProductCount: request.RequestedProductCount));

        return this.ReturnResponse(operation);
    }
}