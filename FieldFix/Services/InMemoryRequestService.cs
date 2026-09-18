using System.Text.Json;
using FieldFix.Models;
using Microsoft.AspNetCore.Hosting;

namespace FieldFix.Services;

public class InMemoryRequestService
{
    private readonly string _storagePath;
    private readonly List<ServiceRequest> _requests;

    public InMemoryRequestService(IWebHostEnvironment environment)
    {
        var directory = Path.Combine(environment.ContentRootPath, "Data");
        Directory.CreateDirectory(directory);

        _storagePath = Path.Combine(directory, "requests.json");
        _requests = Load();
    }

    public List<ServiceRequest> GetAll()
    {
        return _requests.OrderByDescending(r => r.CreatedAt).ToList();
    }

    public ServiceRequest? GetById(int id)
    {
        return _requests.FirstOrDefault(r => r.Id == id);
    }

    public void Add(ServiceRequest request)
    {
        request.Id = _requests.Count == 0 ? 1 : _requests.Max(r => r.Id) + 1;
        request.CreatedAt = DateTime.UtcNow;
        _requests.Insert(0, request);
        Save();
    }

    public bool Update(ServiceRequest updatedRequest)
    {
        var request = _requests.FirstOrDefault(r => r.Id == updatedRequest.Id);
        if (request is null)
        {
            return false;
        }

        request.Title = updatedRequest.Title;
        request.Location = updatedRequest.Location;
        request.Priority = updatedRequest.Priority;
        request.Status = updatedRequest.Status;
        request.Category = updatedRequest.Category;
        request.RequestedBy = updatedRequest.RequestedBy;
        request.AssignedTo = updatedRequest.AssignedTo;
        request.Description = updatedRequest.Description;
        Save();
        return true;
    }

    public bool Delete(int id)
    {
        var request = _requests.FirstOrDefault(r => r.Id == id);
        if (request is null)
        {
            return false;
        }

        _requests.Remove(request);
        Save();
        return true;
    }

    private List<ServiceRequest> Load()
    {
        if (!File.Exists(_storagePath))
        {
            var seeded = CreateSeedData();
            var json = JsonSerializer.Serialize(seeded, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_storagePath, json);
            return seeded;
        }

        try
        {
            var json = File.ReadAllText(_storagePath);
            var requests = JsonSerializer.Deserialize<List<ServiceRequest>>(json);
            return requests ?? CreateSeedData();
        }
        catch
        {
            var seeded = CreateSeedData();
            var json = JsonSerializer.Serialize(seeded, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_storagePath, json);
            return seeded;
        }
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_requests, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_storagePath, json);
    }

    private static List<ServiceRequest> CreateSeedData()
    {
        return new List<ServiceRequest>
        {
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
        };
    }
}

