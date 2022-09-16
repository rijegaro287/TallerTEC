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
    [HttpGet]
    [Route("get_all")]
    //[Authorize(Policy = "Employee")]
    public void getAllClients()
    {
        Client[]? clients = Client.SelectAllClients();
        Response.WriteAsJsonAsync(clients);
    }

    [HttpGet]
    [Route("get/{id}")]
    //[Authorize(Policy = "Employee")]
    public void getClient(int id)
    {
        Client client = Client.SelectClient(id);
        Response.WriteAsJsonAsync(client);
    }

    [HttpPost]
    [Route("add")]
    [Authorize(Policy = "Employee")]
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

    [HttpPatch]
    [Route("update/{id}")]
    [Authorize(Policy = "Employee")]
    public void UpdateClient(int id, [FromBody] Client newClient)
    {
        if (Client.UpdateClient(id, newClient))
        {
            Response.WriteAsJsonAsync(new { message = "Client updated" });
        }
        else
        {
            Response.WriteAsJsonAsync(new { message = "Client not found" });
        }
    }

    [HttpDelete]
    [Route("delete/{id}")]
    [Authorize(Policy = "Employee")]
    public void DeleteClient(int id)
    {
        Client.DeleteClient(id);
        Response.WriteAsJsonAsync(new { message = "Client deleted" });
    }

    // [HttpPatch]
    // [Route("change_password/{email}")]
    // // public void ChangePassword(string email, [FromBody] string oldPassword, string newPassword, string confirmPassword)
    // public void ChangePassword(string email, [FromBody] Passwords passwords)
    // {
    //     string oldPassword = passwords.oldPassword;
    //     string newPassword = passwords.newPassword;
    //     string confirmPassword = passwords.confirmPassword;
    //     if (Client.UpdatePassword(email, oldPassword, newPassword, confirmPassword))
    //     {
    //         Response.WriteAsJsonAsync(new { message = "Password changed" });
    //     }
    //     else
    //     {
    //         Response.WriteAsJsonAsync(new { message = "Password not changed" });
    //     }
    // }
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
