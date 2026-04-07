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
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            ViewBag.Error = "Credenziali non valide";
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("FullName", user.FullName)
        };
        if (user.CompanyId.HasValue)
            claims.Add(new Claim("CompanyId", user.CompanyId.Value.ToString()));
        if (!string.IsNullOrEmpty(user.Team))
            claims.Add(new Claim("Team", user.Team));
        if (!string.IsNullOrEmpty(user.Level))
            claims.Add(new Claim("Level", user.Level));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyAtLeast32CharsLong!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        Response.Cookies.Append("jwt", tokenString);

        AddAuditLog(user.Email, "Login", $"Accesso effettuato");

        if (user.Role == "Client")
            return RedirectToAction("Dashboard", "Client");
        else
            return RedirectToAction("Dashboard", "Tech");
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return RedirectToAction("Login");
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
