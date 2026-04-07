using NOC_NG.Models;

namespace NOC_NG.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Companies.Any())
        {
            context.Companies.AddRange(
                new Company { Id = 1, Name = "TechInnovate" },
                new Company { Id = 2, Name = "Global Logistics" },
                new Company { Id = 3, Name = "FinanzaSicura" },
                new Company { Id = 4, Name = "EcoEnergy" }
            );
        }

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                // Clienti (Tabella 4.4 tesi)
                new ApplicationUser { Email = "client_ti", Password = "Pass@1234", Role = "Client", FullName = "Mario Tech", CompanyId = 1 },
                new ApplicationUser { Email = "client_gl", Password = "Pass@1234", Role = "Client", FullName = "Laura Log", CompanyId = 2 },
                new ApplicationUser { Email = "client_fs", Password = "Pass@1234", Role = "Client", FullName = "Giuseppe Fin", CompanyId = 3 },
                new ApplicationUser { Email = "client_ee", Password = "Pass@1234", Role = "Client", FullName = "Francesca Eco", CompanyId = 4 },
                // Tecnici Network
                new ApplicationUser { Email = "net_l1", Password = "Tech@1234", Role = "Technician", FullName = "Mario Rossi", Team = "Network", Level = "L1" },
                new ApplicationUser { Email = "net_l2", Password = "Tech@1234", Role = "Technician", FullName = "Anna Gialli", Team = "Network", Level = "L2" },
                new ApplicationUser { Email = "net_l3", Password = "Tech@1234", Role = "Technician", FullName = "Chiara Rosa", Team = "Network", Level = "L3" },
                // Cloud
                new ApplicationUser { Email = "cloud_l1", Password = "Tech@1234", Role = "Technician", FullName = "Giulia Verdi", Team = "Cloud", Level = "L1" },
                new ApplicationUser { Email = "cloud_l2", Password = "Tech@1234", Role = "Technician", FullName = "Marco Blu", Team = "Cloud", Level = "L2" },
                new ApplicationUser { Email = "cloud_l3", Password = "Tech@1234", Role = "Technician", FullName = "Andrea Grigio", Team = "Cloud", Level = "L3" },
                // Cyber Security
                new ApplicationUser { Email = "cyber_l1", Password = "Tech@1234", Role = "Technician", FullName = "Paolo Neri", Team = "Cyber Security", Level = "L1" },
                new ApplicationUser { Email = "cyber_l2", Password = "Tech@1234", Role = "Technician", FullName = "Stefano Arancio", Team = "Cyber Security", Level = "L2" },
                new ApplicationUser { Email = "cyber_l3", Password = "Tech@1234", Role = "Technician", FullName = "Elena Marrone", Team = "Cyber Security", Level = "L3" },
                // Dispatcher
                new ApplicationUser { Email = "dispatcher", Password = "Tech@1234", Role = "Technician", FullName = "Dispatcher NOC", Team = "Dispatcher", Level = "L1" },
                // Admin
                new ApplicationUser { Email = "admin", Password = "Admin@1234", Role = "Admin", FullName = "System Administrator" }
            );
        }

        context.SaveChanges();
    }
}
