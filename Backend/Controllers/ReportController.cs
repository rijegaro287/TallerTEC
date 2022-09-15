using Microsoft.AspNetCore.Mvc;

using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("report")]
public class ReportController : ControllerBase
{

    [HttpGet]
    [Route("sales_per_branch")]

    public void SalesPerBranch([FromBody] DatesInfo dates)
    {   
        string fromDate = dates.FromDate;
        string toDate = dates.ToDate;
        Dictionary<int,int> salesPerBranch = Report.SalesPerBranch(fromDate, toDate);
        // Bill[] salesPerBranch = Report.SalesPerBranch(fromDate, toDate);
        // List<Bill> salesPerBranch = Report.SalesPerBranch(fromDate, toDate);
        Response.WriteAsJsonAsync(salesPerBranch);
    }    
    
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

public struct DatesInfo
{
    public string FromDate { get; set; }
    public string ToDate { get; set; }

    public DatesInfo(string fromDate, string toDate)
    {
        this.FromDate = fromDate;
        this.ToDate = toDate;
    }
}