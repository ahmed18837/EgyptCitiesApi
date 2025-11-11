using EgyptCitiesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EgyptCitiesApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Common Sequence for IDs
            modelBuilder.HasSequence<int>("CommonSequence", schema: "dbo")
                .StartsAt(100)
                .IncrementsBy(5);

            modelBuilder.Entity<City>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");
          
            modelBuilder.Entity<Governorate>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.CommonSequence");

            // One Governorate to Many Cities
            modelBuilder.Entity<City>()
                .HasOne(c => c.Governorate)
                .WithMany(g => g.Cities)   
                .HasForeignKey(c => c.GovernorateId)
                .OnDelete(DeleteBehavior.Cascade);            
        }
    }
}
