namespace NOC_NG.Models;

public class Incident : Ticket
{
    public string? Category { get; set; }
    public string? Resolution { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string? Caller { get; set; }
    public string? Location { get; set; }
    public string? ServiceOffering { get; set; }
    public string? Ci { get; set; }
    public string? InternalNotes { get; set; }
    public string? Contract { get; set; }
    public string? ParentIncident { get; set; }
    public string? Channel { get; set; }
    public string? AssignmentGroup { get; set; }
}