namespace Domain.Entities;

public class Product : Entity
{
    private Product(string title, int inventoryCount, double price, float discount)
    {
        Title = title;
        InventoryCount = inventoryCount;
        Price = price;
        Discount = discount;
    }

    public string Title { get; private set; }
    public int InventoryCount { get; private set; }
    public double Price { get; private set; }
    public float Discount { get; private set; }

    public static Product Create(string title, double price, float discount)
    {
        if (title?.Length is < DomainConstants.ProductTitleMinLength or > DomainConstants.ProductTitleMaxLength)
            throw new InvalidDataException("The product title length is invalid.");

        return new Product(
            title: title,
            inventoryCount: 0,
            price: price,
            discount: discount);
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public void UpdatePrice(double price)
    {
        Price = price;
    }

    public void UpdateDiscount(float discount)
    {
        Discount = discount;
    }

    public void IncreaseInventoryCount(int addedNumberToInventoryCount)
    {
        if (addedNumberToInventoryCount < 1)
            throw new InvalidDataException("The number entered to increase the inventory count must be greater than zero");

        InventoryCount += addedNumberToInventoryCount;
    }

    public Order Buy(User user, int requestedProductCount)
    {
        if (requestedProductCount is < 1)
            throw new InvalidDataException("The requested product count must be greater than zero.");

        if (requestedProductCount > InventoryCount)
            throw new InvalidDataException("The requested product count must be less than the inventory count.");

        InventoryCount -= requestedProductCount;

        var order = new Order(Id, user.Id);

        return order;
    }
}