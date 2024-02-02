using Application.Models.Operations;
using MediatR;

namespace Application.Products.Queries.GetProduct;

public sealed record GetProductQuery(int ProductId) : IRequest<OperationResult>;