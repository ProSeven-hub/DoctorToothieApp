namespace DoctorToothieApp.DbModels;

public class Room
{
    public int Id { get; set; }
    public string Number { get; set; }

    public int ParentId { get; set; }
    public Location Parent { get; set; }
}