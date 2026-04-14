using Microsoft.EntityFrameworkCore;
using NOC_NG.Models;

namespace NOC_NG.Data;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<ChangeRequest> ChangeRequests => Set<ChangeRequest>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>().HasQueryFilter(e => e.CompanyId == GetCurrentCompanyId());
        modelBuilder.Entity<ChangeRequest>().HasQueryFilter(e => e.CompanyId == GetCurrentCompanyId());
        modelBuilder.Entity<AuditLog>().HasQueryFilter(e => e.CompanyId == GetCurrentCompanyId());
    }

    private int GetCurrentCompanyId()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var claim = user?.FindFirst("CompanyId");
        return claim != null ? int.Parse(claim.Value) : 0;
    }
}