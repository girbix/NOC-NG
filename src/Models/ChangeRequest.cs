namespace NOC_NG.Models;

public class ChangeRequest : Ticket
{
    public string? Justification { get; set; }
    public string? ImplementationPlan { get; set; }
    public string? RiskAnalysis { get; set; }
    public string? TicketGrade { get; set; }
    public string? Rca { get; set; }
    public DateTime? ChangeWindow { get; set; }
    public bool IsApproved { get; set; } = false;
}
