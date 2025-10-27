using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Dominio.DTOs;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Dominio.ModelViews;
using MinimalApi.Infraestrutura.Db;
using MinimalApi.Dominio.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

#region Builder
var builder = WebApplication.CreateBuilder(args);

var Key = builder.Configuration.GetSection("Jwt").ToString(); 
if (string.IsNullOrEmpty(Key))
    throw new Exception("Chave JWT não encontrada no arquivo de configuração.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),

    };
});

builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddScoped<IveiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Dbcontexto>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    )
);

var app = builder.Build();
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home()));
#endregion

#region Administradores
app.MapPost("/administradores/login", ([FromBody]LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    if (administradorServico.Login(loginDTO) != null)
        return Results.Ok("Login realizado com sucesso!");
    else
        return Results.Unauthorized();
});
app.MapPost("/administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorServico administradorServico) =>
{
    if (administradorServico.Incluir(administradorDTO) != null)
        return Results.Ok("Administrador incluído com sucesso!");
    else
        return Results.Unauthorized();
});
app.MapGet("/administradores", ([FromQuery] int pagina, IAdministradorServico administradorServico) =>
{
    var administradores = administradorServico.ObterTodos(pagina);
    return Results.Ok(administradores);
});
app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
{
    var administrador = administradorServico.ObterPorId(id);
    if (administrador != null)
        return Results.Ok(administrador);
    else
        return Results.NotFound();
});
#endregion

#region Veiculos
app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IveiculoServico veiculoServico) =>
{
    var validacao = new ErroDeValidacao();
    if(string.IsNullOrEmpty(veiculoDTO.Nome) || string.IsNullOrEmpty(veiculoDTO.Marca) || veiculoDTO.Ano >= 0)
    {
        validacao.Mensagens = new List<string>();
        if (string.IsNullOrEmpty(veiculoDTO.Nome))
            validacao.Mensagens.Add("O campo Nome é obrigatório.");
        if (string.IsNullOrEmpty(veiculoDTO.Marca))
            validacao.Mensagens.Add("O campo Marca é obrigatório.");
        if (veiculoDTO.Ano <= 1900)
            validacao.Mensagens.Add("O campo Ano deve ser maior que 1900.");

        return Results.BadRequest(validacao);
    }

    var veiculo = new Veiculo
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano
    };
    veiculoServico.Incluir(veiculo);
    return Results.Created($"/veiculos/{veiculo.Id}", veiculo);
});
app.MapGet("/veiculos", ([FromQuery] VeiculoDTO veiculoDTO, IveiculoServico veiculoServico) =>
{
    var veiculos = veiculoServico.ObterTodos();
    return Results.Ok(veiculos);
});
app.MapGet("/veiculos/{id}", ([FromRoute] int id, IveiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.ObterPorId(id);
    if (veiculo != null)
        return Results.Ok(veiculo);
    else
        return Results.NotFound();
});
app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, IveiculoServico veiculoServico) =>
{
    var validacao = new ErroDeValidacao();
    if (string.IsNullOrEmpty(veiculoDTO.Nome) || string.IsNullOrEmpty(veiculoDTO.Marca) || veiculoDTO.Ano >= 0)
    {
        validacao.Mensagens = new List<string>();
        if (string.IsNullOrEmpty(veiculoDTO.Nome))
            validacao.Mensagens.Add("O campo Nome é obrigatório.");
        if (string.IsNullOrEmpty(veiculoDTO.Marca))
            validacao.Mensagens.Add("O campo Marca é obrigatório.");
        if (veiculoDTO.Ano <= 1900)
            validacao.Mensagens.Add("O campo Ano deve ser maior que 1900.");

        return Results.BadRequest(validacao);
    }

    var veiculo = veiculoServico.ObterPorId(id);
    if (veiculo != null)
    {
        veiculo.Nome = veiculoDTO.Nome;
        veiculo.Marca = veiculoDTO.Marca;
        veiculo.Ano = veiculoDTO.Ano;

        veiculoServico.Atualizar(veiculo);
        return Results.Ok(veiculo);
    }
    else
        return Results.NotFound();
});
app.MapDelete("/veiculos/{id}", ([FromRoute] int id, IveiculoServico veiculoServico) =>
{
    var veiculo = veiculoServico.ObterPorId(id);
    if (veiculo != null)
    {
        veiculoServico.Excluir(veiculo);
        return Results.NoContent();
    }
    else
        return Results.NotFound();
});
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
#endregion

