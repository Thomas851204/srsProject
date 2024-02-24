using Microsoft.EntityFrameworkCore;

namespace srsProject.Models
{
    public class srsProjectContext : DbContext
    {
        public srsProjectContext(DbContextOptions options) : base(options)
        {
        
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Ownership> Ownerships { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ownership>()
                        .HasOne(o => o.Owner)
                        .WithMany()
                        .HasForeignKey(o => o.OwnerId);

            modelBuilder.Entity<Ownership>()
                        .HasOne(o => o.Car)
                        .WithMany()
                        .HasForeignKey(o => o.CarId);
        }
    }
}

