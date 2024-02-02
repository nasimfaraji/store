using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly IQueryable<Product> _queryable;

    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _queryable = dbContext.Set<Product>();
    }

    public async Task<Product> GetProductById(int id) =>
        await _queryable.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<bool> CheckExistanceByTitle(string title) =>
        await _queryable.AnyAsync(x => x.Title.ToUpper() == title.ToUpper());
}