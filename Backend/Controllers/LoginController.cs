using Microsoft.AspNetCore.Mvc;

using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("/")]
public class LoginController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    // Verifica si la contraseña ingresada es correcta. Todavía no autentica, sólo verifica.
    public void login([FromBody] LoginInfo loginInfo)
    {
        string email = loginInfo.email;
        string password = loginInfo.password;
        string userType = loginInfo.userType;

        try
        {
            bool validPassword = false;

            switch (userType)
            {
                case "employee":
                    validPassword = EmployeePassword.ValidatePassword(email, password);
                    break;
                case "client":
                    // validPassword = ClientPassword.ValidatePassword(email, password);
                    validPassword = true;
                    break;
                default:
                    throw new Exception("Tipo de usuario no válido");
            }

            if (validPassword)
            {
                Response.StatusCode = 200;
            }
            else
            {
                throw new Exception("Correo o contraseña incorrectos");
            }
        }
        catch (System.Exception error)
        {
            Response.StatusCode = 403;
            Response.WriteAsJsonAsync(new { error = error.Message });
        }
    }
}

public struct LoginInfo
{
    public string email { get; set; }
    public string password { get; set; }
    public string userType { get; set; }

    public LoginInfo(string email, string password, string userType)
    {
        this.email = email;
        this.password = password;
        this.userType = userType;
    }
}
