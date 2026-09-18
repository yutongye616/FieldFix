using FieldFix.Models;
using FieldFix.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FieldFix.Pages;

public class EditRequestModel : PageModel
{
    private readonly RequestDbService _requestService;

    public EditRequestModel(RequestDbService requestService)
    {
        _requestService = requestService;
    }

    [BindProperty]
    public EditRequestInput Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var request = await _requestService.GetByIdAsync(id);
        if (request is null)
        {
            return RedirectToPage("/Requests");
        }

        Input = new EditRequestInput
        {
            Id = request.Id,
            Title = request.Title,
            Location = request.Location,
            Category = request.Category,
            Priority = request.Priority,
            Status = request.Status,
            RequestedBy = request.RequestedBy,
            AssignedTo = request.AssignedTo,
            Description = request.Description
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var request = await _requestService.GetByIdAsync(Input.Id);
        if (request is null)
        {
            return RedirectToPage("/Requests");
        }

        request.Title = Input.Title;
        request.Location = Input.Location;
        request.Category = Input.Category;
        request.Priority = Input.Priority;
        request.Status = Input.Status;
        request.RequestedBy = Input.RequestedBy;
        request.AssignedTo = Input.AssignedTo;
        request.Description = Input.Description;

        await _requestService.UpdateAsync(request);
        return RedirectToPage("/Requests");
    }
}

public class EditRequestInput
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Category { get; set; } = "Infrastructure";
    public string Priority { get; set; } = "Medium";
    public string Status { get; set; } = "Open";
    public string RequestedBy { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = "Unassigned";
    public string Description { get; set; } = string.Empty;
}
