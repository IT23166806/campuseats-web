
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(
                "https://campuseats-web-umber.vercel.app",
                "http://localhost:5173",
                "http://127.0.0.1:5173"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Frontend");

app.MapGet("/", () => "CampusEats API is running!");

app.MapGet("/api/menu", () =>
{
    var menu = new[]
    {
        new { Id = 1, Name = "Chicken Rice", Price = 850 },
        new { Id = 2, Name = "Fried Rice", Price = 750 },
        new { Id = 3, Name = "Kottu", Price = 900 }
    };

    return Results.Ok(menu);
});

app.Run();
