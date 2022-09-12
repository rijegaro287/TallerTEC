using Backend.Models;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Backend.Controllers;

[ApiController]
[Route("/")]
public class LoginController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async void login([FromBody] LoginInfo loginInfo)
    {
        string email = loginInfo.email;
        string password = loginInfo.password;
        string userType = loginInfo.userType;

        try
        {
            bool isValidPassword = false;

            switch (userType)
            {
                case "employee":
                    isValidPassword = EmployeePassword.ValidatePassword(email, password);
                    break;
                case "client":
                    isValidPassword = ClientPassword.ValidatePassword(email, password);
                    break;
                default:
                    throw new Exception("Tipo de usuario no válido");
            }

            await GenerateCookieAsync(isValidPassword, userType);
        }
        catch (System.Exception error)
        {
            Response.StatusCode = 403;
            await Response.WriteAsJsonAsync(new { error = error.Message });
        }
    }

    [HttpGet]
    [Route("logout")]
    public async void logout()
    {
        try
        {
            await HttpContext.SignOutAsync();
        }
        catch (System.Exception error)
        {
            Response.StatusCode = 500;
            await Response.WriteAsJsonAsync(new { error = error.Message });
        }
    }

    private async Task GenerateCookieAsync(bool isValidPassword, string userType)
    {
        if (isValidPassword)
        {
            var claims = new List<Claim> {
                    new Claim("userType", userType)
                };
            var identity = new ClaimsIdentity(claims, "AuthCookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("AuthCookie", principal);
            Response.StatusCode = 200;
        }
        else
        {
            throw new Exception("Correo o contraseña incorrectos");
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
