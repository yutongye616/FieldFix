using FieldFix.Models;
using FieldFix.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldFix.Pages;

public class IndexModel : PageModel
{
    private readonly RequestDbService _requestService;

    public IndexModel(RequestDbService requestService)
    {
        _requestService = requestService;
    }

    public RequestSummary Summary { get; set; } = new();

    public async Task OnGetAsync()
    {
        var requests = await _requestService.GetAllAsync();

        Summary = new RequestSummary
        {
            OpenRequests = requests.Count(r => r.Status == "Open"),
            HighPriority = requests.Count(r => r.Priority == "High" || r.Priority == "Critical"),
            AssignedToday = requests.Count(r => r.Status == "Assigned" || r.Status == "In Progress"),
            Resolved = requests.Count(r => r.Status == "Resolved")
        };
    }
}
