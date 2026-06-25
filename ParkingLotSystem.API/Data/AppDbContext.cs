using Microsoft.EntityFrameworkCore;
using ParkingLotSystem.API.Domain.Entities;
using ParkingLotSystem.API.Domain.Entities.Spots;
using ParkingLotSystem.API.Domain.Entities.Vehicles;
using ParkingLotSystem.API.Domain.Enums;

namespace ParkingLotSystem.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ParkingLot> ParkingLots => Set<ParkingLot>();
    public DbSet<Floor> Floors => Set<Floor>();
    public DbSet<ParkingSpot> ParkingSpots => Set<ParkingSpot>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ParkingTicket> ParkingTickets => Set<ParkingTicket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Motorcycle>("Motorcycle")
            .HasValue<Car>("Car")
            .HasValue<Truck>("Truck");

        modelBuilder.Entity<ParkingSpot>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<CompactSpot>("Compact")
            .HasValue<LargeSpot>("Large")
            .HasValue<HandicappedSpot>("Handicapped");

        modelBuilder.Entity<Floor>()
            .HasOne(f => f.ParkingLot)
            .WithMany(p => p.Floors)
            .HasForeignKey(f => f.ParkingLotId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParkingSpot>()
            .HasOne(s => s.Floor)
            .WithMany(f => f.ParkingSpots)
            .HasForeignKey(s => s.FloorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ParkingTicket>()
            .HasOne(t => t.Vehicle)
            .WithMany(v => v.Tickets)
            .HasForeignKey(t => t.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParkingTicket>()
            .HasOne(t => t.ParkingSpot)
            .WithMany()
            .HasForeignKey(t => t.ParkingSpotId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingLot>().HasData(
            new ParkingLot { Id = 1, Name = "Cairo Mall Parking", Address = "Cairo, Egypt" }
        );

        modelBuilder.Entity<Floor>().HasData(
            new Floor { Id = 1, FloorNumber = 1, ParkingLotId = 1 },
            new Floor { Id = 2, FloorNumber = 2, ParkingLotId = 1 }
        );

        modelBuilder.Entity<CompactSpot>().HasData(
            new { Id = 1, SpotNumber = "F1-C1", FloorId = 1, IsOccupied = false, Type = SpotType.Compact },
            new { Id = 2, SpotNumber = "F1-C2", FloorId = 1, IsOccupied = false, Type = SpotType.Compact },
            new { Id = 6, SpotNumber = "F2-C1", FloorId = 2, IsOccupied = false, Type = SpotType.Compact }
        );

        modelBuilder.Entity<LargeSpot>().HasData(
            new { Id = 3, SpotNumber = "F1-L1", FloorId = 1, IsOccupied = false, Type = SpotType.Large },
            new { Id = 4, SpotNumber = "F1-L2", FloorId = 1, IsOccupied = false, Type = SpotType.Large },
            new { Id = 7, SpotNumber = "F2-L1", FloorId = 2, IsOccupied = false, Type = SpotType.Large },
            new { Id = 8, SpotNumber = "F2-L2", FloorId = 2, IsOccupied = false, Type = SpotType.Large }
        );

        modelBuilder.Entity<HandicappedSpot>().HasData(
            new { Id = 5, SpotNumber = "F1-H1", FloorId = 1, IsOccupied = false, Type = SpotType.Handicapped },
            new { Id = 9, SpotNumber = "F2-H1", FloorId = 2, IsOccupied = false, Type = SpotType.Handicapped },
            new { Id = 10, SpotNumber = "F2-H2", FloorId = 2, IsOccupied = false, Type = SpotType.Handicapped }
        );
    }
}
