using Backend.Models;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("appointment")]

public class AppointmentController : ControllerBase
{
    [HttpGet]
    [Route("get_all")]
    public void GetAllAppointments()
    {
        Appointment[]? appointments = Appointment.SelectAllAppointments();
        Response.WriteAsJsonAsync(appointments);
    }

    [HttpGet]
    [Route("get/{id}")]
    public void GetAppointment(int id)
    {
        Appointment appointment = Appointment.SelectAppointment(id);
        Response.WriteAsJsonAsync(appointment);
    }

    [HttpPost]
    [Route("add")]
    public void AddAppointment([FromBody] Appointment newAppointment)
    {
        Appointment.InsertAppointment(newAppointment);
        Response.WriteAsJsonAsync(new { message = "Appointment added" });
    }

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

    [HttpDelete]
    [Route("delete/{id}")]
    public void DeleteAppointment(int id)
    {
        Appointment.DeleteAppointment(id);
        Response.WriteAsJsonAsync(new { message = "Appointment deleted" });
    }

    [HttpGet]
    [Route("get_bill/{id}")]
    public void SendBill(int id)
    {
        Bill bill = Appointment.GenerateBill(id);
        Response.WriteAsJsonAsync(bill);
    }
}