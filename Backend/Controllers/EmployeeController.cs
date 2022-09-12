using Microsoft.AspNetCore.Mvc;

using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    [Route("get_all")]
    public void GetAllEmployees()
    {
        Employee[]? employees = Employee.SelectAllEmployees();
        Response.WriteAsJsonAsync(employees);
    }

    [HttpGet]
    [Route("get/{id}")]
    public void GetEmployee(int id)
    {
        Employee employee = Employee.SelectEmployee(id);
        Response.WriteAsJsonAsync(employee);
    }

    [HttpPost]
    [Route("add")]
    public void AddEmployee([FromBody] Employee newEmployee)
    {
        Employee.InsertEmployee(newEmployee);
        Response.WriteAsJsonAsync(new { message = "Employee added" });
    }
}
