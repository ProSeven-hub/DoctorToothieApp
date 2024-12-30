using Microsoft.AspNetCore.Identity;

namespace DoctorToothieApp.DbModels;

public enum ReservationStage: int
{
    LOCATION,
    ROOM,
    PATIENT,
    DOCTOR,
    PROCEDURE,
    DATE,
    REVIEW,
    COMPLETED,
    CANCELED
}

public class Reservation
{
    public int Id { get; set; }
    public ReservationStage Stage { get; set; }
    public int Status { get; set; }
    public User? Patient { get; set; }
    public string? PatientId { get; set; }
    public Location? Location { get; set; }
    public int? LocationId { get; set; }
    public Room? Room { get; set; }
    public int? RoomId { get; set; }
    public User? Doctor { get; set; }
    public string? DoctorId { get; set; }
    public ProcedureType? ProcedureType { get; set; }
    public int? ProcedureTypeId { get; set; }
    public DateTime? Time{ get; set; }
    public string? ProcedureNotes { get; set; }
}
