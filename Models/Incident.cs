namespace NOC_NG.Models;

public class Incident : Ticket
{
    public string? Category { get; set; }
    public string? Resolution { get; set; }
    public DateTime? ClosedDate { get; set; }
}
