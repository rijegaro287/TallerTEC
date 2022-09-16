using Microsoft.AspNetCore.Mvc;

using Backend.Models;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers;

[ApiController]
[Route("report")]
[EnableCors("AllowAllOrigins")]
public class ReportController : ControllerBase
{

    [HttpGet]
    [Route("sales_per_branch")]

    public async Task SalesPerBranch([FromBody] DatesInfo dates)
    {
        string fromDate = dates.FromDate;
        string toDate = dates.ToDate;
        Report.SalesPerBranch(fromDate, toDate);
        string filePath = "SalesPerBranch.pdf";
        await Response.SendFileAsync(filePath);
    }    

    [HttpGet]
    [Route("sales_per_branch2")]

    public async Task SalesPerBranch2()
    {
        string filePath = "SalesPerBranch.pdf";
        await Response.SendFileAsync(filePath);
    }    
    
    [HttpGet]
    [Route("get_top_clients")]

    public async Task geTopClients()
    {
        Report.TopFrequentClients();
        // Response.WriteAsJsonAsync(clients);
        string filePath = "TopClients.pdf";
        await Response.SendFileAsync(filePath);
    }

    [HttpGet]
    [Route("get_top_vehicles")]
    public async Task geTopVehicles()
    {
        Report.TopFrequentVehicles();
        string filePath = "TopVehicles.pdf";
        await Response.SendFileAsync(filePath);
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