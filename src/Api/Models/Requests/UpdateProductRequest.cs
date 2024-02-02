namespace Api.Models.Requests;

public sealed record UpdateProductRequest(string Title, double? Price,
    float? Discount, int? AddedNumberToInventoryCount);