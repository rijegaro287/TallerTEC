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
    /// Sends all appointments to the frontend.
    /// </summary>
    [HttpGet]
    [Route("get_all")]
    public void GetAllAppointments()
    {
        Appointment[]? appointments = Appointment.SelectAllAppointments();
        Response.WriteAsJsonAsync(appointments);
    }
    /// <summary>
    /// Sends an appointment to the frontend.
    /// </summary>
    /// <param name="id">The id of the appointment to send.</param>
    [HttpGet]
    [Route("get/{id}")]
    public void GetAppointment(int id)
    {
        Appointment appointment = Appointment.SelectAppointment(id);
        Response.WriteAsJsonAsync(appointment);
    }

    /// <summary>
    /// Creates an appointment.
    /// </summary>
    /// <param name="appointment">The appointment to create.</param>
    [HttpPost]
    [Route("add")]
    public void AddAppointment([FromBody] Appointment newAppointment)
    {
        Appointment.InsertAppointment(newAppointment);
        Response.WriteAsJsonAsync(new { message = "Appointment added" });
    }

    /// <summary>
    /// Updates an appointment.
    /// </summary>
    /// <param name="newAppointment">The appointment to update.</param>
    /// <param name="id">The id of the appointment to update.</param>
    [HttpPatch]
    [Route("update/{id}")]
    public void UpdateAppointment(int id, [FromBody] Appointment newAppointment)
    {
        if (Appointment.UpdateAppointment(id, newAppointment))
        {
            Response.WriteAsJsonAsync(new { message = "Appointment updated" });
        }
        else
        {
            Response.WriteAsJsonAsync(new { message = "Appointment not found" });
        }
    }

    /// <summary>
    /// Deletes an appointment.
    /// </summary>
    /// <param name="id">The id of the appointment to delete.</param>
    [HttpDelete]
    [Route("delete/{id}")]
    public void DeleteAppointment(int id)
    {
        Appointment.DeleteAppointment(id);
        Response.WriteAsJsonAsync(new { message = "Appointment deleted" });
    }

    /// <summary>
    /// Generates and sends a bill for an appointment. 
    /// </summary>
    /// <param name="id">The id of the appointment to generate a bill for.</param>
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