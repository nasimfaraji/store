using Application.Constants;
using Domain;
using FluentValidation;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Title)
            .Length(DomainConstants.ProductTitleMinLength, DomainConstants.ProductTitleMaxLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Title))
            .WithMessage(ErrorMessages.TitleLengthValidation);

        RuleFor(x => x.AddedNumberToInventoryCount)
            .Must(x => x.Value > 0)
            .When(x => x.AddedNumberToInventoryCount.HasValue)
            .WithMessage(ErrorMessages.AddedNumberToInventoryCountMustBeGreatedThanZero);
    }
}