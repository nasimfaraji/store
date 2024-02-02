using Domain.Interfaces;
using Persistence.Repositories;

namespace Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private IProductRepository _productRepository;
    private IUserRepository _userRepository;
    private IOrderRepository _orderRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProductRepository Products =>
        _productRepository ??= new ProductRepository(_context);

    public IUserRepository Users =>
        _userRepository ??= new UserRepository(_context);

    public IOrderRepository Orders =>
        _orderRepository ??= new OrderRepository(_context);

    public async Task<bool> CommitAsync() =>
        await _context.SaveChangesAsync() > 0;

    public void Dispose() => _context.Dispose();
}