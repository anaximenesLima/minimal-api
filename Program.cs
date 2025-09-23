using MinimalApi.DTOs;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Olá pessoal!");

app.MapPost("/login", (loginDTO loginDTO) =>
{
    if (loginDTO.Email == "admin@test.com" && loginDTO.Senha == "123456")
        return Results.Ok("Login realizado com sucesso!");
    else
        return Results.Unauthorized();
});


app.Run();

