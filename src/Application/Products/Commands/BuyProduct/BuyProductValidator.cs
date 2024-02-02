using Application.Constants;
using FluentValidation;

namespace Application.Products.Commands.BuyProduct;

public sealed class BuyProductValidator : AbstractValidator<BuyProductCommand>
{
    public BuyProductValidator()
    {
        RuleFor(x => x.ProductId)
            .Must(x => x > 0)
            .WithMessage(ErrorMessages.InvalidProductId);

        RuleFor(x => x.UserId)
            .Must(x => x > 0)
            .WithMessage(ErrorMessages.InvalidUserId);

        RuleFor(x => x.RequestedProductCount)
            .Must(x => x > 0)
            .WithMessage(ErrorMessages.InvalidRequestedProductCount);
    }
}