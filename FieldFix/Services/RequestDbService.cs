using FieldFix.Data;
using FieldFix.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldFix.Services;

public class RequestDbService
{
    private readonly AppDbContext _context;

    public RequestDbService(AppDbContext context)
    {
        _context = context;
    }

    public static List<ServiceRequest> FilterRequests(IEnumerable<ServiceRequest> requests, string? searchText, string? statusFilter, string? priorityFilter)
    {
        var term = searchText?.Trim();
        var status = statusFilter ?? "All";
        var priority = priorityFilter ?? "All";

        return requests
            .Where(r => string.IsNullOrWhiteSpace(term)
                || r.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                || r.Location.Contains(term, StringComparison.OrdinalIgnoreCase)
                || r.Category.Contains(term, StringComparison.OrdinalIgnoreCase)
                || r.RequestedBy.Contains(term, StringComparison.OrdinalIgnoreCase)
                || r.Description.Contains(term, StringComparison.OrdinalIgnoreCase))
            .Where(r => status == "All" || string.Equals(r.Status, status, StringComparison.OrdinalIgnoreCase))
            .Where(r => priority == "All" || string.Equals(r.Priority, priority, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
    }

    public async Task<List<ServiceRequest>> GetAllAsync()
    {
        return await _context.ServiceRequests
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<ServiceRequest>> GetFilteredAsync(string? searchText, string? statusFilter, string? priorityFilter)
    {
        var query = _context.ServiceRequests.AsQueryable();
        var term = searchText?.Trim();
        var status = statusFilter ?? "All";
        var priority = priorityFilter ?? "All";

        if (!string.IsNullOrWhiteSpace(term))
        {
            query = query.Where(r => r.Title.Contains(term)
                || r.Location.Contains(term)
                || r.Category.Contains(term)
                || r.RequestedBy.Contains(term)
                || r.Description.Contains(term));
        }

        if (!string.Equals(status, "All", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(r => r.Status == status);
        }

        if (!string.Equals(priority, "All", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(r => r.Priority == priority);
        }

        return await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<ServiceRequest?> GetByIdAsync(int id)
    {
        return await _context.ServiceRequests.FindAsync(id);
    }

    public async Task AddAsync(ServiceRequest request)
    {
        request.CreatedAt = DateTime.UtcNow;
        await _context.ServiceRequests.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(ServiceRequest updatedRequest)
    {
        var request = await _context.ServiceRequests.FindAsync(updatedRequest.Id);
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

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var request = await _context.ServiceRequests.FindAsync(id);
        if (request is null)
        {
            return false;
        }

        _context.ServiceRequests.Remove(request);
        await _context.SaveChangesAsync();
        return true;
    }
}
