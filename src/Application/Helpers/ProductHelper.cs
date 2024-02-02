namespace Application.Helpers;

public static class ProductHelper
{
    private const int FractionalNumberDigits = 2;
    private const int OneHundredPercent = 100;

    public static double CalculateDiscountedPrice(double salePrice, float discount)
    {
        if (discount == 0)
            return salePrice;

        var discountedPrice = salePrice - (salePrice * discount / OneHundredPercent);

        if (discountedPrice < 0)
            discountedPrice = 0;

        return Math.Round(discountedPrice, FractionalNumberDigits);
    }
}