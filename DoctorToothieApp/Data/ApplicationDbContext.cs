using DoctorToothieApp.DbModels;
using DoctorToothieApp.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorToothieApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options), IDbContext
{
    public DbSet<ProcedureType> ProcedureTypes { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasOne<User>(e => e.Parent)
            .WithMany(e => e.Children)
            .HasForeignKey(e => e.ParentId);

        builder.Entity<Room>()
            .HasOne<Location>(e => e.Parent)
            .WithMany(e => e.Rooms)
            .HasForeignKey(e => e.ParentId);

        builder.Entity<User>()
            .HasOne<Location>(e => e.EmployeedLocation)
            .WithMany(e => e.Employees)
            .HasForeignKey(e => e.EmployeedLocationId);

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
