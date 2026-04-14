using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NOC_NG.Data;
using NOC_NG.Models;

namespace NOC_NG.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        var incidents = _context.Incidents.IgnoreQueryFilters().Include(i => i.Company).ToList();
        ViewBag.TotalIncidents = incidents.Count;
        ViewBag.OpenIncidents = incidents.Count(i => i.State == "New");
        ViewBag.InProgressIncidents = incidents.Count(i => i.State == "In Progress");
        return View(incidents);
    }

    public IActionResult IncidentDetail(int id)
    {
        var incident = _context.Incidents.IgnoreQueryFilters().FirstOrDefault(i => i.Id == id);
        if (incident == null) return NotFound();
        return View("~/Views/Tech/IncidentDetail.cshtml", incident);
    }
}