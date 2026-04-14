namespace NOC_NG.Models;

public abstract class Ticket
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Opened { get; set; } = DateTime.Now;
    public string State { get; set; } = "New";
    public string Priority { get; set; } = "Low";
    public int CompanyId { get; set; }
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? AssignedTo { get; set; }
    public string? RequestedBy { get; set; }
    public string? AssignedTeam { get; set; }
    public string? SuggestedTeam { get; set; }
    public string Urgency { get; set; } = "Low";
    public string Impact { get; set; } = "Low";
    public Company? Company { get; set; }

    public static string CalculatePriority(string urgency, string impact)
    {
        if (urgency == "High" && impact == "High") return "1 - Critical";
        if (urgency == "High" || impact == "High") return "2 - High";
        if (urgency == "Medium" && impact == "Medium") return "2 - High";
        return "3 - Low";
    }
}