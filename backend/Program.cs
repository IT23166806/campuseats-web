using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.AllowAnyOrigin()
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

app.MapGet("/api/db-status", async (IConfiguration config) =>
{
    try
    {
        var connectionString =
            config.GetConnectionString("Default");

        if (string.IsNullOrEmpty(connectionString))
        {
            return Results.Problem(
                "Database connection is not configured.");
        }

        await using var connection =
            new NpgsqlConnection(connectionString);

        await connection.OpenAsync();

        await using var command =
            new NpgsqlCommand("SELECT 1", connection);

        await command.ExecuteScalarAsync();

        return Results.Ok(new
        {
            status = "Connected",
            database = "Neon PostgreSQL"
        });
    }
    catch
    {
        return Results.Problem(
            "Database connection failed.");
    }
});

app.Run();
