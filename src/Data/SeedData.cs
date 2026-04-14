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
                new ApplicationUser { Email = "client_ti", Password = "Pass@1234", Role = "Client", FullName = "Mario Tech", CompanyId = 1 },
                new ApplicationUser { Email = "client_gl", Password = "Pass@1234", Role = "Client", FullName = "Laura Log", CompanyId = 2 },
                new ApplicationUser { Email = "client_fs", Password = "Pass@1234", Role = "Client", FullName = "Giuseppe Fin", CompanyId = 3 },
                new ApplicationUser { Email = "client_ee", Password = "Pass@1234", Role = "Client", FullName = "Francesca Eco", CompanyId = 4 },
                new ApplicationUser { Email = "net_l1", Password = "Tech@1234", Role = "Technician", FullName = "Mario Rossi", Team = "Network", Level = "L1" },
                new ApplicationUser { Email = "net_l2", Password = "Tech@1234", Role = "Technician", FullName = "Anna Gialli", Team = "Network", Level = "L2" },
                new ApplicationUser { Email = "net_l3", Password = "Tech@1234", Role = "Technician", FullName = "Chiara Rosa", Team = "Network", Level = "L3" },
                new ApplicationUser { Email = "cloud_l1", Password = "Tech@1234", Role = "Technician", FullName = "Giulia Verdi", Team = "Cloud", Level = "L1" },
                new ApplicationUser { Email = "cloud_l2", Password = "Tech@1234", Role = "Technician", FullName = "Marco Blu", Team = "Cloud", Level = "L2" },
                new ApplicationUser { Email = "cloud_l3", Password = "Tech@1234", Role = "Technician", FullName = "Andrea Grigio", Team = "Cloud", Level = "L3" },
                new ApplicationUser { Email = "cyber_l1", Password = "Tech@1234", Role = "Technician", FullName = "Paolo Neri", Team = "Cyber Security", Level = "L1" },
                new ApplicationUser { Email = "cyber_l2", Password = "Tech@1234", Role = "Technician", FullName = "Stefano Arancio", Team = "Cyber Security", Level = "L2" },
                new ApplicationUser { Email = "cyber_l3", Password = "Tech@1234", Role = "Technician", FullName = "Elena Marrone", Team = "Cyber Security", Level = "L3" },
                new ApplicationUser { Email = "dispatcher", Password = "Tech@1234", Role = "Technician", FullName = "Dispatcher NOC", Team = "Dispatcher", Level = "L1" },
                new ApplicationUser { Email = "admin", Password = "Admin@1234", Role = "Admin", FullName = "System Administrator" }
            );
        }

        if (!context.Incidents.Any())
        {
            context.Incidents.AddRange(
                new Incident { Number = "INC000001", Opened = DateTime.Now.AddDays(-5), State = "Resolved", Priority = "2 - High", CompanyId = 1, ShortDescription = "Problema di connessione VPN", Description = "Impossibile connettersi alla VPN aziendale da remoto", Urgency = "High", Impact = "Medium", RequestedBy = "client_ti", Caller = "Mario Tech", Location = "Torino", ServiceOffering = "VPN Access", Ci = "VPN-GW-01", AssignedTeam = "Network", AssignedTo = "Mario Rossi", SuggestedTeam = "Network", Contract = "SLA-GOLD", ClosedDate = DateTime.Now.AddDays(-1), Resolution = "Riavvio del gateway VPN", InternalNotes = "Ticket risolto dal L1 Network" },
                new Incident { Number = "INC000002", Opened = DateTime.Now.AddDays(-2), State = "In Progress", Priority = "1 - Critical", CompanyId = 1, ShortDescription = "Server di produzione down", Description = "Il server SVR-PROD-01 non risponde ai ping", Urgency = "High", Impact = "High", RequestedBy = "client_ti", Caller = "Laura Bianchi", Location = "Milano", ServiceOffering = "IaaS", Ci = "SVR-PROD-01", AssignedTeam = "Cloud", AssignedTo = "Marco Blu", SuggestedTeam = "Cloud", Contract = "SLA-PLATINUM", InternalNotes = "In attesa di reboot remoto" },
                new Incident { Number = "INC000006", Opened = DateTime.Now.AddDays(-1), State = "New", Priority = "3 - Low", CompanyId = 1, ShortDescription = "Richiesta nuova mailbox", Description = "Creare mailbox per nuovo assunto", Urgency = "Low", Impact = "Low", RequestedBy = "client_ti", Caller = "HR TechInnovate", Location = "Torino", ServiceOffering = "Email Service", Ci = "EXCH-01", AssignedTeam = null, SuggestedTeam = "Network", Contract = "SLA-BRONZE", InternalNotes = "" },
                new Incident { Number = "INC000003", Opened = DateTime.Now.AddDays(-3), State = "In Progress", Priority = "2 - High", CompanyId = 2, ShortDescription = "Stampa etichette lenta", Description = "La stampante delle etichette impiega troppo tempo", Urgency = "Medium", Impact = "High", RequestedBy = "client_gl", Caller = "Laura Log", Location = "Bra", ServiceOffering = "Fleet Tracking", Ci = "PRN-LOG-01", AssignedTeam = "Network", AssignedTo = "Anna Gialli", SuggestedTeam = "Network", Contract = "SLA-SILVER", InternalNotes = "Sostituito cavo di rete, monitorare" },
                new Incident { Number = "INC000007", Opened = DateTime.Now.AddDays(-4), State = "Resolved", Priority = "3 - Low", CompanyId = 2, ShortDescription = "Aggiornamento app magazzino", Description = "Richiesta di aggiornamento app per scanner", Urgency = "Low", Impact = "Medium", RequestedBy = "client_gl", Caller = "Magazzino", Location = "Novara", ServiceOffering = "Mobile App", Ci = "SCAN-01", AssignedTeam = "Cloud", AssignedTo = "Giulia Verdi", SuggestedTeam = "Cloud", Contract = "SLA-BRONZE", ClosedDate = DateTime.Now.AddDays(-1), Resolution = "App aggiornata con successo" },
                new Incident { Number = "INC000008", Opened = DateTime.Now, State = "New", Priority = "2 - High", CompanyId = 2, ShortDescription = "VPN non funzionante su sede di Bra", Description = "Diversi utenti segnalano impossibilità a connettersi", Urgency = "High", Impact = "Medium", RequestedBy = "client_gl", Caller = "Ufficio Tecnico", Location = "Bra", ServiceOffering = "VPN Access", Ci = "VPN-GW-02", AssignedTeam = null, SuggestedTeam = "Network", Contract = "SLA-GOLD", InternalNotes = "" },
                new Incident { Number = "INC000004", Opened = DateTime.Now.AddDays(-6), State = "Closed", Priority = "1 - Critical", CompanyId = 3, ShortDescription = "Accesso negato a NAS", Description = "Alcuni utenti non riescono ad accedere alla share NAS", Urgency = "High", Impact = "High", RequestedBy = "client_fs", Caller = "Giuseppe Fin", Location = "Torino Centro", ServiceOffering = "SAN Storage", Ci = "NAS-BACKUP", AssignedTeam = "Cyber Security", AssignedTo = "Paolo Neri", SuggestedTeam = "Cyber Security", Contract = "SLA-PLATINUM", ClosedDate = DateTime.Now.AddDays(-2), Resolution = "Permessi ripristinati da backup", InternalNotes = "Incidente risolto dal L1 Cyber" },
                new Incident { Number = "INC000009", Opened = DateTime.Now.AddDays(-2), State = "In Progress", Priority = "2 - High", CompanyId = 3, ShortDescription = "Lentezza applicazione bancaria", Description = "Tempi di risposta elevati su modulo conti correnti", Urgency = "Medium", Impact = "High", RequestedBy = "client_fs", Caller = "Helpdesk", Location = "Torino Centro", ServiceOffering = "Core Banking", Ci = "SRV-FIN-01", AssignedTeam = "Cloud", AssignedTo = "Andrea Grigio", SuggestedTeam = "Cloud", Contract = "SLA-SILVER", InternalNotes = "Analisi in corso su DB" },
                new Incident { Number = "INC000005", Opened = DateTime.Now.AddDays(-8), State = "Resolved", Priority = "3 - Low", CompanyId = 4, ShortDescription = "Monitoraggio non aggiornato", Description = "I dati del consumo energetico non vengono aggiornati", Urgency = "Low", Impact = "Medium", RequestedBy = "client_ee", Caller = "Francesca Eco", Location = "Moncalieri", ServiceOffering = "Infrastructure Monitoring", Ci = "MON-EE-01", AssignedTeam = "Cloud", AssignedTo = "Andrea Grigio", SuggestedTeam = "Cloud", Contract = "SLA-BRONZE", ClosedDate = DateTime.Now.AddDays(-3), Resolution = "Riavvio del servizio di telemetria", InternalNotes = "" },
                new Incident { Number = "INC000010", Opened = DateTime.Now.AddDays(-1), State = "New", Priority = "2 - High", CompanyId = 4, ShortDescription = "Allarme UPS", Description = "UPS in stato di allarme, batteria scarica", Urgency = "High", Impact = "Medium", RequestedBy = "client_ee", Caller = "Facility", Location = "Moncalieri", ServiceOffering = "Power Management", Ci = "UPS-APC-05", AssignedTeam = null, SuggestedTeam = "Network", Contract = "SLA-GOLD", InternalNotes = "" }
            );
        }

        if (!context.ChangeRequests.Any())
        {
            context.ChangeRequests.AddRange(
                new ChangeRequest { Number = "CHG000001", Opened = DateTime.Now.AddDays(-4), State = "Approved", Priority = "2 - High", CompanyId = 1, ShortDescription = "Aggiornamento firmware firewall", Description = "Applicare patch di sicurezza al firewall", Urgency = "Medium", Impact = "Medium", RequestedBy = "client_ti", Justification = "Necessario per conformità ISO 27001", ImplementationPlan = "Eseguire il 15/04 in finestra di manutenzione 22:00-23:00", RiskAnalysis = "Basso rischio, piano di rollback pronto", TicketGrade = "3", IsApproved = true, AssignedTeam = "Network", AssignedTo = "Mario Rossi" },
                new ChangeRequest { Number = "CHG000002", Opened = DateTime.Now.AddDays(-2), State = "In Progress", Priority = "1 - Critical", CompanyId = 2, ShortDescription = "Migrazione server centrale", Description = "Spostamento workload su nuova infrastruttura cloud", Urgency = "High", Impact = "High", RequestedBy = "client_gl", Justification = "Fine vita hardware attuale", ImplementationPlan = "Pianificato per il 20/04 con rollback immediato", RiskAnalysis = "Rischio alto, necessario test approfondito", TicketGrade = "1", IsApproved = false, AssignedTeam = "Cloud", AssignedTo = "Marco Blu" }
            );
        }

        context.SaveChanges();
    }
}