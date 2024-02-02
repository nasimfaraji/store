using Domain;

namespace Application.Constants;

public static class ErrorMessages
{
    public static string TitleLengthValidation =
        @$"The product title length must be between {DomainConstants.ProductTitleMinLength} and {DomainConstants.ProductTitleMaxLength}.";

    public static string TitleIsUsedBefore = "The title you've chosen is already used for another product.";

    public static string AddedNumberToInventoryCountMustBeGreatedThanZero =
        "To increase the inventory count, the number entered must be greater than zero.";

    public static string ProductNotFound =
        "The product you have selected does not exist in our system. Please try again with a valid product.";

    public static string InvalidProductId = "The product you have selected is not valid.";

    public static string InvalidUserId = "The user is not valid.";

    public static string InvalidRequestedProductCount =
        "The requested product count must be bigger than zero.";

    public static string RequestedProductCountIsBiggerThanInventoryCount =
        "You cannot purchase a quantity greater than what is currently available for the product.";
}