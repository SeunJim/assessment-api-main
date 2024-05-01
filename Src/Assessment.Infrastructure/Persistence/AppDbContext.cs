using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

     
        public virtual async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
       
        public DbSet<Domain.Entities.Document> Documents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}