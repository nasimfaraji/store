namespace Api.Models.Requests;

public record BuyProductRequest(int UserId, int RequestedProductCount);