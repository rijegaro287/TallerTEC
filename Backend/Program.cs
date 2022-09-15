var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder => builder.WithOrigins("*"));
});

builder.Services.AddControllers();

builder.Services.AddAuthentication("AuthCookie")
    .AddCookie("AuthCookie", options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Employee", policy => policy.RequireClaim("userType", "employee"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
