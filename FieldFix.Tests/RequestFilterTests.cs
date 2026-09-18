using FieldFix.Models;
using FieldFix.Services;

namespace FieldFix.Tests;

public class RequestFilterTests
{
    [Fact]
    public void FilterRequests_AppliesSearchStatusAndPriority()
    {
        var requests = new List<ServiceRequest>
        {
            new() { Id = 1, Title = "Broken elevator", Status = "Open", Priority = "High", Location = "Grand Central", Category = "Infrastructure" },
            new() { Id = 2, Title = "Escalator sensor", Status = "In Progress", Priority = "Medium", Location = "42nd Street", Category = "Equipment" },
            new() { Id = 3, Title = "Lighting outage", Status = "Open", Priority = "Low", Location = "Atlantic Avenue", Category = "Safety" }
        };

        var filtered = RequestDbService.FilterRequests(requests, "elev", "Open", "High");

        Assert.Single(filtered);
        Assert.Equal(1, filtered[0].Id);
    }
}
