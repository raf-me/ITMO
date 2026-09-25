using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Data;

public class ContractDbContext : DbContext
{
    public ContractDbContext(DbContextOptions<ContractDbContext> options)
        : base(options)
    {
    }

    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Meter> Meters => Set<Meter>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name).IsRequired().HasMaxLength(255);
            entity.Property(x => x.Inn).IsRequired().HasMaxLength(12);
            entity.Property(x => x.Kpp).HasMaxLength(9);
            entity.Property(x => x.Ogrn).HasMaxLength(15);
            entity.Property(x => x.LegalAddress).HasMaxLength(500);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);

            entity.HasIndex(x => x.Inn).IsUnique();
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ContractNumber).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);
            entity.Property(x => x.TariffType).IsRequired().HasMaxLength(50);

            entity.HasIndex(x => x.ContractNumber).IsUnique();

            entity.HasOne(x => x.Organization)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Meter>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.SerialNumber).IsRequired().HasMaxLength(100);
            entity.Property(x => x.MeterType).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Location).HasMaxLength(255);
            entity.Property(x => x.Status).IsRequired().HasMaxLength(50);

            entity.HasIndex(x => x.SerialNumber).IsUnique();

            entity.HasOne(x => x.Contract)
                .WithMany(x => x.Meters)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ContractNumber).IsRequired().HasMaxLength(100);
            entity.Property(x => x.DocumentType).IsRequired().HasMaxLength(50);
            entity.Property(x => x.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(x => x.VerificationStatus).IsRequired().HasMaxLength(50);

            entity.HasOne(x => x.Contract)
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Action).IsRequired().HasMaxLength(100);
            entity.Property(x => x.TargetType).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Description).HasMaxLength(1000);
        });
    }
}