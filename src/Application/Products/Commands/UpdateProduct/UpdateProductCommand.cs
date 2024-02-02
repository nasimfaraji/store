using Application.Models.Operations;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(int ProductId, string Title, double? Price,
    float? Discount, int? AddedNumberToInventoryCount) : IRequest<OperationResult>;