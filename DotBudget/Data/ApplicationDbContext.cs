using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DotBudget.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
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

            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasMaxLength(450);

                entity.Property(p => p.Name)
                    .HasMaxLength(128)
                    .IsRequired(); 

                entity.Property(p => p.BudgetedAmount)
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.CurrentSpent)
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.ApplicationUserId)
                    .IsRequired();

                entity.HasOne(c => c.ApplicationUser)  
                    .WithMany(u => u.Categories)
                    .HasForeignKey(c => c.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Receipts)
                    .WithOne(r => r.Category)
                    .HasForeignKey(r => r.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipts");
                entity.HasKey(r => r.Id);

                entity.Property(p => p.Id)
                    .HasMaxLength(450);

                entity.Property(r => r.ImagePath)
                    .IsRequired(false);

                entity.Property(r => r.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(r => r.Date)
                    .HasColumnType("datetime");

                entity.Property(r => r.ApplicationUserId)
                    .IsRequired();

                entity.Property(r => r.CategoryId)
                    .IsRequired();

                entity.HasOne(r => r.ApplicationUser)
                    .WithMany(u => u.Receipts)
                    .HasForeignKey(r => r.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Category)
                    .WithMany(c => c.Receipts)
                    .HasForeignKey(r => r.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey<Profile>(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        
        }
    }
}
