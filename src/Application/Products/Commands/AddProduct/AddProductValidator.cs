using Application.Constants;
using Domain;
using FluentValidation;

namespace Application.Products.Commands.AddProduct;

public sealed class AddProductValidator : AbstractValidator<AddProductCommand>
{
    public AddProductValidator()
    {
        RuleFor(x => x.Title)
            .Length(DomainConstants.ProductTitleMinLength, DomainConstants.ProductTitleMaxLength)
            .WithMessage(ErrorMessages.TitleLengthValidation);
    }
}