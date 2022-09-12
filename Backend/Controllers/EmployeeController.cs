using Microsoft.AspNetCore.Mvc;

using Backend.Models;

namespace Backend.Controllers;

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

    [HttpGet]
    [Route("get_by_email/{email}")]
    public void GetEmployeeByEmail(string email)
    {
        Employee employee = Employee.SelectEmployee(email);
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
