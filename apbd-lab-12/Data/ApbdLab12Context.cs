using apbd_lab_12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_lab_12.Data;

public class ApbdLab12Context
    : DbContext
{
    public ApbdLab12Context(DbContextOptions<ApbdLab12Context> options)
        : base(options)
    {
    }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<ClientTrip> ClientTrips { get; set; }
    public DbSet<CountryTrip> TripCountries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // client trip
        modelBuilder.Entity<ClientTrip>()
            .HasKey(ct => new { ct.IdClient, ct.IdTrip });

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Client)
            .WithMany(c => c.ClientTrips)
            .HasForeignKey(ct => ct.IdClient);

        modelBuilder.Entity<ClientTrip>()
            .HasOne(ct => ct.Trip)
            .WithMany(t => t.ClientTrips)
            .HasForeignKey(ct => ct.IdTrip);

        // trip country
        modelBuilder.Entity<CountryTrip>()
            .HasKey(tc => new { tc.IdTrip, tc.IdCountry });

        modelBuilder.Entity<CountryTrip>()
            .HasOne(tc => tc.Trip)
            .WithMany(t => t.TripCountries)
            .HasForeignKey(tc => tc.IdTrip);

        modelBuilder.Entity<CountryTrip>()
            .HasOne(tc => tc.Country)
            .WithMany(c => c.TripCountries)
            .HasForeignKey(tc => tc.IdCountry);
    }
}
