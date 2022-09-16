using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("client")]
[EnableCors("AllowAllOrigins")]
public class ClientController : ControllerBase
{
    /// <summary>
    /// Sends all clients to the frontend.
    /// </summary>
    [HttpGet]
    [Route("get_all")]
    // [Authorize(Policy = "Employee")]
    public void getAllClients()
    {
        Client[]? clients = Client.SelectAllClients();
        Response.WriteAsJsonAsync(clients);
    }

    /// <summary>
    /// Sends an clients to the frontend.
    /// </summary>
    /// <param name="id">The id of the client to send.</param>
    [HttpGet]
    [Route("get/{id}")]
    // [Authorize(Policy = "Employee")]
    public void getClient(int id)
    {
        Client client = Client.SelectClient(id);
        Response.WriteAsJsonAsync(client);
    }

    /// <summary>
    /// Creates an client.
    /// </summary>
    /// <param name="newClient">The client to create.</param>
    [HttpPost]
    [Route("add")]
    //[Authorize(Policy = "Employee")]
    public async Task AddClient([FromBody] Client newClient)
    {
        try
        {
            await Client.InsertClientAsync(newClient);
            await Response.WriteAsJsonAsync(new { status = "Ok" });
        }
        catch (System.Exception error)
        {
            await Response.WriteAsJsonAsync(new { error = error.Message });
        }
    }

    /// <summary>
    /// Updates a client
    /// </summary>
    /// <param name="id">The id of the appointment to send.</param>
    /// <param name="newClient">The client to update.</param>
    [HttpPatch]
    [Route("update/{id}")]
    //[Authorize(Policy = "Employee")]
    public void UpdateClient(int id, [FromBody] Client newClient)
    {
        if (Client.UpdateClient(id, newClient))
        {
            Response.WriteAsJsonAsync(new { status = "Ok" });
        }
        else
        {
            Response.WriteAsJsonAsync(new { error = "Client not found" });
        }
    }

    /// <summary>
    /// Deletes a client.
    /// </summary>
    /// <param name="id">The id of the client to delete.</param>
    [HttpDelete]
    [Route("delete/{id}")]
    //[Authorize(Policy = "Employee")]
    public void DeleteClient(int id)
    {
        Client.DeleteClient(id);
        Response.WriteAsJsonAsync(new { message = "Ok" });
    }
}

public class Passwords
{
    public string oldPassword { get; set; }
    public string newPassword { get; set; }
    public string confirmPassword { get; set; }

    public Passwords(string oldPassword, string newPassword, string confirmPassword)
    {
        this.oldPassword = oldPassword;
        this.newPassword = newPassword;
        this.confirmPassword = confirmPassword;

    }
}
