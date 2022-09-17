using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("client")]
[EnableCors("AllowAllOrigins")]
public class ClientController : ControllerBase
{
    /// <summary>
    /// Envía un array con todos los clientes registrados en la base de datos.
    /// </summary>
    [HttpGet]
    [Route("get_all")]
    public void getAllClients()
    {
        Client[]? clients = Client.SelectAllClients();
        Response.WriteAsJsonAsync(clients);
    }

    /// <summary>
    /// Envía un cliente con el id especificado.
    /// </summary>
    /// <param name="id">El id del cliente que se solicita</param>
    [HttpGet]
    [Route("get/{id}")]
    public void getClient(int id)
    {
        Client client = Client.SelectClient(id);
        Response.WriteAsJsonAsync(client);
    }

    /// <summary>
    /// Crea un nuevo cliente en la base de datos.
    /// </summary>
    /// <param name="newClient">La información del cliente que se creará</param>
    [HttpPost]
    [Route("add")]
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
    /// Actualiza un cliente en la base de datos.
    /// </summary>
    /// <param name="id">El id del cliente que se editará</param>
    /// <param name="newClient">La nueva infomación del cliente</param>
    [HttpPatch]
    [Route("update/{id}")]
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
    /// Elimina un cliente de la base de datos.
    /// </summary>
    /// <param name="id">El id del cliente que se eliminará</param>
    [HttpDelete]
    [Route("delete/{id}")]
    public void DeleteClient(int id)
    {
        Client.DeleteClient(id);
        Response.WriteAsJsonAsync(new { message = "Ok" });
    }
}
