namespace NOC_NG.Services;

public class AiTeamSuggestionService : IAiTeamSuggestionService
{
    public string SuggestTeam(string shortDescription, string description)
    {
        var keywords = new Dictionary<string, string[]>
        {
            ["Network"] = new[] { "router", "switch", "firewall", "vpn", "dhcp", "dns", "rete", "ping", "latenza", "bandwidth" },
            ["Cloud"] = new[] { "aws", "azure", "cloud", "s3", "bucket", "kubernetes", "container", "vm", "virtual machine" },
            ["Cyber Security"] = new[] { "virus", "malware", "ransomware", "phishing", "breach", "vulnerability", "attacco", "hacker" }
        };

        var text = (shortDescription + " " + description).ToLower();
        foreach (var (team, teamKeywords) in keywords)
        {
            if (teamKeywords.Any(k => text.Contains(k)))
                return team;
        }
        return "Network";
    }
}
