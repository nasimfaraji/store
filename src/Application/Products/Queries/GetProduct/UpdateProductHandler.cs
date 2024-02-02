using Application.Constants;
using Application.Helpers;
using Application.Models.Operations;
using Application.Models.Produtcts;
using Domain.Interfaces;
using MediatR;

namespace Application.Products.Queries.GetProduct;

internal sealed class GetProductHandler : IRequestHandler<GetProductQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetProductById(request.ProductId);

        if (product is null)
        {
            return new OperationResult(OperationResultStatus.NotFound,
                value: ErrorMessages.ProductNotFound);
        }

        var result = new ProductModel(
            ProductId: product.Id,
            Title: product.Title,
            InventoryCount: product.InventoryCount,
            SalePrice: product.Price,
            Discount: product.Discount,
            DiscountedPrice: ProductHelper.CalculateDiscountedPrice(product.Price, product.Discount));

        return new OperationResult(OperationResultStatus.Ok, value: result);
    }
}