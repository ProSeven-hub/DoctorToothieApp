namespace DoctorToothieApp.DbModels;

public class Location { 

    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;

    public string? Image { get; set; } = default!;
    public IList<Room> Rooms { get; set; } = [];
    public IList<User> Employees { get; set; } = [];

}