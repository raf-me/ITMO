using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class IdentityDBContext : DbContext
{
    public IdentityDBContext(DbContextOptions<IdentityDBContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.RoleId);
            entity.HasIndex(r => r.Name).IsUnique();
            entity.Property(r => r.Name).HasMaxLength(50).IsRequired();
            entity.Property(r => r.Description).HasMaxLength(255).IsRequired();

            entity.HasData(
                new Role { RoleId = 1, Name = "Admin", Description = "Администратор системы" },
                new Role { RoleId = 2, Name = "Client", Description = "Потребитель электроэнергии" }
            );
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);

            entity.HasIndex(u => u.Login).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Phone).IsUnique();

            entity.Property(u => u.Login).HasMaxLength(50).IsRequired();
            entity.Property(u => u.Email).HasMaxLength(255).IsRequired();
            entity.Property(u => u.Phone).HasMaxLength(20).IsRequired();
            entity.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();
            entity.Property(u => u.Status).HasConversion<string>().HasMaxLength(20);
            entity.Property(u => u.TwoFactorDeliveryChannel).HasConversion<string>().HasMaxLength(10);
            entity.Property(u => u.TwoFactorSecret).HasMaxLength(64).IsRequired(false);

            entity.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
