using FieldFix.Models;
using FieldFix.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FieldFix.Pages;

public class RequestFormModel : PageModel
{
    private readonly RequestDbService _requestService;

    public RequestFormModel(RequestDbService requestService)
    {
        _requestService = requestService;
    }

    [BindProperty]
    public RequestInput Input { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var request = new ServiceRequest
        {
            Title = Input.Title,
            Location = Input.Location,
            Category = Input.Category,
            Priority = Input.Priority,
            RequestedBy = Input.RequestedBy,
            AssignedTo = string.IsNullOrWhiteSpace(Input.AssignedTo) ? "Unassigned" : Input.AssignedTo,
            Description = Input.Description,
            Status = "Open"
        };

        await _requestService.AddAsync(request);

        return RedirectToPage("/Requests");
    }
}

public class RequestInput
{
    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(120)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public string Category { get; set; } = "Infrastructure";

    [Required]
    public string Priority { get; set; } = "Medium";

    [Required]
    public string RequestedBy { get; set; } = string.Empty;

    public string AssignedTo { get; set; } = "Unassigned";

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
}
