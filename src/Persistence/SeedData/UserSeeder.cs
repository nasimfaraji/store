using Domain.Entities;

namespace Persistence.SeedData;

public static class UserSeeder
{
    public static void Seed(ApplicationDbContext dbContext)
    {
        if (dbContext.Users.Any())
            return;

        var users = new List<User>{
        new User("User1"),
        new User("User2"),
        new User("User3"),
        new User("User4"),
        new User("User5"),
        new User("User6"),
        new User("User7"),
        new User("User8"),
        new User("User9"),
        new User("User10"),
    };

        dbContext.AddRange(users);
        dbContext.SaveChanges();
    }
}