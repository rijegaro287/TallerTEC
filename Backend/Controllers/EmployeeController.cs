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
    public async void GetAllEmployees()
    {
        Employee[] employees = Employee.SelectAllEmployees();
        await Response.WriteAsJsonAsync(employees);
    }

    [HttpGet]
    [Route("get/{id}")]
    [Authorize(Policy = "Employee")]
    public async void GetEmployee(int id)
    {
        Employee employee = Employee.SelectEmployee(id);
        await Response.WriteAsJsonAsync(employee);
    }

    [HttpGet]
    [Route("get_by_email/{email}")]
    [Authorize(Policy = "Employee")]
    public async void GetEmployeeByEmail(string email)
    {
        Employee employee = Employee.SelectEmployee(email);
        await Response.WriteAsJsonAsync(employee);
    }

    [HttpPost]
    [Route("add")]
    [Authorize(Policy = "Employee")]
    public async void AddEmployee([FromBody] NewEmployeeInfo body)
    {
        Employee newEmployee = body.newEmployee;
        string password = body.password;

        Console.WriteLine(newEmployee);
        Console.WriteLine(password);

        Employee.InsertEmployee(newEmployee, password);
        await Response.WriteAsJsonAsync(new { message = "Employee added" });
    }
}

public struct NewEmployeeInfo
{
    public Employee newEmployee { get; set; }
    public string password { get; set; }

    public NewEmployeeInfo(Employee newEmployee, string password)
    {
        this.newEmployee = newEmployee;
        this.password = password;
    }
}