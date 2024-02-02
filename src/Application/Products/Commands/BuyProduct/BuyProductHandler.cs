using Application.Constants;
using Application.Models.Operations;
using Domain.Interfaces;
using MediatR;

namespace Application.Products.Commands.BuyProduct;

internal sealed class BuyProductHandler : IRequestHandler<BuyProductCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public BuyProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(BuyProductCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetUserById(request.UserId);

        if (user is null)
        {
            return new OperationResult(OperationResultStatus.NotFound,
                value: ErrorMessages.InvalidUserId);
        }

        var product = await _unitOfWork.Products.GetProductById(request.ProductId);

        if (product is null)
        {
            return new OperationResult(OperationResultStatus.NotFound,
                value: ErrorMessages.ProductNotFound);
        }

        if (product.InventoryCount < request.RequestedProductCount)
        {
            return new OperationResult(OperationResultStatus.Unprocessable,
                value: ErrorMessages.RequestedProductCountIsBiggerThanInventoryCount);
        }

        var order = product.Buy(user, request.RequestedProductCount);

        _unitOfWork.Orders.Add(order);

        _ = await _unitOfWork.CommitAsync();

        var operationValues = new Dictionary<string, object> {
            { OperationValueKeys.ProductId, request.ProductId } };

        return new OperationResult(OperationResultStatus.Ok, value: order,
            operationValues: operationValues);
    }
}