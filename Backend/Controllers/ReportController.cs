using Microsoft.AspNetCore.Mvc;

using Backend.Models;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers;

[ApiController]
[Route("report")]
[EnableCors("AllowAllOrigins")]
public class ReportController : ControllerBase
{

    /// <summary>
    /// Envía un PDF con todas las ventas realizadas en un rango de fechas.
    /// </summary>
    /// <param name="body">El rango de fechas a utilizar</param>
    [HttpGet]
    [Route("sales_per_branch")]
    public async Task SalesPerBranch([FromBody] DatesInfo dates)
    {
        string fromDate = dates.FromDate;
        string toDate = dates.ToDate;
        Report.SalesPerBranch(fromDate, toDate);

        // string filePath = "Reports/SalesPerBranch.pdf";
        // Response.Headers.Add("Content-Disposition", "attachment; filename=SalesPerBranch.pdf");
        // await Response.SendFileAsync(filePath);
    }

    [HttpGet]
    [Route("sales_per_branch2")]
    public async Task SalesPerBranch2()
    {
        string filePath = "Reports/SalesPerBranch.pdf";
        await Response.SendFileAsync(filePath);
    }

    /// <summary>
    /// Envía un PDF con los 10 clientes que más compras han realizado
    /// </summary>
    [HttpGet]
    [Route("get_top_clients")]
    public async Task geTopClients()
    {
        Report.TopFrequentClients();
        // Response.WriteAsJsonAsync(clients);

        string filePath = "Reports/TopClients.pdf";
        Response.Headers.Add("Content-Disposition", "attachment; filename=TopClients.pdf");
        await Response.SendFileAsync(filePath);
    }

    /// <summary>
    /// Envía un PDF con los 10 vehículos que se han registrado más
    /// </summary>
    [HttpGet]
    [Route("get_top_vehicles")]
    public async Task geTopVehicles()
    {
        Report.TopFrequentVehicles();

        string filePath = "Reports/TopVehicles.pdf";
        Response.Headers.Add("Content-Disposition", "attachment; filename=TopVehicles.pdf");
        await Response.SendFileAsync(filePath);
    }
}

/// <summary>
/// Contiene el rango de fechas para obtener un reporte
/// </summary>
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