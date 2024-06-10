using Microsoft.EntityFrameworkCore;
using sample_test02.Models;

namespace sample_test02.Context;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<BoatStandard> BoatStandards { get; set; }
    public DbSet<Promotion> Promotions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Reservations)
            .WithOne(r => r.Client)
            .HasForeignKey(r => r.IdClient);

        modelBuilder.Entity<Promotion>()
            .HasMany(p => p.Clients)
            .WithMany(c => c.Promotions);

        base.OnModelCreating(modelBuilder);
    }
}