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

        try
        {
            bool isValidPassword = false;


            isValidPassword = EmployeePassword.ValidatePassword(email, password);



            await GenerateCookieAsync(isValidPassword);
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

    private async Task GenerateCookieAsync(bool isValidPassword)
    {
        if (isValidPassword)
        {
            var claims = new List<Claim> {
                    new Claim("userType", "employee")
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

    public LoginInfo(string email, string password, string userType)
    {
        this.email = email;
        this.password = password;
    }
}
