using Microsoft.EntityFrameworkCore;
using TradesCore_API.Entities;

namespace TradesCore_API.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
