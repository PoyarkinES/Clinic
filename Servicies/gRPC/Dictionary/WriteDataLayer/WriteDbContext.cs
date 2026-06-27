using Microsoft.EntityFrameworkCore;
using WriteDataLayer.Model;

namespace WriteDataLayer
{
    public class WriteDbContext : DbContext
    {
        public virtual DbSet<Country> Countries { get; init; }

        public WriteDbContext(DbContextOptions<WriteDbContext> options)
            : base(options)
        {
        }
    }
}
