using FieldFix.Models;
using FieldFix.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldFix.Pages;

public class RequestsModel : PageModel
{
    private readonly RequestDbService _requestService;

    public RequestsModel(RequestDbService requestService)
    {
        _requestService = requestService;
    }

    [BindProperty(SupportsGet = true)]
    public string SearchText { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Status { get; set; } = "All";

    [BindProperty(SupportsGet = true)]
    public string Priority { get; set; } = "All";

    public List<string> StatusOptions { get; } = new() { "All", "Open", "Assigned", "In Progress", "Resolved" };

    public List<string> PriorityOptions { get; } = new() { "All", "Low", "Medium", "High", "Critical" };

    public List<ServiceRequest> Requests { get; private set; } = new();

    public async Task OnGet()
    {
        Requests = await _requestService.GetFilteredAsync(SearchText, Status, Priority);
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        await _requestService.DeleteAsync(id);
        return RedirectToPage();
    }
}
