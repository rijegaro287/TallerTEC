using Microsoft.AspNetCore.Mvc;

using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("report")]
public class ReportController : ControllerBase
{
    [HttpGet]
    [Route("get_top_clients")]

    public void geTopClients()
    {
        Dictionary<int,int> clients = Report.TopFrequentClients();
        Response.WriteAsJsonAsync(clients);
    }

    [HttpGet]
    [Route("get_top_vehicles")]

    public void geTopVehicles()
    {
        // string[]? vehiclesPlates = Report.TopFrequentVehicles();
        Dictionary<string,int> vehiclesPlatesFrequency = Report.TopFrequentVehicles();
        Response.WriteAsJsonAsync(vehiclesPlatesFrequency);
    }
}