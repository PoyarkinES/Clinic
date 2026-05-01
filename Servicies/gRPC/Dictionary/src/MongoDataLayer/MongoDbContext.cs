using Microsoft.EntityFrameworkCore;
using MongoDataLayerService.Model;

namespace MongoDataLayerService
{
    public class MongoDbContext : DbContext
    {
        public DbSet<Country> countries { get; init; }

        public MongoDbContext(DbContextOptions<MongoDbContext> options)
            : base(options)
        {
        }
    }
}
