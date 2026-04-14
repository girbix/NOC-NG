using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        ViewBag.OpenCount = incidents.Count(i => i.State == "New");
        ViewBag.InProgressCount = incidents.Count(i => i.State == "In Progress");
        ViewBag.CriticalCount = incidents.Count(i => i.Priority == "1 - Critical");
        return View(incidents);
    }

    public IActionResult IncidentDetail(int id)
    {
        var incident = _context.Incidents.FirstOrDefault(i => i.Id == id);
        if (incident == null) return NotFound();
        ViewBag.Technicians = _context.Users.Where(u => u.Role == "Technician").ToList();
        ViewBag.ParentIncidents = _context.Incidents.Where(i => i.Id != id).Select(i => i.Number).ToList();
        ViewBag.ClientTickets = _context.Incidents.Where(i => i.CompanyId == incident.CompanyId && i.Id != id).OrderByDescending(i => i.Opened).Take(5).ToList();
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
        incident.Caller = model.Caller;
        incident.Location = model.Location;
        incident.ServiceOffering = model.ServiceOffering;
        incident.Ci = model.Ci;
        incident.Contract = model.Contract;
        incident.ParentIncident = model.ParentIncident;
        incident.AssignmentGroup = model.AssignmentGroup;
        incident.Channel = model.Channel ?? "Self-Service";

        if (incident.State == "Resolved" && incident.ClosedDate == null)
            incident.ClosedDate = DateTime.Now;
        if (incident.State == "Closed" && incident.ClosedDate == null)
            incident.ClosedDate = DateTime.Now;

        _context.SaveChanges();
        AddAuditLog(User.Identity!.Name!, "UpdateIncident", $"Aggiornato incidente {incident.Number}");
        return RedirectToAction("Dashboard");
    }

    public IActionResult ChangeDetail(int id)
    {
        var change = _context.ChangeRequests.FirstOrDefault(c => c.Id == id);
        if (change == null) return NotFound();
        return View(change);
    }

    [HttpPost]
    public IActionResult UpdateChange(ChangeRequest model)
    {
        var change = _context.ChangeRequests.FirstOrDefault(c => c.Id == model.Id);
        if (change == null) return NotFound();

        change.Justification = model.Justification;
        change.ImplementationPlan = model.ImplementationPlan;
        change.RiskAnalysis = model.RiskAnalysis;
        change.WorkNotes = model.WorkNotes;
        change.TicketGrade = model.TicketGrade;
        change.Rca = model.Rca;
        change.Risk = model.Risk;
        change.ServiceOffering = model.ServiceOffering;
        change.Ci = model.Ci;
        change.ShortDescription = model.ShortDescription;
        change.Description = model.Description;

        if (change.TicketGrade == "1" && string.IsNullOrEmpty(change.Rca))
        {
            ModelState.AddModelError("Rca", "RCA obbligatoria per grado 1");
            return View(change);
        }

        _context.SaveChanges();
        AddAuditLog(User.Identity!.Name!, "UpdateChange", $"Aggiornata change {change.Number}");
        return RedirectToAction("Dashboard");
    }

    private void AddAuditLog(string username, string action, string details)
    {
        var log = new AuditLog { Timestamp = DateTime.Now, Username = username, Action = action, Details = details };
        _context.AuditLogs.Add(log);
        _context.SaveChanges();
    }
}