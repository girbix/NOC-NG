using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NOC_NG.Data;
using NOC_NG.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NOC_NG.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Verifica utente (demo: confronto hardcoded - in produzione usare Identity)
        var user = _context.Users.FirstOrDefault(u => u.Email == username && u.Password == password);
        if (user == null)
        {
            ViewBag.Error = "Credenziali non valide";
            return View();
        }

        // Genera JWT
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("CompanyId", user.CompanyId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyAtLeast32CharsLong!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        Response.Cookies.Append("jwt", tokenString);

        // Audit log
        AddAuditLog(user.Email, "Login", $"Accesso effettuato da IP {HttpContext.Connection.RemoteIpAddress}");

        return user.Role == "Client" ? RedirectToAction("Dashboard", "Client") : RedirectToAction("Dashboard", "Tech");
    }

    private void AddAuditLog(string username, string action, string details)
    {
        var log = new AuditLog
        {
            Timestamp = DateTime.Now,
            Username = username,
            Action = action,
            Details = details
        };
        _context.AuditLogs.Add(log);
        _context.SaveChanges();
    }
}
