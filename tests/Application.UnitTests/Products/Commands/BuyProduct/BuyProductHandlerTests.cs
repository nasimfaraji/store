using Application.Constants;
using Application.Products.Commands.BuyProduct;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Products.Commands.BuyProduct;

public class BuyProductHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    const string _title = "title";
    const double _price = 1000;
    const float _discount = 10;
    const string _userName = "user";
    public BuyProductHandlerTests()
    {
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new BuyProductCommand(
            ProductId: 1, UserId: 1, RequestedProductCount: 1);

        _unitOfWorkMock
            .Setup(x => x.Users.GetUserById(It.IsAny<int>()))
            .ReturnsAsync((User)null);

        var handler = new BuyProductHandler(_unitOfWorkMock.Object);

        // Act
        var operationResult = await handler.Handle(command, default);

        // Assert
        Assert.False(operationResult.Succeeded);
        Assert.Equal(ErrorMessages.InvalidUserId, operationResult.Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenProductDoesNotExist()
    {
        // Arrange
        var command = new BuyProductCommand(
            ProductId: 1, UserId: 1, RequestedProductCount: 1);

        _unitOfWorkMock
            .Setup(x => x.Users.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(new User(_userName));

        _unitOfWorkMock
            .Setup(x => x.Products.GetProductById(It.IsAny<int>()))
            .ReturnsAsync((Product)null);

        var handler = new BuyProductHandler(_unitOfWorkMock.Object);

        // Act
        var operationResult = await handler.Handle(command, default);

        // Assert
        Assert.False(operationResult.Succeeded);
        Assert.Equal(ErrorMessages.ProductNotFound, operationResult.Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenRequestedProductCountIsGreaterThanInventoryCount()
    {
        // Arrange
        var command = new BuyProductCommand(
            ProductId: 1, UserId: 1, RequestedProductCount: 3);

        _unitOfWorkMock
            .Setup(x => x.Users.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(new User(_userName));

        var product = Product.Create(_title, _price, _discount);
        product.IncreaseInventoryCount(2);

        _unitOfWorkMock
            .Setup(x => x.Products.GetProductById(It.IsAny<int>()))
            .ReturnsAsync(product);

        var handler = new BuyProductHandler(_unitOfWorkMock.Object);

        // Act
        var operationResult = await handler.Handle(command, default);

        // Assert
        Assert.False(operationResult.Succeeded);
        Assert.Equal(ErrorMessages.RequestedProductCountIsBiggerThanInventoryCount, operationResult.Value);
    }


    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAllArgumentsAreValid()
    {
        // Arrange
        var command = new BuyProductCommand(
            ProductId: 1, UserId: 1, RequestedProductCount: 1);

        _unitOfWorkMock
            .Setup(x => x.Users.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(new User(_userName));

        var product = Product.Create(_title, _price, _discount);
        product.IncreaseInventoryCount(2);

        _unitOfWorkMock
            .Setup(x => x.Products.GetProductById(It.IsAny<int>()))
            .ReturnsAsync(product);

        _unitOfWorkMock
            .Setup(x => x.Orders.Add(It.IsAny<Order>()));

        _unitOfWorkMock
            .Setup(x => x.CommitAsync())
            .ReturnsAsync(true);

        var handler = new BuyProductHandler(_unitOfWorkMock.Object);

        // Act
        var operationResult = await handler.Handle(command, default);

        // Assert
        Assert.True(operationResult.Succeeded);
        Assert.IsType<Order>(operationResult.Value);
    }
}