using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    private readonly IQueryable<User> _queryable;

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _queryable = dbContext.Set<User>();
    }

    public async Task<User> GetUserById(int id) =>
        await _queryable.SingleOrDefaultAsync(x => x.Id == id);
}