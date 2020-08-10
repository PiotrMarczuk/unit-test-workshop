using Microsoft.EntityFrameworkCore;

using UnitTestMockingExamples.Database.DbModels;

namespace UnitTestMockingExamples.Database.Context
{
    public class SimpleDbContext : DbContext
    {
        public virtual DbSet<SimpleDbModel> SimpleDbModels { get; set; }

        public SimpleDbContext(DbContextOptions<SimpleDbContext> options) : base(options)
        { }
    }
}
