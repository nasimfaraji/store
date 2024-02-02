using Application.Constants;
using Application.Models.Operations;
using Domain.Interfaces;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

internal sealed class UpdateProductHandler : IRequestHandler<UpdateProductCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetProductById(request.ProductId);

        if (product is null)
        {
            return new OperationResult(OperationResultStatus.NotFound,
                value: ErrorMessages.ProductNotFound);
        }

        if (!string.IsNullOrWhiteSpace(request.Title) &&
            request.Title.ToUpper() != product.Title.ToUpper())
        {
            var isTitleUsed = await _unitOfWork.Products.CheckExistanceByTitle(request.Title);

            if (isTitleUsed)
            {
                return new OperationResult(OperationResultStatus.Unprocessable,
                    value: ErrorMessages.TitleIsUsedBefore);
            }

            product.UpdateTitle(request.Title);
        }

        if (request.Price.HasValue)
            product.UpdatePrice(request.Price.Value);

        if (request.Discount.HasValue)
            product.UpdateDiscount(request.Discount.Value);

        if (request.AddedNumberToInventoryCount.HasValue)
            product.IncreaseInventoryCount(request.AddedNumberToInventoryCount.Value);

        _unitOfWork.Products.Update(product);

        await _unitOfWork.CommitAsync();

        var operationValues = new Dictionary<string, object> {
            { OperationValueKeys.ProductId, request.ProductId } };

        return new OperationResult(OperationResultStatus.Ok, value: product,
            operationValues: operationValues);
    }
}