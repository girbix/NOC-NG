using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NOC_NG.Data;
using NOC_NG.Models;
using NOC_NG.Services;
using System.Security.Claims;

namespace NOC_NG.Controllers;

[Authorize(Roles = "Client")]
public class ClientController : Controller
{
    private readonly AppDbContext _context;
    private readonly IAiTeamSuggestionService _aiService;

    public ClientController(AppDbContext context, IAiTeamSuggestionService aiService)
    {
        _context = context;
        _aiService = aiService;
    }

    public IActionResult Dashboard()
    {
        var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
        var tickets = _context.Incidents.Where(i => i.CompanyId == companyId).OrderByDescending(i => i.Opened).ToList();
        return View(tickets);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateIncident(Incident model)
    {
        var companyId = int.Parse(User.FindFirst("CompanyId")?.Value ?? "0");
        model.Number = GenerateTicketNumber("INC");
        model.Opened = DateTime.Now;
        model.CompanyId = companyId;
        model.Priority = Ticket.CalculatePriority(model.Urgency, model.Impact);
        model.SuggestedTeam = _aiService.SuggestTeam(model.ShortDescription, model.Description ?? "");
        model.State = "New";
        model.RequestedBy = User.FindFirst(ClaimTypes.Email)?.Value;
        model.Channel = "Self-Service";

        _context.Incidents.Add(model);
        _context.SaveChanges();

        AddAuditLog(User.Identity!.Name!, "CreateIncident", $"Creato incidente {model.Number} - Team suggerito: {model.SuggestedTeam}");
        return RedirectToAction("Dashboard");
    }

    private string GenerateTicketNumber(string prefix) => $"{prefix}-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";

    private void AddAuditLog(string username, string action, string details)
    {
        var log = new AuditLog { Timestamp = DateTime.Now, Username = username, Action = action, Details = details };
        _context.AuditLogs.Add(log);
        _context.SaveChanges();
    }
}