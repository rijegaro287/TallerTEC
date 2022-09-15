using Backend.Models;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("client")]
public class ClientController : ControllerBase
{
    [HttpGet]
    [Route("get_all")]

    public void getAllClients()
    {
        Client[]? clients = Client.SelectAllClients();
        Response.WriteAsJsonAsync(clients);
    }

    [HttpGet]
    [Route("get/{id}")]
    public void getClient(int id)
    {
        Client client = Client.SelectClient(id);
        Response.WriteAsJsonAsync(client);
    }

    [HttpPost]
    [Route("add")]
    public void AddClient([FromBody] NewClientInfo body)
    {
        Client newClient = body.newClient;
        string password = body.password;

        Client.InsertClient(newClient, password);
        Response.WriteAsJsonAsync(new { message = "Client added" });
    }

    [HttpPatch]
    [Route("update/{id}")]
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
    public void DeleteClient(int id)
    {
        Client.DeleteClient(id);
        Response.WriteAsJsonAsync(new { message = "Client deleted" });
    }
}

public struct NewClientInfo
{
    public Client newClient { get; set; }
    public string password { get; set; }

    public NewClientInfo(Client newClient, string password)
    {
        this.newClient = newClient;
        this.password = password;
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
