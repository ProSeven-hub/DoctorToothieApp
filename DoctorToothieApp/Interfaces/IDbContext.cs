using DoctorToothieApp.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DoctorToothieApp.Interfaces;

public interface IDbContext
{
    DbSet<ProcedureType> ProcedureTypes { get; }
    DbSet<User> Users { get; }
    DbSet<Room> Rooms { get; }
    DbSet<Location> Locations { get; }
}
