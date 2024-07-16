using Microsoft.EntityFrameworkCore;

namespace ApiSample.Infraestrutura
{
    public class MongoDbContext: DbContext
    {

        public MongoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Customer>().ToCollection("customers");
        }
    }
}
