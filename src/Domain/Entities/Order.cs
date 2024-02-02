namespace Domain.Entities;

public class Order : Entity
{
    internal Order(int productId, int userId)
    {
        ProductId = productId;
        UserId = userId;
        CreationDate = DateTime.UtcNow;
    }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public DateTime CreationDate { get; private set; }
    public int UserId { get; private set; }
    public User Buyer { get; private set; }
}