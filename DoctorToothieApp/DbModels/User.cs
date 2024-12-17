using Microsoft.AspNetCore.Identity;

namespace DoctorToothieApp.DbModels;

public class User: IdentityUser
{    
    public string FirstName { get; set; }
    public string LastName { get; set; }


    public int? EmployeedLocationId { get; set; }
    public Location? EmployeedLocation { get; set; }


    public string? ParentId { get; set; }
    public User? Parent { get; set; }
    public IList<User> Children { get; set; } = [];
}
