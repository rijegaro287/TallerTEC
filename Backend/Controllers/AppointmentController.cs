using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("appointment")]
[EnableCors("AllowAllOrigins")]
public class AppointmentController : ControllerBase
{
    /// <summary>
    /// Envía un array con todas las citas registradas en la base de datos.
    /// </summary>
    [HttpGet]
    [Route("get_all")]
    public void GetAllAppointments()
    {
        Appointment[]? appointments = Appointment.SelectAllAppointments();
        Response.WriteAsJsonAsync(appointments);
    }
    /// <summary>
    /// Envía una cita con el id especificado.
    /// </summary>
    /// <param name="id">El id de la cita que se solicita</param>
    [HttpGet]
    [Route("get/{id}")]
    public void GetAppointment(int id)
    {
        Appointment appointment = Appointment.SelectAppointment(id);
        Response.WriteAsJsonAsync(appointment);
    }

    /// <summary>
    /// Crea una nueva cita en la base de datos.
    /// </summary>
    /// <param name="appointment">Los datos de la cita que se creará</param>
    [HttpPost]
    [Route("add")]
    public void AddAppointment([FromBody] Appointment newAppointment)
    {
        Appointment.InsertAppointment(newAppointment);
        Response.WriteAsJsonAsync(new { status = "Ok" });
    }

    /// <summary>
    /// Actualiza una cita en la base de datos.
    /// </summary>
    /// <param name="newAppointment">La nueva información que tendrá la cita.</param>
    /// <param name="id">El id de la cita que se editará</param>
    [HttpPatch]
    [Route("update/{id}")]
    public void UpdateAppointment(int id, [FromBody] Appointment newAppointment)
    {
        if (Appointment.UpdateAppointment(id, newAppointment))
        {
            Response.WriteAsJsonAsync(new { status = "Ok" });
        }
        else
        {
            Response.WriteAsJsonAsync(new { error = "Appointment not found" });
        }
    }

    /// <summary>
    /// Elimina una cita de la base de datos.
    /// </summary>
    /// <param name="id">El id de la cita a eliminar</param>
    [HttpDelete]
    [Route("delete/{id}")]
    public void DeleteAppointment(int id)
    {
        Appointment.DeleteAppointment(id);
        Response.WriteAsJsonAsync(new { status = "Ok" });
    }

    /// <summary>
    /// Genera la factura de una cita en PDF y la envía al cliente.
    /// </summary>
    /// <param name="id">El id de la cita que se generará</param>
    [HttpGet]
    [Route("generate_bill/{id}")]
    public async Task SendBill(int id)
    {
        Appointment.GenerateBill(id);

        string filePath = "Reports/Bill.pdf";
        Response.Headers.Add("Content-Disposition", "attachment; filename=Bill.pdf");
        await Response.SendFileAsync(filePath);
    }
}