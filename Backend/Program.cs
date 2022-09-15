var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("https://localhost:4200/login");
                      });
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
    options.AddPolicy("Client", policy => policy.RequireClaim("userType", "client"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
