using AutoFixture;
using Domain.Entities;

namespace Domain.UnitTest.Entities;

public class ProductTests
{
    const string _title = "title";
    const double _price = 1000;
    const float _discount = 10;

    private Fixture _fixture;
    public ProductTests() => _fixture = new Fixture();

    [Fact]
    public void Create_ShouldThrowException_WhenTitleLengthIsLessThanMinLength()
    {
        // Arrange
        const string title = "t";

        // Act

        // Assert
        Assert.ThrowsAny<InvalidDataException>(() => Product.Create(title, _price, _discount));
    }

    [Fact]
    public void Create_ShouldThrowException_WhenTitleLengthIsGreaterThanMaxLength()
    {
        // Arrange
        var title = string.Join(string.Empty, _fixture.CreateMany<char>(41));

        // Act

        // Assert
        Assert.ThrowsAny<InvalidDataException>(() => Product.Create(title, _price, _discount));
    }

    [Fact]
    public void Create_ShouldRetunrValidProduct_WhenAllArgumentsAreValid()
    {
        // Arrange

        // Act
        var product = Product.Create(_title, _price, _discount);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(_title, product.Title);
        Assert.Equal(_price, product.Price);
        Assert.Equal(_discount, product.Discount);
        Assert.Equal(0, product.InventoryCount);
    }

    [Fact]
    public void UpdateTitle_ShouldUpdateProductTitle()
    {
        // Arrange
        const string firstTitle = "firstTitle";

        var product = Product.Create(firstTitle, _price, _discount);

        const string newTitle = "newTitle";

        // Act
        product.UpdateTitle(newTitle);

        // Assert
        Assert.NotEqual(firstTitle, product.Title);
        Assert.Equal(newTitle, product.Title);
        Assert.Equal(_price, product.Price);
        Assert.Equal(_discount, product.Discount);
        Assert.Equal(0, product.InventoryCount);
    }

    [Fact]
    public void UpdatePrice_ShouldUpdateProductPrice()
    {
        // Arrange
        const double firstPrice = 10;

        var product = Product.Create(_title, firstPrice, _discount);

        const double newPrice = 20;

        // Act
        product.UpdatePrice(newPrice);

        // Assert
        Assert.NotEqual(firstPrice, product.Price);
        Assert.Equal(newPrice, product.Price);
        Assert.Equal(_title, product.Title);
        Assert.Equal(_discount, product.Discount);
        Assert.Equal(0, product.InventoryCount);
    }

    [Fact]
    public void UpdateDiscount_ShouldUpdateProductDiscount()
    {
        // Arrange
        const float firstDiscount = 10;

        var product = Product.Create(_title, _price, firstDiscount);

        const float newDiscount = 20;

        // Act
        product.UpdateDiscount(newDiscount);

        // Assert
        Assert.NotEqual(firstDiscount, product.Discount);
        Assert.Equal(newDiscount, product.Discount);
        Assert.Equal(_title, product.Title);
        Assert.Equal(_price, product.Price);
        Assert.Equal(0, product.InventoryCount);
    }

    [Fact]
    public void IncreaseInventoryCount_ShouldThrowException_WhenaAdedNumberToInventoryCountIsLessThanOne()
    {
        // Arrange
        const int addedNumberToInventoryCount = 0;

        var product = Product.Create(_title, _price, _discount);

        // Act

        // Assert
        Assert.ThrowsAny<InvalidDataException>(() => product.IncreaseInventoryCount(addedNumberToInventoryCount));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(200)]
    public void IncreaseInventoryCount_ShouldUpdateInventoryCount_WhenaAdedNumberToInventoryCountIsValid(
        int addedNumberToInventoryCount)
    {
        // Arrange
        var product = Product.Create(_title, _price, _discount);

        // Act
        product.IncreaseInventoryCount(addedNumberToInventoryCount);

        // Assert
        Assert.Equal(addedNumberToInventoryCount, product.InventoryCount);
    }

    [Fact]
    public void Buy_ShouldThrowException_WhenRequestedProductCountIsLessThanOne()
    {
        // Arrange
        const int requestedProductCount = 0;

        var product = Product.Create(_title, _price, _discount);

        var user = new User("user");

        // Act

        // Assert
        Assert.ThrowsAny<InvalidDataException>(() => product.Buy(user, requestedProductCount));
    }

    [Fact]
    public void Buy_ShouldThrowException_WhenRequestedProductCountIsGreaterThanInventoryCount()
    {
        // Arrange
        const int requestedProductCount = 11;

        var product = Product.Create(_title, _price, _discount);
        product.IncreaseInventoryCount(10);

        var user = new User("user");

        // Act

        // Assert
        Assert.ThrowsAny<InvalidDataException>(() => product.Buy(user, requestedProductCount));
    }

    [Fact]
    public void Buy_ShouldUpdateInventoryCountAndReturnOrder_WhenRequestedProductCountIsValid()
    {
        // Arrange
        const int requestedProductCount = 8;

        var product = Product.Create(_title, _price, _discount);
        product.IncreaseInventoryCount(10);

        var user = new User("user");

        // Act
        var order = product.Buy(user, requestedProductCount);

        // Assert
        Assert.Equal(2, product.InventoryCount);
        Assert.NotNull(order);
    }
}