using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NOC_NG.Data;
using NOC_NG.Models;
using System.Security.Claims;

namespace NOC_NG.Controllers;

[Authorize(Roles = "Technician,Admin")]
public class TechController : Controller
{
    private readonly AppDbContext _context;

    public TechController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        var userTeam = User.FindFirst("Team")?.Value;
        var isDispatcher = userTeam == "Dispatcher";
        List<Incident> incidents;
        if (isDispatcher)
            incidents = _context.Incidents.ToList();
        else
            incidents = _context.Incidents.Where(i => i.AssignedTeam == userTeam).ToList();

        return View(incidents);
    }

    public IActionResult IncidentDetail(int id)
    {
        var incident = _context.Incidents.FirstOrDefault(i => i.Id == id);
        if (incident == null) return NotFound();
        return View(incident);
    }

    [HttpPost]
    public IActionResult UpdateIncident(Incident model)
    {
        var incident = _context.Incidents.FirstOrDefault(i => i.Id == model.Id);
        if (incident == null) return NotFound();

        incident.State = model.State;
        incident.Impact = model.Impact;
        incident.Urgency = model.Urgency;
        incident.Priority = Ticket.CalculatePriority(incident.Urgency, incident.Impact);
        incident.AssignedTeam = model.AssignedTeam;
        incident.AssignedTo = model.AssignedTo;
        incident.ShortDescription = model.ShortDescription;
        incident.Description = model.Description;
        incident.InternalNotes = model.InternalNotes;

        _context.SaveChanges();
        AddAuditLog(User.Identity!.Name!, "UpdateIncident", $"Aggiornato incidente {incident.Number}");
        return RedirectToAction("Dashboard");
    }

    // ChangeDetail, UpdateChange simili...

    private void AddAuditLog(string username, string action, string details)
    {
        var log = new AuditLog { Timestamp = DateTime.Now, Username = username, Action = action, Details = details };
        _context.AuditLogs.Add(log);
        _context.SaveChanges();
    }
}
