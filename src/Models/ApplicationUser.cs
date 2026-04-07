namespace NOC_NG.Models;

public class ApplicationUser
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int? CompanyId { get; set; }
    public string? Team { get; set; }
    public string? Level { get; set; }
}
