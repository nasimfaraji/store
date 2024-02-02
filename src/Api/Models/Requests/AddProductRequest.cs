namespace Api.Models.Requests;

public sealed record AddProductRequest(string Title, double Price, float Discount);