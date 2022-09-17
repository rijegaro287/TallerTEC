using Backend.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController : ControllerBase
{
    /// <summary>
    /// Envía un array con todos los empleados registrados en la base de datos.
    /// </summary>
    [HttpGet]
    [Route("get_all")]
    public async void GetAllEmployees()
    {
        Employee[] employees = Employee.SelectAllEmployees();
        await Response.WriteAsJsonAsync(employees);
    }

    /// <summary>
    /// Envía un empleado con el id especificado.
    /// </summary>
    /// <param name="id">El id del empleado que se solicita</param>
    [HttpGet]
    [Route("get/{id}")]
    public async void GetEmployee(int id)
    {
        Employee employee = Employee.SelectEmployee(id);
        await Response.WriteAsJsonAsync(employee);
    }

    /// <summary>
    /// Crea un nuevo empleado en la base de datos.
    /// </summary>
    /// <param name="newEmployee">La información del empleado que se creará</param>
    [HttpPost]
    [Route("add")]
    public async void AddEmployee([FromBody] NewEmployeeInfo body)
    {
        Console.WriteLine(body.newEmployee);
        Employee newEmployee = body.newEmployee;
        string password = body.password;

        Employee.InsertEmployee(newEmployee, password);
        await Response.WriteAsJsonAsync(new { status = "Ok" });
    }

    /// <summary>
    /// Actualiza un empleado en la base de datos.
    /// </summary>
    /// <param name="newEmployee">La nueva información del empleado</param>
    /// <param name="id">El id del empleado que se editará</param>
    [HttpPatch]
    [Route("update/{id}")]
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
    /// Elimina un empleado de la base de datos.
    /// </summary>
    /// <param name="id">El id del empleado que se eliminará</param>
    [HttpDelete]
    [Route("delete/{id}")]
    public void DeleteEmployee(int id)
    {
        Employee.DeleteEmployee(id);
        Response.WriteAsJsonAsync(new { status = "Ok" });
    }
}

/// <summary>
/// Contiene la información necesaria para crear un nuevo empleado.
/// </summary>
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