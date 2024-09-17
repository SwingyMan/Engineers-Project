using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Seeder;

public class DbSeeder
{
    private readonly SocialPlatformDbContext _dbContext;

    public DbSeeder(SocialPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        SeedRoles();
    }

    public void SeedRoles()
    {
        if (!_dbContext.Roles.Any())
        {
            IEnumerable<Role> roles = new List<Role>()
            {
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "ADMIN"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "USER"
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "MODERATOR"
                }
            };

            _dbContext.Roles.AddRange(roles);
            _dbContext.SaveChanges();
        }
    }

    public void SeedUsers()
    {
        if (!_dbContext.Users.Any())
        {
            var adminRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "ADMIN");
            var userRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "USER");

            if (adminRole == null || userRole == null)
            {
                throw new Exception("Roles not seeded properly.");
            }

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    RoleId = adminRole.Id,
                    CreatedAt = DateTime.UtcNow,
                    IpOfRegistry = "127.0.0.1"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "user",
                    Email = "user@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("user123"),
                    RoleId = userRole.Id,
                    CreatedAt = DateTime.UtcNow,
                    IpOfRegistry = "127.0.0.1"
                }
            };

            _dbContext.Users.AddRange(users);
            _dbContext.SaveChanges();
        }
    }

}
