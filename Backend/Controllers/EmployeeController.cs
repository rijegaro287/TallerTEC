using Backend.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    [Route("get_all")]
    [Authorize(Policy = "Employee")]
    public void GetAllEmployees()
    {
        Employee[]? employees = Employee.SelectAllEmployees();
        Response.WriteAsJsonAsync(employees);
    }

    [HttpGet]
    [Route("get/{id}")]
    [Authorize(Policy = "Employee")]
    public void GetEmployee(int id)
    {
        Employee employee = Employee.SelectEmployee(id);
        Response.WriteAsJsonAsync(employee);
    }

    [HttpGet]
    [Route("get_by_email/{email}")]
    [Authorize(Policy = "Employee")]
    public void GetEmployeeByEmail(string email)
    {
        Employee employee = Employee.SelectEmployee(email);
        Response.WriteAsJsonAsync(employee);
    }

    [HttpPost]
    [Route("add")]
    [Authorize(Policy = "Employee")]
    public void AddEmployee([FromBody] Employee newEmployee)
    {
        Employee.InsertEmployee(newEmployee);
        Response.WriteAsJsonAsync(new { message = "Employee added" });
    }
}
