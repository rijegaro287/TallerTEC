using Backend.Models;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers;

[ApiController]
[Route("/")]
[EnableCors("AllowAllOrigins")]
public class LoginController : ControllerBase
{
    /// <summary>
    /// Verifica las credenciales ingresadas y envía un cookie de sesión si son correctas.
    /// </summary>
    /// <param name="loginInfo">El email y la contraseña ingresados</param>
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
            await Response.WriteAsJsonAsync(new { error = error.Message });
        }
    }

    /// <summary>
    /// Cierra la sesión actual y elimina el cookie de sesión.
    /// </summary>
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
            await Response.WriteAsJsonAsync(new { error = error.Message });
        }
    }

    /// <summary>
    /// Genera y envía un cookie de sesión si las credenciales son válidas.
    /// </summary>
    private async Task GenerateCookieAsync(bool isValidPassword)
    {
        if (isValidPassword)
        {
            var claims = new List<Claim> { new Claim("userType", "employee") };
            var identity = new ClaimsIdentity(claims, "AuthCookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("AuthCookie", principal);
            await Response.WriteAsJsonAsync(new { status = "Ok" });
        }
        else
        {
            throw new Exception("Correo o contraseña incorrectos");
        }
    }
}

/// <summary>
/// Contiene el email y la contraseña ingresados por el usuario
/// </summary>
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
