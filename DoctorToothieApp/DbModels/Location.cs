namespace DoctorToothieApp.DbModels;

public class Location { 

    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public IList<Room> Rooms { get; set; } = [];
    public IList<User> Employees { get; set; } = [];

}