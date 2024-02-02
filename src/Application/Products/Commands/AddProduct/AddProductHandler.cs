using Application.Constants;
using Application.Models.Operations;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Products.Commands.AddProduct;

internal sealed class AddProductHandler : IRequestHandler<AddProductCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var isTitleUsed = await _unitOfWork.Products.CheckExistanceByTitle(request.Title);

        if (isTitleUsed)
        {
            return new OperationResult(OperationResultStatus.Unprocessable,
                value: ErrorMessages.TitleIsUsedBefore);
        }

        var product = Product.Create(
            title: request.Title,
            price: request.Price,
            discount: request.Discount);

        _unitOfWork.Products.Add(product);

        await _unitOfWork.CommitAsync();

        return new OperationResult(OperationResultStatus.Ok, value: product);
    }
}