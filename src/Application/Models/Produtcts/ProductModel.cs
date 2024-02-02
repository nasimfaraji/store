namespace Application.Models.Produtcts;

public record ProductModel(int ProductId, string Title, int InventoryCount,
    double SalePrice, float Discount, double DiscountedPrice);