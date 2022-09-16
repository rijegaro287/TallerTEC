using Backend.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[ApiController]
[Route("employee")]
//[Authorize]
public class EmployeeController : ControllerBase
{
    /// <summary>
    /// Sends all employees to the frontend.
    /// </summary>
    [HttpGet]
    [Route("get_all")]
    //[Authorize(Policy = "Employee")]
    public async void GetAllEmployees()
    {
        Employee[] employees = Employee.SelectAllEmployees();
        await Response.WriteAsJsonAsync(employees);
    }

    /// <summary>
    /// Sends an employee to the frontend.
    /// </summary>
    /// <param name="id">The id of the employee to send.</param>
    [HttpGet]
    [Route("get/{id}")]
    //[Authorize(Policy = "Employee")]
    public async void GetEmployee(int id)
    {
        Employee employee = Employee.SelectEmployee(id);
        await Response.WriteAsJsonAsync(employee);
    }

    /// <summary>
    /// Sends an employee to the frontend.
    /// </summary>
    /// <param email="email">The email of the employee to send.</param>
    [HttpGet]
    [Route("get_by_email/{email}")]
    //[Authorize(Policy = "Employee")]
    public async void GetEmployeeByEmail(string email)
    {
        Employee employee = Employee.SelectEmployee(email);
        await Response.WriteAsJsonAsync(employee);
    }

    /// <summary>
    /// Creates an employee.
    /// </summary>
    /// <param name="newEmployee">The employee to create.</param>
    [HttpPost]
    [Route("add")]
    //[Authorize(Policy = "Employee")]
    public async void AddEmployee([FromBody] NewEmployeeInfo body)
    {
        Console.WriteLine(body.newEmployee);
        Employee newEmployee = body.newEmployee;
        string password = body.password;

        Employee.InsertEmployee(newEmployee, password);
        await Response.WriteAsJsonAsync(new { status = "Ok" });
    }

    /// <summary>
    /// Updates an employee
    /// </summary>
    /// <param name="newEmployee">The employee to update.</param>
    /// <param name="id">The id of the employee to update.</param>
    [HttpPatch]
    [Route("update/{id}")]
    //[Authorize(Policy = "Employee")]
    public void UpdateEmployee(int id, [FromBody] Employee newEmployee)
    {
        if (Employee.UpdateEmployee(id, newEmployee))
        {
            Response.WriteAsJsonAsync(new { status = "Ok" });
        }
        else
        {
            Response.WriteAsJsonAsync(new { error = "Employee not found" });
        }
    }

    /// <summary>
    /// Deletes an employee.
    /// </summary>
    /// <param name="id">The id of the employee to delete.</param>
    [HttpDelete]
    [Route("delete/{id}")]
    //[Authorize(Policy = "Employee")]
    public void DeleteEmployee(int id)
    {
        Employee.DeleteEmployee(id);
        Response.WriteAsJsonAsync(new { status = "Ok" });
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