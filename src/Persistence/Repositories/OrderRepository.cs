using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.Repositories;

public sealed class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly IQueryable<Order> _queryable;

    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _queryable = dbContext.Set<Order>();
    }
}