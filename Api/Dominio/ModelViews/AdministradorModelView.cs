using MinimalApi.Dominio.DTOs;

namespace MinimalApi.Dominio.ModelViews;

public record AdministradorModelView
{
   public int Id { get; init; }
    public string Email { get; init; } = default!;
    public string Senha { get; init; } = default!;
    public string Perfil { get; init; } = default!;
}