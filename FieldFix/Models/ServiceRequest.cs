namespace FieldFix.Models;

public class ServiceRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Priority { get; set; } = "Medium";
    public string Status { get; set; } = "Open";
    public string Category { get; set; } = "Infrastructure";
    public string RequestedBy { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = "Unassigned";
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
