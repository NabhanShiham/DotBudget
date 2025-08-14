using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DotBudget.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Profile> Profiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");  
                entity.HasKey(p => p.Id);    

                entity.Property(p => p.Name)
                    .HasMaxLength(128)       
                    .IsRequired(false);      

                entity.Property(p => p.MainBudget)
                    .HasColumnType("decimal(18,2)");  

                entity.Property(p => p.CurrentSpending)
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.ApplicationUserId)
                    .IsRequired();          
            });

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey<Profile>(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
