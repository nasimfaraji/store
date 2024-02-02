using Application.Models.Operations;
using MediatR;

namespace Application.Products.Commands.AddProduct;

public sealed record AddProductCommand(
    string Title, double Price, float Discount) : IRequest<OperationResult>;