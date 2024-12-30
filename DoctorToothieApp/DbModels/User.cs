using Microsoft.AspNetCore.Identity;

namespace DoctorToothieApp.DbModels;

public class User: IdentityUser
{    
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;


    public int? EmployeedLocationId { get; set; }
    public Location? EmployeedLocation { get; set; }


    public string? ParentId { get; set; }
    public User? Parent { get; set; }
    public IList<User> Children { get; set; } = [];
    public IList<Reservation> Reservations { get; set; } = [];
}
