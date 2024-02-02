namespace Domain.Entities;

public class User : Entity
{
    private readonly List<Order> _orders = new();

    public User(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyCollection<Order> Orders => _orders;
}