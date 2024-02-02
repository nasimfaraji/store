using Application.Models.Operations;
using MediatR;

namespace Application.Products.Commands.BuyProduct;

public sealed record BuyProductCommand(int ProductId, int UserId, int RequestedProductCount) :
    IRequest<OperationResult>;