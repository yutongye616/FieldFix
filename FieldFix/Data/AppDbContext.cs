using FieldFix.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldFix.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ServiceRequest>().HasData(
            new ServiceRequest
            {
                Id = 1,
                Title = "Broken elevator panel",
                Location = "Grand Central",
                Priority = "High",
                Status = "Assigned",
                Category = "Infrastructure",
                RequestedBy = "Transit Operations",
                Description = "Passenger panel is not responding and requires a field inspection.",
                AssignedTo = "Tech A-14",
                CreatedAt = DateTime.UtcNow.AddHours(-3)
            },
            new ServiceRequest
            {
                Id = 2,
                Title = "Escalator sensor fault",
                Location = "42nd Street",
                Priority = "Medium",
                Status = "In Progress",
                Category = "Equipment",
                RequestedBy = "Station Operations",
                Description = "Sensor alarm is intermittently triggering on the inbound escalator.",
                AssignedTo = "Tech B-09",
                CreatedAt = DateTime.UtcNow.AddHours(-2)
            },
            new ServiceRequest
            {
                Id = 3,
                Title = "Lighting outage",
                Location = "Atlantic Avenue",
                Priority = "Low",
                Status = "Open",
                Category = "Safety",
                RequestedBy = "Customer Service",
                Description = "Multiple platform lights are out after a power fluctuation.",
                AssignedTo = "Unassigned",
                CreatedAt = DateTime.UtcNow.AddHours(-1)
            }
        );
    }
}
