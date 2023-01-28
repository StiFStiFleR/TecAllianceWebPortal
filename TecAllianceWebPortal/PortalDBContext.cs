using Microsoft.EntityFrameworkCore;
using TecAllianceWebPortal.Model;

namespace TecAllianceWebPortal
{
    public class PortalDBContext : DbContext
    {
        public PortalDBContext(DbContextOptions<PortalDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(f => f.Notes).WithOne(f => f.User).HasForeignKey(f => f.UserId);
            modelBuilder.Entity<User>(f =>
            {
                f.Property(t => t.Email).IsRequired().HasMaxLength(100);
                f.HasAlternateKey(t => t.Email);
                f.HasIndex(t => t.Email);

            });
            modelBuilder.Entity<Note>(f => f.Property(f => f.Description).IsRequired().HasMaxLength(250));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
